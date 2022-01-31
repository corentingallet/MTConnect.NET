// Copyright (c) 2022 TrakHound Inc., All Rights Reserved.

// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace MTConnect.Devices
{
    /// <summary>
    /// Reference is a pointer to information that is associated with another Structural Element defined elsewhere in the XML document for a piece of equipment. 
    /// That information may be data from the other element or the entire structure of that element.
    /// </summary>
    public class Reference
    {
        /// <summary>
        /// A pointer to the id attribute of the element that contains the information to be associated with this XML element.    
        /// </summary>
        [XmlAttribute("idRef")]
        [JsonPropertyName("idRef")]
        public string IdRef { get; set; }

        /// <summary>
        /// The optional name of the element. Only informative.    
        /// </summary>
        [XmlAttribute("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}