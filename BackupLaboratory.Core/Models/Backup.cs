using System;
using System.Collections.Generic;
using System.Linq;
using BackupLaboratory.Core.Enums;
using BackupLaboratory.Core.Models.CleanAlgorithms;
using BackupLaboratory.Core.Models.Files;
using BackupLaboratory.Core.Models.Providers;
using BackupLaboratory.Core.Models.Repositories;
using BackupLaboratory.Core.Models.RestorePoints;
using BackupLaboratory.Core.Models.StorageAlgorithms;

namespace BackupLaboratory.Core.Models
{
    public class Backup
    {
        public Guid Id { get; }
        public Int64 Size => _restorePoints.Sum(point => point.Size);
        public IReadOnlyCollection<RestorePoint> RestorePoints => _restorePoints;
        private String BackupPath => $"_/Backup_{Id}";
        
        private readonly List<String> _watchedFilePaths;
        private List<RestorePoint> _restorePoints;

        private readonly IStorageAlgorithm _storageAlgorithm;
        private readonly ICleanAlgorithm _cleanAlgorithm;

        private readonly IFileRepository _fileRepository;

        private readonly DateTimeProvider _dateTimeProvider;

        public Backup(IStorageAlgorithm storageAlgorithm, ICleanAlgorithm cleanAlgorithm, IFileRepository fileRepository, DateTimeProvider dateTimeProvider)
        {
            _storageAlgorithm = storageAlgorithm ?? throw new ArgumentNullException(nameof(storageAlgorithm));
            _cleanAlgorithm = cleanAlgorithm ?? throw new ArgumentNullException(nameof(cleanAlgorithm));
            _fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));

            Id = Guid.NewGuid();
            _watchedFilePaths = new List<String>();
            _restorePoints = new List<RestorePoint>();
        }

        public void Add(String filePath)
        {
            _watchedFilePaths.Add(filePath);
        }

        public void CreateRestorePoint(RestoreType restoreType)
        {
            DateTime creationTime = _dateTimeProvider.GetUtcNow();
            IEnumerable<FileData> watchedFiles = _watchedFilePaths.Select(path => _fileRepository.Read(path));
            List<FileData> files = restoreType switch
            {
                RestoreType.Full => watchedFiles.ToList(),
                RestoreType.Increment => 
                    _restorePoints.Count == 0 
                        ? throw new ArgumentException()
                        : watchedFiles.Select(GetDiff).ToList(),
                _ => throw new ArgumentException()
            };

            RestorePoint restorePoint = _storageAlgorithm.PackRestorePoint(BackupPath, files, restoreType, creationTime);
            _restorePoints.Add(restorePoint);
            _fileRepository.Write(restorePoint.ToPackedFileData());
            
            Clean();
        }

        private FileData GetDiff(FileData file)
        {
            List<RestoredFileData> fileRestores = _restorePoints
                .Select(point => point.GetRestoredFormPath(file.Path))
                .Where(restoredFile => !restoredFile.IsNull)
                .Reverse()
                .ToList();

            if (fileRestores.Count == 0)
                return file;

            RestoredFileData lastInit = fileRestores
                .First(restoredFile => restoredFile.UpdateType == FileUpdateType.Init);
            List<FileData> intermediateDiffFiles = fileRestores
                .TakeWhile(restoredFile => restoredFile.UpdateType == FileUpdateType.Diff)
                .Cast<FileData>()
                .ToList();

            return file.GetDiff(lastInit, intermediateDiffFiles);
        }

        private void Clean()
        {
            Int32 suggestedForRemoveCount = _cleanAlgorithm.CalculatePointsForClean(_restorePoints);
            
            List<RestorePoint> pointsForRemove = _restorePoints.Take(suggestedForRemoveCount).ToList();
            if (suggestedForRemoveCount == _restorePoints.Count || _restorePoints[suggestedForRemoveCount].RestoreType == RestoreType.Increment)
                pointsForRemove = _restorePoints
                    .Take(suggestedForRemoveCount)
                    .Reverse()
                    .SkipWhile(point => point.RestoreType == RestoreType.Increment)
                    .SkipLast(1)
                    .ToList();


            _restorePoints = _restorePoints.Skip(pointsForRemove.Count).ToList();
            
            pointsForRemove.ForEach(point => _fileRepository.Remove(point.Path));
        }
    }
}