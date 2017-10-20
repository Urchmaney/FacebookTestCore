using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacebookAppTest.ViewModel
{
    public class PostViewModel
    {
        public string Post { get; set; }

        public string PostId { get; set; }

        public List<comment> comments { get; set; }

       
    }

    public class PostListViewModel
    {
        public List<PostViewModel> postViewModel { get; set; }

        public string pageId { get; set; }
    }
}
