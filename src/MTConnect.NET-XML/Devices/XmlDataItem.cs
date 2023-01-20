// Copyright (c) 2023 TrakHound Inc., All Rights Reserved.

// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using MTConnect.Devices.Configurations.Relationships;
using MTConnect.Devices.DataItems;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace MTConnect.Devices.Xml
{
    public class XmlDataItem
    {
        private static readonly XmlSerializer _serializer = new XmlSerializer(typeof(XmlDataItem));


        [XmlAttribute("category")]
        public DataItemCategory DataItemCategory { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("coordinateSystem")]
        public DataItemCoordinateSystem CoordinateSystem { get; set; }

        [XmlAttribute("coordinateSystemIdRef")]
        public string CoordinateSystemIdRef { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("compositionId")]
        public string CompositionId { get; set; }

        [XmlAttribute("nativeScale")]
        public double NativeScale { get; set; }

        [XmlAttribute("nativeUnits")]
        public string NativeUnits { get; set; }

        [XmlAttribute("subType")]
        public string SubType { get; set; }

        [XmlAttribute("statistic")]
        public DataItemStatistic Statistic { get; set; }

        [XmlAttribute("units")]
        public string Units { get; set; }

        [XmlAttribute("sampleRate")]
        public double SampleRate { get; set; }

        [XmlAttribute("discrete")]
        public string Discrete { get; set; }

        [XmlAttribute("representation")]
        public DataItemRepresentation Representation { get; set; }

        [XmlAttribute("significantDigits")]
        public int SignificantDigits { get; set; }

        [XmlElement("Source")]
        public XmlSource Source { get; set; }

        [XmlElement("Constraints")]
        public XmlConstraints Constraints { get; set; }

        [XmlArray("Filters")]
        [XmlArrayItem("Filter")]
        public List<XmlFilter> Filters { get; set; }

        [XmlElement("InitialValue")]
        public string InitialValue { get; set; }

        [XmlElement("ResetTrigger")]
        public DataItemResetTrigger ResetTrigger { get; set; }

        [XmlElement("Definition")]
        public XmlDataItemDefinition Definition { get; set; }

        [XmlArray("Relationships")]
        [XmlArrayItem("DataItemRelationship", typeof(XmlDataItemRelationship))]
        [XmlArrayItem("SpecificationRelationship", typeof(XmlSpecificationRelationship))]
        public List<XmlRelationship> Relationships { get; set; }


        public DataItem ToDataItem()
        {
            var dataItem = DataItem.Create(Type);
            if (dataItem == null) dataItem = new DataItem();

            dataItem.Category = DataItemCategory;
            dataItem.Id = Id;
            dataItem.Name = Name;
            dataItem.Type = Type;
            dataItem.SubType = SubType;
            dataItem.NativeUnits = NativeUnits;
            dataItem.NativeScale = NativeScale;
            dataItem.SampleRate = SampleRate;
            dataItem.CompositionId = CompositionId;
            dataItem.Representation = Representation;
            dataItem.ResetTrigger = ResetTrigger;
            dataItem.CoordinateSystem = CoordinateSystem;
            dataItem.CoordinateSystemIdRef = CoordinateSystemIdRef;
            dataItem.Units = Units;
            dataItem.Statistic = Statistic;
            dataItem.SignificantDigits = SignificantDigits;
            dataItem.InitialValue = InitialValue;
            dataItem.Discrete = Discrete.ToBoolean();

            // Source
            if (Source != null) dataItem.Source = Source.ToSource();

            // Constraints
            if (Constraints != null) dataItem.Constraints = Constraints.ToConstraints();

            // Definition
            if (Definition != null) dataItem.Definition = Definition.ToDefinition();

            // Filters
            if (!Filters.IsNullOrEmpty())
            {
                var filters = new List<IFilter>();
                foreach (var filter in Filters)
                {
                    filters.Add(filter.ToFilter());
                }
                dataItem.Filters = filters;
            }

            // Relationships
            if (!Relationships.IsNullOrEmpty())
            {
                var relationships = new List<IRelationship>();
                foreach (var relationship in Relationships)
                {
                    relationships.Add(relationship.ToRelationship());
                }
                dataItem.Relationships = relationships;
            }

            return dataItem;
        }


        public static IDataItem FromXml(byte[] xmlBytes)
        {
            if (xmlBytes != null && xmlBytes.Length > 0)
            {
                try
                {
                    using (var textReader = new MemoryStream(xmlBytes))
                    {
                        using (var xmlReader = XmlReader.Create(textReader))
                        {
                            var xmlObj = (XmlDataItem)_serializer.Deserialize(xmlReader);
                            if (xmlObj != null)
                            {
                                return xmlObj.ToDataItem();
                            }
                        }
                    }
                }
                catch { }
            }

            return null;
        }


        public static void WriteXml(XmlWriter writer, IDataItem dataItem, bool outputComments = false)
        {
            if (dataItem != null)
            {
                // Add Comments
                if (outputComments && dataItem != null)
                {
                    // Write DataItem Type Description as Comment
                    if (!string.IsNullOrEmpty(dataItem.TypeDescription))
                    {
                        writer.WriteComment($"Type = {dataItem.Type} : {dataItem.TypeDescription}");
                    }

                    // Write DataItem SubType Description as Comment
                    if (!string.IsNullOrEmpty(dataItem.SubType) && !string.IsNullOrEmpty(dataItem.SubTypeDescription))
                    {
                        writer.WriteComment($"SubType = {dataItem.SubType} : {dataItem.SubTypeDescription}");
                    }
                }

                writer.WriteStartElement("DataItem");

                // Write DataItem Properties
                writer.WriteAttributeString("category", dataItem.Category.ToString());
                writer.WriteAttributeString("id", dataItem.Id);
                if (!string.IsNullOrEmpty(dataItem.Name)) writer.WriteAttributeString("name", dataItem.Name);
                writer.WriteAttributeString("type", dataItem.Type);
                if (!string.IsNullOrEmpty(dataItem.SubType)) writer.WriteAttributeString("subType", dataItem.SubType);
                if (dataItem.CoordinateSystem != DataItemCoordinateSystem.MACHINE) writer.WriteAttributeString("coordinateSystem", dataItem.CoordinateSystem.ToString());
                if (!string.IsNullOrEmpty(dataItem.CoordinateSystemIdRef)) writer.WriteAttributeString("coordinateSystemIdRef", dataItem.CoordinateSystemIdRef);
                if (dataItem.NativeScale > 0) writer.WriteAttributeString("nativeScale", dataItem.NativeScale.ToString());
                if (!string.IsNullOrEmpty(dataItem.NativeUnits)) writer.WriteAttributeString("nativeUnits", dataItem.NativeUnits);
                if (!string.IsNullOrEmpty(dataItem.Units)) writer.WriteAttributeString("units", dataItem.Units);
                if (dataItem.Statistic != DataItemStatistic.NONE) writer.WriteAttributeString("statistic", dataItem.Statistic.ToString());
                if (dataItem.SampleRate > 0) writer.WriteAttributeString("sampleRate", dataItem.SampleRate.ToString());
                if (dataItem.Discrete) writer.WriteAttributeString("discrete", dataItem.Discrete.ToString());
                if (dataItem.Representation != DataItemRepresentation.VALUE) writer.WriteAttributeString("representation", dataItem.Representation.ToString());
                if (dataItem.SignificantDigits > 0) writer.WriteAttributeString("significantDigits", dataItem.SignificantDigits.ToString());
                if (!string.IsNullOrEmpty(dataItem.CompositionId)) writer.WriteAttributeString("compositionId", dataItem.CompositionId);
                if (!string.IsNullOrEmpty(dataItem.InitialValue)) writer.WriteAttributeString("initialValue", dataItem.InitialValue);
                if (dataItem.ResetTrigger != DataItemResetTrigger.NONE) writer.WriteAttributeString("resetTrigger", dataItem.ResetTrigger.ToString());


                // Write Source
                XmlSource.WriteXml(writer, dataItem.Source);

                // Write Constraints
                XmlConstraints.WriteXml(writer, dataItem.Constraints);

                // Write Filters
                if (!dataItem.Filters.IsNullOrEmpty())
                {
                    writer.WriteStartElement("Filters");
                    foreach (var filter in dataItem.Filters)
                    {
                        XmlFilter.WriteXml(writer, filter);
                    }
                    writer.WriteEndElement();
                }

                // Write Definition
                XmlDataItemDefinition.WriteXml(writer, dataItem.Definition);

                // Write Relationships
                if (!dataItem.Relationships.IsNullOrEmpty())
                {
                    writer.WriteStartElement("Relationships");
                    foreach (var relationship in dataItem.Relationships)
                    {
                        XmlRelationship.WriteXml(writer, relationship);
                    }
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
        }
    }
}
