using BLL;
using Core;
using System.Web.Http;

namespace API.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public EntityResult HomeData(string StateTime, string EndTime,string SPTXT,string K)
        {
            try
            {
                Outpatient bll = new Outpatient();
                return new EntityResult(ResultType.Success, "", bll.BigDataBLL(StateTime, EndTime, SPTXT, K));
            }
            catch (System.Exception ex)
            {
                return new EntityResult(ResultType.Error, "", null);
                throw;
            }
        }
        

        #region 二期
        #region 全市数据查询
        [HttpGet]
        public EntityResult TTPHomeData(string StateTime, string EndTime)
        {
            try
            {
                Outpatient bll = new Outpatient();
                return new EntityResult(ResultType.Success, "", bll.BigTTPDataBLL(StateTime, EndTime, "", 0));
            }
            catch (System.Exception ex)
            {
                return new EntityResult(ResultType.Error, "", null);
                throw;
            }
        }
        #endregion

        #region 医院信息查询
        [HttpGet]
        public EntityResult TTPHospData(string StateTime, string EndTime, string HospCode)
        {
            try
            {
                Outpatient bll = new Outpatient();
                return new EntityResult(ResultType.Success, "", bll.BigTTPDataBLL(StateTime, EndTime, HospCode, 1));
            }
            catch (System.Exception ex)
            {
                return new EntityResult(ResultType.Error, "", null);
                throw;
            }
        }
        #endregion

        #region 根据县查询
        [HttpGet]
        public EntityResult TTPCityData(string StateTime, string EndTime, string City)
        {
            try
            {
                Outpatient bll = new Outpatient();
                return new EntityResult(ResultType.Success, "", bll.BigTTPDataBLL(StateTime, EndTime, City, 2));
            }
            catch (System.Exception ex)
            {
                return new EntityResult(ResultType.Error, "", null);
                throw;
            }
        }
        #endregion 
        #endregion

        [HttpGet]
        public EntityResult CityHospList(string City)
        {
            try
            {
                Outpatient bll = new Outpatient();
                return new EntityResult(ResultType.Success, "", bll.CityHospList(City));
            }
            catch (System.Exception ex)
            {
                return new EntityResult(ResultType.Error, "", null);
                throw;
            }
        }
    }
}