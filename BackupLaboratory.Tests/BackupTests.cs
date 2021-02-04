using System;
using System.Collections.Generic;
using System.Linq;
using BackupLaboratory.Core.Enums;
using BackupLaboratory.Core.Models;
using BackupLaboratory.Core.Models.CleanAlgorithms;
using BackupLaboratory.Core.Models.Files;
using BackupLaboratory.Core.Models.Providers;
using BackupLaboratory.Core.Models.Repositories;
using BackupLaboratory.Core.Models.RestorePoints;
using BackupLaboratory.Core.Models.StorageAlgorithms;
using Moq;
using NUnit.Framework;

namespace BackupLaboratory.Tests
{
    public class BackupTests
    {
        private IFileRepository _fileRepository;
        private List<FileData> _files;

        private Int32 _archiveWrites;
        private Int32 _separateWrites;
        
        [SetUp]
        public void Setup()
        {
            _files = new List<FileData> {new FileData("storage/a.txt", 100), new FileData("storage/b.cpp", 100), new FileData("storage/c.exe", 100)};
            _archiveWrites = 0;
            _separateWrites = 0;
            
            var mockFileRepository = new Mock<IFileRepository>();
            mockFileRepository.Setup(x => x.Read(It.IsAny<String>()))
                .Returns((String x) => _files.First(file => file.Path == x));
            mockFileRepository.Setup(x => x.Write(It.IsAny<FileData>()))
                .Callback((FileData x) =>
                {
                    switch (x)
                    {
                        case ArchivedFileData:
                            _archiveWrites++;
                            break;
                        case PackedFileData y:
                            _separateWrites += y.Files.Count;
                            break;
                        default:
                            _separateWrites++;
                            break;
                    }
                });


            _fileRepository = mockFileRepository.Object;
        }
        
        [Test]
        public void RestorePointCreationTest()
        {
            Backup backup = new Backup(new SeparateStorageAlgorithm(), new CountCleanAlgorithm(1), _fileRepository, new DateTimeProvider());
            
            backup.Add(_files[0].Path);
            backup.Add(_files[1].Path);
            backup.CreateRestorePoint(RestoreType.Full);
            
            Assert.That(backup.RestorePoints.Count, Is.EqualTo(1));
        }
        
        [Test]
        public void SeparateStorageTest()
        {
            Backup backup = new Backup(new SeparateStorageAlgorithm(), new CountCleanAlgorithm(1), _fileRepository, new DateTimeProvider());
            
            backup.Add(_files[0].Path);
            backup.Add(_files[1].Path);
            backup.CreateRestorePoint(RestoreType.Full);

            RestorePoint restorePoint = backup.RestorePoints.First();
            
            Assert.That(restorePoint.StorageAlgorithmType, Is.EqualTo(StorageAlgorithmType.Separate));
            Assert.That(_archiveWrites, Is.EqualTo(0));
            Assert.That(_separateWrites, Is.EqualTo(2));

        }

        [Test]
        public void JointStorageTest()
        {
            Backup backup = new Backup(new JointStorageAlgorithm(), new CountCleanAlgorithm(1), _fileRepository, new DateTimeProvider());
            
            backup.Add(_files[0].Path);
            backup.Add(_files[1].Path);
            backup.CreateRestorePoint(RestoreType.Full);

            RestorePoint restorePoint = backup.RestorePoints.First();
            
            Assert.That(restorePoint.StorageAlgorithmType, Is.EqualTo(StorageAlgorithmType.Joint));
            Assert.That(restorePoint, Is.AssignableFrom(typeof(JointRestorePoint)));
            Assert.That(_archiveWrites, Is.EqualTo(1));
            Assert.That(_separateWrites, Is.EqualTo(0));
        }
        
        [Test]
        public void IncrementTest()
        {
            Backup backup = new Backup(new SeparateStorageAlgorithm(), new CountCleanAlgorithm(1), _fileRepository, new DateTimeProvider());
            
            backup.Add(_files[0].Path);
            backup.Add(_files[1].Path);
            backup.CreateRestorePoint(RestoreType.Full);
            backup.Add(_files[2].Path);
            backup.CreateRestorePoint(RestoreType.Increment);

            RestorePoint restorePoint = backup.RestorePoints.Last();

            Assert.That(restorePoint.RestoreType, Is.EqualTo(RestoreType.Increment));
            Assert.That(restorePoint.Files.Count, Is.EqualTo(1));
            Assert.That(_separateWrites, Is.EqualTo(3));

        }
        
