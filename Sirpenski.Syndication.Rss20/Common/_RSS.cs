// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   _RSS.cs
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

namespace Sirpenski.Syndication.Rss20
{
    [Serializable]
    internal static class RSS
    {
        public const string GENERATOR = "Sirpenski RSS Feed Generator (sirpenski.com)";

        public const int GET_DESCRIPTION_FAVOR_CONTENT_ENCODED = 0;
        public const int GET_DESCRIPTION_FAVOR_DESCRIPTION = 1;

        public const int MIME_TYPE_IMAGE = 1;
        public const int MIME_TYPE_VIDEO = 2;
        public const int MIME_TYPE_AUDIO = 3;
        public const int MIME_TYPE_UNDEFINED = 0;

        public const string MEDIUM_TYPE_IMAGE = "image";
        public const string MEDIUM_TYPE_VIDEO = "video";
        public const string MEDIUM_TYPE_AUDIO = "audio";

        // extra space in front of zzz to accomodate for plus
        public const string RSS_DATETIME_FORMAT = "ddd, dd MMM yyyy HH:mm:ss zz00";

        public const int NAMESPACE_UNDEFINED = 0;
        public const int NAMESPACE_NONE = 1;

        // Namespace definitions
        public const string MEDIA_NAMESPACE_URL = "http://search.yahoo.com/mrss/";
        public const string MEDIA_NAMESPACE_PREFIX = "media";
        public const int MEDIA_NAMESPACE_ID = 2;

        public const string DUBLIN_CORE_NAMESPACE_URL = "http://purl.org/dc/elements/1.1/";
        public const string DUBLIN_CORE_NAMESPACE_PREFIX = "dc";
        public const int DUBLIN_CORE_NAMESPACE_ID = 3;

        public const string DUBLIN_CORE_TERMS_NAMESPACE_URL = "http://purl.org/dc/terms/";
        public const string DUBLIN_CORE_TERMS_NAMESPACE_PREFIX = "dcterms";
        public const int DUBLIN_CORE_TERMS_NAMESPACE_ID = 4;


        public const string GEORSS_NAMESPACE_URL = "http://www.georss.org/georss";
        public const string GEORSS_NAMESPACE_PREFIX = "georss";
        public const int GEORSS_NAMESPACE_ID = 5;

        public const string OPEN_GIS_GML_NAMESPACE_URL = "http://www.opengis.net/gml";
        public const string OPEN_GIS_GML_NAMESPACE_PREFIX = "gml";
        public const int OPEN_GIS_GML_NAMESPACE_ID = 6;


        public const string CONTENT_NAMESPACE_URL = "http://purl.org/rss/1.0/modules/content/";
        public const string CONTENT_NAMESPACE_PREFIX = "content";
        public const int CONTENT_NAMESPACE_ID = 7;

        public const string ATOM_NAMESPACE_URL = "http://www.w3.org/2005/Atom";
        public const string ATOM_NAMESPACE_PREFIX = "atom";
        public const int ATOM_NAMESPACE_ID = 8;

        public const string SLASH_NAMESPACE_URL = "http://purl.org/rss/1.0/modules/slash/";
        public const string SLASH_NAMESPACE_PREFIX = "slash";
        public const int SLASH_NAMESPACE_ID = 9;

        public const string CREATIVE_COMMONS_NAMESPACE_URL = "http://http://backend.userland.com/creativeCommonsRssModule";
        public const string CREATIVE_COMMONS_NAMESPACE_PREFIX = "creativeCommons";
        public const int CREATIVE_COMMONS_NAMESPACE_ID = 10;


        public static List<string> IMAGE_FILE_EXTENSIONS = new List<string>() 
        { 
            ".gif", ".png", ".jpg", ".jpeg", ".tiff", ".tif", ".bmp", 
            ".svg", ".psd", ".ai", ".xcf", ".crd",                                                           
            ".webp", ".eps", ".jif", ".jfif", ".heif", ".ico", 
            ".pct", ".pcx", ".pdf", ".psd", ".wmf", ".tga", ".raw", 
            ".indd"
        };

