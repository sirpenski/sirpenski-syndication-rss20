// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssCoreChannel_Extension.cs
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

    public partial class RssCoreChannel
    {

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Sorts the items by publication date
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public virtual void SortItems()
        {
            items.Sort(new SortByPubDateDescending());
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Sorts the items by pubdate descending
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        private class SortByPubDateDescending : IComparer<RssCoreItem>
        {
            public int Compare(RssCoreItem itm1, RssCoreItem itm2)
            {
                return DateTime.Compare(itm1.pubDate, itm2.pubDate) * -1;
            }
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        ///<summary>
        /// AddTitle sets the title
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddTitle(string channel_title)
        {
            title = channel_title;
        }

        /// <summary>
        /// sets the link
        /// </summary>
        /// <param name="channel_link">The channel link</param>
        public void AddLink(string channel_link)
        {
            link = channel_link;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        ///<summary>
        /// sets the description
        /// </summary>
        /// <param name="channel_description">The channel description (Html Encoded)</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddDescription(string channel_description)
        {
            description = channel_description;
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds the language element
        /// </summary>
        /// <param name="channel_language">The channel language</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddLanguage(string channel_language)
        {
            language = channel_language;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds the coyright element
        /// </summary>
        /// <param name="channel_copyright"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddCopyright(string channel_copyright)
        {
            copyright = channel_copyright;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a managing editor
        /// </summary>
        /// <param name="managing_editor">The channel Managing Editor</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddManagingEditor(string managing_editor)
        {
            managingEditor = managing_editor;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds the channel webmaster
        /// </summary>
        /// <param name="channel_webmaster"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddWebMaster(string channel_webmaster)
        {
            webMaster = channel_webmaster;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds the channel publication date
        /// </summary>
        /// <param name="channel_pubdate">Channel Publication Date</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddPubDate(DateTime channel_pubdate)
        {
            pubDate = channel_pubdate;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds the channel last build date
        /// </summary>
        /// <param name="channel_lastbuilddate">Channel Last Build Date</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddLastBuildDate(DateTime channel_lastbuilddate)
        {
            lastBuildDate = channel_lastbuilddate;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a category to the channel
        /// </summary>
        /// <param name="channel_category">Category</param>
        /// <param name="channel_category_domain">Domain</param>
        /// <returns>bool (True if added, False if not added)</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddCategory(string channel_category, string channel_category_domain = "")
        {
            RssCoreChannelCategory obj = new RssCoreChannelCategory();
            obj.category = channel_category;
            obj.domain = channel_category_domain;
            AddCategory(obj);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds the channel generator (software that created channel)
        /// </summary>
        /// <param name="channel_generator">Software that built the channel</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddGenerator(string channel_generator)
        {
            generator = channel_generator;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds the specification document to the channel
        /// </summary>
        /// <param name="channel_docs"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddDocs(string channel_docs)
        {
            docs = channel_docs;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a RssCoreChannelCloud object to the channel
        /// </summary>
        /// <param name="cloud_domain">domain</param>
        /// <param name="cloud_port">port</param>
        /// <param name="cloud_path">path</param>
        /// <param name="cloud_registerprocedure">procedure name</param>
        /// <param name="cloud_protocol">protocol</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddCloud(string cloud_domain, int cloud_port, string cloud_path, string cloud_registerprocedure, string cloud_protocol)
        {
            RssCoreChannelCloud c = new RssCoreChannelCloud();
            c.domain = cloud_domain;
            c.port = cloud_port;
            c.path = cloud_path;
            c.registerProcedure = cloud_registerprocedure;
            c.protocol = cloud_protocol;
            AddCloud(c);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a channel time to live element
        /// </summary>
        /// <param name="channel_timetolive">Time To Live</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddTtl(int channel_timetolive)
        {
            ttl = channel_timetolive;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a skip hour to the channel
        /// </summary>
        /// <param name="hr">Hour</param>
        /// <returns>True if added, False if not added</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public bool AddSkipHour(int hr)
        {
            bool rt = false;
            if (hr >= 0 && hr <= 23)
            {
                int ndx = skipHours.Hours.FindIndex(x => x == hr);
                if (ndx < 0)
                {
                    skipHours.Hours.Add(hr);
                    rt = true;
                }
            }
            return rt;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a skip day to the channel
        /// </summary>
        /// <param name="day">Day</param>
        /// <returns>True if added, False if not added</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public bool AddSkipDay(string day)
        {
            bool rt = false;

            // add to the array
            skipDays.Days.Add(day);
            rt = true;


            return rt;

        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds the channel image
        /// </summary>
        /// <param name="url">url to image</param>
        /// <param name="title">title of the image (ie the alt text)</param>
        /// <param name="link">link to the site, url of the feed itself</param>
        /// <param name="width">image width must be greater than zero and less than 144</param>
        /// <param name="height">image height - Must be greater than 0 and less than 400</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddImage(string url, string title, string link, int width = 0, int height = 0)
        {

            RssCoreChannelImage imgObj = new RssCoreChannelImage();
            imgObj.url = url;
            imgObj.title = title;
            imgObj.link = link;
            imgObj.width = width;
            imgObj.height = height;
            AddImage(imgObj);
 
        }




        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a category object to the channel
        /// </summary>
        /// <param name="obj">RssCoreChannelCategory</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddCategory(RssCoreChannelCategory obj)
        {
            categories.Add(obj);
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a cloud object to the channel
        /// </summary>
        /// <param name="obj">RssCoreChannelCloud</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddCloud(RssCoreChannelCloud obj)
        {
            cloud.Add(obj);
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a channel image to the channel
        /// </summary>
        /// <param name="obj">RssCoreChannelImage</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddImage(RssCoreChannelImage obj)
        {
            image = obj;
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a skip days objec to the channel
        /// </summary>
        /// <param name="obj">RssCoreChannelSkipDays</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddSkipDays(RssCoreChannelSkipDays obj)
        {
            skipDays = obj;
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a skip hours object to the channel
        /// </summary>
        /// <param name="obj">RssCoreChannelSkipHours</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddSkipHours(RssCoreChannelSkipHours obj)
        {
            skipHours = obj;
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a TextInput object to the channel (Don't Use)
        /// </summary>
        /// <param name="obj"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddTextInput(RssCoreChannelTextInput obj)
        {
            textInput = obj;
        }



    }
}




