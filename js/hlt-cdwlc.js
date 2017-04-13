var pagesize = 6; //条目
var parentId = "9,8,7,6,5,4";//默认为“项目配套”下的所有图册
// $(function() {
//     //首次加载
//     PageShow(0, pagesize);
// });
$(function() {
    var title = getQueryString("title");
    if (title == 1) {
        $('#xmgh').click();

    } else if (title == 2) {
        $('#xmpt').click();

    } else {
        $('#jtwz').click();

    }
    _current(title);
});
//调用分页函数
function PageShow(pageindex, pagesize) {
    GetData(pageindex, pagesize);
    doDatePagerForControl(GetCount(), pagesize, function () { GetData(clickpage, pagesize); }, "pager");
}
//获取数据源
function GetData(pageindex, pagesize) {
    var html = "";
    var urlList = "/Handler/HandlerProject.ashx?action=GetAlbumTypeList&parentId=" + parentId + "&pageSize=" + pagesize + "&pageindex=" + pageindex;
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
                $(data).each(function (i, item) {
                    //绑定数据
                    html += "<div class='grid-item'>";
                    html += "<div class='img' style='background-image:url(" + item.CoverImage + ")'><a href='tpl-inner-gallery.html?typeId=" + item.AlbumTypeId + "&parentId=" + item.ParentId + "' class='mask'><b>查看详情</b></a></div>";
                    html += "<h4>" + item.AlbumTypeName + "</h4></div>";
                });
            }
        },
        error: function() {
            alert("系统异常！");
        }
    });
    $("#grid-wrap-list").html(html);
}
//获取总条数
function GetCount() {
    var num = 0;
    var urlCount = "/Handler/HandlerProject.ashx?action=GetAlbumTypeCount&parentId=" + parentId;
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
//点击项目配套图册获取数据
$('#xmpt').click(function () {
    $("#pager").html("");
    $("#list").html("");
    $(".nav-content").show();
    $('#grid-wrap-list').html("");
    $(this).addClass('active').siblings().removeClass('active');
    PageShow(0, pagesize);
});
$(".nav-content li").click(function () {
    $(this).addClass('active').siblings().removeClass('active');
    parentId = $(this).attr("ids");
    PageShow(0, pagesize);
});

//点击项目规划获取数据
$('#xmgh').click(function() {
    $("#pager").html("");
    $("#list").html("");
    $(".nav-content").hide();
    $('#grid-wrap-list').html("<div class='grid-article' id='list' style='margin-left:30px;color: #858585;'></div>");
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

//点击交通位置获取数据
$('#jtwz').click(function() {
    $("#pager").html("");
    $("#list").html("");
    $(".nav-content").hide();
    $('#grid-wrap-list').html("<div class='grid-article' id='list' style='margin-left:30px;color: #858585;'></div>");
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