        public static List<string> VIDEO_FILE_EXTENSIONS = new List<string>() 
        { 
            ".webm", ".mkv", ".flv", ".vob", ".ogv", ".ogg", ".drc", 
            ".gifv", ".mng", ".mts", ".m2ts", ".mov", ".qt", ".wmv", 
            ".yuv", ".rm", ".rmvb", ".asf", ".amv", ".mp4", ".m4p", 
            ".m4v", ".mpg", ".mp2", ".mpeg", ".m2v", ".m4v", ".svi", 
            ".3gp", ".3g2", ".mxf", ".roq", ".nsv", ".f4v", ".f4p", 
            ".f4a", ".f4b"
        };


        public static List<string> AUDIO_FILE_EXTENSIONS = new List<string>() 
        { 
            ".aa", ".aac", ".aax", ".act", ".aiff", ".amr", ".ape", ".au", ".awb", 
            ".dct", ".dvf", ".flas", ".gsm", ".iklax", ".ivs", ".m4a", ".m4b", "m4p", 
            ".mmf", ".mp3", ".mpc", ".msv", ".nmf", ".nsf", ".oga", ".mogg", ".opus", 
            ".ra", ".rm", ".raw", ".sln", ".tta", ".vox", ".wma", ".8svx"
        };

        /// <summary>
        /// Flag indicating whether namespaces initialized.
        /// </summary>
        public static bool NamespacesInitialized { get; set; } = false;


        /// <summary>
        /// Media Namespace
        /// </summary>
        public static XNamespace MEDIA_NS { get; set; } = null;

        /// <summary>
        /// Dublin Core Namespace
        /// </summary>
        public static XNamespace DUBLIN_CORE_NS { get; set; } = null;

        /// <summary>
        /// Dublin Core Terms Namespace
        /// </summary>
        public static XNamespace DUBLIN_CORE_TERMS_NS { get; set; } = null;

        /// <summary>
        /// Geo Rss Namespace
        /// </summary>
        public static XNamespace GEORSS_NS { get; set; } = null;

        /// <summary>
        /// Geography Markup Language Namespace
        /// </summary>
        public static XNamespace GML_NS { get; set; } = null;

        /// <summary>
        /// Content Namespace
        /// </summary>
        public static XNamespace CONTENT_NS { get; set; } = null;

        /// <summary>
        /// Atom Namespace
        /// </summary>
        public static XNamespace ATOM_NS { get; set; } = null;

        /// <summary>
        /// Slash Namespace
        /// </summary>
        public static XNamespace SLASH_NS { get; set; } = null;


        /// <summary>
        /// Creative Commons Namespace
        /// </summary>
        public static XNamespace CREATIVE_COMMONS_NS { get; set; } = null;


        /// <summary>
        /// Initializes Static Namespaces.  This is called by the constructors if the 
        /// namespaces have not been initialized.
        /// </summary>
        public static void InitializeStaticNamespaces()
        {
            // define namespaces
            MEDIA_NS = XNamespace.Get(MEDIA_NAMESPACE_URL);
            DUBLIN_CORE_NS = XNamespace.Get(DUBLIN_CORE_NAMESPACE_URL);
            DUBLIN_CORE_TERMS_NS = XNamespace.Get(DUBLIN_CORE_TERMS_NAMESPACE_URL);
            GEORSS_NS = XNamespace.Get(GEORSS_NAMESPACE_URL);
            GML_NS = XNamespace.Get(OPEN_GIS_GML_NAMESPACE_URL);
            CONTENT_NS = XNamespace.Get(CONTENT_NAMESPACE_URL);
            ATOM_NS = XNamespace.Get(ATOM_NAMESPACE_URL);
            SLASH_NS = XNamespace.Get(SLASH_NAMESPACE_URL);
            CREATIVE_COMMONS_NS = XNamespace.Get(CREATIVE_COMMONS_NAMESPACE_URL);

            // set the flag
            NamespacesInitialized = true;
        }


