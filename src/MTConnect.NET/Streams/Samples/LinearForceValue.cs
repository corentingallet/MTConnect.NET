// Copyright (c) 2022 TrakHound Inc., All Rights Reserved.

// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

namespace MTConnect.Streams.Samples
{
    /// <summary>
    /// A Force applied to a mass in one direction only
    /// </summary>
    public class LinearForceValue : SampleValue
    {
        protected override double MetricConversion => 4.448221615254;
        protected override double InchConversion => 0.2248089431;
        protected override string MetricUnits => "NEWTON";
        protected override string InchUnits => "POUND";


        public LinearForceValue(double force, UnitSystem unitSystem = UnitSystem.METRIC)
        {
            Value = force;
            UnitSystem = unitSystem;
        }
    }
}