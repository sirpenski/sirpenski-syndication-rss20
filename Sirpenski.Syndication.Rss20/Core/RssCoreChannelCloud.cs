// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssCoreChannelCloud.cs
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
using System.Diagnostics;


namespace Sirpenski.Syndication.Rss20.Core
{

    /// <summary>
    /// The cloud class indicates that updates to the feed can be monitored using a web service that implements the 
    /// RssCloud application programming interface (optional).
    /// </summary>
    [Serializable]
    public class RssCoreChannelCloud
    {
        public const string TAG_PARENT = "cloud";
        public const string ATTR_DOMAIN = "domain";
        public const string ATTR_PORT = "port";
        public const string ATTR_PATH = "path";
        public const string ATTR_REGISTERPROCEDURE = "registerProcedure";
        public const string ATTR_PROTOCOL = "protocol";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// The domain attribute identifies the host name or IP address of the web service that monitors updates to the feed.
        /// </summary>
        public string domain { get; set; } = "";

        /// <summary>
        /// The port attribute identifies the web service's TCP port.
        /// </summary>
        public int port { get; set; } = 0;

        /// <summary>
        /// The path attribute provides the web service's path.
        /// </summary>
        public string path { get; set; } = "";

        /// <summary>
        /// The registerProcedure attribute names the remote procedure to call when requesting notification of updates
        /// </summary>
        public string registerProcedure { get; set; } = "";

        /// <summary>
        /// The protocol attribute must contain the value "xml-rpc" if the service employs XML-RPC or "soap" if it employs SOAP.
        /// </summary>
        public string protocol { get; set; } = "";


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the RssCoreChannelCloud class properties as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = new XElement(TAG_PARENT);

            xUtil.AddAttr(parEl, ATTR_DOMAIN, domain);
            xUtil.AddAttr(parEl, ATTR_PORT, port);
            xUtil.AddAttr(parEl, ATTR_PATH, path);
            xUtil.AddAttr(parEl, ATTR_REGISTERPROCEDURE, registerProcedure);
            xUtil.AddAttr(parEl, ATTR_PROTOCOL, protocol);

            return parEl;

        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssCoreChannelCloud class properties from an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {

            // get the blank namespace
            if (parEl.Name.Namespace == XNamespace.None)
            {


                IEnumerable<XAttribute> lstAttr = parEl.Attributes();
                foreach (XAttribute attr in lstAttr)
                {
                    switch (attr.Name.LocalName)
                    {
                        case ATTR_DOMAIN:
                            domain = xUtil.GetAttrStr(attr);
                            break;
                        case ATTR_PORT:
                            port = xUtil.GetAttrInt(attr);
                            break;
                        case ATTR_PATH:
                            path = xUtil.GetAttrStr(attr);
                            break;
                        case ATTR_REGISTERPROCEDURE:
                            registerProcedure = xUtil.GetAttrStr(attr);
                            break;
                        case ATTR_PROTOCOL:
                            protocol = xUtil.GetAttrStr(attr);
                            break;
                    }
                }

            }

        }


    }
}




