using hn.Common;
using hn.DataAccess.Dal;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using WindowsService;


namespace hn.TimerService
{
    public partial class Service1 : ServiceBase
    {
        private APIService.APIServiceClient _service;
        private string _token;
        private DateTime _lastRunTime1;
        private bool _run = true;
        private int _time = 60000;
        private int _startTime = 800;
        private int _endTime = 2200;
        private int _syncinventorytime = 900;

        Thread _thread1 = null;
        Thread _thread2 = null;
        public Service1()
        {
            InitializeComponent();
        }


        protected override void OnStart(string[] args)
        {
            _service = new APIService.APIServiceClient();
            _token = ConfigurationManager.AppSettings["token"];
            _run = true;
            _time = Convert.ToInt32(ConfigurationManager.AppSettings["timer"]);

            _startTime = Convert.ToInt32(ConfigurationManager.AppSettings["starttime"].Replace(":", ""));
            _endTime = Convert.ToInt32(ConfigurationManager.AppSettings["endtime"].Replace(":", ""));

            _syncinventorytime = Convert.ToInt32(ConfigurationManager.AppSettings["syncinventorytime"].Replace(":", "")+"00");


            //在系统事件查看器里的应用程序事件里来源的描述
            EventLog.WriteEntry("厂家同步服务启动");

            _thread1 = new Thread(RunThread);
            _thread1.Start();

            _thread2 = new Thread(RunThread2);
            _thread2.Start();
        }
        protected override void OnStop()
        {
            _run = false;
            EventLog.WriteEntry("厂家同步服务停止");
        }

        private void RunThread()
        {

            while (_run)
            {
                TimeSpan timeSpan = DateTime.Now - _lastRunTime1;

                //每一分钟执行一次轮值检查
                if (timeSpan.TotalMilliseconds >= _time)
                {
                    int hhmm = Convert.ToInt32(DateTime.Now.ToString("HHmm"));
                    //LogHelper.WriteLog("time=" + hhmm);
                    if (_startTime <= hhmm && _endTime >= hhmm)
                    {
                        try
                        {
                            this.onUptICSEOUT();
                            Thread.Sleep(2000);
                            this.onUptICSEOUT2();
                            Thread.Sleep(2000);
                            this.onUptICSEOUT3();
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteLog(typeof(Service1), ex);
                        }                       
                        this._lastRunTime1 = DateTime.Now;
                    }
                }
                else
                {
                    Thread.Sleep(_time);
                }
            }
        }

        private void RunThread2()
        {

            while (_run)
            {
                int hhmm = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                //判断每天九点
                if (hhmm == _syncinventorytime)
                {
                    LogHelper.WriteLog("同步时间："+_syncinventorytime);
                    onSyncToService();

                }
            }
        }

