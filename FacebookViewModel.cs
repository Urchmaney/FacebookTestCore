using System;
namespace FacebookTest.Models
{
    public class FacebookUserModel
    {

        public string id { get; set; }

        public string name { get; set; }

        public string email { get; set; }


    }

    public class TokenModel
    {
        public string access_token { get; set; }

        public string token_type { get; set; }

        public string expires_in { get; set; }

    }

    public class FacebookPages
    {
        public account account { get; set; }

        public string id { get; set; }
    }

    public class account
    {
        public List<data> data { get; set; }

        public paging paging { get; set; }
    }

    public class paging
    {
        public cursor cursors { get; set; }
    }

    public class cursor
    {
        public string before { get; set; }
        public string after { get; set; }
    }

    public class data
    {
        public string access_token { get; set; }
        public string category { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public List<string> perms { get; set; }
    }
}
