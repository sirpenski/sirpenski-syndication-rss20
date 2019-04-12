// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssCore20.cs
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
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using Sirpenski.Syndication.Rss20.Core;
using System.Diagnostics;


namespace Sirpenski.Syndication.Rss20
{
    [Serializable]
    public partial class RssCore20
    {

        public const string TAG_ROOT = "rss";
        public const string RSS_VERSION = "2.0";
        public const string ATTR_VERSION = "version";
        public const string TAG_CHANNEL = "channel";

        /// <summary>
        /// Sml Helper Utilities
        /// </summary>
        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();


        /// <summary>
        /// Current URL
        /// </summary>
        public string url { get; set; } = "";


        /// <summary>
        /// When loading the url, the items can be sorted by pubDate.  Default false
        /// </summary>
        public bool SortItemsOnLoad { get; set; } = false;


        // source xml
        [NonSerialized]
        private string strResponseRawXml = "";


        [NonSerialized]
        private string strResponseXml = "";

        
        /// <summary>
        /// Saves the source xml
        /// </summary>
        public bool PersistResponseXml { get; set; } = false;


        /// <summary>
        /// Raw XML Received From Request. This is the exact xml as received. This property available only if using LoadURL Method
        /// </summary>
        public string ResponseRawXml
        {
            get {

                StringBuilder sb = new StringBuilder(strResponseRawXml);
                StringWriter sw = new RssStringWriterUtf8(sb);
                return sw.ToString();

            }
            set {
                strResponseRawXml = value;
            }
        }


        /// <summary>
        /// XML Received From Request With Processing Instructions Removed.  This property available only if using LoadURL Method
        /// </summary>
        public string ResponseXml
        {
            get {

                StringBuilder sb = new StringBuilder(strResponseXml);
                StringWriter sw = new RssStringWriterUtf8(sb);
                return sw.ToString();

            }
            set {
                strResponseXml = value;
            }
        }


        /// <summary>
        /// Response Status Code.  Only Valid if Load URL
        /// </summary>
        public HttpStatusCode ResponseStatusCode { get; set; } = HttpStatusCode.OK;

        /// <summary>
        /// Response Complete Flag.  Indicates data received and parsed.  Only valid if LoadUrl used
        /// </summary>
        public bool ResponseComplete { get; set; } = false;

        /// <summary>
        /// Maximum times it http client will try and redirect before failing
        /// </summary>
        public int ResponseMaxRedirectCount { get; set; } = 3;


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// RSS Channel Object
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssCoreChannel channel { get; set; } = new RssCoreChannel();


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the Rss Channel as an XElement
        /// </summary>
        /// <returns>XElement (root node)</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = new XElement(TAG_ROOT);
            xUtil.AddAttr(parEl, ATTR_VERSION, RSS_VERSION);

            if (channel != null)
            {
                XElement channelEl = channel.GetEl();
                parEl.Add(channelEl);
            }

