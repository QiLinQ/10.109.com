using Core.Responese;
using DAL;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BLL
{
    /// <summary>
    /// 门诊类
    /// </summary>
    public class Outpatient
    {
        #region 查询地区所有机构
        public string GetNewList(string K)
        {
            BigDataDAL dal = new BigDataDAL();
            List<Dictionary<string, object>> l = dal.GetNewList(K);
            string Key = string.Empty;
            for (int i = 0; i < l.Count; i++)
            {
                Key += "'" + l[i]["ORGCODE"].ToString() + "',";
            }
            return Key.Substring(0, Key.Length - 1);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StateTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="SPTXT">医院编号</param>
        /// <param name="K"></param>
        /// <returns></returns>
        public List<BigDataHome> BigDataBLL(string StateTime, string EndTime, string SPTXT, string K)
        {
            try
            {
                BigDataDAL dal = new BigDataDAL();
                SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@StateTime",SqlDbType.VarChar,50),
                new SqlParameter("@EndTime",SqlDbType.VarChar,50),
                new SqlParameter("@HospCode",SqlDbType.VarChar,200),
                new SqlParameter("@type",SqlDbType.VarChar,200)
                 };
                paras[0].Value = StateTime;
                paras[1].Value = EndTime;
                paras[2].Value = SPTXT;
                if (K == "H")
                {
                    paras[3].Value = 1;
                }
                if (K == "C")
                {
                    paras[3].Value = 2;
                }
                if (K == "Y")
                {
                    paras[3].Value = 3;
                }
                return dal.HomeBigDataDAL("SP_PHASE1_HomeData", paras);
            }
            catch 
            {
                return new List<BigDataHome>();
            }
        }

        #region 二期
        public List<BigDataHome> BigTTPDataBLL(string StateTime, string EndTime, string K, int n)
        {
            BigDataDAL dal = new BigDataDAL();
            if (n == 0)
            {
                SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@StateTime",StateTime),
                new SqlParameter("@EndTime",EndTime)
                 };
                return dal.HomeTTPBigDataDAL("SP_TTP_HomeBigData", paras);
            }
            if (n == 1)
            {
                SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@StateTime",StateTime),
                new SqlParameter("@EndTime",EndTime),
                new SqlParameter("@HospCode",K) };
                return dal.HomeTTPBigDataDAL("SP_TTP_HospBigData", paras);
            }
            if (n == 2)
            {
                SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@StateTime",StateTime),
                new SqlParameter("@EndTime",EndTime),
                new SqlParameter("@HospCode",K) };
                return dal.HomeTTPBigDataDAL("SP_TTP_CityBigData", paras);
            }
            return new List<BigDataHome>();
        }
        #endregion

        public List<BigDataHome> CityHospList(string City)
        {
            BigDataDAL dal = new BigDataDAL();
            return dal.CityHospList(City);

        }
    }
}
