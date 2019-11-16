using hn.Client.Core;
using hn.Common;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static hn.Client.FrmPleasePurchasePlan;

namespace hn.Client
{
    public partial class frm发货计划拆单 : FrmBase
    {
        ApiService.APIServiceClient _service;

        //从请购计划单传入时，接受数据用================
        public V_ICPRBILLMODEL 请购计划Data;
        public hn.DataAccess.Model.V_ICPRBILLENTRYMODEL[] 请购计划明细Data;
        //==============================================

        //编辑时用======================================
        public V_ICSEOUTBILLMODEL IcseoutbillModel;
        public List<V_ICSEOUTBILLENTRYMODEL> IcseoutbillEntrys;
        //==============================================

        public event EventHandler SaveAfter;



        public frm发货计划拆单()
        {
            InitializeComponent();

            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);
            initComboBox();
        }

        private void initComboBox()
        {
            //初始化品牌列表
            var listBrand = _service.GetBrandList(Global.LoginUser);
            foreach (var item in listBrand)
            {
                cbo品牌.Properties.Items.Add(item);
            }

            //初始化运输方式
            var dicList1 = _service.GetDics("106", "", false);
            foreach (var item in dicList1)
            {
                cbo运输方式.Properties.Items.Add(item);
            }

            //初始化发货方式
            var dicList2 = _service.GetDics("113", "", false);
            foreach (var item in dicList2)
            {
                cbo发货方式.Properties.Items.Add(item);
            }
            cbo发货方式.SelectedIndex = 0;

            cbo开单类型.Properties.Items.Add(new CodeValueClass("1", "普通开单"));
            cbo开单类型.Properties.Items.Add(new CodeValueClass("2", "托管库开单"));
            cbo开单类型.SelectedIndex = 0;
        }

        private void setFormData()
        {
            if (IcseoutbillModel != null)
            {
                //生成新的发货计划单号
                txt发货计划号.Text = _service.GetDismantlingNo(IcseoutbillModel.FBILLNO);

                foreach (var item in cbo品牌.Properties.Items)
                {
                    if (((TB_BrandModel)item).FID == IcseoutbillModel.FBRANDID)
                    {
                        cbo品牌.SelectedItem = item;
                        break;
                    }
                }
                sr厂家账户.Text = IcseoutbillModel.FCLIENTNAME;
                sr厂家账户.Tag = IcseoutbillModel.FCLIENTID;
                sr二级销区.Text = IcseoutbillModel.FCLASSAREA2NAME;
                sr二级销区.Tag = IcseoutbillModel.FPREMISEID;

                foreach (var item in cbo运输方式.Properties.Items)
                {
                    if (((SYS_SUBDICSMODEL)item).FID == IcseoutbillModel.FTRANSID)
                    {
                        cbo运输方式.SelectedItem = item;
                        break;
                    }
                }

                txt车牌号.Text = IcseoutbillModel.FCARNUMBER;
                txt车型载重.Text = IcseoutbillModel.FLOADCAPACITY;
                txt提货人.Text = IcseoutbillModel.FDELIVERER;
                txt提货人电话.Text = IcseoutbillModel.FDELIVERERTEL;
                txt收货方.Text = IcseoutbillModel.FRECEIVERADDR;
                txt汇总重量.Text = IcseoutbillModel.FALLWEIGHT.ToStr();
                txt汇总体积.Text = IcseoutbillModel.FALLVOLUME.ToStr();
                txt中心仓.Text = IcseoutbillModel.FCENTER_WAREHOUSE;
                txt发货地点.Text = IcseoutbillModel.FDELIVERERADDR;
                txt采购订单.Text = IcseoutbillModel.FPURCHASE_NO;
                txt描述.Text = IcseoutbillModel.FPLANDESC;
                txt工程名称.Text = IcseoutbillModel.FPROJECTNAME;
                txt其他.Text = IcseoutbillModel.FREMARK;
                txt发货日期.DateTime = IcseoutbillModel.FDELIVERDATE;
                txt销区发货要求.Text = IcseoutbillModel.FDELIVERY_REQUIRE;
                txt品牌部.Text = IcseoutbillModel.FBRAND_DEPART;
                txt结算分部号.Text = IcseoutbillModel.FSETTLE_ORG;

                foreach (var item in cbo发货方式.Properties.Items)
                {
                    if (((SYS_SUBDICSMODEL)item).FID == IcseoutbillModel.FDELIVERY_METHOD)
                    {
                        cbo发货方式.SelectedItem = item;
                        break;
                    }
                }

                sr承运公司.Text = IcseoutbillModel.FEXPRESSCOMPANYNAME;
                sr承运公司.Tag = IcseoutbillModel.FEXPRESSCOMPANYID;

                foreach (var item in cbo开单类型.Properties.Items)
                {
                    if (((CodeValueClass)item).value.ToDecimal() == IcseoutbillModel.FBILLING_TYPE)
                    {
                        cbo开单类型.SelectedItem = item;
                        break;
                    }
                }

                chk是否托管.Checked = IcseoutbillModel.FIS_CONSIGN == 1;

                gridControl发货计划明细.DataSource = IcseoutbillEntrys;
            }
          
        }