        [Test]
        public void CountCleanAlgorithmTest()
        {
            Backup backup = new Backup(new JointStorageAlgorithm(), new CountCleanAlgorithm(1), _fileRepository, new DateTimeProvider());
            
            backup.Add(_files[0].Path);
            backup.Add(_files[1].Path);
            backup.CreateRestorePoint(RestoreType.Full);
            backup.CreateRestorePoint(RestoreType.Full);
            
            Assert.That(backup.RestorePoints.Count, Is.EqualTo(1));
        }
        
        [Test]
        public void CleanWithIncrementsTest()
        {
            Backup backup = new Backup(new JointStorageAlgorithm(), new CountCleanAlgorithm(1), _fileRepository, new DateTimeProvider());
            
            backup.Add(_files[0].Path);
            backup.Add(_files[1].Path);
            backup.CreateRestorePoint(RestoreType.Full);
            backup.Add(_files[2].Path);
            backup.CreateRestorePoint(RestoreType.Increment);
            
            Assert.That(backup.RestorePoints.Count, Is.EqualTo(2));
        }
        
        [Test]
        public void AllHybridNegativeCleanTest()
        {
            var cleanAlgorithm = new HybridCleanAlgorithm(new List<ICleanAlgorithm> {new CountCleanAlgorithm(1), new SizeCleanAlgorithm(500)}, HybridType.All);
            Backup backup = new Backup(new JointStorageAlgorithm(), cleanAlgorithm, _fileRepository, new DateTimeProvider());
            
            backup.Add(_files[0].Path);
            backup.Add(_files[1].Path);
            backup.CreateRestorePoint(RestoreType.Full);
            backup.Add(_files[2].Path);
            backup.CreateRestorePoint(RestoreType.Full);
            
            Assert.That(backup.RestorePoints.Count, Is.EqualTo(2));
        }
        
        [Test]
        public void AllHybridPositiveCleanTest()
        {
            var cleanAlgorithm = new HybridCleanAlgorithm(new List<ICleanAlgorithm> {new CountCleanAlgorithm(1), new SizeCleanAlgorithm(400)}, HybridType.All);
            Backup backup = new Backup(new JointStorageAlgorithm(), cleanAlgorithm, _fileRepository, new DateTimeProvider());
            
            backup.Add(_files[0].Path);
            backup.Add(_files[1].Path);
            backup.CreateRestorePoint(RestoreType.Full);
            backup.Add(_files[2].Path);
            backup.CreateRestorePoint(RestoreType.Full);
            
            Assert.That(backup.RestorePoints.Count, Is.EqualTo(1));
        }
        
        [Test]
        public void AnyHybridNegativeCleanTest()
        {
            var cleanAlgorithm = new HybridCleanAlgorithm(new List<ICleanAlgorithm> {new CountCleanAlgorithm(2), new SizeCleanAlgorithm(500)}, HybridType.Any);
            Backup backup = new Backup(new JointStorageAlgorithm(), cleanAlgorithm, _fileRepository, new DateTimeProvider());
            
            backup.Add(_files[0].Path);
            backup.Add(_files[1].Path);
            backup.CreateRestorePoint(RestoreType.Full);
            backup.Add(_files[2].Path);
            backup.CreateRestorePoint(RestoreType.Full);
            
            Assert.That(backup.RestorePoints.Count, Is.EqualTo(2));
        }
        
        [Test]
        public void AnyHybridPositiveCleanTest()
        {
            var cleanAlgorithm = new HybridCleanAlgorithm(new List<ICleanAlgorithm> {new CountCleanAlgorithm(1), new SizeCleanAlgorithm(500)}, HybridType.Any);
            Backup backup = new Backup(new JointStorageAlgorithm(), cleanAlgorithm, _fileRepository, new DateTimeProvider());
            
            backup.Add(_files[0].Path);
            backup.Add(_files[1].Path);
            backup.CreateRestorePoint(RestoreType.Full);
            backup.Add(_files[2].Path);
            backup.CreateRestorePoint(RestoreType.Full);
            
            Assert.That(backup.RestorePoints.Count, Is.EqualTo(1));
        }
    }
}