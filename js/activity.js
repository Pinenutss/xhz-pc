var articleHandler = function() {
    $('.article').on('mouseenter', function() {
        var date = $(this).find('.date'),
            cont = $(this).find('.content'),
            arrow = $(this).find('i');
        arrow.css('color', '#252525');
        date.velocity({translateX: 30});
        cont.velocity({translateX: 30});
    }).on('mouseleave', function() {
        var date = $(this).find('.date'),
            cont = $(this).find('.content'),
            arrow = $(this).find('i');
        arrow.css('color', '#b0b0b0');
        date.velocity({
            translateX: 0
        }, 150);
        cont.velocity({
            translateX: 0
        }, 150);
    })
}
var pagesize = 5; //条目
var typeId = 1;
// $(function () {
//     //首次加载
//     // PageShow(0, pagesize);
// });
//调用分页函数
$(function() {
    var title = getQueryString("title");
    if (title == 1) {
        $('#xw').click();
    } else if (title == 2) {
        $('#hdhd').click();
    } else {
        $('#sqhd').click();
    }
    _current(title);
});
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
                var activet_type = "";
                if ($('#hdhd').hasClass('active')) {
                    activet_type = 1;
                }
                if ($('#sqhd').hasClass('active')) {
                    activet_type = 2;
                }

                if (handlerType == "GetActivityList") {
                    $(data).each(function(i, item) {

                        //绑定数据
                        var str = "";
                        var co = "";
                        if (item.Status == 1) {
                            str = "正在进行";
                            co = "progress";
                        } else if (item.Status == 2) {
                            str = "即将开始";
                            co = "pending";
                        } else if (item.Status == 3) {
                            str = "已结束";
                            co = "finish";
                        }

                        var _time = item.StartDate+"~"+item.EndDate;
                        html += "<div class='grid-item clearfix'><a href='activity-hd-detail.html?Id=" + item.ActivityId + "&activet_type=" + activet_type + "'>";
                        html += "<div class='img' style='background-image:url(" + item.CoverSmallImage + ")'></div>";
                        html += "<div class='content'>";
                        html += "<h4>" + item.ActivityName + "</h4>";
                        html += "<p class='des'>" + item.CoverFont + "</p>";
                        html += "<p>活动时间：" + _time + "</p>";
                        html += "<p>活动地点：" + item.ActivityAddress + "</p>";
                        html += "<span class='tag tag-" + co + "'>" + str + "</span>";
                        html += "</div></a>";
                        html += "</div>";
                    });
                    $("#list").html(html);

                } else if (handlerType == "GetNewsList") {
                    // console.log(data);
                    $(data).each(function(i, item) {
                        //绑定数据
                        var _year = data[i].CreateDate.substring(0, 4);

                        html += '<div class="article clearfix"><a href="news-article-detail.html?Id=' + data[i].NewsId + '">'
                        html += '<div class="date">'
                        html += '<h3>' + '3.20' + '</h3><h5>' + _year + '</h5>'
                        html += '</div>'
                        html += '<div class="content">'
                        html += '<h4><a href="news-article-detail.html?Id=' + data[i].NewsId + '">' + data[i].NewsTitle + '</a></h4>'
                        html += '<p>' + data[i].CoverFont + '</p>'
                        html += '</div>'
                        html += '<i class="fa fa-arrow-right"></i>'
                        html += '</div>'
                    });
                    $("#list").html(html);
                    articleHandler();

                }

            }
        },
        error: function() {
            alert("系统异常！");
        }
    });

}
//获取总条数
function GetCount() {
    var num = 0;
    var urlCount = "/Handler/HandlerActivity.ashx?action=" + handlerType_2 + "&typeId=" + typeId;
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

//点击互动活动
$('#hdhd').click(function() {
    $("#pager").html("");
    $(".grid-article").html("");
    $(".article-wrap").html("");

    typeId = $(this).attr("ids");
    handlerName = $(this).attr("data-hn");
    handlerType_2 = $(this).attr("data-ht-2");
    handlerType = $(this).attr("data-ht");
    $(this).addClass('active').siblings().removeClass('active');
    PageShow(0, pagesize);
})
//点击社群活动
$('#sqhd').click(function() {
    $("#pager").html("");
    $(".grid-article").html("");
    $(".article-wrap").html("");
    typeId = $(this).attr("ids");
    handlerName = $(this).attr("data-hn");
    handlerType_2 = $(this).attr("data-ht-2");
    handlerType = $(this).attr("data-ht");
    $(this).addClass('active').siblings().removeClass('active');
    PageShow(0, pagesize);
})
//点击获取新闻
$('#xw').click(function() {
    $("#pager").html("");
    $(".grid-article").html("");
    $(".article-wrap").html("");
    typeId = $(this).attr("ids");
    handlerName = $(this).attr("data-hn");
    handlerType_2 = $(this).attr("data-ht-2");
    handlerType = $(this).attr("data-ht");
    $(this).addClass('active').siblings().removeClass('active');
    PageShow(0, pagesize);
})
