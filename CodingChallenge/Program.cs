using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace CodingChallenge
{
    class Program
    {
        public static void Main(string[] args)
        {          
            tweets_cleaned tweet = new tweets_cleaned();
            tweet.ReadJsonFiles();
            average_degree average = new average_degree();
            average.ReadJsonFiles();
        }
    }
}
