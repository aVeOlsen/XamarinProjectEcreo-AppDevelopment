using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XamarinEcreo.Common;
using XamarinEcreo.Models;
using Xamarin.Essentials;

namespace XamarinEcreo.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly IHttpClientFactory _factory;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthenticationService> _logger;
        string localPath;
        //private readonly ILogger<AuthenticationService> _logger;
        public AuthenticationService(IHttpClientFactory factory,
                                     IConfiguration config)
        {
            _client = new HttpClient();
            _factory = factory;
            _config = config;
            localPath=Path.Combine(FileSystem.AppDataDirectory);
        }

        public async Task<ClaimsPrincipal> SignIn(string username, string password)
        {
            //_logger.LogInformation(username, password);
            
            Uri uri = new Uri(string.Format(Constants.AuthenticationRestUrl, string.Empty));
            Dictionary<string, object> formData = new Dictionary<string, object>();
            //username= Crypto.Encrypt("Username", username);
            formData.Add("Email", username);
            password = Crypto.Encrypt("Password", password);
            formData.Add("Password", password);
            //_client = _factory.CreateClient(uri.ToString());
            StringContent content = new StringContent(JsonSerializer.Serialize(formData), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                string responseFailed = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(responseFailed);
                //File.WriteAllText(localPath, responseFailed); 
                return null;
            }

            string responseString = await response.Content.ReadAsStringAsync();
            JsonDocument jDoc = JsonDocument.Parse(responseString);
            string tokenString = jDoc.RootElement.GetProperty("token").GetString();
            ClaimsPrincipal claim = GetPrincipalFromToken(tokenString);

            string tag = "XamarinEcreo.Android";
            
            Android.Util.Log.WriteLine(Android.Util.LogPriority.Info, tag, username+": Have Logged on");
            return claim;
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            JwtSecurityTokenHandler tokenValidator = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.AuthenticationKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var parameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateLifetime = false
            };

            try
            {
                var principal = tokenValidator.ValidateToken(token, parameters, out var securityToken);

                if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    //_logger.LogError($"Token validation failed");
                    Debug.WriteLine($"Token validation failed");
                    return null;
                }

                return principal;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Token validation failed: {e.Message}");
                //_logger.LogError($"Token validation failed: {e.Message}");
                return null;
            }
        }

    }
}
