$(function () {
    ajaxLoad = !1;
    if (!$.browser.msie || $.browser.msie && 10 < $.browser.version) ajaxLoad = !0
});

function displayContent() {
    $("#content_wrapper").is(":visible") || ($.browser.msie && 8 > $.browser.version ? ($("div.main").remove(), $("body").append($("<div>").attr({
        id: "error_unsupported-browser"
    }))) : ($("noscript").remove(), $("html, div.unsupported").removeClass("unsupported"), initSuperNav(), initMainNav(), ajaxLoad && initLinks(), initClickEvents()))
}

function getABFromGuid(a, b) {
    for (var c = a.substring(b, 1), d = "01234567".split(""), f = 0; f < d.length; f++)
        if (d[f] === c) return "a";
    return "b"
}

function postLoad(a) {
    window.location.hash && (document.title = "undefined" != typeof $("#page_header").attr("metatitle") ? $("#page_header").attr("metatitle") : $("#header_main").attr("metatitle"));
    0 == $("#header_nav_top").find("li[id=top_" + a + "]").length ? $("#header_nav").find("a,ul").removeClass("hovered active") : 0 == $("#header_nav_sub").find("ul[id=nav_sub_" + a + "]").length && activeNavs(a);
    showScrolls();
    $("div.page_body div, div.page_body ul, div.page_body iframe").removeClass("scrollHide")
}

function writePageTitle(a) {
    a = a.replace(/[^a-zA-Z0-9\.]+/g, "");
    title[a] && (document.title = title[a], $("meta[name=description]").attr("content", metadesc[a]))
}

function initSuperNav() {
    $(window).scroll(function () {
        $("#supernav_wrapper").css("top", Math.max(-20, -1 * $(this).scrollTop()))
    })
}

function refreshSuperNav(a) {
    $.get("/content/main/supernav.php", function (b) {
        "login" != a && $("#supernav_wrapper").html(b)
    })
}

function initMainNav() {
    $("#header_nav_top li a").mouseenter(function () {
        resetNav();
        $("#header_nav_top li a, #header_nav_sub ul").removeClass("hovered");
        $(this).addClass("hovered");
        $(this).attr("sub") && $("#header_nav_sub").find("ul#" + $(this).attr("sub")).addClass("hovered")
    });
    $("#header_nav_sub ul li a").mouseenter(function () {
        $("#header_nav_sub ul li a").removeClass("hovered");
        $(this).addClass("hovered")
    });
    $("#header_nav").mouseleave(function () {
        resetNav()
    });
    $("#header_nav").on("click", "a", function () {
        if ($(this).hasClass("sublink")) {
            var a =
                $(this).closest("ul").attr("id").split("nav_sub_")[1],
                b = $(this).attr("id").split("sub_")[1];
            activeNavs(a, b)
        } else $(this).hasClass("toplink") && (a = $(this).parent().attr("id").split("top_")[1], activeNavs(a))
    })
}

function activeNavs(a, b) {
    $("#header_nav").find("a,ul").removeClass("hovered active");
    $("ul#header_nav_top li#top_" + a).find("a").addClass("active");
    $("div#header_nav_sub ul#nav_sub_" + a).addClass("active");
    $("div#header_nav_sub ul#nav_sub_" + a).find("a#sub_" + b).addClass("active");
    $("#header_nav").find(".active").addClass("hovered")
}

function resetNav() {
    $("#header_nav").find("a,ul").removeClass("hovered");
    $("#header_nav").find(".active").addClass("hovered")
}

function initLinks() {
    $("#content, #supernav").on("click", "a.content", function (a) {
        $(this).hasClass("social") || parseHash($(this));
        a.preventDefault()
    })
}

function parseHash(a) {
	/*
	var b = $(a).attr("href").split("/");
    b[1] = "#" + b[1];
    contentURL = b.join("/");
	*/
    "_blank" == $(a).attr("target") ? window.open(contentURL) : window.location = contentURL
}

function hawkenAlert(a, b, c) {
    $.fancybox("<div id='alert_dialog'><div class='alert_body'><p>" + a + "</p></div><div class='alert_buttons'><button id='alert_dialog_ok' class='w250h40 yellow'>" + alert.ok + "</button></div></div>", {
        autoSize: !1,
        closeBtn: !1,
        wrapCSS: "popupBorder",
        width: c,
        height: b,
        padding: 0,
        helpers: {
            overlay: null
        }
    });
    $("#alert_dialog_ok").click(function () {
        $.fancybox.close()
    })
}

function hawkenError(a, b, c) {
    $.fancybox("<div id='alert_error'><div class='alert_body'><p>" + alert.errorOccurred + "</p><p>" + a + "</p></div><div class='alert_buttons'><button id='alert_error_ok' class='w250h40 yellow'>" + alert.ok + "</button></div></div>", {
        autoSize: !1,
        closeBtn: !1,
        wrapCSS: "popupBorder",
        width: c,
        height: b,
        padding: 0,
        helpers: {
            overlay: null
        }
    });
    $("#alert_error_ok").click(function () {
        $.fancybox.close()
    })
}

function initClickEvents() {
    /*$("#content, #supernav").on("click", "a[clickEvent]", function () {
        sendEvent("PlayHawkenWebsite", "0", "Player", "0", "Clicked", "Button", $(this).attr("clickEvent"), !1)
    })
	*/
}