            return parEl;

        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the Channel as an XDocument
        /// </summary>
        /// <returns>XDocument</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XDocument GetXd()
        {
            return xUtil.GetXd(GetEl());
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the channel as a UTF-8 encoded XML string
        /// </summary>
        /// <returns></returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public string GetXml()
        {
            return xUtil.GetXml(GetXd());
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the channel with the contents of an XDocument
        /// </summary>
        /// <param name="xd">XDocument</param>
        /// <returns>bool (True-OK, False-Error)</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public bool Load(XDocument xd)
        {
            bool rt = false;

            // try and parse the document
            try
            {
                // get all the sub elements, there should only be one.  
                IEnumerable<XElement> lstEls = xd.Root.Elements();

                foreach (XElement el in lstEls)
                {
                    switch (el.Name.LocalName)
                    {
                        case TAG_CHANNEL:
                            channel = new RssCoreChannel();

                            // load the channel
                            channel.Load(el);
                            break;
                    }
                }


                // if specifed, sort the items
                if (SortItemsOnLoad)
                {
                    if (channel != null)
                    {
                        channel.SortItems();
                    }
                }


                rt = true;
            }
            catch (Exception) { }

            return rt;
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads a channel object with the contents of the Xml String
        /// </summary>
        /// <param name="xml">xml string</param>
        /// <returns>bool (True-OK, False-Error)</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public bool Load(string xml)
        {
            bool rt = false;


            try
            {
                XDocument xd = XDocument.Parse(xml);
                if (Load(xd))
                {
                    rt = true;
                }
            }
            catch (Exception) { }

            return rt;
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads a URL
        /// </summary>
        /// <param name="Url">URL of the Rss Feed</param>
        /// <returns>HttpStatusCode (200-OK, 400-Error)</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public async Task<HttpStatusCode> LoadUrl(string Url)
        {
            // set the response complete flag
            ResponseComplete = false;

            // define an xd
            XDocument xd = null;

            // set the return to error
            HttpStatusCode rt = HttpStatusCode.BadRequest;

            // set to empty string
            ResponseRawXml = "";

            // set the response xml to empty
            ResponseXml = "";

            // set the class level variable to bad
            ResponseStatusCode = HttpStatusCode.BadRequest;


            // if the url was specified
            if (Url.Length > 0)
            {
                // set the url
                url = Url;

                RssHttpClient httpClient = new RssHttpClient();

                // now we iterate and follow redirects
                bool loopControl = true;
                int nRedirectCount = 0;
                while (loopControl && nRedirectCount < ResponseMaxRedirectCount)
                {
                    // do teh load
                    httpClient = new RssHttpClient();
                    httpClient.Url = url;
                    rt = await httpClient.Get();

                    Debug.WriteLine("URL: " + url + ", RSLT: " + (rt.ToString()));

                    if (rt == HttpStatusCode.Moved || rt == HttpStatusCode.MovedPermanently)
                    {
                        url = httpClient.HttpResponse.Headers.Location.ToString();
                        nRedirectCount += 1;
                    }
                    else
                    {
                        loopControl = false;
                    }
                }


                // if the response is OK
                if (rt == HttpStatusCode.OK)
                {

                    // if we should save the source xml, then save it.
                    if (PersistResponseXml)
                    {
                        ResponseRawXml = await httpClient.GetResponseAsString();
                    }

                    // reset the result to bad request
                    rt = HttpStatusCode.BadRequest;

                    // try and process the xml received
                    try
                    {

                        // get the response as an xml reader
                        XmlReader xr = await httpClient.GetResponseAsXmlReader();

                        // load into the xdocument
                        xd = XDocument.Load(xr);

                        // now load the document
                        if (Load(xd))
                        {
                            rt = HttpStatusCode.OK;

                            // if we are saving the source xml, 
                            // we need o
                            if (PersistResponseXml)
                            {
                                // remove all the processing instructions
                                xd.Nodes().Where(x => x.NodeType == XmlNodeType.ProcessingInstruction).Remove();

                                // now set the response xml with no processing instructions
                                ResponseXml = xUtil.GetXml(xd);

                            }


                        }   // end if load xdocument

                    }   // end try

                    catch (Exception) { }


                }   // end if document received ok

            }   // end if url is specified.

            // set the class level status code
            ResponseStatusCode = rt;

            // set the response complete flag
            ResponseComplete = true;

            return rt;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// reads an file 
        /// </summary>
        /// <param name="fn">fileName</param>
        /// <returns>bool</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public bool LoadFile(string fn)
        {
            bool rt = false;

            // get the reader writer object
            RssFileReaderWriter rw = new RssFileReaderWriter();

            // load the file from disk
            string xml = rw.Read(fn);

            if (!string.IsNullOrEmpty(xml))
            {
                rt = Load(xml);
            }
            return rt;

        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Saves a file
        /// </summary>
        /// <param name="fn">file name</param>
        /// <returns>bool</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public bool SaveFile(string fn)
        {
            bool rt = false;

            RssFileReaderWriter rw = new RssFileReaderWriter();

            try
            {
                // get the xml
                string xml = GetXml();

                // write the file
                bool rt1 = rw.Save(xml, fn);

                // assign (ie try and let save complete
                rt = rt1;
            }
            catch (Exception) { }
            return rt;
        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// On Deserialization
        /// </summary>
        /// <param name="context"></param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        [OnDeserialized]
        private void OnDeserialize(StreamingContext context)
        {
            xUtil = new RssXmlUtilities();
        }


    }
}




