<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bid_Step1.aspx.cs" Inherits="DTcms.Web.Bid_Step1" %>

<%@ Register Src="~/Control/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>
<%@ Register Src="~/Control/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/Control/DocBanner.ascx" TagPrefix="uc1" TagName="DocBanner" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title><%=config.webtitle %></title>
    <%="<meta content=\""+config.webkeyword+"\" name=\"keywords\" />" %>
    <%="<meta content=\""+config.webdescription+"\" name=\"description\" />" %>
    <link href="favicon.ico" rel="shortcut icon">
    <link href="style/doc.css" type="text/css" rel="stylesheet" />
    <link href="script/easydialog.css" rel="stylesheet" />
    <link href="css/swfupload_cus.css" rel="stylesheet" />
    <script src="scripts/jquery/jquery-1.10.2.min.js"></script>
    <script src="script/easydialog.js"></script>
    <script src="script/jquery.cookie.js"></script>
    <script src="script/common.js" type="text/javascript"></script>
    <script type="text/javascript" src="scripts/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="scripts/swfupload/swfupload.queue.js"></script>
    <script type="text/javascript" src="scripts/swfupload/swfupload.handlers.js"></script>
    <script>
        $(function () {
            //翻译语言下拉框集合
            var $selTRLanguageArr=$("select[name=selTRLanguage]");

            //翻译语言选择
            $selTRLanguageArr.change(function(){
                var $this=$(this);
                if($this.val()!=-1&&$selTRLanguageArr.find("option:selected[value="+$this.val()+"]").length==2)
                    $this.val("-1");
                reckonPrice();//计算价格
            }).blur(function(i,obj){
                var $this=$(this);
                var index=$selTRLanguageArr.index(this);
                if($this.val()==-1)
                    for (var i = index; i < $selTRLanguageArr.length; i++) {
                        $selTRLanguageArr.eq(i).val(i==$selTRLanguageArr.length-1?"-1":$selTRLanguageArr.eq(i+1).val());
                    }
            }).click(function(){
                //必须先选择国家
                if($("#selCountry").val()==-1){
                    $.dialogAlert("请先选择前往国家");
                    this.blur();
                    return;
                }
                //必须先选择上一翻译语言
                var index=$selTRLanguageArr.index(this);
                if(index){
                    var $prev=$selTRLanguageArr.eq(index-1);
                    if($prev.val()==-1){
                        $.dialogAlert("请选择"+$prev.prev("label").text());
                        this.blur();
                    }
                }
            });

            //前往国家
            $("#selCountry").change(function(){
                //置空
                $(".local_selected").empty();
                //重新选择
                $.each($("input:checkbox[name=cbxBidBusiness]:checked"),function(i,obj){
                    var $this=$(obj);
                    //选中项
                    $this.parent().clone().add($("#selCountry option:selected").attr("tagts")=="True"?$this.parent().clone().find("div").text("译源相符").end().addClass("ts"):"").appendTo($(".local_selected")).addClass("k").click(function () {
                        $(".local_selected input:checkbox[value=" + $this.val() + "]").parent().remove();//选中项移除
                        $("#divCertificateStyle .choice[tagBidBusiness="+$this.val()+"]").remove();//选择证件样式移除
                        $this[0].checked = false;
                        reckonPrice();//计算价格
                    });
                });
                //操作限制
                //初始化语言选择
                $selTRLanguageArr.val(-1).find("option:contains('中文原件不翻译')").hide();
                //$("select[name=selTRLanguage]:not(:first)").parent().show();
                var countryName=$("#selCountry option:selected").text();//国家
                switch(countryName){
                    //当前往国家是香港和日本两个国家时，第一翻译语言可出现中文不翻译选项，其它国家均不出现中文不翻译选项
                    case"中国香港":
                    case"日本":
                        $selTRLanguageArr.find("option:contains('中文原件不翻译')").show();
                        break;
                        //当前往国家是俄罗斯、奥地利、美国、韩国四个出现译原相符的国家时，不允许选择第二、三翻译语言
                    case"俄罗斯":
                    case"奥地利":
                    case"美国":
                    case"韩国":
                        $("select[name=selTRLanguage]:not(:first)").parent().hide()
                        break;
                }
                reckonPrice();//计算价格
            });

            //副本
            $("#txtCopyCount").change(function(){
                if(!this.value)
                    this.value=1;
                reckonPrice();//计算价格
            });

            //业务选择
            $("input:checkbox[name=cbxBidBusiness]").click(function () {
                var $this = $(this);
                //选中
                if (this.checked){
                    //选中项
                    $this.parent().clone().add($("#selCountry option:selected").attr("tagts")=="True"?$this.parent().clone().find("div").text("译源相符").end().addClass("ts"):"").appendTo($(".local_selected")).addClass("k").click(function () {
                        $(".local_selected input:checkbox[value=" + $this.val() + "]").parent().remove();//选中项移除
                        $("#divCertificateStyle .choice[tagBidBusiness="+$this.val()+"]").remove();//选择证件样式移除
                        $this[0].checked = false;
                        reckonPrice();//计算价格
                        //上传所需公证文本限制
                        checkUploadBidSource();
                    });
                    //证书样式
                    $.ajax({
                        url:"ashx/UserBid.ashx",
                        type:"GET",
                        data:{
                            option:"GetCertificateStyle",
                            bbsid:this.value
                        },
                        async:false,//同步
                        dataType:"JSON",
                        success: function(ret){
                            var styleHtml=[];
                            styleHtml.push("<div class=\"choice\" tagBidBusiness=\""+$this.val()+"\">");
                            styleHtml.push("    <strong>"+$this.parent().text().trim()+"：</strong>");
                            $.each(ret,function(i,obj){
                                styleHtml.push("    <span>");
                                styleHtml.push("        <label style=\"cursor: pointer;color:white;\"><input "+(!i?"checked=\"checked\"":"")+" type=\"radio\" name=\"radCertificateStyle"+$this.val()+"\" value=\""+obj.ID+"\">");
                                styleHtml.push("        "+obj.Title+"</label><strong class=\"tc\" tagsrc=\""+obj.ImgUrl+"\">【样式预览】</strong>");
                                styleHtml.push("    </span>");
                            });
                            styleHtml.push("</div>");
                            $(styleHtml.join("")).find(".tc").click(function(){
                                $(".yuyue_box").show().find("img").attr("src",$(this).attr("tagsrc"));
                            }).end().find("label").click(function(){
                                checkUploadBidSource();//上传所需公证文本限制
                            }).end().insertBefore($("#divCertificateStyle .clear"));
                        }
                    });
                }
                else{
                    $(".local_selected input:checkbox[value=" + $this.val() + "]").parent().remove();//选中项移除
                    $("#divCertificateStyle .choice[tagBidBusiness="+$this.val()+"]").remove();//相关选择证件样式移除
                }
                reckonPrice();//计算价格
                //上传所需公证文本限制
                checkUploadBidSource();
            });

            //业务对应翻译费用
            var bidTRLanguageArr=<%=new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(BidBusiness_TRLanguage)%>;

            //计算价格
            var reckonPrice = function () {
                var price = 0;
                //循环计算价格
                $.each($(".local_selected>:not(.ts)>input:checkbox:checked"),function(i,obj){
                    var $this=$(this);
                    //是否译源相符
                    var isTs=$("#selCountry option:selected").attr("tagts")=="True";
                    //副本费用
                    price+=parseInt($this.attr("copyprice"))*parseInt(($("#txtCopyCount").val())-1)*(isTs?2:1);
                    //翻译费用
                    $.each(bidTRLanguageArr,function(i,obj){
                        //翻译语言
                        if(obj.ID==$this.val()&&(obj.TRLanguageID==$("#selTRLanguage1").val()||obj.TRLanguageID==$("#selTRLanguage2").val()||obj.TRLanguageID==$("#selTRLanguage3").val())){
                            //公证费用
                            price+=parseInt($this.attr("notaryprice"));
                            //翻译费用
                            price+=obj.TRPrice;
                            if(isTs)price+=80+obj.TRPrice;//译源相符项
                            return;
                        }
                    }); 
                });
                //输出
                $("#totalPrice").text(price);
            };

            $("#btnTrue").click(function(i,obj){
                if($selTRLanguageArr.val()==-1){
                    $.dialogAlert("请选择翻译语言");
                    return;
                }
                if($("#selCountry").val()==-1){
                    $.dialogAlert("请选择前往国家");
                    return;
                }
                if(!$(".local_selected>:not(.ts)>input:checkbox:checked").length){
                    $.dialogAlert("请选择申办业务");
                    return;
                }
                //是否必须上传所需公证文本
                var hasBidSourceFile=checkUploadBidSource();
                if(hasBidSourceFile&&!$("#trUploadBidSource .file").length){
                    $.dialogAlert("请上传所需公证文本");
                    return;
                }
                var data=function(){
                    var TRLanguage=[];//翻译语言
                    $.each($selTRLanguageArr,function(i,obj){
                        if(obj.value!=-1)
                            TRLanguage.push(obj.value)
                    });
                    //前往国家
                    var Country=$("#selCountry").val();
                    //用途
                    var Purpose=$("[name=radPurpose]:checked").val();
                    //申办业务
                    var BidBusiness=[];
                    $.each($(".local_selected>:not(.ts)>input:checkbox:checked"),function(i,obj){
                        BidBusiness.push({
                            BidBusiness:obj.value,//申办业务
                            CertificateStyle:$("input:radio[name=radCertificateStyle"+obj.value+"]:checked").val()||0//证书样式
                        });
                    });
                    //副本数量
                    var CopyCount=$("#txtCopyCount").val();
                    //申办数据
                    var bidData={};
                    //所需公证文本
                    var BidSourceFile=hasBidSourceFile?{
                        Path:$("#trUploadBidSource .file").attr("tagsrc"),
                        NeedTranslation:$("#trUploadBidSource input[name=radNeedTranslation]:checked").val()
                    }:null;
                    if($.cookie("BidData"))
                        bidData=eval("("+$.cookie("BidData")+")");
                    return $.extend(bidData,{
                        TRLanguage:TRLanguage,
                        Country:Country,
                        Purpose:Purpose,
                        BidBusiness:BidBusiness,
                        CopyCount:CopyCount,
                        BidSourceFile:BidSourceFile
                    });
                }();
                //装载
                $.cookie("BidData",JSON.stringify(data),{
                    path:"/"
                });
                location.href="Bid_Step2.aspx"+location.search;
            });
            //样式预览弹框隐藏
            $(".yuyue_box .bg").click(function(){
                $(".yuyue_box").hide();
            });


            //上传所需公证文本限制
            var checkUploadBidSource=function(){
                //出生公证（直接证）、结婚公证（直接证）、学历公证（直接证）、受或未受刑公证、亲属关系公证无需上传
                var checkBidbusinessArr=[6,10,18,15,20];
                //返回结果
                var ret=false;
                //检查
                $.each($("#divCertificateStyle .choice"),function(){
                    var $this=$(this);
                    //有其他选项、检查证件样式
                    if($(checkBidbusinessArr).index(parseInt($this.attr("tagbidbusiness")))==-1||$this.find(":radio").index($this.find(":radio:checked"))>0){
                        ret=true;
                        return false;
                    }
                });
                if(ret)$("#trUploadBidSource").show();
                else $("#trUploadBidSource").hide();
                return ret;
            };

            //数据绑定
            if($.cookie("BidData")){
                //申办数据
                var bidData=eval("("+$.cookie("BidData")+")");
                //翻译语言
                $.each(bidData.TRLanguage,function(i,obj){
                    $selTRLanguageArr.eq(i).val(obj);
                });
                //用途
                $("input:radio[name=radPurpose][value="+bidData.Purpose+"]").click();
                //前往国家
                $("#selCountry").val(bidData.Country);
                //副本
                $("#txtCopyCount").val(bidData.CopyCount);
                //业务选择以及证书样式
                $.each(bidData.BidBusiness,function(i,obj){
                    var $obj= $("input:checkbox[name=cbxBidBusiness][value="+obj.BidBusiness+"]");
                    $obj[0].checked=false;
                    $obj.click()
                    $("input:radio[name=radCertificateStyle"+obj.BidBusiness+"][value="+obj.CertificateStyle+"]").click();
                });
                //上传所需公证文本
                if(checkUploadBidSource()){
                    if(bidData.BidSourceFile){
                        $("#trUploadBidSource .pshow").html("<a href=\"javascript:;\" onclick=\"$(this).parent().empty();\">[删除]</a><strong class=\"file\" style=\"color:green;\" tagsrc=\""+bidData.BidSourceFile.Path+"\">"+bidData.BidSourceFile.Path.substring(bidData.BidSourceFile.Path.lastIndexOf("/")+1)+"</strong>");
                        $("#trUploadBidSource input[name=radNeedTranslation][value="+bidData.BidSourceFile.NeedTranslation+"]")[0].checked=true;
                    }
                }
            }

            //初始化上传控件
            $("#trUploadBidSource .upload-img").each(function () {
                var $this = $(this);
                //初始化批量上传
                $this.InitSWFUpload({
                    btntext: "上传",
                    btnwidth: 66,
                    filesize: "20480",
                    sendurl: "tools/upload_ajax.ashx?filetype=bidsourcefile",
                    flashurl: "scripts/swfupload/swfupload.swf",
                    filetypes: "*.doc;*.docx;*.pdf;*.zip;*.rar;",
                    uploadSuccess: function (file, serverData) {
                        var serverData = eval("(" + serverData + ")");//返回值
                        $this.next(".pshow").html("<a href=\"javascript:;\" onclick=\"$(this).parent().empty();\">[删除]</a><strong class=\"file\" style=\"color:green;\" tagsrc=\""+serverData.path+"\">"+serverData.path.substring(serverData.path.lastIndexOf("/")+1)+"</strong>");
                    }
                });
            });

        });
    </script>
    <style>
        .choice {
            margin: 8px;
        }
    </style>
