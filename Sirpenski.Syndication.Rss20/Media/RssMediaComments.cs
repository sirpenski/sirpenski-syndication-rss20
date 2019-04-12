// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaComments.cs
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
    /// The RssMediaComments object contains the comments a media item has received.
    /// </summary>
    [Serializable]
    public class RssMediaComments
    {
        public const string TAG_PARENT = "comments";
        public const string TAG_COMMENT = "comment";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// A list of comments
        /// </summary>
        public List<string> comments { get; set; } = new List<string>();


        /// <summary>
        ///  Default Constructor
        /// </summary>
        public RssMediaComments() { }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the RssMediaComment object properties as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = xUtil.CreateNSEl(TAG_PARENT, RSS.MEDIA_NS);

            for (int i = 0; i < comments.Count; i++)
            {
                xUtil.AddNsEl(parEl, RSS.MEDIA_NS, TAG_COMMENT, comments[i]);
            }


            return parEl;
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssMediaComment object properties with the contents of the XElement
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
                        case TAG_COMMENT:
                            comments.Add(xUtil.GetStr(el));
                            break;

                    }
                }

            }

        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a comment to the RssMediaComments comments collection
        /// </summary>
        /// <param name="s">Comment</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Add(string s)
        {
            comments.Add(s);
        }



        [OnDeserializing]
        private void OnDeserialize(StreamingContext context)
        {
            xUtil = new RssXmlUtilities();
        }


    }
}




