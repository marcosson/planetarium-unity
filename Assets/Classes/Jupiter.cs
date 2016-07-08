using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class Jupiter : IPlanet
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

        public Jupiter()
        {
            _nAphelion = 196.30667949;
            _nPerihelion = 193.944856429;
            _eccentricity = 0.048498f;
            _nSemiMajorAxis = 195.262782713f;
            _orbitalPeriod = 4332.59f;
            _rotationPeriod = 0.413f;
            _mass = 1898;
            _diameter = 142984;
            _gravity = 23.1;
            _meanTemperature = -110;
            _numberOfMoons = 67;
            _alphaStart = PlanetFactory.GetStartingAlpha(273.8777f, 0.0000164505f);
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
