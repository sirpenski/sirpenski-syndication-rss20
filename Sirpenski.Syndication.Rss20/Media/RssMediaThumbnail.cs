// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaThumbnail.cs
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
    /// Defines a particular image as the representative images for the media object
    /// </summary>
    [Serializable]
    public class RssMediaThumbnail: IRssParentReference
    {
        public const string TAG_PARENT = "thumbnail";
        public const string ATTR_URL = "url";
        public const string ATTR_WIDTH = "width";
        public const string ATTR_HEIGHT = "height";
        public const string ATTR_TIME = "time";


        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        [NonSerialized]
        private RssUtilities rssUtil = new RssUtilities();

        /// <summary>
        /// url specifies the url of the thumbnail. It is a required attribute.
        /// </summary>
        public string url { get; set; } = "";

        /// <summary>
        /// width specifies the width of the thumbnail. It is an optional attribute.
        /// </summary>
        public int width { get; set; } = 0;

        /// <summary>
        /// height specifies the height of the thumbnail. It is an optional attribute.
        /// </summary>
        public int height { get; set; } = 0;

        /// <summary>
        /// time specifies the time offset in relation to the media object.
        /// </summary>
        public TimeSpan time { get; set; } = TimeSpan.MinValue;


        /// <summary>
        /// Parent Reference
        /// </summary>
        public object Parent { get; set; } = null;


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Default Constructor
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaThumbnail() { }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Constructor with Parent Reference
        /// </summary>
        /// <param name="parentRef">Parent object</param>
        /// // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaThumbnail(object parentRef)
        {
            Parent = parentRef;
            Debug.WriteLine("SETTING PARENT REF");
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the properties of the RssMediaThumbnail object as an XElement
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

            if (time > TimeSpan.MinValue)
            {
                string time_string = time.ToString("hh\\:mm\\:ss\\.fff");
                xUtil.AddAttr(parEl, ATTR_TIME, time_string);
            }

            return parEl;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the properties of the RssMediaThumbnail object from an XElement
        /// </summary>
        /// <param name="parEl"></param>
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
                        case ATTR_TIME:
                            time = xUtil.GetAttrTimeSpan(attr);
                            break;

                    }
                }
            }
        }


        /// <summary>
        /// Gets the parent item
        /// </summary>
        /// <returns></returns>
        public RssItem GetParentItem()
        {
            return rssUtil.GetParentItem(this);
        }



        [OnDeserializing]
        private void OnDeserialize(StreamingContext context)
        {
            xUtil = new RssXmlUtilities();
            rssUtil = new RssUtilities();
        }

    }
}




