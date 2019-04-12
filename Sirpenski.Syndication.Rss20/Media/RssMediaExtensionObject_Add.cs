// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaExtensionObject_Add.cs
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
    /// The Media Extension object extension contains all the add helpermethods of the Media Extension object
    /// </summary>


    public partial class RssMediaExtensionObject
    {

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a backlink
        /// </summary>
        /// <param name="backlink">Backlink url</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaBacklink(string backlink)
        {
            if (MediaBacklinks == null)
            {
                MediaBacklinks = new RssMediaBacklinks();
            }
            MediaBacklinks.Add(backlink);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a category to the media extension object
        /// </summary>
        /// <param name="ctg">Category</param>
        /// <param name="scheme">scheme</param>
        /// <param name="label">label</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaCategory(string ctg, string scheme = "", string label = "")
        {
           
            RssMediaCategory MediaCategory = new RssMediaCategory();
            MediaCategory.category = ctg;
            MediaCategory.scheme = scheme;
            MediaCategory.label = label;
            MediaCategories.Add(MediaCategory);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media comment
        /// </summary>
        /// <param name="comment">Comment</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaComment(string comment)
        {
            if (MediaComments == null)
            {
                MediaComments = new RssMediaComments();
            }
            MediaComments.Add(comment);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a community rating to the media object
        /// </summary>
        /// <param name="starRatingAverage">Star Rating Average</param>
        /// <param name="starRatingCount">Star Rating Count</param>
        /// <param name="starRatingMin">Star Rating Min</param>
        /// <param name="starRatingMax">Star Rating Max</param>
        /// <param name="statisticsViews">View Count</param>
        /// <param name="statisticsFavorites">Favorite Count</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaCommunity(double starRatingAverage, int starRatingCount, int starRatingMin, int starRatingMax, 
                                    int statisticsViews, int statisticsFavorites, string tags = "")
        {
            if (MediaCommunity == null)
            {
                MediaCommunity = new RssMediaCommunity();
            }
            MediaCommunity.starRatingAverage = starRatingAverage;
            MediaCommunity.starRatingCount = starRatingCount;
            MediaCommunity.starRatingMax = starRatingMax;
            MediaCommunity.starRatingMin = starRatingMin;
            MediaCommunity.statisticsViews = statisticsViews;
            MediaCommunity.statisticsFavorites = statisticsFavorites;
            MediaCommunity.tags = tags;
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a copyright to the media
        /// </summary>
        /// <param name="url">url where copyright information displayed</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaCopyright(string url)
        {
            if (MediaCopyright == null)
            {
                MediaCopyright = new RssMediaCopyright();
            }
            MediaCopyright.url = url;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media credit
        /// </summary>
        /// <param name="credit">Media Creator</param>
        /// <param name="role">Role of Creator</param>
        /// <param name="scheme">url that identifies role scheme</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaCredit(string credit, string role = "", string scheme = "")
        {
            RssMediaCredit crd = new RssMediaCredit();
            crd.credit = credit;
            crd.role = role;
            crd.scheme = scheme;
            MediaCredits.Add(crd);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media description to the media 
        /// </summary>
        /// <param name="description">description</param>
        /// <param name="type">type of text in description field, ie plain, html</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaDescription(string description, string type)
        {
            if (MediaDescription == null)
            {
                MediaDescription = new RssMediaDescription();
            }
            MediaDescription.description = description;
            MediaDescription.type = type;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds an Embedded Resource (media player, etc) to the media options
        /// </summary>
        /// <param name="url">Media Player url location</param>
        /// <param name="width">Media Player Width</param>
        /// <param name="height">Media Player Height</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaEmbeddedResource(string url, int width = 0, int height = 0)
        {
            if (MediaEmbed == null)
            {
                MediaEmbed = new RssMediaEmbed();
            }
            MediaEmbed.url = url;
            MediaEmbed.width = width;
            MediaEmbed.height = height;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media embed parameter
        /// </summary>
        /// <param name="key">Embedded Resource Parameter Key</param>
        /// <param name="value">Embedded Resource Parameter Value</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaEmbeddedResourceParameter(string key, string value)
        {
            if (MediaEmbed == null)
            {
                MediaEmbed = new RssMediaEmbed();
            }
            MediaEmbed.Add(key, value);
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media hash to the item
        /// </summary>
        /// <param name="hash">Hash Value</param>
        /// <param name="algorithm">Alogrithm used to create hash</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaHash(string hash, string algorithm = "")
        {
            RssMediaHash hashObj = new RssMediaHash();
            hashObj.hash = hash;
            hashObj.algo = algorithm;
            MediaHashes.Add(hashObj);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media keyword to the item
        /// </summary>
        /// <param name="keyword">key word</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaKeyword(string keyword)
        {
            if (MediaKeywords == null)
            {
                MediaKeywords = new RssMediaKeywords();
            }
            MediaKeywords.Add(keyword);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media license to the media item
        /// </summary>
        /// <param name="license">license title</param>
        /// <param name="href">url where license description is</param>
        /// <param name="type">content type of text provided at the href url</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaLicense(string license, string href, string type)
        {
            if (MediaLicense == null)
            {
                MediaLicense = new RssMediaLicense();
            }
            MediaLicense.license = license;
            MediaLicense.href = href;
            MediaLicense.type = type;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a location to the media item
        /// </summary>
        /// <param name="description">descripton of the location</param>
        /// <param name="startTimeInTicks">start time in TICKS</param>
        /// <param name="endTimeInTicks">end time in TICKS</param>
        /// <param name="latitude">latitude</param>
        /// <param name="longitude">longitude</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaLocation(string description, long startTimeInTicks = 0, long endTimeInTicks = 0, double latitude = 0.0, double longitude = 0.0)
        {
           
            RssMediaLocation mediaLocation = new RssMediaLocation();
            mediaLocation.description = description;
            if (startTimeInTicks != 0)
            {
                mediaLocation.start = new TimeSpan(startTimeInTicks);
            }
            if (endTimeInTicks != 0)
            {
                mediaLocation.end = new TimeSpan(endTimeInTicks);
            }
            mediaLocation.latitude = latitude;
            mediaLocation.longitude = longitude;
            MediaLocations.Add(mediaLocation);


        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Add a peer to peer link where media is located
        /// </summary>
        /// <param name="href">url</param>
        /// <param name="type">Mime Type</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaPeerLink(string href, string type = "")
        {
            if (MediaPeerLink == null)
            {
                MediaPeerLink = new RssMediaPeerLink();
            }
            MediaPeerLink.href = href;
            MediaPeerLink.type = type;

        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a default media player for the item if one can't be found in the embed
        /// </summary>
        /// <param name="url">url location of media player</param>
        /// <param name="width">width of media player window</param>
        /// <param name="height">height of media player window</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaPlayer(string url, int width = 0, int height = 0)
        {
            if (MediaPlayer == null)
            {
                MediaPlayer = new RssMediaPlayer();
            }
            MediaPlayer.url = url;
            MediaPlayer.width = width;
            MediaPlayer.height = height;
            
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media price object to the item
        /// </summary>
        /// <param name="price">amount</param>
        /// <param name="currency">currency</param>
        /// <param name="type">type of pricing</param>
        /// <param name="info">url with more information</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaPrice(decimal price, string currency = "", string type = "", string info = "")
        {
            RssMediaPrice prc = new RssMediaPrice();
            prc.price = price;
            prc.currency = currency;
            prc.type = type;
            prc.info = info;
            MediaPrices.Add(prc);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media rating to the item
        /// </summary>
        /// <param name="rating">rating</param>
        /// <param name="scheme">URI that explains the rating</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaRating(string rating, string scheme = "")
        {
            RssMediaRating r = new RssMediaRating();
            r.rating = rating;
            r.scheme = scheme;
            MediaRatings.Add(r);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a user response to the media
        /// </summary>
        /// <param name="response">the response</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaResponse(string response)
        {
            if (MediaResponses == null)
            {
                MediaResponses = new RssMediaResponses();
            }
            MediaResponses.Add(response);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a restriction to the media
        /// </summary>
        /// <param name="restriction">restriction value</param>
        /// <param name="relationship">to what restriction applies</param>
        /// <param name="type">the type of restriction</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaRestriction(string restriction, string relationship = "", string type = "")
        {
            RssMediaRestriction r = new RssMediaRestriction();
            r.restriction = restriction;
            r.relationship = relationship;
            r.type = type;
            MediaRestrictions.Add(r);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds the media right to the item
        /// </summary>
        /// <param name="status">status</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaRights(string status)
        {
            if (MediaRights == null)
            {
                MediaRights = new RssMediaRights();
            }
            MediaRights.status = status;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media scene description
        /// </summary>
        /// <param name="title">Scene title</param>
        /// <param name="description">Scene description</param>
        /// <param name="startTimeInTicks">start time of scene in ticks (timespan)</param>
        /// <param name="endTimeInTicks">end time of scene in ticks (timespan)</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaScene(string title, string description = "", long startTimeInTicks = 0, long endTimeInTicks = 0)
        {
            if (MediaScenes == null)
            {
                MediaScenes = new RssMediaScenes();
            }
            RssMediaScene s = new RssMediaScene();
            s.sceneTitle = title;
            s.sceneDescription = description;
            if (startTimeInTicks > 0)
            {
                s.sceneStartTime = new TimeSpan(startTimeInTicks);
            }
            if (endTimeInTicks > 0)
            {
                s.sceneEndTime = new TimeSpan(endTimeInTicks);
            }
            MediaScenes.Add(s);
            
        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media status item
        /// </summary>
        /// <param name="status">The status</param>
        /// <param name="reason">The url describing the status</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaStatus(string status, string reason = "")
        {
            if (MediaStatus == null)
            {
                MediaStatus = new RssMediaStatus();
            }
            MediaStatus.state = status;
            MediaStatus.reason = reason;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media subtitle entry
        /// </summary>
        /// <param name="href">url where subtitles are</param>
        /// <param name="mimetype">mime type</param>
        /// <param name="lang">language</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaSubtitle(string href, string mimetype, string lang)
        {
            RssMediaSubtitle s = new RssMediaSubtitle();
            s.href = href;
            s.type = mimetype;
            s.lang = lang;
            MediaSubtitles.Add(s);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds media text to the item
        /// </summary>
        /// <param name="text">text</param>
        /// <param name="mimetype">mime type of text</param>
        /// <param name="lang">language</param>
        /// <param name="startTimeInTicks">start time</param>
        /// <param name="endTimeInTicks">end time</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaText(string text, string mimetype, string lang, long startTimeInTicks = 0, long endTimeInTicks = 0)
        {
            RssMediaText txt = new RssMediaText();
            txt.text = text;
            txt.type = mimetype;
            txt.lang = lang; 
            if (startTimeInTicks > 0)
            {
                txt.start = new TimeSpan(startTimeInTicks);
            }
            if (endTimeInTicks > 0)
            {
                txt.end = new TimeSpan(endTimeInTicks);
            }
            MediaTexts.Add(txt);
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Helper method to add media thumbnail
        /// </summary>
        /// <param name="url">image url</param>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        /// <param name="startTimeInTicks">Start time thumbnail should be displayed</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaThumbnail(string url, int width = 0, int height = 0, long startTimeInTicks = 0)
        {
            RssMediaThumbnail obj = new RssMediaThumbnail();
            obj.url = url;
            obj.width = width;
            obj.height = height;
            if (startTimeInTicks > 0)
            {
                obj.time = new TimeSpan(startTimeInTicks);
            }
            MediaThumbnails.Add(obj);

        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media title
        /// </summary>
        /// <param name="title">title</param>
        /// <param name="type">content type</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaTitle(string title, string contenttype)
        {
            if (MediaTitle == null)
            {
                MediaTitle = new RssMediaTitle();
            }
            MediaTitle.title = title;
            MediaTitle.type = contenttype;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a new Valid parameter
        /// </summary>
        /// <param name="start">Start Date Media VAlid</param>
        /// <param name="end">end date valid </param>
        /// <param name="scheme">urn defining the validity</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaValid(DateTime start, DateTime end, string scheme = "")
        {
            if (MediaValid == null)
            {
                MediaValid = new RssDublinCoreValid();
            }
            MediaValid.start = start;
            MediaValid.end = end;
            MediaValid.scheme = scheme;
        }

    }
}




