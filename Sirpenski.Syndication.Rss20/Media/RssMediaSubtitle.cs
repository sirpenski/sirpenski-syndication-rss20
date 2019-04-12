// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaSubtitle.cs
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
    /// Optional element for subtitle/CC link. It contains type and language attributes.
    /// </summary>
    [Serializable]
    public class RssMediaSubtitle
    {

        public const string TAG_PARENT = "subTitle";
        public const string ATTR_TYPE = "type";
        public const string ATTR_LANG = "lang";
        public const string ATTR_HREF = "href";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();


        /// <summary>
        /// mime type
        /// </summary>
        public string type { get; set; } = "";

        /// <summary>
        /// language
        /// </summary>
        public string lang { get; set; } = "";

        /// <summary>
        /// url pointing to subtitle
        /// </summary>
        public string href { get; set; } = "";


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Default Constructor
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaSubtitle() { }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the RssMediaSubtitle object properties as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = xUtil.CreateNSEl(TAG_PARENT, RSS.MEDIA_NS);

            if (type.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_TYPE, type);
            }

            if (lang.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_LANG, lang);
            }

            if (href.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_HREF, href);
            }

            return parEl;
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssMediaSubtitle properties from the parent XElement
        /// </summary>
        /// <param name="parEl">Parent XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {
            if (parEl.Name.Namespace == RSS.MEDIA_NS)
            {

                IEnumerable<XAttribute> lstAttr = parEl.Attributes();
                foreach (XAttribute attr in lstAttr)
                {
                    switch (attr.Name.LocalName)
                    {
                        case ATTR_TYPE:
                            type = xUtil.GetAttrStr(attr);
                            break;

                        case ATTR_LANG:
                            lang = xUtil.GetAttrStr(attr);
                            break;

                        case ATTR_HREF:
                            href = xUtil.GetAttrStr(attr);
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




