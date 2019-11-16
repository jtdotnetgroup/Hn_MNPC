using hn.APIService.DataAccess.Model;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace hn.APIService
{
    [ServiceContract]
    public interface IAPIService
    {

        /// <summary>
        /// 厂家库存接口
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataTable WmStock(
            string productnumber,
            string productname,
            string stockname,          
            string brandname,
            string batchno,
            string wdr,
            string mode,
            string colorno,
            out int pagecount,
            int pagesize = 20, 
            int page = 1,
            bool isfactory = true,
            bool istrust = false);

        /// <summary>
        /// 发货计划-输出接口
        /// </summary>
        /// <param name="icseout">发货计划主记录</param>
        /// <param name="icseoutentry">发货计划明细记录</param>
        /// <returns></returns>
        [OperationContract]
        string ICSEOUTBILLSync(List<V_ICSEOUTBILLMODEL> icseout, List<V_ICSEOUTBILLENTRYMODEL> icseoutentry);

        /// <summary>
        /// 获取厂家发货计划最新数据
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataTable GetICSEOUTUpdateData();

        /// <summary>
        /// 更新发货计划同步状态
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string UpdateCSEOUTSyncStatus(int id,int status);


        /// <summary>
        /// 获取厂家发货计划最新数据
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataTable GetICSEOUTUpdateData2();

        /// <summary>
        /// 更新发货计划同步状态
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string UpdateCSEOUTSyncStatus2(int id, int status);

        ///// <summary>
        ///// 获取厂家发货计划最新数据
        ///// </summary>
        ///// <returns></returns>
        //[OperationContract]
        //DataTable GetICSEOUTEntryUpdateData(int icseoutid);

        /// <summary>
        /// 获取厂家销区发货最新数据
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataTable GetSTOCKUpdateData();

        [OperationContract]
        DataTable GetTMP_STOCKBill();

        [OperationContract]
        string UpdateTMP_STOCKBillStatus(int id,int status);

        /// <summary>
        /// 获取Finfo_RE_id字段等于0的数据
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataTable GetFinfo_RE_id0();

        /// <summary>
        /// 更新Finfo_RE_id字段
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        string UpdateFinfo_RE_id(int id);

        [OperationContract]
        string InventorySync(List<SALES_ORDER_DATAMODEL> salesOrderDatas, List<SALESOUT_DATAMODEL> salesoutDatas, List<TP_INVENTORYMODEL> inventorys);

    

        [OperationContract]
        bool InsertICPOEntry(string strModelJson);

        [OperationContract]
        ICPOBILLENTRYModel_MHLS GetICPOEntry(string strFID,string strEntryID);

        [OperationContract]
        List<ICPO_BOLentryModel_MNLS> GetICPO_BOEntry(string fbillno, string entryid);
        [OperationContract]
        int SetICPO_BOEntryStatus(string fbillno, string entryid);

    }

}
