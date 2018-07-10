using System;

namespace Backend.SDK
{
    public class BackendSDKModel
    {
        private string _PermissionFunctionID;

        public string PermissionFunctionID
        {
            get { return _PermissionFunctionID; }
            set { _PermissionFunctionID = value; }
        }

        private bool _IsValid;

        public bool IsValid
        {
            get { return _IsValid; }
            set { _IsValid = value; }
        }

        private string _AccountID;

        public string AccountID
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }

        private int _BrandID;

        public int BrandID
        {
            get { return _BrandID; }
            set { _BrandID = value; }
        }

        private string _CurrencyID;

        public string CurrencyID
        {
            get { return _CurrencyID; }
            set { _CurrencyID = value; }
        }

        private string _LanguageCode;

        public string LanguageCode
        {
            get { return _LanguageCode; }
            set { _LanguageCode = value; }
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
