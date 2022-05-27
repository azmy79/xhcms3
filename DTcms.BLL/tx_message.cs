using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Cryptography;
using DTcms.Common;
using System.Net;

namespace DTcms.BLL
{
    /// <summary>
    /// 腾讯云平台-手机短信
    /// </summary>
    public partial class tx_message
    {
        private const string URL = "https://sms.tencentcloudapi.com";
        private const string EndpointName = "sms.tencentcloudapi.com";
        private const string APPID = "1400500512";
        private const string Service = "sms";
        private const string Region = "ap-guangzhou";
        private const string Action = "SendSms";
        private const string Version = "2021-01-11";

        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息

        public tx_message()
        {
        }

        /// <summary>
        /// 检查账户信息是否正确
        /// </summary>
        /// <returns></returns>
        public bool Exists()
        {
            if (string.IsNullOrEmpty(siteConfig.txaccesskeyid)
                || string.IsNullOrEmpty(siteConfig.txaccesskeysecret)
                || string.IsNullOrEmpty(siteConfig.txsignname))
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

            //检查手机号码，如果超过200则分批发送 190
            //int sucCount = 0; //成功提交数量
            string errorMsg = string.Empty; //错误消息
            string[] oldMobileArr = mobiles.Split(',');
            int batch = oldMobileArr.Length / 190 + 1; //190条为一批，求出分多少批

            for (int i = 0; i < batch; i++)
            {
                StringBuilder sb = new StringBuilder();
                int sendCount = 0; //发送数量
                int maxLenght = (i + 1) * 190; //循环最大的数

                //检测号码，忽略不合格的，重新组合
                for (int j = 0; j < oldMobileArr.Length && j < maxLenght; j++)
                {
                    int arrNum = j + (i * 190);
                    string pattern = @"^1\d{10}$";
                    string mobile = oldMobileArr[arrNum].Trim();
                    Regex r = new Regex(pattern, RegexOptions.IgnoreCase); //正则表达式实例，不区分大小写
                    Match m = r.Match(mobile); //搜索匹配项
                    if (m != null)
                    {
                        sendCount++;
                        var mob = "\"" + mobile + "\"";
                        sb.Append(mob + ",");
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
            string SECRET_ID = siteConfig.txaccesskeyid;
            string SECRET_KEY = siteConfig.txaccesskeysecret;

            // 实际调用需要更新参数，这里仅作为演示签名验证通过的例子
            //string requestPayload1 = "{\"SmsSdkAppId\":\"1400006666\",\"TemplateParamSet\":[\"12345\"],\"PhoneNumberSet\":[\"+8618511122266\"]," +
            //    "\"SessionContext\":\"test\",\"SignName\":\"腾讯云\",\"TemplateId\":\"1234\"}";

            string requestPayload = GetPayload(param, mobileNumber, templateCode);

            try
            {
                string result = "";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
                req.Method = "POST";

                DateTime date = DateTime.UtcNow;

                Dictionary<string, string> headers = BuildHeaders(SECRET_ID, SECRET_KEY, Service
                , EndpointName, Region, Action, Version, date, requestPayload);

                foreach (KeyValuePair<string, string> kv in headers)
                {
                    req.Headers.Add(kv.Key + ": " + kv.Value);
                }
                req.ContentType = "application/json; charset=utf-8";

                req.Referer = null;
                req.AllowAutoRedirect = true;
                req.Accept = "*/*";

                byte[] data = Encoding.UTF8.GetBytes(requestPayload);
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                }

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        private string GetPayload(string paramset, string numberset, string TemplateId)
        {
            string payload = "{";
            payload = payload + "\"PhoneNumberSet\":[" + numberset + "],";

            payload = payload + "\"SmsSdkAppId\":\"" + APPID + "\",";
            payload = payload + "\"SignName\":\"" + siteConfig.txsignname + "\",";
            payload = payload + "\"TemplateId\":\"" + TemplateId + "\",";
            payload = payload + "\"TemplateParamSet\":[" + paramset + "]";
            payload = payload + "}";

            return payload;
        }

        private string SHA256Hex(string s)
        {
            using (SHA256 algo = SHA256.Create())
            {
                byte[] hashbytes = algo.ComputeHash(Encoding.UTF8.GetBytes(s));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashbytes.Length; ++i)
                {
                    builder.Append(hashbytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private byte[] HmacSHA256(byte[] key, byte[] msg)
        {
            using (HMACSHA256 mac = new HMACSHA256(key))
            {
                return mac.ComputeHash(msg);
            }
        }

        private Dictionary<String, String> BuildHeaders(string secretid,
            string secretkey, string service, string endpoint, string region,
            string action, string version, DateTime date, string requestPayload)
        {
            string datestr = date.ToString("yyyy-MM-dd");
            DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            long requestTimestamp = (long)Math.Round((date - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero) / 1000;
            // ************* 步骤 1：拼接规范请求串 *************
            string algorithm = "TC3-HMAC-SHA256";
            string httpRequestMethod = "POST";
            string canonicalUri = "/";
            string canonicalQueryString = "";
            string contentType = "application/json";
            string canonicalHeaders = "content-type:" + contentType + "; charset=utf-8\n" + "host:" + endpoint + "\n";
            string signedHeaders = "content-type;host";
            string hashedRequestPayload = SHA256Hex(requestPayload);
            string canonicalRequest = httpRequestMethod + "\n"
                + canonicalUri + "\n"
                + canonicalQueryString + "\n"
                + canonicalHeaders + "\n"
                + signedHeaders + "\n"
                + hashedRequestPayload;
            Console.WriteLine(canonicalRequest);

            // ************* 步骤 2：拼接待签名字符串 *************
            string credentialScope = datestr + "/" + service + "/" + "tc3_request";
            string hashedCanonicalRequest = SHA256Hex(canonicalRequest);
            string stringToSign = algorithm + "\n" + requestTimestamp.ToString() + "\n" + credentialScope + "\n" + hashedCanonicalRequest;
            Console.WriteLine(stringToSign);

            // ************* 步骤 3：计算签名 *************
            byte[] tc3SecretKey = Encoding.UTF8.GetBytes("TC3" + secretkey);
            byte[] secretDate = HmacSHA256(tc3SecretKey, Encoding.UTF8.GetBytes(datestr));
            byte[] secretService = HmacSHA256(secretDate, Encoding.UTF8.GetBytes(service));
            byte[] secretSigning = HmacSHA256(secretService, Encoding.UTF8.GetBytes("tc3_request"));
            byte[] signatureBytes = HmacSHA256(secretSigning, Encoding.UTF8.GetBytes(stringToSign));
            string signature = BitConverter.ToString(signatureBytes).Replace("-", "").ToLower();
            Console.WriteLine(signature);

            // ************* 步骤 4：拼接 Authorization *************
            string authorization = algorithm + " "
                + "Credential=" + secretid + "/" + credentialScope + ", "
                + "SignedHeaders=" + signedHeaders + ", "
                + "Signature=" + signature;
            Console.WriteLine(authorization);

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", authorization);
            //headers.Add("Host", endpoint);
            //headers.Add("Content-Type", contentType + "; charset=utf-8");
            headers.Add("X-TC-Timestamp", requestTimestamp.ToString());
            headers.Add("X-TC-Version", version);
            headers.Add("X-TC-Action", action);
            headers.Add("X-TC-Region", region);
            return headers;
        }

    }

}
