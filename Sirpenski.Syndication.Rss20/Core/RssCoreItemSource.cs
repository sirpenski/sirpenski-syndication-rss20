// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssCoreItemSource.cs
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
    /// An item's source object indicates the fact that the item has been republished from another RSS feed (optional).
    /// </summary>
    [Serializable]
    public class RssCoreItemSource
    {

        public const string TAG_PARENT = "source";
        public const string ATTR_URL = "url";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// The title of the source RSS Feed
        /// </summary>
        public string source { get; set; } = "";

        /// <summary>
        /// The url of the source feed.
        /// </summary>
        public string url { get; set; } = "";


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the RssCoreItemSource object properties as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {

            XElement parEl = new XElement(TAG_PARENT, source);
            if (url.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_URL, url);
            }

            return parEl;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssCoreItemSource properties from the XElement
        /// </summary>
        /// <param name="parEl">XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {

            if (parEl.Name.Namespace == XNamespace.None)
            {

                source = xUtil.GetStr(parEl);
                IEnumerable<XAttribute> lstAttr = parEl.Attributes();

                foreach (XAttribute attr in lstAttr)
                {
                    switch (attr.Name.LocalName)
                    {
                        case ATTR_URL:
                            url = xUtil.GetAttrStr(attr);
                            break;
                    }
                }
            }
        }
    }
}




