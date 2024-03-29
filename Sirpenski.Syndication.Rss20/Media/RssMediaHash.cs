﻿// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaHash.cs
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
    /// Defines the media hash of the binary media file
    /// </summary>
    [Serializable]
    public class RssMediaHash
    {
        public const string TAG_PARENT = "hash";
        public const string ATTR_ALGO = "algo";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// The hash of the file
        /// </summary>
        public string hash { get; set; } = "";

        ///<summary>
        ///The algorithm used to compute the hash
        ///</summary>
        public string algo { get; set; } = "";



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Default Constructor
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaHash() { }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the properties of the RssMediaHash object as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = xUtil.CreateNSEl(TAG_PARENT, hash, RSS.MEDIA_NS);
            if (algo.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_ALGO, algo);
            }
            return parEl;
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the properties of the RssMediaHash object as an XElement
        /// </summary>
        /// <param name="parEl">XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {
            if (parEl.Name.Namespace == RSS.MEDIA_NS)
            {
                hash = xUtil.GetStr(parEl);

                IEnumerable<XAttribute> lstAttr = parEl.Attributes();
                foreach (XAttribute attr in lstAttr)
                {
                    switch (attr.Name.LocalName)
                    {
                        case ATTR_ALGO:
                            algo = xUtil.GetAttrStr(attr);
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




