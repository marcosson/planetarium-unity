using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class Venus : IPlanet
    {
        private double _nAphelion;
        private double _nPerihelion;
        private float _eccentricity;
        private float _nSemiMajorAxis;
        private float _orbitalPeriod;
        private float _rotationPeriod;
        private float _alphaStart;
        private double _mass;
        private double _diameter;
        private double _gravity;
        private double _meanTemperature;
        private double _numberOfMoons;

        public Venus()
        {
            _nAphelion = 158.073248827;
            _nPerihelion = 157.908709321;
            _eccentricity = 0.006772f;
            _nSemiMajorAxis = 158.024891963f;
            _orbitalPeriod = 224.701f;
            _rotationPeriod = -243.025f;
            _mass = 4.87;
            _diameter = 12104;
            _gravity = 8.9;
            _meanTemperature = 464;
            _numberOfMoons = 0;
            _alphaStart = PlanetFactory.GetStartingAlpha(54.8910f, 0.0000138374f);
        }

        public double NormalizedAphelion
        {
            get { return _nAphelion; }
            set { _nAphelion = value; }
        }

        public double NormalizedPerihelion
        {
            get { return _nPerihelion; }
            set { _nPerihelion = value; }
        }

        public float Eccentricity
        {
            get { return _eccentricity; }
            set { _eccentricity = value; }
        }

        public float NormalizedSemiMajorAxis
        {
            get { return _nSemiMajorAxis; }
            set { _nSemiMajorAxis = value; }
        }

        public float OrbitalPeriod
        {
            get { return _orbitalPeriod; }
            set { _orbitalPeriod = value; }
        }

        public float RotationPeriod
        {
            get { return _rotationPeriod; }
            set { _rotationPeriod = value; }
        }

        public float AlphaStart
        {
            get { return _alphaStart; }
            set { _alphaStart = value; }
        }

        public double Mass
        {
            get { return _mass; }
            set { _mass = value; }
        }

        public double Diameter
        {
            get { return _diameter; }
            set { _mass = value; }
        }

        public double Gravity
        {
            get { return _gravity; }
            set { _mass = value; }
        }

        public double MeanTemperature
        {
            get { return _meanTemperature; }
            set { _mass = value; }
        }

        public double NumberOfMoons
        {
            get { return _numberOfMoons; }
            set { _mass = value; }
        }

        public double GetNormalizedAphelion()
        {
            return _nAphelion;
        }

        public double GetNormalizedPerihelion()
        {
            return _nPerihelion;
        }

        public float GetEccentricity()
        {
            return _eccentricity;
        }

        public float GetNormalizedSemiMajorAxis()
        {
            return _nSemiMajorAxis;
        }

        public float GetOrbitalPeriod()
        {
            return _orbitalPeriod;
        }

        public float GetRotationPeriod()
        {
            return _rotationPeriod;
        }

        public float GetAlphaStart()
        {
            return _alphaStart;
        }

        public double GetMass()
        {
            return _mass;
        }

        public double GetDiameter()
        {
            return _diameter;
        }

        public double GetGravity()
        {
            return _gravity;
        }

        public double GetTemperature()
        {
            return _meanTemperature;
        }

        public double GetNumberOfMoons()
        {
            return _numberOfMoons;
        }
    }
}
