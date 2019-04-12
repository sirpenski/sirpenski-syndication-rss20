// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssHttpClient.cs
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
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml;
using System.Net.Mime;
using System.Diagnostics;

namespace Sirpenski.Syndication.Rss20
{
    class RssHttpClient: IDisposable
    {
        // header constants
        private const string HEADER_CONTENT_LENGTH = "Content-Length";
        private const string HEADER_CONTENT_TYPE = "Content-Type";

        // defaults for timeouts
        private const int DEFAULT_TIMEOUT_HOURS = 0;
        private const int DEFAULT_TIMEOUT_MINUTES = 2;
        private const int DEFAULT_TIMEOUT_SECONDS = 0;

        // response handler delegates.
        public delegate void HttpClientResponseHandler(RssHttpClient objThis, object UserDataObject);
        public delegate void HttpClientTimeoutHandler(RssHttpClient objThis, object UserDataObject);
        public delegate void HttpClientErrorHandler(RssHttpClient objThis, object UserDataObject);

        // Headers 
        private List<KeyValuePair<string, string>> lstHeaders = new List<KeyValuePair<string, string>>();                 // list that holds headers
        private List<KeyValuePair<string, string>> lstAcceptHeaders = new List<KeyValuePair<string, string>>();         // accept headers
        private List<KeyValuePair<string, string>> lstQueryParameters = new List<KeyValuePair<string, string>>();         // This holds 

        // local handlers (functions)
        private HttpClientResponseHandler dlgResponseHandler = null;
        private HttpClientTimeoutHandler dlgTimeoutHandler = null;
        private HttpClientErrorHandler dlgErrorHandler = null;

        // request object
        private HttpClient objHttpClient = null;

        // response object
        private HttpResponseMessage objHttpResponse = null;


        // User Data that is state persistent
        private object objUserData = new object();

        // local class properties.
        private string strUrl = "";
        private string strUserID = "";
        private string strPassword = "";
        private int intTimeoutHours = DEFAULT_TIMEOUT_HOURS;
        private int intTimeoutMinutes = DEFAULT_TIMEOUT_MINUTES;
        private int intTimeoutSeconds = DEFAULT_TIMEOUT_SECONDS;
        private string strID = "";

        private int intExceptionCode = 0;
        private string strExceptionSource = "";
        private string strExceptionMessage = "";
        private bool boolExceptionFlag = false;


        // timespan variable
        private TimeSpan tsHttpTimeOut = new TimeSpan(DEFAULT_TIMEOUT_HOURS, DEFAULT_TIMEOUT_MINUTES, DEFAULT_TIMEOUT_SECONDS);

        // post bodys.
        private string strPostBody = "";
        private string strXml = "";
        private string strJson = "";


        // Get the HttpClient object
        public HttpClient HttpRequest
        {
            get { return objHttpClient; }
        }

        // Get the Response Message object
        public HttpResponseMessage HttpResponse
        {
            get { return objHttpResponse; }
        }

        // Get the Status Code
        public HttpStatusCode StatusCode
        {
            get {
                HttpStatusCode rt = HttpStatusCode.BadRequest;

                if (objHttpResponse != null)
                {
                    rt = objHttpResponse.StatusCode;
                }

                return rt;
            }
        }

        // Get the exception code.
        public int ExceptionCode
        {
            get { return intExceptionCode; }
        }

        // the exception source
        public string ExceptionSource
        {
            get { return strExceptionSource; }
        }

        // the exception message
        public string ExceptionMessage
        {
            get { return strExceptionMessage; }
        }

        // flag indicating whether exception occured
        public bool ExceptionFlag
        {
            get { return boolExceptionFlag; }
        }

        // stateful user data that will get 
        public object UserData
        {
            get { return objUserData; }
            set { objUserData = value; }

        }

        // url.  May or may not contain query parameters
        public string Url
        {
            get { return strUrl; }
            set { strUrl = value; }
        }

        // a string value user defined.
        public string ID
        {
            get { return strID; }
            set { strID = value; }
        }

        // function after response received.
        public HttpClientResponseHandler ResponseHandler
        {
            get { return dlgResponseHandler; }
            set { dlgResponseHandler = value; }
        }

        // function called if timeout occurs
        public HttpClientTimeoutHandler TimeoutHandler
        {
            get { return dlgTimeoutHandler; }
            set { dlgTimeoutHandler = value; }
        }

        // function called if error occurs
        public HttpClientErrorHandler ErrorHandler
        {
            get { return dlgErrorHandler; }
            set { dlgErrorHandler = value; }
        }



        // post body.  manually set
        public string PostBody
        {
            get { return strPostBody; }
            set { strPostBody = value; }
        }

