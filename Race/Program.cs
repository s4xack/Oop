using System;
using Race.Transport;
using Race.Transport.Air;
using Race.Transport.Land;

namespace Race
{
    class Program
    {
        static void Main()
        {
            var race = new Race<Vehicle>();
            race.Register(new Centaur());
            race.Register(new Broom());

            Console.WriteLine(race.Start(100));
        }
    }
}
