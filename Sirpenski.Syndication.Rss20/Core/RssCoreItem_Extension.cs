// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssCoreItem_Extension.cs
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
    public partial class RssCoreItem
    {
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a title to the item
        /// </summary>
        /// <param name="item_title">Item title</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddTitle(string item_title)
        {
            title = item_title;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a link
        /// </summary>
        /// <param name="item_link">Url</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddLink(string item_link)
        {
            link = item_link;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a description
        /// </summary>
        /// <param name="item_description">Html Encoded description</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddDescription(string item_description)
        {
            description = item_description;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds an author to the item
        /// </summary>
        /// <param name="item_author"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddAuthor(string item_author)
        {
            author = item_author;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a category to the item
        /// </summary>
        /// <param name="obj"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddCategory(RssCoreItemCategory obj)
        {
            categories.Add(obj);
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a category to the item
        /// </summary>
        /// <param name="item_category">category</param>
        /// <param name="item_category domain">category domain</param>
        /// <returns>bool (True if added, False if not added)</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddCategory(string item_category, string item_category_domain = "")
        {
            RssCoreItemCategory obj = new RssCoreItemCategory();
            obj.category = item_category;
            obj.domain = item_category_domain;
            categories.Add(obj);

        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a url containing comments for the item
        /// </summary>
        /// <param name="item_comment_url">url containing comments</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddComment(string item_comment_url)
        {
            comments = item_comment_url;
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds an enclosure to the item.  Multiple enclosures are allowed by this library
        /// </summary>
        /// <param name="url">url of media item</param>
        /// <param name="length">length in bytes of media item</param>
        /// <param name="mimetype">mime type of media item</param>
        /// <returns></returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public bool AddEnclosure(string url, string mimetype, long length = 0)
        {
            bool rt = false;

            if (url.Length > 0)
            {
                RssCoreItemEnclosure enc = new RssCoreItemEnclosure();
                enc.url = url;
                enc.length = length;
                enc.type = mimetype;
                AddEnclosure(enc);

                rt = true;

            }
            return rt;

        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds an enclosure to the enclosures collection
        /// </summary>
        /// <param name="obj"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddEnclosure(RssCoreItemEnclosure obj)
        {
            enclosures.Add(obj);
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a globally unique identifier to the item
        /// </summary>
        /// <param name="guid_string">The Globally Unique ID (GUID)</param>
        /// <param name="isPermalink">Flag indicating whether GUID is a permalink</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddGuid(string guid_string, bool isPermalink = false)
        {
            RssCoreItemGuid guidObj = new RssCoreItemGuid();
            guidObj.guid = guid_string;
            guidObj.isPermalink = isPermalink;
            AddGuid(guidObj);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a guid object to the item
        /// </summary>
        /// <param name="item_guid"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddGuid(RssCoreItemGuid item_guid)
        {
            guid = item_guid;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a publication date to the item
        /// </summary>
        /// <param name="item_pubdate">Publication Date</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddPubDate(DateTime item_pubdate)
        {
            pubDate = item_pubdate;
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds an item source to the item.  The item source is 
        /// where the item initially originated from.
        /// </summary>
        /// <param name="item_source_url">location </param>
        /// <param name="item_source_src"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddSource(string item_source_url, string item_source_src = "")
        {
            RssCoreItemSource objSource = new RssCoreItemSource();
            objSource.source = item_source_src;
            objSource.url = item_source_url;
            AddSource(objSource);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a source object
        /// </summary>
        /// <param name="obj"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddSource(RssCoreItemSource obj)
        {
            source = obj;
        }

    }
}




