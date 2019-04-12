// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaText.cs
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
    /// The RssMediaText object allows the inclusion of a text transcript, closed captioning or lyrics of the media content
    /// </summary>
    [Serializable]
    public class RssMediaText
    {

        public const string TAG_PARENT = "text";
        public const string ATTR_TYPE = "type";
        public const string ATTR_LANG = "lang";
        public const string ATTR_START = "start";
        public const string ATTR_END = "end";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// Text defining media item
        /// </summary>
        public string text { get; set; } = "";

        /// <summary>
        /// type specifies the type of text embedded
        /// </summary>
        public string type { get; set; } = "";

        /// <summary>
        /// lang is the primary language encapsulated in the media object
        /// </summary>
        public string lang { get; set; } = "";


        /// <summary>
        /// start specifies the start time offset that the text starts being relevant to the media object.
        /// </summary>
        public TimeSpan start { get; set; } = TimeSpan.MinValue;

        /// <summary>
        /// end specifies the end time that the text is relevant.
        /// </summary>
        public TimeSpan end { get; set; } = TimeSpan.MinValue;


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Default Constructor
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaText() { }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the RssMediaText object's properties as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = xUtil.CreateNSEl(TAG_PARENT, text, RSS.MEDIA_NS);

            if (type.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_TYPE, type);
            }

            if (lang.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_LANG, lang);
            }

            if (start > TimeSpan.MinValue)
            {
                xUtil.AddAttr(parEl, ATTR_START, start.ToString("hh\\:mm\\:ss\\.fff"));
            }

            if (end > TimeSpan.MinValue)
            {
                xUtil.AddAttr(parEl, ATTR_END, end.ToString("hh\\:mm\\:ss\\.fff"));
            }


            return parEl;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssMediaText object properties with the contents of the XElement
        /// </summary>
        /// <param name="parEl">Parent XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {
            if (parEl.Name.Namespace == RSS.MEDIA_NS)
            {
                text = xUtil.GetStr(parEl);

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

                        case ATTR_START:
                            start = xUtil.GetAttrTimeSpan(attr);
                            break;

                        case ATTR_END:
                            end = xUtil.GetAttrTimeSpan(attr);
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