        // used for submitting xml only
        public string Xml
        {
            get { return strXml; }
            set { strXml = value; }
        }

        // used for submitting JSON
        public string Json
        {
            get { return strJson; }
            set { strJson = value; }
        }


        // user id
        public string UserID
        {
            get { return strUserID; }
            set { strUserID = value; }
        }

        // user password
        public string Password
        {
            get { return strPassword; }
            set { strPassword = value; }
        }

        // timeout hours
        public int TimeoutHours
        {
            get { return intTimeoutHours; }
            set { intTimeoutHours = value; }
        }

        // timeout minutes
        public int TimeoutMinutes
        {
            get { return intTimeoutMinutes; }
            set { intTimeoutMinutes = value; }
        }

        // timeout seconds
        public int TimeoutSeconds
        {
            get { return intTimeoutSeconds; }
            set { intTimeoutSeconds = value; }
        }


        // -------------------------------------------
        // Adds a query or post parameter
        // -------------------------------------------
        public void AddQueryParameter(string sKey, string sValue)
        {
            KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(sKey, sValue);
            lstQueryParameters.Add(kvp);
        }

        // adds string query or post parameter
        public void aqp(string sKey, string sValue)
        {
            AddQueryParameter(sKey, sValue);
        }

        // adds datetime query or post parameter
        public void aqp(string sKey, DateTime dtValue)
        {
            string strDt = dtValue.ToString("yyyy-MM-ddTHH:mm:ss");
            AddQueryParameter(sKey, strDt);
        }

        // adds int query or post parameter
        public void aqp(string sKey, int nValue)
        {
            AddQueryParameter(sKey, nValue.ToString());
        }

        // adds double query or post parameter
        public void aqp(string sKey, double dValue)
        {
            AddQueryParameter(sKey, dValue.ToString());
        }

        // adds long query or post parameter
        public void aqp(string sKey, long lValue)
        {
            AddQueryParameter(sKey, lValue.ToString());
        }

        // adds float query or post parameter
        public void aqp(string sKey, float fVal)
        {
            AddQueryParameter(sKey, fVal.ToString());
        }

        // adds decimal query or post parameter
        public void aqp(string sKey, decimal dValue)
        {
            AddQueryParameter(sKey, dValue.ToString());
        }

        // adds byte query or post parameter
        public void aqp(string sKey, byte b)
        {
            aqp(sKey, (int)b);
        }

        // adds char query or post parameter
        public void aqp(string sKey, char c)
        {
            AddQueryParameter(sKey, c.ToString());
        }

        // adds boolean query or post parameter
        public void aqp(string sKey, bool b)
        {
            int nval = 0;
            if (b)
            {
                nval = 1;
            }
            aqp(sKey, nval);
        }

        // ----------------------------------------------
        // Adds a request header
        // ----------------------------------------------
        public void AddHdr(string sKey, string sVal)
        {
            // only add 
            if (string.Compare(sKey.ToLower(), HEADER_CONTENT_LENGTH.ToLower()) != 0)
            {

                KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(sKey, sVal);
                lstHeaders.Add(kvp);
            }
        }

        // -----------------------------------------
        // Adds an accept header
        // -----------------------------------------
        public void AddAcceptHdr(string sKey)
        {
            KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(sKey, "");
        }

        public void AddAcceptHdr(string sKey, double dVal)
        {
            KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(sKey, dVal.ToString());
        }


        // ------------------------------------------------------------
        // Gets the response as a string
        // ------------------------------------------------------------
        public async Task<string> GetResponseAsString()
        {
            string s = "";

            if (objHttpResponse != null)
            {
                if (objHttpResponse.Content != null)
                {
                    s = await objHttpResponse.Content.ReadAsStringAsync();
                }
            }
            return s;
        }

        // ------------------------------------------------------------
        // Gets response as a text reader
        // ------------------------------------------------------------
        public async Task<TextReader> GetResponseAsTextReader()
        {

            TextReader tx = null;

            try
            {
                string s = await GetResponseAsString();
                tx = new StringReader(s);
            }
            catch (Exception) { }

            return tx;
        }


        // ------------------------------------------------------------
        // Gets response as a xml reader
        // ------------------------------------------------------------

        public async Task<XmlReader> GetResponseAsXmlReader()
        {
            XmlReader xr = null;

            try
            {
                TextReader tx = await GetResponseAsTextReader();
                xr = XmlReader.Create(tx);
            }
            catch (Exception) { }

            return xr;

        }

        // ---------------------------------------------------------
        // Gets response as a stream
        // ---------------------------------------------------------
        public async Task<Stream> GetResponseAsStream()
        {
            Stream fs = null;

            if (objHttpResponse != null)
            {
                if (objHttpResponse.Content != null)
                {
                    try
                    {
                        Stream fss = await objHttpResponse.Content.ReadAsStreamAsync();
                        fs = fss;
                    }
                    catch (Exception) { }
                }
            }
            return fs;
        }


