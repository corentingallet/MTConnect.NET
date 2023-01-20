// Copyright (c) 2023 TrakHound Inc., All Rights Reserved.

// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using MTConnect.Devices.Compositions;

namespace MTConnect.Models.Compositions
{
    /// <summary>
    /// A mechanism that provides or applies a stretch or strain to another mechanism.
    /// </summary>
    public class TensionerModel : CompositionModel, ITensionerModel
    {
        public TensionerModel() 
        {
            Type = TensionerComposition.TypeId;
        }

        public TensionerModel(string compositionId)
        {
            Id = compositionId;
            Type = TensionerComposition.TypeId;
        }
    }
}
