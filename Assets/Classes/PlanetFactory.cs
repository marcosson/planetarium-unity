using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class PlanetFactory
    {
        /*
         * Return a different planet according to the planet name
         * that the Factory receives
         */
        public static IPlanet GetPlanet (string planetName)
        {
            switch(planetName)
            {
                case "Mercury": return new Mercury();
                case "Venus": return new Venus();
                case "Earth": return new Earth();
                case "Mars": return new Mars();
                case "Jupiter": return new Jupiter();
                case "Saturn": return new Saturn();
                case "Uranus": return new Uranus();
                case "Neptune": return new Neptune();
                default: return null;
            }
        }

        /*
         * Returns the current position of the planet in the universe,
         * the position will be in function of the date
         */
        public static float GetStartingAlpha(float par1, float par2)
        {
            int Y = DateTime.Now.Year;
            int M = DateTime.Now.Month;
            int D = DateTime.Now.Day;
            float d = 367 * Y - (7 * (Y + ((M + 9) / 12))) / 4 + (275 * M) / 9 + D - 730530;
            return par1 + par2 * d;
        }
    }
}
