// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaGroup_Extension_MediaOptionsAddObject.cs
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
    /// This partial contains the media add objects
    /// </summary>
    public partial class RssMediaGroup
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
            mediaOptions.AddMediaRating(obj);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a Media Title object
        /// </summary>
        /// <param name="obj">RssMediaTitel object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaTitle(RssMediaTitle obj)
        {
            mediaOptions.AddMediaTitle(obj);
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
            mediaOptions.AddMediaDescription(obj);
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
            mediaOptions.AddMediaThumbnail(obj);
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
            mediaOptions.AddMediaKeywords(obj);
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
            mediaOptions.AddMediaCategory(obj);
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
            mediaOptions.AddMediaHash(obj);
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
            mediaOptions.AddMediaPlayer(obj);
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media embed object 
        /// </summary>
        /// <param name="obj">RSSMediaEmbed object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaEmbed(RssMediaEmbed obj)
        {
            mediaOptions.AddMediaEmbed(obj);
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
            mediaOptions.AddMediaCredit(obj);
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
            mediaOptions.AddMediaCopyright(obj);
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
            mediaOptions.AddMediaText(obj);
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
            mediaOptions.AddMediaRestriction(obj);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a Media Community object
        /// </summary>
        /// <param name="obj">RssMediaCommityObject</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaCommunity(RssMediaCommunity obj)
        {
            mediaOptions.AddMediaCommunity(obj);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds media comments
        /// </summary>
        /// <param name="obj">RssMediaComments object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaComments(RssMediaComments obj)
        {
            mediaOptions.AddMediaComments(obj);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a MediaResponses Object
        /// </summary>
        /// <param name="obj">RssMediaResponses object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaResponses(RssMediaResponses obj)
        {
            mediaOptions.AddMediaResponses(obj);
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
            mediaOptions.AddMediaBacklinks(obj);
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
            mediaOptions.AddMediaStatus(obj);
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
            mediaOptions.AddMediaPrice(obj);
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
            mediaOptions.AddMediaLicense(obj);
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
            mediaOptions.AddMediaSubtitle(obj);
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
            mediaOptions.AddMediaPeerLink(obj);
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
            mediaOptions.AddMediaRights(obj);
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
            mediaOptions.AddMediaScenes(obj);
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
            mediaOptions.AddMediaLocation(obj);
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a Media Dublin Core Valid Terms object
        /// </summary>
        /// <param name="obj">DublinCoreValidTerms object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaValid(RssDublinCoreValid obj)
        {
            mediaOptions.AddMediaValid(obj);
        }


    }
}




