// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaExtensionObject.cs
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
    /// The media extension object provides an ecapsulated entity that contains all media optional items that 
    /// can be part of mediaContent, mediaGroup, Item, and Channel.  It's easy just to put one object together 
    /// and handle everything regardless of parent item
    /// </summary>

    [Serializable]
    public partial class RssMediaExtensionObject: IRssParentReference
    {

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

 
        // Parent interface
        public object Parent { get; set; } = null;


        // media ratings
        public List<RssMediaRating> MediaRatings { get; set; } = new List<RssMediaRating>();

        // Media title
        public RssMediaTitle MediaTitle { get; set; } = null;

        // media description
        public RssMediaDescription MediaDescription { get; set; } = null;

        // media keywords
        public RssMediaKeywords MediaKeywords { get; set; } = null;

        // media thumbnail
        public List<RssMediaThumbnail> MediaThumbnails { get; set; } = new List<RssMediaThumbnail>();

        // Media Categories
        public List<RssMediaCategory> MediaCategories { get; set; } = new List<RssMediaCategory>();


        // Media Hash
        public List<RssMediaHash> MediaHashes { get; set; } = new List<RssMediaHash>();

        // Media Player
        public RssMediaPlayer MediaPlayer { get; set; } = null;

        // Media Credits
        public List<RssMediaCredit> MediaCredits { get; set; } = new List<RssMediaCredit>();

        // Media Copyright
        public RssMediaCopyright MediaCopyright { get; set; } = null;

        // Media Text
        public List<RssMediaText> MediaTexts { get; set; } = new List<RssMediaText>();

        // Media Restrictions
        public List<RssMediaRestriction> MediaRestrictions { get; set; } = new List<RssMediaRestriction>();

        // Media Community
        public RssMediaCommunity MediaCommunity { get; set; } = null;

        // media comments.  single object holds the comments
        public RssMediaComments MediaComments { get; set; } = null;

        // player instructions
        public RssMediaEmbed MediaEmbed { get; set; } = null;

        // Media Responses contained in single object
        public RssMediaResponses MediaResponses { get; set; } = null;

        // media back links
        public RssMediaBacklinks MediaBacklinks { get; set; } = null;

        // media status
        public RssMediaStatus MediaStatus { get; set; } = null;

        // Media Price
        public List<RssMediaPrice> MediaPrices { get; set; } = new List<RssMediaPrice>();

        // Media License
        public RssMediaLicense MediaLicense { get; set; } = null;

        // Media Subtitles
        public List<RssMediaSubtitle> MediaSubtitles { get; set; } = new List<RssMediaSubtitle>();

        // Media Peerlink
        public RssMediaPeerLink MediaPeerLink { get; set; } = null;

        // media rights
        public RssMediaRights MediaRights { get; set; } = null;

        // contains scenes
        public RssMediaScenes MediaScenes { get; set; } = null;

        // media location
        public List<RssMediaLocation> MediaLocations { get; set; } = new List<RssMediaLocation>();

        // Dublin core Valid
        public RssDublinCoreValid MediaValid { get; set; } = null;


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Default Constructor
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaExtensionObject() { }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Constructor with parent object reference
        /// </summary>
        /// <param name="parentObjectRef">Reference To Parent Object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaExtensionObject(object parentObjectRef)
        {
            Parent = parentObjectRef;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Sets the parent XElement with the contents of the RssMediaExtension object 
        /// properties
        /// </summary>
        /// <param name="parEl">Parent element</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void SetEl(XElement parEl)
        {
            
            for (int i = 0; i < MediaRatings.Count; i++)
            {
                parEl.Add(MediaRatings[i].GetEl());
            }

            if (MediaTitle != null)
            {
                parEl.Add(MediaTitle.GetEl());
            }

            if (MediaDescription != null)
            {
                parEl.Add(MediaDescription.GetEl());
            }

            if (MediaKeywords != null)
            {
                parEl.Add(MediaKeywords.GetEl());
            }

            if (MediaThumbnails.Count > 0)
            {
                for (int i = 0; i < MediaThumbnails.Count; i++)
                {
                    parEl.Add(MediaThumbnails[i].GetEl());
                }
            }

            if (MediaCategories.Count > 0)
            {
                for (int i = 0; i < MediaCategories.Count; i++)
                {
                    parEl.Add(MediaCategories[i].GetEl());
                }
            }

            if (MediaHashes.Count > 0)
            {
                for (int i = 0; i < MediaHashes.Count; i++)
                {
                    parEl.Add(MediaHashes[i].GetEl());
                }
            }

            if (MediaPlayer != null)
            {
                parEl.Add(MediaPlayer.GetEl());
            }

            if (MediaCredits.Count > 0)
            {
                for (int i = 0; i < MediaCredits.Count; i++)
                {
                    parEl.Add(MediaCredits[i].GetEl());
                }
            }

            if (MediaCopyright != null)
            {
                parEl.Add(MediaCopyright.GetEl());
            }

            if (MediaTexts.Count > 0)
            {
                // sort the list
                MediaTexts.Sort(new SortMediaText());

                // now iterate
                for (int i = 0; i < MediaTexts.Count; i++)
                {
                    parEl.Add(MediaTexts[i].GetEl());
                }
            }


            if (MediaRestrictions.Count > 0)
            {
                for (int i = 0; i < MediaRestrictions.Count; i++)
                {
                    parEl.Add(MediaRestrictions[i].GetEl());
                }
            }


            if (MediaCommunity != null)
            {
                parEl.Add(MediaCommunity.GetEl());
            }

            if (MediaComments != null)
            {
                parEl.Add(MediaComments.GetEl());
            }


            if (MediaEmbed != null)
            {
                parEl.Add(MediaEmbed.GetEl());
            }

            if (MediaResponses != null)
            {
                parEl.Add(MediaResponses.GetEl());
            }

            if (MediaBacklinks != null)
            {
                parEl.Add(MediaBacklinks.GetEl());
            }

            if (MediaStatus != null)
            {
                parEl.Add(MediaStatus.GetEl());
            }

            if (MediaPrices.Count > 0)
            {
                for (int i = 0; i < MediaPrices.Count; i++)
                {
                    parEl.Add(MediaPrices[i].GetEl());
                }
            }

            if (MediaLicense != null)
            {
                parEl.Add(MediaLicense.GetEl());
            }

            if (MediaSubtitles.Count > 0)
            {
                for (int i = 0; i < MediaSubtitles.Count; i++)
                {
                    parEl.Add(MediaSubtitles[i].GetEl());
                }
            }

            if (MediaPeerLink != null)
            {
                parEl.Add(MediaPeerLink.GetEl());
            }

            if (MediaRights != null)
            {
                parEl.Add(MediaRights.GetEl());
            }

            if (MediaScenes != null)
            {
                parEl.Add(MediaScenes.GetEl());
            }

            if (MediaLocations.Count > 0)
            {
                for (int i = 0; i < MediaLocations.Count; i++)
                {
                    parEl.Add(MediaLocations[i].GetEl());
                }
            }

            if (MediaValid != null)
            {
                parEl.Add(MediaValid.GetEl());
            }
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// This loads property of the RssMediaExtension object with the contents of the 
        /// XElement
        /// </summary>
        /// <param name="el">Parent XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void LoadEl(XElement el)
        {
            // process this switch only if element is in the media namespace


            if (el.Name.Namespace == RSS.MEDIA_NS)
            {
                switch (el.Name.LocalName)
                {
                    case RssMediaRating.TAG_PARENT:
                        RssMediaRating rating = new RssMediaRating();
                        rating.Load(el);
                        MediaRatings.Add(rating);
                        break;

                    case RssMediaTitle.TAG_PARENT:
                        MediaTitle = new RssMediaTitle();
                        MediaTitle.Load(el);
                        break;

                    case RssMediaDescription.TAG_PARENT:
                        MediaDescription = new RssMediaDescription();
                        MediaDescription.Load(el);
                        break;

                    case RssMediaKeywords.TAG_PARENT:
                        MediaKeywords = new RssMediaKeywords();
                        MediaKeywords.Load(el);
                        break;

                    case RssMediaThumbnail.TAG_PARENT:
                        RssMediaThumbnail thumb = new RssMediaThumbnail(this);
                        thumb.Load(el);
                        MediaThumbnails.Add(thumb);
                        break;

                    case RssMediaCategory.TAG_PARENT:
                        RssMediaCategory MediaCategory = new RssMediaCategory();
                        MediaCategory.Load(el);
                        MediaCategories.Add(MediaCategory);
                        break;

                    case RssMediaHash.TAG_PARENT:
                        RssMediaHash hash = new RssMediaHash();
                        hash.Load(el);
                        MediaHashes.Add(hash);
                        break;

                    case RssMediaPlayer.TAG_PARENT:
                        MediaPlayer = new RssMediaPlayer();
                        MediaPlayer.Load(el);
                        break;

                    case RssMediaCredit.TAG_PARENT:
                        RssMediaCredit credit = new RssMediaCredit();
                        credit.Load(el);
                        MediaCredits.Add(credit);
                        break;

                    case RssMediaCopyright.TAG_PARENT:
                        MediaCopyright = new RssMediaCopyright();
                        MediaCopyright.Load(el);
                        break;

                    case RssMediaText.TAG_PARENT:
                        RssMediaText txt = new RssMediaText();
                        txt.Load(el);
                        MediaTexts.Add(txt);
                        break;

                    case RssMediaRestriction.TAG_PARENT:
                        RssMediaRestriction restriction = new RssMediaRestriction();
                        restriction.Load(el);
                        MediaRestrictions.Add(restriction);
                        break;

                    case RssMediaCommunity.TAG_PARENT:
                        MediaCommunity = new RssMediaCommunity();
                        MediaCommunity.Load(el);
                        break;

                    case RssMediaComments.TAG_PARENT:
                        MediaComments = new RssMediaComments();
                        MediaComments.Load(el);
                        break;

                    case RssMediaEmbed.TAG_PARENT:
                        MediaEmbed = new RssMediaEmbed();
                        MediaEmbed.Load(el);
                        break;

                    case RssMediaResponses.TAG_PARENT:
                        MediaResponses = new RssMediaResponses();
                        MediaResponses.Load(el);
                        break;

                    case RssMediaBacklinks.TAG_PARENT:
                        MediaBacklinks = new RssMediaBacklinks();
                        MediaBacklinks.Load(el);
                        break;


                    case RssMediaStatus.TAG_PARENT:
                        MediaStatus = new RssMediaStatus();
                        MediaStatus.Load(el);
                        break;

                    case RssMediaPrice.TAG_PARENT:
                        RssMediaPrice price = new RssMediaPrice();
                        price.Load(el);
                        MediaPrices.Add(price);
                        break;

                    case RssMediaLicense.TAG_PARENT:
                        MediaLicense = new RssMediaLicense();
                        MediaLicense.Load(el);
                        break;

                    case RssMediaSubtitle.TAG_PARENT:
                        RssMediaSubtitle subtitle = new RssMediaSubtitle();
                        subtitle.Load(el);
                        MediaSubtitles.Add(subtitle);
                        break;

                    case RssMediaPeerLink.TAG_PARENT:
                        MediaPeerLink = new RssMediaPeerLink();
                        MediaPeerLink.Load(el);
                        break;

                    case RssMediaRights.TAG_PARENT:
                        MediaRights = new RssMediaRights();
                        MediaRights.Load(el);
                        break;

                    case RssMediaScenes.TAG_PARENT:
                        MediaScenes = new RssMediaScenes();
                        MediaScenes.Load(el);
                        break;

                    case RssMediaLocation.TAG_PARENT:
                        RssMediaLocation mediaLocation = new RssMediaLocation();
                        mediaLocation.Load(el);
                        MediaLocations.Add(mediaLocation);
                        break;

                   
                }
            }


            // Dublin core terms namespace
            else if (el.Name.Namespace == RSS.DUBLIN_CORE_TERMS_NS) {


                switch(el.Name.LocalName)
                {
                    case RssDublinCoreValid.TAG_PARENT:
                        MediaValid = new RssDublinCoreValid();
                        MediaValid.Load(el);
                        break;
                }
            }

            else if (el.Name.Namespace == RSS.GEORSS_NS)
            {

            }

            else if (el.Name.Namespace == RSS.GML_NS)
            {

            }


        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// This sorts the media text according to lang and time.  THis way, we can implement close captioning, etc.
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public class SortMediaText : IComparer<RssMediaText>
        {
            StringComparer st = StringComparer.OrdinalIgnoreCase;

            public int Compare(RssMediaText obj1, RssMediaText obj2)
            {
                int rt = st.Compare(obj1.lang, obj2.lang);
                if (rt == 0)
                {
                    rt = TimeSpan.Compare(obj1.start, obj2.start);
                }
                return rt;

            }
        }




        [OnDeserializing]
        private void OnDeserialize(StreamingContext context)
        {
            xUtil = new RssXmlUtilities();
        }






    }
}




