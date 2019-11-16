using hn.Common;
using hn.Core;
using hn.Core.Bll;
using hn.DataAccess.bll;
using hn.DataAccess.Bll;
using hn.DataAccess.dal;
using hn.DataAccess.model;
using hn.DataAccess.Model;
using hn.Mvc.Models;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hn.Mvc.Controllers
{
    public class ProductController : BaseController
    {
        //
        // GET: /Goods/

        public ActionResult Index()
        {
            ViewBag.ToolBar = BuildToolbar();

            return View();
        }

        [HttpPost]
        public string Data(
            int page = 1, 
            int rows = 15, 
            string FORGID = null, 
            string FTYPEID = null, 
            string categoryID = null, 
            string FBRANDID = null, 
            string keywords = null,
            string sort = "FPRODUCTCODE",
            string order = "")
        {
            return ProductViewBll.Instance.GetEasyUIJson(page, rows, FORGID, FTYPEID, categoryID, FBRANDID, keywords,"", sort,order);
        }

        [HttpPost]
        public string DataByAudit(int page = 1, int rows = 15, string FORGID = null, string FTYPEID = null, string categoryID = null, string FBRANDID = null, string keywords = null)
        {
            return ProductViewBll.Instance.GetEasyUIJson(page, rows, FORGID, FTYPEID, categoryID, FBRANDID, keywords, "1");
        }


        public string Category()
        {
            string categoryTreeData = CategoryBll.Instance.GetCategoryTreegridData();
            return categoryTreeData.Replace("FID", "id").Replace("CATEGORY_NAME", "text");
        }

        public ActionResult Add()
        {
            return View();
        }

        //[HttpPost]
        //public string Add(FormCollection context)
        //{

        //    UserBll.Instance.CheckUserOnlingState();

        //    var rpm = GetRpm(context);
        //    ProductModel model = new ProductModel();
        //    model.InjectFrom(rpm.Entity);
        //    model.FProductName = GoodsBll.Instance.GetNumber(rpm.Entity.FProductType);

        //    return ProductBll.Instance.AddGoods(model);
        //}

        [HttpPost]
        public JsonResult Add(ProductModel product)
        {
            string result = ProductBll.Instance.Add(product);

            return result.IsGuid() ? JsonResultHelper.ToSuccess("新增商品完成！") : JsonResultHelper.ToFailed(result);
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Edit(ProductModel product)
        {
            string result = ProductBll.Instance.Update(product);

            return result.IsNullOrEmpty() ? JsonResultHelper.ToSuccess("修改商品资料完成！") : JsonResultHelper.ToFailed(result);
        }


        [HttpPost]
        public JsonResult Status(string fid)
        {
            var product = ProductDal.Instance.Get(fid);
            product.FSTATUS = product.FSTATUS == 1 ? 0 : 1;

            var result = ProductDal.Instance.Update(product);

            return result > 0 ? JsonResultHelper.ToSuccess("商品资料已" + (product.FSTATUS == 1 ? "启用" : "禁用") + "！") : JsonResultHelper.ToFailed("设置失败");
        }

        [HttpPost]
        public string Delete(FormCollection context)
        {
            return ProductBll.Instance.DeleteAllBat(Request["id"]).ToString();
        }

        private RequestParamModel<ProductModel> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<ProductModel>(context) { CurrentContext = context };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<ProductModel>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }

        [HttpPost]
        public string Audit(FormCollection context)
        {
            try
            {
                string ids = context["ids"];
                string status = context["status"];

                string[] array = ids.Split(',');
                foreach (string id in array)
                {
                    var count = SRCDal.Instance.CountWhere(new { FPRODUCTID = id });
                    if (count > 0)
                    {

                        ProductDal.Instance.UpdateWhatWhere(
                            new
                            {
                                FCHECKSTATUS = 1,// status.ToInt(),
                                FCHECKERID = SysVisitor.Instance.UserId,
                                FCHECKTIME = DateTime.Now
                            },
                            new { FID = id });
                    }
                }

                return JSONhelper.ToJson(new { errCode = 0, errMsg = "" });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(ex.Message);
            }

        }

        [HttpPost]
        public string AuditAnti(FormCollection context)
        {
            try
            {
                string ids = context["ids"];
                string status = context["status"];

                string[] array = ids.Split(',');
                foreach (string id in array)
                {
                    ProductDal.Instance.UpdateWhatWhere(
                             new
                             {
                                 FCHECKSTATUS = 0,// status.ToInt(),
                                FCHECKERID = "",
                                 FCHECKTIME = DBNull.Value
                             },
                             new { FID = id });
                }

                return JSONhelper.ToJson(new { errCode = 0, errMsg = "" });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(ex.Message);
            }

        }        

        /// <summary>
        /// 厂家代码数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpPost]
        public string Src(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            string productid = context["FPRODUCTID"];

            var list = SRCDal.Instance.GetWhere(new { FPRODUCTID = productid }).ToList();

            return JSONhelper.FormatJSONForEasyuiDataGrid(list.Count, list);
        }

        /// <summary>
        /// 厂家代码数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpPost]
        public string Unit(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            string productid = context["FPRODUCTID"];

            var list = V_UNITGROUPDal.Instance.GetWhere(new { FPRODUCTID = productid }).ToList();

            return JSONhelper.FormatJSONForEasyuiDataGrid(list.Count, list);
        }

        [HttpPost]
        public string SrcSave(FormCollection context)
        {
            try
            {
                string id = context["FID"];

                if (!string.IsNullOrEmpty(id))
                {
                    SRCModel model = SRCDal.Instance.Get(id);
                    model.FPRODUCTID = context["FPRODUCTID"];
                    model.FSRCNAME = context["FSRCNAME"];
                    model.FSRCCODE = context["FSRCCODE"];
                    model.FSRCMODEL = context["FSRCMODEL"];
                    model.FUNIT = context["FUNIT"];
                    model.FORDERUNIT = context["FORDERUNIT"];
                    model.FRATE = context["FRATE"].ToDecimal();
                    model.FWEIGHT = context["FWEIGHT"].ToDecimal();

                    SRCDal.Instance.Update(model);
                }
                else
                {
                    SRCModel model = new SRCModel();
                    model.FPRODUCTID = context["FPRODUCTID"];
                    model.FSRCNAME = context["FSRCNAME"];
                    model.FSRCCODE = context["FSRCCODE"];
                    model.FSRCMODEL = context["FSRCMODEL"];
                    model.FUNIT = context["FUNIT"];
                    model.FORDERUNIT = context["FORDERUNIT"];
                    model.FRATE = context["FRATE"].ToDecimal();
                    model.FWEIGHT = context["FWEIGHT"].ToDecimal();

                    SRCDal.Instance.Insert(model);
                }


                return JSONhelper.ToJson(new { errCode = 0, errMsg = "" });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(new { errCode = -1, errMsg = ex.Message });
            }
        }

        public ActionResult SrcAdd()
        {
            UserBll.Instance.CheckUserOnlingState();

            SRCModel model = SRCDal.Instance.Get(Request.QueryString["id"]);
            if (model == null)
            {
                model = new SRCModel();
            }
            return View(model);
        }


        [HttpPost]
        public string SrcDelete(FormCollection context)
        {
            UserBll.Instance.CheckUserOnlingState();

            string id = context["FID"];

            return SRCDal.Instance.Delete(id).ToString();
        }
    }
}
