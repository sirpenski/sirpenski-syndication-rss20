// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssCoreChannelCategory.cs
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
    /// The category object identifies a category or tag to which the feed belongs (optional).
    /// </summary>
    [Serializable]
    public class RssCoreChannelCategory
    {

        public const string TAG_PARENT = "category";
        public const string ATTR_DOMAIN = "domain";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// The category.
        /// </summary>
        public string category { get; set; } = "";

        /// <summary>
        /// The domain identifies the taxonomy in which the category is placed.
        /// </summary>
        public string domain { get; set; } = "";


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the object as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {

            XElement parEl = new XElement(TAG_PARENT, (string)category);
            if (domain.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_DOMAIN, domain);
            }
            return parEl;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the object from an XElement
        /// </summary>
        /// <param name="parEl">Parent XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {
            // ensure we are getting the blank namespace
            if (parEl.Name.Namespace == XNamespace.None)
            {

                category = xUtil.GetStr(parEl);

                IEnumerable<XAttribute> lstAttr = parEl.Attributes();
                foreach (XAttribute attr in lstAttr)
                {
                    switch (attr.Name.LocalName)
                    {
                        case ATTR_DOMAIN:
                            domain = xUtil.GetAttrStr(attr);
                            break;
                    }
                }

            }

        }
    }
}




