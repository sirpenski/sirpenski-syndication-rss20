// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssCoreChannelTextInput.cs
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
    /// The textInput class defines a form to submit a text query to the feed's publisher over the Common Gateway Interface (CGI) (optional).
    /// The class must contain a description, link, name and title child element.
    /// </summary>
    [Serializable]
    public class RssCoreChannelTextInput
    {

        public const string TAG_PARENT = "textInput";
        public const string TAG_TITLE = "title";
        public const string TAG_DESCRIPTION = "description";
        public const string TAG_NAME = "name";
        public const string TAG_LINK = "link";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// The title property holds character data that provides the name of the gateway (required).
        /// </summary>
        public string title { get; set; } = "";

        /// <summary>
        /// The description property holds character data that provides a human-readable characterization or summary of the gateway (required).
        /// </summary>
        public string description { get; set; } = "";

        /// <summary>
        /// The name property holds character data that provides a human-readable characterization of the name of the gateway (required).
        /// </summary>
        public string name { get; set; } = "";

        ///<summary>
        /// The link element identifies the URL of the web site associated with the gateway (required)
        ///</summary>
        public string link { get; set; } = "";

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the properties of the RssCoreChannelTextInput class as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = xUtil.CreateEl(TAG_PARENT);
            xUtil.AddEl(parEl, TAG_TITLE, title);
            xUtil.AddEl(parEl, TAG_DESCRIPTION, description);
            xUtil.AddEl(parEl, TAG_NAME, name);
            xUtil.AddEl(parEl, TAG_LINK, link);

            return parEl;

        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the properties of the RssCoreChannelTextInput Class from the children of the parent XElement
        /// </summary>
        /// <param name="parEl">XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {
            if (parEl.Name.Namespace == XNamespace.None)
            {
                IEnumerable<XElement> lst = parEl.Elements();
                foreach (XElement el in lst)
                {
                    switch (el.Name.LocalName)
                    {
                        case TAG_TITLE:
                            title = xUtil.GetStr(el);
                            break;

                    }
                }
            }
        }

    }
}




