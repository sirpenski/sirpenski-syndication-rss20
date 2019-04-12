// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssCoreItem_Extension_Get.cs
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
using System.Xml;
using System.Net;
using System.IO;
using System.Diagnostics;


namespace Sirpenski.Syndication.Rss20.Core
{
    public partial class RssCoreItem
    {

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// This method retrieves enclosures of a specific mime type
        /// </summary>
        /// <param name="mimeType">Mime Type.  NOTE... paramters such as image, video, audio alone are also allowed.  </param>
        /// <param name="ExitAfterOneFound"></param>
        /// <returns></returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public IEnumerable<RssCoreItemEnclosure> GetEnclosures(string medium = "", string mimeType = "", bool ExitAfterOneFound = false)
        {
            List<RssCoreItemEnclosure> rt = new List<RssCoreItemEnclosure>();
            StringComparer cmp = StringComparer.OrdinalIgnoreCase;
    

            bool TestMedium = false;
            bool TestMimeType = false;

            // set the test flags
            if (medium.Length > 0)
            {
                TestMedium = true;
            }
            if (mimeType.Length > 0)
            {
                TestMimeType = true;
            }


            
            // now loop through each enclosure
            for (int i = 0; i < enclosures.Count; i++)
            {
                bool bRslt = false;

                if (!bRslt  && TestMimeType)
                {
                    if (TestMimeType)
                    {
                        if (cmp.Compare(mimeType, enclosures[i].type) == 0)
                        {
                            bRslt = true;
                        }
                    }
                }

                // if not good and we are testing for the general image, video, or 
                // audio mime types
                if (!bRslt && TestMedium)
                {
                    switch(medium)
                    {
                        case RSS.MEDIUM_TYPE_IMAGE:
                            if (RSS.IsImageMimeType(enclosures[i].type))
                            {
                                bRslt = true;
                            }
                            break;
                        case RSS.MEDIUM_TYPE_VIDEO:
                            if (RSS.IsVideoMimeType(enclosures[i].type))
                            {
                                bRslt = true;
                            }
                            break;
                        case RSS.MEDIUM_TYPE_AUDIO:
                            if (RSS.IsAudioMimeType(enclosures[i].type))
                            {
                                bRslt = true;

                            }
                            break;
                    }
                }


                // if we are testing the url?
                if (!bRslt && !TestMimeType)
                {
                    switch(mimeType)
                    {
                        case RSS.MEDIUM_TYPE_IMAGE:
                            if (RSS.IsImageUrl(enclosures[i].url))
                            {
                                bRslt = true;
                            }
                            break;
                        case RSS.MEDIUM_TYPE_VIDEO:
                            if (RSS.IsVideoUrl(enclosures[i].url))
                            {
                                bRslt = true;
                            }
                            break;

                        case RSS.MEDIUM_TYPE_AUDIO:
                            if (RSS.IsAudioUrl(enclosures[i].url))
                            {
                                bRslt = true;
                            }
                            break;
                    }
                }


                // if there were no tests, 
                if (!bRslt && !TestMimeType && !TestMedium)
                {
                    bRslt = true;
                }


                // if enclosure is a goodie
                if (bRslt)
                {
                    // add the item to the returned enclosures list
                    rt.Add(enclosures[i]);

                    // now, if we are looking for only the first one, then we need to break
                    if (ExitAfterOneFound)
                    {
                        break;
                    }
                }

            }
     

            return rt;

        }

        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Gets the first enclosure
        /// </summary>
        /// <param name="mimeType">Mime Type.  NOTE... paramters such as image, video, audio alone are also allowed.</param>
        /// <returns>RssCoreItemEnclosure</returns>
        // -------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------
        public RssCoreItemEnclosure GetFirstEnclosure(string medium = "", string mimeType = "")
        {
            RssCoreItemEnclosure rt = null;
            IEnumerable<RssCoreItemEnclosure> lst = GetEnclosures(medium, mimeType, ExitAfterOneFound: true);

            if (lst.Count() > 0)
            {
                rt = lst.First();
            }
            return rt;

        }


    



    }
}




