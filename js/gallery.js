var typeId = getQueryString("typeId");
var parentId = getQueryString("parentId");
$(function () {
    var title = "";
    if (parentId == 9) {
        title = "黑龙滩洲际旅游酒店";
    }
    else if (parentId == 8) {
        title = "生态运动公园";
    } else if (parentId == 7) {
        title = "鲜花小镇";
    }
    else if (parentId == 6) {
        title = "生态观光农场";
    }
    else if (parentId == 5) {
        title = "马术俱乐部";
    }
    else if (parentId == 4) {
        title = "游艇会所";
    }
    else if (parentId == 10) {
        title = "图册";
    }
    else if (parentId == 11) {
        title = "周边环境";
    }
    $("#navTitle").text(title);
    AlbumNext();
    var html = ""; var html2 = "";
    var urlList = "/Handler/HandlerProject.ashx?action=GetAlbumList&typeId=" + typeId;
    $.ajax({
        url: urlList,
        type: "post",
        async: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Status == "0") {
                html += "<h4><center>暂无数据！</center></h4>";
                $('#js_gallery,#js_gallery_2').hide();
                $('.article-wrap').append(html);
            } else if (data.Status == "ex") {
                alert("系统异常！");
            } else {
                $(data).each(function (i, item) {
                    html += "<div class='swiper-slide' style='background-image:url(" + item.CoverBigImage + ")'></div>";
                    html2 += "<div class='swiper-slide'><div class='img-container'><a href='javascript:;' style='background-image:url(" + item.CoverSmallImage + ")'></a></div></div>";
                });
            }
        },
        error: function () {
            alert("系统异常！");
        }
    });
    $("#js_gallery .swiper-wrapper").html(html);
    $("#js_gallery_2 .swiper-wrapper").html(html2);
    var swiper1 = new Swiper('#js_gallery .swiper-container', {
        nextButton: '.swiper-button-next',
        prevButton: '.swiper-button-prev',
        // loop: true,
        onSlideChangeStart: function () {
            var index = swiper1.activeIndex;
            console.log("big" + index);
            swiper2.swipeTo(index);
        }
    });
    var swiper2 = new Swiper('#js_gallery_2 .swiper-container', {
        direction: 'horizontal',
        slidesPerView: 5,
        offsetSlidesAfter: 2,
        onSlideTouch: function () {
            var index = Number(swiper2.clickedSlideIndex);
            console.log("small" + index);
            swiper1.swipeTo(index);
            $('#js_gallery_2').find('.swiper-slide').eq(index).addClass('active');
            // return index;
        }
    });
    $('.swiper-button-prev').on('click', function (e) {
        e.preventDefault();
        swiper1.swipePrev();
    });
    $('.swiper-button-next').on('click', function (e) {
        e.preventDefault();
        swiper1.swipeNext();
    });
});
function AlbumNext() {
    var urlList2 = "/Handler/HandlerProject.ashx?action=GetAlbumNextOrLast&typeId=" + typeId;
    $.ajax({
        url: urlList2,
        type: "post",
        async: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Status == "0") {
                html += "<div>暂无数据！</div>";
            }
            else if (data.Status == "ex") {
                alert("系统异常！");
            }
            else {
                $(data).each(function (i, item) {
                    if (item.LastAlbumTypeId != "") {
                        $("#imgLast").attr("style", "background-image:url(" + item.LastCoverImage + ")");
                        $("#btnLast").text(item.LastAlbumTypeName).attr("href", "tpl-inner-gallery.html?typeId=" + item.LastAlbumTypeId + "&parentId=" + item.LastParentId);
                    }
                    else {
                        $("#btnLast").text("已经是第一页了").attr("href", "tpl-inner-gallery.html?typeId=" + typeId + "&parentId=" + parentId);
                    }
                    if (item.NextAlbumTypeId != "") {
                        $("#imgNext").attr("style", "background-image:url(" + item.NextCoverImage + ")");
                        $("#btnNext").text(item.NextAlbumTypeName).attr("href", "tpl-inner-gallery.html?typeId=" + item.NextAlbumTypeId + "&parentId=" + item.NextParentId);
                    }
                    else {
                        $("#btnNext").text("已经是最后一页了").attr("href", "tpl-inner-gallery.html?typeId=" + typeId + "&parentId=" + parentId);
                    }
                });
            }
        },
        error: function () {
            alert("系统异常！");
        }
    });
}
