using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data;
using DTcms.Common;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Core;
using Aliyun.Acs.Dysmsapi.Model.V20170525;
using Aliyun.Acs.Core.Exceptions;

namespace DTcms.BLL
{
    /// <summary>
    /// 阿里云平台-手机短信
    /// </summary>
    public partial class ali_message
    {
        //产品名称:云通信短信API产品
        private const string Product = "Dysmsapi";
        private const string Domain = "dysmsapi.aliyuncs.com";         //产品域名
        private const string RegionId = "cn-hangzhou";
        private const string EndpointName = "cn-hangzhou";

        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息

        public ali_message()
        {
        }

        /// <summary>
        /// 检查账户信息是否正确
        /// </summary>
        /// <returns></returns>
        public bool Exists()
        {
            if (string.IsNullOrEmpty(siteConfig.aliaccesskeyid)
                || string.IsNullOrEmpty(siteConfig.aliaccesskeysecret)
                || string.IsNullOrEmpty(siteConfig.alisignname))
            {
                return false;
            }
            return true;
        }

        // 这里的content就是短信的模板号
        public bool Send(string mobiles, string content, int pass, string param, out string msg)
        {
            //检查是否设置好短信账号
            if (!Exists())
            {
                msg = "短信配置参数有误，请完善后再提交！";
                return false;
            }

            //检查手机号码，如果超过2000则分批发送
            //int sucCount = 0; //成功提交数量
            string errorMsg = string.Empty; //错误消息
            string[] oldMobileArr = mobiles.Split(',');
            int batch = oldMobileArr.Length / 2000 + 1; //2000条为一批，求出分多少批

            for (int i = 0; i < batch; i++)
            {
                StringBuilder sb = new StringBuilder();
                int sendCount = 0; //发送数量
                int maxLenght = (i + 1) * 2000; //循环最大的数

                //检测号码，忽略不合格的，重新组合
                for (int j = 0; j < oldMobileArr.Length && j < maxLenght; j++)
                {
                    int arrNum = j + (i * 2000);
                    string pattern = @"^1\d{10}$";
                    string mobile = oldMobileArr[arrNum].Trim();
                    Regex r = new Regex(pattern, RegexOptions.IgnoreCase); //正则表达式实例，不区分大小写
                    Match m = r.Match(mobile); //搜索匹配项
                    if (m != null)
                    {
                        sendCount++;
                        sb.Append(mobile + ",");
                    }
                }

                //发送短信
                if (sb.ToString().Length > 0)
                {
                    try
                    {
                        SendImple(Utils.DelLastComma(sb.ToString()), content, param);
                    }
                    catch (Exception ex)
                    {
                        //报错到前端
                        errorMsg = ex.Message;
                    }
                }
            }

            //返回状态
            if (string.IsNullOrEmpty(errorMsg))
            {
                msg = "成功发送";
                return true;
            }

            msg = errorMsg;
            return false;
        }

        private void SendImple(string mobileNumber, string templateCode, string param)
        {
            IClientProfile profile = DefaultProfile.GetProfile(RegionId, siteConfig.aliaccesskeyid, siteConfig.aliaccesskeysecret);
            DefaultProfile.AddEndpoint(EndpointName, RegionId, Product, Domain);

            IAcsClient acsClient = new DefaultAcsClient(profile);
            SendSmsRequest request = new SendSmsRequest();
            SendSmsResponse response = null;

            try
            {
                //使用post提交
                request.Method = Aliyun.Acs.Core.Http.MethodType.POST;

                //必填:待发送手机号。支持以逗号分隔的形式进行批量调用，批量上限为1000个手机号码,
                //批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式
                request.PhoneNumbers = mobileNumber;

                //必填:短信签名-可在短信控制台中找到
                request.SignName = siteConfig.alisignname;

                //必填:短信模板-可在短信控制台中找到
                request.TemplateCode = templateCode;

                //可选:模板中的变量替换JSON串
                request.TemplateParam = param;

                //请求失败这里会抛ClientException异常
                response = acsClient.GetAcsResponse(request);

            }
            catch (Aliyun.Acs.Core.Exceptions.ServerException e)
            {
                throw new ApplicationException(e.ErrorCode);
            }
            catch (ClientException e)
            {
                throw new ApplicationException(e.ErrorCode);
            }
        }
    }

}
