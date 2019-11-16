using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using hn.Common.Data;
using hn.Common;

namespace hn.Core.Model
{
    [TableName("Sys_Seq")]
    [Description("采番管理表")]
    public class SeqModel
	{
        /// <summary>
        /// KeyId
        /// </summary>
        [Description("KeyId")]
        public string FID { get; set; }

		/// <summary>
        /// 采番编号
		/// </summary>
        [Description("采番编号")]
        public string FFieldName { get; set; }
      
		/// <summary>
        /// 开始番号
		/// </summary>
        [Description("开始番号")]
        public decimal FStartNo { get; set; }
      
		/// <summary>
        /// 最终番号
		/// </summary>
        [Description("最终番号")]
        public decimal FLastNo { get; set; }
      
		/// <summary>
        /// 终了番号
		/// </summary>
        [Description("终了番号")]
        public decimal FMaxNo { get; set; }
      
		/// <summary>
        /// 自增值
		/// </summary>
        [Description("自增值")]
        public int FIncr { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public string FRemark { get; set; }
      
				
		public override string ToString()
		{
			return JSONhelper.ToJson(this);
		}
	}
}