        // -----------------------------------------------------------
        // Gets response as a byte array
        // -----------------------------------------------------------
        public async Task<byte[]> GetResponseAsByteArray()
        {
            byte[] b = null;

            if (objHttpResponse != null)
            {
                if (objHttpResponse.Content != null)
                {
                    try
                    {
                        b = await objHttpResponse.Content.ReadAsByteArrayAsync();
                    }
                    catch (Exception)
                    { }
                }
            }
            return b;
        }



        // --------------------------------------------------------------
        // Does a get call.
        // --------------------------------------------------------------
        public async Task<HttpStatusCode> Get()
        {
            HttpStatusCode rc = HttpStatusCode.BadRequest;

            objHttpResponse = new HttpResponseMessage();

            // defines a new http client
            SetNewHttpClient();


            // now build the url.  
            string sUrl = strUrl;


            if (lstQueryParameters.Count > 0)
            {
                string sep = "";
                sUrl = sUrl + "?";
                for (int i = 0; i < lstQueryParameters.Count; i++)
                {
                    string tmp = sep + lstQueryParameters[i].Key + "=" + WebUtility.UrlEncode(lstQueryParameters[i].Value);
                    sUrl += tmp;
                    sep = "&";
                }
            }


            // add the headers
            AddHeaders();


            // do the get request
            try
            {


                // wait the whole time
                objHttpResponse = await objHttpClient.GetAsync(sUrl, HttpCompletionOption.ResponseContentRead);

                // capture the status code
                rc = objHttpResponse.StatusCode;




                // if the response is a timeout, then if the handler is defined, do the timeout handler, otherwise 
                // try and do the regular response handler
                if (rc == HttpStatusCode.RequestTimeout || rc == HttpStatusCode.GatewayTimeout)
                {
                    if (dlgTimeoutHandler != null)
                    {
                        dlgTimeoutHandler(this, objUserData);
                    }
                    else if (dlgResponseHandler != null)
                    {
                        dlgResponseHandler(this, objUserData);
                    }

                }


                // if not a timeout handler, then if the response handler is defined, 
                // do that.
                else
                {
                    if (dlgResponseHandler != null)
                    {
                        dlgResponseHandler(this, objUserData);
                    }
                }


            }


            // exception occurred
            catch (Exception ex)
            {
                boolExceptionFlag = true;
                intExceptionCode = ex.HResult;
                strExceptionSource = ex.Source;
                strExceptionMessage = ex.Message;

                if (dlgErrorHandler != null)
                {
                    dlgErrorHandler(this, objUserData);
                }
                else if (dlgResponseHandler != null)
                {
                    dlgResponseHandler(this, objUserData);
                }

            }

            return rc;
        }




        // -----------------------------------------------
        // does a post call
        // -----------------------------------------------
        public async Task<HttpStatusCode> Post()
        {
            HttpStatusCode rc = HttpStatusCode.BadRequest;

            int nContentLength = 0;

            string Utf8PostBody = "";

            // this is the post content
            HttpContent uCont;


            // defines a new http client
            SetNewHttpClient();



            // now we need to either post the query parameters, the preset post data, or the xml.
            if (strXml.Length == 0)
            {
                if (strJson.Length == 0)
                {

                    // if the post body is blank, then try and set via 
                    // any query parameters.
                    if (PostBody.Length == 0)
                    {

                        string sep = "";
                        for (int i = 0; i < lstQueryParameters.Count; i++)
                        {
                            KeyValuePair<string, string> kvp = lstQueryParameters[i];
                            strPostBody += sep + kvp.Key + "=" + WebUtility.UrlEncode(kvp.Value);
                            sep = "&";
                        }
                        Utf8PostBody = StringToUtf8(strPostBody);
                        nContentLength = Utf8PostBody.Length;


                        uCont = new StringContent(Utf8PostBody, Encoding.UTF8, "application/x-www-form-urlencoded");

                    }

                    else
                    {
                        Utf8PostBody = StringToUtf8(strPostBody);
                        uCont = new StringContent(Utf8PostBody, Encoding.UTF8, "application/x-www-form-urlencoded");
                    }

                    nContentLength = Utf8PostBody.Length;
                }

                else
                {
                    Utf8PostBody = StringToUtf8(strJson);
                    nContentLength = Utf8PostBody.Length;
                    uCont = new StringContent(strJson, Encoding.UTF8, "application/json");
                }

            }

            else
            {
                nContentLength = strXml.Length;
                Utf8PostBody = StringToUtf8(strXml);
                uCont = new StringContent(strXml, Encoding.UTF8, "application/xml");
            }




            //// add the rest of the headers
            AddHeaders();


            // get the response.
            try
            {

                // post the content.  wait for response
                objHttpResponse = await objHttpClient.PostAsync(Url, uCont);


                // get the status code
                rc = objHttpResponse.StatusCode;



                // if the response is a timeout, then if the handler is defined, do the timeout handler, otherwise 
                // try and do the regular response handler
                if (rc == HttpStatusCode.RequestTimeout || rc == HttpStatusCode.GatewayTimeout)
                {
                    if (dlgTimeoutHandler != null)
                    {
                        dlgTimeoutHandler(this, objUserData);
                    }
                    else if (dlgResponseHandler != null)
                    {
                        dlgResponseHandler(this, objUserData);
                    }

                }

                // if not a timeout handler, then if the response handler is defined, 
                // do that.
                else
                {
                    if (dlgResponseHandler != null)
                    {
                        dlgResponseHandler(this, objUserData);
                    }
                }


            }

            // exception occured. 
            catch (Exception ex)
            {

                boolExceptionFlag = true;
                intExceptionCode = ex.HResult;
                strExceptionSource = ex.Source;
                strExceptionMessage = ex.Message;

                if (dlgErrorHandler != null)
                {
                    dlgErrorHandler(this, objUserData);
                }
                else if (dlgResponseHandler != null)
                {
                    dlgResponseHandler(this, objUserData);
                }
            }

            return rc;
        }


