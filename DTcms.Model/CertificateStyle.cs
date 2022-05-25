using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//CertificateStyle
		public class CertificateStyle
	{
   		     
      	/// <summary>
		/// ID
        /// </summary>		
		private int _id;
        public int ID
        {
            get{ return _id; }
            set{ _id = value; }
        }        
		/// <summary>
		/// 申办业务ID
        /// </summary>		
		private int _bidbusinessid;
        public int BidBusinessID
        {
            get{ return _bidbusinessid; }
            set{ _bidbusinessid = value; }
        }        
		/// <summary>
		/// 公证书类型名称
        /// </summary>		
		private string _title;
        public string Title
        {
            get{ return _title; }
            set{ _title = value; }
        }        
		/// <summary>
		/// 图片地址 
        /// </summary>		
		private string _imgurl;
        public string ImgUrl
        {
            get{ return _imgurl; }
            set{ _imgurl = value; }
        }        
		/// <summary>
		/// 描述
        /// </summary>		
		private string _memo;
        public string Memo
        {
            get{ return _memo; }
            set{ _memo = value; }
        }        
		/// <summary>
		/// 排序
        /// </summary>		
		private int _sort;
        public int Sort
        {
            get{ return _sort; }
            set{ _sort = value; }
        }        
		   
	}
}

