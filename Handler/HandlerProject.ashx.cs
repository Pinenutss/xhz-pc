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
    /// HandlerProject 的摘要说明
    /// </summary>
    public class HandlerProject : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request.Params["action"];
            if (!string.IsNullOrEmpty(action))
            {
                switch (action)
                {
                    case "GetAlbumList":
                        GetAlbumList(context);
                        break;
                    case "GetAlbumCount":
                        GetAlbumCount(context);
                        break;
                    case "GetAlbumTypeList":
                        GetAlbumTypeList(context);
                        break;
                    case "GetAlbumTypeCount":
                        GetAlbumTypeCount(context);
                        break;
                    case "GetAlbumNextOrLast":
                        GetAlbumNextOrLast(context);
                        break;
                    case "GetApartmentList":
                        GetApartmentList(context);
                        break;
                    case "GetApartmentCount":
                        GetApartmentCount(context);
                        break;
                    case "GetApartmentDetails":
                        GetApartmentDetails(context);
                        break;
                    case "GetVideoList":
                        GetVideoList(context);
                        break;
                    case "GetVideoCount":
                        GetVideoCount(context);
                        break;
                    case "GetVideoDetails":
                        GetVideoDetails(context);
                        break;
                }
            }
            context.Response.End();
        }

        #region 获取图册信息
        /// <summary>
        /// 获取图册类型列表
        /// </summary>
        /// <returns></returns>
        public void GetAlbumTypeList(HttpContext context)
        {
            try
            {
                BLL.AlbumType bll = new BLL.AlbumType();
                //参数
                string parentId = context.Request.QueryString["parentId"];
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
                DataTable dt = bll.GetListByPage("IsDeleted=0 and ParentId in(" + parentId + ")", " Sort desc", _startIndex, _endIndex).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //定义JsonWriter
                    LitJson.JsonWriter jw = new LitJson.JsonWriter(sb);
                    jw.WriteArrayStart();
                    foreach (DataRow dr in dt.Rows)
                    {
                        jw.WriteObjectStart();

                        //获取字段
                        jw.WritePropertyName("AlbumTypeId");
                        jw.Write(dr["AlbumTypeId"].ToString());
                        jw.WritePropertyName("AlbumTypeName");
                        jw.Write(dr["AlbumTypeName"].ToString());
                        jw.WritePropertyName("CoverImage");
                        jw.Write(Utils.GetAppSettings("Localhost") + dr["CoverImage"].ToString());
                        jw.WritePropertyName("ParentId");
                        jw.Write(dr["ParentId"].ToString());

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
        /// 获取图册总条数
        /// </summary>
        /// <returns></returns>
        public void GetAlbumTypeCount(HttpContext context)
        {
            try
            {
                //类别
                string parentId = context.Request.QueryString["parentId"];
                BLL.AlbumType bll = new BLL.AlbumType();
                string num = bll.GetRecordCount("IsDeleted=0 and ParentId in(" + parentId + ")").ToString();
                context.Response.Write("{\"Count\":\"" + num + "\"}");
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"Status\":\"ex\"}");//系统异常
            }
        }
        /// <summary>
        /// 获取图册照片列表
        /// </summary>
        /// <returns></returns>
        public void GetAlbumList(HttpContext context)
        {
            try
            {
                BLL.AlbumInfo bll = new BLL.AlbumInfo();
                //参数
                string typeId = context.Request.QueryString["typeId"];
                //定义StringBuilder
                StringBuilder sb = new StringBuilder();
                //获取列表
                DataTable dt = bll.GetList(0, "IsDeleted=0 and AlbumTypeId=" + typeId, " Sort desc").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //定义JsonWriter
                    LitJson.JsonWriter jw = new LitJson.JsonWriter(sb);
                    jw.WriteArrayStart();
                    foreach (DataRow dr in dt.Rows)
                    {
                        jw.WriteObjectStart();

                        //获取字段
                        jw.WritePropertyName("Id");
                        jw.Write(dr["Id"].ToString());
                        jw.WritePropertyName("AlbumTypeId");
                        jw.Write(dr["AlbumTypeId"].ToString());
                        jw.WritePropertyName("AlbumTitle");
                        jw.Write(dr["AlbumTitle"].ToString());
                        jw.WritePropertyName("CoverSmallImage");
                        jw.Write(Utils.GetAppSettings("Localhost") + dr["CoverSmallImage"].ToString());
                        jw.WritePropertyName("CoverBigImage");
                        jw.Write(Utils.GetAppSettings("Localhost") + dr["CoverBigImage"].ToString());
                        jw.WritePropertyName("CoverFont");
                        jw.Write(dr["CoverFont"].ToString());
                        jw.WritePropertyName("CreateDate");
                        jw.Write(dr["CreateDate"].ToString());
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
        /// 获取图册照片总条数
        /// </summary>
        /// <returns></returns>
        public void GetAlbumCount(HttpContext context)
        {
            try
            {
                //类别
                string typeId = context.Request.QueryString["typeId"];
                BLL.AlbumInfo bll = new BLL.AlbumInfo();
                string num = bll.GetRecordCount("IsDeleted=0 and AlbumTypeId=" + typeId).ToString();
                context.Response.Write("{\"Count\":\"" + num + "\"}");
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"Status\":\"ex\"}");//系统异常
            }
        }
        /// <summary>
        /// 获取上一个下一个
        /// </summary>
        /// <param name="context"></param>
        public void GetAlbumNextOrLast(HttpContext context)
        {
            try
            {
                //参数
                string typeId = context.Request.QueryString["typeId"];
                //定义StringBuilder
                StringBuilder sb = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(typeId))
                {
                    ///定义JsonWriter
                    LitJson.JsonWriter jw = new LitJson.JsonWriter(sb);
                    jw.WriteArrayStart();
                    jw.WriteObjectStart();

                    BLL.AlbumType bll = new BLL.AlbumType();
                    //当前
                    Model.AlbumType model = bll.GetModel(Convert.ToInt32(typeId));
                    if (model != null)
                    {

                        //上一个
                        DataTable dtlLast = bll.GetList(1," IsDeleted=0 and ParentId=" + model.ParentId + " and Sort>" + model.Sort, " Sort asc").Tables[0];
                        string LastAlbumTypeId = ""; string LastAlbumTypeName = ""; string LastParentId = "";string LastCoverImage = "/Images/NoPeoplePic.jpg";
                        if (dtlLast.Rows.Count > 0)
                        {
                            LastAlbumTypeId = dtlLast.Rows[0]["AlbumTypeId"].ToString();
                            LastAlbumTypeName = dtlLast.Rows[0]["AlbumTypeName"].ToString();
                            LastParentId = dtlLast.Rows[0]["ParentId"].ToString();
                            LastCoverImage = dtlLast.Rows[0]["CoverImage"].ToString();
                        }
                        jw.WritePropertyName("LastAlbumTypeId");
                        jw.Write(LastAlbumTypeId);
                        jw.WritePropertyName("LastAlbumTypeName");
                        jw.Write(LastAlbumTypeName);
                        jw.WritePropertyName("LastParentId");
                        jw.Write(LastParentId);
                        jw.WritePropertyName("LastCoverImage");
                        jw.Write(Utils.GetAppSettings("Localhost") + LastCoverImage);

                        //下一个
                        DataTable dtNext = bll.GetList(1," IsDeleted=0 and ParentId=" + model.ParentId + " and Sort<" + model.Sort, " Sort desc").Tables[0];
                        string NextAlbumTypeId = ""; string NextAlbumTypeName = ""; string NextParentId = ""; string NextCoverImage = "/Images/NoPeoplePic.jpg";
                        if (dtNext.Rows.Count > 0)
                        {
                            NextAlbumTypeId = dtNext.Rows[0]["AlbumTypeId"].ToString();
                            NextAlbumTypeName = dtNext.Rows[0]["AlbumTypeName"].ToString();
                            NextParentId = dtNext.Rows[0]["ParentId"].ToString();
                            NextCoverImage = dtNext.Rows[0]["CoverImage"].ToString();
                        }

                        jw.WritePropertyName("NextAlbumTypeId");
                        jw.Write(NextAlbumTypeId);
                        jw.WritePropertyName("NextAlbumTypeName");
                        jw.Write(NextAlbumTypeName);
                        jw.WritePropertyName("NextParentId");
                        jw.Write(NextParentId);
                        jw.WritePropertyName("NextCoverImage");
                        jw.Write(Utils.GetAppSettings("Localhost") + NextCoverImage);

                        jw.WriteObjectEnd();
                        jw.WriteArrayEnd();
                    }
                }
                context.Response.Write(sb.ToString());
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"Status\":\"ex\"}");//系统异常
            }
        }
        #endregion

        #region 获取文章/户型信息
        /// <summary>
        /// 获取文章/户型列表
        /// </summary>
        /// <returns></returns>
        public void GetApartmentList(HttpContext context)
        {
            try
            {
                BLL.ArticleInfo bll = new BLL.ArticleInfo();
                //类别
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
                DataTable dt = bll.GetListByPage("IsDeleted=0 and ArticleTypeId=" + typeId, " Sort desc", _startIndex, _endIndex).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //定义JsonWriter
                    LitJson.JsonWriter jw = new LitJson.JsonWriter(sb);
                    jw.WriteArrayStart();
                    foreach (DataRow dr in dt.Rows)
                    {
                        jw.WriteObjectStart();

                        //获取字段
                        jw.WritePropertyName("Id");
                        jw.Write(dr["Id"].ToString());
                        jw.WritePropertyName("ArticleTypeId");
                        jw.Write(dr["ArticleTypeId"].ToString());
                        jw.WritePropertyName("ArticleTitle");
                        jw.Write(dr["ArticleTitle"].ToString());
                        jw.WritePropertyName("CoverSmallImage");
                        jw.Write(Utils.GetAppSettings("Localhost") + dr["CoverSmallImage"].ToString());
                        jw.WritePropertyName("ArticleContent");
                        jw.Write(dr["ArticleContent"].ToString());
                        jw.WritePropertyName("CoverFont");
                        jw.Write(dr["CoverFont"].ToString());
                        jw.WritePropertyName("CreateDate");
                        jw.Write(dr["CreateDate"].ToString());
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
        /// 获取文章/户型详情
        /// </summary>
        /// <param name="context"></param>
        public void GetApartmentDetails(HttpContext context)
        {
            try
            {
                BLL.ArticleInfo bll = new BLL.ArticleInfo();
                //参数
                string Id = context.Request.QueryString["Id"];
                //获取列表
                DataTable dt = bll.GetList("IsDeleted=0 and Id=" + Id).Tables[0];
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
                        jw.WritePropertyName("Id");
                        jw.Write(dr["Id"].ToString());
                        jw.WritePropertyName("ArticleTypeId");
                        jw.Write(dr["ArticleTypeId"].ToString());
                        jw.WritePropertyName("ArticleTitle");
                        jw.Write(dr["ArticleTitle"].ToString());
                        jw.WritePropertyName("CoverSmallImage");
                        jw.Write(Utils.GetAppSettings("Localhost") + dr["CoverSmallImage"].ToString());
                        jw.WritePropertyName("ArticleContent");
                        jw.Write(dr["ArticleContent"].ToString());
                        jw.WritePropertyName("CoverFont");
                        jw.Write(dr["CoverFont"].ToString());
                        jw.WritePropertyName("CreateDate");
                        jw.Write(dr["CreateDate"].ToString());
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
        /// 获取文章/户型总条数
        /// </summary>
        /// <returns></returns>
        public void GetApartmentCount(HttpContext context)
        {
            try
            {
                BLL.ArticleInfo bll = new BLL.ArticleInfo();
                //类别
                string typeId = context.Request.QueryString["typeId"];
                string num = bll.GetRecordCount("IsDeleted=0 and ArticleTypeId=" + typeId).ToString();
                context.Response.Write("{\"Count\":\"" + num + "\"}");
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"Status\":\"ex\"}");//系统异常
            }
        }
        #endregion

        #region 获取视频信息
        /// <summary>
        /// 获取视频列表
        /// </summary>
        /// <returns></returns>
        public void GetVideoList(HttpContext context)
        {
            try
            {
                BLL.VideoInfo bll = new BLL.VideoInfo();
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
                DataTable dt = bll.GetListByPage("IsDeleted=0", " Sort desc", _pageIndex, _endIndex).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //定义JsonWriter
                    LitJson.JsonWriter jw = new LitJson.JsonWriter(sb);
                    jw.WriteArrayStart();
                    foreach (DataRow dr in dt.Rows)
                    {
                        jw.WriteObjectStart();

                        //获取字段
                        jw.WritePropertyName("Id");
                        jw.Write(dr["Id"].ToString());
                        jw.WritePropertyName("VideoTitle");
                        jw.Write(dr["VideoTitle"].ToString());
                        jw.WritePropertyName("VideoLink");
                        jw.Write(dr["VideoLink"].ToString());
                        jw.WritePropertyName("CoverSmallImage");
                        jw.Write(Utils.GetAppSettings("Localhost") + dr["CoverSmallImage"].ToString());
                        jw.WritePropertyName("CoverBigImage");
                        jw.Write(Utils.GetAppSettings("Localhost") + dr["CoverBigImage"].ToString());
                        jw.WritePropertyName("CoverFont");
                        jw.Write(dr["CoverFont"].ToString());
                        jw.WritePropertyName("CreateDate");
                        jw.Write(dr["CreateDate"].ToString());
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
        /// 获取视频详情
        /// </summary>
        /// <param name="context"></param>
        public void GetVideoDetails(HttpContext context)
        {
            try
            {
                BLL.VideoInfo bll = new BLL.VideoInfo();
                //参数
                string Id = context.Request.QueryString["Id"];
                //获取列表
                DataTable dt = bll.GetList("IsDeleted=0 and Id=" + Id).Tables[0];
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
                        jw.WritePropertyName("Id");
                        jw.Write(dr["Id"].ToString());
                        jw.WritePropertyName("VideoTitle");
                        jw.Write(dr["VideoTitle"].ToString());
                        jw.WritePropertyName("VideoLink");
                        jw.Write(dr["VideoLink"].ToString());
                        jw.WritePropertyName("CoverSmallImage");
                        jw.Write(Utils.GetAppSettings("Localhost") + dr["CoverSmallImage"].ToString());
                        jw.WritePropertyName("CoverBigImage");
                        jw.Write(Utils.GetAppSettings("Localhost") + dr["CoverBigImage"].ToString());
                        jw.WritePropertyName("CoverFont");
                        jw.Write(dr["CoverFont"].ToString());
                        jw.WritePropertyName("CreateDate");
                        jw.Write(dr["CreateDate"].ToString());
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
        /// 获取视频总条数
        /// </summary>
        /// <returns></returns>
        public void GetVideoCount(HttpContext context)
        {
            try
            {
                BLL.VideoInfo bll = new BLL.VideoInfo();
                string num = bll.GetRecordCount("IsDeleted=0").ToString();
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