        // -----------------------------------------------------------
        // Sets a new HttpClient
        // -----------------------------------------------------------
        private void SetNewHttpClient()
        {
            // if the user id is specified, then we 
            // need to specify a user id and passsword
            if (strUserID.Length > 0)
            {
                HttpClientHandler cliHandler = new HttpClientHandler();
                cliHandler.Credentials = new NetworkCredential(strUserID, strPassword);
                objHttpClient = new HttpClient(cliHandler);
            }
            else
            {
                objHttpClient = new HttpClient();
            }

            // set the client timeout
            objHttpClient.Timeout = new TimeSpan(intTimeoutHours, intTimeoutMinutes, intTimeoutSeconds);

        }




        // --------------------------------------------------------
        // This adds the request and accept headers to the client
        // --------------------------------------------------------
        private void AddHeaders()
        {
            // add the headers except for content type and content length
            for (int i = 0; i < lstHeaders.Count; i++)
            {
                KeyValuePair<string, string> kvp = lstHeaders[i];
                objHttpClient.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
            }

            // add the accept headers
            for (int i = 0; i < lstAcceptHeaders.Count; i++)
            {
                MediaTypeWithQualityHeaderValue mtyp = null;
                KeyValuePair<string, string> kvp = lstAcceptHeaders[i];
                if (kvp.Value.Length > 0)
                {
                    mtyp = new MediaTypeWithQualityHeaderValue(kvp.Key, Convert.ToDouble(kvp.Value));
                }
                else
                {
                    mtyp = new MediaTypeWithQualityHeaderValue(kvp.Key);
                }
                objHttpClient.DefaultRequestHeaders.Accept.Add(mtyp);
            }
        }

        // ----------------------------------------------------
        // Dispose of everything
        // ----------------------------------------------------
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                objHttpResponse.Dispose();
                objHttpClient.Dispose();
                dlgResponseHandler = null;
                dlgErrorHandler = null;
                dlgTimeoutHandler = null;
                objUserData = null;
            }
        }

        // -----------------------------------------------------
        // Disposing
        // -----------------------------------------------------

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }






        //// ----------------------------------------------------------------
        //// Convenience Routines
        //// ----------------------------------------------------------------

        //// form submit.
        //public void AddDefaultPostHeaders()
        //{
        //    AddHdr(HEADER_CONTENT_TYPE, "application/x-www-form-urlencoded");
        //}


        //// convenience for xml posts
        //public void AddDefaultXmlPostHeaders()
        //{
        //    AddHdr(HEADER_CONTENT_TYPE, "text/xml");
        //}

        //// convenience for json post
        //public void AddDefaultJsonPostHeader()
        //{
        //    AddHdr(HEADER_CONTENT_TYPE, "application/json");
        //}

        // convenience for xml accept
        public void AddXmlAcceptHeader()
        {
            AddAcceptHdr("application/xml");
        }

        // convenience for JSON accept
        public void AddJsonAcceptHeader()
        {
            AddAcceptHdr("application/json");
        }




        // ----------------------------------------------------------------
        // returns a utf8 encoded string
        // -----------------------------------------------------------------
        private string StringToUtf8(string s)
        {
            byte[] bytes = Encoding.Default.GetBytes(s);
            string pb = Encoding.UTF8.GetString(bytes);
            return pb;
        }





    }
}




