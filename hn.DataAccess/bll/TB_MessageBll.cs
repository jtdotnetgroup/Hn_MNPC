using hn.Common;
using hn.Common.Provider;
using hn.Core;
using hn.Core.Bll;
using hn.Core.Dal;
using hn.Core.Model;
using hn.DataAccess.Bll;
using hn.DataAccess.dal;
using hn.DataAccess.model;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.bll
{

    public class TB_MessageBll
    {
 
        public static TB_MessageBll Instance
        {
            get { return SingletonProvider<TB_MessageBll>.Instance; }
        }

        //判断编号是否存在
        public bool isSerialNumber(string categoryname, string depid = "")
        {
            var UnitGroupsList = TB_MessageDal.Instance.GetAll().ToList();
            return UnitGroupsList.Any(n => n.FID == categoryname && n.FID != depid);
        }

        //判断编号是否存在
        public bool isFID(string categoryname)
        {

            var UnitGroupsList = TB_MessageDal.Instance.GetAll().ToList();

            return UnitGroupsList.Any(n => n.FID == categoryname);
        }


        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return TB_MessageDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }


        //过去树
        public IEnumerable<TB_TreeUserModel> GetOrganizationTreegridData() {

            return  TB_TreeUserDal.Instance.GetAll();
        }


        //获取链表
        public List<TB_SahowListMessageModel> GetAllBT_MessageModel()
        {
            
            TB_MessageDal Dal = new TB_MessageDal();
            List<TB_MESSAGEMODEL> list = Dal.GetAll().ToList();

            //select* from (select rownum rn, *from 表名 wher rownum < 20) a where a.rn > 10


            //Dal.GetWhere(new { Num:0, Num:20 });
            List<String> listFSenderID = new List<string>();
            List<String> listFReceiverID = new List<string>();

            for (int i = 0; i < list.Count; i++) {
                listFSenderID.Add(list[i].FSENDERID);
                listFReceiverID.Add(list[i].FRECEIVERID);
            }
            listFSenderID = listFSenderID.Distinct().ToList();
            listFReceiverID = listFReceiverID.Distinct().ToList();

            List<User> listUserModel = getAllFSenderID(listFSenderID);

            List<TB_OrganizationModel> listOrganizeModel = getAllReceiverID(listFReceiverID);

            int count = listOrganizeModel.Count > listUserModel.Count ? listOrganizeModel.Count : listUserModel.Count;

            List <TB_SahowListMessageModel> ListOrganizeModel = new List<TB_SahowListMessageModel>();
            TB_SahowListMessageModel model;

            for (int i = 0; i < list.Count; i++) {
                model = new TB_SahowListMessageModel();
                model.FID = list[i].FID;
                model.FDate = list[i].FDATE;
                model.FTitle = list[i].FTITLE;
                model.FSenderID = list[i].FSENDERID;
                model.FReceiverID = list[i].FID;
                model.FState = list[i].FSTATE;
                model.FContent = Encoding.UTF8.GetString(list[i].FCONTENT);
                model.FType = list[i].FTYPE;
                model.FSubType = list[i].FSUBTYPE;
                model.FBillNo = list[i].FBILLNO;
                ListOrganizeModel.Add(model);
                for (int j = 0; j < count; j++){
                    if (j < listUserModel.Count)
                    {
                        if (list[i].FSENDERID.Equals(listUserModel[j].FID))
                        {
                            model.FSenderName = listUserModel[j].UserName;
                        }
                    }
                    if (j < listOrganizeModel.Count)
                    {
                        if (list[i].FRECEIVERID.Equals(listOrganizeModel[j].FID))
                        {
                            model.FReceiverName = listOrganizeModel[j].FORGNAME;
                        }
                    }
                }
            }

            return ListOrganizeModel;

        }

        //通过指定ID找到指定的OrganizeModel
        public List<TB_OrganizationModel> getAllReceiverID(List<String> list) {
            List<TB_OrganizationModel> ListIEOrganizeModel = new List<TB_OrganizationModel>();
            TB_OrganizationModel model;
            for (int i = 0; i < list.Count; i++)
            {
                model = TB_OrganizationBll.Instance.getOrganizeModeName(list[i]);
                ListIEOrganizeModel.Add(model);
            }

            return ListIEOrganizeModel;
        }

        //public User GetUserBy(string userName)
        //{
        //    return GetAll().ToList().Find(n => n.UserName == userName);
        //}

        public List<User> getAllFSenderID(List<String> list) {
            List<User> listUser = new List<User>();
            User u;
            for (int i = 0; i < list.Count; i++) {
                u = UserDal.Instance.GetUser(list[i]);
                listUser.Add(u);
            }

            return listUser;
        }

        //删除
        public JsonMessage DelectBT_MessageModel(String FID)
        {
            int k = 0;
            string msg = "删除失败";
            string[] fids = FID.Split(',');

            if (fids.Length == 1)
            {
                if (isFID(FID))
                {
                    msg = "删除失败";
                }
                else
                {
                    k = TB_MessageDal.Instance.Delete(FID);
                    if (k > 0)
                    {
                        msg = "删除成功";
                    }
                    
                }

                return new JsonMessage { Data = k.ToString(), Message = msg, Success = k != 0 };
            }
            else {
                for (int i = 0; i < fids.Length; i++) {
                    if (isFID(fids[i]))
                    {
                        msg = "删除失败";
                    }
                    else
                    {
                        k = TB_MessageDal.Instance.Delete(fids[i]);
                        if (k > 0)
                        {
                            msg = "删除成功";
                        }

                    }
                }
                return new JsonMessage { Data = k.ToString(), Message = msg, Success = k != 0 };
            }

        }

        //修改
        public JsonMessage EditBT_MessageModel(TB_MESSAGEMODEL model)
        {
            string msg = "修改失败！";
            int k = 0;
            if (isFID(model.FID))

                k = TB_MessageDal.Instance.Update(model);
            if (k > 0)
            {
                msg = "修改成功";
            }
            else
            {
                msg = "修改失败！";
            }

            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k != 0 };
        }


        //添加
        public string AddNewBT_MessageModel(TB_MESSAGEMODEL model, int i)
        {
            string k = "";
            string msg = "添加失败！";
            if (isSerialNumber(model.FID))
                msg = "编号已存在！";

            else
            {
                k = TB_MessageDal.Instance.Insert(model);
                if (k != "")
                {
                    msg = "添加成功。";
                    model.FID = k;
                }

            }

            return "第"+ i.ToString()+" 条消息," + msg ;
        }


        //添加
        public JsonMessage AddNewBT_MessageModel(TB_MESSAGEMODEL model)
        {
            string k = "";
            string msg = "添加失败！";
            if (isSerialNumber(model.FID))
                msg = "编号已存在！";

            else
            {
                k = TB_MessageDal.Instance.Insert(model);
                if (k != "")
                {
                    msg = "添加成功。";
                    model.FID = k;
                }

            }

            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k != "" };
        }



        public int Delete(string FID)
        {
            return TB_MessageDal.Instance.Delete(FID);
        }

        public int BatchDelete(string FID)
        {
            return TB_MessageDal.Instance.BatchDelete(FID);
        }
    }
        
}
