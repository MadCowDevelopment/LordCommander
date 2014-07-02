using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LordCommander.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LordCommander.Client
{
    [Export(typeof(IAuthenticationHelper))]
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private const string RegistrationUrl = "/api/Account/Register";
        private const string LogoutUrl = "/api/Account/Logout";
        private const string ChangePasswordUrl = "/api/Account/ChangePassword";

        private readonly string _baseUrl;
        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings();

        public AuthenticationHelper() : this(ServerConstants.Base)
        {
        }

        public AuthenticationHelper(string baseUrl)
        {
            _baseUrl = baseUrl;

            _serializerSettings.Converters.Add(new IsoDateTimeConverter());
        }

        public async Task Register(string email, string password, string confirmPassword)
        {
            var registerModel = new RegisterBindingModel
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            await PostAsJsonAsync<object, ModelValidationError>(RegistrationUrl, registerModel);
        }

        public Task<LoginResult> Login(string username, string password)
        {
            return Task.Factory.StartNew(() =>
            {
                var request = (HttpWebRequest) WebRequest.Create(_baseUrl + "Token");

                var postData = string.Format("grant_type=password&username={0}&password={1}", username, password);
                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.CookieContainer = new CookieContainer();

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse) request.GetResponse();
                var authCookie = response.Cookies[".AspNet.Cookies"];
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseString = reader.ReadToEnd();
                    var loginResult = JsonConvert.DeserializeObject<LoginResult>(responseString, _serializerSettings);
                    loginResult.AuthCookie = authCookie;
                    return loginResult;
                }
            });
        }

        public async void ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            var localPasswordModel = new ChangePasswordBindingModel
                {
                    OldPassword = oldPassword,
                    NewPassword = newPassword,
                    ConfirmPassword = confirmPassword
                };

            await PostAsJsonAsync<object, ModelValidationError>(ChangePasswordUrl, localPasswordModel);
        }

        private async Task<TResult> PostAsJsonAsync<TResult, TError>(string url, object content) where TError : WebError
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TResult>(responseString);
                }
                else
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var badRequest = JsonConvert.DeserializeObject<TError>(responseString, _serializerSettings);
                    throw new WebCommunicationException(badRequest);
                }
            }
        }
    }
}