namespace APP.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    using Newtonsoft.Json;
    //using Plugin.Connectivity;
    using Xamarin.Essentials;

    public class ApiService
    {
        public async Task<Response<T>> CheckConnection<T>()
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                return new Response<T>
                {
                    checkResponse = false,
                    message = "Compruebe su conexión a internet.",
                };
            }
            //if (!CrossConnectivity.Current.IsConnected)
            //{
            //    return new Response<T>
            //    {
            //        checkResponse = false,
            //        message = "Encienda la configuración de internet.",
            //    };
            //}
            //var isReachable = await CrossConnectivity.Current.IsRemoteReachable(
            //    "google.com", 80 , 5000);
            //if (!isReachable)
            //{
            //    return new Response<T>
            //    {
            //        checkResponse = false,
            //        message = "Compruebe su conexión a internet.",
            //    };
            //}

            return new Response<T>
            {
                checkResponse = true,
                message = "Ok",
            };
        }

        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                //if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                //return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        public async Task<Response<T>> Get<T>(
            string urlBase,
            string tokenType,
            string accessToken)
        {
            try
            {
                HttpClientHandler insecureHandler = GetInsecureHandler();
                HttpClient client = new HttpClient(insecureHandler);
                client.DefaultRequestHeaders.TryAddWithoutValidation(tokenType, accessToken);
                //client.DefaultRequestHeaders.Authorization =
                //    new AuthenticationHeaderValue(tokenType, accessToken);

                var response = await client.GetAsync(urlBase);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<T>
                    {
                        checkResponse = false,
                        message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<Response<T>>(result);
                return model;
            }
            catch (Exception ex)
            {
                return new Response<T>
                {
                    checkResponse = false,
                    message = ex.Message,
                };
            }
        }

        public async Task<Response<T>> Get<T>(
           string urlBase)
        {
            try
            {
                HttpClientHandler insecureHandler = GetInsecureHandler();
                HttpClient client = new HttpClient(insecureHandler);
                Uri uri = new Uri(string.Format(urlBase, string.Empty));
                HttpResponseMessage response = await client.GetAsync(uri);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<T>
                    {
                        checkResponse = false,
                        message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<Response<T>>(result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                return model;
            }
            catch (Exception ex)
            {
                return new Response<T>
                {
                    checkResponse = false,
                    message = ex.Message,
                };
            }
        }

        public async Task<Response<T>> Post<T, Y>(
            string urlBase,
            string tokenType,
            string accessToken,
            Y model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request, Encoding.UTF8,
                    "application/json");
                HttpClientHandler insecureHandler = GetInsecureHandler();
                HttpClient client = new HttpClient(insecureHandler);
                client.DefaultRequestHeaders.TryAddWithoutValidation(tokenType, accessToken);
                //client.DefaultRequestHeaders.Authorization =
                //    new AuthenticationHeaderValue(tokenType, accessToken);
                var response = await client.PostAsync(urlBase, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response<T>>(result);
                    error.checkResponse = false;
                    return error;
                }

                var newRecord = JsonConvert.DeserializeObject<Response<T>>(result);

                return newRecord;
            }
            catch (Exception ex)
            {
                return new Response<T>
                {
                    checkResponse = false,
                    message = ex.Message,
                };
            }
        }

        public async Task<Response<T>> Post<T, Y>(
            string urlBase,
            Y model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request,
                    Encoding.UTF8,
                    "application/json");

                HttpClientHandler insecureHandler = GetInsecureHandler();
                HttpClient client = new HttpClient(insecureHandler);

                var response = await client.PostAsync(urlBase, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<T>
                    {
                        checkResponse = false,
                        message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var newRecord = JsonConvert.DeserializeObject<Response<T>>(result);

                return newRecord;
            }
            catch (Exception ex)
            {
                return new Response<T>
                {
                    checkResponse = false,
                    message = ex.Message
                };
            }
        }

        public async Task<Response<T>> Put<T>(
            string urlBase,
            string tokenType,
            string accessToken,
            T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request,
                    Encoding.UTF8, "application/json");
                HttpClientHandler insecureHandler = GetInsecureHandler();
                HttpClient client = new HttpClient(insecureHandler);
                client.DefaultRequestHeaders.TryAddWithoutValidation(tokenType, accessToken);
                //client.DefaultRequestHeaders.Authorization =
                //    new AuthenticationHeaderValue(tokenType, accessToken);
                var response = await client.PutAsync(urlBase, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response<T>>(result);
                    error.checkResponse = false;
                    return error;
                }

                var newRecord = JsonConvert.DeserializeObject<Response<T>>(result);

                return newRecord;
            }
            catch (Exception ex)
            {
                return new Response<T>
                {
                    checkResponse = false,
                    message = ex.Message,
                };
            }
        }

        public async Task<Response<T>> Delete<T>(
            string urlBase,
            string tokenType,
            string accessToken,
            T model)
        {
            try
            {
                HttpClientHandler insecureHandler = GetInsecureHandler();
                HttpClient client = new HttpClient(insecureHandler);
                client.DefaultRequestHeaders.TryAddWithoutValidation(tokenType, accessToken);
                //client.DefaultRequestHeaders.Authorization =
                //    new AuthenticationHeaderValue(tokenType, accessToken);
                var response = await client.DeleteAsync(urlBase);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response<T>>(result);
                    error.checkResponse = false;
                    return error;
                }

                return new Response<T>
                {
                    checkResponse = true,
                };
            }
            catch (Exception ex)
            {
                return new Response<T>
                {
                    checkResponse = false,
                    message = ex.Message,
                };
            }
        }
    }
}