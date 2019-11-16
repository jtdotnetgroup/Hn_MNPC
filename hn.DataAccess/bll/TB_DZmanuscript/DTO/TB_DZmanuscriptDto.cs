using System;
using System.ComponentModel.DataAnnotations;

namespace hn.DataAccess.bll
{
    /// <summary>
    /// 厂家订单确认扣款接口传入参数
    /// </summary>
    public class Forder_confirm_Input
    {
        /// <summary>
        /// 销售订单号
        /// </summary>
        /// 
        [Required]
        public string Fjde_sono { get; set; }
        /// <summary>
        /// 厂家订单号
        /// </summary>
        public string Fsrcbillno { get; set; }
        /// <summary>
        /// 分录序号
        /// </summary>
        /// 
        [Required]
        public int FsoentryID { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public string Fproductcode { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Forderunit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Forderqty { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Forderprice { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal Forderamount { get; set; }
        /// <summary>
        /// 同步标识
        /// </summary>
        public int Fbuttstatus2 { get; set; }
        /// <summary>
        /// 是否驳回订单
        /// </summary>
        public int Fisrejected { get; set; }
    }

    /// <summary>
    /// 厂家发货接口
    /// </summary>
    public class Forder_delivery_Input
    {
        /// <summary>
        /// 销售订单号
        /// </summary>
        [Required]
        public string Fjde_sono { get; set; }
        /// <summary>
        /// 厂家订单号/客户采购单号	SO订单属性	与数据底稿表外关联一致
        /// </summary>
        public string Fsrcbillno { get; set; }
        /// <summary>
        /// 厂家发货单号	厂家发货单号/运单号
        /// </summary>
        public string Fsrcdelno { get; set; }
        /// <summary>
        /// 发货日期	用户操作填写或者接口导入	用户操作填写或者接口导入
        /// </summary>
        public DateTime Fdeldate { get; set; }
        /// <summary>
        /// 订单行号	JDE同步信息	与数据底稿表外关联一致
        /// </summary>
        /// 
        [Required]
        public int FsoentryID { get; set; }
        /// <summary>
        /// 第二项目号	商品ID	与数据底稿表外关联一致
        /// </summary>
        public string Fproductcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Forderunit { get; set; }
        public decimal Forderqty { get; set; }
        /// <summary>
        /// 本次实际发货件数	用户操作填写或者接口导入	厂家发货合计件数回写
        /// </summary>
        public decimal Fdelqty { get; set; }
        /// <summary>
        /// 厂家包装箱码
        /// </summary>
        public string Fcasecode { get; set; }
        /// <summary>
        /// 是否发货完毕	用户操作填写或者接口导入	系统更新
        /// </summary>
        public int Fisdelover { get; set; }
        /// <summary>
        /// 备注1	预留备注信息栏	预留字段
        /// </summary>
        public string Fremark2 { get; set; }
        /// <summary>
        /// 接口3同步标识	接口同步状态标识	同步接口更新
        /// </summary>
        public int Fbuttstatus3 { get; set; }
    }
}