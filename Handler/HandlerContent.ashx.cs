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
    /// HandlerContent 的摘要说明
    /// </summary>
    public class HandlerContent : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request.Params["action"];
            if (!string.IsNullOrEmpty(action))
            {
                switch (action)
                {
                    case "GetColumnList":
                        GetColumnList(context);
                        break;
                    case "GetBannerList":
                        GetBannerList(context);
                        break;
                }
            }
            context.Response.End();
        }

        #region 获取单页信息
        /// <summary>
        /// 获取单页信息
        /// </summary>
        /// <returns></returns>
        public void GetColumnList(HttpContext context)
        {
            try
            {
                BLL.ColumnInfo bll = new BLL.ColumnInfo();
                //ID
                string Id = context.Request.QueryString["Id"];
                //定义StringBuilder
                StringBuilder sb = new StringBuilder();
                //获取列表
                DataTable dt = bll.GetList("IsDeleted=0 and Id="+ Id).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //定义JsonWriter
                    LitJson.JsonWriter jw = new LitJson.JsonWriter(sb);
                    jw.WriteArrayStart();
                    jw.WriteObjectStart();

                    //获取字段
                    jw.WritePropertyName("Id");
                    jw.Write(dt.Rows[0]["Id"].ToString());
                    jw.WritePropertyName("ColumnType");
                    jw.Write(dt.Rows[0]["ColumnType"].ToString());
                    jw.WritePropertyName("ColumnName");
                    jw.Write(dt.Rows[0]["ColumnName"].ToString());
                    jw.WritePropertyName("ColumnContent");
                    jw.Write(dt.Rows[0]["ColumnContent"].ToString());
                    jw.WritePropertyName("IsLink");
                    jw.Write(dt.Rows[0]["IsLink"].ToString());
                    jw.WritePropertyName("LinkAddress");
                    jw.Write(dt.Rows[0]["LinkAddress"].ToString());
                    jw.WritePropertyName("ClickCount");
                    jw.Write(dt.Rows[0]["ClickCount"].ToString());
                    jw.WritePropertyName("Sort");
                    jw.Write(dt.Rows[0]["Sort"].ToString());

                    jw.WriteObjectEnd();
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
        #endregion

        #region 获取轮播信息
        /// <summary>
        /// 获取轮播信息
        /// </summary>
        /// <returns></returns>
        public void GetBannerList(HttpContext context)
        {
            try
            {
                BLL.Carousel bll = new BLL.Carousel();
                //定义StringBuilder
                StringBuilder sb = new StringBuilder();
                //获取列表
                DataTable dt = bll.GetList("IsDeleted=0").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //定义JsonWriter
                    LitJson.JsonWriter jw = new LitJson.JsonWriter(sb);
                    jw.WriteArrayStart();
                    foreach (DataRow dr in dt.Rows)
                    {
                        jw.WriteObjectStart();

                        //获取字段
                        jw.WritePropertyName("CoverlImage");
                        jw.Write(Utils.GetAppSettings("Localhost") + dr["CoverlImage"].ToString());
                        jw.WritePropertyName("RedirectUrl");
                        jw.Write(dr["RedirectUrl"].ToString());
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
