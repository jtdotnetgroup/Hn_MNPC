using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using hn.Common.Data;
using hn.Common;
using hn.Core.Dal;
using System.Collections;
using hn.Core.Model;
using hn.DataAccess.Dal;
namespace hn.DataAccess.Model
{
    [TableName("V_ICPRBILLENTRY")]
    [Description("请购计划明细")]
    public class V_ICPRBILLENTRYMODEL : ICPRBILLENTRYMODEL
    {

        public bool FCHECK { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        public string FORDERUNITID { get; set; }

        /// <summary>
        /// 到货时间
        /// </summary>
        public string FNEEDDATESTR
        {
            get
            {
                return FNEEDDATE != null ? FNEEDDATE.ToString("yyyy-MM-dd") : "";
            }
        }

        public string ICPRBILLNO { get; set; }

        /// <summary>
        /// 产品系列
        /// </summary>
        public string FPRODUCTTYPE { get; set; }


        /// <summary>
        /// 产品代码
        /// </summary>
        public string FPRODUCTCODE { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string FPRODUCTNAME { get; set; }

        /// <summary>
        /// 产品规格
        /// </summary>
        public string FMODEL { get; set; }
        public decimal FWEIGHT { get; set; }
        public decimal FVOLUME { get; set; }

        /// <summary>
        /// 基本单位名称
        /// </summary>
        public string FUNITNAME { get; set; }

        /// <summary>
        /// 经营场所ID
        /// </summary>
        public string FPREMISEID { get; set; }

        /// <summary>
        /// 经营场所名称
        /// </summary>
        public string FPREMISENAME { get; set; }

        /// <summary>
        /// 二级销区ID
        /// </summary>
        public string FCLASSAREA2 { get; set; }


        /// <summary>
        /// 二级销区
        /// </summary>
        public string FCLASSAREA2NAME { get; set; }


        /// <summary>
        /// 品牌部ID
        /// </summary>
        public string FPREMISEBRANDID { get; set; }

        /// <summary>
        /// 品牌部
        /// </summary>
        public string FPREMISEBRANDNAME { get; set; }


        /// <summary>
        /// 包装规格
        /// </summary>
        public string FPKGFORMAT { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public string FBRANDNAME { get; set; }
        public string FBRANDID { get; set; }
        public string FSRCID { get; set; }
        public string FSRCNAME { get; set; }
        public string FSRCCODE { get; set; }
        public string FSRCMODEL { get; set; }
        public string FSRCUNIT { get; set; }
        public decimal FRATE { get; set; }

        public string FRECEIVINGADDR { get; set; }
        public string FDELIVERYDES { get; set; }

        public decimal FORDERUNITLEFTQTY { get; set; }
        public string FCATEGORYCODE { get; set; }
        public string FCATEGORYNAME { get; set; }
        /// <summary>
        /// 审核状态名称
        /// </summary>
        [DbField(false)]
        public string FSTATUSNAME
        {
            get { return Enum.GetName(typeof(Constant.ICPRBILL_FSTATUS), FSTATUS.ToInt()); }
        }

        /// <summary>
        /// 确认人
        /// </summary>
        public string FCONFIRM_USERNAME { get; set; }

        public string FFACTORYNO { get; set; }

        public string JDE { get; set; }

        public string FDELIVERYADDR { get; set; }

        public string SIGN_MAIN { get; set; }

        public string FSRC_GROUP_ID { get; set; }
        public string FSRC_GROUP_UNIT { get; set; }
        public decimal FSRC_GROUP_RATE { get; set; }
        public decimal FGROUPLEFTQTY { get; set; }

        public string FTYPENAME { get; set; }
        public string FPROJECTNAME { get; set; }

        public string FCLOSE_USERNAME { get; set; }

        public string FPROVINCEID { get; set; }
        public string FCITYID { get; set; }
        public string FDISTRICTID { get; set; }
        public string FCOUNTYID { get; set; }
        public string FCONSIGNEE { get; set; }
        public string FCONSIGNEE_TEL { get; set; }
        public string FFREIGHTID { get; set; }

        public int FISTICKET { get; set; }
        public string FCOMPANY { get; set; }
        public string FCOMPANY_NO { get; set; }
        public string FTRANSID { get; set; } 
        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
        public string fbrandname { get; set; }
        public string fbiller { get; set; }
        public string fsalearea { get; set; }

        public string fpremisename { get; set; }
}
}
