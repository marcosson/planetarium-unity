using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    /*
     * This interface will be used for obtaining peculiar
     * data for every planet that will be used either to
     * make it rotate around the Sun and on its axis and
     * to show in the info panel
     */
    public interface IPlanet
    {
        /* 
         * Variables of a planet
         */
        float Eccentricity { get; set; }
        float NormalizedSemiMajorAxis { get; set; }
        float OrbitalPeriod { get; set; }
        float RotationPeriod { get; set; }
        float AlphaStart { get; set; }
        double NormalizedAphelion { get; set; }
        double NormalizedPerihelion { get; set; }
        double Mass { get; set; }
        double Diameter { get; set; }
        double Gravity { get; set; }
        double MeanTemperature { get; set; }
        double NumberOfMoons { get; set; }

        /*
         * Methods to get the variables of a planet
         */
        float GetEccentricity();
        float GetNormalizedSemiMajorAxis();
        float GetOrbitalPeriod();
        float GetRotationPeriod();
        float GetAlphaStart();
        double GetNormalizedAphelion();
        double GetNormalizedPerihelion();
        double GetMass();
        double GetDiameter();
        double GetGravity();
        double GetTemperature();
        double GetNumberOfMoons();
    }
}
