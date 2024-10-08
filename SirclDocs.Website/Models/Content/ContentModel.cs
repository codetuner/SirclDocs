﻿using SirclDocs.Website.Data.Content;

namespace SirclDocs.Website.Models.Content
{
    /// <summary>
    /// Model of a content document to render.
    /// </summary>
    public class ContentModel
    {
        /// <summary>
        /// The document to render content for.
        /// </summary>
        public required PublishedDocument Document { get; set; }
    }
}
