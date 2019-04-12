// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssItem_Extension.cs
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



namespace Sirpenski.Syndication.Rss20
{

    /// <summary>
    /// This the parent exension object that subclasses on all the versions.  NOTE TE NAMESPACE!!!!! It does not 
    /// correspond to the folder that it is in (ie the versions folder).  THis is so it makes the object easier to use 
    /// by spcecifying a single using statement.  Better explanation needed.  I agree
    /// </summary>
    

    public partial class RssItem 
    {



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a category object to the item
        /// </summary>
        /// <param name="obj"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddCategory(RssItemCategory obj)
        {
            base.AddCategory(obj);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds an enclosure to the item
        /// </summary>
        /// <param name="obj"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddEnclosure(RssItemEnclosure obj)
        {
            base.AddEnclosure(obj);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a guid object to the item
        /// </summary>
        /// <param name="obj">RssItemGuid </param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddGuid(RssItemGuid obj)
        {
            base.AddGuid(obj);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a source object
        /// </summary>
        /// <param name="obj">Source Object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddSource(RssItemSource obj)
        {
            base.AddSource(obj);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a creator to the item
        /// </summary>
        /// <param name="item_creator"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddCreator(string item_creator)
        {
            RssDublinCoreCreator obj = new RssDublinCoreCreator();
            obj.creator = item_creator;
            AddCreator(obj);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a creator to the item 
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddCreator(RssDublinCoreCreator dcCreator)
        {
            creators.Add(dcCreator);

        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a content encoded object
        /// </summary>
        /// <param name="enc"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddContentEncoded(RssContentEncoded enc)
        {
            ContentEncoded = enc;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a content encoded string to item
        /// </summary>
        /// <param name="encodedcontent"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddContentEncoded(string encodedcontent)
        {
            if (ContentEncoded == null)
            {
                ContentEncoded = new RssContentEncoded();
            }
            ContentEncoded.encoded = encodedcontent;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds an atom link object to the feed
        /// </summary>
        /// <param name="lnk">Atom Link object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddAtomLink(RssAtomLink lnk)
        {
            AtomLink = lnk;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// adds an atom link
        /// </summary>
        /// <param name="href">link of this feed</param>
        /// <param name="rel">relationship (ie self)</param>
        /// <param name="mimetype">mime type (application/rss+xml)</param>
        /// <param name="title">title (channel title)</param>
        /// <param name="hreflang">language</param>
        /// <param name="length">length</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddAtomLink(string href, string rel, string mimetype, string title, string hreflang = "", int length = 0)
        {

            RssAtomLink lnk = new RssAtomLink();
            lnk.href = href;
            lnk.rel = rel;
            lnk.type = mimetype;
            lnk.title = title;
            lnk.hreflang = hreflang;
            lnk.length = length;
            AddAtomLink(lnk);


        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a slash comments object
        /// </summary>
        /// <param name="obj">Comment Count</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddSlashComments(RssSlashComments obj)
        {
            SlashComments = obj;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a slash comments count
        /// </summary>
        /// <param name="n"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddSlashComments(int n)
        {
            if (SlashComments == null)
            {
                SlashComments = new RssSlashComments();

            }
            SlashComments.comments = n;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a Creative Commons License to the item
        /// </summary>
        /// <param name="obj"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddCreativeCommonsLicense(RssCreativeCommonsLicense obj)
        {
            CreativeCommonsLicense = obj;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a creative commons license
        /// </summary>
        /// <param name="url"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddCreativeCommonsLicense(string url)
        {
            CreativeCommonsLicense = new RssCreativeCommonsLicense();
            CreativeCommonsLicense.license = url;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a Media Content Item to the Items collection
        /// </summary>
        /// <param name="contentItem"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaContentItem(RssMediaContent contentItem)
        {
            contentItem.Parent = this;
            mediaContentItems.Add(contentItem);

        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media group to the item.
        /// </summary>
        /// <param name="mg">RssMediaGroup</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaGroup(RssMediaGroup mg)
        {
            mg.Parent = this;


            for (int i = 0; i < mg.mediaContentItems.Count; i++)
            {
                // set parent
                mg.mediaContentItems[i].Parent = mg;
            }

            // now add to the media groups
            mediaGroups.Add(mg);


        }




    }
}



