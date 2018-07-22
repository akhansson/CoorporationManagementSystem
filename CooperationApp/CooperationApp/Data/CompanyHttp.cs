using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace CooperationApp.Data
{
    class CompanyHttp
    {
        private int _expiresIn = 0;
        private DateTime _expiresInDate;
        private string _accessToken;

        /// <summary>
        /// Check if an company exist
        /// </summary>
        /// <param name="name">name of the company</param>
        /// <returns>1 if the company exist, -1 if an error occured, 0 if the company dosent exist</returns>
        public async Task<int> CheckIfCompanyExist(string name)
        {
            /*
             * Get a new access token if it has expired or we dont have any
             */ 
            if(this._expiresIn == 0 || DateTime.Now > this._expiresInDate)
            {
               var result = await GetAccessToken();

               if (result == null)
                    return -1;
            }

            var client = new RestClient("https://api.roaring.io/no/company/simple-search/1.0/search?name=" + System.Web.HttpUtility.UrlEncode(name));
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("authorization", "Bearer " + _accessToken);
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials", ParameterType.RequestBody);
            

            var cancellationTokenSource = new CancellationTokenSource();
            var restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);

            if (restResponse.IsSuccessful)
            {
                try
                {
                    var hitcount = (int)JObject.Parse(restResponse.Content)["hitCount"];
                    if (hitcount > 0)
                        return 1;
                    else
                        return 0;
                }
                catch
                {
                    return -1;
                }
            }


            return -1;
        }

        private async Task<JObject> GetAccessToken()
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("PjzlnjJw_vw7x0KShAr77RZo3Q4a:0vb7ioBFhU4Dk8WkJMMwxKOR3p8a");
            var toBase64 = System.Convert.ToBase64String(plainTextBytes);

            var client = new RestClient("https://api.roaring.io/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("authorization", "Basic " + toBase64);
            request.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials", ParameterType.RequestBody);

            var cancellationTokenSource = new CancellationTokenSource();
            var restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);

            if(restResponse.IsSuccessful)
            {   
                try
                {
                    var content = JObject.Parse(restResponse.Content);

                    this._expiresIn = (int)content["expires_in"];
                    this._accessToken = (string)content["access_token"];
                    this._expiresInDate = DateTime.Now.AddSeconds(_expiresIn);

                    return content;
                }
                catch(Exception)
                {
                    return null;
                }
           
            }

            return null;

        }
    }

    


}