</head>
<body>
    <uc1:Header runat="server" ID="Header" />
    <uc1:DocBanner runat="server" ID="DocBanner" />
    <!--顶部banner-->
    <div class="w">
        <div class="style_but3">在线申办流程指南</div>
        <div class="step_bz" style="background-position: 0px 0px"></div>
        <div class="w_bz">
            <div class="title">
                <h3 class="f_l">选择申办的业务</h3>
                <div class="f_r">预收费用：<em id="totalPrice">0</em>元</div>
            </div>
            <div class="c">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <label>前往什么国家：</label></td>
                        <td><span class="k">
                            <select name="selCountry" id="selCountry">
                                <option value="-1">-请选择-</option>
                                <%
                                    Country.ForEach(p =>
                                    {
                                        Response.Write("<option tagts=\"" + p.IsTS + "\" value=\"" + p.ID + "\">" + p.Name + "</option>");
                                    });  
                                %>
                            </select>
                        </span>
                            <span class="k">公证书
                                <input type="number" value="1" id="txtCopyCount" min="1" style="width: 50px" />本
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td width="120">
                            <label>选择用途：</label></td>
                        <td>
                            <%
                                var isFirst = true;
                                Purpose.ForEach(p =>
                                {
                                    Response.Write("<label class=\"k\" style=\"cursor: pointer;font-weight:normal\">");
                                    Response.Write("    <input " + (isFirst ? "checked=\"checked\"" : string.Empty) + " name=\"radPurpose\" type=\"radio\" value=\"" + p.ID + "\">");
                                    Response.Write("    " + p.Name + "</label>");
                                    if (isFirst) isFirst = false;
                                });
                            %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>选择公证证词翻译语言：</label>
                        </td>
                        <td><span class="k">
                            <label>第一翻译语言</label>
                            <select name="selTRLanguage" id="selTRLanguage1">
                                <option value="-1">-请选择-</option>
                                <%
                                    for (int i = 0; i < TRLanguage.Count; i++)
                                    {
                                        Response.Write("<option value=\"" + TRLanguage[i].ID + "\">" + TRLanguage[i].Name + "</option>");
                                    }
                                %>
                            </select>
                        </span><span class="k" style="display:none">
                            <label>第二翻译语言</label>
                            <select name="selTRLanguage" id="selTRLanguage2">
                                <option value="-1">-请选择-</option>
                                <%
                                    TRLanguage.ForEach(p =>
                                    {
                                        Response.Write("<option value=\"" + p.ID + "\">" + p.Name + "</option>");
                                    });  
                                %>
                            </select>
                        </span><span class="k" style="display:none">
                            <label>第三翻译语言</label>
                            <select name="selTRLanguage" id="selTRLanguage3">
                                <option value="-1">-请选择-</option>
                                <%
                                    TRLanguage.ForEach(p =>
                                    {
                                        Response.Write("<option value=\"" + p.ID + "\">" + p.Name + "</option>");
                                    });  
                                %>
                            </select>
                        </span></td>
                    </tr>
                    <tr>
                        <td>
                            <label>已选择申办业务：</label></td>
                        <td>
                            <div class="local_selected">
                                <div class="clear"></div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>业务选择：</label></td>
                        <td>
                            <ul>
                                <%
                                    BidBusiness.FindAll(p => p.IsTop).ForEach(p =>
                                    {
                                        Response.Write("<li><label style=\"cursor: pointer;font-weight:normal\">");
                                        Response.Write("     <input notaryprice=\"" + p.NotaryPrice + "\" copyprice=\"" + p.CopyPrice + "\" name=\"cbxBidBusiness\" type=\"checkbox\" value=\"" + p.ID + "\">");
                                        Response.Write("     <div style=\"display:inline\">" + p.Name + "</div></label></li>");
                                    });
                                %>
                            </ul>
                            <div class="clear"></div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="qt">
                                <h3><span>其他</span></h3>
                                <div class="f" style="display: none">
                                    <ul>
                                        <%
                                            BidBusiness.FindAll(p => !p.IsTop).ForEach(p =>
                                            {
                                                Response.Write("<li><label style=\"cursor: pointer;font-weight:normal\">");
                                                Response.Write("     <input notaryprice=\"" + p.NotaryPrice + "\" copyprice=\"" + p.CopyPrice + "\" name=\"cbxBidBusiness\" type=\"checkbox\" value=\"" + p.ID + "\">");
                                                Response.Write("     <div style=\"display:inline\">" + p.Name + "</div></label></li>");
                                            });
                                        %>
                                    </ul>
                                    <div class="clear"></div>

                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>证书样式选择：</label>
                        </td>
                        <td>
                            <div class="type" id="divCertificateStyle">
                                <div class="clear"></div>
                            </div>
                        </td>
                    </tr>
                    <tr id="trUploadBidSource">
                        <td>
                            <label>上传所需公证文本：</label>
                        </td>
                        <td>
                            <div class="type">
                                是否需要翻译：
                                <label style="cursor: pointer; font-weight: normal" class="k">
                                    <input type="radio" value="True" name="radNeedTranslation" checked="checked">是
                                </label>
                                <label style="cursor: pointer; font-weight: normal" class="k">
                                    <input type="radio" value="False" name="radNeedTranslation">否
                                </label>
                                <br />
                                <br />
                                <div class="upload-box upload-img"></div>
                                <div class="pshow"></div>
                                <p class="red">
                                    1、您可自行翻译所需公证文本后上传翻译件（word或pdf）给我处制作公证书；
                                    <br />2、您自行提供的公证文本翻译件，因翻译错误导致公证书无法使用的，公证处将不承担任何责任且不予退还相关公证费用；
                                    <br />3、如需我处翻译，翻译公司将按照您需要翻译的材料和翻译的语言收取所需公证文本翻译费（不含公证证词翻译费）；
                                    <br />4、为便于公证书制作，请将清晰不影响阅读的扫描文件压缩后再上传，请上传zip和rar格式的文件，文件大小小于20M；
                                    <br />5、如需重新上传文件，请先删除原来已经上传的文件。
                                </p>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input class="style_but2" type="button" value="下一步" id="btnTrue"></td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="clear"></div>
    </div>
    <div class="yuyue_box" style="display: none;">
        <div class="bg"></div>
        <div class="yuyue_con">
            <img src="">
        </div>
    </div>
    <uc1:Footer runat="server" ID="Footer" />
    <script src="script/jQuery-carouFredSel.js" type="text/javascript"></script>
    <!--浮动框 -->
    <ul class="float-rig">
        <li class="l4 amn3"><b class="amn2 b4 png"></b></li>
        <li class="l5"><b class="amn2 b5 png"><a class="yuyue" style="width: 56px; height: 56px; display: block;"
            href="Single.aspx?cid=82" target="_blank"></a></b></li>
    </ul>
    <script>
        $(function () {
	
            $(".float-rig li").hover(function () {
                $(this).addClass("h");
            }, function () {
                $(this).removeClass("h");
            })
            $(".float-rig .b4").click(function () {
                $('body,html').animate({ scrollTop: 0 }, 500);
            })
        })
	
        $(window).scroll(function (e) {
            lk = location.pathname;
            if ($(this).scrollTop() >= 200) {
                $(".l4").fadeIn();
          
            } else {
                $(".l4").fadeOut();
          
            }
		
        });
    </script>
    <!--浮动框End -->
</body>
</html>
