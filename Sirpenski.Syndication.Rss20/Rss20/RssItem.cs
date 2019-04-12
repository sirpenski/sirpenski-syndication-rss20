// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssItem.cs
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
using Sirpenski.Syndication.Rss20.Core;



namespace Sirpenski.Syndication.Rss20
{
    // ----------------------------------------------------------------
    // Extends rss item
    // ----------------------------------------------------------------

    [Serializable]
    public partial class RssItem: RssCoreItem, IRssParentReference, IRssMediaExtension, IRssMediaContent
    {



        /// <summary>
        /// Parent Object Reference 
        /// </summary>
        public object Parent { get; set; } = null;


        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        [NonSerialized]
        private RssUtilities rssUtil = new RssUtilities();


        // media group, can be more than one group
        public List<RssMediaGroup> mediaGroups { get; set; } = new List<RssMediaGroup>();

        // media content, can be more than one.
        public List<RssMediaContent> mediaContentItems { get; set; } = new List<RssMediaContent>();

        // Media Options
        public RssMediaExtensionObject mediaOptions { get; set; } = new RssMediaExtensionObject();

        // Creator
        public List<RssDublinCoreCreator> creators { get; set; } = new List<RssDublinCoreCreator>();

        // Content Encoded
        public RssContentEncoded ContentEncoded { get; set; } = null;

        // atom link
        public RssAtomLink AtomLink { get; set; } = null;

        // slash comment count
        public RssSlashComments SlashComments { get; set; }  = null;

        // creative commons license
        public RssCreativeCommonsLicense CreativeCommonsLicense { get; set; } = null;



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Constructor
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssItem() 
        {
            mediaOptions.Parent = this; 
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Constructor with parent reference object
        /// </summary>
        /// <param name="parentObjectRef">Parent Object Reference</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssItem(object parentObjectRef)
        {
           
            Parent = parentObjectRef;
            mediaOptions.Parent = this;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Sets the element from the properties
        /// </summary>
        /// <param name="parEl">Parent element</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public new void SetEl(XElement parEl)
        {

            // set the base
            base.SetEl(parEl);


            // add encoded content after base
            if (ContentEncoded != null)
            {
                parEl.Add(ContentEncoded.GetEl());

            }


            // load the creators
            if (creators.Count > 0)
            {
                for (int i = 0; i < creators.Count; i++)
                {
                    parEl.Add(creators[i].GetEl());
                }
            }

            // get the item atom link
            if (AtomLink != null)
            {
                parEl.Add(AtomLink.GetEl());
            }

            if (SlashComments != null)
            {
                parEl.Add(SlashComments.GetEl());
            }
            
            if (CreativeCommonsLicense != null)
            {
                parEl.Add(CreativeCommonsLicense.GetEl());
            }

            // now set the media groups
            if (mediaGroups.Count > 0)
            {
                for (int i = 0; i < mediaGroups.Count; i++)
                {
                    mediaGroups[i].Parent = this;
                    parEl.Add(mediaGroups[i].GetEl());
                }
            }

            // set the media contents
            if (mediaContentItems.Count > 0)
            {
                for (int i = 0; i < mediaContentItems.Count; i++)
                {
                    mediaContentItems[i].Parent = this;
                    parEl.Add(mediaContentItems[i].GetEl());
                }
            }

            // set the media options
            mediaOptions.Parent = this;
            mediaOptions.SetEl(parEl);

        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the RssItem as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public override XElement GetEl()
        {
            XElement parEl = new XElement(TAG_PARENT);

            SetEl(parEl);

            return parEl;
        }




        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssItem object from the XElement
        /// </summary>
        /// <param name="el">Current Element</param>
        private new void LoadEl(XElement el)
        {
            // if the el is a core element, load from the base
            if (el.Name.Namespace == XNamespace.None)
            {
                base.LoadEl(el);
            }

            // if the el is in the media namespace or the dublin core terms namespace, load what we know.
            else if (el.Name.Namespace == RSS.MEDIA_NS || el.Name.Namespace == RSS.DUBLIN_CORE_TERMS_NS)
            {

                switch (el.Name.LocalName)
                {
                    case RssMediaGroup.TAG_PARENT:
                        RssMediaGroup mediaGroup = new RssMediaGroup(this);
                        mediaGroup.Load(el);
                        mediaGroups.Add(mediaGroup);
                        break;


                    case RssMediaContent.TAG_PARENT:
                        RssMediaContent mediaContent = new RssMediaContent(this);
                        mediaContent.Load(el);
                        mediaContentItems.Add(mediaContent);
                        break;


                    default:
                        mediaOptions.LoadEl(el);
                        break;
                }



            }

            // if the element is in the DublinCoreNS, load what we know
            else if (el.Name.Namespace == RSS.DUBLIN_CORE_NS)
            {

                switch (el.Name.LocalName)
                {
                    case RssDublinCoreCreator.TAG_PARENT:
                        RssDublinCoreCreator creator = new RssDublinCoreCreator();
                        creator.Load(el);
                        creators.Add(creator);
                        break;
                }

            }

            // is the element in the Content Namespace, if so, process it
            else if (el.Name.Namespace == RSS.CONTENT_NS)
            {
                switch (el.Name.LocalName)
                {
                    case RssContentEncoded.TAG_PARENT:
                        ContentEncoded = new RssContentEncoded();
                        ContentEncoded.Load(el);
                        break;
                }
            }

            else if (el.Name.Namespace == RSS.SLASH_NS)
            {
                switch (el.Name.LocalName)
                {
                    case RssSlashComments.TAG_PARENT:
                        SlashComments = new RssSlashComments();
                        SlashComments.Load(el);
                        break;
                }
            }

            else if (el.Name.Namespace == RSS.CREATIVE_COMMONS_NS)
            {
                switch (el.Name.LocalName)
                {
                    case RssCreativeCommonsLicense.TAG_PARENT:
                        CreativeCommonsLicense = new RssCreativeCommonsLicense();
                        CreativeCommonsLicense.Load(el);
                        break;
                }
            }

        }




        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the item and all children from this element.
        /// </summary>
        /// <param name="parEl"></param>
        /// <returns>bool</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public override void Load(XElement parEl)
        {
            
            IEnumerable<XElement> lstEls = parEl.Elements();
            foreach (XElement el in lstEls)
            {
  
               LoadEl(el);

            }   // end foreach

        }   // end load




        [OnDeserializing]
        private void OnDeserialize(StreamingContext context)
        {
            xUtil = new RssXmlUtilities();
            rssUtil = new RssUtilities();
        }

    }
}