        #region ■------------------ 运行日志

        private void LogError(Exception ex)
        {
            LogHelper.WriteLog(typeof(FrmPPPImmediateSendGoods), ex);
        }

        private void LogError(string msg)
        {
            LogHelper.WriteLog(typeof(FrmPPPImmediateSendGoods), msg);
        }

        #endregion



     
        //private void pnl跑龙套1_Paint(object sender, PaintEventArgs e)
        //{
        //    try
        //    {
        //        using (Pen pen = new Pen(Color.FromArgb(165, 172, 181)))
        //        {
        //            e.Graphics.DrawLine(pen, new Point(0, 0), new Point(pnl跑龙套1.Width, 0));
        //            e.Graphics.DrawLine(pen, new Point(0, 0), new Point(0, pnl跑龙套1.Height));
        //            e.Graphics.DrawLine(pen, new Point(pnl跑龙套1.Width - 1, 0), new Point(pnl跑龙套1.Width - 1, pnl跑龙套1.Height));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError(ex);
        //    }
        //}

        //private void pnl跑龙套3_Paint(object sender, PaintEventArgs e)
        //{
        //    try
        //    {
        //        using (Pen pen = new Pen(Color.FromArgb(165, 172, 181)))
        //        {
        //            e.Graphics.DrawLine(pen, new Point(0, 0), new Point(pnl跑龙套1.Width, 0));
        //            e.Graphics.DrawLine(pen, new Point(0, pnl跑龙套1.Height - 1), new Point(pnl跑龙套1.Width, pnl跑龙套1.Height - 1));

        //            e.Graphics.DrawLine(pen, new Point(0, 0), new Point(0, pnl跑龙套1.Height));
        //            e.Graphics.DrawLine(pen, new Point(pnl跑龙套1.Width - 1, 0), new Point(pnl跑龙套1.Width - 1, pnl跑龙套1.Height));

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError(ex);
        //    }
        //}

        private void sr厂家账户_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                string brand = "";
                if (cbo品牌.SelectedItem != null)
                {
                    TB_BrandModel model = cbo品牌.SelectedItem as TB_BrandModel;
                    if (model != null)
                        brand = model.FID;
                }

