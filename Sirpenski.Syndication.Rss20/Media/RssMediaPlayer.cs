// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaPlayer.cs
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
    /// The RssMediaPlayer object allows the media item to be accessed through a web browser media player console
    /// </summary>
    [Serializable]
    public class RssMediaPlayer
    {

        public const string TAG_PARENT = "player";
        public const string ATTR_URL = "url";
        public const string ATTR_WIDTH = "width";
        public const string ATTR_HEIGHT = "height";


        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// url is the URL of the player console that plays the media. It is a required attribute.
        /// </summary>
        public string url { get; set; } = "";

        /// <summary>
        /// width is the width of the browser window that the URL should be opened in. It is an optional attribute.
        /// </summary>
        public int width { get; set; } = 0;

        /// <summary>
        /// height is the height of the browser window that the URL should be opened in. It is an optional attribute.
        /// </summary>
        public int height { get; set; } = 0;


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Default Constructor
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaPlayer() { }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the RssMediaPlayer object properties as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = xUtil.CreateNSEl(TAG_PARENT, RSS.MEDIA_NS);
            xUtil.AddAttr(parEl, ATTR_URL, url);

            if (width > 0)
            {
                xUtil.AddAttr(parEl, ATTR_WIDTH, width.ToString());
            }

            if (height > 0)
            {
                xUtil.AddAttr(parEl, ATTR_HEIGHT, height.ToString());
            }


            return parEl;
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssMediaPlayer object properties from an XElement
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
                        case ATTR_URL:
                            url = xUtil.GetAttrStr(attr);
                            break;
                        case ATTR_WIDTH:
                            width = xUtil.GetAttrInt(attr);
                            break;
                        case ATTR_HEIGHT:
                            height = xUtil.GetAttrInt(attr);
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




