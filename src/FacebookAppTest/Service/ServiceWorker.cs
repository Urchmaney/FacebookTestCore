using FacebookAppTest.ViewModel;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FacebookAppTest.Service
{


    public class ServiceWorker
    {
      
        public static async Task<string> GetFromAccessTokenUrl(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "get";
                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    var value = reader.ReadToEnd();

                    TokenModel model = JsonConvert.DeserializeObject<TokenModel>(value);


                    return model.access_token;
                }
            }
            catch (WebException e)
            {
                var ff = e.Status;
                return null;
            }

       }

       
    

    public static async Task<T> GetFromUrlClient<T>(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "get";
                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    
                    var value = reader.ReadToEnd();
                
                    T model =JsonConvert.DeserializeObject<T>(value);
                   

                    return model;
                }
            }
            catch (WebException e)
            {
                var ff = e.Status;
                return default(T);
            }

          
        }


        //public static string PostUri(string uri,string token,string msg)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
                
        //        var reqparm = new System.Collections.Specialized.NameValueCollection();
        //        reqparm.Add("param1", "<any> kinds & of = ? strings");
        //        reqparm.Add("param2", "escaping is already handled");
        //        byte[] responsebytes = client.UploadValues("http://localhost", "POST", reqparm);
        //        string responsebody = Encoding.UTF8.GetString(responsebytes);
        //    }
        //}

        public static async Task<T> PostToUrlClient<T>(string url,string token,string msg)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                
                //string postData = "message=" + msg + "access_token=" + token;
                request.Method = "POST";
                request.ContentType = "application/json";
              
                
                using (var streamWriter = new StreamWriter(await request.GetRequestStreamAsync()))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(new { message = msg, access_token = token }));
                }
               
                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    var value = reader.ReadToEnd();

                    T model = JsonConvert.DeserializeObject<T>(value);


                    return model;
                }
            }
            catch (WebException e)
            {
                var ff = e.Status;
                return default(T);
            }


        }
    }
}

