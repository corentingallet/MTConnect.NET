// Copyright (c) 2023 TrakHound Inc., All Rights Reserved.

// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using MTConnect.Observations.Samples.Values;

namespace MTConnect.Models.DataItems
{
    /// <summary>
    /// The positive rate of change of velocity.
    /// </summary>
    public class AccelerationModel
    {
        /// <summary>
        /// The measured or reported value of an observation.
        /// </summary>
        public AccelerationValue Actual { get; set; }
        public IDataItemModel ActualDataItem { get; set; }

        /// <summary>
        /// Directive value including adjustments such as an offset or overrides.
        /// </summary>
        public AccelerationValue Commanded { get; set; }
        public IDataItemModel CommandedDataItem { get; set; }

        /// <summary>
        /// Directive value without offsets and adjustments.
        /// </summary>
        public AccelerationValue Programmed { get; set; }
        public IDataItemModel ProgrammedDataItem { get; set; }
    }
}
