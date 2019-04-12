// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaCommunity.cs
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
    /// The RssMediaCommunity object allows inclusion of the user perception about a media object in the form of view count, ratings and tags
    /// </summary>
    [Serializable]
    public class RssMediaCommunity
    {
        public const string TAG_PARENT = "community";
        public const string TAG_STARRATING = "starRating";
        public const string TAG_STATISTICS = "statistics";
        public const string TAG_TAGS = "tags";

        public const string ATTR_STARRATING_AVERAGE = "average";
        public const string ATTR_STARRATING_COUNT = "count";
        public const string ATTR_STARRATING_MIN = "min";
        public const string ATTR_STARRATING_MAX = "max";

        public const string ATTR_STATISTICS_VIEWS = "views";
        public const string ATTR_STATISTICS_FAVORITES = "favorites";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        private string _tags = "";


        /// <summary>
        /// star rating average about the media content item
        /// </summary>
        public double starRatingAverage { get; set; } = -1.0;

        /// <summary>
        /// the number of ratings made on the media item
        /// </summary>
        public int starRatingCount { get; set; } = -1;

        /// <summary>
        /// the minimum star rating made on the media item
        /// </summary>
        /// 
        public int starRatingMin { get; set; } = -1;

        /// <summary>
        /// the max star rating made on the media item
        /// </summary>
        public int starRatingMax { get; set; } = -1;

        /// <summary>
        /// the total number of views on the media item
        /// </summary>
        public int statisticsViews { get; set; } = -1;

        /// <summary>
        /// the total number of times the item was favorited
        /// </summary>
        public int statisticsFavorites { get; set; } = -1;

        /// <summary>
        /// A collection of tags and weights for items ie tag:3.5, etc.
        /// </summary>
        public List<string> tagList { get; set; } = new List<string>();

        // the tags parameter
        public string tags
        {
            get { return Concatenate(); }
            set {
                _tags = value;
                Parse(_tags);
            }
        }


        /// <summary>
        /// Default Constructor
        /// </summary>
        public RssMediaCommunity() { }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the RssMediaCommunity object properties as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = xUtil.CreateNSEl(TAG_PARENT, RSS.MEDIA_NS);

            XElement el = xUtil.CreateNSEl(TAG_STARRATING, RSS.MEDIA_NS);
            if (starRatingAverage >= 0)
            {
                xUtil.AddAttr(el, ATTR_STARRATING_AVERAGE, starRatingAverage.ToString("0.00"));
            }
            if (starRatingCount >= 0)
            {
                xUtil.AddAttr(el, ATTR_STARRATING_COUNT, starRatingCount.ToString());
            }
            if (starRatingMin >= 0)
            {
                xUtil.AddAttr(el, ATTR_STARRATING_MIN, starRatingMin.ToString());
            }
            if (starRatingMax >= 0)
            {
                xUtil.AddAttr(el, ATTR_STARRATING_MAX, starRatingMax);
            }
            parEl.Add(el);


            el = xUtil.CreateNSEl(TAG_STATISTICS, RSS.MEDIA_NS);
            if (statisticsViews >= 0)
            {
                xUtil.AddAttr(el, ATTR_STATISTICS_VIEWS, statisticsViews);
            }
            if (statisticsFavorites >= 0)
            {
                xUtil.AddAttr(el, ATTR_STATISTICS_FAVORITES, statisticsFavorites);
            }
            parEl.Add(el);

            el = xUtil.CreateNSEl(TAG_TAGS, Concatenate(), RSS.MEDIA_NS);
            parEl.Add(el);




            return parEl;
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssMediaCommunity object properties with the contents of the parent XElement
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
                        case TAG_STARRATING:
                            IEnumerable<XAttribute> lstAttr1 = el.Attributes();
                            foreach (XAttribute attr in lstAttr1)
                            {
                                switch (attr.Name.LocalName)
                                {
                                    case ATTR_STARRATING_AVERAGE:
                                        starRatingAverage = xUtil.GetAttrDbl(attr);
                                        break;
                                    case ATTR_STARRATING_COUNT:
                                        starRatingCount = xUtil.GetAttrInt(attr);
                                        break;
                                    case ATTR_STARRATING_MIN:
                                        starRatingMin = xUtil.GetAttrInt(attr);
                                        break;
                                    case ATTR_STARRATING_MAX:
                                        starRatingMax = xUtil.GetAttrInt(attr);
                                        break;
                                }
                            }
                            break;


                        case TAG_STATISTICS:
                            IEnumerable<XAttribute> lstAttr2 = el.Attributes();
                            foreach (XAttribute attr in lstAttr2)
                            {
                                switch (attr.Name.LocalName)
                                {
                                    case ATTR_STATISTICS_VIEWS:
                                        statisticsViews = xUtil.GetAttrInt(attr);
                                        break;
                                    case ATTR_STATISTICS_FAVORITES:
                                        statisticsFavorites = xUtil.GetAttrInt(attr);
                                        break;
                                }
                            }
                            break;

                        case TAG_TAGS:
                            _tags = xUtil.GetStr(el);
                            Parse(_tags);

                         
                            break;

                    }


                }


            }

        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// parses the tags in the list
        /// </summary>
        /// <param name="s"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        private void Parse(string s)
        {
            tagList = new List<string>();
            if (s.Length > 0)
            {
                string[] a = s.Split(',');
                for (int i = 0; i < a.Length; i++)
                {
                    tagList.Add(a[i].Trim());
                }
            }
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Concatenates the tags in the list
        /// </summary>
        /// <returns></returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        private string Concatenate()
        {

            string tmp = "";
            string sep = "";
            for (int i = 0; i < tagList.Count; i++)
            {
                tmp += sep + tagList[i];
                sep = ", ";
            }
            return tmp;
        }

        /// <summary>
        /// Adds a tag.  A tag is in the format tag:weight i.e. thriller:3.5
        /// </summary>
        /// <param name="t"></param>
        public void AddTag(string t)
        {
            tagList.Add(t);
        }



        [OnDeserializing]
        private void OnDeserialize(StreamingContext context)
        {
            xUtil = new RssXmlUtilities();
        }
    }
}