        // ----------------------------------------------------------------------
        // Begin Static Methods
        // ----------------------------------------------------------------------


        /// <summary>
        /// This translates the namespace to a namespace id constant which 
        /// can then be used in switch statements
        /// </summary>
        /// <param name="ns">namespace</param>
        /// <returns>int</returns>
        public static int NamespaceToID(XNamespace ns)
        {
            int rt = NAMESPACE_UNDEFINED;
            if (ns == XNamespace.None)
            {
                rt = NAMESPACE_NONE;
            }
            else if (ns == MEDIA_NS) {
                rt = MEDIA_NAMESPACE_ID;
            }
            else if (ns == DUBLIN_CORE_NS)
            {
                rt = DUBLIN_CORE_NAMESPACE_ID;
            }
            else if (ns == DUBLIN_CORE_TERMS_NS)
            {
                rt = DUBLIN_CORE_TERMS_NAMESPACE_ID;
            }
            else if (ns == GEORSS_NS)
            {
                rt = GEORSS_NAMESPACE_ID;
            }
            else if (ns == GML_NS)
            {
                rt = OPEN_GIS_GML_NAMESPACE_ID;
            }
            else if (ns == CONTENT_NS)
            {
                rt = CONTENT_NAMESPACE_ID;
            }
            else if (ns == ATOM_NS)
            {
                rt = ATOM_NAMESPACE_ID;
            }
            else if (ns == SLASH_NS)
            {
                rt = SLASH_NAMESPACE_ID;
            }
            else if (ns == CREATIVE_COMMONS_NS)
            {
                rt = CREATIVE_COMMONS_NAMESPACE_ID;
            }


            return rt;


        }



        /// <summary>
        /// Adds an extension which is associated with image urls
        /// </summary>
        /// <param name="extension_with_period"></param>
        public static void AddImageFileExtension(string extension_with_period)
        {
            string ext = extension_with_period.ToLower();
            int ndx = IMAGE_FILE_EXTENSIONS.FindIndex(x => x == ext);
            if (ndx == -1)
            {
                IMAGE_FILE_EXTENSIONS.Add(ext);
            }

        }

        /// <summary>
        /// Remove an audio extension.  Use this to maintain teh
        /// </summary>
        /// <param name="extension_with_period"></param>
        public static void RemoveImageFileExtension(string extension_with_period)
        {
            string ext = extension_with_period.ToLower();
            int ndx = IMAGE_FILE_EXTENSIONS.FindIndex(x => x == ext);
            if (ndx >= 0)
            {
                IMAGE_FILE_EXTENSIONS.RemoveAt(ndx);
            }
        }



        /// <summary>
        /// Adds an extension which is associated with video urls
        /// </summary>
        /// <param name="extension_with_period"></param>
        public static void AddVideoFileExtension(string extension_with_period)
        {
            string ext = extension_with_period.ToLower();
            int ndx = VIDEO_FILE_EXTENSIONS.FindIndex(x => x == ext);
            if (ndx == -1)
            {
                VIDEO_FILE_EXTENSIONS.Add(ext);
            }

        }

        /// <summary>
        /// Remove a video extension
        /// </summary>
        /// <param name="extension_with_period"></param>
        public static void RemoveVideoFileExtension(string extension_with_period)
        {
            string ext = extension_with_period.ToLower();
            int ndx = VIDEO_FILE_EXTENSIONS.FindIndex(x => x == ext);
            if (ndx >= 0)
            {
                VIDEO_FILE_EXTENSIONS.RemoveAt(ndx);
            }
        }


        /// <summary>
        /// Adds an extension which is associated with video urls
        /// </summary>
        /// <param name="extension_with_period"></param>
        public static void AddAudioFileExtension(string extension_with_period)
        {
            string ext = extension_with_period.ToLower();
            int ndx = AUDIO_FILE_EXTENSIONS.FindIndex(x => x == ext);
            if (ndx == -1)
            {
                AUDIO_FILE_EXTENSIONS.Add(ext);
            }

        }

