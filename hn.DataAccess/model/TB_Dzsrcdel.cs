using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.model
{
    public class TB_Dzsrcdel
    {
        /// <summary>
        /// 记录ID	主键，系统生成
        /// </summary>
        public string FID { get; set; }
        /// <summary>
        /// 定制品厂家数据底表ID	与数据底稿表外关联ID
        /// </summary>
        public string FmanuscriptID { get; set; }
        /// <summary>
        /// 品牌/厂家	枚举项，由JDE_SO单同步生成	与数据底稿表外关联一致
        /// </summary>
        public string FbrandID { get; set; }
        /// <summary>
        /// JDE_SO订单号	SO订单号	与数据底稿表外关联一致
        /// </summary>
        public string Fjde_sono { get; set; }
        /// <summary>
        /// 厂家订单号/客户采购单号	SO订单属性	与数据底稿表外关联一致
        /// </summary>
        public string Fsrcbillno { get; set; }
        /// <summary>
        /// 第二项目号	商品ID	与数据底稿表外关联一致
        /// </summary>
        public string Fproductcode { get; set; }
        /// <summary>
        /// 订单行号	JDE同步信息	与数据底稿表外关联一致
        /// </summary>
        public decimal FsoentryID { get; set; }
        /// <summary>
        /// 本次实际发货件数	用户操作填写或者接口导入	厂家发货合计件数回写
        /// </summary>
        public decimal? fdelqty { get; set; }
        /// <summary>
        /// 是否发货完毕	用户操作填写或者接口导入	系统更新
        /// </summary>
        public decimal? Fisdelover { get; set; }
        /// <summary>
        /// 厂家包装箱码		
        /// </summary>
        public string Fcasecode { get; set; }
        /// <summary>
        /// 厂家发货单号	厂家发货单号/运单号	
        /// </summary>
        public string fsrcdelno { get; set; }
        /// <summary>
        /// 13	发货日期	用户操作填写或者接口导入	用户操作填写或者接口导入
        /// </summary>
        public DateTime fdeldate { get; set; }
        /// <summary>
        /// 接口3同步标识	接口同步状态标识	同步接口更新
        /// </summary>
        public decimal? Fbuttstatus3 { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        //public string Forderunit { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        //public decimal? Forderqty { get; set; }

        public string Fremark1 { get; set; }
        /// <summary>
        /// 同步时间
        /// </summary>
        public DateTime FTime { get; set; }
        
    }
}
