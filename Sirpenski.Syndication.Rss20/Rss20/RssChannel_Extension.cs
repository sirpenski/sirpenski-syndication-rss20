// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssCHannel_Extension.cs
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

namespace Sirpenski.Syndication.Rss20
{
    public partial class RssChannel
    {


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Sorts the items by publication date
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public override void SortItems()
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
        private class SortByPubDateDescending : IComparer<RssItem>
        {
            public int Compare(RssItem itm1, RssItem itm2)
            {
                return DateTime.Compare(itm1.pubDate, itm2.pubDate) * -1;
            }
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds an item to the channel
        /// </summary>
        /// <param name="rssItem">RssItem object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddItem(RssItem rssItem)
        {
            rssItem.Parent = this;
            items.Add(rssItem);
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
        /// Adds an atom link object to the feed
        /// </summary>
        /// <param name="atomLink"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddAtomLink(RssAtomLink atomLink)
        {
            AtomLinks.Add(atomLink);
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





    }
}



