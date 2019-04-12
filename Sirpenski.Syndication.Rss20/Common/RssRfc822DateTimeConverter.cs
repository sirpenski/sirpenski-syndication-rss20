// ****************************************************************************************
// ****************************************************************************************
// Programmer: Paul F. Sirpenski
// FileName:   RssRfc822DateTimeConverter.cs
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
using System.Xml;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace Sirpenski.Syndication.Rss20
{
    class RssRfc822DateTimeConverter
    {
        // these constants indicate how the timezone was set.
        const int TIMEZONE_NOT_SET = 0;
        const int TIMEZONE_SET_BY_TIMEZONE_INFO = 1;
        const int TIMEZONE_SET_BY_MILITARY_MINUS_OFFSET_TO_UTC = 2;
        const int TIMEZONE_SET_BY_MILITARY_PLUS_OFFSET_TO_UTC = 3;
        const int TIMEZONE_SET_BY_4_CHARACTER_TIMEZONE_OFFSET = 4;
        const int TIMEZONE_SET_BY_5_CHARACTER_TIMEZONE_OFFSET = 5;

        // DatePart keys
        public const string DATEPART_DAY_OF_WEEK = "day_of_week";
        public const string DATEPART_DAY = "day";
        public const string DATEPART_MONTH = "month";
        public const string DATEPART_YEAR = "year";
        public const string DATEPART_HOUR = "hour";
        public const string DATEPART_MINUTE = "minute";
        public const string DATEPART_SECOND = "second";
        public const string DATEPART_TIMEZONE = "timezone";
        public const string DATEPART_TIMEZONE_OFFSET = "timezone_offset";
        public const string DATEPART_TIMEZONE_INFO_NAME = "timezone_info_name";

        // public list of keys to allow iteration of the dateparts
        public List<string> DATEPART_KEYS = new List<string>() { DATEPART_DAY_OF_WEEK, DATEPART_DAY, DATEPART_MONTH,
                                                                  DATEPART_YEAR, DATEPART_HOUR, DATEPART_MINUTE,
                                                                  DATEPART_SECOND, DATEPART_TIMEZONE, DATEPART_TIMEZONE_OFFSET,
                                                                  DATEPART_TIMEZONE_INFO_NAME};



        // working array to quickly validate day of week parameter
        private List<string> DAYS_OF_WEEK = new List<string>() { "MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN" };


        // private variables
        private string strInput = "";
        private DateTime rt = DateTime.MinValue;
        private DateTime rtUtc = DateTime.MinValue.ToUniversalTime();
        private TimeSpan tsUtcOffset = TimeSpan.Zero;
        private bool boolTimeZoneUsesDaylightSavingsTime = false;
        private bool InvalidTokenFormatError = false;
        private HashSet<KeyValuePair<string, string>> arrDateParts = new HashSet<KeyValuePair<string, string>>();


        /// <summary>
        ///  Input Value.  Set by parameter to parse
        /// </summary>
        public string Input
        {
            get {
                return strInput;
            }
        }

        /// <summary>
        /// Result In Local Time>
        /// </summary>
        public DateTime Result
        {
            get {
                return rt;
            }
        }

        /// <summary>
        /// Result In Universal Time
        /// </summary>
        public DateTime ResultUtc
        {
            get {
                return rtUtc;
            }
        }

        /// <summary>
        /// TImezone Offset of Local Time
        /// </summary>
        public TimeSpan UtcOffset
        {
            get {
                return tsUtcOffset;
            }
        }

        /// <summary>
        /// Flag indicating whether Input Timezone uses daylight savings time
        /// </summary>
        public bool TimeZoneUsesDaylightSavingsTime
        {
            get {
                return boolTimeZoneUsesDaylightSavingsTime;
            }
        }

        /// <summary>
        /// Gets the local time result in an RFC 822 compliant string
        /// </summary>
        public string ResultRfc822
        {
            get {
                return rt.ToString("ddd, dd MMM yyyy HH:mm:ss ") + UtcOffset.Hours.ToString("00") + UtcOffset.Minutes.ToString("00");
            }
        }



        /// <summary>
        /// Error Flag
        /// </summary>
        public bool Error
        {
            get {
                return InvalidTokenFormatError;
            }
        }


        /// <summary>
        /// DateParts, ie the tokens
        /// </summary>
        public HashSet<KeyValuePair<string, string>> DateParts
        {
            get {
                return arrDateParts;
            }
        }

        /// <summary>
        /// Gets the date part
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetDatePartValue(string key)
        {
            string rt = "";
            KeyValuePair<string, string> kvp = arrDateParts.FirstOrDefault(x => x.Key == key);
            if (!kvp.Equals(default(KeyValuePair<string, string>)))
            {
                rt = kvp.Value;
            }
            return rt;
        }



        /// <summary>
        /// Parse parses the input but throws an exception if the input is not 
        /// in RFC 822 compliant format.  It behaves like the DateTime.Parse method
        /// </summary>
        /// <param name="rfcDateTimeInput">RFC 822 Date Compliant String</param>
        /// <returns>DateTime</returns>
        public DateTime Parse(string rfcDateTimeInput)
        {
            DateTime rt = ParseRfc822(rfcDateTimeInput);
            if (Error)
            {
                throw new Exception("DateTime String Input Is Not RFC 822 Compliant");
            }
            return rt;

        }

        /// <summary>
        /// TryParse tries to do a parse and if successful, sets the reference. It behaves like microsoft
        /// TryParse DateTime method
        /// </summary>
        /// <param name="rfcDateTimeInput">RFC 822 Date Compliant String</param>
        /// <param name="dt">Input Parameter</param>
        /// <returns>boolean</returns>
        public bool TryParse(string rfcDateTimeInput, out DateTime dt)
        {
            bool rt = false;

            // set the date time to the minimum value
            dt = DateTime.MinValue;

            // parse the results
            DateTime rslt = ParseRfc822(rfcDateTimeInput);
            if (!Error)
            {
                dt = rslt;
                rt = true;
            }
            return rt;
        }




        /// <summary>
        /// Converts a datetime string in RFC 822 format to a datetime value
        /// </summary>
        /// <param name="txt">string</param>
        /// <returns></returns>
        public DateTime ParseRfc822(string txt)
        {


            // these are indexes that will be used to regenerate the 
            // datetime after parsing
            const int DAY_INDEX = 0;
            const int MONTH_INDEX = 1;
            const int YEAR_INDEX = 2;
            const int HOUR_INDEX = 3;
            const int MINUTE_INDEX = 4;
            const int SECOND_INDEX = 5;

            const int DATEPART_DAY_OF_WEEK_INDEX = 0;
            const int DATEPART_DAY_INDEX = 1;
            const int DATEPART_MONTH_INDEX = 2;
            const int DATEPART_YEAR_INDEX = 3;
            const int DATEPART_TIME_INDEX = 4;
            const int DATEPART_TIMEZONE_INDEX = 5;


            // Debug.WriteLine("RFC822 BEGIN CONVERT " + txt);

            // set the input string
            strInput = txt;




            // default to min date value
            rt = DateTime.MinValue;

            // clear the universal time;
            rtUtc = rt.ToUniversalTime();


            // set the timezone info to null
            TimeZoneInfo tzInfo = null;

            // set the uses daylightsavings time to false
            boolTimeZoneUsesDaylightSavingsTime = false;

            // error handler flag
            InvalidTokenFormatError = false;


            // a working array to hold the date parts.  default to min date time
            // equivalent to 1/1/0001 00:00:00 Universal time.  Note, array index 0 corresponds to the 
            // day in words token, we will ignore that seel later
            int[] wrkDt = new int[6] { 1, 1, 1, 0, 0, 0 };


            // default datetime offset
            tsUtcOffset = TimeSpan.Zero;


            // this holds the count of time tokens.  It will be used 
            // to validate the format
            int TimeTokenComponentCount = 0;

            // set the code of the part that actually sets the timezone to nothing.
            int TimeZoneSetByCode = TIMEZONE_NOT_SET;



            // define a list of days, we will use this as a look up tabl
            List<string> MONTHS = new List<string>() { "", "JAN", "FEB", "MAR", "APR", "MAY", "JUN",
                                                       "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };


            // split the string into parts by splitting on a space.  note that we trim the input text string first.
            string[] tokens = txt.Trim().Split(' ');


            // now, we are going to loop through the tokens.  Because the date component might have more 
            // than one space between the date components, we have to iterate skipping the tokens that are 
            // zero length

            // first, 
            int currentDatePartIndex = DATEPART_DAY_OF_WEEK_INDEX;


            // ITERATE THROUGH EACH TOKEN in the input string
            for (int tokenIndex = 0; tokenIndex < tokens.Length && !InvalidTokenFormatError; tokenIndex++)
            {

                // trim the token.  clear out any spaces, etc.
                string tkn = tokens[tokenIndex].Trim();


                // if the token has a non zero length, it is something
                if (tkn.Length > 0)
                {


                    // switch based upon the current part of the date we are examining.
                    switch (currentDatePartIndex)
                    {
                        // this is supposed to be the day of week token,  it may be the 
                        // day token.  we have to check it to see if it is a day of week token
                        case DATEPART_DAY_OF_WEEK_INDEX:

                            // set the day of week to the token
                            string DayOfWeek = tkn;

                            // lets get the first 3 letters of the token
                            if (tkn.Length >= 3)
                            {
                                // upper case the token
                                DayOfWeek = tkn.Substring(0, 3);
                            }

                            // now do a lookup in the DAYS_OF_WEEKS list
                            int DayOfWeekIndexFound = DAYS_OF_WEEK.FindIndex(x => x == DayOfWeek.ToUpper());


                            // if the token was not found, the token is actually the day.  Thus, 
                            // we need to decrement tokenIndex so the token will be processed again.  
                            // however, we need to increment the current datepart index so this process 
                            // gets skipped next iteration
                            if (DayOfWeekIndexFound == -1)
                            {

                                // one more check, we have to validate that this is a number
                                if (int.TryParse(tkn, out int rslt2))
                                {

                                    // reset
                                    tokenIndex -= 1;
                                    DayOfWeek = "";
                                }

                                // otherwise, the first token isnt a day or a number so it is an 
                                // invalid format
                                else
                                {
                                    InvalidTokenFormatError = true;
                                }
                            }


                            // set the datepart array
                            AddToDatePartList(DATEPART_DAY_OF_WEEK, DayOfWeek);

                            // increment the next part of the date
                            currentDatePartIndex += 1;
                            break;

                        // try and parse the token into a day integer value.  if successful, we 
                        // will store it in the working array which will be used to rebuild a 
                        // valid date time.
                        case DATEPART_DAY_INDEX:


                            if (int.TryParse(tkn, out wrkDt[DAY_INDEX]))
                            {

                                // add to the public datepart list
                                AddToDatePartList(DATEPART_DAY, wrkDt[DAY_INDEX].ToString());

                                // we have a good day token, proceed to month token
                                currentDatePartIndex += 1;
                            }

                            // otherwise, we have an invalid day token, bail out of loop
                            else
                            {
                                InvalidTokenFormatError = true;
                            }
                            break;



                        // token two is the month in 3 letters.  we do a lookup in the MONTHS array for the 
                        // index.  The index is the numeric representation of the month.  if we find it, we 
                        // wil insert it into the working date time array which will be used to rebuild the 
                        // date time.
                        case DATEPART_MONTH_INDEX:

                            // Debug.WriteLine("RFC822 PROCESSING MONTH TOKEN: " + tkn);
                            // Debug.WriteLine("");

                            wrkDt[MONTH_INDEX] = MONTHS.FindIndex(x => x == tkn.ToUpper());

                            // if the month index was found, it will be greater than zero
                            if (wrkDt[MONTH_INDEX] >= 0)
                            {

                                // add to the public datepart list
                                AddToDatePartList(DATEPART_MONTH, wrkDt[MONTH_INDEX].ToString());


                                // proceed to next token
                                currentDatePartIndex += 1;
                            }

                            // otherwise, we have an invalid month token, bail out of loop
                            else
                            {
                                InvalidTokenFormatError = true;
                            }
                            break;


                        // process the year token.  If valid, set it in the datetime working array 
                        // which we will use to reconstruct a valid datetime
                        case DATEPART_YEAR_INDEX:

                            // Debug.WriteLine("RFC822 PROCESSING YEAR TOKEN: " + tkn);
                            // Debug.WriteLine("");

                            if (int.TryParse(tkn, out wrkDt[YEAR_INDEX]))
                            {
                                // add to the public datepart list
                                AddToDatePartList(DATEPART_YEAR, wrkDt[YEAR_INDEX].ToString());

                                currentDatePartIndex += 1;
                            }

                            // otherwise, bail
                            else
                            {
                                InvalidTokenFormatError = true;
                            }
                            break;


                        // the time part is of the form hh:mm:ss  
                        case DATEPART_TIME_INDEX:

                            // Debug.WriteLine("RFC822 PROCESSING TIME TOKEN: " + tkn);
                            // Debug.WriteLine("");

                            // we need to split the time compent up by the colons
                            string[] tm = tkn.Split(':');

                            // here we are going to do a trick.  We are going to set the
                            // invalid token format to true.  If we get at least two time tokens, 
                            // we will reset it to false.
                            InvalidTokenFormatError = true;

                            if (tm.Length > 0)
                            {
                                // the first token is the hour
                                if (int.TryParse(tm[0], out wrkDt[HOUR_INDEX]))
                                {

                                    // reset to false, we have at least one time tokens (hr)
                                    InvalidTokenFormatError = false;

                                    // add to date part list
                                    AddToDatePartList(DATEPART_HOUR, wrkDt[HOUR_INDEX].ToString());

                                    // we are also going to increment the current date part index
                                    currentDatePartIndex += 1;

                                    // keep track of time token count.  
                                    TimeTokenComponentCount += 1;

                                    if (tm.Length > 1)
                                    {
                                        // now do the second token
                                        if (int.TryParse(tm[1], out wrkDt[MINUTE_INDEX]))
                                        {
                                            // add to the public datepart list
                                            AddToDatePartList(DATEPART_MINUTE, wrkDt[MINUTE_INDEX].ToString());

                                            // increment and keep track of time token component count
                                            TimeTokenComponentCount += 1;

                                            if (tm.Length > 2)
                                            {
                                                if (int.TryParse(tm[2], out wrkDt[SECOND_INDEX]))
                                                {
                                                    // add to the public datepart list
                                                    AddToDatePartList(DATEPART_SECOND, wrkDt[SECOND_INDEX].ToString());

                                                    TimeTokenComponentCount += 1;
                                                }

                                            }   // end if time tokens length is greater than 2

                                        }   // end if try parsing minute index

                                    }   // end if time tokens length greater than 1

                                }   // end try parse hour

                            }   // end if tm tokens length is greaterthan zero
                            break;



                        // now do the timezone.
                        case DATEPART_TIMEZONE_INDEX:

                            currentDatePartIndex += 1;



                            // upper case timezone token.
                            string tzToken = tkn.ToUpper();

                            // switch on the tzToken looking for the timezone info that corresponds to the 3 letter 
                            // timezone specified.
                            try
                            {

                                switch (tzToken)
                                {
                                    case "EST":
                                        tzInfo = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                                        break;
                                    case "EDT":
                                        tzInfo = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                                        boolTimeZoneUsesDaylightSavingsTime = true;
                                        break;
                                    case "CST":
                                        tzInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                                        break;
                                    case "CDT":
                                        tzInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                                        boolTimeZoneUsesDaylightSavingsTime = true;
                                        break;
                                    case "MST":
                                        tzInfo = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
                                        break;
                                    case "MDT":
                                        tzInfo = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
                                        boolTimeZoneUsesDaylightSavingsTime = true;
                                        break;
                                    case "PST":
                                        tzInfo = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
                                        break;
                                    case "PDT":
                                        tzInfo = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
                                        boolTimeZoneUsesDaylightSavingsTime = true;
                                        break;
                                    case "Z":
                                        tzInfo = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                                        break;
                                    case "UT":
                                        tzInfo = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                                        break;
                                    case "GMT":
                                        tzInfo = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                                        break;
                                    default:
                                        break;

                                }
                            }
                            catch (Exception) { }





                            // if we found it for the timezone id, 
                            if (tzInfo != null)
                            {
                                // calculate the offset from Universal time.
                                tsUtcOffset = tzInfo.BaseUtcOffset;

                                // add to the public datepart list
                                AddToDatePartList(DATEPART_TIMEZONE, tzToken);
                                AddToDatePartList(DATEPART_TIMEZONE_INFO_NAME, tzInfo.StandardName);

                                // set the control flag
                                TimeZoneSetByCode = TIMEZONE_SET_BY_TIMEZONE_INFO;

                            }



                            // if timespan not set, then let's check the 1 character military time for negative 
                            // offsets COdes A-M (excludes J)
                            if (TimeZoneSetByCode == TIMEZONE_NOT_SET)
                            {

                                // if the timezone token length is 1, then proceed
                                if (tzToken.Length == 1)
                                {


                                    string MIL1 = " ABCDEFGHIKLM";

                                    int ndx = MIL1.IndexOf(tzToken);

                                    if (ndx != -1)
                                    {
                                        TimeSpan tstmp = new TimeSpan(ndx, 0, 0);

                                        long tsTicks = tstmp.Ticks * -1;

                                        tsUtcOffset = new TimeSpan(tsTicks);

                                        TimeZoneSetByCode = TIMEZONE_SET_BY_MILITARY_MINUS_OFFSET_TO_UTC;

                                        boolTimeZoneUsesDaylightSavingsTime = false;

                                        AddToDatePartList(DATEPART_TIMEZONE, tzToken);

                                    }

                                }

                            }


                            // If timespan still not set, check the positive Military 1 character codes N-Y
                            if (TimeZoneSetByCode == TIMEZONE_NOT_SET)
                            {

                                // if timezone token length is 1, proceed
                                if (tzToken.Length == 1)
                                {

                                    string MIL2 = " NOPQRSTUVWXY";

                                    int ndx = MIL2.IndexOf(tzToken);

                                    if (ndx != -1)
                                    {
                                        tsUtcOffset = new TimeSpan(ndx, 0, 0);

                                        TimeZoneSetByCode = TIMEZONE_SET_BY_MILITARY_PLUS_OFFSET_TO_UTC;

                                        boolTimeZoneUsesDaylightSavingsTime = false;

                                        AddToDatePartList(DATEPART_TIMEZONE, tzToken);

                                    }
                                }

                            }




                            // if the timezone info was null, then we have to do more work, 
                            // ie parse the info and calculate the hours and minutes offset.
                            if (TimeZoneSetByCode == TIMEZONE_NOT_SET)
                            {
                                // Debug.WriteLine("RFC822 TESTNG FOR HOUR-MINUTE OFFSET TIMEZONE SPECIFICATION");

                                // define a ticks multiplier, 
                                long tsTickMultiplier = 1;
                                int tsHr = 0;
                                int tsMin = 0;


                                // set the invalid format flag, we will unset if we get 
                                // a good timezone offset conversion
                                InvalidTokenFormatError = true;

                                // parse timezone offset checking for a plus, minus at the beginning
                                if (tkn.Length == 5)
                                {

                                    // check first character to see if plus or minus.  If minus, 
                                    // set a negative multiplier
                                    if (string.Compare(tkn.Substring(0, 1), "-") == 0)
                                    {
                                        tsTickMultiplier = -1;
                                    }

                                    // parse the timezone offset, hours in positions 1,2
                                    if (int.TryParse(tkn.Substring(1, 2), out tsHr))
                                    {
                                        // parse minutes offset, minutes is in positions 3,4
                                        if (int.TryParse(tkn.Substring(3, 2), out tsMin))
                                        {

                                            InvalidTokenFormatError = false;

                                            TimeZoneSetByCode = TIMEZONE_SET_BY_5_CHARACTER_TIMEZONE_OFFSET;

                                        }

                                    }

                                }

                                // if the timezone info contains only 4 digits, assume a plus
                                else if (tkn.Length == 4)
                                {


                                    // parse the timezone offset.  Hour is in positions 0,1
                                    if (int.TryParse(tkn.Substring(0, 2), out tsHr))
                                    {

                                        // parse the minutes.  Minutes is in position 2,3
                                        if (int.TryParse(tkn.Substring(2, 2), out tsMin))
                                        {

                                            InvalidTokenFormatError = false;

                                            TimeZoneSetByCode = TIMEZONE_SET_BY_4_CHARACTER_TIMEZONE_OFFSET;

                                        }

                                    }
                                }

                                // otherwise, it is an invalid timezone format
                                else
                                {
                                    InvalidTokenFormatError = true;
                                }


                                // if the timezone is valid, let's compute the timestamp offset
                                if (!InvalidTokenFormatError)
                                {
                                    // compute a new timespan
                                    TimeSpan tsWrk = new TimeSpan(tsHr, tsMin, 0);

                                    // convert to ticks and apply the multiplier so we either get 
                                    // a positive or negative offset
                                    long tsTicks = tsWrk.Ticks * tsTickMultiplier;

                                    // now finally create the timspan offset
                                    tsUtcOffset = new TimeSpan(tsTicks);

                                    // add to public datepart list
                                    AddToDatePartList(DATEPART_TIMEZONE, tzToken);



                                }   // end if not invalid token

                            }   // end if not TimeSpanOffsetSet by 3 char code or 1 char militarycode

                            break;


                    }   // end switch


                }   // end token length > 0


            }   // end foreach datepart token


            // final format checks.  First, if the date format is not military, then 
            // ensure that there are at least two time tokens (hour and minute)
            if (TimeZoneSetByCode != TIMEZONE_SET_BY_MILITARY_MINUS_OFFSET_TO_UTC &&
                TimeZoneSetByCode != TIMEZONE_SET_BY_MILITARY_PLUS_OFFSET_TO_UTC)
            {
                if (TimeTokenComponentCount < 2)
                {
                    InvalidTokenFormatError = true;
                }
            }


            // if we did not have an invalid format error
            if (!InvalidTokenFormatError)
            {


                // now we begin to build a base universal time
                DateTime dtUniversalTime = new DateTime(wrkDt[YEAR_INDEX], wrkDt[MONTH_INDEX], wrkDt[DAY_INDEX],
                    wrkDt[HOUR_INDEX], wrkDt[MINUTE_INDEX], wrkDt[SECOND_INDEX], DateTimeKind.Utc);



                // now, if we got the timezone offset using a 3 character timezone specification, like EDT, PDT, MDT, CDT then 
                // we need to check if daylight savings time is in effect
                if (tzInfo != null)
                {
                    // verify timezone set by the timezone info structure
                    if (TimeZoneSetByCode == TIMEZONE_SET_BY_TIMEZONE_INFO)
                    {
                        // if the timezone uses daylight savings time
                        if (TimeZoneUsesDaylightSavingsTime)
                        {
                            // recalculate offset to universal time
                            tsUtcOffset = tzInfo.GetUtcOffset(dtUniversalTime);

                        }
                    }
                }

                // figure out plus, minus sign
                string plusMinusSign = "-";
                if (tsUtcOffset.Ticks > 0)
                {
                    plusMinusSign = "+";
                }
                else if (tsUtcOffset.Ticks == 0)
                {
                    plusMinusSign = "";
                }


                // AddToDatePart List
                AddToDatePartList(DATEPART_TIMEZONE_OFFSET, plusMinusSign + tsUtcOffset.ToString("hh") + ":" + tsUtcOffset.ToString("mm"));


                // now, negate the time span in order to calculate the universal time
                TimeSpan tsAdd = new TimeSpan(tsUtcOffset.Ticks * -1);


                // now we add the timespan to get to the true universal time
                dtUniversalTime = dtUniversalTime.Add(tsAdd);

                // set the utc result
                rtUtc = dtUniversalTime;


                // convert to local time and set the return
                rt = dtUniversalTime.ToLocalTime();

            }   // end if not invalid token





            // return the local time
            return rt;


        }



        /// *************************************************************************
        /// *************************************************************************
        /// <summary>
        /// This formats any DateTime to an RFC822 compliant string
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <returns>string</returns>
        /// *************************************************************************
        /// *************************************************************************
        public string FormatDateTime(DateTime dt)
        {
            return dt.ToString("ddd, dd MMM yyyy HH:mm:ss") + " " + dt.ToString("zzz").Replace(":", "");
        }



        /// <summary>
        /// This is a helper routine to add items to the datepart array
        /// </summary>
        /// <param name="ky">Key</param>
        /// <param name="v">Value</param>
        private void AddToDatePartList(string key, string v)
        {
            KeyValuePair<string, string> part = new KeyValuePair<string, string>(key, v);

            KeyValuePair<string, string> kvp = arrDateParts.FirstOrDefault(x => x.Key == part.Key);
            if (kvp.Equals(default(KeyValuePair<string, string>)))
            {
                arrDateParts.Add(part);
            }
            else
            {
                kvp = part;
            }

        }



    }
}





