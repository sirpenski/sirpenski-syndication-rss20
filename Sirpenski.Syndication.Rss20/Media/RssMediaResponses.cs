// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaResponses.cs
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
    /// The RssMediaResponses object contains a collection of responses to the media item
    /// </summary>
    [Serializable]
    public class RssMediaResponses
    {

        public const string TAG_PARENT = "responses";
        public const string TAG_RESPONSE = "response";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// A Collection of media item responses
        /// </summary>
        public List<string> responses { get; set; } = new List<string>();


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Default Constructor
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaResponses() { }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the RssMediaResponse object properties as an XElement 
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = xUtil.CreateNSEl(TAG_PARENT, RSS.MEDIA_NS);

            for (int i = 0; i < responses.Count; i++)
            {
                xUtil.AddNsEl(parEl, RSS.MEDIA_NS, TAG_RESPONSE, responses[i]);
            }


            return parEl;
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssMediaResponse object properties with the contents of the parent XElement
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
                        case TAG_RESPONSE:
                            responses.Add(xUtil.GetStr(el));
                            break;

                    }
                }

            }

        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a response to the RssMediaResponses object
        /// </summary>
        /// <param name="response">Response</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Add(string response)
        {
            responses.Add(response);
        }


        [OnDeserializing]
        private void OnDeserialize(StreamingContext context)
        {
            xUtil = new RssXmlUtilities();
        }

    }
}




