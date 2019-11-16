using hn.Common.Data;
using hn.Common.Data.Checker;
using hn.DataAccess;
using hn.DataAccess.bll;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hn.Client.Service
{
    public partial class APIService
    {
        public DataResult Forder_confirm(Forder_confirm_Input input)
        {
            string sql = "";
            string where = " WHERE Fjde_sono=:Fjde_sono AND FsoentryID=:FsoentryID";
            DataResult result = new DataResult() { errCode = 0 };

            try
            {
                DataChecker.CheckObj(input);


                TB_DZmanuscript model = new TB_DZmanuscript();

                model.Fjde_sono = input.Fjde_sono;
                model.Fsrcbillno = input.Fsrcbillno;
                model.Fproductcode = input.Fproductcode;
                //model.forderunit = input.Forderunit;
                model.Forderamount = input.Forderamount;
                model.FsoentryID = input.FsoentryID;
                model.Forderprice = input.Forderprice;
                model.Fisrejected = input.Fisrejected;


                var cmd = DbUtils.GetCommand(model, Operate.Select);

                cmd.Parameters.AddWithValue("Fjde_sono", model.Fjde_sono);
                cmd.Parameters.AddWithValue("FsoentryID", model.FsoentryID);
                sql = "SELECT COUNT(*) FROM TB_DZmanuscript" + where;
                cmd.CommandText = sql;
                var count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count == -1)
                {
                    throw new Exception(string.Format("【{0}】销售订单号，行号【{1}】的记录不存在", input.Fjde_sono, input.FsoentryID));
                }

                sql = DbUtils.GetSqlWithObject(model, Operate.Update);

                DbUtils.GetParams(model, cmd);
                sql += where;
                cmd.CommandText = sql;
                count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    cmd.Transaction.Commit();
                    cmd.Connection.Close();
                    return result;
                }
                else
                {
                    throw new Exception("更新记录为0，未找到相关记录");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                LogHelper.WriteLog("Input: " + JsonHelper.ToJson(input));
                result.errMsg = ex.Message;
                result.errCode = -1;
                return result;
            }
        }

        public DataResult Forder_delivery(List<Forder_delivery_Input> input)
        {
            string sql = "";
            string where = "WHERE Fjde_sono=:Fjde_sono AND FsoentryID=:FsoentryID";
            int count = 0;
            DataResult result = new DataResult() { errCode = 0 };
            System.Data.OracleClient.OracleCommand cmd=null;
            try
            {
                if (input == null || input.Count == 0)
                {
                    throw new Exception("传入参数不能为空");
                }

                TB_Dzsrcdel model = null;
                cmd = DbUtils.GetCommand(model, Operate.Insert);

                input.ForEach(p =>
                {
                    DataChecker.CheckObj(p);
                    //根据Fjde_sono和FsoentryID获取底表记录FID
                    var manuscript = GetManuscript_By_SonoAnd_EntryID(p.Fjde_sono, p.FsoentryID, cmd);

                    model = GetDzsrcdel(p.Fjde_sono, p.FsoentryID, cmd);
                    Operate insertOrupdate = Operate.Update;
                    if (model == null)
                    {
                        insertOrupdate = Operate.Insert;
                        model = new TB_Dzsrcdel();
                        model.FmanuscriptID = manuscript.FID;
                        model.Fjde_sono = p.Fjde_sono;
                        model.FsoentryID = p.FsoentryID;
                        model.Fsrcbillno = p.Fsrcbillno;
                        model.fsrcdelno = p.Fsrcdelno;
                        model.fdeldate = p.Fdeldate;
                        model.Fproductcode = p.Fproductcode;
                    }
                    
                    model.fdelqty = p.Fdelqty;
                    model.Fcasecode = p.Fcasecode;
                    model.Fisdelover = p.Fisdelover;
                    model.Fbuttstatus3 = p.Fbuttstatus3;
                    model.Fremark1 = p.Fremark2;
                    model.FTime = DateTime.Now;


                    //插入或更新子表记录
                    sql = DbUtils.GetSqlWithObject(model, insertOrupdate);
                    sql = insertOrupdate == Operate.Update ? sql += where:sql;

                    cmd.CommandText = sql;
                    DbUtils.GetParams(model, cmd);
                    count += cmd.ExecuteNonQuery();
                });

                //按销售单号，行号进行汇总求和发货数量
                var group = input.GroupBy(p => new { p.Fjde_sono, p.FsoentryID })
                    .Select(p=>new { Fjde_sono=p.Key.Fjde_sono,FsoentryID=p.Key.FsoentryID,Fdelqty_total=p.Sum(item=>item.Fdelqty)})
                    .ToList();

                //更新WHERE语句，根据销售单号和行号更新发货数量
                where = "WHERE Fjde_sono=:Fjde_sono AND FsoentryID=:FsoentryID";
                group.ForEach(p => {
                    // 更新底表发货数量
                    sql =string.Format( "UPDATE TB_DZmanuscript SET Fdelqty_total={0} WHERE Fjde_sono={1} AND FsoentryID={2}",p.Fdelqty_total,p.Fjde_sono,p.FsoentryID);

                    cmd = DbUtils.GetCommand(new TB_DZmanuscript(), Operate.Update);
                    cmd.CommandText = sql;
                    
                    count += cmd.ExecuteNonQuery();
                });

                cmd.Transaction.Commit();
                cmd.Connection.Close();

                if (count == 0)
                {
                    throw new Exception("同步失败，插入了0条数据");
                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                LogHelper.WriteLog("Input: "+JsonHelper.ToJson( input));
                LogHelper.WriteLog("SQL: " + sql);
                result.errMsg = ex.Message;
                result.errCode = -1;
                if (cmd != null)
                {
                    cmd.Transaction.Rollback();
                    cmd.Connection.Close();
                }
                
                return result;
            }
        }

        private TB_DZmanuscript GetManuscript_By_SonoAnd_EntryID(string sono,int entryid,System.Data.OracleClient.OracleCommand cmd)
        {
            TB_DZmanuscript model = new TB_DZmanuscript() { Fjde_sono = sono, FsoentryID = entryid };

            string sql = DbUtils.GetSqlWithObject(model, Operate.Select);

            sql += "WHERE Fjde_sono=:Fjde_sono AND FsoentryID=:FsoentryID";
            cmd.CommandText = sql;
            DbUtils.GetParams(model, cmd);

            System.Data.OracleClient.OracleDataAdapter da = new System.Data.OracleClient.OracleDataAdapter(cmd);

            System.Data.DataTable table = new System.Data.DataTable();
            da.Fill(table);

            if (table.Rows.Count ==1)
            {
                var t = model.GetType();
                var pis = t.GetProperties().ToList();
                var row = table.Rows[0];

                model.FID = row["FID"].ToString();

                return model;
            }

            if (table.Rows.Count > 1)
            {
                throw new Exception(string.Format("{2}表中存在多条符合条件【Fjde_sono={0} AND FsoentryID={1}】的记录",sono,entryid,table.TableName));
            }

            throw new Exception("未找到相关的底表记录");


        }

        private TB_Dzsrcdel GetDzsrcdel(string sono,int entryid,System.Data.OracleClient.OracleCommand cmd)
        {
            TB_Dzsrcdel model = new TB_Dzsrcdel() { Fjde_sono = sono, FsoentryID = entryid };

            string sql = DbUtils.GetSqlWithObject(model, Operate.Select);
            sql += "WHERE Fjde_sono=:Fjde_sono AND FsoentryID=:FsoentryID";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("Fjde_sono", model.Fjde_sono);
            cmd.Parameters.AddWithValue("FsoentryID", model.FsoentryID);
            
            System.Data.OracleClient.OracleDataAdapter da = new System.Data.OracleClient.OracleDataAdapter(cmd);

            System.Data.DataTable table = new System.Data.DataTable();
            da.Fill(table);

            if (table.Rows.Count == 1)
            {
                var t = model.GetType();
                var pis = t.GetProperties().ToList();
                var row = table.Rows[0];

                pis.ForEach(p =>
                {
                    var value = row[p.Name];
                    if(value!=null&&DBNull.Value!=value) p.SetValue(model, value,null);
                });

                return model;
            }

            if (table.Rows.Count > 1)
            {
                throw new Exception(string.Format("TB_DZsrcdel表中存在多条符合条件【Fjde_sono={0} AND FsoentryID={1}】的记录", sono, entryid));
            }

            return null;

        }

    }
}