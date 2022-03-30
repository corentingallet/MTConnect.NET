// Copyright (c) 2022 TrakHound Inc., All Rights Reserved.

// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using MTConnect.Devices.Compositions;

namespace MTConnect.Models.Compositions
{
    /// <summary>
    /// A container for liquid or powdered materials.
    /// </summary>
    public class VatModel : CompositionModel, IVatModel
    {
        public VatModel() 
        {
            Type = VatComposition.TypeId;
        }

        public VatModel(string compositionId)
        {
            Id = compositionId;
            Type = VatComposition.TypeId;
        }
    }
}