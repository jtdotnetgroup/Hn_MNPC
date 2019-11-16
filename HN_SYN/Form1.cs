using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HN_SYN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                WebAPI.APIServiceClient api = new WebAPI.APIServiceClient();
                string strStartDate = api.GetStartDate_syn();
                strStartDate = "2019-2-6";
                DateTime dtStartTime = DateTime.Parse(strStartDate);

                TimeSpan iTimeSpan = DateTime.Now - dtStartTime;



                for (int i = 0; i <= iTimeSpan.Days; i++)
                {

                    try
                    {
                        DateTime theTime = dtStartTime.AddDays(i);
                        result = api.GeICPO_BOLListMN_syn(theTime);
                        backgroundWorker1.ReportProgress(0, result);
                    }
                    catch (Exception ee)
                    {
                        result = ee.ToStr();
                        backgroundWorker1.ReportProgress(0, result);
                    }

                }

            }
            catch (Exception ee)
            {
                backgroundWorker1.ReportProgress(0, ee.ToString());
            }
            /*
            DateTime theTime = DateTime.Now;
            MApiModel.recToken.Rootobject g_token = new MApiModel.recToken.Rootobject();
            string rq1 = theTime.Year + "/" + (theTime.Month < 10 ? "0" + theTime.Month.ToStr() : theTime.Month.ToStr() + "/" + (theTime.Day < 10 ? "0" + theTime.Day.ToStr() : theTime.ToStr()));

            MApiModel.recApi8.Rootobject recapi8 = new MApiModel.recApi8.Rootobject();
            MApiAccess.AccessApi Mapi = new MApiAccess.AccessApi();
            if (g_token == null || g_token.tokenInfo == null || string.IsNullOrEmpty(g_token.tokenInfo.endDate) || DateTime.Parse(g_token.tokenInfo.endDate) < DateTime.Now)
            {
                MApiModel.api1.Rootobject tempToken = new MApiModel.api1.Rootobject();
                tempToken.comid = 101;
                tempToken.action = "getToken";
                tempToken.khhm = "300384";
                tempToken.openkey = hn.Common.StringHelper.MD5string("10011630");
                g_token = Mapi.AccessApi1(tempToken);
            }


            MApiModel.api8.Rootobject getapi8 = new MApiModel.api8.Rootobject();
            getapi8.action = "getMN_cp_13";
            getapi8.token = g_token.token;
            getapi8.khhm = "300384";
            getapi8.comid = 101;
            getapi8.pageSize = 1000;
            getapi8.pageIndex = 1;
            getapi8.rq1 = rq1;
            getapi8.rq2 = rq1;
            string strParam = MApiAccess.Helper.getProperties<MApiModel.api8.Rootobject>(getapi8);
            recapi8 = Mapi.AccessApi8(getapi8);
            */
        }

        string result = "";
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            textBox1.AppendText("starting...\r\n");
            button1.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            textBox1.AppendText("同步结果"+result+ "\r\n");
            textBox1.AppendText("end!\r\n十分钟同步一次\r\n");
            i = 0;
            button1.Enabled = true;
            timer1.Start();


        }

        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            i++;
            textBox1.AppendText(i+".");
            if (i >= 600)
            {
                textBox1.AppendText("\r\n");
                button1_Click(null,null);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                textBox1.AppendText(e.UserState.ToString()+"\r\n");
            }
        }
    }
}
