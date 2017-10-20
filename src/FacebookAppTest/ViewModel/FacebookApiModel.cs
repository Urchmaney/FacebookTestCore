using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacebookAppTest.ViewModel
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
        public account accounts { get; set; }

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

    public class PagePosts
    {
        public feed feed { get; set; }
        public paging paging { get; set; }
    }

    public class feed
    {
        public List<Post> data { get; set; }
    }

   

    public class Post
    {
        public string created_time { get; set; }

        public string message { get; set; }

        public string id { get; set; }
    }

    public class Comments
    {
        public commentData comments { get; set; }


        public string id { get; set; }
    }
    public class commentData
    {
        public List<comment> data { get; set; }
        public paging paging { get; set; }
    }

    public class comment
    {
        public string created_time { get; set; }
        public UserFrom from { get; set; }
        public string message { get; set; }
        public string id { get; set; }
    }

    public class UserFrom
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public  class pageAccess
    {
        public string access_token { get; set; }
        public string id { get; set; }
    }
}
