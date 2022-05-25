using System;

namespace DTcms.Model
{
    [Serializable]
    public class feedback
    {
        public feedback()
        {
            user_tel = "";
            user_qq = "";
            user_name = "";
            title = "";
            reply_content = "";
            is_lock = 0;
            content = "";
            add_time = DateTime.Now;
            UserID = 0;
        }

        public DateTime add_time { get; set; }

        public string content { get; set; }

        public int id { get; set; }

        public int is_lock { get; set; }

        public string reply_content { get; set; }

        public DateTime? reply_time { get; set; }

        public string title { get; set; }

        public string user_email { get; set; }

        public string user_name { get; set; }

        public string user_qq { get; set; }

        public string user_tel { get; set; }

        /// <summary>
        /// 用户ID        
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int MsgType { get; set; }
    }
}

