// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssMediaPrice.cs
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
    /// The RssMediaPrice object contains pricing information about the media item
    /// </summary>
    [Serializable]
    public class RssMediaPrice
    {
        public const string TAG_PARENT = "price";
        public const string ATTR_TYPE = "type";
        public const string ATTR_INFO = "info";
        public const string ATTR_PRICE = "price";
        public const string ATTR_CURRENCY = "currency";

        [NonSerialized]
        private RssXmlUtilities xUtil = new RssXmlUtilities();

        /// <summary>
        /// The type of pricing
        /// </summary>
        public string type { get; set; } = "";

        /// <summary>
        /// url or description describing price
        /// </summary>
        public string info { get; set; } = "";

        /// <summary>
        /// the price of the media item
        /// </summary>
        public decimal price { get; set; } = -1.0M;

        /// <summary>
        ///  unit of currency
        /// </summary>
        public string currency { get; set; } = "";


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Default Constructor
        /// </summary>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssMediaPrice() { }



        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the RssMediaPrice object properties as an XElement
        /// </summary>
        /// <returns>XElement</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public XElement GetEl()
        {
            XElement parEl = xUtil.CreateNSEl(TAG_PARENT, RSS.MEDIA_NS);

            if (type.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_TYPE, type);
            }

            if (info.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_INFO, info);
            }

            if (price >= 0M)
            {
                xUtil.AddAttr(parEl, ATTR_PRICE, price.ToString("0.00"));
            }

            if (currency.Length > 0)
            {
                xUtil.AddAttr(parEl, ATTR_CURRENCY, currency);
            }

            return parEl;
        }


        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Loads the RssMediaPrice object properties with the contents of the parent XElement
        /// </summary>
        /// <param name="parEl">Parent XElement</param>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public void Load(XElement parEl)
        {
            if (parEl.Name.Namespace == RSS.MEDIA_NS)
            {

                IEnumerable<XAttribute> lstAttr = parEl.Attributes();
                foreach (XAttribute attr in lstAttr)
                {
                    switch (attr.Name.LocalName)
                    {
                        case ATTR_TYPE:
                            type = xUtil.GetAttrStr(attr);
                            break;

                        case ATTR_INFO:
                            info = xUtil.GetAttrStr(attr);
                            break;

                        case ATTR_PRICE:
                            price = xUtil.GetAttrDec(attr);
                            break;

                        case ATTR_CURRENCY:
                            currency = xUtil.GetAttrStr(attr);
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




