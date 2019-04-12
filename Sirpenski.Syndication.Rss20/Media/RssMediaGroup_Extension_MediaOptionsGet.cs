// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaGroup_Extension_MediaOptionsAdd.cs
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
using System.Linq;
using System.Runtime.Serialization;
using System.Diagnostics;



namespace Sirpenski.Syndication.Rss20
{

    /// <summary>
    /// Adds media options
    /// </summary>
    public partial class RssMediaGroup
    {

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the effective thumbnail for this item.  Note, it does not go down tree.
        /// </summary>
        /// <returns>RssMediaThumbnail</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaThumbnail GetThumbnail()
        {
            return rssUtil.GetThumbnail(this);         
        }


        /// <summary>
        /// Gets all thumbnail including child Media Content Items
        /// </summary>
        /// <returns></returns>
        public RssMediaThumbnail GetThumbnailAll()
        {
            RssMediaThumbnail rt = rssUtil.GetThumbnail(this);
            if (rt == null)
            {
                for (int i = 0; i < mediaContentItems.Count; i++)
                {
                    rt = rssUtil.GetThumbnail(mediaContentItems[i]);
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
        /// Gets all the media thumbnails for this object.  Note, it does not go down tree.
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
        /// Gets all the media thumbnails for this object.  This looks at content items too
        /// </summary>
        /// <returns>IEnumerable&lt;RssMediaThumbnail&gt;</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public IEnumerable<RssMediaThumbnail> GetThumbnailsAll()
        {
            IEnumerable<RssMediaThumbnail> rt = rssUtil.GetThumbnails(this);
            for (int i = 0; i < mediaContentItems.Count; i++)
            {
                IEnumerable<RssMediaThumbnail> lst = rssUtil.GetThumbnails(mediaContentItems[i]);
                rt = rt.Concat(lst);
            }
            return rt;

        }





        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the Effective image in the group.  ie the first one that fits the criteria
        /// </summary>
        /// <param name="mimeType">mime type</param>
        /// <param name="minWidth">minimum image width (inclusive)</param>
        /// <param name="maxWidth">maximum image width (inclusive)</param>
        /// <param name="minHeight">minimum image height (inclusive)</param>
        /// <param name="maxHeight">maximum image height (inclusive)</param>
        /// <returns>RssMediaContent</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public IEnumerable<RssMediaContent> GetImageItems(string mimeType = "", int minWidth = 0, int maxWidth = 0, int minHeight = 0, int maxHeight = 0)
        {
            return rssUtil.GetImageItems(this, mimeType, minWidth, maxWidth, minHeight, maxHeight);
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the first image that fits critera. 
        /// </summary>
        /// <param name="mimeType">mime type</param>
        /// <param name="minWidth">minimum image width (inclusive)</param>
        /// <param name="maxWidth">maximum image width (inclusive)</param>
        /// <param name="minHeight">minimum image height (inclusive)</param>
        /// <param name="maxHeight">maximum image height (inclusive)</param>
        /// <returns>RssMediaContent</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaContent GetImageItem(string mimeType = "", int minWidth = 0, int maxWidth = 0, int minHeight = 0, int maxHeight = 0)
        {
            return rssUtil.GetImageItem(this, mimeType, minWidth, maxWidth, minHeight, maxHeight);
        }




        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the effective image item from the group
        /// </summary>
        /// <returns>RssMediaContent</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaContent GetFirstMediaContentImageItem()
        {
            return rssUtil.GetFirstContentItem(this, RSS.MEDIUM_TYPE_IMAGE, "");
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the effective video item from the group
        /// </summary>
        /// <returns>RssMediaContent</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaContent GetFirstMediaContentVideoItem()
        {
            return rssUtil.GetFirstContentItem(this, RSS.MEDIUM_TYPE_VIDEO, "");            
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the effective video item from the group
        /// </summary>
        /// <returns>RssMediaContent</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaContent GetFirstMediaContentAudioItem()
        {
            return rssUtil.GetFirstContentItem(this, RSS.MEDIUM_TYPE_AUDIO, "");
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the effective video item from the group
        /// </summary>
        /// <returns>RssMediaContent</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaContent GetFirstMediaContentItem(string medium = "", string mimeType = "")
        {
            return rssUtil.GetFirstContentItem(this, medium, mimeType);
        }




        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets all the image items of the media group
        /// </summary>
        /// <returns></returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public IEnumerable<RssMediaContent> GetMediaContentImageItems()
        {
            return rssUtil.GetContentItems(this, RSS.MEDIUM_TYPE_IMAGE, "");
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets all the video items of the media group
        /// </summary>
        /// <returns>IEnumerable&lt;RssMediaContent&gt;</returns>
        /// -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public IEnumerable<RssMediaContent> GetMediaContentVideoItems()
        {
            return rssUtil.GetContentItems(this, RSS.MEDIUM_TYPE_VIDEO, "");
 
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets all the audio items of the media group
        /// </summary>
        /// <returns>IEnumerable&lt;RssMediaContent&gt;</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public IEnumerable<RssMediaContent> GetMediaContentAudioItems()
        {
            return rssUtil.GetContentItems(this, RSS.MEDIUM_TYPE_AUDIO, "");
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

            return rssUtil.GetContentItems(this, medium, mimeType);

        }



        /// <summary>
        /// Gets the parent item
        /// </summary>
        /// <returns></returns>
        public RssItem GetParentItem()
        {
            return rssUtil.GetParentItem(this);
        }


    }
}




