using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using FacebookAppTest.ViewModel;
using FacebookAppTest.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FacebookAppTest.Controllers
{
    public class FacebookController : Controller
    {

        private readonly AppSettings _appsettings;

        public FacebookController(IOptions<AppSettings> appsettings)
        {

            _appsettings = appsettings.Value;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index(string code)
        {
            
            var absoluteUri = "http://localhost:50233/Facebook/";

            HttpContext.Session.SetString("code", code);

            string url = string.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}",
                          _appsettings.appId, absoluteUri, _appsettings.appSecret, code);
            var token = await ServiceWorker.GetFromAccessTokenUrl(url);
            HttpContext.Session.SetString("access_token",token); ;

            return RedirectToAction("FbActivity");
           
        }

        public async Task<IActionResult> FbActivity()
        {
            string url = "https://graph.facebook.com/me?fields=id,name,email&access_token=" + HttpContext.Session.GetString("access_token");
            var model = await ServiceWorker.GetFromUrlClient<FacebookUserModel>(url);
            if (HttpContext.Session.GetString("currentUser") == null)
            {
                HttpContext.Session.SetString("currentUser", model.email);
            }

            if (HttpContext.Session.GetString("currentUser") != model.email)
            {
                HttpContext.Session.SetString("currentUser", null);
                var absoluteUrl = "http://" + Request.PathBase+ "/Fb/";

                ViewBag.url = new HtmlString(string.Format("https://www.facebook.com/v2.10/dialog/oauth?client_id={0}&redirect_uri={1}",_appsettings.appId, absoluteUrl));
                return View("LogoutHelper");
            }


            return View(model);
        }
        public IActionResult GetCode()
        {
            string scope = "email,manage_pages,publish_pages,pages_show_list,pages_manage_cta";
            ViewBag.url = new HtmlString(string.Format("https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}",
                            _appsettings.appId, "http://localhost:50233/Facebook/", scope));
            //Response.Redirect("google.com");
            return View();

        }

        public async Task<IActionResult> ViewPages()
        {
            string url = string.Format("https://graph.facebook.com/v2.10/me?fields=accounts&access_token={0}", HttpContext.Session.GetString("access_token"));

            var model = await ServiceWorker.GetFromUrlClient<FacebookPages>(url);
            return View(model.accounts);
        }

        public async Task<IActionResult> Page(string id)
        {
            PostListViewModel posts = new PostListViewModel();
            posts.postViewModel = new List<PostViewModel>();
            posts.pageId = id;
            string url = string.Format("https://graph.facebook.com/v2.10/{0}?fields=feed&access_token={1}", id, HttpContext.Session.GetString("access_token"));

            var pagePosts = await ServiceWorker.GetFromUrlClient<PagePosts>(url);
           
            foreach(Post p in pagePosts.feed.data)
            {
                string commentUrl = string.Format("https://graph.facebook.com/v2.10/{0}?fields=comments&access_token={1}", p.id, HttpContext.Session.GetString("access_token"));

                var model = await ServiceWorker.GetFromUrlClient<Comments>(commentUrl);
                
                posts.postViewModel.Add(new PostViewModel { Post = p.message,PostId=p.id, comments = model.comments.data });

            }
            return View(posts);
        }

        public async Task<IActionResult> Post(string id)
        {
            string url = string.Format("https://graph.facebook.com/v2.10/{0}/comments", id);

            var model = await ServiceWorker.GetFromUrlClient<PagePosts>(url);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(string pageId,string message)
        {
            string pageTokenUrl = string.Format("https://graph.facebook.com/v2.10/{0}?fields=access_token&access_token={1}", pageId, HttpContext.Session.GetString("access_token"));
            var token= await ServiceWorker.GetFromUrlClient<pageAccess>(pageTokenUrl);
            string url = string.Format("https://graph.facebook.com/{0}/feed",pageId);
            var model = await ServiceWorker.PostToUrlClient<PagePosts>(url,token.access_token,message);
            return View("Success");

        }

        [HttpPost]
        public async Task<IActionResult> Comment(string postId ,string Comment,string pageId)
        {
            string pageTokenUrl = string.Format("https://graph.facebook.com/v2.10/{0}?fields=access_token&access_token={1}", pageId, HttpContext.Session.GetString("access_token"));
            var token = await ServiceWorker.GetFromUrlClient<pageAccess>(pageTokenUrl);
            string url = string.Format("https://graph.facebook.com/{0}/comments", postId);
            var model = await ServiceWorker.PostToUrlClient<PagePosts>(url, token.access_token, Comment);
            return View("Success");
        }
    }
}
