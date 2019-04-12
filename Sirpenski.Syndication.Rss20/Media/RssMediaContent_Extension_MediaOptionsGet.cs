// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaContent_Extension_MediaOptionsGet.cs
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
using System.Runtime.Serialization;
using System.Diagnostics;


namespace Sirpenski.Syndication.Rss20
{

    /// <summary>
    /// Contains the Get Helper Methods for the media content item
    /// </summary>
    public partial class RssMediaContent
    {

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the effective thumbnail for the media content item
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
        /// Gets all the media thumbnails for the this content item
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
        /// This returns the effective image.  It either returns self (this) or null if it doesn't meet the specified 
        /// criteria.
        /// </summary>
        /// <param name="item_type">image, audio, document, etc.</param>
        /// <param name="item_mime">Type>Mime Type</param>
        /// <param name="minWidth">Minimum Width</param>
        /// <param name="maxWidth">Maximum Width</param>
        /// <param name="minHeight">Minimum Width</param>
        /// <param name="maxHeight">Maximum Width</param>
        /// <returns>RssMediaContent object</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaContent GetIfImageItem(string item_mimeType = "", int minWidth = 0, int maxWidth = 0, int minHeight = 0, int maxHeight = 0)
        {
            RssMediaContent rt = null;

            string tmp1 = "";
            string tmp2 = "";

            bool bRslt = false;
            
            if (RSS.IsMediumEqual(RSS.MEDIUM_TYPE_IMAGE, medium))
            {
                bRslt = true;
            }


            // if the result of the last operation is false, then 
            // test the mime type of the image.  If it is an image mime type, then 
            // set to true.
            if (!bRslt)
            {
                if (RSS.IsImageMimeType(type))
                {
                    bRslt = true;
                }
            }

            // if we still don't know if it is an image, then 
            // lets test the extension.  NOTE, extension might not be in the list.  
            // if not, then the feed creator should specify the medium or the mime type.
            if (!bRslt && medium.Length == 0 && type.Length == 0)
            {
                if (RSS.IsImageUrl(url))
                {
                    bRslt = true;
                }
            }


         
            // test mime type
            if (bRslt)
            {
                if (item_mimeType.Length > 0)
                {
                    tmp1 = type.ToLower();
                    tmp2 = item_mimeType.ToLower();
                    if (string.Compare(tmp1, tmp2) != 0)
                    {
                        bRslt = false;
                    }
                }
            }

            // min width test
            if (bRslt)
            {
                if (minWidth > 0)
                {
                    if (width < minWidth)
                    {
                        bRslt = false;
                    }
                }
            }


            // max width test
            if (bRslt)
            {
                if (maxWidth > 0)
                {
                    if (width > maxWidth)
                    {
                        bRslt = false;
                    }
                }
            }

            // min height
            if (bRslt)
            {
                if (minHeight > 0)
                {
                    if (height < minHeight)
                    {
                        bRslt = false;
                    }
                }
            }

            // max height
            if (bRslt)
            {
                if (maxHeight > 0)
                {
                    if (height > maxHeight)
                    {
                        bRslt = false;
                    }
                }
            }

            // if there are no errors, then return the self
            if (bRslt)
            {
                rt = this;
            }

            return rt;
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Returns itself if this is a valid video content item
        /// </summary>
        /// <returns>RssMediaContent object</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaContent GetIfVideoItem()
        {
            RssMediaContent rt = null;


            bool bRslt = false;

            // test the medium.  if it is image, set things to true
            if (RSS.IsMediumEqual("video", medium))
            {
                bRslt = true;
            }

            // if the result of the last operation is false, then 
            // test the mime type of the image.  If it is an image mime type, then 
            // set to true.
            if (!bRslt)
            {
                if (RSS.IsVideoMimeType(type))
                {
                    bRslt = true;
                }
            }

            // if we still don't know if it is an image, then 
            // lets test the extension.  NOTE, extension might not be in the list.  
            // if not, then the feed creator should specify the medium or the mime type.
            if (!bRslt)
            {
                if (medium.Length == 0 && type.Length == 0)
                {
                    if (RSS.IsVideoUrl(url))
                    {
                        bRslt = true;
                    }
                }
            }

            if (bRslt)
            {
                rt = this;

            }
            return rt;

        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Returns itself if this is a valid audio content item
        /// </summary>
        /// <returns>RssMediaContent object</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaContent GetIfAudioItem()
        {
            RssMediaContent rt = null;


            bool bRslt = false;

            // test the medium.  if it is image, set things to true
            if (RSS.IsMediumEqual("audio", medium))
            {
                bRslt = true;
            }

            // if the result of the last operation is false, then 
            // test the mime type of the image.  If it is an image mime type, then 
            // set to true.
            if (!bRslt)
            {
                if (RSS.IsAudioMimeType(type))
                {
                    bRslt = true;
                }
            }

            // if we still don't know if it is an image, then 
            // lets test the extension.  NOTE, extension might not be in the list.  
            // if not, then the feed creator should specify the medium or the mime type.
            // only check if medium not specified.
            if (!bRslt)
            {
                if (medium.Length == 0 && type.Length == 0)
                {
                    if (RSS.IsVideoUrl(url))
                    {
                        bRslt = true;
                    }
                }

            }

            if (bRslt)
            {
                rt = this;

            }
            return rt;

        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Returns self if medium or mimetype matches the properties of this item
        /// </summary>
        /// <param name="mediumX">medium</param>
        /// <param name="mimeTypeX">Mime Type</param>
        /// <returns>RssMediaContent</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaContent GetIfMediumOrMimeType(string mediumX = "", string mimeTypeX = "")
        {

            RssMediaContent rt = null;

            string med = mediumX.ToLower();
            string mtype = mimeTypeX.ToLower();

            bool mediumTest = med.Length > 0 ? true : false;
            bool mimeTypeTest = mtype.Length > 0 ? true : false;


            bool bRslt = false;
            if (bRslt)
            {
                if (mediumTest)
                {
                    if (string.Compare(med, medium) == 0)
                    {
                        bRslt = true;
                    }
                }
            }

            if (!bRslt)
            {
                if (mimeTypeTest)
                {
                    if (string.Compare(mtype, type) == 0)
                    {
                        bRslt = true;
                    }
                }
            }

            if (!bRslt)
            {
                if (!mediumTest && !mimeTypeTest)
                {
                    bRslt = true;
                }
            }

            if (bRslt)
            {
                rt = this;
            }

            return rt;
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




