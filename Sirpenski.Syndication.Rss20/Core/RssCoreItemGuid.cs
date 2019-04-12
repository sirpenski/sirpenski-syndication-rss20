// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssCoreItemGuid.cs
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
    /// An item's guid object provides a string that uniquely identifies the item (optional).
    /// </summary>
    [Serializable]
    public class RssCoreItemGuid
    {
        public const string TAG_PARENT = "guid";
        public const string ATTR_ISPERMALINK = "isPermaLink";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();


        /// <summary>
        /// An item's guid element provides a string that uniquely identifies the item (optional).
        /// </summary>
        public string guid { get; set; } = "";

        /// <summary>
        /// Indicates whether the GUID is a permalink or not.
        /// </summary>
        public bool isPermalink { get; set; } = false;


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the RssCoreItemGuid properties as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = new XElement(TAG_PARENT, guid);
            if (isPermalink)
            {
                xUtil.AddAttr(parEl, ATTR_ISPERMALINK, "true");
            }

            return parEl;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssCoreItemGuid object properties from the XElement
        /// </summary>
        /// <param name="parEl">XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {

            if (parEl.Name.Namespace == XNamespace.None)
            {

                guid = xUtil.GetStr(parEl);

                IEnumerable<XAttribute> lstAttr = parEl.Attributes();
                foreach (XAttribute attr in lstAttr)
                {
                    switch (attr.Name.LocalName)
                    {
                        case ATTR_ISPERMALINK:
                            string s = xUtil.GetAttrStr(attr);
                            isPermalink = false;
                            if (string.Compare(s, "true") == 0)
                            {
                                isPermalink = true;
                            }
                            break;
                    }
                }
            }
        }

    }
}



