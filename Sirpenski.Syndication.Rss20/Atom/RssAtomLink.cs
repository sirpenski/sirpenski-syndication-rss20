// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssAtomLink.cs
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
    /// The atom:link element defines a relationship between a web resource (such as a page) and an RSS channel or 
    /// item (optional). The most common use is to identify an HTML representation of an entry in an RSS or Atom feed.
    /// </summary>
    [Serializable]
    public class RssAtomLink
    {
        public const string TAG_PARENT = "link";
        public const string ATTR_HREF = "href";
        public const string ATTR_REL = "rel";
        public const string ATTR_TYPE = "type";
        public const string ATTR_HREFLANG = "hreflang";
        public const string ATTR_TITLE = "title";
        public const string ATTR_LENGTH = "length";

        [NonSerialized]
        RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>Contains the URL of the related resource</summary>
        public string href { get; set; } = "";

        /// <summary>Contains a keyword that identifies the nature of the relationship between the linked resouce and the element</summary>
        public string rel { get; set; } = "";

        /// <summary>Identifies the resource's MIME media type</summary>
        public string type { get; set; } = "";

        ///<summary>Identifies the language used by the related resource using an HTML language code</summary>
        public string hreflang { get; set; } = "";

        /// <summary>Provides a human-readable description of the resource</summary>
        public string title { get; set; } = "";

        /// <summary>Contains the resource's size, in bytes</summary>
        public int length { get; set; } = 0;



        /// <summary>
        /// Default Constructor
        /// </summary>
        public RssAtomLink() { }




        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the RssAtomLink object properties as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = xUtil.CreateNSEl(TAG_PARENT, RSS.ATOM_NS);

            xUtil.AddAttr(parEl, ATTR_HREF, href);


            if (rel.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_REL, rel);
            }
            if (type.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_TYPE, type);
            }
            if (hreflang.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_HREFLANG, hreflang);
            }
            if (title.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_TITLE, title);
            }
            if (length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_LENGTH, length);
            }


            return parEl;
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssAtomLink object properties with the contents of the parent XElement
        /// </summary>
        /// <param name="parEl">Parent Element</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {
            if (parEl.Name.Namespace == RSS.ATOM_NS)
            {

                IEnumerable<XAttribute> lstAttr = parEl.Attributes();
                foreach (XAttribute attr in lstAttr)
                {
                    switch (attr.Name.LocalName)
                    {
                        case ATTR_HREF:
                            href = xUtil.GetAttrStr(attr);
                            break;
                        case ATTR_REL:
                            rel = xUtil.GetAttrStr(attr);
                            break;
                        case ATTR_TYPE:
                            type = xUtil.GetAttrStr(attr);
                            break;
                        case ATTR_HREFLANG:
                            hreflang = xUtil.GetAttrStr(attr);
                            break;
                        case ATTR_TITLE:
                            title = xUtil.GetAttrStr(attr);
                            break;
                        case ATTR_LENGTH:
                            length = xUtil.GetAttrInt(attr);
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




