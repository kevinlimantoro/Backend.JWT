using System;

namespace Backend.SDK
{
    public class BackendSDKModel
    {
        private string _PermissionFunctionID;
        /// <summary>
        /// Permission ID / FUnctionID
        /// </summary>
        public string PermissionFunctionID
        {
            get { return _PermissionFunctionID; }
            set { _PermissionFunctionID = value; }
        }

        private bool _IsValid;
        /// <summary>
        /// Value will be set after calling Init() to validate token and permission
        /// </summary>
        public bool IsValid
        {
            get { return _IsValid; }
            set { _IsValid = value; }
        }

        private string _AccountID;
        /// <summary>
        /// Backend_Account AccountID
        /// </summary>
        public string AccountID
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }

        private int _BrandID;
        /// <summary>
        /// Backend Brand ID 2-7, 998, 999
        /// </summary>
        public int BrandID
        {
            get { return _BrandID; }
            set { _BrandID = value; }
        }

        private string _CurrencyID;
        /// <summary>
        /// Currency ID
        /// </summary>
        public string CurrencyID
        {
            get { return _CurrencyID; }
            set { _CurrencyID = value; }
        }

        private string _LanguageCode;
        /// <summary>
        /// en-us / zh-tw
        /// </summary>
        public string LanguageCode
        {
            get { return _LanguageCode; }
            set { _LanguageCode = value; }
        }

        private APIResult apiResult;
        /// <summary>
        /// In case of IsValid is false, check the error here
        /// </summary>
        public APIResult APIResult
        {
            get { return apiResult; }
            set { apiResult = value; }
        }

    }

    [Serializable]
    public class APIResult
    {
        public int Result { get; set; }

        public string Outstring { get; set; }

        public string Memo { get; set; }

        public APIResult()
        {
            Result = 1;
            Outstring = string.Empty;
        }

        public APIResult(int result, string outString)
        {
            Result = result;
            Outstring = outString;
        }
        public APIResult(int? result, string outString)
        {
            Result = (int)result;
            Outstring = outString;
        }

        public APIResult(int result, string outString, string memo)
        {
            Result = result;
            Outstring = outString;
            Memo = memo;
        }

        public APIResult(int? result, string outString, string memo)
        {
            Result = (int)result;
            Outstring = outString;
            Memo = memo;
        }
    }

}