        private void onUptICSEOUT()
        {
            try
            {
                DataTable table = _service.GetICSEOUTUpdateData();
                foreach (DataRow row in table.Rows)
                {
                    int syncstatus = PublicMethod.GetInt(row["FSYNCSTATUS"]);
                    if (syncstatus == 2) //2：厂家更新成功
                    {
                        var icsoutlist = V_ICSEOUTBILLDAL.Instance.GetWhere(new { FBILLNO = row["FBILLNO"] }).ToList();
                        if (icsoutlist.Count > 0)
                        {
                            ICSEOUTBILLENTRYDAL.Instance.UpdateWhatWhere(
                                   new
                                   {
                                       //FCOMMITQTY = row["FCOMMITQTY"],
                                       //FBATCHNO = row["FBATCHNO"],
                                       //FCOLORNO = row["FCOLORNO"],
                                       //FLEVEL = row["FGRADE"],
                                       //FINFO_RE_STATUS = row["Finfo_RE_status"],
                                       //FINFO_RE_QTY = row["Finfo_RE_qty"],
                                       FERR_MESSAGE = row["FERR_MESSAGE"]
                                   },
                                   new { FENTRYID = row["FENTRYID"], FICSEOUTID = icsoutlist[0].FID });

                            ICSEOUTBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = 5, FSYNCSTATUS = 2, FSRCBILLNO = row["FSRCBillNo"] }, new { FID = icsoutlist[0].FID });
                        }

                        //将同步状态修改为3：华耐同步成功
                        _service.UpdateCSEOUTSyncStatus(PublicMethod.GetInt(row["FID"]), 3);

                    }
                    else if (syncstatus == -1)  //1：厂家同步成功； -1: 数据检查不通过
                    {

                        var icsoutlist = V_ICSEOUTBILLDAL.Instance.GetWhere(new { FBILLNO = row["FBILLNO"] }).ToList();
                        if (icsoutlist.Count > 0)
                        {
                            ICSEOUTBILLENTRYDAL.Instance.UpdateWhatWhere(
                                   new
                                   {
                                       FERR_MESSAGE = row["FERR_MESSAGE"]
                                   },
                                   new { FENTRYID = row["FENTRYID"], FICSEOUTID = icsoutlist[0].FID });

                            int count = ICSEOUTBILLDAL.Instance.UpdateWhatWhere(
                                new { FFACTORYSTATUS = row["FSTATUS"], FSYNCSTATUS = -1 }, new { FID = icsoutlist[0].FID });
                            LogHelper.WriteLog("FID=" + icsoutlist[0].FID + ",FSYNCSTATUS=" + row["FSYNCSTATUS"]);
                            if (count > 0)
                            {
                                //将同步状态修改为-3：错误处理成功
                                _service.UpdateCSEOUTSyncStatus(PublicMethod.GetInt(row["FID"]), -3);
                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
        }

        private void onUptICSEOUT2()
        {
            try
            {
                DataTable table = _service.GetICSEOUTUpdateData2();
                foreach (DataRow row in table.Rows)
                {
                    int syncstatus = PublicMethod.GetInt(row["FSYNCSTATUS"]);
                    if (syncstatus == 2) //2：厂家更新成功
                    {
                        var icsoutlist = V_ICSEOUTBILLDAL.Instance.GetWhere(new { FBILLNO = row["FBILLNO"] }).ToList();
                        if (icsoutlist.Count > 0)
                        {
                            ICSEOUTBILLENTRYDAL.Instance.UpdateWhatWhere(
                                   new
                                   {
                                       //FCOMMITQTY = row["FCOMMITQTY"],
                                       //FBATCHNO = row["FBATCHNO"],
                                       //FCOLORNO = row["FCOLORNO"],
                                       //FLEVEL = row["FGRADE"],
                                       //FPRICE = row["FPRICE"],
                                       //FAMOUNT = row["FAMOUNT"],
                                       FERR_MESSAGE = row["FERR_MESSAGE"]
                                   },
                                   new { FENTRYID = row["FENTRYID"], FICSEOUTID = icsoutlist[0].FID });

                            ICSEOUTBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = 5, FSYNCSTATUS = 2, FSRCBILLNO = row["codetg"] }, new { FID = icsoutlist[0].FID });
                        }

                        //将同步状态修改为3：华耐同步成功
                        _service.UpdateCSEOUTSyncStatus2(PublicMethod.GetInt(row["FID"]), 3);

                    }
                    else if (syncstatus == -1)  //1：厂家同步成功； -1: 数据检查不通过
                    {

                        var icsoutlist = V_ICSEOUTBILLDAL.Instance.GetWhere(new { FBILLNO = row["FBILLNO"] }).ToList();
                        if (icsoutlist.Count > 0)
                        {
                            ICSEOUTBILLENTRYDAL.Instance.UpdateWhatWhere(
                                   new
                                   {
                                       FERR_MESSAGE = row["FERR_MESSAGE"]
                                   },
                                   new { FENTRYID = row["FENTRYID"], FICSEOUTID = icsoutlist[0].FID });

                            int count = ICSEOUTBILLDAL.Instance.UpdateWhatWhere(
                                new { FSYNCSTATUS = -1 }, new { FID = icsoutlist[0].FID });
                            LogHelper.WriteLog("FID=" + icsoutlist[0].FID + ",FSYNCSTATUS=" + row["FSYNCSTATUS"]);
                            if (count > 0)
                            {
                                //将同步状态修改为-3：错误处理成功
                                _service.UpdateCSEOUTSyncStatus2(PublicMethod.GetInt(row["FID"]), -3);
                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
        }

        private void onUptICSEOUT3()
        {
            try
            {
                DataTable table = _service.GetFinfo_RE_id0();
                foreach (DataRow row in table.Rows)
                {
                    var icsoutlist = V_ICSEOUTBILLDAL.Instance.GetWhere(new { FBILLNO = row["FBILLNO"] }).ToList();
                    if (icsoutlist.Count > 0)
                    {
                        ICSEOUTBILLENTRYDAL.Instance.UpdateWhatWhere(
                               new
                               {
                                   FINFO_RE_STATUS = row["Finfo_RE_status"],
                                   FINFO_RE_QTY = row["Finfo_RE_qty"]
                               },
                               new { FENTRYID = row["FENTRYID"], FICSEOUTID = icsoutlist[0].FID });
                    }

                    //将同步状态修改为3：华耐同步成功
                    _service.UpdateFinfo_RE_id(PublicMethod.GetInt(row["FID"]));
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
        }

        private void onSyncToService()
        {
            try
            {
                LogHelper.WriteLog("开始获取数据");
                var salesOrderDatas = SALES_ORDER_DATADAL.Instance.GetAll().ToList().ToArray();
                var salesoutDatas = SALESOUT_DATADAL.Instance.GetAll().ToList().ToArray();
                var inventorys = TP_INVENTORYDAL.Instance.GetAll().ToList().ToArray();

                _service.InventorySync(salesOrderDatas, salesoutDatas, inventorys);
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
        }

        public double ConvertDateTimeInt(System.DateTime time)
        {
            double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (time - startTime).TotalSeconds;
            return intResult;
        }


        private void onUptTMP_STOCKBill()
        {
            DataTable table = _service.GetTMP_STOCKBill();
            string billno = "";
            string billid = "";
            foreach (DataRow row in table.Rows)
            {
                if (billno != row["FBILLNO"].ToString())
                {
                    ICSTOCKBILLMODEL model = new ICSTOCKBILLMODEL();
                    model.FACCOUNT = PublicMethod.GetString(row["FACCOUNT"]);
                    model.FBILLNO = PublicMethod.GetString(row["FBILLNO"]);
                    model.FSYNCSTATUS = PublicMethod.GetInt(row["FSYNCSTATUS"]);
                    billid = ICSTOCKBILLDAL.Instance.Insert(model);
                }

                ICSTOCKBILLENTRYMODEL entryModel = new ICSTOCKBILLENTRYMODEL();
                entryModel.ICSTOCKBILLID = billid;
                entryModel.FENTRYID = PublicMethod.GetDecimal(row["FENTRYID"]);
                entryModel.FSRCCODE = PublicMethod.GetString(row["FSRCCODE"]);
                entryModel.FSRCMODEL = PublicMethod.GetString(row["FSRCMODEL"]);
                entryModel.FBATCHNO = PublicMethod.GetString(row["FBATCHNO"]);
                entryModel.FCOLORNO = PublicMethod.GetString(row["FCOLORNO"]);
                entryModel.FAUDQTY = PublicMethod.GetDecimal(row["FAUDQTY"]);
                entryModel.FBASENUMBER = PublicMethod.GetString(row["FBASENUMBER"]);
                entryModel.FSTOCKNUMBER = PublicMethod.GetString(row["FSTOCKNUMBER"]);
                entryModel.FSTOCKNAME = PublicMethod.GetString(row["FSTOCKNAME"]);
                entryModel.FSPNUMBER = PublicMethod.GetString(row["FSPNUMBER"]);
                entryModel.FSPNAME = PublicMethod.GetString(row["FSPNAMEFACCOUNT"]);
                entryModel.FREMARK = PublicMethod.GetString(row["FREMARKFACCOUNT"]);
                ICSTOCKBILLENTRYDAL.Instance.Insert(entryModel);

                billno = row["FBILLNO"].ToString();


            }
        }

    }
}

