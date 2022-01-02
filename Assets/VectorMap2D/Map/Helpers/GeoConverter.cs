﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Helpers
{
    public static class GeoConvert
    {
        private const double MeterScale = 1000;//1;
        private const double InvMeterScale = 0.001;//1;

        public static float TileSize = 500;
        private const int EarthRadius = 6378137;
        private const int EarthRadiusPolar = 6356863;
        public static double InitialResolution = 2 * Math.PI * EarthRadius / TileSize;
        private const double OriginShift = 2 * Math.PI * EarthRadius / 2;

        public static Vector3d LatLonToMetersForEarth(double lat, double lon)
        {
            var p = new Vector2d();
            p.x = (lon * OriginShift / 180);
            p.y = (Math.Log(Math.Tan((90 + lat) * Math.PI / 360)) / (Math.PI / 180));
            p.y = (/*p.y*/lat * OriginShift / 180);
            return new Vector2d(p.x, p.y) * InvMeterScale / 10;
        }

    }
}
