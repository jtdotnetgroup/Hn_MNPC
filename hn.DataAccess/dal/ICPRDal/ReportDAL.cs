using hn.Common;
using hn.Common.Data;
using hn.Common.Provider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace hn.DataAccess.dal
{
    public class ReportDAL
    {
        public static ReportDAL Instance
        {
            get { return SingletonProvider<ReportDAL>.Instance; }
        }

        /// <summary>
        /// 开发采购申请执行跟踪表
        /// </summary>
        /// <param name="billno"></param>
        /// <param name="preM_name"></param>
        /// <param name="src_name"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public DataTable Report1(string billno, string preM_name, string src_name, string startdate, string enddate)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select t2.fbillno 请购计划单号,                                                                                                            ");
                sql.AppendLine("       t4.fcode 结算分部号,                                                                                                                ");
                sql.AppendLine("       t4.fname 经营场所,                                                                                                                  ");
                sql.AppendLine("       t5.fname 计划类型,                                                                                                                  ");
                sql.AppendLine("       t2.fdate 单据日期,                                                                                                                  ");
                sql.AppendLine("       t2.freceivingaddr 收货地址,                                                                                                         ");
                sql.AppendLine("       t2.sign_main 签约主体,                                                                                                              ");
                sql.AppendLine("       t2.fprojectname 工程名称,                                                                                                           ");
                sql.AppendLine("       t2.jde JDE地址号,                                                                                                                   ");
                sql.AppendLine("       t2.fpurchase_no 采购订单号,                                                                                                         ");
                sql.AppendLine("       t6.fsrccode 默认厂家代码,                                                                                                           ");
                sql.AppendLine("       t6.fsrcname 厂家型号,                                                                                                               ");
                sql.AppendLine("       t6.fsrcmodel 规格,                                                                                                                  ");
                sql.AppendLine("       trim(t1.fitemid) 商品代码,                                                                                                          ");
                sql.AppendLine("       t1.faskqty 主单位请购数量,                                                                                                          ");
                sql.AppendLine("       t1.funitid 主计量单位,                                                                                                              ");
                sql.AppendLine("       t1.forderunitqty 采购单位请购数量,                                                                                                  ");
                sql.AppendLine("       t1.forderunit 采购计量单位,                                                                                                         ");
                sql.AppendLine("       t1.fleftamount 主单位剩余数量,                                                                                                      ");
                sql.AppendLine("       (case t1.fstatus                                                                                                                    ");
                sql.AppendLine("         when 1 then                                                                                                                       ");
                sql.AppendLine("          '草稿'                                                                                                                           ");
                sql.AppendLine("         when 2 then                                                                                                                       ");
                sql.AppendLine("          '待审核'                                                                                                                         ");
                sql.AppendLine("         when 3 then                                                                                                                       ");
                sql.AppendLine("          '审核'                                                                                                                           ");
                sql.AppendLine("         when 7 then                                                                                                                       ");
                sql.AppendLine("          '采购确认'                                                                                                                       ");
                sql.AppendLine("         when 5 then                                                                                                                       ");
                sql.AppendLine("          '关闭'                                                                                                                           ");
                sql.AppendLine("         else                                                                                                                              ");
                sql.AppendLine("          '0'                                                                                                                              ");
                sql.AppendLine("       end) as 请购记录状态,                                                                                                               ");
                sql.AppendLine("       t7.fbillno 发货计划单号,                                                                                                            ");
                sql.AppendLine("       t7.fcarnumber 开单车牌号,                                                                                                           ");
                sql.AppendLine("       t7.fcommitqty 主单位开单数量,                                                                                                       ");
                sql.AppendLine("       t7.fsrcbillno 厂家开单号,                                                                                                           ");
                sql.AppendLine("       t7.cust_ref 参考信息,                                                                                                               ");
                sql.AppendLine("       t7.fsrcqty as 主单位装车数量                                                                                                        ");
                sql.AppendLine("  from icprbillentry t1                                                                                                                    ");
                sql.AppendLine(" inner join icprbill t2                                                                                                                    ");
                sql.AppendLine("    on t1.fplanid = t2.fid                                                                                                                 ");
                sql.AppendLine(" inner join tb_products t3                                                                                                                 ");
                sql.AppendLine("    on trim(t1.fitemid) = trim(t3.fid)                                                                                                     ");
                sql.AppendLine("  left join TB_PREMISE t4                                                                                                                  ");
                sql.AppendLine("    on t2.fpremiseid = t4.fid                                                                                                              ");
                sql.AppendLine("  left join sys_subdics t5                                                                                                                 ");
                sql.AppendLine("    on t2.ftypeid = t5.fid                                                                                                                 ");
                sql.AppendLine("  left join tb_products t6                                                                                                                 ");
                sql.AppendLine("    on trim(t1.fitemid) = trim(t6.fid)                                                                                                     ");
                sql.AppendLine("  left join (select t1.fprbillno,                                                                                                          ");
                sql.AppendLine("                    t1.ficprid,                                                                                                            ");
                sql.AppendLine("                    ListAgg(to_char(t1.fbillno), ',') WITHIN GROUP(ORDER BY t1.fprbillno, t1.ficprid) AS fbillno,                          ");
                sql.AppendLine("                    ListAgg(to_char(t1.fcarnumber), ', ') WITHIN GROUP(ORDER BY t1.fprbillno, t1.ficprid) AS fcarnumber,                   ");
                sql.AppendLine("                    ListAgg(to_char(t1.fsrcbillno), ',') WITHIN GROUP(ORDER BY t1.fprbillno, t1.ficprid) AS fsrcbillno,                    ");
                sql.AppendLine("                    sum(t1.fcommitqty) as fcommitqty,                                                                                      ");
                sql.AppendLine("                    ListAgg(to_char(t2.cust_ref), ', ') WITHIN GROUP(ORDER BY t1.fprbillno, t1.ficprid) AS cust_ref,                       ");
                sql.AppendLine("                    sum(t2.fsrcqty) as fsrcqty                                                                                             ");
                sql.AppendLine("               from (select t6.fbillno fprbillno,                                                                                          ");
                sql.AppendLine("                            t1.ficprid,                                                                                                    ");
                sql.AppendLine("                            t2.fbillno,                                                                                                    ");
                sql.AppendLine("                            t2.fcarnumber,                                                                                                 ");
                sql.AppendLine("                            t3.fsrcname,                                                                                                   ");
                sql.AppendLine("                            t3.funitid,                                                                                                    ");
                sql.AppendLine("                            t2.fsrcbillno,                                                                                                 ");
                sql.AppendLine("                            sum(t1.fcommitqty * t4.frate) as fcommitqty                                                                    ");
                sql.AppendLine("                       from icseoutbillentry t1                                                                                            ");
                sql.AppendLine("                       left join icseoutbill t2                                                                                            ");
                sql.AppendLine("                         on t1.ficseoutid = t2.fid                                                                                         ");
                sql.AppendLine("                       left join tb_products t3                                                                                            ");
                sql.AppendLine("                         on trim(t1.fitemid) = trim(t3.fid)                                                                                ");
                sql.AppendLine("                       left join src t4                                                                                                    ");
                sql.AppendLine("                         on t1.fsrcid = t4.fid                                                                                             ");
                sql.AppendLine("                       left join icprbillentry t5                                                                                          ");
                sql.AppendLine("                         on t1.ficprid = t5.fid                                                                                            ");
                sql.AppendLine("                       left join icprbill t6                                                                                               ");
                sql.AppendLine("                         on t5.fplanid = t6.fid                                                                                            ");
                sql.AppendLine("                      group by t6.fbillno,                                                                                                 ");
                sql.AppendLine("                               t1.ficprid,                                                                                                 ");
                sql.AppendLine("                               t2.fbillno,                                                                                                 ");
                sql.AppendLine("                               t2.fcarnumber,                                                                                              ");
                sql.AppendLine("                               t3.fsrcname,                                                                                                ");
                sql.AppendLine("                               t3.funitid,                                                                                                 ");
                sql.AppendLine("                               t2.fsrcbillno) t1                                                                                           ");
                sql.AppendLine("               left join (select fplan_no,                                                                                                 ");
                sql.AppendLine("                                fbillno,                                                                                                   ");
                sql.AppendLine("                                part_name,                                                                                                 ");
                sql.AppendLine("                                ListAgg(to_char(cust_ref), ',') WITHIN GROUP(ORDER BY fplan_no, fbillno, part_name) AS cust_ref,           ");
                sql.AppendLine("                                sum(fsrcqty) as fsrcqty                                                                                    ");
                sql.AppendLine("                           from (select t1.fplan_no,                                                                                       ");
                sql.AppendLine("                                        t2.fbillno,                                                                                        ");
                sql.AppendLine("                                        t1.part_name,                                                                                      ");
                sql.AppendLine("                                        t1.cust_ref,                                                                                       ");
                sql.AppendLine("                                        sum(t1.pic_total) fsrcqty                                                                          ");
                sql.AppendLine("                                   from wm_stockout_line t1                                                                                ");
                sql.AppendLine("                                   left join icseoutbill t2                                                                                ");
                sql.AppendLine("                                     on trim(t1.fplan_no) =                                                                                ");
                sql.AppendLine("                                        trim(t2.fsrcbillno)                                                                                ");
                sql.AppendLine("                                  where t1.fplan_no is not null                                                                            ");
                sql.AppendLine("                                  group by t1.fplan_no,                                                                                    ");
                sql.AppendLine("                                           t1.part_name,                                                                                   ");
                sql.AppendLine("                                           t1.cust_ref,                                                                                    ");
                sql.AppendLine("                                           t2.fbillno)                                                                                     ");
                sql.AppendLine("                          group by fplan_no, fbillno, part_name) t2                                                                        ");
                sql.AppendLine("                 on t1.fbillno = t2.fbillno                                                                                                ");
                sql.AppendLine("                and t1.fsrcname = t2.part_name                                                                                             ");
                sql.AppendLine("              group by t1.fprbillno, t1.ficprid) t7                                                                                        ");
                sql.AppendLine("    on t1.fid = t7.ficprid                                                                                                                 ");
                sql.AppendLine(" where 1=1 ");
                if (!string.IsNullOrEmpty(billno))
                {
                    sql.AppendFormat("   and instr( t2.fbillno,'{0}')>0", billno);
                }

                if (!string.IsNullOrEmpty(preM_name))
                {
                    sql.AppendFormat("   and instr( t4.fname ,'{0}')>0", preM_name);
                }
                if (!string.IsNullOrEmpty(src_name))
                {
                    sql.AppendFormat("   and instr(t6.fsrcname,'{0}')>0", src_name);
                }
                if (!string.IsNullOrEmpty(startdate))
                {
                    sql.AppendFormat("   and t2.fdate >= to_date('{0}', 'yyyy-mm-dd')", startdate);
                }
                if (!string.IsNullOrEmpty(enddate))
                {
                    sql.AppendFormat("   and t2.fdate <= to_date('{0}', 'yyyy-mm-dd')", enddate);
                }
                return DbUtils.Query(sql.ToString());
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 厂家发货明细表
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public DataTable Report2(string startdate, string enddate)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select Customer_No                客户编码,      ");
                sql.AppendLine("       CUSTOMER_NAME              客户名称,      ");
                sql.AppendLine("       ORDER_NO                   订单号,        ");
                sql.AppendLine("       LINE_NO                    订单行号,      ");
                sql.AppendLine("       REL_NO                     顺序号,        ");
                sql.AppendLine("       Product_code_Name          品牌,          ");
                sql.AppendLine("       Product_Family_Name        类别,          ");
                sql.AppendLine("       Prime_Commodity_Name       规格,          ");
                sql.AppendLine("       Part_No                    产品编码,      ");
                sql.AppendLine("       Part_Name                  产品名称,      ");
                sql.AppendLine("       UNIT_MEAS                  单位,          ");
                sql.AppendLine("       PER_BOX                    包装,          ");
                sql.AppendLine("       LOT_BATCH_NO               批次,          ");
                sql.AppendLine("       W_D_R_ID                   WDR号,         ");
                sql.AppendLine("       PRICE_LIST_NO              价格单号,      ");
                sql.AppendLine("       List_SALE_UNIT_PRICE       销售价,        ");
                sql.AppendLine("       List_PACKING_PRICE         包装费,        ");
                sql.AppendLine("       List_TRUCKAGE_PRICE        装车费,        ");
                sql.AppendLine("       List_TRANSPORTATION_PRICE  打托费其他,    ");
                sql.AppendLine("       Order_SALE_UNIT_PRICE      销售价1,       ");
                sql.AppendLine("       Order_PACKING_PRICE        包装费1,       ");
                sql.AppendLine("       Order_TRUCKAGE_PRICE       装车费1,       ");
                sql.AppendLine("       Order_TRANSPORTATION_PRICE 打托费其他1,   ");
                sql.AppendLine("       C_Manage_Price             管理费,        ");
                sql.AppendLine("       CUST_REF                   参考,          ");
                sql.AppendLine("       NOTE_TEXT                  订单备注,      ");
                sql.AppendLine("       LINE_NOTE_TEXT             订单行备注,    ");
                sql.AppendLine("       BUY_QTY                    箱数,          ");
                sql.AppendLine("       Pic_total                  总片数,        ");
                sql.AppendLine("       Unit_Total                 平米数,        ");
                sql.AppendLine("       GRADE                      等级,          ");
                sql.AppendLine("       LOCATION_NO                库位,          ");
                sql.AppendLine("       WAREHOUSE                  仓库,          ");
                sql.AppendLine("       NET_PART_TOTAL             总销售价,      ");
                sql.AppendLine("       PACKING_TOTAL              总包装费,      ");
                sql.AppendLine("       TRUCKAGE_TOTAL             总装车费,      ");
                sql.AppendLine("       TRANSPORTATION_TOTAL       打托费其他合计,");
                sql.AppendLine("       C_Manage_Total             总管理费,      ");
                sql.AppendLine("       DATE_ENTERED               开单日期,      ");
                sql.AppendLine("       DATE_APPLIED               提货日期,      ");
                sql.AppendLine("       C_CENTER_WAREHOUSE         中心仓,        ");
                sql.AppendLine("       Customer_PO_No             客户订单号,    ");
                sql.AppendLine("       FPLAN_NO                   开单号         ");
                sql.AppendLine("  from wm_stockout_line");
                sql.AppendLine(" where 1=1 ");

                if (!string.IsNullOrEmpty(startdate))
                {
                    sql.AppendFormat("   and date_applied >= to_date('{0}', 'yyyy-mm-dd')", startdate);
                }
                if (!string.IsNullOrEmpty(enddate))
                {
                    sql.AppendFormat("   and date_applied <= to_date('{0}', 'yyyy-mm-dd')", enddate);
                }

                return DbUtils.Query(sql.ToString());
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 厂家发货明细表
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public DataTable Report3(string startdate, string enddate, string ftypename, string fbrandname)
        {
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine(" select                                                                            ");
                sql.AppendLine("    fsrcname 厂家型号,                                                             ");
                sql.AppendLine("    fsrcmodel 规格,                                                                ");
                sql.AppendLine("    max(case when fname='张家口' then fqty end)  张家口,                           ");
                sql.AppendLine("    max(case when fname='淄博' then fqty end)  淄博,                               ");
                sql.AppendLine("    max(case when fname='德州' then fqty end)  德州,                               ");
                sql.AppendLine("    max(case when fname='秦皇岛' then fqty end)  秦皇岛,                           ");
                sql.AppendLine("    max(case when fname='唐山' then fqty end)  唐山,                               ");
                sql.AppendLine("    max(case when fname='沧州' then fqty end)  沧州,                               ");
                sql.AppendLine("    max(case when fname='太原' then fqty end)  太原,                               ");
                sql.AppendLine("    max(case when fname='聊城' then fqty end)  聊城,                               ");
                sql.AppendLine("    max(case when fname='北京' then fqty end)  北京,                               ");
                sql.AppendLine("    max(case when fname='离石' then fqty end)  离石,                               ");
                sql.AppendLine("    max(case when fname='潍坊' then fqty end)  潍坊,                               ");
                sql.AppendLine("    max(case when fname='青岛' then fqty end)  青岛,                               ");
                sql.AppendLine("    max(case when fname='广州' then fqty end)  广州,                               ");
                sql.AppendLine("    max(case when fname='山东本部' then fqty end)  山东本部,                       ");
                sql.AppendLine("    max(case when fname='泰安' then fqty end)  泰安,                               ");
                sql.AppendLine("    max(case when fname='无锡' then fqty end)  无锡,                               ");
                sql.AppendLine("    max(case when fname='整合营销中心' then fqty end)  整合营销中心,               ");
                sql.AppendLine("    max(case when fname='滇西' then fqty end)  滇西,                               ");
                sql.AppendLine("    max(case when fname='保定' then fqty end)  保定,                               ");
                sql.AppendLine("    max(case when fname='东营' then fqty end)  东营,                               ");
                sql.AppendLine("    max(case when fname='成都' then fqty end)  成都,                               ");
                sql.AppendLine("    max(case when fname='济南' then fqty end)  济南,                               ");
                sql.AppendLine("    max(case when fname='石家庄' then fqty end)  石家庄,                           ");
                sql.AppendLine("    max(case when fname='爱驰家居' then fqty end)  爱驰家居,                       ");
                sql.AppendLine("    max(case when fname='昆明' then fqty end)  昆明,                               ");
                sql.AppendLine("    max(case when fname='总部' then fqty end)  总部,                               ");
                sql.AppendLine("    max(case when fname='菏泽' then fqty end)  菏泽,                               ");
                sql.AppendLine("    max(case when fname='滨州' then fqty end)  滨州,                               ");
                sql.AppendLine("    max(case when fname='廊坊' then fqty end)  廊坊,                               ");
                sql.AppendLine("    max(case when fname='邢台' then fqty end)  邢台,                               ");
                sql.AppendLine("    max(case when fname='承德' then fqty end)  承德,                               ");
                sql.AppendLine("    max(case when fname='天津市' then fqty end)  天津市,                           ");
                sql.AppendLine("    max(case when fname='南京' then fqty end)  南京,                               ");
                sql.AppendLine("    max(case when fname='哈尔滨' then fqty end)  哈尔滨,                           ");
                sql.AppendLine("    max(case when fname='衡水' then fqty end)  衡水,                               ");
                sql.AppendLine("    max(case when fname='邯郸' then fqty end)  邯郸,                               ");
                sql.AppendLine("    max(case when fname='芜湖' then fqty end)  芜湖,                               ");
                sql.AppendLine("    max(case when fname='安庆' then fqty end)  安庆,                               ");
                sql.AppendLine("    max(case when fname='合计' then fqty end)  合计                                ");
                sql.AppendLine("  from (select t4.fname, t5.fsrcname, t5.fsrcmodel, sum(t1.faskqty) fqty           ");
                sql.AppendLine("          from icprbillentry t1                                                    ");
                sql.AppendLine("          left join icprbill t2                                                    ");
                sql.AppendLine("            on t1.fplanid = t2.fid                                                 ");
                sql.AppendLine("          left join tb_premise t3                                                  ");
                sql.AppendLine("            on t2.fpremiseid = t3.fid                                              ");
                sql.AppendLine("          left join sys_subdics t4                                                 ");
                sql.AppendLine("            on t3.fclassarea2 = t4.fid                                             ");
                sql.AppendLine("           and t4.fclassid = '5F517C1D75904D80AA92049BAECC35C3'                    ");
                sql.AppendLine("          left join tb_products t5                                                 ");
                sql.AppendLine("            on trim(t1.fitemid) = trim(t5.fid)                                     ");
                sql.AppendLine("          left join sys_subdics t6                                                 ");
                sql.AppendLine("            on t2.ftypeid = t6.fid                                                 ");
                sql.AppendLine("           and t6.fclassid = '7787DFDC0F0D4B5D8E684B169DFA620B'                    ");
                sql.AppendLine("          left join tb_brand t7                                                    ");
                sql.AppendLine("            on trim(t2.fbrandid) = trim(t7.fid)                                    ");
                sql.AppendLine("         where t2.fstatus > 3                                                      ");
                if (!string.IsNullOrEmpty(startdate))
                {
                    sql.AppendFormat("           and t2.fbilldate >= to_date('{0}', 'yyyy-mm-dd')", startdate);
                }
                if (!string.IsNullOrEmpty(enddate))
                {
                    sql.AppendFormat("           and t2.fbilldate <= to_date('{0}', 'yyyy-mm-dd')", enddate);
                }

                if (!string.IsNullOrEmpty(ftypename))
                {
                    sql.AppendFormat("          and t6.fname like '%' || '{0}' || '%' ", ftypename);
                }

                if (!string.IsNullOrEmpty(fbrandname))
                {
                    sql.AppendFormat("         and instr(t7.fname, '{0}') > 0 ", fbrandname);
                }

                sql.AppendLine("         group by t4.fname, t5.fsrcname, t5.fsrcmodel                              ");
                sql.AppendLine("        union all (select '合计' as fname,                                         ");
                sql.AppendLine("                         t5.fsrcname,                                              ");
                sql.AppendLine("                         t5.fsrcmodel,                                             ");
                sql.AppendLine("                         sum(t1.faskqty) fqty                                      ");
                sql.AppendLine("                    from icprbillentry t1                                          ");
                sql.AppendLine("                    left join icprbill t2                                          ");
                sql.AppendLine("                      on t1.fplanid = t2.fid                                       ");
                sql.AppendLine("                    left join tb_premise t3                                        ");
                sql.AppendLine("                      on t2.fpremiseid = t3.fid                                    ");
                sql.AppendLine("                    left join sys_subdics t4                                       ");
                sql.AppendLine("                      on t3.fclassarea2 = t4.fid                                   ");
                sql.AppendLine("                     and t4.fclassid = '5F517C1D75904D80AA92049BAECC35C3'          ");
                sql.AppendLine("                    left join tb_products t5                                       ");
                sql.AppendLine("                      on trim(t1.fitemid) = trim(t5.fid)                           ");
                sql.AppendLine("                    left join sys_subdics t6                                       ");
                sql.AppendLine("                      on t2.ftypeid = t6.fid                                       ");
                sql.AppendLine("                     and t6.fclassid = '7787DFDC0F0D4B5D8E684B169DFA620B'          ");
                sql.AppendLine("                    left join tb_brand t7                                          ");
                sql.AppendLine("                      on trim(t2.fbrandid) = trim(t7.fid)                          ");
                sql.AppendLine("                   where t2.fstatus > 3                                            ");
                if (!string.IsNullOrEmpty(startdate))
                {
                    sql.AppendFormat("                     and t2.fbilldate >= to_date('{0}', 'yyyy-mm-dd') ", startdate);
                }
                if (!string.IsNullOrEmpty(enddate))
                {
                    sql.AppendFormat("                     and t2.fbilldate <= to_date('{0}', 'yyyy-mm-dd') ", enddate);
                }

                if (!string.IsNullOrEmpty(ftypename))
                {
                    sql.AppendFormat("                     and t6.fname like '%' || '{0}' || '%' ", ftypename);
                }

                if (!string.IsNullOrEmpty(fbrandname))
                {
                    sql.AppendFormat("                     and instr(t7.fname, '{0}') > 0 ", fbrandname);
                }

                sql.AppendLine("                   group by t5.fsrcname, t5.fsrcmodel)) t                          ");
                sql.AppendLine(" group by fsrcname, fsrcmodel                                                      ");
                sql.AppendLine(" order by fsrcname                                                                 ");

               
                LogHelper.WriteLog(sql.ToStr());

                return DbUtils.Query(sql.ToString());
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 在采购结算确认明细表处理界面上可以通过引入业务数据功能直接由用户抽取厂家发货数据记录或者发货计划表（托管仓出库）数据来生成结算表记录
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public DataTable GetSettlementData(string startdate, string enddate)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("(                                                                                                                                                                                       ");
                sql.AppendLine("select t2.fbrand, --品牌/厂家                                                                                                                                                           ");
                sql.AppendLine("       t1.customer_no, --厂家账号                                                                                                                                                       ");
                sql.AppendLine("       t1.customer_name, --厂家账户名                                                                                                                                                   ");
                sql.AppendLine("       t5.FJDE, --JDE账号                                                                                                                                                               ");
                sql.AppendLine("       t1.order_no, --厂家订单号                                                                                                                                                        ");
                sql.AppendLine("       t1.fplan_no, --厂家开单号                                                                                                                                                        ");
                sql.AppendLine("       to_char(t1.date_entered, 'yyyy-mm-dd') fdate_entered, --申请日期                                                                                                                 ");
                sql.AppendLine("       to_char(t1.date_applied, 'yyyy-mm-dd') fdate_applied, --提货日期                                                                                                                 ");
                sql.AppendLine("       t3.fproductid, --商品代码                                                                                                                                                        ");
                sql.AppendLine("       t1.part_name, --厂家型号                                                                                                                                                         ");
                sql.AppendLine("       t4.funitid, --主计量单位                                                                                                                                                         ");
                sql.AppendLine("       t4.funitname, --主计量单位名称                                                                                                                                                   ");
                sql.AppendLine("       t1.grade, --等级                                                                                                                                                                 ");
                sql.AppendLine("       t1.warehouse, --厂家仓库（需要对应到发货基地信息）                                                                                                                               ");
                sql.AppendLine("       t1.pic_total, --数量                                                                                                                                                             ");
                sql.AppendLine("       round((t1.net_part_total + t1.packing_total + t1.truckage_total +                                                                                                                ");
                sql.AppendLine("             t1.transportation_total + t1.c_manage_total) / (case                                                                                                                       ");
                sql.AppendLine("               when t1.pic_total = 0 then                                                                                                                                               ");
                sql.AppendLine("                1                                                                                                                                                                       ");
                sql.AppendLine("               else                                                                                                                                                                     ");
                sql.AppendLine("                t1.pic_total                                                                                                                                                            ");
                sql.AppendLine("             end),                                                                                                                                                                      ");
                sql.AppendLine("             2) as fprice, --单价                                                                                                                                                       ");
                sql.AppendLine("       (t1.net_part_total + t1.packing_total + t1.truckage_total +                                                                                                                      ");
                sql.AppendLine("       t1.transportation_total + t1.c_manage_total) as famount, --金额                                                                                                                  ");
                sql.AppendLine("       t1.cust_ref, --参考信息                                                                                                                                                          ");
                sql.AppendLine("       reverse(substr(reverse(t1.order_no), 1, 5)) || '+' ||                                                                                                                            ");
                sql.AppendLine("       reverse(substr(reverse(t1.lot_batch_no),                                                                                                                                         ");
                sql.AppendLine("                      1,                                                                                                                                                                ");
                sql.AppendLine("                      instr(reverse(t1.lot_batch_no), '/', 1, 1) - 1)) as Fspid, --库位                                                                                                 ");
                sql.AppendLine("       to_char(t1.fid) fid, --记录ID                                                                                                                                                    ");
                sql.AppendLine("       t2.fgroup_no, --组柜编号                                                                                                                                                         ");
                sql.AppendLine("       t2.fcode, --结算分部号                                                                                                                                                           ");
                sql.AppendLine("       t2.farea, --二级销区                                                                                                                                                             ");
                sql.AppendLine("       t2.freceiveraddr, --收货方                                                                                                                                                       ");
                sql.AppendLine("       t2.ftransname, --运输方式                                                                                                                                                        ");
                sql.AppendLine("       case                                                                                                                                                                             ");
                sql.AppendLine("         when t3.funit like '%托%' and t1.transportation_total > 1 then                                                                                                                 ");
                sql.AppendLine("          t1.buy_qty                                                                                                                                                                    ");
                sql.AppendLine("         else                                                                                                                                                                           ");
                sql.AppendLine("          0                                                                                                                                                                             ");
                sql.AppendLine("       end as pic_tp, --托盘数                                                                                                                                                          ");
                sql.AppendLine("       case                                                                                                                                                                             ");
                sql.AppendLine("         when t3.funit like '%托%' and t1.transportation_total > 1 and                                                                                                                  ");
                sql.AppendLine("              t1.per_box not in (470, 490) then                                                                                                                                         ");
                sql.AppendLine("          round((t1.transportation_total - t1.buy_qty * 40) / 20, 0)                                                                                                                    ");
                sql.AppendLine("         when t3.funit like '%托%' and t1.transportation_total > 1 and                                                                                                                  ");
                sql.AppendLine("              t1.per_box in (470, 490) then                                                                                                                                             ");
                sql.AppendLine("          t1.buy_qty * 6                                                                                                                                                                ");
                sql.AppendLine("         else                                                                                                                                                                           ");
                sql.AppendLine("          0                                                                                                                                                                             ");
                sql.AppendLine("       end as pic_td, --拖带数                                                                                                                                                          ");
                sql.AppendLine("       case                                                                                                                                                                             ");
                sql.AppendLine("         when t3.funit like '%托%' and t1.transportation_total > 1 and                                                                                                                  ");
                sql.AppendLine("              t1.per_box in (470, 490) then                                                                                                                                             ");
                sql.AppendLine("          t1.buy_qty * 2                                                                                                                                                                ");
                sql.AppendLine("         else                                                                                                                                                                           ");
                sql.AppendLine("          0                                                                                                                                                                             ");
                sql.AppendLine("       end as pic_TJ, --铁架数                                                                                                                                                          ");
                sql.AppendLine("       case                                                                                                                                                                             ");
                sql.AppendLine("         when t1.pic_total <= 0 and （t1.warehouse like '%调货%' or t1.warehouse = '0')                                                                                                 ");
                sql.AppendLine("           then '退费用'                                                                                                                                                                ");
                sql.AppendLine("         when t1.pic_total <= 0 and instr(t1.warehouse, '调货') = 0 and t1.warehouse != '0' and instr(t1.cust_ref, '托管') = 0                                                          ");
                sql.AppendLine("           then '退货'                                                                                                                                                                  ");
                sql.AppendLine("         when t1.pic_total <= 0 and instr(t1.warehouse, '调货') = 0 and t1.warehouse != '0' and instr(t1.cust_ref, '托管') > 0                                                          ");
                sql.AppendLine("           then '退出托管'                                                                                                                                                              ");
                sql.AppendLine("         when t1.cust_ref like '%托管%' and pic_total > 0 and （instr(t1.cust_ref, '盖') = 0 or instr(t1.cust_ref, '+') = 0)                                                            ");
                sql.AppendLine("           then '入托管仓' else '普通'                                                                                                                                                  ");
                sql.AppendLine("         end as ftype --记录类型                                                                                                                                                        ");
                sql.AppendLine("  from wm_stockout_line t1                                                                                                                                                              ");
                sql.AppendLine("  left join (select t7.fsrcname,                                                                                                                                                        ");
                sql.AppendLine("                    t2.fsrcbillno,                                                                                                                                                      ");
                sql.AppendLine("                    t5.fcode,                                                                                                                                                           ");
                sql.AppendLine("                    t6.fname farea,                                                                                                                                                     ");
                sql.AppendLine("                    t2.freceiveraddr,                                                                                                                                                   ");
                sql.AppendLine("                    t8.fname ftransname,                                                                                                                                                ");
                sql.AppendLine("                    t2.fgroup_no,                                                                                                                                                       ");
                sql.AppendLine("                    t9.fname || '/' || t9.ffactory as fbrand                                                                                                                            ");
                sql.AppendLine("               from icseoutbillentry t1                                                                                                                                                 ");
                sql.AppendLine("               left join icseoutbill t2                                                                                                                                                 ");
                sql.AppendLine("                 on t1.ficseoutid = t2.fid                                                                                                                                              ");
                sql.AppendLine("               left join icprbillentry t3                                                                                                                                               ");
                sql.AppendLine("                 on t1.ficprid = t3.fid                                                                                                                                                 ");
                sql.AppendLine("               left join icprbill t4                                                                                                                                                    ");
                sql.AppendLine("                 on t3.fplanid = t4.fid                                                                                                                                                 ");
                sql.AppendLine("               left join tb_premise t5                                                                                                                                                  ");
                sql.AppendLine("                 on t4.fpremiseid = t5.fid                                                                                                                                              ");
                sql.AppendLine("               left join sys_subdics t6                                                                                                                                                 ");
                sql.AppendLine("                 on t5.fclassarea2 = t6.fid                                                                                                                                             ");
                sql.AppendLine("               left join tb_products t7                                                                                                                                                 ");
                sql.AppendLine("                 on trim(t1.fitemid) = trim(t7.fid)                                                                                                                                     ");
                sql.AppendLine("               left join sys_subdics t8                                                                                                                                                 ");
                sql.AppendLine("                 on t2.ftransid = t8.fid                                                                                                                                                ");
                sql.AppendLine("               left join tb_brand t9                                                                                                                                                    ");
                sql.AppendLine("                 on t2.fbrandid = t9.fid                                                                                                                                                ");
                sql.AppendLine("              where t1.fid in                                                                                                                                                           ");
                sql.AppendLine("                    (select t1.fid                                                                                                                                                      ");
                sql.AppendLine("                       from (select min(t1.fid) fid, t1.fitemid, t2.fsrcbillno                                                                                                          ");
                sql.AppendLine("                               from icseoutbillentry t1                                                                                                                                 ");
                sql.AppendLine("                               left join icseoutbill t2                                                                                                                                 ");
                sql.AppendLine("                                 on t1.ficseoutid = t2.fid                                                                                                                              ");
                sql.AppendLine("                              group by t1.fitemid, t2.fsrcbillno) t1)) t2                                                                                                               ");
                sql.AppendLine("    on trim(t1.fplan_no) = trim(t2.fsrcbillno)                                                                                                                                          ");
                sql.AppendLine("   and trim(t1.part_name) = trim(t2.fsrcname)                                                                                                                                           ");
                sql.AppendLine("  left join src t3                                                                                                                                                                      ");
                sql.AppendLine("    on trim(t1.part_no) = trim(t3.fsrccode)                                                                                                                                             ");
                sql.AppendLine("  left join v_products t4                                                                                                                                                               ");
                sql.AppendLine("    on trim(t3.fproductid) = trim(t4.fid)                                                                                                                                               ");
                sql.AppendLine("  left join (select *                                                                                                                                                                   ");
                sql.AppendLine("               from tb_clientaccount                                                                                                                                                    ");
                sql.AppendLine("              where fid in (select t1.fid                                                                                                                                               ");
                sql.AppendLine("                              from (select max(fid) fid, faccount                                                                                                                       ");
                sql.AppendLine("                                      from tb_clientaccount                                                                                                                             ");
                sql.AppendLine("                                     group by faccount) t1)) t5                                                                                                                         ");
                sql.AppendLine("    on trim(t1.customer_no) = trim(t5.faccount)                                                                                                                                         ");
                if (!string.IsNullOrEmpty(startdate))
                {
                    sql.AppendFormat(" where t1.date_applied >= to_date('{0}', 'yyyy-mm-dd')", startdate);
                }
                if (!string.IsNullOrEmpty(enddate))
                {
                    sql.AppendFormat("   and t1.date_applied <= to_date('{0}', 'yyyy-mm-dd')", enddate);
                }
                sql.AppendLine(")                                                                                                                                                                                       ");
                sql.AppendLine("union all                                                                                                                                                                               ");
                sql.AppendLine("(                                                                                                                                                                                       ");
                sql.AppendLine("select t11.fname || '/' || t11.ffactory as fbrand,                                                                                                                                      ");
                sql.AppendLine("       t3.faccount CUSTOMER_NO,                                                                                                                                                         ");
                sql.AppendLine("       t3.fname CUSTOMER_NAME,                                                                                                                                                          ");
                sql.AppendLine("       t12.fjde,                                                                                                                                                                        ");
                sql.AppendLine("       t2.fbillno ORDER_NO,                                                                                                                                                             ");
                sql.AppendLine("       t2.fsrcbillno FPLAN_NO,                                                                                                                                                          ");
                sql.AppendLine("       to_char(t2.fbilldate, 'yyyy-mm-dd') fdate_entered,                                                                                                                               ");
                sql.AppendLine("       to_char(t2.fdeliverdate, 'yyyy-mm-dd') fdate_applied,                                                                                                                            ");
                sql.AppendLine("       t4.fid as fproductid,                                                                                                                                                            ");
                sql.AppendLine("       t4.fsrcname PART_NAME,                                                                                                                                                           ");
                sql.AppendLine("       t4.funitid,                                                                                                                                                                      ");
                sql.AppendLine("       t4.funitname,                                                                                                                                                                    ");
                sql.AppendLine("       t1.flevel GRADE,                                                                                                                                                                 ");
                sql.AppendLine("       t1.fstock WAREHOUSE,                                                                                                                                                             ");
                sql.AppendLine("       t1.fcommitqty * t5.frate PIC_TOTAL,                                                                                                                                              ");
                sql.AppendLine("       0 FPRICE,                                                                                                                                                                        ");
                sql.AppendLine("       0 FAMOUNT,                                                                                                                                                                       ");
                sql.AppendLine("       t2.fcarnumber CUST_REF,                                                                                                                                                          ");
                sql.AppendLine("       reverse(substr(reverse(t1.fbatchno),                                                                                                                                             ");
                sql.AppendLine("                      1,                                                                                                                                                                ");
                sql.AppendLine("                      instr(reverse(t1.fbatchno), '/', 1, 1) - 1)) as Fspid,                                                                                                            ");
                sql.AppendLine("       t1.fid,                                                                                                                                                                          ");
                sql.AppendLine("       t2.fgroup_no,                                                                                                                                                                    ");
                sql.AppendLine("       t8.fcode,                                                                                                                                                                        ");
                sql.AppendLine("       t9.fname farea,                                                                                                                                                                  ");
                sql.AppendLine("       t2.FRECEIVERADDR,                                                                                                                                                                ");
                sql.AppendLine("       t10.fname FTRANSNAME,                                                                                                                                                            ");
                sql.AppendLine("       0 PIC_TP,                                                                                                                                                                        ");
                sql.AppendLine("       0 PIC_TD,                                                                                                                                                                        ");
                sql.AppendLine("       0 PIC_TJ,                                                                                                                                                                        ");
                sql.AppendLine("       '托管出仓' FTYPE                                                                                                                                                                 ");
                sql.AppendLine("  from icseoutbillentry t1                                                                                                                                                              ");
                sql.AppendLine("  left join icseoutbill t2                                                                                                                                                              ");
                sql.AppendLine("    on t1.ficseoutid = t2.fid                                                                                                                                                           ");
                sql.AppendLine("  left join tb_clientaccount t3                                                                                                                                                         ");
                sql.AppendLine("    on t2.fclientid = t3.fid                                                                                                                                                            ");
                sql.AppendLine("  left join v_products t4                                                                                                                                                               ");
                sql.AppendLine("    on t1.fitemid = t4.fid                                                                                                                                                              ");
                sql.AppendLine("  left join src t5                                                                                                                                                                      ");
                sql.AppendLine("    on t1.fsrcid = t5.fid                                                                                                                                                               ");
                sql.AppendLine("  left join icprbillentry t6                                                                                                                                                            ");
                sql.AppendLine("    on t1.ficprid = t6.fid                                                                                                                                                              ");
                sql.AppendLine("  left join icprbill t7                                                                                                                                                                 ");
                sql.AppendLine("    on t6.fplanid = t7.fid                                                                                                                                                              ");
                sql.AppendLine("  left join tb_premise t8                                                                                                                                                               ");
                sql.AppendLine("    on t7.fpremiseid = t8.fid                                                                                                                                                           ");
                sql.AppendLine("  left join sys_subdics t9                                                                                                                                                              ");
                sql.AppendLine("    on t8.fclassarea2 = t9.fid                                                                                                                                                          ");
                sql.AppendLine("  left join sys_subdics t10                                                                                                                                                             ");
                sql.AppendLine("    on t2.ftransid = t10.fid                                                                                                                                                            ");
                sql.AppendLine("  left join tb_brand t11                                                                                                                                                                ");
                sql.AppendLine("    on t2.fbrandid = t11.fid                                                                                                                                                            ");
                sql.AppendLine("  left join tb_clientaccount t12                                                                                                                                                        ");
                sql.AppendLine("    on t2.fclientid = t12.fid                                                                                                                                                           ");
                sql.AppendLine(" where t2.fbilling_type = 2                                                                                                                                                             ");
                if (!string.IsNullOrEmpty(startdate))
                {
                    sql.AppendFormat("   and t2.fdeliverdate >= to_date('{0}', 'yyyy-mm-dd')", startdate);
                }
                if (!string.IsNullOrEmpty(enddate))
                {
                    sql.AppendFormat("   and t2.fdeliverdate <= to_date('{0}', 'yyyy-mm-dd')", enddate);
                }
                sql.AppendLine(")");



                return DbUtils.Query(sql.ToString());
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }
    }
}
