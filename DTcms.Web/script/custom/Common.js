
//获取缩略图路径
var GetThumbImg = function (path) {
    var pos1 = path.lastIndexOf('/');
    var pos2 = path.lastIndexOf('\\');
    var pos = Math.max(pos1, pos2)
    if (pos < 0)
        return path;
    else {
        var fileName = path.substring(pos + 1);
        return path.replace(fileName, "thumb_" + fileName);
    }
}

//获取文章详情链接
var GetNews_ViewUrl = function (newsObj) {
    return "News_View.aspx?cid=" + newsObj.CID + "&id=" + newsObj.ID;
}


//字符串截取(超出用省略号代替)
String.prototype.ToShortString = function (length) {
    return this.length > length ? this.substring(0, length - 3) + "..." : this;
};


//时间戳转时间
String.prototype.ToDate = function () {
    return new Date(parseInt(this.replace("/Date(", "").replace(")/", "")));
};


// 日期格式化  
// 格式 YYYY/yyyy/YY/yy 表示年份  
// MM/M 月份  
// W/w 星期  
// dd/DD/d/D 日期  
// hh/HH/h/H 时间  
// mm/m 分钟  
// ss/SS/s/S 秒  
Date.prototype.ToString = function (str) {
    var Week = ['日', '一', '二', '三', '四', '五', '六'];
    str = str.replace(/yyyy|YYYY/, this.getFullYear());
    str = str.replace(/yy|YY/, (this.getYear() % 100) > 9 ? (this.getYear() % 100).toString() : '0' + (this.getYear() % 100));
    str = str.replace(/MM/, (this.getMonth() + 1) > 9 ? (this.getMonth() + 1).toString() : '0' + (this.getMonth() + 1));
    str = str.replace(/M/g, this.getMonth() + 1);
    str = str.replace(/w|W/g, Week[this.getDay()]);
    str = str.replace(/dd|DD/, this.getDate() > 9 ? this.getDate().toString() : '0' + this.getDate());
    str = str.replace(/d|D/g, this.getDate());
    str = str.replace(/hh|HH/, this.getHours() > 9 ? this.getHours().toString() : '0' + this.getHours());
    str = str.replace(/h|H/g, this.getHours());
    str = str.replace(/mm/, this.getMinutes() > 9 ? this.getMinutes().toString() : '0' + this.getMinutes());
    str = str.replace(/m/g, this.getMinutes());
    str = str.replace(/ss|SS/, this.getSeconds() > 9 ? this.getSeconds().toString() : '0' + this.getSeconds());
    str = str.replace(/s|S/g, this.getSeconds());
    return str;
};

/*
* 智能机浏览器版本信息:
*/
var browser = {
    versions: function () {
        var u = navigator.userAgent, app = navigator.appVersion;
        //移动终端浏览器版本信息
        return {
            //IE内核
            trident: u.indexOf('Trident') > -1,
            //opera内核
            presto: u.indexOf('Presto') > -1,
            //苹果、谷歌内核
            webKit: u.indexOf('AppleWebKit') > -1,
            //火狐内核
            gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1,
            //是否为移动终端
            mobile: !!u.match(/AppleWebKit.*Mobile.*/),
            //ios终端
            ios: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/),
            //android终端或者uc浏览器
            android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1,
            //是否为iPhone或者QQHD浏览器
            iPhone: u.indexOf('iPhone') > -1 || u.indexOf('Mac') > -1,
            //是否iPad
            iPad: u.indexOf('iPad') > -1,
            //是否web应该程序，没有头部与底部
            webApp: u.indexOf('Safari') == -1
        };
    }(),
    language: (navigator.browserLanguage || navigator.language).toLowerCase()
};


/*
获取地址栏参数
key:参数键
*/
$.GetQueryString = function (key) {
    var reg = new RegExp("(^|&)" + key + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return decodeURIComponent(r[2]); return null;
};