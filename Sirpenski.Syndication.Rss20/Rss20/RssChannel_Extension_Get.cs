// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssChannel_Extension_Get.cs
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
using System.Linq;
using System.Diagnostics;

namespace Sirpenski.Syndication.Rss20
{
    public partial class RssChannel
    {

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the effective thumbnail for this channel.  This does not search children
        /// </summary>
        /// <returns>RssMediaThumbnail</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaThumbnail GetThumbnail()
        {
            return rssUtil.GetThumbnail(this);
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the effective thumbnail for this item.  This searches children
        /// </summary>
        /// <returns>RssMediaThumbnail</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaThumbnail GetThumbnailAll()
        {
            RssMediaThumbnail rt = rssUtil.GetThumbnail(this);
            if (rt == null)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    rt = items[i].GetThumbnailAll();
                    if (rt != null)
                    {
                        break;
                    }
                }
            }

            return rt;
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets all the media thumbnails for this object.  This does not search children
        /// </summary>
        /// <returns>IEnumerable&lt;RssMediaThumbnail&gt;</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public IEnumerable<RssMediaThumbnail> GetThumbnails()
        {
            return rssUtil.GetThumbnails(this);
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets all the media thumbnails for this object.  This searches children
        /// </summary>
        /// <returns>IEnumerable&lt;RssMediaThumbnail&gt;</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public IEnumerable<RssMediaThumbnail> GetThumbnailsAll()
        {

            IEnumerable<RssMediaThumbnail> lst;


            // Get all the thumbnails for the channel
            IEnumerable<RssMediaThumbnail> rt = rssUtil.GetThumbnails(this);

            // now get all the thumbnails for the items
            for (int i = 0; i < items.Count; i++)
            {
                lst = items[i].GetThumbnailsAll();
                rt = rt.Concat(lst);
            }

            return rt;

        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        ///  Gets all content of type image, video, audio etc.
        /// </summary>
        /// <returns>IEnumerable&gt;RssMediaContentItem&gt;</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public IEnumerable<RssMediaContent> GetMediaContentItems(string medium = "", string mimeType = "")
        {

            IEnumerable<RssMediaContent> rt = new List<RssMediaContent>();

            for (int i = 0; i < items.Count; i++)
            {
                IEnumerable<RssMediaContent> lst =  items[i].GetMediaContentItems(medium, mimeType);

                rt = rt.Concat(lst);
            }
            return rt;

        }


    }
}



