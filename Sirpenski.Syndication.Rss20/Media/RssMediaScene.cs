// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaScene.cs
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
    /// The RssMediaScene object contains information about a particular scene
    /// </summary>
    [Serializable]
    public class RssMediaScene
    {

        public const string TAG_PARENT = "scene";
        public const string TAG_SCENE_TITLE = "sceneTitle";
        public const string TAG_SCENE_DESCRIPTION = "sceneDescription";
        public const string TAG_SCENE_STARTTIME = "sceneStartTime";
        public const string TAG_SCENE_ENDTIME = "sceneEndTime";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// Scene title
        /// </summary>
        public string sceneTitle { get; set; } = "";

        /// <summary>
        /// Scene description
        /// </summary>
        public string sceneDescription { get; set; } = "";

        /// <summary>
        /// Scene start time
        /// </summary>
        public TimeSpan sceneStartTime { get; set; } = TimeSpan.MinValue;

        /// <summary>
        /// Scene end time
        /// </summary>
        public TimeSpan sceneEndTime { get; set; } = TimeSpan.MinValue;


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Default Constructor
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaScene() { }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the RssMediaScene object properties as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = xUtil.CreateNSEl(TAG_PARENT, RSS.MEDIA_NS);

            if (sceneTitle.Length > 0)
            {
                xUtil.AddNsEl(parEl, RSS.MEDIA_NS, TAG_SCENE_TITLE, sceneTitle);
            }
            if (sceneDescription.Length > 0)
            {
                xUtil.AddNsEl(parEl, RSS.MEDIA_NS, TAG_SCENE_DESCRIPTION, sceneDescription);
            }
            if (sceneStartTime > TimeSpan.MinValue)
            {
                xUtil.AddNsEl(parEl, RSS.MEDIA_NS, TAG_SCENE_STARTTIME, sceneStartTime);
            }

            if (sceneEndTime > TimeSpan.MinValue)
            {
                xUtil.AddNsEl(parEl, RSS.MEDIA_NS, TAG_SCENE_ENDTIME, sceneEndTime);
            }

            return parEl;


        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssMediaScene object properties with the contents of the parent XElement
        /// </summary>
        /// <param name="parEl">Parent XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {
            if (parEl.Name.Namespace == RSS.MEDIA_NS)
            {
                IEnumerable<XElement> lst = parEl.Elements();
                foreach(XElement el in lst)
                {
                    switch(el.Name.LocalName)
                    {
                        case TAG_SCENE_TITLE:
                            sceneTitle = xUtil.GetStr(el);
                            break;
                        case TAG_SCENE_DESCRIPTION:
                            sceneDescription = xUtil.GetStr(el);
                            break;
                        case TAG_SCENE_STARTTIME:
                            sceneStartTime = xUtil.GetTm(el);
                            break;
                        case TAG_SCENE_ENDTIME:
                            sceneEndTime = xUtil.GetTm(el);
                            break;
                    }
                }
            }
        }



        [OnDeserializing]
        private void OnDeserialize(StreamingContext context)
        {
            xUtil = new RssXmlUtilities();
        }


    }
}

