using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;


namespace CodingChallenge
{
    public class tweets_cleaned
    {
        public  void Page_Load(object sender, EventArgs e)
        {
            ReadJsonFiles();
        }
        public void ReadJsonFiles()
        {
            List<string> Lines = GetJsonItems();
            string contentLines = string.Empty;
            int ASCIIcnt = 0;
            foreach (var jsonline in Lines)
            {
                RootObject objItem = JsonConvert.DeserializeObject<RootObject>(jsonline);
                string Created_at = objItem.created_at;
                string Text = objItem.text;
                if (objItem.text != null)
                {
                    if (Encoding.UTF8.GetByteCount(Text) != Text.Length)
                    {
                        Text = Regex.Replace(Text, @"[^\u0000-\u007F]", string.Empty);
                        ASCIIcnt++;
                    }
                }
                contentLines += Text + "(timestamp: " + Created_at + ")\r\n";

            }
            if (ASCIIcnt > 0)
            {
                contentLines += ASCIIcnt + " tweets contained unicode.";
            }
            
            string filepath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\tweet_output\\"));            
            System.IO.StreamWriter file = new System.IO.StreamWriter(filepath + "ft1.txt");
            file.WriteLine(contentLines);
            file.Close();
        }

        public List<string> GetJsonItems()
        {
            string filepath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\tweet_input\\"));
            
            int BracketCount = 0;
            string JSON = new StreamReader(filepath + "tweets.txt").ReadToEnd();
            List<string> JsonItems = new List<string>();
            StringBuilder Json = new StringBuilder();

            foreach (char c in JSON)
            {
                if (c == '{')
                    ++BracketCount;
                else if (c == '}')
                    --BracketCount;
                Json.Append(c);

                if (BracketCount == 0 && c != ' ' && c != '\r' && c != '\n')
                {
                    JsonItems.Add(Json.ToString());
                    Json = new StringBuilder();
                }
            }
            return JsonItems;
        }

        public class User
        {
            public string id_str { get; set; }
            public string name { get; set; }
            public string screen_name { get; set; }
            public object location { get; set; }
            public object url { get; set; }
            public string description { get; set; }
            public bool @protected { get; set; }
            public bool verified { get; set; }
            public int followers_count { get; set; }
            public int friends_count { get; set; }
            public int listed_count { get; set; }
            public int favourites_count { get; set; }
            public int statuses_count { get; set; }
            public string created_at { get; set; }

            public string time_zone { get; set; }
            public bool geo_enabled { get; set; }
            public string lang { get; set; }
            public bool contributors_enabled { get; set; }
            public bool is_translator { get; set; }
            public string profile_background_color { get; set; }
            public string profile_background_image_url { get; set; }
            public string profile_background_image_url_https { get; set; }
            public bool profile_background_tile { get; set; }
            public string profile_link_color { get; set; }
            public string profile_sidebar_border_color { get; set; }
            public string profile_sidebar_fill_color { get; set; }
            public string profile_text_color { get; set; }
            public bool profile_use_background_image { get; set; }
            public string profile_image_url { get; set; }
            public string profile_image_url_https { get; set; }
            public bool default_profile { get; set; }
            public bool default_profile_image { get; set; }
            public object following { get; set; }
            public object follow_request_sent { get; set; }
            public object notifications { get; set; }
        }

        public class BoundingBox
        {
            public string type { get; set; }
            public List<List<List<double>>> coordinates { get; set; }
        }

        public class Attributes
        {
        }

        public class Place
        {
            public string id { get; set; }
            public string url { get; set; }
            public string place_type { get; set; }
            public string name { get; set; }
            public string full_name { get; set; }
            public string country_code { get; set; }
            public string country { get; set; }
            public BoundingBox bounding_box { get; set; }
            public Attributes attributes { get; set; }
        }

        public class Entities
        {
            public List<object> hashtags { get; set; }
            public List<object> urls { get; set; }
            public List<object> user_mentions { get; set; }
            public List<object> symbols { get; set; }
        }

        public class RootObject
        {
            public string created_at { get; set; }
            public long id { get; set; }
            public string id_str { get; set; }
            public string text { get; set; }
            public string source { get; set; }
            public bool truncated { get; set; }
            public object in_reply_to_status_id { get; set; }
            public object in_reply_to_status_id_str { get; set; }
            public object in_reply_to_user_id { get; set; }
            public object in_reply_to_user_id_str { get; set; }
            public object in_reply_to_screen_name { get; set; }
            public User user { get; set; }
            public object geo { get; set; }
            public object coordinates { get; set; }
            public Place place { get; set; }
            public object contributors { get; set; }
            public bool is_quote_status { get; set; }
            public int retweet_count { get; set; }
            public int favorite_count { get; set; }
            public Entities entities { get; set; }
            public bool favorited { get; set; }
            public bool retweeted { get; set; }
            public string filter_level { get; set; }
            public string lang { get; set; }
            public string timestamp_ms { get; set; }
        }
    }
}
