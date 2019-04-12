// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaContent.cs
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
    /// RssMediaContent defines a media content item
    /// </summary>
    [Serializable]
    public partial class RssMediaContent: IRssParentReference, IRssMediaExtension
    {
        public const string TAG_PARENT = "content";
        public const string ATTR_URL = "url";
        public const string ATTR_FILESIZE = "fileSize";
        public const string ATTR_TYPE = "type";
        public const string ATTR_MEDIUM = "medium";
        public const string ATTR_ISDEFAULT = "isDefault";
        public const string ATTR_EXPRESSION = "expression";
        public const string ATTR_BITRATE = "bitrate";
        public const string ATTR_FRAMERATE = "framerate";
        public const string ATTR_SAMPLINGRATE = "samplingrate";
        public const string ATTR_CHANNELS = "channels";
        public const string ATTR_DURATION = "duration";
        public const string ATTR_HEIGHT = "height";
        public const string ATTR_WIDTH = "width";
        public const string ATTR_LANG = "lang";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();


        [NonSerialized]
        private RssUtilities rssUtil = new RssUtilities();

        /// <summary>
        /// Reference to parent object, may be channel, item, media group
        /// </summary>
        public object Parent { get; set; } = null;

 
        /// <summary>
        /// url should specify the direct URL to the media object
        /// </summary>
        public string url { get; set; } = "";

        /// <summary>
        /// fileSize is the number of bytes of the media object. It is an optional attribute.
        /// </summary>
        public long fileSize { get; set; } = 0;

        /// <summary>
        /// type is the standard MIME type of the object. It is an optional attribute.
        /// </summary>
        public string type { get; set; } = "";

        /// <summary>
        /// medium is the type of object (image | audio | video | document | executable). 
        /// While this attribute can at times seem redundant if type is supplied, 
        /// it is included because it simplifies decision making on the reader side, 
        /// as well as flushes out any ambiguities between MIME type and object type. It is an optional attribute.
        /// </summary>
        public string medium { get; set; } = "";

        /// <summary>
        /// isDefault determines if this is the default object that should be used for the <media:group>. 
        /// There should only be one default object per <media:group>. It is an optional attribute.
        /// </summary>
        public bool isDefault { get; set; } = false;

        /// <summary>
        /// expression determines if the object is a sample or the full version of the object, 
        /// or even if it is a continuous stream (sample | full | nonstop). Default value is "full". It is an optional attribute.
        /// </summary>
        public string expression { get; set; } = "";

        /// <summary>
        /// bitrate is the kilobits per second rate of media. It is an optional attribute.
        /// </summary>
        public int bitrate { get; set; } = 0;

        /// <summary>
        /// framerate is the number of frames per second for the media object. It is an optional attribute.
        /// </summary>
        public int frameRate { get; set; } = 0;

        /// <summary>
        /// samplingrate is the number of samples per second taken to create the media object. 
        /// It is expressed in thousands of samples per second (kHz). It is an optional attribute.
        /// </summary>
        public double samplingrate { get; set; } = 0.0;

        /// <summary>
        /// channels is number of audio channels in the media object. It is an optional attribute.
        /// </summary>
        public int channels { get; set; } = 0;

        /// <summary>
        /// duration is the number of seconds the media object plays. It is an optional attribute.
        /// </summary>
        public long duration { get; set; } = 0;

        /// <summary>
        /// height is the height of the media object. It is an optional attribute.
        /// </summary>
        public int height { get; set; } = 0;

        /// <summary>
        /// width is the width of the media object. It is an optional attribute.
        /// </summary>
        public int width { get; set; } = 0;

        /// <summary>
        /// lang is the primary language encapsulated in the media object. Language codes possible are 
        /// detailed in RFC 3066. This attribute is used similar to the xml:lang attribute 
        /// detailed in the XML 1.0 Specification (Third Edition). It is an optional attribute.
        /// </summary>
        public string lang { get; set; } = "";


        // media extension object
        public RssMediaExtensionObject mediaOptions { get; set; } = new RssMediaExtensionObject();




        /// <summary>
        /// Default Constructor
        /// </summary>
        public RssMediaContent() 
        {
            mediaOptions.Parent = this;    
            
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="media_ns">Media XNamespace</param>
        /// <param name="dublincore_ns">Dublin Core XNamespace</param>
        /// <param name="georss_ns">Geo RSS XNamespace</param>
        /// <param name="gml_ns">GML XNamespace</param>
        /// <param name="parentObjectRef">Reference To Parent Object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaContent(object parentObjectRef)
        {
            Parent = parentObjectRef;
            mediaOptions.Parent = this;
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the Media Content Properties as an XElement
        /// </summary>
        /// <returns></returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = xUtil.CreateNSEl(TAG_PARENT, RSS.MEDIA_NS);

            if (url.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_URL, url);
            }
            if (fileSize > 0)
            {
                xUtil.AddAttr(parEl, ATTR_FILESIZE, fileSize.ToString());
            }
            if (type.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_TYPE, type);
            }
            if (medium.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_MEDIUM, medium);
            }
            if (isDefault)
            {
                xUtil.AddAttr(parEl, ATTR_ISDEFAULT, "true");
            }
            if (expression.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_EXPRESSION, expression);
            }
            if (bitrate > 0)
            {
                xUtil.AddAttr(parEl, ATTR_BITRATE, bitrate.ToString());
            }
            if (samplingrate > 0.0)
            {
                xUtil.AddAttr(parEl, ATTR_SAMPLINGRATE, samplingrate.ToString("0.0"));
            }
            if (channels > 0)
            {
                xUtil.AddAttr(parEl, ATTR_CHANNELS, channels);
            }
            if (duration > 0)
            {
                xUtil.AddAttr(parEl, ATTR_DURATION, duration.ToString());
            }
            if (height > 0)
            {
                xUtil.AddAttr(parEl, ATTR_HEIGHT, height);
            }
            if (width > 0)
            {
                xUtil.AddAttr(parEl, ATTR_WIDTH, width);
            }
            if (lang.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_LANG, lang);
            }

            mediaOptions.Parent = this;
            mediaOptions.SetEl(parEl);

            return parEl;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssMediaContent properties with the contents of the Parent XElement
        /// </summary>
        /// <param name="parEl">XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {

            if (parEl.Name.Namespace == RSS.MEDIA_NS || 
                parEl.Name.Namespace == RSS.DUBLIN_CORE_TERMS_NS || 
                parEl.Name.Namespace == RSS.GEORSS_NS || 
                parEl.Name.Namespace== RSS.GML_NS)
            {

                // we are just going to load all the media options...if there are any
                IEnumerable<XElement> lstEl = parEl.Elements();
                foreach(XElement el in lstEl)
                {
                    mediaOptions.LoadEl(el);
                }



                IEnumerable<XAttribute> lstAttr = parEl.Attributes();
                foreach (XAttribute attr in lstAttr)
                {
                    switch (attr.Name.LocalName)
                    {
                        case ATTR_URL:
                            url = xUtil.GetAttrStr(attr);
                            break;
                        case ATTR_FILESIZE:
                            fileSize = xUtil.GetAttrLong(attr);
                            break;
                        case ATTR_TYPE:
                            type = xUtil.GetAttrStr(attr);
                            break;
                        case ATTR_MEDIUM:
                            medium = xUtil.GetAttrStr(attr);
                            break;
                        case ATTR_ISDEFAULT:
                            isDefault = xUtil.GetAttrBool(attr, RssXmlUtilities.BOOL_FORMAT_TRUE_FALSE);
                            break;
                        case ATTR_EXPRESSION:
                            expression = xUtil.GetAttrStr(attr);
                            break;
                        case ATTR_BITRATE:
                            bitrate = xUtil.GetAttrInt(attr);
                            break;
                        case ATTR_FRAMERATE:
                            frameRate = xUtil.GetAttrInt(attr);
                            break;
                        case ATTR_SAMPLINGRATE:
                            samplingrate = xUtil.GetAttrDbl(attr);
                            break;
                        case ATTR_CHANNELS:
                            channels = xUtil.GetAttrInt(attr);
                            break;
                        case ATTR_DURATION:
                            duration = xUtil.GetAttrLong(attr);
                            break;
                        case ATTR_HEIGHT:
                            height = xUtil.GetAttrInt(attr);
                            break;
                        case ATTR_WIDTH:
                            width = xUtil.GetAttrInt(attr);
                            break;
                        case ATTR_LANG:
                            lang = xUtil.GetAttrStr(attr);
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




