// Copyright (c) 2023 TrakHound Inc., All Rights Reserved.

// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MTConnect.Configurations
{
    /// <summary>
    /// Configuration for an MTConnect Http Gateway Agent Application
    /// </summary>
    public class HttpAgentGatewayApplicationConfiguration : HttpAgentApplicationConfiguration
    {
        /// <summary>
        /// List of MTConnect Http Clients to read from
        /// </summary>
        [JsonPropertyName("clients")]
        public List<HttpClientConfiguration> Clients { get; set; }
    }
}
