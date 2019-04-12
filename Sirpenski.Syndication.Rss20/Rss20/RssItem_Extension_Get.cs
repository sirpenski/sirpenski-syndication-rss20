// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssItem_Extension_Get.cs
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
using System.Xml.Linq;
using System.Xml;
using System.Net;
using System.IO;
using Sirpenski.Syndication.Rss20.Core;
using System.Diagnostics;



namespace Sirpenski.Syndication.Rss20
{

    /// <summary>
    /// This file contains the Get Method extensions to the RssItem class
    /// </summary>
    public partial class RssItem 
    {

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// This method gets the item description.  It selects either the description or the 
        /// content encoded value based upon preference.  The default is to get the content 
        /// encoded value.
        /// </summary>
        /// <param name="favor">Indicates which value (description or Content Encoded) is to be the default</param>
        /// <returns>string</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public string GetEffectiveDescription(int favor = RSS.GET_DESCRIPTION_FAVOR_CONTENT_ENCODED)
        {
            string rt = "";


            // if we are favoring content encoded, do it this way
            if (favor == RSS.GET_DESCRIPTION_FAVOR_CONTENT_ENCODED)
            {

                if (ContentEncoded != null)
                {
                    rt = ContentEncoded.encoded;
                }

                if (rt.Length == 0)
                {
                   rt = description;
                }
            }

            // if we are favoring item description, do it this way
            else
            {
                // set to description
                rt = description;

                // if the length is zero, then check the content encoded
                if (rt.Length == 0)
                {
                    if (ContentEncoded != null)
                    {
                        rt = ContentEncoded.encoded;
                    }
                }
            }

            return rt;

        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the effective thumbnail for this item.  This does not search children
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
        /// Gets the effective thumbnail for this item.  This goes down tree
        /// </summary>
        /// <returns>RssMediaThumbnail</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
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

            if (rt == null)
            {
                for (int i = 0; i < mediaGroups.Count; i++)
                {
                    rt = rssUtil.GetThumbnail(mediaGroups[i]);
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

            IEnumerable<RssMediaThumbnail> lst;

            // Get all the thumbnails for this item
            IEnumerable<RssMediaThumbnail> rt = rssUtil.GetThumbnails(this);

            // now get all the thumbnails for the content items
            for (int i = 0; i < mediaContentItems.Count; i++)
            {
                lst = rssUtil.GetThumbnails(mediaContentItems[i]);
                rt.Concat(lst);
            }

            // now get all the media group thumbnails.....this includes media content items
            for (int i = 0; i < mediaGroups.Count; i++)
            {
                lst = mediaGroups[i].GetThumbnailsAll();
                rt.Concat(lst);

            }
            return rt;

        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// THis returns the effective image for the item.  If the enclosure is found, it returns that in the form of a 
        /// RSSMediaContent object.  Otherwise, it searches the tree downwards looking for a media content item.
        /// </summary>
        /// <returns>RssMediaContent</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaContent GetFirstContentItem(string medium, string mimeType = "", bool IncludeEnclosures = true)
        {
            RssMediaContent rt = null;

            if (IncludeEnclosures)
            {
                // try and get an image enclosure
                RssCoreItemEnclosure enc = base.GetFirstEnclosure(medium, mimeType);

                // if we got an enclosure, good, set the return and we will fall through
                if (enc != null)
                {
                    rt = rssUtil.CreateMediaContentItemFromEnclosure(enc);
                }

            }


           // if no enclosures, search the media content items
            if (rt == null)
            {
                rt = rssUtil.GetFirstContentItem(this, medium, mimeType);

                // none found in current content items, lets look to the content groupings
                if (rt == null)
                {
                    for (int i = 0; i < mediaGroups.Count; i++)
                    {
                        rt = rssUtil.GetFirstContentItem(mediaGroups[i], medium, mimeType);
                        if (rt != null) 
                        {
                            break;
                        }
                    }
                }

            }

            return rt;

        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the first image item i.e. the default
        /// </summary>
        /// <param name="mimeType">Mime Type</param>
        /// <returns>RssMediaContent object</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaContent GetFirstImageItem(string mimeType = "", bool IncludeEnclosures = true)
        {
            return GetFirstContentItem(RSS.MEDIUM_TYPE_IMAGE, mimeType, IncludeEnclosures);
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the first image item i.e. the default
        /// </summary>
        /// <param name="mimeType">Mime Type</param>
        /// <returns>RssMediaContent object</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaContent GetFirstVideoItem(string mimeType = "", bool IncludeEnclosures = true)
        {
            return GetFirstContentItem(RSS.MEDIUM_TYPE_VIDEO, mimeType, IncludeEnclosures);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the first image item i.e. the default
        /// </summary>
        /// <param name="mimeType">Mime Type</param>
        /// <returns>RssMediaContent object</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaContent GetFirstAudioItem(string mimeType = "", bool IncludeEnclosures = true)
        {
            return GetFirstContentItem(RSS.MEDIUM_TYPE_AUDIO, mimeType, IncludeEnclosures);
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
            return GetMediaContentItems(RSS.MEDIUM_TYPE_IMAGE, "");
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
            return GetMediaContentItems(RSS.MEDIUM_TYPE_VIDEO, "");

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
            return GetMediaContentItems(RSS.MEDIUM_TYPE_AUDIO, "");
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

            IEnumerable<RssMediaContent> rt = rssUtil.GetContentItems(this, medium, mimeType);

            foreach (RssMediaGroup grp in mediaGroups)
            {
                IEnumerable<RssMediaContent> grplst = rssUtil.GetContentItems(grp, medium, mimeType);                
                rt = rt.Concat(grplst);
            }

            return rt;

        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the image media content items that meet the criteria
        /// </summary>
        /// <param name="mimeType">Mime Type</param>
        /// <param name="minWidth">Minimun Width</param>
        /// <param name="maxWidth">Maximum width</param>
        /// <param name="minHeight">Minimum Height</param>
        /// <param name="maxHeight">Maximun Height</param>
        /// <returns>IEnumerable&lt;RssMediaContent&gt;</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public IEnumerable<RssMediaContent> GetImageItems(string mimeType = "", int minWidth = 0, int maxWidth = 0, int minHeight = 0, int maxHeight = 0)
        {
            List<RssMediaContent> rt = new List<RssMediaContent>();

            // first, get current item images
            IEnumerable<RssMediaContent> lst = rssUtil.GetImageItems(this, mimeType, minWidth, maxWidth, minHeight, maxHeight);

            // add the images
            rt.Concat(lst);

            // now iterate through each group
            for (int i = 0; i < mediaGroups.Count; i++)
            {
                lst = rssUtil.GetImageItems(mediaGroups[i], mimeType, minWidth, maxWidth, minHeight, maxHeight);
                rt.Concat(lst);
            }
            return rt;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the first image that meets the criteria
        /// </summary>
        /// <param name="mimeType">Mime Type</param>
        /// <param name="minWidth">Minimun Width</param>
        /// <param name="maxWidth">Maximum width</param>
        /// <param name="minHeight">Minimum Height</param>
        /// <param name="maxHeight">Maximun Height</param>
        /// <returns>IEnumerable&lt;RssMediaContent&gt;</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaContent GetImageItem(string mimeType = "", int minWidth = 0, int maxWidth = 0, int minHeight = 0, int maxHeight = 0)
        {
            RssMediaContent rt = rssUtil.GetImageItem(this, mimeType, minWidth, maxWidth, minHeight, maxHeight);
            
            if (rt == null)
            {
                for (int i = 0; i < mediaGroups.Count; i++)
                {
                    rt = rssUtil.GetImageItem(mediaGroups[i], mimeType, minWidth, maxWidth, minHeight, maxHeight);
                    if (rt != null)
                    {
                        break;
                    }
                }


            }

            return rt;

        }



        /// <summary>
        /// Gets the parent channel for the item
        /// </summary>
        /// <returns>RssChannel</returns>
        public RssChannel GetParentChannel()
        {
            return rssUtil.GetParentChannel(this);
        }


    }
}



