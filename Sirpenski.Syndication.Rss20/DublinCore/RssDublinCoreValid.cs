// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssDublinCoreValid.cs
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
using System.Runtime.Serialization;


namespace Sirpenski.Syndication.Rss20
{

    /// <summary>
    /// This class defines the Dublin Core Valid tag which determines the period of time the item is valid
    /// </summary>
    [Serializable]
    public class RssDublinCoreValid
    {
        public const string TAG_PARENT = "valid";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

 
        /// <summary>
        /// The instant corresponding to the commencement of the time interval
        /// </summary>
        public DateTime start { get; set; } = DateTime.MinValue;

        /// <summary>
        /// The instant corresponding to the termination of the time interval
        /// </summary>
        public DateTime end { get; set; } = DateTime.MinValue;

        /// <summary>
        /// The encoding used for the representation of the time-instants in the start and end component
        /// </summary>
        public string scheme { get; set; } = "";


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Default Constructor
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssDublinCoreValid() { }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the RssDublinCoreValid properties as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            const string DFMT = "yyyy-MM-ddTHH:mm:sszzz";

            // build string
            string tmp = "";
            string sep = "";
            
            if (start > DateTime.MinValue)
            {            
                tmp += sep + "start=" + start.ToString(DFMT);
                sep = ";";
            }

            if (end > DateTime.MinValue)
            {
                tmp += sep + "end=" + end.ToString(DFMT);
                sep = ";";
            }

            if (scheme.Length > 0)
            {
                tmp += sep + "scheme=" + scheme;
                sep = ";";
            }


            XElement parEl = xUtil.CreateNSEl(TAG_PARENT, tmp, RSS.DUBLIN_CORE_TERMS_NS);

            return parEl;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssDublinCoreValid properties from an XElement
        /// </summary>
        /// <param name="parEl">XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {
            if (parEl.Name.Namespace == RSS.DUBLIN_CORE_TERMS_NS)
            {

                // read the value
                string tmp = xUtil.GetStr(parEl);

                // split by semi colon
                string[] a = tmp.Split(';');
                
                // trim it all
                for (int i = 0; i < a.Length; i++)
                {
                    a[i] = a[i].Trim();
                    KeyValuePair<string, string> kvp = Parse(a[i]);

                    switch(kvp.Key)
                    {
                        case "start":
                            if (DateTime.TryParse(kvp.Value, out DateTime tmp1))
                            {
                                start = tmp1;
                            }
                            break;

                        case "end":
                            if (DateTime.TryParse(kvp.Value, out DateTime tmp2))
                            {
                                end = tmp2;
                            }
                            break;

                        case "scheme":
                            scheme = kvp.Value;
                            break;
                    }


                }


            }
        }




        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Key Value Pair Object
        /// </summary>
        /// <param name="s">Key Value Pair to parse</param>
        /// <returns></returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        private KeyValuePair<string,string> Parse(string s)
        {
            KeyValuePair<string, string> rt = new KeyValuePair<string, string>();

            // split by the equal sign
            string[] a = s.Split('=');

            if (a.Length == 2)
            {
                // now rebuild the key value pair with the key being token 0 and the 
                // value being token 1
                rt = new KeyValuePair<string, string>(a[0].Trim(), a[1].Trim());
            }
            return rt;
        }


        [OnDeserializing]
        private void OnDeserialize(StreamingContext context)
        {
            xUtil = new RssXmlUtilities();
        }

    }
}


