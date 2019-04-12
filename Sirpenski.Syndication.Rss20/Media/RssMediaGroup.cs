// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaGroup.cs
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
    /// RssMediaGroup defines a media grouping object which allows multiple media contents of the same type to be grouped together.
    /// </summary>
    [Serializable]
    public partial class RssMediaGroup: IRssParentReference, IRssMediaExtension, IRssMediaContent
    {
        public const string TAG_PARENT = "group";
        public const string TAG_MEDIACONTENT = "content";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        [NonSerialized]
        private RssUtilities rssUtil = new RssUtilities();

   
        /// <summary>
        /// Parent reference.  May be Item, Channel
        /// </summary>
        public object Parent { get; set; } = null;

  

        /// <summary>
        /// Media Content Items Collection
        /// </summary>
        public List<RssMediaContent> mediaContentItems { get; set; } = new List<RssMediaContent>();


        

        /// <summary>
        ///   media item extension object
        /// </summary>
        public RssMediaExtensionObject mediaOptions { get; set; } = new RssMediaExtensionObject();






        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Default Constructor
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaGroup() 
        {
            mediaOptions.Parent = this;    
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Constructor With Parent Object Reference
        /// </summary>
        /// <param name="parentObjectRef">Reference To Parent Object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaGroup(object parentObjectRef)
        {
            Parent = parentObjectRef;
            mediaOptions.Parent = this;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the media group properties as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = xUtil.CreateNSEl(TAG_PARENT, RSS.MEDIA_NS);

            for (int i = 0; i < mediaContentItems.Count; i++)
            {
 
                mediaContentItems[i].Parent = this;
                XElement recEl = mediaContentItems[i].GetEl();
                parEl.Add(recEl);
            }

            mediaOptions.SetEl(parEl);


            return parEl;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the media group properties with the content of the XElement
        /// </summary>
        /// <param name="parEl">XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {

            if (parEl.Name.Namespace == RSS.MEDIA_NS || 
                parEl.Name.Namespace == RSS.DUBLIN_CORE_TERMS_NS || 
                parEl.Name.Namespace == RSS.GEORSS_NS || 
                parEl.Name.Namespace == RSS.GML_NS)
            {

                IEnumerable<XElement> lst = parEl.Elements();
                foreach (XElement el in lst)
                {

                    switch (el.Name.LocalName)
                    {
                        case TAG_MEDIACONTENT:
                            RssMediaContent mc = new RssMediaContent(this);
                            mc.Load(el);
                            mediaContentItems.Add(mc);
                            break;

                        // load the media options if any exist for the media group element
                        default:
                            mediaOptions.LoadEl(el);
                            break;
                    }

                }

            }

        }


        [OnDeserializing]
        private void OnDeserialize(StreamingContext context)
        {
            xUtil = new RssXmlUtilities();
            rssUtil = new RssUtilities();
        }

    }
}




