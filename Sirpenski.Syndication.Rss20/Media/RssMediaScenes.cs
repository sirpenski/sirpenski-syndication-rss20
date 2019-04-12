// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaScenes.cs
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
    /// The RssMediaScenes object is a collection of media scenes
    /// </summary>
    [Serializable]
    public class RssMediaScenes
    {
        public const string TAG_PARENT = "scenes";
        public const string TAG_SCENE = "scene";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// A collection of media scenes
        /// </summary>
        public List<RssMediaScene> scenes = new List<RssMediaScene>();


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Default Constructor
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaScenes() { }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the RssMediaScenes object properties as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = xUtil.CreateNSEl(TAG_PARENT, RSS.MEDIA_NS);

            for (int i = 0; i < scenes.Count; i++)
            {
                XElement recEl = scenes[i].GetEl();
                parEl.Add(recEl);
            }
            return parEl;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssMediaScenes object properties with the contents of the parent XElement
        /// </summary>
        /// <param name="parEl">Parent XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {

            if (parEl.Name.Namespace == RSS.MEDIA_NS)
            {

                IEnumerable<XElement> lst = parEl.Elements();
                foreach (XElement el in lst)
                {

                    switch (el.Name.LocalName)
                    {
                        case TAG_SCENE:
                            RssMediaScene sc = new RssMediaScene();
                            sc.Load(el);
                            scenes.Add(sc);
                            break;
                    }

                }

            }

        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a RssMediaScene item to the scenes collection
        /// </summary>
        /// <param name="scene">RssMediaScene object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Add(RssMediaScene scene)
        {
            scenes.Add(scene);
        }


        [OnDeserializing]
        private void OnDeserialize(StreamingContext context)
        {
            xUtil = new RssXmlUtilities();
        }


    }
}




