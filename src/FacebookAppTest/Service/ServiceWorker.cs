using FacebookAppTest.ViewModel;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
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

        public static async Task<T> PostToUrlClient<T>(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
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

