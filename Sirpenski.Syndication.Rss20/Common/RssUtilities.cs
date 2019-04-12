// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssUtilities.cs
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
using Sirpenski.Syndication.Rss20.Core;


namespace Sirpenski.Syndication.Rss20
{

    /// <summary>
    /// A Collection of utilities used throughout
    /// </summary>
    public class RssUtilities
    {

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the media content items that meet the criteria
        /// </summary>
        /// <param name="obj">IRssMediaContent object</param>
        /// <param name="medium">medium, resource type</param>
        /// <param name="mimeType">mime type</param>
        /// <returns>IEnumerable&lt;RssMediaContent&gt;</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public IEnumerable<RssMediaContent> GetContentItems(IRssMediaContent obj, string medium = "", string mimeType = "", bool ExitAfterOneFound = false)
        {
            List<RssMediaContent> rt = new List<RssMediaContent>();
            bool TestMedium = medium.Length > 0 ? true : false;
            bool TestMimeType = mimeType.Length > 0 ? true : false;

            StringComparer cmp = StringComparer.OrdinalIgnoreCase;


            for (int i = 0; i < obj.mediaContentItems.Count; i++)
            {
                bool bRslt = false;

                // if test the medium
                if (TestMedium)
                {
                    switch (medium)
                    {
                        case RSS.MEDIUM_TYPE_IMAGE:
                            if (obj.mediaContentItems[i].GetIfImageItem() != null)
                            {
                                bRslt = true;
                            }
                            break;

                        case RSS.MEDIUM_TYPE_VIDEO:
                            if (obj.mediaContentItems[i].GetIfVideoItem() != null)
                            {
                                bRslt = true;
                            }
                            break;

                        case RSS.MEDIUM_TYPE_AUDIO:
                            if (obj.mediaContentItems[i].GetIfAudioItem() != null)
                            {
                                bRslt = true;
                            }
                            break;

                        default:
                            if (cmp.Compare(medium, obj.mediaContentItems[i].medium) == 0)
                            {
                                bRslt = true;
                            }
                            break;

                    }
                }

                // if not brslt and test mime type
                if (!bRslt && TestMimeType)
                {
                    if (cmp.Compare(mimeType, obj.mediaContentItems[i].type) == 0)
                    {
                        bRslt = true;
                    }
                }


                // if we didn't test the medium or the mime type, set the result to true 
                // so we add the item.  because we are just accumulating the items in this case
                if (!TestMedium && !TestMimeType)
                {
                    bRslt = true;
                }


                // if ok to add, then add it to the return set
                if (bRslt)
                {
                    rt.Add(obj.mediaContentItems[i]);


                    // if the ExitAfterOneFound flag is set, then we want to bail 
                    // this is for the get first things
                    if (ExitAfterOneFound)
                    {
                        break;
                    }

                }


            }   // next media content item


            return rt;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the media content items
        /// </summary>
        /// <param name="obj">IRssMediaContent object</param>
        /// <param name="medium">medium, resource type</param>
        /// <param name="mimeType">mime type</param>
        /// <returns>IEnumerable&lt;RssMediaContent&gt;</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaContent GetFirstContentItem(IRssMediaContent obj, string medium = "", string mimeType = "")
        {
            RssMediaContent rt = null;

            // get the content items in a list.  Should only be one.
            IEnumerable<RssMediaContent> lst = GetContentItems(obj, medium, mimeType, ExitAfterOneFound: true);

            // if the list count is greater than zero
            if (lst.Count() > 0)
            {
                // get the first one....the only one.
                rt = lst.First();
            }

            return rt;
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the media content items that meet the criteria
        /// </summary>
        /// <param name="obj">IRssMEdiaContent object</param>
        /// <param name="mimeType">Mime Type</param>
        /// <param name="minWidth">Minimun Width</param>
        /// <param name="maxWidth">Maximum width</param>
        /// <param name="minHeight">Minimum Height</param>
        /// <param name="maxHeight">Maximun Height</param>
        /// <returns>IEnumerable&lt;RssMediaContent&gt;</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public IEnumerable<RssMediaContent> GetImageItems(IRssMediaContent obj, string mimeType = "", int minWidth = 0, int maxWidth = 0, int minHeight = 0, int maxHeight = 0,  bool ExitAfterOneFound = false)
        {
            List<RssMediaContent> rt = new List<RssMediaContent>();
            bool TestMimeType = mimeType.Length > 0 ? true : false;

            StringComparer cmp = StringComparer.OrdinalIgnoreCase;


            // loop through each content item getting the ones that match the criteria
            for (int i = 0; i < obj.mediaContentItems.Count; i++)
            {

                RssMediaContent rslt = obj.mediaContentItems[i].GetIfImageItem(mimeType, minWidth, maxWidth, minHeight, maxHeight);

                if (rslt != null)
                {
                    rt.Add(rslt);

                    if (ExitAfterOneFound)
                    {
                        break;
                    }
                }

            }   // next media content item


            return rt;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets a single image
        /// </summary>
        /// <param name="obj">IRssMEdiaContent object</param>
        /// <param name="mimeType">Mime Type</param>
        /// <param name="minWidth">Minimun Width</param>
        /// <param name="maxWidth">Maximum width</param>
        /// <param name="minHeight">Minimum Height</param>
        /// <param name="maxHeight">Maximun Height</param>
        /// <returns>RssMediaContent</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaContent GetImageItem(IRssMediaContent obj, string mimeType = "", int minWidth = 0, int maxWidth = 0, int minHeight = 0, int maxHeight = 0)
        {
            RssMediaContent rt = null;
            IEnumerable<RssMediaContent> lst = GetImageItems(obj, mimeType, minWidth, maxWidth, minHeight, maxHeight, true);
            if (lst.Count() > 0)
            {
                rt = lst.First();
            }
            return rt;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Creates a RSSMediaContent object from a RssCoreItemEnclosure object
        /// </summary>
        /// <param name="enc">RSSItemEnclosure</param>
        /// <returns>RSSMediaContent</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaContent CreateMediaContentItemFromEnclosure(RssCoreItemEnclosure enc)
        {
            RssMediaContent rt = new RssMediaContent();
            rt.url = enc.url;
            rt.type = enc.type;
            
            if (RSS.IsImageMimeType(enc.type))
            {
                rt.medium = RSS.MEDIUM_TYPE_IMAGE;
            }
            else if (RSS.IsVideoMimeType(enc.type))
            {
                rt.medium = RSS.MEDIUM_TYPE_VIDEO;
            }
            else if (RSS.IsAudioMimeType(enc.type))
            {
                rt.medium = RSS.MEDIUM_TYPE_AUDIO;
            }

            return rt;
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the media thumbnails for an item
        /// </summary>
        /// <param name="obj">IRssMediaExtension</param>
        /// <returns>IEnumerable&t;RssMediaThumbnail&gt;</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public IEnumerable<RssMediaThumbnail> GetThumbnails(IRssMediaExtension obj)
        {
            return obj.mediaOptions.MediaThumbnails.ToList();
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the media thumbnails for an item
        /// </summary>
        /// <param name="obj">IRssMediaExtension</param>
        /// <returns>RssMediaThumbnail</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaThumbnail GetThumbnail(IRssMediaExtension obj)
        {

            RssMediaThumbnail rt = null;

            IEnumerable<RssMediaThumbnail> lst = GetThumbnails(obj);
            if (lst.Count() > 0)
            {
                rt = lst.First();
            }
            return rt;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// This gets the parent RssItem object
        /// </summary>
        /// <param name="obj">current object to start at</param>
        /// <returns>RssItem</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssItem GetParentItem(IRssParentReference obj)
        {
            RssItem rt = null;

            if (obj.GetType() == typeof(RssItem))
            {
                rt = (RssItem) obj;
            }
            else
            {
                if (obj.Parent != null)
                {
                    if (obj.Parent is IRssParentReference)
                    {
                        rt = GetParentItem((IRssParentReference)obj.Parent);
                    }
                }
            }

            return rt;
   
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// This gets the parent RssItem object
        /// </summary>
        /// <param name="obj">current object to start at</param>
        /// <returns>RssItem</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssChannel GetParentChannel(IRssParentReference obj)
        {
            RssChannel rt = null;

            if (obj.GetType() == typeof(RssChannel))
            {
                rt = (RssChannel)obj;
            }
            else
            {
                if (obj.Parent != null)
                {
                    if (obj.Parent is IRssParentReference)
                    {
                        rt = GetParentChannel((IRssParentReference)obj.Parent);
                    }
                }
            }

            return rt;

        }


    }
}
