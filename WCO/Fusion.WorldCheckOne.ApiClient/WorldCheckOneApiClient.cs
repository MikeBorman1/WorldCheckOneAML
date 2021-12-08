using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;
using RestSharp;

namespace Fusion.WorldCheckOne.ApiClient
{
    /// <summary>
    /// 
    /// </summary>
    public class WorldCheckOneApiClient
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.Name);

        /// <summary>
        /// 
        /// </summary>
        private const string EndpointHostLive = "rms-world-check-one-api.thomsonreuters.com";

        /// <summary>
        /// 
        /// </summary>
        private const string EndpointHostTest = "rms-world-check-one-api-pilot.thomsonreuters.com";

        /// <summary>
        /// 
        /// </summary>
        private const string ApiVersion = "v1";

        /// <summary>
        /// 
        /// </summary>
        public string ApiKey { get; }

        /// <summary>
        /// 
        /// </summary>
        private readonly string mApiSecret;

        /// <summary>
        /// 
        /// </summary>
        public bool LiveMode { get; }

        /// <summary>
        /// 
        /// </summary>
        public string EndpointHost { get; }

        /// <summary>
        /// 
        /// </summary>
        public string CacheDirectory { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public bool UseCache { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="apiSecret"></param>
        /// <param name="live"></param>
        /// <param name="cacheDirectory"></param>
        public WorldCheckOneApiClient(string apiKey, string apiSecret, bool live = false, string cacheDirectory = "")
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentException($"{nameof(apiKey)} cannot be empty");
            }
            //
            if (string.IsNullOrEmpty(apiSecret))
            {
                throw new ArgumentException($"{nameof(apiSecret)} cannot be empty");
            }
            //
            if (!string.IsNullOrEmpty(cacheDirectory))
            {
                if (!Directory.Exists(cacheDirectory))
                {
                    Directory.CreateDirectory(cacheDirectory);
                }
            }
            //
            ApiKey = apiKey;
            mApiSecret = apiSecret;
            LiveMode = live;
            EndpointHost = LiveMode ? EndpointHostLive : EndpointHostTest;
            CacheDirectory = cacheDirectory;
            UseCache = !string.IsNullOrEmpty(cacheDirectory);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string MakeGetRequest(string path)
        {
            var responseString = string.Empty;
            //
            var cacheKey = path.Replace("/", "_");
            var cachePath = Path.Combine(CacheDirectory, $@"{cacheKey}.json");
            //
            if (UseCache)
            {
                try
                {

                    if (File.Exists(cachePath))
                    {
                        responseString = File.ReadAllText(cachePath);
                    }
                    //
                    if (responseString != string.Empty)
                    {
                        return responseString;
                    }
                }
                catch (Exception e)
                {
                    Log.Error("", e);
                }
            }
            //
            Thread.Sleep(500);
            //
            try
            {
                path = path.StartsWith("/") ? path.Substring(1) : path;
                //
                var fullPath = $"/{ApiVersion}/{path}";
                var fullUri = $"https://{EndpointHost}{fullPath}";
                //
                var currentDate = DateTime.Now;
                var currentDateString = currentDate.AddHours(-1).ToString("R");
                //
                var dataToSign = $"(request-target): get {fullPath}\n" +
                                 $"host: {EndpointHost}\n" +
                                 $"date: {currentDateString}";
                //
                var hmac = CreateHmac(dataToSign, mApiSecret);
                //
                var authorisation = $"Signature keyId=\"{ApiKey}\",algorithm=\"hmac-sha256\",headers=\"(request-target) host date";
                //
                authorisation = authorisation + $"\",signature=\"{hmac}\"";
                //
                var httpClient = new HttpClient();
                //
                httpClient.DefaultRequestHeaders.Date = currentDate;
                httpClient.DefaultRequestHeaders.Add(HttpRequestHeader.Authorization.ToString(), authorisation);
                //
                try
                {
                    responseString = httpClient.GetStringAsync(fullUri).Result;
                    //
                    if (UseCache)
                    {
                        try
                        {
                            if (responseString != string.Empty)
                            {
                                File.WriteAllText(cachePath, responseString);
                            }
                        }
                        catch (Exception e)
                        {
                            Log.Error("", e);
                        }
                    }
                }
                catch (Exception e)
                {
                    if (e is AggregateException aggregateException)
                    {
                        Log.Error($"{aggregateException.InnerExceptions.Count} Exceptions:");
                        //
                        foreach (var aggregateExceptionInnerException in aggregateException.InnerExceptions)
                        {
                            Log.Error("Exception", aggregateExceptionInnerException);
                        }
                    }
                    else
                    {
                        Log.Error("Exception", e);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("", e);
            }
            //
            return responseString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="body"></param>
        /// <param name="expectedResponseStatusCode"></param>
        /// <returns></returns>
        private string MakePostRequest(string path, string body, HttpStatusCode expectedResponseStatusCode = HttpStatusCode.Created)
        {
            var responseString = string.Empty;
            //body = "a";
            //
            try
            {
                path = path.StartsWith("/") ? path.Substring(1) : path;
                //
                var fullPath = $"/{ApiVersion}/{path}";
                var fullUri = $"https://{EndpointHost}{fullPath}";
                //
                var currentDate = DateTime.Now;
                var currentDateString = currentDate.AddHours(-1).ToString("R");
                //var currentDateString = "Thu, 27 Jul 2017 13:53:18 GMT";
                //
                var contentType = "application/json";
                var content = body;
                var contentLength = content.Length;
                //
                var dataToSign = $"(request-target): post {fullPath}\n" +
                                 $"host: {EndpointHost}\n" +
                                 $"date: {currentDateString}\n" +
                                 $"content-type: {contentType}\n" +
                                 $"content-length: {contentLength}\n" +
                                 content;
                //
                var hmac = CreateHmac(dataToSign, mApiSecret);
                //
                var authorisation = $"Signature keyId=\"{ApiKey}\",algorithm=\"hmac-sha256\",headers=\"(request-target) host date content-type content-length\",signature=\"{hmac}\"";
                //
                var client = new RestClient(fullUri);
                //
                var request = new RestRequest(Method.POST);
                //
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("content-length", contentLength.ToString());
                request.AddHeader("date", currentDateString);
                request.AddHeader("authorization", authorisation);
                //
                request.AddParameter(contentType, content, ParameterType.RequestBody);
                //
                try
                {
                    var r = client.Execute(request);
                    //
                    if (r.StatusCode == expectedResponseStatusCode)
                    {
                        responseString = r.Content;
                    }
                    else
                    {
                        Console.WriteLine(r.Content);
                    }
                }
                catch (Exception e)
                {
                    if (e is AggregateException aggregateException)
                    {
                        Log.Error($"{aggregateException.InnerExceptions.Count} Exceptions:");
                        //
                        foreach (var aggregateExceptionInnerException in aggregateException.InnerExceptions)
                        {
                            Log.Error("Exception", aggregateExceptionInnerException);
                        }
                    }
                    else
                    {
                        Log.Error("Exception", e);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("", e);
            }
            //
            return responseString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataToSign"></param>
        /// <param name="apiSecret"></param>
        /// <returns></returns>
        private string CreateHmac(string dataToSign, string apiSecret)
        {
            var hmac = new HMACSHA256
            {
                Key = Encoding.UTF8.GetBytes(apiSecret)
            };
            var dataBytes = Encoding.UTF8.GetBytes(dataToSign);
            var computedHash = hmac.ComputeHash(dataBytes);
            var computedHashString = Convert.ToBase64String(computedHash);
            //
            return computedHashString;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        private T MakeGetRequest<T>(string path)
        {
            var json = MakeGetRequest(path);
            var obj = JsonConvert.DeserializeObject<T>(json);
            
            return obj;
        }

        private T MakePostRequest<T>(string path, object body)
        {
            var bodyJson = JsonConvert.SerializeObject(body);
            var json = MakePostRequest(path, bodyJson);
            var obj = JsonConvert.DeserializeObject<T>(json);
            
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WcoGroup> GetGroups()
        {
            return MakeGetRequest<List<WcoGroup>>("groups") ?? new List<WcoGroup>();
        }

        public List<dynamic> GetGroups2()
        {
            return MakeGetRequest<List<dynamic>>("groups") ?? new List<dynamic>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetReferenceCountries()
        {
            return MakeGetRequest<Dictionary<string, string>>("reference/countries") ?? new Dictionary<string, string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WcoProvider> GetReferenceProvider()
        {
            return MakeGetRequest<List<WcoProvider>>("reference/providers") ?? new List<WcoProvider>();
        }

        public WcoProfile GetProfile(string worldCheckProfileId)
        {
            if (worldCheckProfileId == string.Empty)
            {
                throw new Exception("No world check id specified!");
            }
            //
            return MakeGetRequest<WcoProfile>($"reference/profile/{worldCheckProfileId}");
        }
        public dynamic GetProfile2(string worldCheckProfileId)
        {
            return MakeGetRequest<dynamic>($"reference/profile/{worldCheckProfileId}");
        }

        public WcoProfile GetProfileFromCache(string worldCheckProfileId)
        {
            var cacheFile = Path.Combine(CacheDirectory, $"reference_profile_{worldCheckProfileId}.json");
            //
            if (File.Exists(cacheFile))
            {
                var json = File.ReadAllText(cacheFile);
                //
                return JsonConvert.DeserializeObject<WcoProfile>(json);
            }
            //
            return null; //MakeGetRequest<WcoProfile>($"reference/profile/{worldCheckProfileId}");
        }

        public WcoScreeningResult DoScreening(string groupId, string entityType, string name, string gender = "", string dateOfBirth = "", string country = "")
        {
            var wcoScreeningResult = new WcoScreeningResult();
            //
            var wcoCaseRequest = new WcoCaseRequest(groupId, entityType, name, gender, dateOfBirth, country);
            //
            var makePostRequest = MakePostRequest<WcoRequestScreeningResult>("cases", wcoCaseRequest);
            //
            return wcoScreeningResult;
        }

        
    }
}
