// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssFileReaderWriter.cs
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
using System.IO;

namespace Sirpenski.Syndication.Rss20
{
    public class RssFileReaderWriter
    {
        public string FileName { get; set; } = "";
      
        public bool Save(string s, string fn = "")
        {
            bool rt = false;

            if (fn.Length > 0)
            {
                FileName = fn;
            }

            try
            {
                string folder = Path.GetDirectoryName(FileName);

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                using (StreamWriter writer = new StreamWriter(FileName))
                {
                    writer.Write(s);
                }
                rt = true;
            }
            catch (Exception) { }
            return rt;
        }


        /// <summary>
        /// Reads a file into a string
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string Read(string fn = "")
        {
            string rt = null;

            if (fn.Length > 0)
            {
                FileName = fn;
            }

            if (File.Exists(FileName))
            {

                using (StreamReader rdr = new StreamReader(FileName))
                {
                    string readMeText = rdr.ReadToEnd();
                    rt = readMeText;
                }
            }

            return rt;
        }
    }
}
