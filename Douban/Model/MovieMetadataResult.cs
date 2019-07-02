using System;
using System.Collections.Generic;
using System.Text;

namespace Douban.Model
{
    public class MovieMetadataResult
    {
        public MetRating rating { get; set; }
        public int reviews_count { get; set; }
        public List<Video> videos { get; set; }
        public int wish_count { get; set; }
        public string original_title { get; set; }
        public string[] blooper_urls { get; set; }
        public int collect_count { get; set; }
        public MetImages images { get; set; }
        public string douban_site { get; set; }
        public string year { get; set; }
        public List<PopularComment> popular_comments { get; set; }
        public string alt { get; set; }
        public string id { get; set; }
        public string mobile_url { get; set; }
        public int photos_count { get; set; }
        public string pubdate { get; set; }
        public string title { get; set; }
        public int do_count { get; set; }
        public bool has_video { get; set; }
        public string share_url { get; set; }
        public int seasons_count { get; set; }
        public string[] languages { get; set; }
        public string schedule_url { get; set; }
        public List<Writer> writers { get; set; }
        public string[] pubdates { get; set; }
        public string website { get; set; }
        public string[] tags { get; set; }
        public bool has_schedule { get; set; }
        public string[] durations { get; set; }
        public string[] genres { get; set; }
        public string[] trailers { get; set; }
        public string episodes_count { get; set; }
        public string[] trailer_urls { get; set; }
        public bool has_ticket { get; set; }
        public string[] bloopers { get; set; }
        public string[] clip_urls { get; set; }
        public int current_season { get; set; }
        public List<Cast> casts { get; set; }
        public string[] countries { get; set; }
        public string mainland_pubdate { get; set; }
        public List<Photo> photos { get; set; }
        public string summary { get; set; }
        public string subtype { get; set; }
        public List<Director> directors { get; set; }
        public int comments_count { get; set; }
        public List<PopularReview> popular_reviews { get; set; }
        public int ratings_count { get; set; }
        public string[] aka { get; set; }
    }

    public class MetRating
    {
        public int max { get; set; }
        public float average { get; set; }
        public string stars { get; set; }
        public int min { get; set; }
    }

    public class Source
    {
        public string literal { get; set; }
        public string pic { get; set; }
        public string name { get; set; }
    }

    public class Video
    {
        public Source source { get; set; }
        public string sample_link { get; set; }
        public string video_id { get; set; }
        public bool need_pay { get; set; }
    }

    public class MetImages
    {
        public string small { get; set; }
        public string large { get; set; }
        public string medium { get; set; }
    }

    public class Rating2
    {
        public int max { get; set; }
        public double value { get; set; }
        public int min { get; set; }
    }

    public class Author
    {
        public string uid { get; set; }
        public string avatar { get; set; }
        public string signature { get; set; }
        public string alt { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }

    public class PopularComment
    {
        public Rating2 rating { get; set; }
        public int useful_count { get; set; }
        public Author author { get; set; }
        public string subject_id { get; set; }
        public string content { get; set; }
        public string created_at { get; set; }
        public string id { get; set; }
    }

    public class Avatars
    {
        public string small { get; set; }
        public string large { get; set; }
        public string medium { get; set; }
    }

    public class Writer
    {
        public Avatars avatars { get; set; }
        public string name_en { get; set; }
        public string name { get; set; }
        public string alt { get; set; }
        public string id { get; set; }
    }

    public class Avatars2
    {
        public string small { get; set; }
        public string large { get; set; }
        public string medium { get; set; }
    }

    public class Cast
    {
        public Avatars2 avatars { get; set; }
        public string name_en { get; set; }
        public string name { get; set; }
        public string alt { get; set; }
        public string id { get; set; }
    }

    public class Photo
    {
        public string thumb { get; set; }
        public string image { get; set; }
        public string cover { get; set; }
        public string alt { get; set; }
        public string id { get; set; }
        public string icon { get; set; }
    }

    public class Director
    {
        public object avatars { get; set; }
        public string name_en { get; set; }
        public string name { get; set; }
        public object alt { get; set; }
        public object id { get; set; }
    }

    public class Rating3
    {
        public int max { get; set; }
        public double value { get; set; }
        public int min { get; set; }
    }

    public class Author2
    {
        public string uid { get; set; }
        public string avatar { get; set; }
        public string signature { get; set; }
        public string alt { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }

    public class PopularReview
    {
        public Rating3 rating { get; set; }
        public string title { get; set; }
        public string subject_id { get; set; }
        public Author2 author { get; set; }
        public string summary { get; set; }
        public string alt { get; set; }
        public string id { get; set; }
    }
}