        /// <summary>
        /// Remove an audio extension
        /// </summary>
        /// <param name="extension_with_period"></param>
        public static void RemoveAudioFileExtension(string extension_with_period)
        {
            string ext = extension_with_period.ToLower();
            int ndx = AUDIO_FILE_EXTENSIONS.FindIndex(x => x == ext);
            if (ndx >= 0)
            {
                AUDIO_FILE_EXTENSIONS.RemoveAt(ndx);
            }
        }


        /// <summary>
        /// Compares two strings
        /// </summary>
        /// <param name="compareToMedium">Medium to compare to</param>
        /// <param name="testMedium">Medium parameter to test</param>
        /// <returns>bool (True if equal, False if not equal)</returns>
        public static bool IsMediumEqual(string compareToMedium, string testMedium)
        {
            bool rt = false;
            if (string.Compare(compareToMedium.ToLower(), testMedium.ToLower()) == 0)
            {
                rt = true;
            }
            return rt;
        }


        /// <summary>
        /// Tests if mime type is an image mime type
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        public static bool IsImageMimeType(string mimeType)
        {
            bool rt = false;
            if (mimeType.ToLower().Contains("image/"))
            {
                rt = true;
            }
            return rt;
        }

        /// <summary>
        /// Tests if mime type is a video mime type
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        public static bool IsVideoMimeType(string mimeType)
        {
            bool rt = false;
            if (mimeType.ToLower().Contains("video/"))
            {
                rt = true;
            }
            return rt;
        }

        /// <summary>
        /// Tests if mime type is an audio mime type.
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns>bool</returns>
        public static bool IsAudioMimeType(string mimeType)
        {
            bool rt = false;
            if (mimeType.ToLower().Contains("audio/"))
            {
                rt = true;
            }
            return rt;
        }


        /// <summary>
        /// gets the mime type type (image, video, audio) 
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns>int</returns>
        public static int GetMimeTypeType(string mimeType)
        {
            int rt = MIME_TYPE_UNDEFINED;
            if (IsImageMimeType(mimeType))
            {
                rt = MIME_TYPE_IMAGE;
            }
            
            if (rt == MIME_TYPE_UNDEFINED)
            {
                if (IsVideoMimeType(mimeType))
                {
                    rt = MIME_TYPE_VIDEO;
                }
            }

            if (rt == MIME_TYPE_UNDEFINED)
            {
                if (IsAudioMimeType(mimeType))
                {
                    rt = MIME_TYPE_AUDIO;
                }
            }

            return rt;
        }

        /// <summary>
        /// Tests if the url is an image url.
        /// </summary>
        /// <param name="url">Url of Image File</param>
        /// <returns>bool</returns>
        public static bool IsImageUrl(string url)
        {
            bool rt = false;

            if (Path.HasExtension(url))
            {
                string ext = Path.GetExtension(url).ToLower();
                int ndx = IMAGE_FILE_EXTENSIONS.FindIndex(x => x == ext);
                if (ndx >= 0)
                {
                    rt = true;
                }
            }
            return rt;
        }


        /// <summary>
        /// Tests if the url is a video url.
        /// </summary>
        /// <param name="url">Url of Video File</param>
        /// <returns>bool</returns>
        public static bool IsVideoUrl(string url)
        {
            bool rt = false;

            if (Path.HasExtension(url))
            {
                string ext = Path.GetExtension(url).ToLower();
                int ndx = VIDEO_FILE_EXTENSIONS.FindIndex(x => x == ext);
                if (ndx >= 0)
                {
                    rt = true;
                }
            }
            return rt;
        }


        /// <summary>
        /// Tests if the url is an audio url
        /// </summary>
        /// <param name="url">Url of Video File</param>
        /// <returns>bool</returns>
        public static bool IsAudioUrl(string url)
        {
            bool rt = false;

            if (Path.HasExtension(url))
            {
                string ext = Path.GetExtension(url).ToLower();
                int ndx = AUDIO_FILE_EXTENSIONS.FindIndex(x => x == ext);
                if (ndx >= 0)
                {
                    rt = true;
                }
            }
            return rt;
        }


    }
}





