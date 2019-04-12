// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaExtensionObject_Interface.cs
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
    /// <summary>
    /// This interface is for the helper functions of the media extension.  RssChannel, RssItem, RssMediaContent, and RssMediaGroup all 
    /// contains similar helper methods at their respective class levels
    /// </summary>
    public interface IRssMediaExtension
    {
   
        // global media object
        RssMediaExtensionObject mediaOptions { get; set; }


        // -------------------------------------------------------------------------------
        // Add By Helper Methods
        // -------------------------------------------------------------------------------
        void AddMediaBacklink(string backlink);

        void AddMediaCategory(string ctg, string scheme = "", string label = "");

        void AddMediaComment(string comment);

        void AddMediaCommunity(double starRatingAverage, int starRatingCount, int starRatingMin, int starRatingMax, int statisticsViews, int statisticsFavorites, string tags = "");

        void AddMediaCopyright(string url);

        void AddMediaCredit(string credit, string role = "", string scheme = "");

        void AddMediaDescription(string description, string type);

        void AddMediaEmbeddedResource(string url, int width = 0, int height = 0);

        void AddMediaEmbeddedResourceParameter(string key, string value);

        void AddMediaHash(string hash, string algorithm = "");

        void AddMediaKeyword(string keyword);

        void AddMediaLicense(string license, string href, string type);

        void AddMediaLocation(string description, long startTimeInTicks = 0, long endTimeInTicks = 0, double latitude = 0.0, double longitude = 0.0);

        void AddMediaPeerLink(string href, string type = "");

        void AddMediaPlayer(string url, int width = 0, int height = 0);

        void AddMediaPrice(decimal price, string currency = "", string type = "", string info = "");

        void AddMediaRating(string rating, string scheme = "");

        void AddMediaResponse(string response);

        void AddMediaRestriction(string restriction, string relationship = "", string type = "");

        void AddMediaRights(string status);

        void AddMediaScene(string title, string description = "", long startTimeInTicks = 0, long endTimeInTicks = 0);

        void AddMediaStatus(string status, string reason = "");

        void AddMediaSubtitle(string href, string mimetype, string lang);

        void AddMediaText(string text, string mimetype, string lang, long startTimeInTicks = 0, long endTimeInTicks = 0);

        void AddMediaThumbnail(string url, int width = 0, int height = 0, long startTimeInTicks = 0);

        void AddMediaTitle(string title, string contenttype);

        void AddMediaValid(DateTime start, DateTime end, string scheme = "");


        // ------------------------------------------------------------------
        // Add by objects
        // ------------------------------------------------------------------

        void AddMediaRating(RssMediaRating obj);

        void AddMediaTitle(RssMediaTitle obj);

        void AddMediaDescription(RssMediaDescription obj);

        void AddMediaThumbnail(RssMediaThumbnail obj);

        void AddMediaKeywords(RssMediaKeywords obj);

        void AddMediaCategory(RssMediaCategory obj);

        void AddMediaHash(RssMediaHash obj);

        void AddMediaPlayer(RssMediaPlayer obj);

        void AddMediaCredit(RssMediaCredit obj);

        void AddMediaCopyright(RssMediaCopyright obj);

        void AddMediaText(RssMediaText obj);

        void AddMediaRestriction(RssMediaRestriction obj);

        void AddMediaCommunity(RssMediaCommunity obj);

        void AddMediaComments(RssMediaComments obj);

        void AddMediaEmbed(RssMediaEmbed obj);

        void AddMediaResponses(RssMediaResponses obj);

        void AddMediaBacklinks(RssMediaBacklinks obj);

        void AddMediaStatus(RssMediaStatus obj);

        void AddMediaPrice(RssMediaPrice obj);

        void AddMediaLicense(RssMediaLicense obj);

        void AddMediaSubtitle(RssMediaSubtitle obj);

        void AddMediaPeerLink(RssMediaPeerLink obj);

        void AddMediaRights(RssMediaRights obj);

        void AddMediaScenes(RssMediaScenes obj);

        void AddMediaLocation(RssMediaLocation obj);

        void AddMediaValid(RssDublinCoreValid obj);
 

    }
}
