﻿// Copyright (c) 2022 TrakHound Inc., All Rights Reserved.

// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using System.Collections.Generic;

namespace MTConnect.Formatters
{
    public struct FormattedDocumentResult
    {
        public string Content { get; set; }

        public string ContentType { get; set; }

        public bool Success { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public long ResponseDuration { get; set; }


        public FormattedDocumentResult(string content, string contentType, bool success = true, IEnumerable<string> errors = null)
        {
            Content = content;
            ContentType = contentType;
            Success = success;
            Errors = errors;
            ResponseDuration = 0;
        }

        public static FormattedDocumentResult Error(IEnumerable<string> errors = null)
        {
            return new FormattedDocumentResult(null, null, false, errors);
        }
    }
}
