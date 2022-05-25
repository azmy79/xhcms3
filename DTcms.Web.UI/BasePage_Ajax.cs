using DTcms.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace DTcms.Web.UI
{
    /// <summary>
    /// AJAX请求父类
    /// </summary>
    public class BasePage_Ajax : IHttpHandler, IRequiresSessionState
    {
        public Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig();
        public Model.userconfig userConfig = new BLL.userconfig().loadConfig();
        public JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        public BasePage_Ajax()
        {

        }

        public virtual void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 输出信息
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public string ReturnMsg(string msg = null, bool status = true)
        {
            //JS序列化实例
            var jsSerializer = new JavaScriptSerializer();
            return jsSerializer.Serialize(new
            {
                status = status,
                msg = status ? string.Empty : msg
            });
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public DTcms.Model.users GetUserInfo()
        {
            return new BasePage().GetUserInfo();
        }


        //校检网站验证码
        public string verify_code(HttpContext context, string strcode)
        {
            if (string.IsNullOrEmpty(strcode))
            {
                return "{\"status\":0, \"msg\":\"对不起，请输入验证码！\"}";
            }
            if (context.Session[DTKeys.SESSION_CODE] == null)
            {
                return "{\"status\":0, \"msg\":\"对不起，验证码超时或已过期！\"}";
            }
            if (strcode.ToLower() != (context.Session[DTKeys.SESSION_CODE].ToString()).ToLower())
            {
                return "{\"status\":0, \"msg\":\"您输入的验证码与系统的不一致！\"}";
            }
            context.Session[DTKeys.SESSION_CODE] = null;
            return "success";
        }

        /// <summary>
        /// 发送手机验证码
        /// </summary>
        /// <param name="phoneNum">手机号码</param>
        /// <param name="minCount">过期时间/MIN</param>
        /// <returns></returns>
        public string SendMobileCode(string phoneNum, int minCount)
        {
            var js = new System.Web.Script.Serialization.JavaScriptSerializer();
            if (HttpContext.Current.Session["MobileCode"] != null)
            {
                var sessionDic = HttpContext.Current.Session["MobileCode"] as Dictionary<string, object>;
                //时间间隔未到
                if (((DateTime)sessionDic["Time"]).AddMinutes(minCount) > DateTime.Now)
                {
                    return js.Serialize(new
                    {
                        status = false,
                        msg = "刚已发送过短信，请" + minCount + "分钟后再试！"
                    });
                }
            }
            var strcode = DTcms.Common.Utils.Number(4); //随机验证码
            var smsModel = new BLL.sms_template().GetModel("usercode"); //取得短信内容
            if (smsModel == null)
            {
                return js.Serialize(new
                {
                    status = false,
                    msg = "发送失败，短信模板不存在！"
                });
            }
            //替换标签
            var msgContent = smsModel.content;
            msgContent = msgContent.Replace("{code}", strcode);
            msgContent = msgContent.Replace("{valid}", "2");
            //发送短信
            var tipMsg = string.Empty;
            var result = new BLL.sms_message().Send(phoneNum, msgContent, 1, out tipMsg);
            if (!result)
            {
                return js.Serialize(new
                {
                    status = false,
                    msg = tipMsg
                });
            }
            //写入SESSION，保存验证码
            HttpContext.Current.Session["MobileCode"] = new Dictionary<string, object> { { "PhoneNum", phoneNum }, { "Code", strcode }, { "Time", DateTime.Now } };
            return js.Serialize(new
            {
                status = true
            });
        }

        /// <summary>
        /// 验证手机验证码
        /// </summary>
        /// <param name="phoneNum">手机号码</param>
        /// <param name="code">验证码</param>
        /// <param name="minCount">过期时间/MIN</param>
        /// <returns></returns>
        public bool CheckMobileCode(string phoneNum, string code, int minCount)
        {
            if (HttpContext.Current.Session["MobileCode"] == null) return false;//无数据
            var sessionDic = HttpContext.Current.Session["MobileCode"] as Dictionary<string, object>;
            if (!phoneNum.Equals(sessionDic["PhoneNum"])) return false;//不合法
            if (((DateTime)sessionDic["Time"]).AddMinutes(minCount) < DateTime.Now) return false;//已过期
            if (!code.Equals(sessionDic["Code"])) return false;//无效
            return true;
        }

        /// <summary>
        /// 输出信息
        /// </summary>
        /// <typeparam name="T">输出对象类型</typeparam>
        /// <param name="act">操作函数</param>
        public void ResponseWrite<T>(Action<T> act) where T : new()
        {
            var t = new T();//新建对象
            act(t);//执行操作函数
            System.Web.HttpContext.Current.Response.Write(jsSerializer.Serialize(t));//输出
        }

        /// <summary>
        /// 输出信息(常规)
        /// </summary>
        /// <param name="act">操作函数</param>
        public void ResponseWrite(Action<ReturnResult> act = null)
        {
            ResponseWrite<ReturnResult>(act ?? (p => { }));//输出信息
        }

        /// <summary>
        /// 常规返回数据对象
        /// </summary>
        public class ReturnResult
        {
            /// <summary>
            /// 状态
            /// </summary>
            public bool status = true;

            /// <summary>
            /// 信息
            /// </summary>
            public string msg = string.Empty;
        }
    }
}
