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
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hn.Mvc.Controllers
{
    public class StockController : BaseController
    {
        //
        // GET: /Goods/

        public ActionResult Index()
        {
            ViewBag.ToolBar = BuildToolbar();

            return View();
        }

        [HttpPost]
        public string Data(int page = 1, int rows = 15,
            string productname = null,
            string stockname = null,
             string productnumber = null,
             string wdr = null,
             string batchno = null,
            string brand = null,
            string mode = null,
            string colorno = null)
        {
            if (!string.IsNullOrEmpty(brand) && (!string.IsNullOrEmpty(productname) || !string.IsNullOrEmpty(mode) || !string.IsNullOrEmpty(stockname) || !string.IsNullOrEmpty(colorno)))
            {
                int pagecount = 0;
                APIService.APIServiceClient api = new APIService.APIServiceClient();
                DataTable table = api.WmStock(
                    out pagecount,
                    productnumber,
                    productname,
                    stockname,
                    brand,
                    batchno,
                    wdr,
                    mode,
                    colorno,
                    rows,
                    page, true, false);

                return JSONhelper.FormatJSONForEasyuiDataGrid(pagecount, table);
            }
            return null;
        }

        public string Brand()
        {
            var list = new List<object>();

            var brandlist = TB_BrandBll.Instance.GetAll();
            var userbrand = TB_USERBRANDDal.Instance.GetWhere(new { FUSERID = SysVisitor.Instance.UserId });

            foreach (var model in brandlist)
            {
                if (!SysVisitor.Instance.IsAdmin)
                {
                    var count = userbrand.Where(m => m.FBRANDID == model.FID).Count();
                    if (count > 0)
                    {
                        list.Add(new
                        {
                            id = model.FID,
                            text = model.FNAME + "(" + model.FFACTORYNO + ")",
                            number = model.FFACTORYNO
                        });
                    }
                }
                else
                {
                    list.Add(new
                    {
                        id = model.FID,
                        text = model.FNAME + "(" + model.FFACTORYNO + ")",
                        number = model.FFACTORYNO
                    });
                }
                
            }

            return JSONhelper.ToJson(list);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
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
    }
}
