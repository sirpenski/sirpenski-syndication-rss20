// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssCoreChannelSkipHours.cs
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
    /// The channel's skipHours collection identifies the hours of the day during which the feed is not updated (optional). 
    /// This collection contains individual hour elements identifying the hours to skip.
    /// </summary>
    [Serializable]
    public class RssCoreChannelSkipHours
    {

        public const string TAG_PARENT = "skipHours";
        public const string TAG_HOUR = "hour";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// The hour element identifies an hour of the day in Greenwich Mean Time (GMT) (required). 
        /// The hour must be expressed as an integer representing the number of hours since 00:00:00 GMT. 
        /// Values from 0 to 23 are permitted, with 0 representing midnight. An hour must not be duplicated.
        /// </summary>
        public List<int> Hours { get; set; } = new List<int>();

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the properties of the RssCoreChannelSkipHours collection as an element
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = xUtil.CreateEl(TAG_PARENT);

            for (int i = 0; i < Hours.Count; i++)
            {
                // verify the hour value
                if (Hours[i] >= 0 && Hours[i] <= 23)
                {
                    xUtil.AddEl(parEl, TAG_HOUR, Hours[i]);
                }
            }
            return parEl;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssCoreChannelSkipHours collection from a parent XElement
        /// </summary>
        /// <param name="parEl">XElement</param>
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
                        case TAG_HOUR:
                            Hours.Add(xUtil.GetInt(el));
                            break;
                    }
                }

            }
        }


  

    }
}




