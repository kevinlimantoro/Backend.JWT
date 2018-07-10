using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Backend.SDK;
using JWT;
using System.Collections.Generic;

namespace SDK.Test
{
    [TestClass]
    public class JWTTest
    {
        [TestMethod]
        public void TestTokenEncodeDecodeSubject()
        {
            var item = GetTestModel();
            var token = item.GenerateToken(5);
            var result = token.DecodeToken<TestModel>();
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void TestValidToken()
        {
            var item = GetTestModel();
            var token = item.GenerateToken(5);
            Assert.IsTrue(token.ValidateToken());
        }


        [TestMethod]
        public void TestInvalidToken()
        {
            var item = GetTestModel();
            var token = item.GenerateToken(-100);
            Assert.IsFalse(token.ValidateToken());
        }


        [TestMethod]
        public void TestTokenEncodeDecodeSubjectFailed()
        {
            var item = GetTestModel();
            var token = item.GenerateToken(5) +"A";
            Assert.ThrowsException<SignatureVerificationException>(() => token.DecodeToken<TestModel>());
        }

        private TestModel GetTestModel()
        {
            var secondsSinceEpoch = Math.Round((new DateTime(2030,1,1) - new DateTime(1970, 1, 1)).TotalSeconds);
            var obj = new TestModel() { AccountID = "123asdfgg", BrandID = 2, exp = secondsSinceEpoch, items = new List<TestModel>() };
            obj.items.Add(new TestModel() { AccountID = "1", BrandID = 1 });
            obj.items.Add(new TestModel() { AccountID = "1", BrandID = 1 });
            return obj;
        }

        private TestModel GetExpiredTestModel()
        {
            var secondsSinceEpoch = Math.Round((DateTime.Now.AddDays(-1) - new DateTime(1970, 1, 1)).TotalSeconds);
            var obj = new TestModel() { AccountID = "123asdfgg", BrandID = 2, exp = secondsSinceEpoch, items = new List<TestModel>() };
            obj.items.Add(new TestModel() { AccountID = "1" ,BrandID = 1 });
            obj.items.Add(new TestModel() { AccountID = "1", BrandID = 1 });
            return obj;
        }
    }

    public class TestModel
    {
        public List<TestModel> items { get; set; }
        public string AccountID { get; set; }
        public int BrandID { get; set; }
        public double exp { get; set; }

        public bool Equal(TestModel comparer)
        {
            return AccountID.Equals(comparer.AccountID) && BrandID == comparer.BrandID;
        }
    }
}
