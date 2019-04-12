// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaGroup_Extension.cs
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


    public partial class RssMediaGroup
    {

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Adds a media content item to the media content group
        /// </summary>
        /// <param name="contentItem">RSSMediaContent object</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void AddMediaContentItem(RssMediaContent contentItem)
        {
            contentItem.Parent = this;
            mediaContentItems.Add(contentItem);

        }

    }
}




