// Copyright (c) 2023 TrakHound Inc., All Rights Reserved.

// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

namespace MTConnect.Devices.Configurations.SolidModel
{
    /// <summary>
    /// A SolidModel is a Configuration that references a file with the three-dimensional geometry of the Component or Composition.
    /// The geometry MAY have a transformation and a scale to position the Component with respect to the other Components.
    /// A geometry file can contain a set of assembled items, in this case, the SolidModel reference the id of the assembly model file and the specific item within that file.
    /// </summary>
    public class SolidModel : ISolidModel
    {
        /// <summary>
        /// The unique identifier for this entity within the MTConnectDevices document.     
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The associated model file if an item reference is used.    
        /// </summary>
        public string SolidModelIdRef { get; set; }

        /// <summary>
        /// The URL giving the location of the Solid Model.If not present, the model referenced in the solidModelIdRef is used.       
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// The reference to the item within the model within the related geometry.A solidModelIdRef MUST be given.
        /// </summary>
        public string ItemRef { get; set; }

        /// <summary>
        /// The format of the referenced document.
        /// </summary>
        public SolidModelMediaType MediaType { get; set; }

        /// <summary>
        /// A reference to the coordinate system for this SolidModel.
        /// </summary>
        public string CoordinateSystemIdRef { get; set; }

        /// <summary>
        /// The translation of the origin to the position and orientation.
        /// </summary>
        public ITransformation Transformation { get; set; }

        /// <summary>
        /// The SolidModel Scale is either a single multiplier applied to all three dimensions or a three space multiplier given in the X, Y, and Z dimensions in the coordinate system used for the SolidModel.
        /// </summary>
        public string Scale { get; set; }

        /// <summary>
        /// Native units of measurement for the reported value of the data item.
        /// </summary>
        public string NativeUnits { get; set; }

        /// <summary>
        /// Unit of measurement for the reported value of the data item.
        /// </summary>
        public string Units { get; set; }
    }
}