                FrmQueryClientAccount frm = new FrmQueryClientAccount(brand);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    sr厂家账户.Text = frm.SelectName;
                    sr厂家账户.Tag = frm.SelectID;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void sr承运公司_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                FrmQueryExpressCompany frm = new FrmQueryExpressCompany();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    sr承运公司.Text = frm.SelectName;
                    sr承运公司.Tag = frm.SelectID;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void sr二级销区_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                FrmQueryDictionary frm = new FrmQueryDictionary("101");
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    sr二级销区.Text = frm.SelectName;
                    sr二级销区.Tag = frm.SelectID;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void FrmPPPImmediateSendGoods_Load(object sender, EventArgs e)
        {
            txt发货日期.DateTime = DateTime.Now;
            //判断是否是从请购计划界面进入的直运发货
            if (this.请购计划Data != null && 请购计划明细Data != null)
            {
                foreach (var item in cbo品牌.Properties.Items)
                {
                    if (((TB_BrandModel)item).FID == 请购计划Data.FBRANDID)
                    {
                        cbo品牌.SelectedItem = item;
                        break;
                    }
                }

                txt工程名称.Text = 请购计划Data.FPROJECTNAME;
                sr二级销区.Text = 请购计划Data.FCLASSAREA2NAME;
                sr二级销区.Tag = 请购计划Data.FCLASSAREA2;
                txt收货方.Text = 请购计划Data.FRECEIVINGADDR;
                txt描述.Text = 请购计划Data.JDE;
                txt销区发货要求.Text = 请购计划Data.FREMARK;
                txt采购订单.Text = 请购计划Data.FPURCHASE_NO;
                txt结算分部号.Text = 请购计划Data.FSETTLE_ORG;
                txt品牌部.Text = 请购计划Data.FPREMISEBRANDNAME;


                List<V_ICSEOUTBILLENTRYMODEL> list = new List<V_ICSEOUTBILLENTRYMODEL>();

                int index = 1;
                foreach (V_ICPRBILLENTRYMODEL entry in 请购计划明细Data)
                {
                    if (entry.FSTATUS == 7) //采购确认的状态
                    {

                        decimal rate = entry.FRATE;
                        string unit = "";
                        string srcname = entry.FSRCNAME;
                        string srcmodel = entry.FSRCMODEL;
                        string srccode = entry.FSRCCODE;
                        string srcid = entry.FSRCID;

                        var hnamount = entry.FCOMMITQTY.ToDecimal() * entry.FRATE.ToDecimal();
                        var commitqty = Math.Ceiling(entry.FLEFTAMOUNT / entry.FRATE);
                        list.Add(new V_ICSEOUTBILLENTRYMODEL()
                        {
                            FID = index.ToStr(),
                            FMODEL = entry.FMODEL,
                            FENTRYID = index,
                            FCATEGORYNAME = entry.FCATEGORYNAME,
                            FBRAND = entry.FPREMISEBRANDNAME,
                            FPRODUCTNAME = entry.FPRODUCTNAME,
                            FPRODUCTTYPE = entry.FPRODUCTTYPE,
                            FPRODUCTCODE = entry.FPRODUCTCODE,
                            FUNITNAME = entry.FUNITNAME,
                            FBATCHNO = entry.FBATCHNO,
                            FCOLORNO = entry.FCOLORNO,
                            FREMARK = entry.FREMARK,
                            FSRCID = entry.FSRCID,
                            FSRCNAME = entry.FSRCNAME,
                            FSRCCODE = entry.FSRCCODE,
                            FSRCMODEL = entry.FSRCMODEL,
                            FSRCUNIT = entry.FSRCUNIT,
                            FORDERUNIT = entry.FSRCUNIT,
                            FRATE = entry.FRATE,
                            FCOMMITQTY = commitqty,
                            FNEEDDATE = entry.FNEEDDATE,
                            FPRICEPOLICYENTRYID = "1",
                            FICPRID = entry.FID,
                            FWEIGHT = commitqty * entry.FWEIGHT,
                            FVOLUME = commitqty * entry.FCOMMITQTY,
                            FITEMID = entry.FITEMID,
                            ICPRBILLNO = entry.ICPRBILLNO,
                            FICPRENTRYID = entry.FID.ToDecimal(),
                            FLEVEL = "AA",
                            //FWDR = entry.FWDR,
                            FPREMISENAME = entry.FPREMISENAME,
                            FASKQTY = entry.FASKQTY,
                            FORDERUNITQTY = entry.FORDERUNITQTY,
                            FLEFTAMOUNT = entry.FLEFTAMOUNT,// - hnamount,
                            FORDERREMARK1 = entry.FORDERREMARK1,
                            FORDERREMARK2 = entry.FORDERREMARK2,
                            FFACTORYNO = entry.FFACTORYNO,
                            JDE = entry.JDE,
                            FHNAMOUNT = hnamount
                        });

                        //SRCModel srcModel = _service.GetSrcModelByItemID(entry.FITEMID);
                        //if (srcModel != null)
                        //{
                        //    rate = srcModel.FRATE;
                        //    unit = srcModel.FUNIT;
                        //    srcname = srcModel.FSRCNAME;
                        //    srcmodel = srcModel.FSRCMODEL;
                        //    srccode = srcModel.FSRCCODE;
                        //    srcid = srcModel.FID;

                        //    var temp = rate / entry.FRATE;
                        //    var hnamount = entry.FCOMMITQTY.ToDecimal() * entry.FRATE.ToDecimal();

                        //    var data1 = Math.Floor(entry.FCOMMITQTY / temp);
                        //    list.Add(new V_ICSEOUTBILLENTRYMODEL()
                        //    {
                        //        FID = index.ToStr(),
                        //        FMODEL = entry.FMODEL,
                        //        FENTRYID = index,
                        //        FCATEGORYNAME = entry.FCATEGORYNAME,
                        //        FBRAND = entry.FPREMISEBRANDNAME,
                        //        FPRODUCTNAME = entry.FPRODUCTNAME,
                        //        FPRODUCTTYPE = entry.FPRODUCTTYPE,
                        //        FPRODUCTCODE = entry.FPRODUCTCODE,
                        //        FUNITNAME = entry.FUNITNAME,
                        //        FBATCHNO = entry.FBATCHNO,
                        //        FCOLORNO = entry.FCOLORNO,
                        //        FREMARK = entry.FREMARK,
                        //        FSRCID = srcid,
                        //        FSRCNAME = srcname,
                        //        FSRCCODE = srccode,
                        //        FSRCMODEL = srcmodel,
                        //        FSRCUNIT = unit,
                        //        FORDERUNIT = entry.FSRCUNIT,
                        //        //厂家单位换算率
                        //        FRATE = rate,
                        //        //发货数量
                        //        FCOMMITQTY = data1,
                        //        FNEEDDATE = entry.FNEEDDATE,
                        //        FPRICEPOLICYENTRYID = "1",
                        //        FICPRID = entry.FID,
                        //        FWEIGHT = srcModel.FWEIGHT * data1,
                        //        FVOLUME = entry.FVOLUME.ToDecimal() * data1,
                        //        FITEMID = entry.FITEMID,
                        //        ICPRBILLNO = entry.ICPRBILLNO,
                        //        FICPRENTRYID = entry.FID.ToDecimal(),
                        //        FLEVEL = "AA",
                        //        FPREMISENAME = entry.FPREMISENAME,
                        //        FASKQTY = entry.FASKQTY,
                        //        //采购单位数量
                        //        FORDERUNITQTY = entry.FORDERUNITQTY,
                        //        FORDERUNITLEFTQTY = entry.FORDERUNITQTY,
                        //        FLEFTAMOUNT = entry.FLEFTAMOUNT,// - hnamount,
                        //        FORDERREMARK1 = entry.FORDERREMARK1,
                        //        FORDERREMARK2 = entry.FORDERREMARK2,
                        //        FFACTORYNO = entry.FFACTORYNO,
                        //        JDE = entry.JDE,
                        //        FHNAMOUNT = hnamount
                        //    });


                        //    var data2 = entry.FCOMMITQTY % temp;
                        //    if (data2 > 0)
                        //    {
                        //        index++;
                        //        list.Add(new V_ICSEOUTBILLENTRYMODEL()
                        //        {
                        //            FID = index.ToStr(),
                        //            FMODEL = entry.FMODEL,
                        //            FENTRYID = index,
                        //            FCATEGORYNAME = entry.FCATEGORYNAME,
                        //            FBRAND = entry.FPREMISEBRANDNAME,
                        //            FPRODUCTNAME = entry.FPRODUCTNAME,
                        //            FPRODUCTTYPE = entry.FPRODUCTTYPE,
                        //            FPRODUCTCODE = entry.FPRODUCTCODE,
                        //            FUNITNAME = entry.FUNITNAME,
                        //            FBATCHNO = entry.FBATCHNO,
                        //            FCOLORNO = entry.FCOLORNO,
                        //            FREMARK = entry.FREMARK,
                        //            FSRCID = entry.FSRCID,
                        //            FSRCNAME = entry.FSRCNAME,
                        //            FSRCCODE = entry.FSRCCODE,
                        //            FSRCMODEL = entry.FSRCMODEL,
                        //            FSRCUNIT = entry.FSRCUNIT,
                        //            FORDERUNIT = entry.FSRCUNIT,
                        //            FRATE = entry.FRATE,
                        //            FCOMMITQTY = data2,
                        //            FNEEDDATE = entry.FNEEDDATE,
                        //            FPRICEPOLICYENTRYID = "1",
                        //            FICPRID = entry.FID,
                        //            FWEIGHT = data2 * entry.FCOMMITQTY.ToDecimal(),
                        //            FVOLUME = data2 * entry.FCOMMITQTY.ToDecimal(),
                        //            FITEMID = entry.FITEMID,
                        //            ICPRBILLNO = entry.ICPRBILLNO,
                        //            FICPRENTRYID = entry.FID.ToDecimal(),
                        //            FLEVEL = "AA",
                        //            FPREMISENAME = entry.FPREMISENAME,
                        //            FASKQTY = entry.FASKQTY,
                        //            FORDERUNITQTY = entry.FORDERUNITQTY,
                        //            FORDERUNITLEFTQTY = entry.FORDERUNITQTY,
                        //            FLEFTAMOUNT = entry.FLEFTAMOUNT,// - hnamount,
                        //            FORDERREMARK1 = entry.FORDERREMARK1,
                        //            FORDERREMARK2 = entry.FORDERREMARK2,
                        //            FFACTORYNO = entry.FFACTORYNO,
                        //            JDE = entry.JDE,
                        //            FHNAMOUNT = hnamount
                        //        });
                        //    }
                        //}
                        //else
                        //{
                        //    var hnamount = entry.FCOMMITQTY.ToDecimal() * entry.FRATE.ToDecimal();
                        //    var commitqty = Math.Ceiling(entry.FLEFTAMOUNT / entry.FRATE);
                        //    list.Add(new V_ICSEOUTBILLENTRYMODEL()
                        //    {
                        //        FID = index.ToStr(),
                        //        FMODEL = entry.FMODEL,
                        //        FENTRYID = index,
                        //        FCATEGORYNAME = entry.FCATEGORYNAME,
                        //        FBRAND = entry.FPREMISEBRANDNAME,
                        //        FPRODUCTNAME = entry.FPRODUCTNAME,
                        //        FPRODUCTTYPE = entry.FPRODUCTTYPE,
                        //        FPRODUCTCODE = entry.FPRODUCTCODE,
                        //        FUNITNAME = entry.FUNITNAME,
                        //        FBATCHNO = entry.FBATCHNO,
                        //        FCOLORNO = entry.FCOLORNO,
                        //        FREMARK = entry.FREMARK,
                        //        FSRCID = entry.FSRCID,
                        //        FSRCNAME = entry.FSRCNAME,
                        //        FSRCCODE = entry.FSRCCODE,
                        //        FSRCMODEL = entry.FSRCMODEL,
                        //        FSRCUNIT = entry.FSRCUNIT,
                        //        FORDERUNIT = entry.FSRCUNIT,
                        //        FRATE = entry.FRATE,
                        //        FCOMMITQTY = commitqty,
                        //        FNEEDDATE = entry.FNEEDDATE,
                        //        FPRICEPOLICYENTRYID = "1",
                        //        FICPRID = entry.FID,
                        //        FWEIGHT = commitqty * entry.FWEIGHT,
                        //        FVOLUME = commitqty * entry.FCOMMITQTY,
                        //        FITEMID = entry.FITEMID,
                        //        ICPRBILLNO = entry.ICPRBILLNO,
                        //        FICPRENTRYID = entry.FID.ToDecimal(),
                        //        FLEVEL = "AA",
                        //        //FWDR = entry.FWDR,
                        //        FPREMISENAME = entry.FPREMISENAME,
                        //        FASKQTY = entry.FASKQTY,
                        //        FORDERUNITQTY = entry.FORDERUNITQTY,
                        //        FLEFTAMOUNT = entry.FLEFTAMOUNT,// - hnamount,
                        //        FORDERREMARK1 = entry.FORDERREMARK1,
                        //        FORDERREMARK2 = entry.FORDERREMARK2,
                        //        FFACTORYNO = entry.FFACTORYNO,
                        //        JDE = entry.JDE,
                        //        FHNAMOUNT = hnamount
                        //    });
                        //}

                        index++;
                    }
                }

                gridControl发货计划明细.DataSource = list;
            }
            else
            {
                gridControl发货计划明细.DataSource = new List<V_ICSEOUTBILLENTRYMODEL>();
            }

            onCalcWeightTotal();
            setFormData();

            this.WindowState = FormWindowState.Maximized;
        }

