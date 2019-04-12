﻿// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaRating.cs
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
using System.Runtime.Serialization;
using System.Diagnostics;


namespace Sirpenski.Syndication.Rss20
{

    /// <summary>
    /// The RssMediaRating object defines a media rating
    /// </summary>
    [Serializable]
    public class RssMediaRating
    {

        public const string TAG_PARENT = "rating";
        public const string ATTR_SCHEME = "scheme";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// The Rating
        /// </summary>
        public string rating { get; set; } = "";

        /// <summary>
        /// scheme is the URI that identifies the rating scheme. It is an optional
        /// </summary>
        public string scheme { get; set; } = "";


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Default Constructor
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaRating() { }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the properties of the RssMediaRating object as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = xUtil.CreateNSEl(TAG_PARENT, rating, RSS.MEDIA_NS);
            if (scheme.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_SCHEME, scheme);
            }
            return parEl;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssMediaRating object properties with the contents of the XElement
        /// </summary>
        /// <param name="parEl">XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {
            if (parEl.Name.Namespace == RSS.MEDIA_NS)
            {
                rating = xUtil.GetStr(parEl);

                IEnumerable<XAttribute> lstAttr = parEl.Attributes();
                foreach (XAttribute attr in lstAttr)
                {
                    switch (attr.Name.LocalName)
                    {
                        case ATTR_SCHEME:
                            scheme = xUtil.GetAttrStr(attr);
                            break;
                    }
                }

            }

        }


        [OnDeserializing]
        private void OnDeserialize(StreamingContext context)
        {
            xUtil = new RssXmlUtilities();
        }
    }
}




