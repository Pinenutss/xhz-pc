var pagesize = 6; //条目
var typeId = 2;
//调用分页函数
$(function() {
    var title = getQueryString("title");
    if (title == 1) {
        $('#cpgh').click();

    };
    if (title == 2) {
        $('#jzfg').click();

    };
    if (title == 3) {
        PageShow(0, pagesize);

    };
    _current(title);
});
function PageShow(pageindex, pagesize) {
    GetData(pageindex, pagesize);
    doDatePagerForControl(GetCount(), pagesize, function () { GetData(clickpage, pagesize); }, "pager");
}
//获取数据源
function GetData(pageindex, pagesize) {
    var html = "";
    var urlList = "/Handler/HandlerProject.ashx?action=GetApartmentList&typeId=" + typeId + "&pageSize=" + pagesize + "&pageindex=" + pageindex;
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
                    html += '<div class="grid-item">';
                    html += '<div class="img" style="background-image:url(' + item.CoverSmallImage + ')"><a href="wlc-details-h.html?Id=' + item.Id + '" class="mask"><b>查看详情</b></div></a>';
                    html += '<h4>' + item.ArticleTitle + '</h4>';
                    html += '</div>';
                });
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
    var urlCount = "/Handler/HandlerProject.ashx?action=GetApartmentCount&typeId=" + typeId;
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

$('#homehx').click(function () {
    $("#pager").html("");
    $("#list").html("");
    $(this).addClass('active').siblings().removeClass('active');
    PageShow(0, pagesize);
});

//点击产品规划获取数据
$('#cpgh').click(function() {
    $("#pager").html("");
    $('#list').html("<div class='grid-article' id='lists' style='margin-left:30px;color: #858585;'></div>");
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
    $("#lists").html(html);
});

//点击建筑风格获取数据
$('#jzfg').click(function() {
    $("#pager").html("");
    $('#list').html("<div class='grid-article' id='lists' style='margin-left:30px;color: #858585;'></div>");
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
    $("#lists").html(html);
});
