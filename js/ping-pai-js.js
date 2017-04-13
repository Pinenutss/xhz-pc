var pagesize = 5; //条目
// var typeId = 1;
// var handlerName = "HandlerProject"; //数据文件位置
// var handlerType = "GetApartmentList"; //获取列表
// var handlerType_2 = "GetApartmentCount"; //获取总数
// $(function() {
//     //首次加载
//     PageShow(0, pagesize);
// })

//页面加载
$(function() {
    var title = getQueryString("title");
    if (title == 1) {
        $('#gsjs').click();

    } else {
        $('#wlcxl').click();

    }
    _current(title);
});
//调用分页函数
function PageShow(pageindex, pagesize) {
    GetData(pageindex, pagesize);
    doDatePagerForControl(GetCount(), pagesize, function() {
        GetData(clickpage, pagesize);
    }, "pager");
}
//获取数据源
function GetData(pageindex, pagesize) {
    var html = "";
    var urlList = "/Handler/" + handlerName + ".ashx?action=" + handlerType + "&typeId=" + typeId + "&pageSize=" + pagesize + "&pageindex=" + pageindex;
    $.ajax({
        url: urlList,
        type: "post",
        async: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(data) {
            if (data.Status == "0") {
                html += "<div>暂无数据！</div>";
            } else if (data.Status == "ex") {
                alert("系统异常！");
            } else {
                console.log(data);
                $(data).each(function(i, item) {
                    //绑定数据
                    html += "<div class='grid-item clearfix'>";
                    html += "<a href='wlc-details.html?Id="+item.Id+"'> <div class='img' style='background-image:url(" + item.CoverSmallImage + ")'></div>";
                    html += "<div class='content'>";
                    html += "<h4>" + item.ArticleTitle + "</h4>";
                    html += "<p class='des'>" + item.CoverFont + "</p>";
                    html += "<span class='tag tag-progress'>查看详情</span> ";
                    html += "</div></a>";
                    html += "</div>";
                });

                $('.content-wrap').on("click",".grid-item",function(){
                   var index = $(this).index();
                   var content_html = data[index].ArticleContent;
                   $('.grid-item').hide();
                   $("#pager").html("");
                   $('.grid-article').append(content_html);
                })
            }
        },
        error: function() {
            alert("系统异常！");
        }
    });
    $("#list").html(html);
}
//获取总条数
function GetCount() {
    var num = 0;
    var urlCount = "/Handler/" + handlerName + ".ashx?action=" + handlerType_2 + "&typeId=" + typeId;
    $.ajax({
        url: urlCount,
        type: "post",
        async: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(data) {
            if (data.Status == "ex") {
                alert("系统异常！");
            } else {
                num = data.Count;
            }
        },
        error: function() {
            alert("系统异常！");
        }
    });
    return num;
}
// 点击未来城 获取数据
$('#wlcxl').click(function() {
    $("#list").html("");
    typeId = $(this).attr("ids");
    handlerName = $(this).attr("data-hn");
    handlerType_2 = $(this).attr("data-ht-2");
    handlerType = $(this).attr("data-ht");
    $(this).addClass('active').siblings().removeClass('active');
    PageShow(0, pagesize);
});

//点击公司介绍获取数据
$('#gsjs').click(function() {
    $("#pager").html("");
    $("#list").html("");
    var _id = $(this).attr("ids");
    $(this).addClass('active').siblings().removeClass('active');
    var html = "";
    var urlList = "/Handler/HandlerContent.ashx?action=GetColumnList&Id=" + _id;
    $.ajax({
        url: urlList,
        type: "post",
        async: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(data) {
            if (data.Status == "0") {
                html += "<div>暂无数据！</div>";
            } else if (data.Status == "ex") {
                alert("系统异常！");
            } else {
                html = data[0]["ColumnContent"];
            }
        },
        error: function() {
            alert("系统异常！");
        }
    });
    $("#list").html(html);
});
