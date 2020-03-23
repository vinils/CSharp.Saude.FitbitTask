namespace CSharp.Saude.FitbitTask
{
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RequestToken
    {
        public static Info ENVIRONMENT_VARIABLES = Info.ENVIRONMENT_VARIABLES;
        public string ClientId { get; private set; }
        public string ClientSecret { get; private set; }

        public RequestToken(string clientId, string clientSecret)
        {
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
        }

        public class TokenResponse
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }
            public string refresh_token { get; set; }
            public string scope { get; set; }
            public string token_type { get; set; }
            public string user_id { get; set; }
        }

        public TokenResponse Token(string code)
        {
            var redirectUrl = "http://localhost:5000/fitbittasks";
            var redirectUrlEncoded = System.Web.HttpUtility.UrlEncode(redirectUrl);
            var client = new RestClient("https://api.fitbit.com/oauth2/token");
            var basicStr = ClientId + ":" + ClientSecret;
            var basicBytes = Encoding.UTF8.GetBytes(basicStr);
            var basicBase64 = Convert.ToBase64String(basicBytes);
            //var base64EncodedBytes = System.Convert.FromBase64String(basicBase64);
            //var str = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", "Basic " + basicBase64);
            request.AddParameter(
                "undefined", "clientId=" + ClientId +
                "&grant_type=authorization_code&redirect_uri=" + redirectUrlEncoded +
                "&code=" + code + "&undefined=", ParameterType.RequestBody);
            var response = client.Execute<TokenResponse>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                //when some information is wrong such as invalid code
                //when invalid code => "{\"errors\":[{\"errorType\":\"invalid_grant\",\"message\":\"Authorization code invalid: 26f2cbe7a3e74704068e2c969de28833b5faf732 Visit https://dev.fitbit.com/docs/oauth2 for more information on the Fitbit Web API authorization process.\"}],\"success\":false}"
            }

            return response.Data;
            //return new TokenResponse()
            //{
            //    access_token = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIyMkQ5UEIiLCJzdWIiOiI2SlJTQ0YiLCJpc3MiOiJGaXRiaXQiLCJ0eXAiOiJhY2Nlc3NfdG9rZW4iLCJzY29wZXMiOiJyc29jIHJzZXQgcmFjdCBybG9jIHJ3ZWkgcmhyIHJwcm8gcm51dCByc2xlIiwiZXhwIjoxNTQ3MDEyNzEwLCJpYXQiOjE1NDY5ODM5MTB9.1unlvTwZxFw6mAA_dElNjIGdURdGwf6Zs8cJLjaFz4k",
            //    expires_in = 28800,
            //    refresh_token = "338558e452136650eb70dfa2576769204761ac91904663844c95ecf9fcad2022",
            //    scope = "settings weight heartrate social location profile sleep activity nutrition",
            //    token_type = "Bearer",
            //    user_id = "6JRSCF",
            //};
        }

        public TokenResponse Refresh(string refreshToken)
        {
            var basicStr = ClientId + ":" + ClientSecret;
            var basicBytes = Encoding.UTF8.GetBytes(basicStr);
            var basicBase64 = Convert.ToBase64String(basicBytes);
            var client = new RestClient("https://api.fitbit.com/oauth2/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", "Basic " + basicBase64);
            request.AddParameter("undefined", "grant_type=refresh_token&refresh_token=" + refreshToken, ParameterType.RequestBody);

            var response = client.Execute<TokenResponse>(request);

            if (!string.IsNullOrEmpty(response.Content) && response.Content.Contains("Refresh token invalid"))
            {
                throw new Exception(response.Content);
            }

            return response.Data;
        }

        public void NewToken(string code, int experisIn)
        {
            var url = "https://www.fitbit.com/oauth2/authorize?response_type=code&client_id=" + ClientId + "&redirect_uri=http%3A%2F%2Flocalhost%3A5000%2Ffitbittasks&scope=activity%20heartrate%20location%20nutrition%20profile%20settings%20sleep%20social%20weight&expires_in=" + experisIn;
            var request = new RequestToken(ClientId, ClientSecret);
            var responseToken = request.Token(code);
            if (responseToken.access_token != null && responseToken.refresh_token != null)
            {
                ENVIRONMENT_VARIABLES.AccessToken = responseToken.access_token;
                ENVIRONMENT_VARIABLES.RefreshToken = responseToken.refresh_token;
            }
        }

        //public static void Test(string clientId, string clientSecret, int experisIn)
        //{
        //    var request = new RequestToken(clientId, clientSecret);
        //    //for apps tokens infos https://dev.fitbit.com/apps
        //    var currentFitBitAppToken = "https://dev.fitbit.com/apps/details/" + clientId;
        //    var url = "https://www.fitbit.com/oauth2/authorize?response_type=code&client_id=" + clientId + "&redirect_uri=http%3A%2F%2Flocalhost%3A5000%2Ffitbittasks&scope=activity%20heartrate%20location%20nutrition%20profile%20settings%20sleep%20social%20weight&expires_in=" + experisIn;
        //    //response of the url above with code http://localhost:5000/fitbittasks?code=80072dd3c94ffc627837c4b7a1471db327c73b29#_=_
        //    System.Diagnostics.Debugger.Break();
        //    ENVIRONMENT_VARIABLES.Code = "";
        //    var responseToken = request.Token(ENVIRONMENT_VARIABLES.Code);
        //    if(responseToken.access_token != null && responseToken.refresh_token != null)
        //    {
        //        ENVIRONMENT_VARIABLES.AccessToken = responseToken.access_token;
        //        ENVIRONMENT_VARIABLES.RefreshToken = responseToken.refresh_token;
        //    }
        //    //var refreshResponse = request.Refresh(info.RefreshToken);
        //    //ENVIRONMENT_VARIABLES.AccessToken = refreshResponse.access_token;
        //    //ENVIRONMENT_VARIABLES.RefreshToken = refreshResponse.refresh_token;
        //}
    }
}
