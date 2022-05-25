using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//证件
		public class Document
	{
   		     
      	/// <summary>
		/// 主键
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
		/// 证件类型ID
        /// </summary>		
		private int _documenttypeid;
        public int DocumentTypeID
        {
            get{ return _documenttypeid; }
            set{ _documenttypeid = value; }
        }        
		/// <summary>
		/// 路径
        /// </summary>		
		private string _path;
        public string Path
        {
            get{ return _path; }
            set{ _path = value; }
        }        
		/// <summary>
		/// 上传时间
        /// </summary>		
		private DateTime _addtime;
        public DateTime AddTime
        {
            get{ return _addtime; }
            set{ _addtime = value; }
        }        
		   
	}
}

