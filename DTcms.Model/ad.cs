using System;

namespace DTcms.Model
{
    /// <summary>
    /// 广告位实体类Advertising
    /// </summary>
    [Serializable]
    public class ad
    {
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int adtype { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remarks { get; set; }
        /// <summary>
        /// 广告数量
        /// </summary>
        public int num { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public int price { get; set; }
        /// <summary>
        /// 宽
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// 高
        /// </summary>
        public int height { get; set; }
        /// <summary>
        /// target
        /// </summary>
        public string target { get; set; }

        public int sort_id { get; set; }
    }
}