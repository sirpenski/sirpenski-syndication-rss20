// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   Rss20.cs
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
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;



namespace Sirpenski.Syndication.Rss20
{
    /// <summary>
    /// The Rss20 is the 
    /// </summary>
    [Serializable]
    public partial class Rss20
    {

        // tags, attributes
        public const string TAG_ROOT = "rss";
        public const string RSS_VERSION = "2.0";
        public const string ATTR_VERSION = "version";
        public const string TAG_CHANNEL = "channel";


 


        // helper class
        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        // source xml
        [NonSerialized]
        private string strResponseRawXml = "";


        [NonSerialized]
        private string strResponseXml = "";

        /// <summary>
        /// Maximum times it http client will try and redirect before failing
        /// </summary>
        public int ResponseMaxRedirectCount { get; set; } = 3;


        /// <summary>
        /// Url Of Rss Feed
        /// </summary>
        public string url { get; set; } = "";

        /// <summary>
        /// When loading the url, the items can be sorted by pubDate.  Default false
        /// </summary>
        public bool SortItemsOnLoad { get; set; } = false;

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
        /// Namespace attributes.  These contain the attribute definitions
        /// </summary>
        public List<RssNamespaceAttribute> NamespaceAttributes { get; set; } = new List<RssNamespaceAttribute>();


        /// <summary>
        /// Response Status Code.  Only Valid if Load URL
        /// </summary>
        public HttpStatusCode ResponseStatusCode { get; set; } = HttpStatusCode.OK;

        /// <summary>
        /// Response Complete Flag.  Indicates data received and parsed.  Only valid if LoadUrl used
        /// </summary>
        public bool ResponseComplete { get; set; } = false;

        

        /// <summary>
        /// Channel object
        /// </summary>
        public RssChannel channel { get; set; } = null;

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Default Constructor
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public Rss20()
        {

            // initialize the static namespaces 
            if (!RSS.NamespacesInitialized)
            {
                RSS.InitializeStaticNamespaces();
            }


            // define the default namespace attributes.  We put these in a collection so that the prefixes can be modified 
            // just in case there is a conflict, etc.
            NamespaceAttributes.Add(new RssNamespaceAttribute(RSS.MEDIA_NAMESPACE_PREFIX, RSS.MEDIA_NAMESPACE_URL));
            NamespaceAttributes.Add(new RssNamespaceAttribute(RSS.DUBLIN_CORE_NAMESPACE_PREFIX, RSS.DUBLIN_CORE_NAMESPACE_URL));
            NamespaceAttributes.Add(new RssNamespaceAttribute(RSS.DUBLIN_CORE_TERMS_NAMESPACE_PREFIX, RSS.DUBLIN_CORE_TERMS_NAMESPACE_URL));
            NamespaceAttributes.Add(new RssNamespaceAttribute(RSS.GEORSS_NAMESPACE_PREFIX, RSS.GEORSS_NAMESPACE_URL));
            NamespaceAttributes.Add(new RssNamespaceAttribute(RSS.OPEN_GIS_GML_NAMESPACE_PREFIX, RSS.OPEN_GIS_GML_NAMESPACE_URL));
            NamespaceAttributes.Add(new RssNamespaceAttribute(RSS.CONTENT_NAMESPACE_PREFIX, RSS.CONTENT_NAMESPACE_URL));
            NamespaceAttributes.Add(new RssNamespaceAttribute(RSS.ATOM_NAMESPACE_PREFIX, RSS.ATOM_NAMESPACE_URL));
            NamespaceAttributes.Add(new RssNamespaceAttribute(RSS.SLASH_NAMESPACE_PREFIX, RSS.SLASH_NAMESPACE_URL));
            NamespaceAttributes.Add(new RssNamespaceAttribute(RSS.CREATIVE_COMMONS_NAMESPACE_PREFIX, RSS.CREATIVE_COMMONS_NAMESPACE_URL));


            // create a new channel
            channel = new RssChannel();

        }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the channel as an XElement (root element)
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {

           
            // create root element rss.  Note, it is NOT in a namespace.
            XElement parEl = xUtil.CreateEl(TAG_ROOT);



            // add the version attribute
            xUtil.AddAttr(parEl, ATTR_VERSION, RSS_VERSION);

            // add the default namespace attributes
            for (int i = 0; i < NamespaceAttributes.Count; i++)
            {
                // now add the media namespace attributes
                XAttribute attr = new XAttribute(XNamespace.Xmlns + NamespaceAttributes[i].Prefix, NamespaceAttributes[i].Url);
                xUtil.AddAttr(parEl, attr);
            }




            // get the document.
            XElement channelEl = channel.GetEl();
            parEl.Add(channelEl);

    
            // return
            return parEl;

        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the channel as an XDocument
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
        /// Gets the channel as a UTF-8 encoded XML String
        /// </summary>
        /// <returns>string (UTF-8)</returns>
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
                // get all the elements to root
                IEnumerable<XElement> lstEls = xd.Root.Elements();

                // for each element, note, there should only be 1, channel
                foreach (XElement el in lstEls)
                {
                    // process current element
                    switch (el.Name.LocalName)
                    {
                        case TAG_CHANNEL:
                            // the null parameter is the parent ref.  there is no parent to the channel effectively
                            channel = new RssChannel();

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
            // set the response complete flag to false
            ResponseComplete = false;

            // define an xd
            XDocument xd = null;

            // set the return to error
            HttpStatusCode rt = HttpStatusCode.BadRequest;

            // set to empty string
            ResponseRawXml = "";

            // set the response xml to empty
            ResponseXml = "";


            // Set the class level Response Status Code.  We have a local response and 
            // a class level response.  Because we manipulate te local var response, we 
            // want to keep the state of the class level response consistent.
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
                while(loopControl && nRedirectCount < ResponseMaxRedirectCount)
                {
                    // get url
                    httpClient = new RssHttpClient();                   
                    httpClient.Url = url;
                    rt = await httpClient.Get();

                    // if we get a redirect, then handle it
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
                            // load was good, set the OK Status
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


            // set the response complete flag to true
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
        /// Modifies and existing namespace prefix.  Must be done before generating feed
        /// </summary>
        /// <param name="url"></param>
        /// <param name="new_prefix"></param>
        /// <returns>bool</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public bool ModifyNamespacePrefix(string url, string new_prefix)
        {
            bool rt = false;
            int ndx = NamespaceAttributes.FindIndex(x => x.Url == url);
            if (ndx >= 0)
            {
                NamespaceAttributes[ndx].Prefix = new_prefix;
                rt = true;
            }
            return rt;
        }






        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Deserialization
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