        private void btn关闭_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn行删除_Click(object sender, EventArgs e)
        {
            for (int i = gridView发货计划明细.DataRowCount - 1; i >= 0; i--)
            {
                bool b = gridView发货计划明细.GetRowCellValue(i, "FCHECK").ToBool();
                if (b)
                {
                    gridView发货计划明细.DeleteRow(i);
                }
            }
        }

        private void btn保存_Click(object sender, EventArgs e)
        {
            if (cbo品牌.SelectedItem == null)
            {
                MsgHelper.ShowError("请选择品牌/厂家！");
                return;
            }

            if (string.IsNullOrEmpty(sr厂家账户.Text))
            {
                MsgHelper.ShowError("请选择厂家账户！");
                return;
            }

            if (string.IsNullOrEmpty(sr二级销区.Text))
            {
                MsgHelper.ShowError("请选择二级销区！");
                return;
            }

            if (string.IsNullOrEmpty(txt车牌号.Text))
            {
                MsgHelper.ShowError("请输入车牌号！");
                return;
            }

            if (gridView发货计划明细.RowCount == 0)
            {
                MsgHelper.ShowError("没有发货明细，无法保存！");
                return;
            }

            ICSEOUTBILLMODEL model = new ICSEOUTBILLMODEL();

            if (IcseoutbillModel != null)
            {
                //model.FID = IcseoutbillModel.FID;
                model.FPREMISEID = IcseoutbillModel.FPREMISEID;
                model.FCLIENTID = IcseoutbillModel.FCLIENTID;
                model.FBRANDID = IcseoutbillModel.FBRANDID;
                //model.FBILLNO = IcseoutbillModel.FBILLNO;
                model.FCARNUMBER = IcseoutbillModel.FCARNUMBER;
                model.FLOADCAPACITY = IcseoutbillModel.FLOADCAPACITY;
                model.FDELIVERER = IcseoutbillModel.FDELIVERER;
                model.FDELIVERERTEL = IcseoutbillModel.FDELIVERERTEL;
                model.FDELIVERERIDNO = IcseoutbillModel.FDELIVERERIDNO;
                model.FDELIVERERADDR = IcseoutbillModel.FDELIVERERADDR;
                model.FRECEIVER = IcseoutbillModel.FRECEIVER;
                model.FRECEIVERTEL = IcseoutbillModel.FRECEIVERTEL;
                model.FRECEIVERADDR = IcseoutbillModel.FRECEIVERADDR;
                model.FALLWEIGHT = IcseoutbillModel.FALLWEIGHT;
                model.FALLVOLUME = IcseoutbillModel.FALLVOLUME;
                model.FBILLERID = IcseoutbillModel.FBILLERID;
                model.FBILLDATE = IcseoutbillModel.FBILLDATE;
                model.FSTATUS = IcseoutbillModel.FSTATUS;
                model.FCHECKERID = IcseoutbillModel.FCHECKERID;
                model.FCHECKDATE = IcseoutbillModel.FCHECKDATE;
                model.FREMARK = IcseoutbillModel.FREMARK;
                model.FTRANSTYPE = IcseoutbillModel.FTRANSTYPE;
                model.FTRANSID = IcseoutbillModel.FTRANSID;
                model.FDELIVERDATE = IcseoutbillModel.FDELIVERDATE;
                model.FFACTORYSTATUS = IcseoutbillModel.FFACTORYSTATUS;
                model.FSYNCSTATUS = IcseoutbillModel.FSYNCSTATUS;
                model.FCENTER_WAREHOUSE = IcseoutbillModel.FCENTER_WAREHOUSE;
                model.FIS_CONSIGN = IcseoutbillModel.FIS_CONSIGN;
                model.FDELIVERY_METHOD = IcseoutbillModel.FDELIVERY_METHOD;
                model.FEXPRESSCOMPANYID = IcseoutbillModel.FEXPRESSCOMPANYID;
                model.FPROJECTNAME = IcseoutbillModel.FPROJECTNAME;
                model.FPURCHASE_NO = IcseoutbillModel.FPURCHASE_NO;
                model.FPLANDESC = IcseoutbillModel.FPLANDESC;
                model.FSRCBILLNO = IcseoutbillModel.FSRCBILLNO;
                model.FBILLING_TYPE = IcseoutbillModel.FBILLING_TYPE;
                model.FGROUP_NO = IcseoutbillModel.FGROUP_NO;
                model.FBRAND_DEPART = IcseoutbillModel.FBRAND_DEPART;
            }

            model.FBILLNO = txt发货计划号.Text;
            if (string.IsNullOrEmpty(model.FGROUP_NO))
            {
                model.FGROUP_NO = model.FBILLNO;
            }
            model.FBRANDID = ((TB_BrandModel)cbo品牌.SelectedItem).FID;
            model.FCLIENTID = sr厂家账户.Tag.ToStr();
            model.FPREMISEID = sr二级销区.Tag.ToStr();
            if (cbo运输方式.SelectedItem != null)
            {
                model.FTRANSID = ((SYS_SUBDICSMODEL)cbo运输方式.SelectedItem).FID;
            }
            model.FCARNUMBER = txt车牌号.Text;
            model.FLOADCAPACITY = txt车型载重.Text;
            model.FDELIVERDATE = txt发货日期.DateTime;
            model.FDELIVERER = txt提货人.Text;
            model.FDELIVERERTEL = txt提货人电话.Text;
            model.FRECEIVERADDR = txt收货方.Text;
            model.FALLWEIGHT = txt汇总重量.Text.ToDecimal();
            model.FALLVOLUME = txt汇总体积.Text.ToDecimal();
            model.FCENTER_WAREHOUSE = txt中心仓.Text;
            if (cbo发货方式.SelectedItem != null)
            {
                model.FDELIVERY_METHOD = ((SYS_SUBDICSMODEL)cbo发货方式.SelectedItem).FID;
            }
            model.FDELIVERERADDR = txt发货地点.Text;
            model.FPURCHASE_NO = txt采购订单.Text;
            //model.FEETOTAL = txt标准运费.Text;
            model.FPLANDESC = txt描述.Text;
            model.FEXPRESSCOMPANYID = sr承运公司.Tag.ToStr();
            model.FPROJECTNAME = txt工程名称.Text;
            model.FREMARK = txt其他.Text;
            if (cbo开单类型.SelectedItem != null)
            {
                model.FBILLING_TYPE = ((CodeValueClass)cbo开单类型.SelectedItem).value.ToInt();
            }
            model.FIS_CONSIGN = chk是否托管.Checked ? 1 : 0;
            model.FBILLERID = Global.LoginUser.FID;
            model.FBILLDATE = DateTime.Now;
            model.FDELIVERY_REQUIRE = txt销区发货要求.Text;
            model.FBRAND_DEPART = txt品牌部.Text;

            List<ICSEOUTBILLENTRYMODEL> entrys = new List<ICSEOUTBILLENTRYMODEL>();
            var datasource = gridControl发货计划明细.DataSource as List<V_ICSEOUTBILLENTRYMODEL>;
            int index = 1;
            foreach (V_ICSEOUTBILLENTRYMODEL entry in datasource)
            {
                //if (model.FBILLING_TYPE == 1 && entry.FTRUSTEESHIP == "Y")
                //{
                //    MsgHelper.ShowError("普通开单，明细项目不允许有托管库开单的数据，请检查您的输入！");
                //    return;
                //}

                if (entry.FDISMANTLING.ToDecimal() > 0)
                {
                    _service.UpdateGetDeliveryEntryData(entry.FID, entry.FCOMMITQTY - entry.FDISMANTLING);

                    entrys.Add(new ICSEOUTBILLENTRYMODEL()
                    {
                        FID = entry.FID,
                        FICSEOUTID = entry.FICSEOUTID,
                        FSRCID = entry.FSRCID,
                        FPRICEPOLICYENTRYID = entry.FPRICEPOLICYENTRYID,
                        FICPRID = entry.FICPRID,
                        FENTRYID = index,
                        FCOMMITQTY = entry.FDISMANTLING,
                        FHNAMOUNT = entry.FHNAMOUNT,
                        FSTOCKNUMBER = entry.FSTOCKNUMBER,
                        FSTOCK = entry.FSTOCK,
                        FSTOCKPLACE = entry.FSTOCKPLACE,
                        FWEIGHT = entry.FWEIGHT,
                        FVOLUME = entry.FVOLUME,
                        FREMARK = entry.FREMARK,
                        FBATCHNO = entry.FBATCHNO,
                        FCOLORNO = entry.FCOLORNO,
                        FLEVEL = entry.FLEVEL,
                        FWDR = entry.FWDR,
                        FITEMID = entry.FITEMID,
                        FPRICE = entry.FPRICE,
                        FAMOUNT = entry.FAMOUNT,
                        FERR_MESSAGE = entry.FERR_MESSAGE,
                        FNEEDDATE = entry.FNEEDDATE,
                        FORDERREMARK1 = entry.FORDERREMARK1,
                        FORDERREMARK2 = entry.FORDERREMARK2,
                        FDESCRIPTION = entry.FDESCRIPTION,
                        FBRAND = entry.FBRAND,
                        FLOCATION_NO = entry.FLOCATION_NO
                    });

                    index++;
                }
            }

            _service.DeliveryBillSave(model, entrys.ToArray(), true);

            MsgHelper.ShowInformation("保存成功！");

            if (SaveAfter != null)
            {
                SaveAfter(null, null);
            }

            this.Close();

        }

