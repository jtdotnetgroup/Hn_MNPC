using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace hn.AutoSyncLib.Model
{
    public class MC_OutOfStore_Result:MC_Request_BaseResult
    {
        public List<MC_OutofStore_ResultInfo_DTO> resultInfo { get; set; }
    }

    [TableName("MN_CKD")]
    public class MC_OutofStore_ResultInfo
    {
        public MC_OutofStore_ResultInfo(MC_OutofStore_ResultInfo_DTO dto)
        {
            var t1 = this.GetType();
            var pis1 = t1.GetProperties().ToList();
            var t2 = dto.GetType();
            var pis2 = t2.GetProperties().ToList();

            pis2.ForEach(p2 =>
            {
                var fieldName = p2.Name;
                var value = p2.GetValue(dto, null);
                var p1 = pis1.FirstOrDefault(p =>p.Name==fieldName );
                if (p1 != null)
                {
                    p1.SetValue(this, value, null);
                }
            });

            this.tpackage = dto.package;
        }

        public string pzhm { get; set; }
        public string pjhm { get; set; }
        public DateTime rq { get; set; }
        public string khhm { get; set; }
        public string khmc { get; set; }
        public string cppz { get; set; }
        public string cpgg { get; set; }
        public string cpxh { get; set; }
        public string cpdj { get; set; }
        public string dw { get; set; }
        public decimal ks { get; set; }
        public decimal sl { get; set; }
        public string DB { get; set; }
        public string cpsh { get; set; }
        public string cpcm { get; set; }

        public string tpackage { get; set; }

        public string FID { get; set; }

        public string carno { get; set; }

        public void ComputeFID()
        {
            var h256= SHA256.Create();
            string value = pzhm + pjhm + rq + khhm + cppz + cpgg + cpxh + cpsh + cpcm + tpackage;

            var hasData = Encoding.UTF8.GetBytes(value);

            var has= h256.ComputeHash(hasData);

            string fid = BitConverter.ToString(has).Replace("-", "");
            FID = fid;
        }
    }

    public class MC_OutofStore_ResultInfo_DTO
    {
        public string pzhm { get; set; }
        public string pjhm { get; set; }
        public DateTime rq { get; set; }
        public string khhm { get; set; }
        public string khmc { get; set; }
        public string cppz { get; set; }
        public string cpgg { get; set; }
        public string cpxh { get; set; }
        public string cpdj { get; set; }
        public string dw { get; set; }
        public decimal ks { get; set; }
        public decimal sl { get; set; }
        public string DB { get; set; }
        public string cpsh { get; set; }
        public string cpcm { get; set; }

        public string package { get; set; }

        public string FID { get; set; }

        public void ComputeFID()
        {
            var h256 = SHA256.Create();
            string value = pzhm + pjhm + rq + khhm + cppz + cpgg + cpxh + cpsh + cpcm + package;

            var hasData = Encoding.UTF8.GetBytes(value);

            var has = h256.ComputeHash(hasData);

            string fid = BitConverter.ToString(has).Replace("-", "");
            FID = fid;
        }
    }
}