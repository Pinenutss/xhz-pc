$(function() {

    $(window).bind('resize', function() {
        var winHeight = $(window).height();
        $('#js_swiper, .swiper-slide').css('height', winHeight - 196);
    }).trigger('resize');
    $('body').css('overflow', 'hidden');

    var html = "";
    var urlList = "/Handler/HandlerContent.ashx?action=GetBannerList";

    $.ajax({
        url: urlList,
        type: "post",
        async: true,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(data) {
            if (data.Status == "0") {
                html += "<div class='swiper-slide' style='background-image:url(img/banner-1.jpg)'></div>";
            } else if (data.Status == "ex") {
                alert("ϵͳ�쳣��");
            } else {
                $(data).each(function(i, item) {
                    //��������
                    html += "<div class='swiper-slide' style='background-image:url(" + item.CoverlImage + ")'></div>";
                });
                $("#banner").html(html);

                var winHeight = $(window).height();
                $('#js_swiper, .swiper-slide').css('height', winHeight - 196);
                var swiper = new Swiper('.swiper-container', {
                    loop: true,
                    autoplay: 3500,
                    pagination: '.swiper-pagination',
                    paginationClickable: true,
                    autoplayDisableOnInteraction: false,
                    nextButton: '.swiper-button-next',
                    prevButton: '.swiper-button-prev'
                });

                $('.swiper-button-prev').on('click', function() {
                    swiper.swipePrev();
                });
                $('.swiper-button-next').on('click', function() {
                    swiper.swipeNext();
                });
            }

            console.log(data);
        },
        error: function() {
            alert("ϵͳ�쳣��");
        }
    });



});
