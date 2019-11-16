using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace hn.AutoSyncLib.Model
{
    public class MC_PickUpGoods_Result:MC_Request_BaseResult
    {
        public List<MC_PickUpGoods_ResultInfo_DTO> resultInfo { get; set; }
    }

    
    public class MC_PickUpGoods_ResultInfo_DTO
    {
        [Column(Name ="TPACKAGE")]
        public string package { get; set; }
        public string autoId { get; set; }//
        public string pzhm { get; set; }//
        public string pjhm { get; set; }//
        public DateTime rq { get; set; }//
        public string khhm { get; set; }//
        public string khmc { get; set; }//
        public string cppz { get; set; }//
        public string cpxh { get; set; }//
        public string cpgg { get; set; }
        public string cpsh { get; set; }
        public string cpdj { get; set; }//
        public string cpcm { get; set; }//
        public string dw { get; set; }//
        public decimal ks { get; set; }//
        public decimal sl { get; set; }//
        public decimal dj { get; set; }//
        public decimal je { get; set; }//
        public decimal gg { get; set; }//
        public decimal ggs { get; set; }//
        public string bz { get; set; }//
        public string khhm1 { get; set; }//
        public string khmc1 { get; set; }//
        public int DB { get; set; }//
        public string dhno { get; set; }
        public void ComputeFID()
        {
            var h256 = SHA256.Create();
            string value = pzhm + khhm + cppz + cpgg + cpxh + cpsh + cpcm + package + bz;

            var hasData = Encoding.UTF8.GetBytes(value);

            var has = h256.ComputeHash(hasData);

            string fid = BitConverter.ToString(has).Replace("-", "");

            autoId = fid;
        }
    }

    [TableName("MN_THD")]
    public class MC_PickUpGoods_ResultInfo
    {
        public MC_PickUpGoods_ResultInfo() { }

        public MC_PickUpGoods_ResultInfo(MC_PickUpGoods_ResultInfo_DTO data)
        {
            this.autoId = data.autoId;
            this.bz = data.bz;
            this.cpcm = data.cpcm;
            this.cpdj = data.cpdj;
            this.cpgg = data.cpgg;
            this.cppz = data.cppz;
            this.cpsh = data.cpsh;
            this.cpxh = data.cpxh;
            this.DB = data.DB;
            this.dhno = data.dhno;
            this.dj = data.dj;
            this.dw = data.dw;
            this.gg = data.gg;
            this.ggs = data.ggs;
            this.je = data.je;
            this.khhm = data.khhm;
            this.khhm1 = data.khhm1;
            this.khmc = data.khmc;
            this.khmc1 = data.khmc1;
            this.ks = data.ks;
            this.pjhm = data.pjhm;
            this.pzhm = data.pzhm;
            this.rq = data.rq;
            this.sl = data.sl;
            this.tpackage = data.package;

        }

        public string tpackage { get; set; }
        public string autoId { get; set; }//
        public string pzhm { get; set; }//
        public string pjhm { get; set; }//
        public DateTime rq { get; set; }//
        public string khhm { get; set; }//
        public string khmc { get; set; }//
        public string cppz { get; set; }//
        public string cpxh { get; set; }//
        public string cpgg { get; set; }
        public string cpsh { get; set; }
        public string cpdj { get; set; }//
        public string cpcm { get; set; }//
        public string dw { get; set; }//
        public decimal ks { get; set; }//
        public decimal sl { get; set; }//
        public decimal dj { get; set; }//
        public decimal je { get; set; }//
        public decimal gg { get; set; }//
        public decimal ggs { get; set; }//
        public string bz { get; set; }//
        public string khhm1 { get; set; }//
        public string khmc1 { get; set; }//
        public int DB { get; set; }//
        public string dhno { get; set; }

        public string cyr { get; set; }
        public string dz { get; set; }
        public string pzlb { get; set; }


    }
}