        private void gridView发货计划明细_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {

            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           
        }

        private void btn新增_Click(object sender, EventArgs e)
        {
            var list = gridControl发货计划明细.DataSource as List<V_ICSEOUTBILLENTRYMODEL>;
            list.Add(new V_ICSEOUTBILLENTRYMODEL() { });

            gridControl发货计划明细.RefreshDataSource();
        }

        private void itemButton厂家代码_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gridView发货计划明细.FocusedRowHandle != -1)
            {
                var list = gridControl发货计划明细.DataSource as List<V_ICSEOUTBILLENTRYMODEL>;
                var row = list[gridView发货计划明细.GetDataSourceRowIndex(gridView发货计划明细.FocusedRowHandle)];

                FrmQuerySrc frm = new FrmQuerySrc(row.FITEMID);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    row.FSRCID = frm.SelectData.FID;
                    row.FSRCNAME = frm.SelectData.FSRCNAME;
                    row.FSRCCODE = frm.SelectData.FSRCCODE;
                    row.FSRCMODEL = frm.SelectData.FSRCMODEL;
                    row.FSRCUNIT = frm.SelectData.FUNIT;

                    //row.FHNAMOUNT = 0;

                    var askqty = row.FASKQTY;
                    if (frm.SelectData.FRATE != 0)
                    {
                        row.FCOMMITQTY = Math.Ceiling((row.FCOMMITQTY * row.FRATE) / frm.SelectData.FRATE);

                    }

                    row.FWEIGHT = row.FCOMMITQTY * frm.SelectData.FWEIGHT;
                    row.FRATE = frm.SelectData.FRATE;
                    row.FLEFTAMOUNT = askqty;

                    gridControl发货计划明细.DataSource = list;
                    gridControl发货计划明细.RefreshDataSource();

                    onCalcWeightTotal();
                }
            }
        }

        private void itemButton商品代码_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmQueryProduct frm = new FrmQueryProduct();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var list = gridControl发货计划明细.DataSource as List<V_ICSEOUTBILLENTRYMODEL>;
                var row = list[gridView发货计划明细.GetDataSourceRowIndex(gridView发货计划明细.FocusedRowHandle)];

                row.FMODEL = frm.SelectData.FMODEL;
                row.FITEMID = frm.SelectData.FID;
                row.FPRODUCTNAME = frm.SelectData.FPRODUCTNAME;
                row.FPRODUCTTYPE = frm.SelectData.FPRODUCTTYPE;
                row.FPRODUCTCODE = frm.SelectData.FPRODUCTCODE;
                row.FUNITNAME = frm.SelectData.FUNITNAME;
                row.FUNITID = frm.SelectData.FUNITID;
                row.FORDERUNIT = frm.SelectData.FORDERUNIT;
                row.FWEIGHT = frm.SelectData.FWEIGHT;
                row.FVOLUME = frm.SelectData.FVOLUME;
                row.FRATE = frm.SelectData.FRATE;
                row.FCOLORNO = row.FCOLORNO;
                row.FREMARK = row.FREMARK;
                row.FBATCHNO = row.FBATCHNO;

                gridControl发货计划明细.DataSource = list;
                gridControl发货计划明细.RefreshDataSource();

                onCalcWeightTotal();
            }

        }

     

        private void gridView发货计划明细_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle != -1)
            {
               
            }
        }

        private void onCalcWeightTotal()
        {
            var weightTotal = decimal.Zero;
            var list = gridControl发货计划明细.DataSource as List<V_ICSEOUTBILLENTRYMODEL>;
            foreach (var model in list)
            {
                weightTotal += model.FWEIGHT;
            }

            txt汇总重量.Text = Math.Ceiling(weightTotal).ToStr();
        }

        private void chk全选_CheckedChanged(object sender, EventArgs e)
        {
            var list = gridControl发货计划明细.DataSource as List<V_ICSEOUTBILLENTRYMODEL>;
            foreach (var model in list)
            {
                model.FCHECK = chk全选.Checked;
            }
            gridControl发货计划明细.DataSource = list;
            gridControl发货计划明细.RefreshDataSource();
        }

        private void btn行复制_Click(object sender, EventArgs e)
        {
            if (gridView发货计划明细.FocusedRowHandle > -1)
            {
                var list = gridControl发货计划明细.DataSource as List<V_ICSEOUTBILLENTRYMODEL>;
                var row = list[gridView发货计划明细.GetDataSourceRowIndex(gridView发货计划明细.FocusedRowHandle)];
                list.Add(new V_ICSEOUTBILLENTRYMODEL()
                {
                    FID = list.Count() + 1.ToStr(),
                    FMODEL = row.FMODEL,
                    FENTRYID = list.Count() + 1,
                    FCATEGORYNAME = row.FCATEGORYNAME,
                    FBRAND = row.FBRAND,
                    FPRODUCTNAME = row.FPRODUCTNAME,
                    FPRODUCTTYPE = row.FPRODUCTTYPE,
                    FPRODUCTCODE = row.FPRODUCTCODE,
                    FUNITNAME = row.FUNITNAME,
                    FBATCHNO = row.FBATCHNO,
                    FCOLORNO = row.FCOLORNO,
                    FREMARK = row.FREMARK,
                    FSRCID = row.FSRCID,
                    FSRCNAME = row.FSRCNAME,
                    FSRCCODE = row.FSRCCODE,
                    FSRCMODEL = row.FSRCMODEL,
                    FSRCUNIT = row.FSRCUNIT,
                    FORDERUNIT = row.FSRCUNIT,
                    FRATE = row.FRATE,
                    FCOMMITQTY = row.FCOMMITQTY,
                    FNEEDDATE = row.FNEEDDATE,
                    FPRICEPOLICYENTRYID = row.FPRICEPOLICYENTRYID,
                    FICPRID = row.FICPRID,
                    FWEIGHT = row.FWEIGHT,
                    FVOLUME = row.FVOLUME,
                    FITEMID = row.FITEMID,
                    ICPRBILLNO = row.ICPRBILLNO,
                    FICPRENTRYID = row.FICPRENTRYID,
                    FLEVEL = row.FLEVEL,
                    FPREMISENAME = row.FPREMISENAME,
                    FASKQTY = row.FASKQTY,
                    FORDERUNITQTY = row.FORDERUNITQTY,
                    FLEFTAMOUNT = 0,
                    FORDERREMARK1 = row.FORDERREMARK1,
                    FORDERREMARK2 = row.FORDERREMARK2,
                    FFACTORYNO = row.FFACTORYNO,
                    JDE = row.JDE,
                    FHNAMOUNT = row.FHNAMOUNT
                });

                gridControl发货计划明细.DataSource = list;
                gridControl发货计划明细.RefreshDataSource();

                onCalcWeightTotal();
            }

        }

     
    }
}
