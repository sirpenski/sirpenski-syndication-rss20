// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssCoreItemEnclosure.cs
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


namespace Sirpenski.Syndication.Rss20.Core
{

    /// <summary>
    /// An item's enclosure element associates a media object such as an audio, image or video file with the item (optional).
    /// </summary>
    
    [Serializable]
    public class RssCoreItemEnclosure
    {
        public const string TAG_PARENT = "enclosure";
        public const string ATTR_URL = "url";
        public const string ATTR_LENGTH = "length";
        public const string ATTR_TYPE = "type";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// The url attribute identifies the URL of the file
        /// </summary>
        public string url { get; set; } = "";

        /// <summary>
        /// The length attribute indicates the size of the file in bytes
        /// </summary>
        public long length { get; set; } = 0;

        /// <summary>
        /// The type attribute identifies the file's MIME media type
        /// </summary>
        public string type { get; set; } = "";

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the RssItemEnclosure properties as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {

            XElement parEl = new XElement(TAG_PARENT);
            xUtil.AddAttr(parEl, ATTR_URL, url);
            xUtil.AddAttr(parEl, ATTR_LENGTH, length.ToString());
            xUtil.AddAttr(parEl, ATTR_TYPE, type);

            return parEl;

        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the properties of the RssItemEnclosure object from an XElement
        /// </summary>
        /// <param name="parEl">XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {
            if (parEl.Name.Namespace == XNamespace.None)
            {

                IEnumerable<XAttribute> lst = parEl.Attributes();
                foreach (XAttribute attr in lst)
                {
                    switch (attr.Name.LocalName)
                    {
                        case ATTR_URL:
                            url = xUtil.GetAttrStr(attr);
                            break;
                        case ATTR_LENGTH:
                            length = xUtil.GetAttrLong(attr);
                            break;
                        case ATTR_TYPE:
                            type = xUtil.GetAttrStr(attr);
                            break;
                    }
                }
            }


        }

    }
}




