using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;

namespace Backend.SDK
{
    public static partial class BackendSDK
    {
        #region Private Properties
        private const string _Secret = "1qaz0okm(IJN@WSX3edc*UHB56tYg";
        private static IJwtAlgorithm _Algorithm = new HMACSHA512Algorithm();
        private static IJsonSerializer _Serializer = new CustomJSONSerializer();
        private static IBase64UrlEncoder _Encoder = new JwtBase64UrlEncoder();
        private static BackendSDKModel _BaseResult;
        #endregion

        #region Public Get Properties
        /// <summary>
        /// After call Init()
        /// GET only the user's AccountID
        /// </summary>
        public static string AccountID { get { return _BaseResult.AccountID; } }
        /// <summary>
        /// After call Init()
        /// GET only the user's BrandID
        /// </summary>
        public static int BrandID { get { return _BaseResult.BrandID; } }
        /// <summary>
        /// After call Init()
        /// GET only the user's CurrencyID
        /// </summary>
        public static string CurrencyID { get { return _BaseResult.CurrencyID; } }
        /// <summary>
        /// After call Init()
        /// GET only the user's LanguageCode
        /// </summary>
        public static string LanguageCode { get { return _BaseResult.LanguageCode; } }
        /// <summary>
        /// After call Init()
        /// GET only the PermissionID that this user try to access
        /// </summary>
        public static string PermissionID { get { return _BaseResult.PermissionFunctionID; } }
        /// <summary>
        /// After call Init()
        /// GET only the Validity of the JWT Token, and check permission of the function it try to access
        /// </summary>
        public static bool IsValid { get { return _BaseResult.IsValid; } }
        #endregion

        #region Public Validation Methods
        /// <summary>
        /// This function will automatically get your HTTPContext Request and Validate it token.
        /// Call IsValid after call this method to check Token validity and Permission
        /// </summary>
        public static void Init()
        {
            try
            {
                var parsedString = Regex.Replace(HttpContext.Current.Request.QueryString.ToString(), "&.*token", "&token");
                var token = HttpUtility.ParseQueryString(parsedString)["token"];
                if (string.IsNullOrWhiteSpace(token))
                {
                    _BaseResult = new BackendSDKModel() { IsValid = false };
                }
                else
                {
                    _BaseResult = token.DecodeToken<BackendSDKModel>();
                    _BaseResult.IsValid = APIValidate();
                }
            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// Extension to any object. It will create a JWT Token with Json of the main object that will last for ExpiryMinutes
        /// </summary>
        /// <param name="obj">Any object</param>
        /// <param name="ExpiryMinutes">How long token stays valid</param>
        /// <returns>JWT Token</returns>
        public static string GenerateToken(this object obj, int ExpiryMinutes)
        {
            var token = new JwtBuilder()
                        .WithAlgorithm(_Algorithm)
                        .WithSecret(_Secret)
                        .AddClaim("exp", DateTimeOffset.UtcNow.AddMinutes(ExpiryMinutes).ToUnixTimeSeconds())
                        .AddClaim("sub", new JsonNetSerializer().Serialize(obj))
                        .Build();
            return token;
        }

        /// <summary>
        /// Decode and Deserialize into Object of T
        /// </summary>
        /// <typeparam name="T">The object type of the JSON string</typeparam>
        /// <param name="token">JWT Token</param>
        /// <returns></returns>
        public static T DecodeToken<T>(this string token)
        {
            try
            {
                var payload = new JwtBuilder()
                            .WithSecret(_Secret)
                            .MustVerifySignature()
                            .Decode<IDictionary<string, object>>(token);
                return new JsonNetSerializer().Deserialize<T>(payload["sub"].ToString());
            }
            catch (TokenExpiredException e)
            {
                throw e;
            }
            catch (SignatureVerificationException e)
            {
                throw e;
            }
            catch (InvalidTokenPartsException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Decode and return JSON string
        /// </summary>
        /// <param name="token">JWT Token</param>
        /// <returns>JSON of the JWT token subject</returns>
        public static string DecodeToken(this string token)
        {
            try
            {
                var payload = new JwtBuilder()
                            .WithSecret(_Secret)
                            .MustVerifySignature()
                            .Decode<IDictionary<string, object>>(token);
                return payload["sub"].ToString();
            }
            catch (TokenExpiredException e)
            {
                throw e;
            }
            catch (SignatureVerificationException e)
            {
                throw e;
            }
            catch (InvalidTokenPartsException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Check validation of a JWT Token
        /// </summary>
        /// <param name="token">JWT Token</param>
        /// <returns>True/False</returns>
        public static bool ValidateToken(this string token)
        {
            try
            {
                token.DecodeToken();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion
    }
}
