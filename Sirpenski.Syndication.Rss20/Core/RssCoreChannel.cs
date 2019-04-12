// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssCoreChannel.cs
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
    /// RssCoreChannel class represents the channel object of the Rss 2.0 specification
    /// </summary>
    
    [Serializable]
    public partial class RssCoreChannel
    {


        public const string TAG_PARENT = "channel";


        public const string TAG_TITLE = "title";
        public const string TAG_LINK = "link";
        public const string TAG_DESCRIPTION = "description";

        public const string TAG_LANGUAGE = "language";
        public const string TAG_COPYRIGHT = "copyright";
        public const string TAG_MANAGINGEDITOR = "managingEditor";
        public const string TAG_WEBMASTER = "webMaster";
        public const string TAG_PUBDATE = "pubDate";
        public const string TAG_LASTBUILDDATE = "lastBuildDate";
        public const string TAG_CATEGORY = "category";
        public const string TAG_GENERATOR = "generator";
        public const string TAG_DOCS = "docs";
        public const string TAG_CLOUD = "cloud";
        public const string TAG_TTL = "ttl";
        public const string TAG_IMAGE = "image";
        public const string TAG_TEXTINPUT = "textInput";
        public const string TAG_SKIPHOURS = "skipHours";
        public const string TAG_SKIPDAYS = "skipDays";
        public const string TAG_ITEM = "item";

        [NonSerialized]
        RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// The title property holds character data that provides the name of the feed (required).
        /// </summary>
        public string title { get; set; } = "";

        /// <summary>
        /// The link property identifies the URL of the web site associated with the feed (required).
        /// </summary>
        public string link { get; set; } = "";

        /// <summary>
        /// The description property holds character data that provides a human-readable characterization or summary of the feed (required).
        /// </summary>
        public string description { get; set; } = "";

        // optional propertys

        /// <summary>
        /// The channel's language property identifies the natural language employed in the feed (optional).
        /// </summary>
        public string language { get; set; } = "";

        /// <summary>
        /// The copyright property declares the human-readable copyright statement that applies to the feed(optional).
        /// </summary>
        public string copyright { get; set; } = "";

        /// <summary>
        /// The channel's managingEditor property provides the e-mail address of the person to contact regarding the editorial content of the feed (optional).
        /// </summary>
        public string managingEditor { get; set; } = "";

        /// <summary>
        /// The channel's webMaster property provides the e-mail address of the person to contact about technical issues regarding the feed (optional).
        /// </summary>
        public string webMaster { get; set; } = "";

        /// <summary>
        /// The channel's pubDate property indicates the publication date and time of the feed's content (optional).
        /// </summary>
        public DateTime pubDate { get; set; } = DateTime.MinValue;



        /// <summary>
        /// The channel's lastBuildDate property indicates the last date and time the content of the feed was updated (optional).
        /// </summary>
        public DateTime lastBuildDate { get; set; } = DateTime.MinValue;



        /// <summary>
        /// The categories collection identifies a list of categories or tags to which the feed belongs (optional).
        /// </summary>
        public List<RssCoreChannelCategory> categories { get; set; } = new List<RssCoreChannelCategory>();

        /// <summary>
        /// The generator property credits the software that created the feed (optional).
        /// </summary>
        public string generator { get; set; } = "";

        /// <summary>
        /// The docs property identifies the URL of the RSS specification implemented by the software that created the feed (optional).
        /// </summary>
        public string docs { get; set; } = "";

        /// <summary>
        /// The cloud collection contains multiple cloud definitions that allows updates to the 
        /// feed to be monitored using a web service that implements the RssCloud application programming interface (optional).
        /// </summary>
        public List<RssCoreChannelCloud> cloud { get; set; } = new List<RssCoreChannelCloud>();

        /// <summary>
        /// The channel's ttl element represents the feed's time to live (TTL): the maximum number of minutes to cache the data before an aggregator requests it again (optional)
        /// </summary>
        public int ttl { get; set; } = 0;

        /// <summary>
        /// The image element supplies a graphical logo for the feed (optional).
        /// </summary>
        public RssCoreChannelImage image { get; set; } = null;

        /// <summary>
        /// Not Used.  Not Recommended 
        /// </summary>
        public RssCoreChannelTextInput textInput { get; set; } = null;

        /// <summary>
        /// The channel's skipHours collection contains a list of hours of the day during which the feed is not updated (optional).
        /// </summary>
        public RssCoreChannelSkipHours skipHours { get; set; } = new RssCoreChannelSkipHours();

        /// <summary>
        /// The channel's skipDays collection contains a list of days of the week during which the feed is not updated (optional)
        /// </summary>
        public RssCoreChannelSkipDays skipDays { get; set; } = new RssCoreChannelSkipDays();

        /// <summary>
        /// An item collectio represents a list of distinct content published in the feed such as a news article, weblog entry or 
        /// some other form of discrete update. A channel may contain any number of items (or no items at all).
        /// </summary>
        public List<RssCoreItem> items { get; set; } = new List<RssCoreItem>();

        /// <summary>
        /// Constructor
        /// </summary>
        public RssCoreChannel() { }




        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the RssCoreChannel object properties as an XElemement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public virtual XElement GetEl()
        {
            XElement parEl = xUtil.CreateEl(TAG_PARENT);

            // set the element
            SetEl(parEl);

            for (int i = 0; i < items.Count; i++)
            {
                XElement itemEl = items[i].GetEl();
                parEl.Add(itemEl);
            }


            return parEl;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Sets the element with the descendents of the parent XElement
        /// </summary>
        /// <param name="parEl"></param>
        /// <returns></returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void SetEl(XElement parEl)
        {

            RssRfc822DateTimeConverter dtConvert = new RssRfc822DateTimeConverter();

            // channel title
            xUtil.AddEl(parEl, TAG_TITLE, title);

            // link to channel
            xUtil.AddEl(parEl, TAG_LINK, link);

            // channel description
            xUtil.AddEl(parEl, TAG_DESCRIPTION, WebUtility.HtmlEncode(description));

            // optional elements are only included if they are not null or have string length 
            // greater than zero or in the case of dates, greater that datetime.minvalue

            // language
            if (language.Length > 0)
            {
                xUtil.AddEl(parEl, TAG_LANGUAGE, language);
            }

            // copyright
            if (copyright.Length > 0)
            {
                xUtil.AddEl(parEl, TAG_COPYRIGHT, copyright);
            }

            // managing editor
            if (managingEditor.Length > 0)
            {
                xUtil.AddEl(parEl, TAG_MANAGINGEDITOR, managingEditor);
            }

            // webmaster
            if (webMaster.Length > 0)
            {
                xUtil.AddEl(parEl, TAG_WEBMASTER, webMaster);
            }

            if (pubDate != DateTime.MinValue)
            { 
                xUtil.AddEl(parEl, TAG_PUBDATE, dtConvert.FormatDateTime(pubDate));
            }

            if (lastBuildDate != DateTime.MinValue)
            {
                xUtil.AddEl(parEl, TAG_LASTBUILDDATE, dtConvert.FormatDateTime(lastBuildDate));
            }


            // applicable categories
            if (categories.Count > 0)
            {
                for (int i = 0; i < categories.Count; i++)
                {
                    XElement el = categories[i].GetEl();
                    parEl.Add(el);
                }
            }


            // arrogance
            if (generator.Length == 0)
            {
                generator = RSS.GENERATOR;
            }
           
                 
            // generator
            if (generator.Length > 0)
            {
                xUtil.AddEl(parEl, TAG_GENERATOR, generator);
            }

            if (docs.Length > 0)
            {
                xUtil.AddEl(parEl, TAG_DOCS, docs);
            }

            // multiple cloud interfaces allowed here
            if (cloud.Count > 0)
            {
                for (int i = 0; i < cloud.Count; i++)
                {
                    XElement cloudEl = cloud[i].GetEl();
                    parEl.Add(cloudEl);
                }

            }

            if (ttl > 0)
            {
                xUtil.AddEl(parEl, TAG_TTL, ttl);
            }

            if (image != null)
            {
                XElement imageEl = image.GetEl();
                parEl.Add(imageEl);
            }

            if (textInput != null)
            {
                XElement textInputEl = textInput.GetEl();
                parEl.Add(textInputEl);
            }

            if (skipHours != null)
            {
                if (skipHours.Hours.Count > 0)
                {
                    XElement skipHoursEl = skipHours.GetEl();
                    parEl.Add(skipHoursEl);
                }
            }

            if (skipDays != null)
            {
                if (skipDays.Days.Count > 0)
                {
                    XElement skipDaysEl = skipDays.GetEl();
                    parEl.Add(skipDaysEl);
                }
            }


        }




        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Sets the property based upon the tag name of the provided XElement
        /// </summary>
        /// <param name="el">XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void LoadEl(XElement el)
        {

            RssRfc822DateTimeConverter dtConvert = new RssRfc822DateTimeConverter();

            if (el.Name.Namespace == XNamespace.None)
            {
                switch (el.Name.LocalName)
                {
                    case TAG_TITLE:
                        title = xUtil.GetStr(el);
                        break;

                    case TAG_LINK:
                        link = xUtil.GetStr(el);
                        break;

                    case TAG_DESCRIPTION:
                        description = xUtil.GetStr(el);
                        break;

                    case TAG_LANGUAGE:
                        language = xUtil.GetStr(el);
                        break;

                    case TAG_COPYRIGHT:
                        copyright = xUtil.GetStr(el);
                        break;

                    case TAG_MANAGINGEDITOR:
                        managingEditor = xUtil.GetStr(el);
                        break;

                    case TAG_WEBMASTER:
                        webMaster = xUtil.GetStr(el);
                        break;

                    case TAG_PUBDATE:
                        pubDate = dtConvert.ParseRfc822(xUtil.GetStr(el));
                        break;

                    case TAG_LASTBUILDDATE:
                        lastBuildDate = dtConvert.ParseRfc822(xUtil.GetStr(el));
                        break;

                    case TAG_CATEGORY:
                        RssCoreChannelCategory ctg = new RssCoreChannelCategory();
                        ctg.Load(el);
                        categories.Add(ctg);
                        break;

                    case TAG_GENERATOR:
                        generator = xUtil.GetStr(el);
                        break;

                    case TAG_DOCS:
                        docs = xUtil.GetStr(el);
                        break;

                    case TAG_CLOUD:
                        RssCoreChannelCloud c = new RssCoreChannelCloud();
                        c.Load(el);
                        cloud.Add(c);
                        break;

                    case TAG_TTL:
                        ttl = xUtil.GetInt(el);
                        break;

                    case TAG_IMAGE:
                        image = new RssCoreChannelImage();
                        image.Load(el);
                        break;

                    case TAG_TEXTINPUT:
                        textInput = new RssCoreChannelTextInput();
                        textInput.Load(el);
                        break;

                    case TAG_SKIPHOURS:
                        skipHours = new RssCoreChannelSkipHours();
                        skipHours.Load(el);
                        break;

                    case TAG_SKIPDAYS:
                        skipDays = new RssCoreChannelSkipDays();
                        skipDays.Load(el);
                        break;


                }   // end switch

            }   // end if namespace

        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssCoreChannel object properties with the descendents of the parent element
        /// </summary>
        /// <param name="parEl">Parent Element</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public virtual void Load(XElement parEl)
        {

            IEnumerable<XElement> lstEl = parEl.Elements();
            foreach (XElement el in lstEl)
            {
                switch (el.Name.LocalName)
                {
                    case TAG_ITEM:
                        RssCoreItem itm = new RssCoreItem();
                        itm.Load(el);
                        items.Add(itm);
                        break;

                    default:
                        LoadEl(el);
                        break;
                }

            }   // end for each element

        }   // end load



    }
}




