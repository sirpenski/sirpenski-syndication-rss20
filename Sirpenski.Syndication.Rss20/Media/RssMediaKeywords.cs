// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaKeywords.cs
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
    /// The RssMediaKeywords object is a collection of keywords relevant to the media item
    /// </summary>
    [Serializable]
    public partial class RssMediaKeywords
    {

        public const string TAG_PARENT = "keywords";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// Keywords
        /// </summary>
        private string _keywords = "";

        /// <summary>
        /// A collection of keywords relevant to the media item
        /// </summary>
        public List<string> keywordList { get; set; } = new List<string>();


        /// <summary>
        /// Keywords
        /// </summary>
        public string keywords
        {
            get {
                return Concatenate();
            }
            set {
                Parse(value);
                _keywords = value;
            }
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Default Constructor
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaKeywords() { }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the properties of the RssMediaKeywords object as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = xUtil.CreateNSEl(TAG_PARENT, Concatenate(), RSS.MEDIA_NS);
            return parEl;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the properties of the RssMediaKeywords object with the contents of an XElement
        /// </summary>
        /// <param name="parEl">XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {

            if (parEl.Name.Namespace == RSS.MEDIA_NS)
            {
                _keywords = xUtil.GetStr(parEl);
                Parse(_keywords);
            }
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Concatenates the keywords
        /// </summary>
        /// <returns>Comma delimited string of concatenated keywords</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        private string Concatenate()
        {
            string tmp = "";
            string sep = "";
            for (int i = 0; i < keywordList.Count; i++)
            {
                tmp += sep + keywordList[i];
                sep = ", ";
            }
            return tmp;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Parses up the string
        /// </summary>
        /// <param name="keywords">Ke</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        private void Parse(string keywords)
        {
            keywordList = new List<string>();

            string[] a = keywords.Split(',');
            for (int i = 0; i < a.Length; i++)
            {
                keywordList.Add(a[i].Trim());
            }

        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a keyword to the Keywords collection
        /// </summary>
        /// <param name="keyword">string</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Add(string keyword)
        {
            keywordList.Add(keyword);
        }



        [OnDeserializing]
        private void OnDeserialize(StreamingContext context)
        {
            xUtil = new RssXmlUtilities();
        }

    }
}



