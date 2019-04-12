// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssContentEncoded.cs
// Copyright:  Copyright 2019. Paul F. Sirpenski.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ****************************************************************************************
// ****************************************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization;


namespace Sirpenski.Syndication.Rss20
{

    /// <summary>
    /// The content:encoded element defines the full content of an item (optional).
    /// </summary>
    [Serializable]
    public class RssContentEncoded
    {
        public const string TAG_PARENT = "encoded";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();


        /// <summary>
        /// Provide an item's full content.  More in-depth than description
        /// </summary>
        public string encoded { get; set; } = "";

        /// <summary>
        /// Default Constructor
        /// </summary>
        public RssContentEncoded() { }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the RssContentEncoded object properties as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = xUtil.CreateNSEl(TAG_PARENT, encoded, RSS.CONTENT_NS);

            return parEl;
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssContentEncoded object properties from a parent XElement
        /// </summary>
        /// <param name="parEl">Parent XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {
            if (parEl.Name.Namespace == RSS.CONTENT_NS)
            {
                encoded = xUtil.GetStr(parEl);
            }
        }


        [OnDeserializing]
        private void OnDeserialize(StreamingContext context)
        {
            xUtil = new RssXmlUtilities();
        }

    }
}




