// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssChannel.cs
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

    [Serializable]
    public partial class RssChannel: RssCoreChannel, IRssParentReference, IRssMediaExtension
    {
        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();


        [NonSerialized]
        private RssUtilities rssUtil = new RssUtilities();


        // Media Options
        public RssMediaExtensionObject mediaOptions { get; set; } = new RssMediaExtensionObject();

        /// <summary>
        /// Parent object reference.  i.e. the channel.  THIS IS ALWAYS NULL!!!!
        /// </summary>
        public object Parent
        {
            get { return null; }
            set { value = null; }
        }


  
    

        // atom link tags
        public List<RssAtomLink> AtomLinks { get; set; } = new List<RssAtomLink>();

        // creative commons license
        public RssCreativeCommonsLicense CreativeCommonsLicense { get; set; } = null;


        // Items with media
        public new List<RssItem> items { get; set; } = new List<RssItem>();


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Default Constructor
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssChannel() 
        {
            mediaOptions.Parent = this;    
            
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Sets the Channel object properties from the 
        /// </summary>
        /// <param name="parEl"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public new void SetEl(XElement parEl)
        {
            // set the base elements
            base.SetEl(parEl);

            // add the atom links
            if (AtomLinks.Count > 0)
            {
                for (int i = 0; i < AtomLinks.Count; i++)
                {
                    parEl.Add(AtomLinks[i].GetEl());
                }
            }

            if (CreativeCommonsLicense != null)
            {
                parEl.Add(CreativeCommonsLicense.GetEl());
            }

            // set the media options for the channel
            mediaOptions.Parent = this;
            mediaOptions.SetEl(parEl);

        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the Channel object properties as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public override XElement GetEl()
        {


            XElement parEl = xUtil.CreateEl(TAG_PARENT);

            // now set the channel element
            SetEl(parEl);

            // now iterate the items
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Parent = this;

                XElement itemEl = items[i].GetEl();

                parEl.Add(itemEl);
            }

            return parEl;
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssChannel object properties with the contents of the parent XElement
        /// </summary>
        /// <param name="el"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public new void LoadEl(XElement parEl)
        {

            if (parEl.Name.Namespace == XNamespace.None)
            {
                base.LoadEl(parEl);
            }


            else if (parEl.Name.Namespace == RSS.ATOM_NS)
            {

                switch (parEl.Name.LocalName)
                {
                    case RssAtomLink.TAG_PARENT:
                        RssAtomLink AtomLink = new RssAtomLink();
                        AtomLink.Load(parEl);
                        AtomLinks.Add(AtomLink);
                        break;
                }

            }

            else if (parEl.Name.Namespace == RSS.CREATIVE_COMMONS_NS)
            {
                switch(parEl.Name.LocalName)
                {
                    case RssCreativeCommonsLicense.TAG_PARENT:
                        CreativeCommonsLicense = new RssCreativeCommonsLicense();
                        CreativeCommonsLicense.Load(parEl);
                        break;
                }
            }
            else if (parEl.Name.Namespace == RSS.MEDIA_NS || 
                     parEl.Name.Namespace == RSS.DUBLIN_CORE_TERMS_NS || 
                     parEl.Name.Namespace == RSS.GEORSS_NS|| 
                     parEl.Name.Namespace == RSS.GML_NS)
            {
                mediaOptions.LoadEl(parEl);
            }
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the channel from the parent element
        /// </summary>
        /// <param name="parEl">Parent Element</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public override void Load(XElement parEl)
        {

            IEnumerable<XElement> lstEl = parEl.Elements();
            foreach (XElement el in lstEl)
            {

                // first, look at the tag name, if it is item, 
                // then we handle it here.  otherwise, we hand it off 
                // to load el
                switch (el.Name.LocalName)
                {
                    case TAG_ITEM:
                        RssItem itm = new RssItem(this);
                        itm.Load(el);
                        items.Add(itm);
                        break;

                    default:
                        LoadEl(el);
                        break;
                }

            }   // end for each element

        }   // end load



 




        [OnDeserializing]
        public void OnDeserialize(StreamingContext context)
        {
            xUtil = new RssXmlUtilities();
            rssUtil = new RssUtilities();
        }


    }
}




