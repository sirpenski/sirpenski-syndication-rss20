// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssXmlUtilities.cs
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace Sirpenski.Syndication.Rss20
{
    class RssXmlUtilities
    {
        public const string BOOL_FORMAT_TF = "TF";
        public const string BOOL_FORMAT_TRUE_FALSE = "TRUE-FALSE";
        public const string BOOL_FORMAT_YN = "YN";
        public const string BOOL_FORMAT_YES_NO = "YES-NO";

        public const string SQL_DATETIME_FORMAT_STRING = "yyyy-MM-ddTHH:mm:ss";
        public const string SQL_TIMESPAN_FORMAT_STRING = "hh\\:mm\\:ss";



        public XElement AddEl(XElement parEl, string elName, string elValue)
        {

            string tmp = elValue;
            if (tmp == null)
            {
                tmp = "";
            }
            tmp = WebUtility.HtmlEncode(tmp);

            XElement newEl = new XElement(elName);
            XText newTxt = new XText(tmp);
            newEl.Add(newTxt);
            parEl.Add(newEl);

            return newEl;
        }

        public XElement AddEl(XElement parEl, string elName, char elValue)
        {
            return AddEl(parEl, elName, Convert.ToString(elValue));
        }

        public XElement AddEl(XElement parEl, string elName, int elValue)
        {
            return AddEl(parEl, elName, Convert.ToString(elValue));
        }

        public XElement AddEl(XElement parEl, string elName, Int16 elValue)
        {
            return AddEl(parEl, elName, Convert.ToString(elValue));
        }

        public XElement AddEl(XElement parEl, string elName, long elValue)
        {
            return AddEl(parEl, elName, Convert.ToString(elValue));
        }

        public XElement AddEl(XElement parEl, string elName, decimal elValue)
        {
            return AddEl(parEl, elName, Convert.ToString(elValue));
        }

        public XElement AddEl(XElement parEl, string elName, double elValue)
        {
            return AddEl(parEl, elName, Convert.ToString(elValue));
        }

        public XElement AddEl(XElement parEl, string elName, DateTime elValue)
        {
            string sDt = elValue.ToString(SQL_DATETIME_FORMAT_STRING);
            return AddEl(parEl, elName, sDt);

        }

        public XElement AddEl(XElement parEl, string elName, TimeSpan elValue)
        {
            string sTm = elValue.ToString(SQL_TIMESPAN_FORMAT_STRING);
            return AddEl(parEl, elName, sTm);
        }

        public XElement AddEl(XElement parEl, string elName, byte elValue)
        {
            return AddEl(parEl, elName, Convert.ToString(elValue));
        }

        public XElement AddEl(XElement parEl, string elName, UInt32 elValue)
        {
            return AddEl(parEl, elName, Convert.ToString(elValue));
        }




        //public void AddEl(XElement parEl, string elName, bool elValue, string fmt = "T-F")
        //{
        //    string pfmt = fmt.ToUpper();
        //    string true_value = "T";
        //    string false_value = "F";
        //    if (string.Compare(fmt, "TRUE-FALSE") == 0)
        //    {
        //        true_value = "TRUE";
        //        false_value = "FALSE";
        //    }


        //    string c = false_value;
        //    if (elValue)
        //    {
        //        c = true_value;
        //    }
        //    AddEl(parEl, elName, c);
        //}


        public XElement AddEl(XElement parEl, string elName, bool elValue, string fmt = BOOL_FORMAT_TF)
        {
            string pfmt = fmt.ToUpper();
            string true_value = GetBooleanTrueString(fmt);
            string false_value = GetBooleanFalseString(fmt);

            string c = false_value;
            if (elValue)
            {
                c = true_value;
            }
            return AddEl(parEl, elName, c);
        }




        public XElement AddEl(XElement parEl, string elName)
        {
            XElement newEl = new XElement(elName);
            parEl.Add(newEl);
            return newEl;
        }



        // adds a cdata element
        public XElement AddCD(XElement parEl, string elName, string elValue)
        {
            XElement newEl = new XElement(elName);

            XCData cdEl = new XCData(elValue);
            newEl.Add(cdEl);
            parEl.Add(newEl);

            return newEl;
        }




        public XElement AddNsEl(XElement parEl, XNamespace ns, string elName, string elValue)
        {

            string tmp = elValue;
            if (tmp == null)
            {
                tmp = "";
            }
            tmp = WebUtility.HtmlEncode(tmp);

            XElement newEl = new XElement(ns + elName);
            XText newTxt = new XText(tmp);
            newEl.Add(newTxt);
            parEl.Add(newEl);

            return newEl;
        }

        public XElement AddNsEl(XElement parEl, XNamespace ns, string elName, char elValue)
        {
            return AddNsEl(parEl, ns, elName, Convert.ToString(elValue));
        }

        public XElement AddNsEl(XElement parEl, XNamespace ns, string elName, int elValue)
        {
            return AddNsEl(parEl, ns, elName, Convert.ToString(elValue));
        }

        public XElement AddNsEl(XElement parEl, XNamespace ns, string elName, Int16 elValue)
        {
            return AddNsEl(parEl, ns, elName, Convert.ToString(elValue));
        }

        public XElement AddNsEl(XElement parEl, XNamespace ns, string elName, long elValue)
        {
            return AddNsEl(parEl, ns, elName, Convert.ToString(elValue));
        }

        public XElement AddNsEl(XElement parEl, XNamespace ns, string elName, decimal elValue)
        {
            return AddNsEl(parEl, ns, elName, Convert.ToString(elValue));
        }

        public XElement AddNsEl(XElement parEl, XNamespace ns, string elName, double elValue)
        {
            return AddNsEl(parEl, ns, elName, Convert.ToString(elValue));
        }

        public XElement AddNsEl(XElement parEl, XNamespace ns, string elName, DateTime elValue)
        {
            string sDt = elValue.ToString(SQL_DATETIME_FORMAT_STRING);
            return AddNsEl(parEl, ns, elName, sDt);

        }

        public XElement AddNsEl(XElement parEl, XNamespace ns, string elName, TimeSpan elValue)
        {
            string sTm = elValue.ToString(SQL_TIMESPAN_FORMAT_STRING);
            return AddNsEl(parEl, ns, elName, sTm);

        }

        public XElement AddNsEl(XElement parEl, XNamespace ns, string elName, byte elValue)
        {
            return AddNsEl(parEl, ns, elName, Convert.ToString(elValue));
        }

        public XElement AddNsEl(XElement parEl, XNamespace ns, string elName, UInt32 elValue)
        {
            return AddNsEl(parEl, ns, elName, Convert.ToString(elValue));
        }




        public XElement AddNsEl(XElement parEl, XNamespace ns, string elName, bool elValue, string fmt = BOOL_FORMAT_TF)
        {

            string pfmt = fmt.ToUpper();
            string true_value = GetBooleanTrueString(pfmt);
            string false_value = GetBooleanFalseString(pfmt);


            string c = false_value;
            if (elValue)
            {
                c = true_value;
            }
            return AddNsEl(parEl, ns, elName, c);
        }

        public XElement AddNsEl(XElement parEl, XNamespace ns, string elName)
        {
            XElement newEl = new XElement(ns + elName);
            parEl.Add(newEl);
            return newEl;
        }




        public XDocument GetXd(XElement recEl)
        {
            XDeclaration xdec = new XDeclaration("1.0", "UTF-8", "yes");
            XDocument xd = new XDocument(xdec);
            xd.Add(recEl);
            return xd;
        }

        public string GetXml(XDocument xd)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new RssStringWriterUtf8(sb);
            xd.Save(sw);
            return sw.ToString();
        }



        public XText GetTextEl(XElement el)
        {
            XText rt = null;
            IEnumerable<XNode> lst = el.Nodes();

            string value_string = "";

            foreach (XNode nd in lst)
            {
                if (nd.NodeType == XmlNodeType.Text)
                {
                    value_string += (string)((XText)nd).Value;
                }
                else if (nd.NodeType == XmlNodeType.CDATA)
                {
                    XCData xcd = (XCData)nd;

                    value_string += xcd.Value;
                }
            }

            rt = new XText(value_string);

            return rt;
        }



        // ---------------------------------------------------
        // Gets the cdata element
        // ---------------------------------------------------
        public XCData GetCDataEl(XElement el)
        {
            XCData rt = null;
            IEnumerable<XNode> lst = el.Nodes();
            foreach (XNode nd in lst)
            {
                if (nd.NodeType == XmlNodeType.CDATA)
                {
                    rt = (XCData)nd;
                }
            }
            return rt;
        }



        public char GetCh(XElement el, char def = '\0')
        {
            char rt = def;

            XText txt = GetTextEl(el);
            if (txt != null)
            {
                try
                {
                    rt = Convert.ToChar(txt.Value);
                }
                catch (Exception) { }
            }
            return rt;
        }


        public byte GetByte(XElement el, byte def = 0)
        {
            byte rt = def;

            XText txt = GetTextEl(el);
            if (txt != null)
            {
                try
                {
                    rt = Convert.ToByte(txt.Value);
                }
                catch (Exception) { }
            }
            return rt;
        }


        public string GetStr(XElement el, string def = "")
        {
            string rt = def;

            XText txt = GetTextEl(el);
            if (txt != null)
            {
                try
                {
                    string tmp1 = Convert.ToString(txt.Value);
                    rt = WebUtility.HtmlDecode(tmp1);
                }
                catch (Exception) { }
            }
            return rt;
        }


        public int GetInt(XElement el, int def = 0)
        {
            int rt = def;

            XText txt = GetTextEl(el);
            if (txt != null)
            {
                try
                {
                    rt = Convert.ToInt32(txt.Value);
                }
                catch (Exception) { }
            }
            return rt;
        }

        public UInt32 GetUInt32(XElement el, UInt32 def = 0)
        {
            UInt32 rt = def;

            XText txt = GetTextEl(el);
            if (txt != null)
            {
                try
                {
                    rt = Convert.ToUInt32(txt.Value);
                }
                catch (Exception) { }
            }
            return rt;
        }


        public Int16 GetInt16(XElement el, Int16 def = 0)
        {
            Int16 rt = def;

            XText txt = GetTextEl(el);
            if (txt != null)
            {
                try
                {
                    rt = Convert.ToInt16(txt.Value);
                }
                catch (Exception) { }
            }
            return rt;
        }


        public long GetLong(XElement el, long def = 0)
        {
            long rt = def;

            XText txt = GetTextEl(el);
            if (txt != null)
            {
                try
                {
                    rt = Convert.ToInt64(txt.Value);
                }
                catch (Exception) { }
            }
            return rt;
        }



        public decimal GetDec(XElement el, decimal def = 0)
        {
            decimal rt = def;

            XText txt = GetTextEl(el);
            if (txt != null)
            {
                try
                {
                    rt = Convert.ToDecimal(txt.Value);
                }
                catch (Exception) { }
            }
            return rt;
        }



        public float GetFloat(XElement el, float def = 0)
        {
            float rt = def;

            XText txt = GetTextEl(el);
            if (txt != null)
            {
                try
                {
                    rt = Convert.ToSingle(txt.Value);
                }
                catch (Exception) { }
            }
            return rt;
        }



        public double GetDbl(XElement el, double def = 0)
        {
            double rt = def;

            XText txt = GetTextEl(el);
            if (txt != null)
            {
                try
                {
                    rt = Convert.ToDouble(txt.Value);
                }
                catch (Exception) { }
            }
            return rt;
        }


        public DateTime GetDt(XElement el)
        {
            DateTime rt = DateTime.MinValue;
            DateTime dtParseResult;


            XText txt = GetTextEl(el);
            if (txt != null)
            {
                try
                {
                    if (DateTime.TryParse(txt.Value, null, System.Globalization.DateTimeStyles.None, out dtParseResult))
                    {
                        rt = dtParseResult;
                    }
                }
                catch (Exception) { }
            }
            return rt;
        }






        public TimeSpan GetTm(XElement el)
        {
            TimeSpan rt = TimeSpan.Zero;
            TimeSpan tsParseResult;


            XText txt = GetTextEl(el);
            if (txt != null)
            {
                try
                {
                    if (TimeSpan.TryParse(txt.Value, out tsParseResult))
                    {
                        rt = tsParseResult;
                    }
                }
                catch (Exception) { }
            }

            return rt;
        }


        public bool GetBool(XElement el, string fmt = BOOL_FORMAT_TF)
        {
            bool rt = false;
            string pfmt = fmt.ToUpper();

            string true_value = GetBooleanTrueString(fmt);
            string false_value = GetBooleanFalseString(fmt);

            XText txt = GetTextEl(el);
            if (txt != null)
            {
                string txt1 = Convert.ToString(txt.Value).ToUpper();

                if (string.Compare(txt1, true_value) == 0)
                {
                    rt = true;
                }
            }
            return rt;
        }

        public XAttribute AddAttr(XElement el, string key, string val)
        {
            XAttribute xAttr = new XAttribute(key, val);
            el.Add(xAttr);
            return xAttr;
        }

        public XAttribute AddAttr(XElement el, string key, int val)
        {
            XAttribute xAttr = new XAttribute(key, val.ToString());
            el.Add(xAttr);
            return xAttr;
        }


        public XAttribute AddAttr(XElement parEl, XAttribute attr)
        {
            parEl.Add(attr);
            return attr;
        }


        public string GetAttrStr(XAttribute attr)
        {
            string rt = "";

            try
            {
                rt = attr.Value.ToString();
            }
            catch (Exception) { }
            return rt;
        }


        public int GetAttrInt(XAttribute attr)
        {
            int rt = 0;
            try
            {
                if (int.TryParse(attr.Value, out int tmp))
                {
                    rt = tmp;
                }
            }
            catch (Exception) { }
            return rt;
        }




        public long GetAttrLong(XAttribute attr)
        {
            long rt = 0;
            try
            {
                if (long.TryParse(attr.Value, out long tmp))
                {
                    rt = tmp;
                }
            }
            catch (Exception) { }
            return rt;
        }

        public double GetAttrDbl(XAttribute attr)
        {
            double rt = 0.0;
            try
            {
                if (double.TryParse(attr.Value, out double tmp))
                {
                    rt = tmp;
                }

            }
            catch (Exception) { }
            return rt;
        }

        public decimal GetAttrDec(XAttribute attr)
        {
            decimal rt = 0.0M;
            try
            {
                if (decimal.TryParse(attr.Value, out decimal tmp))
                {
                    rt = tmp;
                }

            }
            catch (Exception) { }
            return rt;
        }


        public DateTime GetAttrDt(XAttribute attr)
        {
            DateTime rt = DateTime.MinValue;
            try
            {
                if (DateTime.TryParse(attr.Value, out DateTime dt))
                {
                    rt = dt;
                }
            }
            catch (Exception) { }
            return rt;
        }

        public bool GetAttrBool(XAttribute attr, string fmt = BOOL_FORMAT_TF)
        {
            string pfmt = fmt.ToUpper();
            string true_value = GetBooleanTrueString(pfmt);
            string false_value = GetBooleanFalseString(pfmt);

            bool rt = false;

            string tmp = GetAttrStr(attr);
            if (string.Compare(tmp, true_value) == 0)
            {
                rt = true;
            }
            return rt;

        }


        public TimeSpan GetAttrTimeSpan(XAttribute attr)
        {
            TimeSpan rt = TimeSpan.MinValue;

            string s = GetAttrStr(attr);
            if (TimeSpan.TryParse(s, out TimeSpan tmp))
            {
                rt = tmp;
            }

            return rt;

        }


        // -----------------------------------------------------------------
        // Locates a child element of the parent element
        // -----------------------------------------------------------------

        public XElement LocateChild(XElement parEl, string tagName)
        {

            XElement rt = null;

            try
            {
                XElement el = parEl.Descendants(tagName).FirstOrDefault();
                if (el != null)
                {
                    rt = el;
                }

            }
            catch (Exception) { }

            return rt;
        }


        // --------------------------------------------------------------
        // Creates a new XElement
        // --------------------------------------------------------------
        public XElement CreateEl(string tagName)
        {
            return new XElement(tagName);
        }

        public XElement CreateEl(string tagName, string v)
        {
            return new XElement(tagName, v);
        }


        public XElement CreateNSEl(string tagName, XNamespace ns)
        {
            return new XElement(ns + tagName);
        }

        public XElement CreateNSEl(string tagName, string v, XNamespace ns)
        {
            return new XElement(ns + tagName, v);
        }



        // -------------------------------------------------
        // Determines the true string
        // -------------------------------------------------

        private string GetBooleanTrueString(string fmt = BOOL_FORMAT_TRUE_FALSE)
        {
            string rt = "";
            switch (fmt)
            {
                case BOOL_FORMAT_TF:
                    rt = "T";
                    break;
                case BOOL_FORMAT_TRUE_FALSE:
                    rt = "TRUE";
                    break;
                case BOOL_FORMAT_YES_NO:
                    rt = "YES";
                    break;
                case BOOL_FORMAT_YN:
                    rt = "Y";
                    break;
                default:
                    rt = "TRUE";
                    break;
            }
            return rt;
        }


        // -------------------------------------------------
        // Determines the true string
        // -------------------------------------------------

        private string GetBooleanFalseString(string fmt = BOOL_FORMAT_TRUE_FALSE)
        {
            string rt = "";
            switch (fmt)
            {
                case BOOL_FORMAT_TF:
                    rt = "F";
                    break;
                case BOOL_FORMAT_TRUE_FALSE:
                    rt = "FALSE";
                    break;
                case BOOL_FORMAT_YES_NO:
                    rt = "NO";
                    break;
                case BOOL_FORMAT_YN:
                    rt = "N";
                    break;
                default:
                    rt = "FALSE";
                    break;
            }
            return rt;
        }


    }
}



