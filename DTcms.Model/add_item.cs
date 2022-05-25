using System;

namespace DTcms.Model
{
    /// <summary>
    /// 广告条实体类Adbanner
    /// </summary>
    [Serializable]
    public class ad_item
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ad_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime start_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime end_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ad_url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string link_url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string remarks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? is_lock { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime add_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int sort_id { get; set; }

        public string tag { get; set; }
    }
}