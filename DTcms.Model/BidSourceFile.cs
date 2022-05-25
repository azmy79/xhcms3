using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//BidSourceFile
		public class BidSourceFile
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
		/// 申办ID
        /// </summary>		
		private int _bidid;
        public int BidID
        {
            get{ return _bidid; }
            set{ _bidid = value; }
        }        
		/// <summary>
		/// 标题
        /// </summary>		
		private string _title;
        public string Title
        {
            get{ return _title; }
            set{ _title = value; }
        }        
		/// <summary>
		/// 问价路径
        /// </summary>		
		private string _path;
        public string Path
        {
            get{ return _path; }
            set{ _path = value; }
        }        
		/// <summary>
		/// 是否需要翻译
        /// </summary>		
		private bool _needtranslation;
        public bool NeedTranslation
        {
            get{ return _needtranslation; }
            set{ _needtranslation = value; }
        }        
		/// <summary>
		/// 备注
        /// </summary>		
		private string _memo;
        public string Memo
        {
            get{ return _memo; }
            set{ _memo = value; }
        }        
		/// <summary>
		/// 翻译价格
        /// </summary>		
		private decimal _translationprice;
        public decimal TranslationPrice
        {
            get{ return _translationprice; }
            set{ _translationprice = value; }
        }        
		/// <summary>
		/// 记录产生时间
        /// </summary>		
		private DateTime _addtime;
        public DateTime AddTime
        {
            get{ return _addtime; }
            set{ _addtime = value; }
        }        
		   
	}
}

