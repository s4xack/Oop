using System;
using NUnit.Framework;
using Race.Transport.Air;

namespace Race.Test
{
    public class AirVenicleTests
    {
        [TestCase(0, 0)]
        [TestCase(20, 1)]
        [TestCase(1000, 49.5)]
        [TestCase(2000, 98)]
        public void Broom_Test(Double distance, Double time)
        {
            var broom = new Broom();
            Assert.That(broom.Ride(distance), Is.EqualTo(time));
        }
        
        [TestCase(0, 0)]
        [TestCase(900, 90)]
        [TestCase(4000, 388)]
        [TestCase(9000, 810)]
        [TestCase(11000, 1045)]
        public void MagicCarpet_Test(Double distance, Double time)
        {
            var magicCarpet = new MagicCarpet();
            Assert.That(magicCarpet.Ride(distance), Is.EqualTo(time));
        }

        [TestCase(0, 0)]
        [TestCase(800, 94)]
        public void Mortar_Test(Double distance, Double time)
        {
            var mortar = new Mortar();
            Assert.That(mortar.Ride(distance), Is.EqualTo(time));
        }

    }
}