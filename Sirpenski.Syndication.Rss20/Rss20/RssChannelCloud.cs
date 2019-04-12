// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssChannelCloud.cs
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
using Sirpenski.Syndication.Rss20.Core;


namespace Sirpenski.Syndication.Rss20
{

    /// <summary>
    /// The cloud class indicates that updates to the feed can be monitored using a web service that implements the 
    /// RssCloud application programming interface (optional).
    /// </summary>
    [Serializable]
    public class RssChannelCloud : RssCoreChannelCloud
    { }

}




