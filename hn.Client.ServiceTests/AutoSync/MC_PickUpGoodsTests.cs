using Microsoft.VisualStudio.TestTools.UnitTesting;
using hn.AutoSyncLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hn.Client.Service;

namespace hn.AutoSyncLib.Tests
{
    [TestClass()]
    public class MC_PickUpGoodsTests
    {
        [TestMethod()]
        public void RequestAndWriteDataTest()
        {
            var service = new APIService();

            string rq1 = DateTime.Now.Date.ToString("yyyy-MM-dd");

            Assert.IsTrue( service.Sync_Today_THD(rq1, rq1));
        }
    }
}