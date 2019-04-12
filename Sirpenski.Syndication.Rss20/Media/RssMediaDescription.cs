// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaDescription.cs
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
    /// Short description describing the media object
    /// </summary>
    [Serializable]
    public class RssMediaDescription
    {

        public const string TAG_PARENT = "description";
        public const string ATTR_TYPE = "type";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// Short description describing the media object
        /// </summary>
        public string description { get; set; } = "";

        /// <summary>
        /// type specifies the type of text embedded
        /// </summary>
        public string type { get; set; } = "";


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Default Constructor
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaDescription() { }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the RssMediaDescription object properties as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = xUtil.CreateNSEl(TAG_PARENT, description, RSS.MEDIA_NS);
            if (type.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_TYPE, type);
            }
            return parEl;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the properties of the RssMediaDescription object with the contents of the XElement
        /// </summary>
        /// <param name="parEl">XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {
            if (parEl.Name.Namespace == RSS.MEDIA_NS)
            {
                description = xUtil.GetStr(parEl);
                IEnumerable<XAttribute> lstAttr = parEl.Attributes();
                foreach (XAttribute attr in lstAttr)
                {
                    switch (attr.Name.LocalName)
                    {
                        case ATTR_TYPE:
                            type = xUtil.GetAttrStr(attr);
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




