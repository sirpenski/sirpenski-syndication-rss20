// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaLocation.cs
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
    /// Optional element to specify geographical information about various locations captured in the content of a media object.
    /// </summary>
    [Serializable]
    public class RssMediaLocation
    {

        public const string TAG_PARENT = "location";
        public const string ATTR_DESCRIPTION = "description";
        public const string ATTR_START = "start";
        public const string ATTR_END = "end";
        public const string TAG_WHERE = "where";
        public const string TAG_POINT = "Point";
        public const string TAG_POS = "pos";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// location description
        /// </summary>
        public string description { get; set; } = "";

        /// <summary>
        /// Start time location applies to
        /// </summary>
        public TimeSpan start { get; set; } = TimeSpan.MinValue;

        /// <summary>
        /// end time location applies to.
        /// </summary>
        public TimeSpan end { get; set; } = TimeSpan.MinValue;

        ///<summary>
        ///latitude of location
        ///</summary>
        public double latitude { get; set; } = 0.0;

        /// <summary>
        /// longitude of location
        /// </summary>
        public double longitude { get; set; } = 0.0;

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Default Constructor
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaLocation() { }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets RssMediaLocation object properties as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = xUtil.CreateNSEl(TAG_PARENT, RSS.MEDIA_NS);

            if (description.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_DESCRIPTION, description);
            }

            if (start > TimeSpan.MinValue)
            {
                xUtil.AddAttr(parEl, ATTR_START, start.ToString("hh\\:mm\\:ss"));
            }


            if (end > TimeSpan.MinValue)
            {
                xUtil.AddAttr(parEl, ATTR_END, end.ToString("hh\\:mm\\:ss"));
            }


            XElement whereEl = xUtil.AddNsEl(parEl, RSS.GEORSS_NS, TAG_WHERE);

            XElement pointEl = xUtil.AddNsEl(whereEl, RSS.GML_NS, TAG_POINT);
            
            string latlong = latitude.ToString("0.0000") + " " + longitude.ToString("0.0000");

            XElement posEl = xUtil.AddNsEl(pointEl, RSS.GML_NS, TAG_POS, latlong);

            return parEl;

        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads RssMediaLocation object properties with contents of parent XElement
        /// </summary>
        /// <param name="parEl">Parent XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {
            IEnumerable<XAttribute> lstAttr = parEl.Attributes();
            foreach(XAttribute attr in lstAttr)
            {
                switch(attr.Name.LocalName)
                {
                    case ATTR_DESCRIPTION:
                        description = xUtil.GetAttrStr(attr);
                        break;
                    case ATTR_START:
                        start = xUtil.GetAttrTimeSpan(attr);
                        break;
                    case ATTR_END:
                        end = xUtil.GetAttrTimeSpan(attr);
                        break;
                }
            }

            // get all teh subelements to location.  should be 1, where
            IEnumerable<XElement> LocationChildren = parEl.Elements();
            foreach(XElement elLocationChild in LocationChildren)
            {
                // only check the elements that are in the georss namespace
                if (elLocationChild.Name.Namespace == RSS.GEORSS_NS)
                {

                    switch(elLocationChild.Name.LocalName)
                    {
                        case TAG_WHERE:

                            IEnumerable<XElement> WhereChildren = elLocationChild.Elements();
                            foreach(XElement WhereChild in WhereChildren)
                            {
                                // if the namespace of the where child is gml, then we should procced
                                if (WhereChild.Name.Namespace == RSS.GML_NS)
                                {

                                    switch(WhereChild.Name.LocalName)
                                    {

                                        case TAG_POINT:

                                            IEnumerable<XElement> PointChildren = WhereChild.Elements();
                                            foreach(XElement PointChild in PointChildren)
                                            {
                                                // if the namespace of point child is GMLNs, then 
                                                // we proceed as we are looking for gml:pos
                                                if (PointChild.Name.Namespace == RSS.GML_NS)
                                                {
                                                    switch(PointChild.Name.LocalName)
                                                    {

                                                        case TAG_POS:
                                                            
                                                            // get the tag value.  it is a string with 
                                                            // a latitude and longitude separated by a 
                                                            // space.
                                                            string coordinates = xUtil.GetStr(PointChild);
                                                            coordinates = coordinates.Trim();

                                                            if (coordinates.Length > 0)
                                                            {

                                                                string[] a = coordinates.Split(' ');

                                                                // now iterate the array taking the first two 
                                                                // non blank values as the data
                                                                string[] latlong = new string[2];
                                                                int ndx = 0;
                                                                for (int j = 0; j < a.Length; j++)
                                                                {
                                                                    string tmp = a[j].Trim();
                                                                    if (tmp.Length > 0 && ndx <= 1)
                                                                    {
                                                                        latlong[ndx] = tmp;
                                                                        ndx += 1;
                                                                    }
                                                                }
                                                                if (double.TryParse(latlong[0], out double tmplat))
                                                                {
                                                                    latitude = tmplat;
                                                                }
                                                                if (double.TryParse(latlong[1], out double tmplong))
                                                                {
                                                                    longitude = tmplong;
                                                                }

                                                            }   // end if position

                                                            break;

                                                    }   // end switch

                                                }   // end if point child in gml namespace

                                            }   // end foreach point child element


                                            break;

                                    }   // end switch "where"



                                }   // end if where child element is in georss namespace


                            }   // end foreach child of where element


                            break;

                    } // end switch

                } // end if location child is in georss namespace

            }  // end foreach child of georss:where element

        }   // end load



        [OnDeserializing]
        private void OnDeserialize(StreamingContext context)
        {
            xUtil = new RssXmlUtilities();
        }


    }
}




