using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hn.Common;
using hn.Core.Bll;
using hn.Core;
using hn.Core.Model;
using Omu.ValueInjecter;
using hn.DataAccess.Model;
using hn.DataAccess.Bll;
using hn.DataAccess.Dal;

namespace hn.Mvc.Controllers
{
    public class CityController : BaseController
    {
        public string Province()
        {
            return JSONhelper.ToJson(ProvinceDal.Instance.GetAll().ToList());
        }

        public string City()
        {
            string provinceid = Request["provinceid"];
            return JSONhelper.ToJson(CityDal.Instance.GetWhere(new { PROVINCEID = provinceid }).ToList());
        }

        public string District()
        {
            string cityid = Request["cityid"];
            return JSONhelper.ToJson(DistrictDal.Instance.GetWhere(new { CITYID = cityid }).ToList());
        }
    }
}
