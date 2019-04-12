// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssCoreChannelImage.cs
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
    /// The RssCoreChannelImage class supplies a graphical logo for the feed (optional).
    /// </summary>
    [Serializable]
    public class RssCoreChannelImage
    {

        public const string TAG_PARENT = "image";
        public const string TAG_URL = "url";
        public const string TAG_TITLE = "title";
        public const string TAG_LINK = "link";
        public const string TAG_DESCRIPTION = "description";
        public const string TAG_WIDTH = "width";
        public const string TAG_HEIGHT = "height";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// The image's url property identifies the URL of the image, which must be in the GIF, JPEG or PNG formats (required).
        /// </summary>
        public string url { get; set; } = "";

        /// <summary>
        /// The image's title property holds character data that provides a human-readable description of the image (required).
        /// </summary>
        public string title { get; set; } = "";

        /// <summary>
        /// The image's link property identifies the URL of the web site represented by the image (required).
        /// </summary>
        public string link { get; set; } = "";

        /// <summary>
        /// The image's description proprety holds character data that provides a human-readable
        /// characterization of the site linked to the image (optional).
        /// </summary>
        public string description { get; set; } = "";

        /// <summary>
        /// The image's width element contains the width, in pixels, of the image (optional). 
        /// The image must be no wider than 144 pixels. If this element is omitted, the image is assumed to be 88 pixels wide.
        /// </summary>
        public int width { get; set; } = 0;

        /// <summary>
        /// The image's height element contains the height, in pixels, of the image (optional). The image must be no taller than 400 pixels. If this element is omitted, the image is assumed to be 31 pixels tall.
        /// </summary>
        public int height { get; set; } = 0;


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the RssCoreChannelImage properties as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = new XElement(TAG_PARENT);
            xUtil.AddEl(parEl, TAG_URL, url);
            xUtil.AddEl(parEl, TAG_TITLE, title);
            xUtil.AddEl(parEl, TAG_LINK, link);

            if (description.Length > 0)
            {
                xUtil.AddEl(parEl, TAG_DESCRIPTION, WebUtility.HtmlEncode(description));
            }

            if (width > 0)
            {
                xUtil.AddEl(parEl, TAG_WIDTH, width);
            }
            if (height > 0)
            {
                xUtil.AddEl(parEl, TAG_HEIGHT, height);
            }

            return parEl;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssCoreChannelImage properties from an XElement
        /// </summary>
        /// <param name="parEl">Parent XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {
            IEnumerable<XElement> lst = parEl.Elements();
            foreach (XElement el in lst)
            {
                if (el.Name.Namespace == XNamespace.None)
                {

                    switch (el.Name.LocalName)
                    {
                        case TAG_URL:
                            url = xUtil.GetStr(el);
                            break;
                        case TAG_TITLE:
                            title = xUtil.GetStr(el);
                            break;
                        case TAG_LINK:
                            link = xUtil.GetStr(el);
                            break;
                        case TAG_DESCRIPTION:
                            description = xUtil.GetStr(el);
                            break;
                        case TAG_WIDTH:
                            width = xUtil.GetInt(el);
                            break;
                        case TAG_HEIGHT:
                            height = xUtil.GetInt(el);
                            break;
                    }
                }
            }


        }


    }
}




