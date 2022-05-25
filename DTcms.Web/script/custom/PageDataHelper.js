/*
分页拉取数据帮助JS
*/

(function () {
    /*
    设置分页
    option:参数
    {
        Container: "",//容器选择器
        Url: "",//请求地址
        PageSize: 10,//页大小
        PageIndex:0,//当前页
        Data: { },//请求参数
        htmlJoinFunc: function (htmlArr, i, obj) { },//HTML拼接回调(待拼接HTML数组,索引,对象),
        IniCallFunc:function (totalCount){ }//初始化结束回调
    }
    */
    $.fn.SetPagination = function (option) {
        var $this = this;//当前JQ对象
        var $container = $(option.Container)//数据容器
        $this.empty();//清空内容
        //默认值
        option = $.extend({
            PageSize: 10,//页大小
            PageIndex: 0,//当前页
        }, option);
        //获取数据源
        $.ajax({
            type: "Get",//请求方式
            url: option.Url,
            dataType: "JSON",
            cache:false,//禁止缓存
            data: $.extend({
                PageIndex: option.PageIndex,//页索引
                PageSize: option.PageSize
            }, option.Data)//参数
        }).done(function (data) {
            var isFirst = true;
            //加载数据
            var htmlArr = [];
            $.each(data.list, function (i, obj) {
                //输出格式
                option.htmlJoinFunc(htmlArr, i, obj);
            });
            //输出
            $container.html(htmlArr.join(""));
            //分页参数设置
            parseInt(data.totalCount) && $this.pagination(data.totalCount, {
                callback: function (pageIndex, domObj) {
                    //$container.empty();//清空数据
                    //拼接HTML
                    if (isFirst)
                        isFirst = false;// 初始化回调不请求数据
                    else//请求数据
                        $.ajax({
                            type: "Get",//请求方式
                            url: option.Url,
                            dataType: "JSON",
                            cache: false,//禁止缓存
                            data: $.extend({
                                PageIndex: pageIndex,//页索引
                                PageSize: option.PageSize
                            }, option.Data)//参数
                        }).done(function (data) {
                            var htmlArr = [];
                            $.each(data.list, function (i, obj) {
                                //输出格式
                                option.htmlJoinFunc(htmlArr, i, obj);
                            });
                            //输出
                            $container.html(htmlArr.join(""));
                        });
                },
                text_text: "当前页",
                text_text_0: "条信息",
                prev_text: "<",
                next_text: ">",
                items_per_page: option.PageSize,
                num_display_entries: 4,
                current_page: option.PageIndex,
                num_edge_entries: 1,
                link_to: "javascript:;"
            });
            //初始化结束回调
            option.IniCallFunc && option.IniCallFunc(data.totalCount);
        });
    };
})()