function sendEvent(a, b, c, d, f, j, g, h) {
    /*
	"undefined" === typeof h && (h = !0);
    var e = {};
    readCookie("phaccess") ? e.visitorGuid = checkForCookie("phtracking") : readCookie("phtracking") && (d = readCookie("phtracking"));
    readCookie("phrefer") ? e.referralSource = readCookie("phrefer") : readCookie("phintrefer") && readCookie("phintrefer") != g && (e.prev = readCookie("phintrefer"));
    e = encodeURIComponent(JSON.stringify(e));
    $.ajax({
        type: "POST",
        async: h,
        url: "/storm/user/sendEvent.php",
        data: "%7B%7D" != e ? "producerType=" + a + "&producerID=" + b + "&subjectType=" +
            c + "&subjectID=" + d + "&verb=" + f + "&targetType=" + j + "&targetID=" + g + "&eventDataString=" + e : "producerType=" + a + "&producerID=" + b + "&subjectType=" + c + "&subjectID=" + d + "&verb=" + f + "&targetType=" + j + "&targetID=" + g,
        success: function () {}
    })
	*/
}

function sendVideoEvent(a, b) {
    /*
	sendEvent("PlayHawkenWebsite", "0", "Player", "0", b, "Video", a)
	*/
}

function showScrolls() {
    $(".custom").tinyscrollbar();
    $(".scroller.default .scrollbar").each(function () {
        $(this).hasClass("disable") || $(this).fadeIn()
    })
}

function logoutUser(a) {
    /*
	var b = readCookie("phaccess");
    void 0 == b ? a && void 0 != a ? window.location = a : window.location.reload() : (eraseCookie("phaccess"), $.ajax({
        type: "POST",
        url: "/storm/user/logoutUser.php",
        data: "AccessGrant=" + b,
        success: function () {
            a && void 0 != a ? window.location = a : window.location.reload()
        }
    }))
	*/
}

function createCookie(a, b, c) {
    if (c) {
        var d = new Date;
        d.setTime(d.getTime() + 864E5 * c);
        c = "; expires=" + d.toGMTString()
    } else c = "";
    document.cookie = a + "=" + b + c + "; path=/"
}

function readCookie(a) {
    a += "=";
    for (var b = document.cookie.split(";"), c = 0; c < b.length; c++) {
        for (var d = b[c];
            " " == d.charAt(0);) d = d.substring(1, d.length);
        if (0 == d.indexOf(a)) return d.substring(a.length, d.length)
    }
    return null
}

function checkForCookie(a) {
    if (readCookie(a)) return readCookie(a)
}

function eraseCookie(a) {
    createCookie(a, "", -1)
}

function isValidEmail(a) {
    return -1 == a.search(/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i) ? !1 : -1 != a.indexOf("..") || -1 != a.indexOf(".@") || -1 != a.indexOf("@.") ? !1 : !0
}
var isOpera = !(!window.opera || !window.opera.version),
    isFirefox = testCSS("MozBoxSizing"),
    isSafari = 0 < Object.prototype.toString.call(window.HTMLElement).indexOf("Constructor"),
    isChrome = !isSafari && testCSS("WebkitTransform"),
    isIE = testCSS("msTransform");

function testCSS(a) {
    return a in document.documentElement.style
}

function saveReferralLink(a) {
    "-1" != a.indexOf("playhawken.com") && "-1" == a.indexOf("community.playhawken.com") || "-1" != a.indexOf("localhost") || (document.cookie = "phrefer=" + a + "; expires=0; path=/")
}

function loginUser(a, b) {
    /*
	if (document.referrer && "-1" != document.referrer.indexOf("community.playhawken.com")) var c = document.referrer;
    $.ajax({
        type: "POST",
        url: "/storm/user/loginUser.php",
        data: "EmailAddress=" + encodeURIComponent(a) + "&Password=" + encodeURIComponent(b),
        success: function (a) {
            a = $.parseJSON(a);
            200 <= a.Status && 300 > a.Status ? (a = c ? c.replace("http://", "https://") : window.location.href.replace("http://", "https://"), "undefined" != typeof modalTimer ? (clearTimeout(modalTimer), $.get("/content/main/update_modal.php?mt=2",
                function (a) {
                    $("div.fancybox-inner").html(a);
                    $("span#update_close").click(function () {
                        window.location.reload()
                    })
                })) : window.location.href == a ? window.location.reload() : window.location.href = a) : hawkenError(a.Status + " - " + a.Message, 150, 400)
        }
    })
	*/
}

function accountUpdateModal(a) {
    /*
	$.get("/content/main/update_modal.php?mt=1&callsign=" + a, function (a) {
        $.fancybox(a, {
            autoResize: !1,
            fitToView: !1,
            closeBtn: !1,
            wrapCSS: "updateModal",
            topRatio: 1,
            autoSize: !0,
            padding: 0,
            margin: 0,
            helpers: {
                overlay: {
                    closeClick: !1,
                    css: {
                        background: "url('/images/main/update-bg-repeat.png')"
                    },
                    speedIn: 5E3,
                    speedOut: 5E3
                }
            }
        })
    })
	*/
}

function showUpdateModal(a, b, c) {
    /*
	accountUpdateModal(c);
    updateModalContent(0);
    loginUser(a, b)
	*/
}

function updateModalContent(a) {
    /*
	a++;
    modalTimer = setTimeout(function () {
        updateModalContent(a)
    }, 1E3)
	*/
};