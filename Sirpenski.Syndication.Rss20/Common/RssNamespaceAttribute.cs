// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssNamespaceAttribute.cs
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
    public class RssNamespaceAttribute
    {
        public string Prefix { get; set; } = "";
        public string Url { get; set; } = "";

        public RssNamespaceAttribute() { }

        public RssNamespaceAttribute(string prefix, string url)
        {
            Prefix = prefix;
            Url = url;
        }

    }

}
