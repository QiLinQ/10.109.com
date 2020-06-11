using Core;
using Core.Responese;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class BigDataDAL
    {
        #region 查询地区所有机构
        public List<Dictionary<string, object>> GetNewList(string K)
        {
            string sql = "select ORGCODE From MediTable where ADMINISTRATIVECODE like '" + K + "%'";
            DBHelper db = new DBHelper();
            return db.GetNewList(sql, System.Data.CommandType.Text);
        }
        #endregion

        #region 一期
        public List<BigDataHome> HomeBigDataDAL(string SP_Name, SqlParameter[] paras)
        {
            DBHelper dB = new DBHelper();
            return dB.ProcHomeData(SP_Name, System.Data.CommandType.StoredProcedure, paras);
        }
        #endregion

        #region 二期
        public List<BigDataHome> HomeTTPBigDataDAL(string SP_Name, SqlParameter[] paras)
        {
            DBHelper dB = new DBHelper();
            return dB.ProcTTPHomeData(SP_Name, System.Data.CommandType.StoredProcedure, paras);
        }
        #endregion

        #region 查询地区所有机构
        public string GetHospNewList(string K)
        {
            List<Dictionary<string, object>> l = GetNewList(K);
            string Key = string.Empty;
            for (int i = 0; i < l.Count; i++)
            {
                Key += "'" + l[i]["ORGCODE"].ToString() + "',";
            }
            return Key.Substring(0, Key.Length - 1);
        }
        #endregion

        #region 根据地区查询所有医院
        public List<BigDataHome> CityHospList(string City)
        {
            string sql1 = "select ORGCODE, MANAGERORGNAME from MediTable where ADMINISTRATIVECODE like '" + City + "%'";
            DBHelper dB = new DBHelper();
            List<Dictionary<string, object>> mzrc = dB.GetNewList(sql1, System.Data.CommandType.Text);
            List<BigDataHome> list = new List<BigDataHome>();
            list.Add(new BigDataHome
            {
                message = "首页数据",
                data = new List<ItmeList>{
            new ItmeList { Name="地区医院",SelectItmeList=mzrc }
            }
            });
            return list;

        }
        #endregion

        public List<BigDataHome> CityBigDataDAL(string stat, string end, string city)
        {
            try
            {
                string k = GetHospNewList(city);
                DBHelper dB = new DBHelper();
                string sql1 = "select SUM(PsitDay) as '门诊人次' from Stocdata WHIT (NOLOCK) where [Data] between '" + stat + "' and '" + end + "' and HospCode in (" + k + ")";
                List<Dictionary<string, object>> mzrc = dB.GetNewList(sql1, System.Data.CommandType.Text);

               
                List<BigDataHome> list = new List<BigDataHome>();
                list.Add(new BigDataHome
                {
                    message = "首页数据",
                    data = new List<ItmeList>{
            new ItmeList { Name="门诊人次",SelectItmeList=mzrc }}
                });
                return list;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
    }
}
