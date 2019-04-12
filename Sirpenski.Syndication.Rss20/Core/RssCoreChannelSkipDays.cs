// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssCoreChannelSkipDays.cs
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
    /// The channel's skipDays element identifies days of the week during which the feed is not updated (optional). 
    /// This collection contains up to seven day elements identifying the days to skip.
    /// </summary>
    [Serializable]
    public class RssCoreChannelSkipDays
    {

        public const string TAG_PARENT = "skipDays";
        public const string TAG_DAY = "day";

        public const string DAY_MONDAY = "Monday";
        public const string DAY_TUESDAY = "Tuesday";
        public const string DAY_WEDNESDAY = "Wednesday";
        public const string DAY_THURSDAY = "Thursday";
        public const string DAY_FRIDAY = "Friday";
        public const string DAY_SATURDAY = "Saturday";
        public const string DAY_SUNDAY = "Sunday";



        private List<string> SKIPDAYS_ARRAY = new List<string>() { DAY_MONDAY, DAY_TUESDAY, DAY_WEDNESDAY, DAY_THURSDAY, DAY_FRIDAY, DAY_SATURDAY, DAY_SUNDAY };



        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();


        /// <summary>
        /// The day element identifies a weekday in Greenwich Mean Time (GMT) (required). 
        /// Seven values are permitted -- "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" or 
        /// "Sunday" -- and must not be duplicated.
        /// </summary>
        public List<string> Days { get; set; } = new List<string>();


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the RssCoreChannelSkipDays collection as an element
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {

            XElement parEl = xUtil.CreateEl(TAG_PARENT);

            for (int i = 0; i < Days.Count; i++)
            {
                // check if day in there.  NOTE SKIPDAYS_ARRAY is an array held in the 
                // static CON class _constants file.  We preserve it due to speed requirements
                int ndx = SKIPDAYS_ARRAY.FindIndex(x => x == Days[i]);
                
                
                if (ndx >= 0)
                { 
                    xUtil.AddEl(parEl, TAG_DAY, Days[i]);
                }
            }
            return parEl;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssCoreChannelSkipDays collection with the contents of the XElement
        /// </summary>
        /// <param name="parEl">XElement, parent element</param>
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
                        case TAG_DAY:
                            Days.Add(xUtil.GetStr(el));
                            break;

                    }
                }

            }
        }
    }
}




