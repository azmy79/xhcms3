

$(function () {

    $("div.nav li a:not(.selected)").parent("li").hover(function () {
        $(this).find("div.nav-down").show(0);
        $(this).find("a.a").addClass("selected");
    }, function () {
        $(this).find("div.nav-down").hide(0);
        $(this).find("a.a").removeClass("selected");
    });

    $("div.nav li a.selected").parent("li").hover(function () {
        $(this).find("div.nav-down").show(0);
    }, function () {
        $(this).find("div.nav-down").hide(0);
    });

    $("div.by a").hover(function () {
        $(this).animate({ width: '100px', height: '50px' }, 100);
    }, function () {
        $(this).stop(true, true).animate({ width: '75px', height: '40px' }, 100);
    });

    $("a.ewm").hover(function () {
        $(this).find("div.drop").show(100);
    }, function () {
        $(this).find("div.drop").hide(100);
    });

    $("div.cave a:even").css("float", "right");
    $("div.w_news_c:eq(3)").css("float", "right").css("margin-right", "0px");

    $("div.cls_al li:even").css("background", "#f0f0f0");
    $("div.w_login").animate({ top: '150px', opacity: '0.9' }, 1000);
    $("div.list_dd tr:eq(0)").css("background", "#666666");



    $("div.qt h3").click(function () {
        $(this).next("div.f").stop(true, true).slideToggle("slow");
        $(this).toggleClass("selected");
    });

    $("div.change_034 span").click(function () {
        $(this).addClass("curr").siblings().removeClass();
        $("#" + $(this).attr("lang")).show().siblings().hide();
    });

});

$(function () {
    var _scroll = {
        delay: 1000,
        easing: 'linear',
        items: 1,
        duration: 0.04,
        timeoutDuration: 0,
        pauseOnHover: 'immediate'
    };
    $('#ticker-1').length && $('#ticker-1').carouFredSel({
        width: 850,
        align: false,
        items: {
            width: 'variable',
            height: 35,
            visible: 1
        },
        scroll: _scroll
    });

    $('#ticker-2').length && $('#ticker-2').carouFredSel({
        width: 1000,
        align: false,
        circular: false,
        items: {
            width: 'variable',
            height: 33,
            visible: 1
        },
        scroll: _scroll
    });

    //	set carousels to be 100% wide
    $('.caroufredsel_wrapper').css('width', '100%');

    //	set a large width on the last DD so the ticker won't show the first item at the end

});

//ºÊ»›IE8Õÿ’π∑Ω∑®
if (!String.prototype.trim)
    String.prototype.trim = function () {
        return $.trim(this);
    }