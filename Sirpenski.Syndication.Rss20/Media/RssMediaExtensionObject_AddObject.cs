// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaExtensionObject_AddObject.cs
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
    /// The Media Extension object extension contains all the helpermethods that add objects
    /// </summary>


    public partial class RssMediaExtensionObject
    {

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media rating
        /// </summary>
        /// <param name="obj">RssMediaRating object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaRating(RssMediaRating obj)
        {
            MediaRatings.Add(obj);
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a Media Title object
        /// </summary>
        /// <param name="obj">RssMediaTitle object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaTitle(RssMediaTitle obj)
        {
            MediaTitle = obj;
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media description object
        /// </summary>
        /// <param name="obj">RssMediaDescription object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaDescription(RssMediaDescription obj)
        {
            MediaDescription = obj;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media thumbnail object
        /// </summary>
        /// <param name="obj">RssMediaThumbnail object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaThumbnail(RssMediaThumbnail obj)
        {
            MediaThumbnails.Add(obj);
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media keyword object
        /// </summary>
        /// <param name="obj">RssMediaKeywords object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaKeywords(RssMediaKeywords obj)
        {
            MediaKeywords = obj;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media category object
        /// </summary>
        /// <param name="obj">RssMediaCategory object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaCategory(RssMediaCategory obj)
        {
            MediaCategories.Add(obj);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// adds a media hash object
        /// </summary>
        /// <param name="obj">RssMediaHash object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaHash(RssMediaHash obj)
        {
            MediaHashes.Add(obj);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media player object
        /// </summary>
        /// <param name="obj">RssMediaPlayer object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaPlayer(RssMediaPlayer obj)
        {
            MediaPlayer = obj;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media embed object
        /// </summary>
        /// <param name="obj">RssMediaEmbed object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaEmbed(RssMediaEmbed obj)
        {
            MediaEmbed = obj;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media credit object
        /// </summary>
        /// <param name="obj">RssMediaCredit object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaCredit(RssMediaCredit obj)
        {
            MediaCredits.Add(obj);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media copyright object
        /// </summary>
        /// <param name="obj">RssMediaCopyright object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaCopyright(RssMediaCopyright obj)
        {
            MediaCopyright = obj;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds A Media Text Object
        /// </summary>
        /// <param name="obj">RssMediaText object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaText(RssMediaText obj)
        {
            MediaTexts.Add(obj);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a Media Restriction object
        /// </summary>
        /// <param name="obj">RssMediaRestriction object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaRestriction(RssMediaRestriction obj)
        {
            MediaRestrictions.Add(obj);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a Media Community object
        /// </summary>
        /// <param name="obj"></param>
        // -------------------------------------------------------------------------------6
        // -------------------------------------------------------------------------------
        public void AddMediaCommunity(RssMediaCommunity obj)
        {
            MediaCommunity = obj;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media comments object
        /// </summary>
        /// <param name="obj">RssMediaComments object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaComments(RssMediaComments obj)
        {
            MediaComments = obj;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a MediaResponses Object
        /// </summary>
        /// <param name="obj">RssMediaResponse object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaResponses(RssMediaResponses obj)
        {
            MediaResponses = obj;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media backlinks object
        /// </summary>
        /// <param name="obj">RssMediaBacklinks object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaBacklinks(RssMediaBacklinks obj)
        {
            MediaBacklinks = obj;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media status object
        /// </summary>
        /// <param name="obj">RssMediaStatus object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaStatus(RssMediaStatus obj)
        {
            MediaStatus = obj;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media price object
        /// </summary>
        /// <param name="obj">RssMediaPrice object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaPrice(RssMediaPrice obj)
        {
            MediaPrices.Add(obj);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media license object
        /// </summary>
        /// <param name="obj">RssMediaLicense object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaLicense(RssMediaLicense obj)
        {
            MediaLicense = obj;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a Media subtitle object
        /// </summary>
        /// <param name="obj">RssMediaSubtitle object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaSubtitle(RssMediaSubtitle obj)
        {
            MediaSubtitles.Add(obj);
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media peer link object
        /// </summary>
        /// <param name="obj">RssMediaPeerLink object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaPeerLink(RssMediaPeerLink obj)
        {
            MediaPeerLink = obj;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// adds a media rights object
        /// </summary>
        /// <param name="obj">RssMediaRights object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaRights(RssMediaRights obj)
        {
            MediaRights = obj;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media scenes object
        /// </summary>
        /// <param name="obj">RssMediaScenes object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaScenes(RssMediaScenes obj)
        {
            MediaScenes = obj;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media location object
        /// </summary>
        /// <param name="obj">RssMediaLocation object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaLocation(RssMediaLocation obj)
        {
            MediaLocations.Add(obj);
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a Media Dublin Core Valid Terms object
        /// </summary>
        /// <param name="obj">RssDublinCoreValid object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaValid(RssDublinCoreValid obj)
        {
            MediaValid = obj;
        }


    }
}




