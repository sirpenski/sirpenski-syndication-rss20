// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaEmbed.cs
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
    /// The RssMediaEmbed object contains player-specific embed code needed for a player to play any video.
    /// </summary>
    [Serializable]
    public class RssMediaEmbed
    {
        public const string TAG_PARENT = "embed";
        public const string TAG_PARAM = "param";
        public const string ATTR_NAME = "name";
        public const string ATTR_URL = "url";
        public const string ATTR_WIDTH = "width";
        public const string ATTR_HEIGHT = "height";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// url of the media item
        /// </summary>
        public string url { get; set; } = "";

        /// <summary>
        /// default width of the media player
        /// </summary>
        public int width { get; set; } = -1;

        /// <summary>
        /// default height of the media player
        /// </summary>
        public int height { get; set; } = -1;

        /// <summary>
        /// A list of key-value pairs defining parameters of the media player
        /// </summary>
        public List<KeyValuePair<string, string>> lstParams = new List<KeyValuePair<string, string>>();


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Default Constructor
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaEmbed() { }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the content of the RssMediaEmbed object properties as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = xUtil.CreateNSEl(TAG_PARENT, RSS.MEDIA_NS);

            if (url.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_URL, url);
            }
            if (width >= 0)
            {
                xUtil.AddAttr(parEl, ATTR_WIDTH, width.ToString());
            }
            if (height > 0)
            {
                xUtil.AddAttr(parEl, ATTR_HEIGHT, height.ToString());
            }



            for (int i = 0; i < lstParams.Count; i++)
            {
                XElement el = xUtil.AddNsEl(parEl, RSS.MEDIA_NS, TAG_PARAM, lstParams[i].Value);
                xUtil.AddAttr(el, ATTR_NAME, lstParams[i].Key);
            }


            return parEl;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssMediaEmbed object properties with the contents of the parent XElement
        /// </summary>
        /// <param name="parEl">Parent XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {
            if (parEl.Name.Namespace == RSS.MEDIA_NS)
            {

                IEnumerable<XElement> lst = parEl.Elements();
                foreach (XElement el in lst)
                {
                    switch (el.Name.LocalName)
                    {
                        case TAG_PARAM:

                            XAttribute attr = el.Attribute(ATTR_NAME);
                            if (attr != null)
                            {
                                string nm = xUtil.GetAttrStr(attr);
                                string val = xUtil.GetStr(el);
                                KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(nm, val);
                                lstParams.Add(kvp);
                            }
                            break;

                    }
                }

            }

        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a key-value pair to the RssMediaEmbed parameters collection
        /// </summary>
        /// <param name="key">Media Embed Parameter Name</param>
        /// <param name="val">Media Embed Parameter Value</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Add(string key, string val)
        {
            KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(key, val);
            lstParams.Add(kvp);
        }


        [OnDeserializing]
        private void OnDeserialize(StreamingContext context)
        {
            xUtil = new RssXmlUtilities();
        }


    }
}




