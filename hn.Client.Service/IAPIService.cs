using hn.Core.Model;
using hn.DataAccess;
using hn.DataAccess.bll;
using hn.DataAccess.Bll;
using hn.DataAccess.model;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace hn.Client.Service
{
    [ServiceContract]
    public interface IAPIService
    {
        #region 登录接口

        /// <summary>
        /// 用户登录接口
        /// </summary>
        /// <param name="username">登录用户名</param>
        /// <param name="password">登录密码</param>
        /// <returns></returns>
        [OperationContract]
        Core.Model.User Login(string username, string password);

        /// <summary>
        /// 修改密码接口
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldpwd">旧密码</param>
        /// <param name="newpwd">新密码</param>
        /// <returns></returns>
        [OperationContract]
        string ModifyPassword(string id,string oldpwd,string newpwd);
        #endregion

        #region 基础资料
        /// <summary>
        /// 获取指定数据字典的列表数据
        /// </summary>
        /// <param name="categorycode"></param>
        /// <returns></returns>
        [OperationContract]
        List<SYS_SUBDICSMODEL> GetDics(string categorycode, string keyword, bool all = false);
        [OperationContract]
        MApiModel.recApi24.Rootobject Remote_Get24(
MApiModel.api24.Rootobject getapi24);

        [OperationContract]
        List<SYS_SUBDICSMODEL> GetDics_Area(string categorycode, string keyword, bool all = false);

        [OperationContract]
        v_thdModel getTHD(string thdbm);

        [OperationContract]
        string ZFICPOBILL(string fEntryID);

        [OperationContract]
        string ZFICPRBILLEntry(string fEntryID);

        [OperationContract]
        V_ICPOBILLMODEL getICPOBillCLientID(string thdbm);

        [OperationContract]
        decimal getLeftNum_THD(string thdbm);
        /// <summary>
        /// 获取经营场所列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<V_PREMISEModel> GetPremiseList(string keyword);

        /// <summary>
        /// 获取品牌列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<TB_BrandModel> GetBrandList(hn.Core.Model.User loginUser);

        /// <summary>
        /// 获取厂家账户列表
        /// </summary>
        /// <param name="brandid">品牌ID</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [OperationContract]
        List<V_CLIENTACCOUNTModel> GetClientAccountList(string brandid, string keyword);

        /// <summary>
        /// 获取承运公司列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [OperationContract]
        List<TB_EXPRESSCOMPANYModel> GetExpressCompanyList(string keyword);

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [OperationContract]
        List<ProductViewModel> GetProductList(hn.Core.Model.User loginUser, string keyword);

        /// <summary>
        /// 获取厂家代码列表
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        [OperationContract]
        List<SRCModel> GetSrcList(string productid, string keyword);

        /// <summary>
        /// 三级城市（省）
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<TB_EBPLModel> GetProvince();

        /// <summary>
        /// 三级城市（省）
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<TB_EBPLModel> GetCity(string provinceid);

        /// <summary>
        /// 三级城市（省）
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<TB_EBPLModel> GetDistrict(string cityid);

        [OperationContract]
        ProductViewModel getProductView(string fid);
        [OperationContract]
        SYS_SUBDICSMODEL GetSingleDics(string fid);
        /// <summary>
        /// 获取厂家发货基地列表
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        [OperationContract]
        List<TB_DELIVER_BASEModel> GetDeliverBaseList(string brandid, string keyword);

        [OperationContract]
        V_CLIENTACCOUNTModel GetClientAccountSingle(string clientid);

        [OperationContract]
        V_CLIENTACCOUNTModel GetClientByAccount(string faccount);

        #endregion

        #region 请购计划
        /// <summary>
        /// 获取请购计划列表
        /// </summary>
        /// <param name="classarea2name">销区</param>
        /// <param name="brand">品牌</param>
        /// <param name="status">状态</param>
        /// <param name="premiseid">经营场所</param>
        /// <returns></returns>
        [OperationContract]
        List<V_ICPRBILLMODEL> GetPurchasePlanList(
            hn.Core.Model.User loginUser, 
            string classarea2name, 
            string brand, 
            int status, 
            string premiseid, 
            string billno,
            string startdate,
            string enddate, bool searchclose = false);

        [OperationContract]
        List<V_ICPRBILLMODEL> GetPurchasePlanImport(
      hn.Core.Model.User loginUser,
      string strQuery);


        [OperationContract]
        List<V_ICPRBILLMODEL> GetPurchasePlanImport2(
      hn.Core.Model.User loginUser,
      string strQuery, string brandid);

        [OperationContract]
        List<V_ICPRBILLMODEL> GetPurchasePlanImport3(
    hn.Core.Model.User loginUser,hn. Common.Cls_query.P_QueryOrder pquery);

        /// <summary>
        /// 获取请购计划数据
        /// </summary>
        /// <param name="icprbillid"></param>
        /// <returns></returns>
        [OperationContract]
        V_ICPRBILLMODEL GetPurchasePlanModel(string icprbillid);

        /// <summary>
        /// 获取请购计划明细对象
        /// </summary>
        /// <param name="FID">请购计划单据ID</param>
        /// <returns></returns>
        [OperationContract]
        V_ICPRBILLENTRYMODEL GetV_ICPRBILLENTRYMODEL(string FID);
        /// <summary>
        /// 获取请购计划明细列表
        /// </summary>
        /// <param name="fpremisename"></param>
        /// <returns></returns>
        [OperationContract]
        List<V_ICPRBILLENTRYMODEL> V_ICPRBILLENTRYMODEL_List(string fpremisename);
        /// <summary>
        /// 获取请购计划明细列表
        /// </summary>
        /// <param name="icprbillid">请购计划单据ID</param>
        /// <returns></returns>
        [OperationContract]
        List<V_ICPRBILLENTRYMODEL> GetPurchasePlanEntryList(string icprbillid, string statu,bool bICPO);
        [OperationContract]
        bool Delete_ICPO(string fid);

        /// <summary>
        /// 获取请购计划明细列表（组柜用）
        /// </summary>
        /// <param name="productinfo">商品信息</param>
        /// <param name="classarea2name">销区</param>
        /// <param name="premiseid">经营场所</param>
        /// <param name="brand">品牌部</param>
        /// <param name="deliveryaddr">发货地点</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        [OperationContract]
        List<V_ICPRBILLENTRYMODEL> GetPurchasePlanEntryByDeliveryList(
            hn.Core.Model.User loginUser,
            string productinfo, 
            string classarea2name, 
            string premiseid, 
            string brand, 
            string deliveryaddr,
            string typename,
            string billno,
            int status,
            bool bHaveSL);

        /// <summary>
        /// 采购确认
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool ConfirmSave(string action, List<ICPRBILLENTRYMODEL> data, User loginUser);

        /// <summary>
        /// 关闭请购计划明细记录
        /// </summary>
        /// <param name="data"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        [OperationContract]
        bool CloseSave(List<ICPRBILLENTRYMODEL> data, User loginUser, string content);

        /// <summary>
        /// 库存查询接口
        /// </summary>
        /// <param name="productname"></param>
        /// <param name="stockname"></param>
        /// <param name="productnumber"></param>
        /// <param name="wdr"></param>
        /// <param name="batchno"></param>
        /// <param name="brand"></param>
        /// <param name="mode"></param>
        /// <param name="colorno"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetStockList(
           string productname = null,
           string stockname = null,
           string productnumber = null,
           string wdr = null,
           string batchno = null,
           string brand = null,
           string mode = null,
           string colorno = null,
           bool isfactory = true,
           bool istrust = false);

        [OperationContract]
        MApiModel.recApi2.Rootobject GetStockListMN(
     MApiModel.api2.Rootobject getapi2);

        [OperationContract]
        MApiModel.recApi2.Rootobject GetStockListMN_2(
   MApiModel.api2.Rootobject getapi2,int comid);

        /// <summary>
        /// 获取请购计划附件列表
        /// </summary>
        /// <param name="icprbillid"></param>
        /// <returns></returns>
        [OperationContract]
        List<TB_ATTACHMENTModel> GetAttachmentList(string icprbillid);

        /// <summary>
        /// 更新请购计划数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        int UpdateICPRBILL(ICPRBILLMODEL model);

        /// <summary>
        /// 刷新价格政策编号处理
        /// </summary>
        /// <param name="icprbillid"></param>
        /// <param name="entryids"></param>
        /// <returns></returns>
        [OperationContract]
        List<V_ICPRBILLENTRYMODEL> RefrshPriceNumberList(string icprbillid, List<string> entryids);

        /// <summary>
        /// 获取价格政策列表
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        [OperationContract]
        List<TB_PRICEPOLICYModel> GetPriceNumberListByItemID(string itemid);

        [OperationContract]
        List<TB_PRICEPOLICYModel> GetPriceNumberListByBrandID(string itemid);

        [OperationContract]
        void UpdateConfirmTime(string FPlanId);

        [OperationContract]
        List<v_thdModel> getTHDList(string[] thdbmList);

        #endregion

        #region 发货计划
        /// <summary>
        /// 生成单据编号
        /// </summary>
        /// <param name="billtype">单据类型</param>
        /// <returns></returns>
        [OperationContract]
        string GetNewBillNo(string billtype);

        /// <summary>
        /// 组柜计划单据保存
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entrys"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteGroupBill(string groupno);

        /// <summary>
        /// 发货计划单据保存
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entrys"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeliveryBillSave(ICSEOUTBILLMODEL model, List<ICSEOUTBILLENTRYMODEL> entrys,bool delivery = true);

        /// <summary>
        /// 更新标识内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="centent"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateIdentification(string ids, string centent);

        /// <summary>
        /// 获取发货计划列表
        /// </summary>
        /// <param name="brand">品牌</param>
        /// <param name="classarea2name">销区</param>
        /// <param name="premiseid">经营场所</param>
        /// <param name="status">状态</param>
        /// <param name="car">车辆</param>
        /// <param name="stock">仓库</param>
        /// <param name="account">厂家账户</param>
        /// <param name="expresscompany">承运公司</param>
        /// <param name="factotryno">厂家单号</param>
        /// <param name="billno">发货计划单号</param>
        /// <param name="groupno">组柜单号</param>
        /// <param name="projectname">工程名称</param>
        /// <param name="startdate">开始日期</param>
        /// <param name="enddate">结束日期</param>
        /// <param name="searchclose">是否查询已关闭的数据</param>
        /// <returns></returns>
        [OperationContract]
        List<V_ICSEOUTBILLMODEL> GetDeliveryList(
             hn.Core.Model.User loginUser,
            string brand,
            string classarea2name,
            string premiseid,
            int status,
            string car,
            string stock,
            string account,
            string expresscompany,
            string factotryno,
            string billno,
            string groupno,
            string projectname,
            string startdate,
            string enddate,
            bool searchclose = false);

        /// <summary>
        /// 获取发货计划明细列表
        /// </summary>
        /// <param name="billid">发货计划单据ID</param>
        /// <returns></returns>
        [OperationContract]
        List<V_ICSEOUTBILLENTRYMODEL> GetDeliveryEntryList(string billid);

        /// <summary>
        /// 批量删除发货计划
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [OperationContract]
        int DeleteDeliveryByIDs(string ids);

        /// <summary>
        /// 批量审核发货计划
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [OperationContract]
        int AuditDeliveryByIDs(string ids, int status, User loginUser);

        /// <summary>
        /// 批量反审核发货计划
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [OperationContract]
        int AuditAntiDeliveryByIDs(string ids);

        /// <summary>
        /// 批量上传数据到厂家库
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [OperationContract]
        string SyncDeliveryByIDs(string ids);

        [OperationContract]
      string  SyncDeliveryByIDsMN(string strJson, string fid);

        [OperationContract]
        string SynDeliveryNot(string fid);

        ///// <summary>
        ///// 获取发货计划数据
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[OperationContract]
        //V_ICSEOUTBILLMODEL GetDeliveryData(string id);

        /// <summary>
        /// 根据组柜编号获取发货计划明细列表
        /// </summary>
        /// <param name="groupno">组柜编号</param>
        /// <returns></returns>
        [OperationContract]
        List<V_ICSEOUTBILLENTRYMODEL> GetDeliveryEntryByGroupNo(string groupno);

        /// <summary>
        /// 根据组柜编号获取发货计划列表
        /// </summary>
        /// <param name="groupno">组柜编号</param>
        /// <returns></returns>
        [OperationContract]
        List<V_ICSEOUTBILLMODEL> GetDeliveryByGroupNo(string groupno);

        /// <summary>
        /// 通过商品ID获取组柜厂家代码信息
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        [OperationContract]
        SRCModel GetSrcModelByItemID(string itemid);

        /// <summary>
        /// 更新发货计划明细发货数量
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commitqty"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateGetDeliveryEntryData(string id, decimal commitqty);

        /// <summary>
        /// 获取新的拆单单号
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        [OperationContract]
        string GetDismantlingNo(string no);

        /// <summary>
        /// 更新组柜明细状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [OperationContract]
        string UpdateDeliveryGroupStatus(string ids, int status);

        /// <summary>
        /// 约车处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [OperationContract]
        string ContractCar(List<string> ids);
        #endregion


        #region 采购订单

        /// <summary>
        /// 插入采购订单
        /// </summary>
        /// <param name="ICPOBILLJson">主表Json</param>
        /// <param name="ICPOBILLENTRYListJson">副表的Json</param>
        /// <returns></returns>
        [OperationContract]
        string SaveICPOBILL(ICPOBILLMODEL ICPOBILL, List<ICPOBILLENTRYMODEL> ICPOBILLENTRYList);


        /// <summary>
        /// 获取采购订单列表
        /// </summary>
        /// <param name="classarea2name">销区</param>
        /// <param name="brand">品牌</param>
        /// <param name="status">状态</param>
        /// <param name="premiseid">经营场所</param>
        /// <returns></returns>
        [OperationContract]
        List<V_ICPOBILLMODEL> GetOrderList(
            hn.Core.Model.User loginUser,
            string classarea2name,
            string brand,
            int status,
            string premiseid,
            string billno,
            string startdate,
            string enddate, bool searchclose = false);


        [OperationContract]
        List<V_ICPOBILLENTRYMODEL> GetOrderEntryList(string icprbillid, string status);

        [OperationContract]
        ICPOBILLMODEL GetSingleOrder(string icprbillid);


        [OperationContract]
        List<ICPO_BOLentryModel> GeICPO_BOLList(
           hn.Core.Model.User loginUser,
           string FPObillno,
           string Ficbolno,
           int FSYNCSTATUS,
           string FACCOUNT,
           string FITEMID,
           string startdate,
           string enddate, bool searchclose = false);

        [OperationContract]
        string GetStartDate_syn();

        [OperationContract]
        string GetStartDate_syn_cc();

        [OperationContract]
        List<TB_PREMISEModel> GetJYDW(string keyword);

        [OperationContract]
        SRCModel getSrc(string pz, string gg, string xh);

        [OperationContract]
        SYS_SUBDICSMODEL GetArea2(string fid);

        [OperationContract]
        string GeICPO_BOLListMN_syn(DateTime starttime);

        [OperationContract]
        int GetLocalSum(string autoid, string icseoutbillentryid);

        [OperationContract]
        string GeICPO_BOLListMN_syn_cc(DateTime starttime);


        [OperationContract]
        string GetCCD(DateTime starttime);

        [OperationContract]
        List<v_thd> v_thdModel_List();

        [OperationContract]
        string[] v_thdModelName_List();
        
        [OperationContract]
        List<v_thdModel> GeICPO_BOLListMN_db(
        MApiModel.api8.Rootobject getapi8,string cpxh,string cpgg,string jhdh, string thd, string area2, int bNotArea);

        [OperationContract]
        bool ConfirmSave_ICPO(string action, List<ICPOBILLENTRYMODEL> data, User loginUser);

        [OperationContract]
        bool CloseSave_ICPO(List<ICPOBILLENTRYMODEL> data, User loginUser, string content);
        [OperationContract]
        bool AuditSave_ICPO(List<ICPOBILLENTRYMODEL> data, User loginUser, string content);
        [OperationContract]
        bool UnAuditSave_ICPO(List<ICPOBILLENTRYMODEL> data, User loginUser, string content);
        #endregion


        #region 同步厂商的接口


        [OperationContract]
        string Remote_InsertICPOEntry(MApiModel.api3.Rootobject getapi3);

        [OperationContract]
        void Remote_GetICPOEntry(string strFID,string strEntryID, ref ICPOBILLMODEL billModel, ref ICPOBILLENTRYMODEL entryModel);

        [OperationContract]
        List<ICPO_BOLentryModel> Remote_GetICPO_BOEntry(string fbillno, string entryid);
        [OperationContract]
        int Remote_SetICPO_BOEntryStatus(string fbillno, string entryid);

        /// <summary>
        /// 单独保存请购计划明细
        /// </summary>
        /// <param name="fbillno"></param>
        /// <param name="entryid"></param>
        /// <returns></returns>
        [OperationContract]
        string Save_ICPREntry_List(ICPRBILLENTRYMODEL tModel);

        [OperationContract]
        int Update_FSYNStatus(ICPOBILLMODEL billMode, int iStatus);

        [OperationContract]
        int Update_FSYN_Remote_Status(string billMode, int iStatus,string cjbh,Dictionary<int,string> dic_entry_thdbmdetail);

        #endregion


        #region 金牌华耐
        [OperationContract]
        DataResult Forder_confirm(Forder_confirm_Input input);

        [OperationContract]
        DataResult Forder_delivery(List<Forder_delivery_Input> input);

        [OperationContract]
        bool Sync_Today_THD(string rq1,string rq2);

        #endregion

    }

}
