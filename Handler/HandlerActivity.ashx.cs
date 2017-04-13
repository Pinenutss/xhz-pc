using OWSF.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace OWSF.PC.Handler
{
    /// <summary>
    /// HandlerActivity 的摘要说明
    /// </summary>
    public class HandlerActivity : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request.Params["action"];
            if (!string.IsNullOrEmpty(action))
            {
                switch (action)
                {
                    case "GetActivityList":
                        GetActivityList(context);
                        break;
                    case "GetActivityCount":
                        GetActivityCount(context);
                        break;
                    case "GetActivityDetails":
                        GetActivityDetails(context);
                        break;
                    case "GetNewsList":
                        GetNewsList(context);
                        break;
                    case "GetNewsCount":
                        GetNewsCount(context);
                        break;
                    case "GetNewsDetails":
                        GetNewsDetails(context);
                        break;
                }
            }
            context.Response.End();
        }

        #region 获取活动信息

        /// <summary>
        /// 获取活动列表
        /// </summary>
        /// <returns></returns>
        public void GetActivityList(HttpContext context)
        {
            try
            {
                BLL.ActivityInfo bll = new BLL.ActivityInfo();
                //参数
                string typeId = context.Request.QueryString["typeId"];
                string pageSize = context.Request.QueryString["pageSize"];
                string pageindex = context.Request.QueryString["pageindex"];
                //定义StringBuilder
                StringBuilder sb = new StringBuilder();
                //分页参数
                int _pageSize = Convert.ToInt32(pageSize);
                int _pageIndex = Convert.ToInt32(pageindex);
                int _startIndex = 0;
                int _endIndex = 0;
                if (_pageIndex == 0)
                {
                    _startIndex = 0;
                    _endIndex = _pageSize;
                }
                else
                {
                    _startIndex = _pageIndex * _pageSize + 1;
                    _endIndex = (_pageIndex + 1) * _pageSize;
                }
                //获取列表
                DataTable dt = bll.GetListByPage("IsDeleted=0 and ActivityTypeId=" + typeId, " Sort desc", _startIndex, _endIndex).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //定义JsonWriter
                    LitJson.JsonWriter jw = new LitJson.JsonWriter(sb);
                    jw.WriteArrayStart();
                    foreach (DataRow dr in dt.Rows)
                    {
                        jw.WriteObjectStart();

                        //获取字段
                        jw.WritePropertyName("ActivityId");
                        jw.Write(dr["ActivityId"].ToString());
                        jw.WritePropertyName("ActivityName");
                        jw.Write(dr["ActivityName"].ToString());
                        jw.WritePropertyName("ActivityAddress");
                        jw.Write(dr["ActivityAddress"].ToString());
                        jw.WritePropertyName("ActivityIntro");
                        jw.Write(dr["ActivityIntro"].ToString());
                        jw.WritePropertyName("CoverSmallImage");
                        jw.Write(Utils.GetAppSettings("Localhost") + dr["CoverSmallImage"].ToString());
                        jw.WritePropertyName("CoverFont");
                        jw.Write(dr["CoverFont"].ToString());
                        jw.WritePropertyName("StartDate");
                        jw.Write(Convert.ToDateTime(dr["StartDate"]).ToString("yyyy.MM.dd"));
                        jw.WritePropertyName("EndDate");
                        jw.Write(Convert.ToDateTime(dr["EndDate"]).ToString("yyyy.MM.dd"));
                        jw.WritePropertyName("ClickCount");
                        jw.Write(dr["ClickCount"].ToString());
                        jw.WritePropertyName("Sort");
                        jw.Write(dr["Sort"].ToString());

                        string status = "0";
                        string StartDate = dr["StartDate"].ToString();
                        string EndDate = dr["EndDate"].ToString();
                        if (!string.IsNullOrWhiteSpace(StartDate) && !string.IsNullOrWhiteSpace(EndDate))
                        {
                            if (Convert.ToDateTime(StartDate) < DateTime.Now && DateTime.Now.AddDays(-1) < Convert.ToDateTime(EndDate))
                            {
                                status = "1";//正在进行
                            }
                            else if (Convert.ToDateTime(StartDate) > DateTime.Now)
                            {
                                status = "2";//即将开始
                            }
                            else if (Convert.ToDateTime(EndDate) < DateTime.Now.AddDays(-1))
                            {
                                status = "3";//已结束
                            }
                        }
                        jw.WritePropertyName("Status");
                        jw.Write(status);

                        jw.WriteObjectEnd();
                    }
                    jw.WriteArrayEnd();
                    context.Response.Write(sb.ToString());
                }
                else
                {
                    context.Response.Write("{\"Status\":\"0\"}");//没有数据
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"Status\":\"ex\"}");//系统异常
            }
        }
        /// <summary>
        /// 获取活动详情
        /// </summary>
        /// <param name="context"></param>
        public void GetActivityDetails(HttpContext context)
        {
            try
            {
                BLL.ActivityInfo bll = new BLL.ActivityInfo();
                //参数
                string Id = context.Request.QueryString["Id"];
                //获取列表
                DataTable dt = bll.GetList("IsDeleted=0 and ActivityId=" + Id).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //定义StringBuilder
                    StringBuilder sb = new StringBuilder();
                    //定义JsonWriter
                    LitJson.JsonWriter jw = new LitJson.JsonWriter(sb);
                    jw.WriteArrayStart();
                    foreach (DataRow dr in dt.Rows)
                    {
                        jw.WriteObjectStart();

                        //获取字段
                        jw.WritePropertyName("ActivityId");
                        jw.Write(dr["ActivityId"].ToString());
                        jw.WritePropertyName("ActivityName");
                        jw.Write(dr["ActivityName"].ToString());
                        jw.WritePropertyName("ActivityAddress");
                        jw.Write(dr["ActivityAddress"].ToString());
                        jw.WritePropertyName("ActivityIntro");
                        jw.Write(dr["ActivityIntro"].ToString());
                        jw.WritePropertyName("CoverSmallImage");
                        jw.Write(Utils.GetAppSettings("Localhost") + dr["CoverSmallImage"].ToString());
                        jw.WritePropertyName("CoverFont");
                        jw.Write(dr["CoverFont"].ToString());
                        jw.WritePropertyName("StartDate");
                        jw.Write(dr["StartDate"].ToString());
                        jw.WritePropertyName("EndDate");
                        jw.Write(dr["EndDate"].ToString());
                        jw.WritePropertyName("ClickCount");
                        jw.Write(dr["ClickCount"].ToString());
                        jw.WritePropertyName("Sort");
                        jw.Write(dr["Sort"].ToString());

                        string status = "0";
                        string StartDate = dr["StartDate"].ToString();
                        string EndDate = dr["EndDate"].ToString();
                        if (!string.IsNullOrWhiteSpace(StartDate) && !string.IsNullOrWhiteSpace(EndDate))
                        {
                            if (Convert.ToDateTime(StartDate) < DateTime.Now && DateTime.Now.AddDays(-1) < Convert.ToDateTime(EndDate))
                            {
                                status = "1";//正在进行
                            }
                            else if (Convert.ToDateTime(StartDate) > DateTime.Now)
                            {
                                status = "2";//即将开始
                            }
                            else if (Convert.ToDateTime(EndDate) < DateTime.Now.AddDays(-1))
                            {
                                status = "3";//已结束
                            }
                        }
                        jw.WritePropertyName("Status");
                        jw.Write(status);

                        jw.WriteObjectEnd();
                    }
                    jw.WriteArrayEnd();
                    context.Response.Write(sb.ToString());
                }
                else
                {
                    context.Response.Write("{\"Status\":\"0\"}");//没有数据
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"Status\":\"ex\"}");//系统异常
            }
        }
        /// <summary>
        /// 获取活动总条数
        /// </summary>
        /// <returns></returns>
        public void GetActivityCount(HttpContext context)
        {
            try
            {
                //类别
                string typeId = context.Request.QueryString["typeId"];
                BLL.ActivityInfo bll = new BLL.ActivityInfo();
                string num = bll.GetRecordCount("IsDeleted=0 and ActivityTypeId=" + typeId).ToString();
                context.Response.Write("{\"Count\":\"" + num + "\"}");
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"Status\":\"ex\"}");//系统异常
            }
        }
        #endregion

        #region 获取新闻资讯
        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <returns></returns>
        public void GetNewsList(HttpContext context)
        {
            try
            {
                BLL.NewsInfo dataProviderProductImage = new BLL.NewsInfo();
                //参数
                string typeId = context.Request.QueryString["typeId"];
                string pageSize = context.Request.QueryString["pageSize"];
                string pageindex = context.Request.QueryString["pageindex"];
                //定义StringBuilder
                StringBuilder sb = new StringBuilder();
                //分页参数
                int _pageSize = Convert.ToInt32(pageSize);
                int _pageIndex = Convert.ToInt32(pageindex);
                int _startIndex = 0;
                int _endIndex = 0;
                if (_pageIndex == 0)
                {
                    _startIndex = 0;
                    _endIndex = _pageSize;
                }
                else
                {
                    _startIndex = _pageIndex * _pageSize + 1;
                    _endIndex = (_pageIndex + 1) * _pageSize;
                }
                //获取列表
                DataTable dt = dataProviderProductImage.GetListByPage("IsDeleted=0 and NewsTypeId=" + typeId, " Sort desc", _startIndex, _endIndex).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //定义JsonWriter
                    LitJson.JsonWriter jw = new LitJson.JsonWriter(sb);
                    jw.WriteArrayStart();
                    foreach (DataRow dr in dt.Rows)
                    {
                        jw.WriteObjectStart();

                        //获取字段
                        jw.WritePropertyName("NewsId");
                        jw.Write(dr["NewsId"].ToString());
                        jw.WritePropertyName("NewsTitle");
                        jw.Write(dr["NewsTitle"].ToString());
                        jw.WritePropertyName("NewsContent");
                        jw.Write(dr["NewsContent"].ToString());
                        jw.WritePropertyName("CreateDate");
                        jw.Write(dr["CreateDate"].ToString());
                        jw.WritePropertyName("CoverSmallImage");
                        jw.Write(Utils.GetAppSettings("Localhost") + dr["CoverSmallImage"].ToString());
                        jw.WritePropertyName("CoverFont");
                        jw.Write(dr["CoverFont"].ToString());
                        jw.WritePropertyName("ClickCount");
                        jw.Write(dr["ClickCount"].ToString());
                        jw.WritePropertyName("Sort");
                        jw.Write(dr["Sort"].ToString());

                        jw.WriteObjectEnd();
                    }
                    jw.WriteArrayEnd();
                    context.Response.Write(sb.ToString());
                }
                else
                {
                    context.Response.Write("{\"Status\":\"0\"}");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"Status\":\"ex\"}");
            }
        }
        /// <summary>
        /// 获取新闻详情
        /// </summary>
        /// <param name="context"></param>
        public void GetNewsDetails(HttpContext context)
        {
            try
            {
                BLL.NewsInfo bll = new BLL.NewsInfo();
                //参数
                string Id = context.Request.QueryString["Id"];
                //获取列表
                DataTable dt = bll.GetList("IsDeleted=0 and NewsId=" + Id).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //定义StringBuilder
                    StringBuilder sb = new StringBuilder();
                    //定义JsonWriter
                    LitJson.JsonWriter jw = new LitJson.JsonWriter(sb);
                    jw.WriteArrayStart();
                    foreach (DataRow dr in dt.Rows)
                    {
                        jw.WriteObjectStart();

                        //获取字段
                        jw.WritePropertyName("NewsId");
                        jw.Write(dr["NewsId"].ToString());
                        jw.WritePropertyName("NewsTitle");
                        jw.Write(dr["NewsTitle"].ToString());
                        jw.WritePropertyName("NewsContent");
                        jw.Write(dr["NewsContent"].ToString());
                        jw.WritePropertyName("CreateDate");
                        jw.Write(dr["CreateDate"].ToString());
                        jw.WritePropertyName("CoverSmallImage");
                        jw.Write(Utils.GetAppSettings("Localhost") + dr["CoverSmallImage"].ToString());
                        jw.WritePropertyName("CoverFont");
                        jw.Write(dr["CoverFont"].ToString());
                        jw.WritePropertyName("ClickCount");
                        jw.Write(dr["ClickCount"].ToString());
                        jw.WritePropertyName("Sort");
                        jw.Write(dr["Sort"].ToString());

                        jw.WriteObjectEnd();
                    }
                    jw.WriteArrayEnd();
                    context.Response.Write(sb.ToString());
                }
                else
                {
                    context.Response.Write("{\"Status\":\"0\"}");//没有数据
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"Status\":\"ex\"}");//系统异常
            }
        }
        /// <summary>
        /// 获取新闻总条数
        /// </summary>
        /// <returns></returns>
        public void GetNewsCount(HttpContext context)
        {
            try
            {
                //类别
                string typeId = context.Request.QueryString["typeId"];
                BLL.NewsInfo dataProviderProductInfo = new BLL.NewsInfo();
                string num = dataProviderProductInfo.GetRecordCount("IsDeleted=0 and NewsTypeId=" + typeId).ToString();
                context.Response.Write("{\"Count\":\"" + num + "\"}");
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"Status\":\"ex\"}");//系统异常
            }
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
