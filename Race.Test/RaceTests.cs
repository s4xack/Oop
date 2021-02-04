using System;
using NUnit.Framework;
using Race.Transport;

namespace Race.Test
{
    [TestFixture]
    public class RaceTests
    {
        [Test]
        public void RaceWithoutVehicles()
        {
            var race = new Race<Vehicle>();
            Assert.Throws<InvalidOperationException>(() => race.Start(100));
        }
    }
}