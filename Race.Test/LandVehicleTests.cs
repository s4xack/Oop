using System;
using NUnit.Framework;
using Race.Transport.Land;

namespace Race.Test
{
    [TestFixture]
    public class LandVehicleTests
    {
        [TestCase(0, 0)]
        [TestCase(6, 1)]
        [TestCase(360, 60)]
        [TestCase(366, 71)]
        [TestCase(1086, 201)]
        public void AllTerrainBoots_Test(Double distance, Double time)
        {
            var boots = new AllTerrainBoots();
            Assert.That(boots.Ride(distance), Is.EqualTo(time));
        }

        [TestCase(0, 0)]
        [TestCase(10, 1)]
        [TestCase(300, 30)]
        [TestCase(310, 36)]
        [TestCase(910, 112)]
        public void BactrianCamel_Test(Double distance, Double time)
        {
            var bactrianCamel = new BactrianCamel();
            Assert.That(bactrianCamel.Ride(distance), Is.EqualTo(time));
        }

        [TestCase(0, 0)]
        [TestCase(15, 1)]
        [TestCase(120, 8)]
        [TestCase(135, 11)]
        [TestCase(255, 21)]
        public void Centaur_Test(Double distance, Double time)
        {
            var centaur = new Centaur();
            Assert.That(centaur.Ride(distance), Is.EqualTo(time));
        }
        
        [TestCase(0, 0)]
        [TestCase(40, 1)]
        [TestCase(400, 10)]
        [TestCase(440, 16)]
        [TestCase(840, 32.5)]
        [TestCase(1240, 50.5)]
        public void SpeedyCamel_Test(Double distance, Double time)
        {
            var speedyCamel = new SpeedyCamel();
            Assert.That(speedyCamel.Ride(distance), Is.EqualTo(time));
        }
    }
}