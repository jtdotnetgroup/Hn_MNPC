using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess
{
    public class Constant
    {
        #region ICPR枚举

        /// <summary>
        /// 请购计划业务类型
        /// </summary>
        public enum BILL_TYPE
        {
            请购计划 = 1,
            发货计划 = 2
        }

        ///// <summary>
        ///// 请购计划状态
        ///// </summary>
        //public enum ICPRBILL_FSTATUS
        //{
        //    草稿 = 1,
        //    待审核 = 2,
        //    审核通过 = 3,
        //    审核不通过 = 4,
        //    关闭 = 5,
        //    完成 = 6
        //}

        ///// <summary>
        ///// 请购计划明细状态
        ///// </summary>
        //public enum ICPRBILLENTRY_FSTATUS
        //{
        //    草稿 = 1,
        //    待审核 = 2,
        //    审核通过 = 3,
        //    审核不通过 = 4,
        //    关闭 = 5,
        //    完成 = 6,
        //}

        #endregion

        public enum BILL_FSTATUS
        {
            草稿 = 1,
            待审核 = 2,
            审核通过 = 3,
            审核不通过 = 4,
            关闭 = 5,
            完成 = 6,
        }


        public enum BILL_SYNStatus
        {
            未同步 = 0,
            已同步 = 1
        }


        public enum CAR_STATUS
        {
            待发布 = 1,
            已发布 = 2,
            发布失败 = 3,
            已确认 = 4,
            关闭 = 5,
            关闭_改 = 6,
        }

        public enum BILL_DELIVERY_FSTATUS
        {
            草稿 = 0,
            确认 = 1
        }

        public enum ICPRBILL_FSTATUS
        {
            草稿 = 1,
            待审核 = 2,
            审核通过 = 3,
            审核不通过 = 4,
            关闭 = 5,
            完成 = 6,
            采购确认 = 7
        }
        public enum ICPOBILL_FSTATUS
        {
            草稿 = 1,
            待审核 = 2,
            审核通过 = 3,
            审核不通过 = 4,
            关闭 = 5,
            完成 = 6,
            采购确认 = 7
        }
        /// <summary>
        /// 组织架构组织类型
        /// </summary>
        public enum TB_ORGANIZATION_FTYPE
        {
            集团 = 1,
            一级销区 = 2,
            二级销区 = 3,
            品牌部 = 4,
            事业部 = 5
        }

        /// <summary>
        /// 数据字典枚举，要和数据库保持一致
        /// </summary>
        public enum SysDics
        {
            销区 = 101,
            请购计划状态 = 102,
            价格政策类型 = 103,
            单位 = 104,
            计划类型 = 105,
            运输方式 = 106,
            一级销区=107,
            二级销区=108,
            品牌部=109,
            组织类型 = 110,
            工程性质 = 111,
            运费结算 = 112,
            发货方式 = 113
        }

        #region IC枚举


        /// <summary>
        /// 价格政策状态
        /// </summary>
        public enum ICPricePolicyStatus
        {
            待审核 = 0,
            审核通过 = 1
        }

        #endregion

        #region ICPO枚举

        ///// <summary>
        ///// 审核状态 
        ///// </summary>
        //public enum ICPOBILL_FSTATUS
        //{
        //    草稿 = 1,
        //    待审核 = 2,
        //    审核通过 = 3,
        //    审核不通过 = 4,
        //    关闭 = 5,
        //    完成 = 6,
        //    部分审核 = 7,
        //}

        /// <summary>
        /// 同步状态
        /// </summary>
        public enum ICPOBILL_FSYNCSTATUS
        {
            未同步 = 1,
            已同步 = 2,
            同步失败 = 3,
        }

        #endregion

        #region 发货通知枚举

        ///// <summary>
        ///// 发货通知状态
        ///// </summary>
        //public enum ICSEOUTBILL_FSTATUS
        //{
        //    草稿 = 1,
        //    提交 = 2,
        //    审核通过 = 3,
        //    审核不通过 = 4,
        //    关闭 = 5,
        //    完成 = 6,
        //}

        #endregion

        #region 系统消息

        /// <summary>
        /// 消息类型
        /// </summary>
        public enum TB_MESSAGE_FTYPE
        {
            订单消息 = 1,
            系统消息 = 2,
        }

        /// <summary>
        /// 消息子类型
        /// </summary>
        public enum TB_MESSAGE_FSUBTYPE
        {
            订单 = 1,
            发货 = 2,
            退货 = 3,
            订单变更 = 4,
        }

        #endregion
    }
}
