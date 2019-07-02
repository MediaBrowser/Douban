using System;
using System.Collections.Generic;
using System.Text;

namespace Douban.Model
{
    class MovieSearchResult
    {
        public int count { get; set; }
        public int start { get; set; }
        public int total { get; set; }
        public List<Subject> subjects { get; set; }
        public string title { get; set; }
    }

    public class Rating
    {
        public int max { get; set; }
        public float average { get; set; }
        public string stars { get; set; }
        public int min { get; set; }
    }

    public class Images
    {
        public string small { get; set; }
        public string large { get; set; }
        public string medium { get; set; }
    }

    public class Subject
    {
        public Rating rating { get; set; }
        public string[] genres { get; set; }
        public string title { get; set; }
        public List<object> casts { get; set; }
        public int collect_count { get; set; }
        public string original_title { get; set; }
        public string subtype { get; set; }
        public string[] directors { get; set; }
        public string year { get; set; }
        public Images images { get; set; }
        public string alt { get; set; }
        public string id { get; set; }
    }
}
