// var head_meta = (function () {
//     var edge_meta_html = '<meta name="format-detection" content="telephone=no">';
//     $('title').before(edge_meta_html);
// })()

var navHandler = (function () {
    var $menu = $('[data-nav]');
    var isOpen = false;
    $menu.on('mouseenter', function () {
        if (!isOpen) {
            $('.sub-mask').velocity({
                opacity: 1
            }, { display: 'block' });
            $('.sub-nav').velocity({
                opacity: 1
            }, { display: 'block' });
            isOpen = true;
        }
    });
    $('.header').on('mouseleave', function () {
        if (isOpen) {
            $('.sub-mask').velocity({
                opacity: 0
            }, { display: 'none' });
            $('.sub-nav').velocity({
                opacity: 0
            }, { display: 'none' });
            isOpen = false;
        }
    })
})()

var navHandler = (function () {
    var $nav = $('.top-nav li:first-child');
    $nav.css('margin-left', '0');
})()


var _footer = (function () {
    var footer_html = '<div class="container-fluid">';
    footer_html += '<div class="footer clearfix row">';
    footer_html += '  <ul class="col-lg-6 col-md-6 col-sm-12 clearfix">';
    footer_html += '  <li><i class="fa fa-phone-square"></i> 028-85333338</li>';
    footer_html += '  <li><i class="fa fa-map-marker"></i> 成都天府新区南黑龙滩度假区</li>';
    footer_html += '  <li><a href="javascript:;" id="js_reserve" class="js_reserve" data-type="1"><i class="fa fa-clock-o"></i> 预约</a></li>';
    footer_html += '  <li><a href="javascript:;" class="js_reserve" data-type="2"><i class="fa fa-qrcode"></i> 关注</a></li>';
    footer_html += '  </ul>';
    footer_html += '  <p class="col-lg-6 col-md-6 col-sm-12 ">版权为长岛黑龙滩生态城所有 转载必究 蜀ICP备xxxxxxxx号-x 技术支持 <a href="http://pc.cdzmd.com" target="_blank">志明达科技</a></p>';
    footer_html += '</div>';
    footer_html += '</div>';
    $('#_footer').after(footer_html);

})()


var reserveBtn = (function () {
    var btn = $('.js_reserve'),
        isOpen = false;
    btn.on('click', function () {
        var a = $(this).attr('data-type');
        if (!isOpen) {
            dialog.open(a);
            isOpen = true;
        }
    })
    $('body').on('click', '.mask', function () {
        if (isOpen) {
            dialog.close();
            isOpen = false;
        }
    })

})()

var dialog = {
    open: function (data) {
        if (data == 1) {
            var str = '<div class="mask"><div class="dialog"><div class="dialog-hd"><i class="fa fa-clock-o"></i> 预约看房</div><div class="dialog-bd"><p><i class="fa fa-phone"></i> 服务电话 : 028-85333338</p><p><i class="fa fa-map-marker"></i> 看房地址 : 成都天府新区黑龙滩度假区</p></div></div></div>';
        }
        if (data == 2) {
            var str = '<div class="mask"><div class="qr-img"><img src="img/qr-code.jpg"></img></div></div>';
        }
        $('body').append(str);
        $('.mask').velocity({
            opacity: 1
        }, { visibility: 'visible' });
        this.flag = true;
    },
    close: function () {
        $('.mask').velocity({
            opacity: 0
        }, { visibility: 'hidden' });
        setTimeout(function () {
            $('.mask').remove();
        }, 200);
    }
}

var Ajax = function (url, type, success, error) {
    $.ajax({
        url: url,
        type: type,
        dataType: 'json',
        timeout: 10000,
        contentType: "application/json; charset=utf-8",
        success: function (d) {
            var data = d;
            success && success(data);
        },
        error: function (e) {
            error && error(e);
        }
    });
};

function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null)
        return unescape(r[2]);
    return null;
}

var _current = function (title) {

    $('.sub-nav').find('.current').eq((title - 1)).css({ "background-color": "#62c2d0", "color": "#fff" })
}
