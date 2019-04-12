// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssCoreItem.cs
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
    /// An item element represents distinct content published in the feed such as a news article, 
    /// weblog entry or some other form of discrete update. A channel may contain any number of items (or no items at all).
    /// An item may contain the following child elements: author, category, comments, description, enclosure, guid, link, 
    /// pubDate, source and title.All of these elements are optional but an item must contain either a title or description.
    /// </summary>
    [Serializable]
    public partial class RssCoreItem
    {

        public const string TAG_PARENT = "item";
        public const string TAG_TITLE = "title";
        public const string TAG_LINK = "link";
        public const string TAG_DESCRIPTION = "description";
        public const string TAG_AUTHOR = "author";
        public const string TAG_CATEGORY = "category";
        public const string TAG_COMMENTS = "comments";
        public const string TAG_ENCLOSURE = "enclosure";
        public const string TAG_GUID = "guid";
        public const string TAG_PUBDATE = "pubDate";
        public const string TAG_SOURCE = "source";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// An item's title property holds character data that provides the item's headline. 
        /// This property is optional if the item contains a description element.
        /// </summary>
        public string title { get; set; } = "";

        /// <summary>
        /// An item's link property identifies the URL of a web page associated with the item (optional).
        /// </summary>
        public string link { get; set; } = "";

        /// <summary>
        /// An item's description element holds character data that contains the item's full 
        /// content or a summary of its contents, a decision entirely at the discretion of the publisher. 
        /// This propertyis optional if the item contains a title element.
        /// </summary>
        public string description { get; set; } = "";

        /// <summary>
        /// An item's author property provides the e-mail address of the person who wrote the item (optional).
        /// </summary>
        public string author { get; set; } = "";

        /// <summary>
        /// The category collection identifies a category/tag or categories/tags to which the item belongs (optional).
        /// </summary>
        public List<RssCoreItemCategory> categories { get; set; } = new List<RssCoreItemCategory>();

        /// <summary>
        /// An item's comments property identifies the URL of a web page that contains comments received in response to the item (optional).
        /// </summary>
        public string comments { get; set; } = "";


        /// <summary>
        /// An item's enclosure collection associates a media object(s) such as an audio, image, or video file with the item (optional).
        /// </summary>
        public List<RssCoreItemEnclosure> enclosures { get; set; } = new List<RssCoreItemEnclosure>();

        /// <summary>
        /// An item's guid property provides a string that uniquely identifies the item (optional).
        /// </summary>
        public RssCoreItemGuid guid { get; set; } = null;

        /// <summary>
        /// An item's pubDate property indicates the publication date and time of the item (optional).
        /// </summary>
        public DateTime pubDate { get; set; } = DateTime.MinValue;



        /// <summary>
        /// An item's source property indicates the fact that the item has been republished from another RSS feed (optional).
        /// </summary>
        public RssCoreItemSource source { get; set; } = null;



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the properties of the RssCoreItem class as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public virtual XElement GetEl()
        {
      
            XElement parEl = new XElement(TAG_PARENT);

            SetEl(parEl);

            return parEl;


        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Builds the xml fragment representing the properties of the RssCoreItem class
        /// </summary>
        /// <param name="parEl"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void SetEl(XElement parEl)
        {
            RssRfc822DateTimeConverter dtConvert = new RssRfc822DateTimeConverter();

            if (title.Length > 0)
            {
                xUtil.AddEl(parEl, TAG_TITLE, title);
            }
            if (link.Length > 0)
            {
                xUtil.AddEl(parEl, TAG_LINK, link);
            }
            if (description.Length > 0)
            {
                xUtil.AddEl(parEl, TAG_DESCRIPTION, WebUtility.HtmlEncode(description));
            }
            if (author.Length > 0)
            {
                xUtil.AddEl(parEl, TAG_AUTHOR, author);
            }


            if (categories.Count > 0)
            {
                for (int i = 0; i < categories.Count; i++)
                {
                    XElement ctgEl = categories[i].GetEl();
                    parEl.Add(ctgEl);
                }
            }


            if (comments.Length > 0)
            {
                xUtil.AddEl(parEl, TAG_COMMENTS, WebUtility.HtmlEncode(comments));
            }

            if (enclosures.Count > 0)
            {
                for (int i = 0; i < enclosures.Count; i++)
                {
                    XElement enclosureEl = enclosures[i].GetEl();
                    parEl.Add(enclosureEl);
                }
            }

            if (guid != null)
            {
                XElement guidEl = guid.GetEl();
                parEl.Add(guidEl);
            }


            if (pubDate != DateTime.MinValue)
            {
                xUtil.AddEl(parEl, TAG_PUBDATE, dtConvert.FormatDateTime(pubDate));
            }
       

            if (source != null)
            {
                XElement sourceEl = source.GetEl();
                parEl.Add(sourceEl);
            }

        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the properties of the RssCoreItem class with the contents of the parent element
        /// </summary>
        /// <param name="el">XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void LoadEl(XElement el)
        {

            RssRfc822DateTimeConverter dtConvert = new RssRfc822DateTimeConverter();

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

                case TAG_AUTHOR:
                    author = xUtil.GetStr(el);
                    break;

                case TAG_CATEGORY:
                    RssCoreItemCategory ctg = new RssCoreItemCategory();
                    ctg.Load(el);
                    categories.Add(ctg);
                    break;

                case TAG_COMMENTS:
                    comments = xUtil.GetStr(el);
                    break;

                case TAG_ENCLOSURE:
                    RssCoreItemEnclosure enc = new RssCoreItemEnclosure();
                    enc.Load(el);
                    enclosures.Add(enc);
                    break;

                case TAG_GUID:
                    guid = new RssCoreItemGuid();
                    guid.Load(el);
                    break;

                case TAG_PUBDATE:
                    pubDate = dtConvert.ParseRfc822(xUtil.GetStr(el));
                    break;

                case TAG_SOURCE:
                    source = new RssCoreItemSource();
                    source.Load(el);
                    break;


            }   // end switch

        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the properties of the RssCoreItem class with the contents of the parent element
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public virtual void Load(XElement parEl)
        {
            IEnumerable<XElement> lstEls = parEl.Elements();
            foreach (XElement el in lstEls)
            {
                if (el.Name.Namespace == XNamespace.None)
                {
                    LoadEl(el);

                }   // end if namespace none

            }   // end foreach

        }   // end load


  



    }
}




