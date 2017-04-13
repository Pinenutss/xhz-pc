var pagesize = 6; //条目
$(function() {
    var title = getQueryString("title");
    if (title == 1) {
        $('#tc').click();
    } else {
        $('#zbhj').click();
    }
    _current(title);
});
//调用分页函数
function PageShow(pageindex, pagesize) {
    GetData(pageindex, pagesize);
    doDatePagerForControl(GetCount(), pagesize, function () {
        GetData(clickpage, pagesize);
    }, "pager");
}
//获取数据源
function GetData(pageindex, pagesize) {
    var html = "";
    var urlList = "/Handler/HandlerProject.ashx?action=GetAlbumTypeList&parentId=" + typeId + "&pageSize=" + pagesize + "&pageindex=" + pageindex;
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
                    html += '<div class="grid-item">'
                    html += '<div class="img" style="background-image:url(' + item.CoverImage + ')"><a href="tpl-inner-gallery-g.html?typeId=' + item.AlbumTypeId + '&parentId=' + item.ParentId + '" class="mask"><b>查看详情</b></div></a>'
                    html += '<h4>' + item.AlbumTypeName + '</h4>'
                    html += '</div>'
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
    var urlCount = "/Handler/HandlerProject.ashx?action=GetAlbumTypeCount&parentId=" + typeId;
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

$('#tc').click(function() {
    $("#pager").html("");
    $("#list").html("");
    $(this).addClass('active').siblings().removeClass('active');
    typeId = $(this).attr("ids");
    PageShow(0, pagesize);
})

$('#zbhj').click(function() {
    $("#pager").html("");
    $("#list").html("");
    $(this).addClass('active').siblings().removeClass('active');
    typeId = $(this).attr("ids");
    PageShow(0, pagesize);
})
