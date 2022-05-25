using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//用户拓展表
		public class UserExt
	{
   		     
      	/// <summary>
		/// 会员ID
        /// </summary>		
		private int _userid;
        public int UserID
        {
            get{ return _userid; }
            set{ _userid = value; }
        }        
		/// <summary>
		/// 中文姓名
        /// </summary>		
		private string _cnname;
        public string CnName
        {
            get{ return _cnname; }
            set{ _cnname = value; }
        }        
		/// <summary>
		/// 英文姓名
        /// </summary>		
		private string _enname;
        public string EnName
        {
            get{ return _enname; }
            set{ _enname = value; }
        }        
		/// <summary>
		/// 证件类型（注册时枚举）
        /// </summary>		
		private int _carttype;
        public int CartType
        {
            get{ return _carttype; }
            set{ _carttype = value; }
        }        
		/// <summary>
		/// 证件号码
        /// </summary>		
		private string _cartnum;
        public string CartNum
        {
            get{ return _cartnum; }
            set{ _cartnum = value; }
        }        
		/// <summary>
		/// 户籍所在地
        /// </summary>		
		private string _craddress;
        public string CRAddress
        {
            get{ return _craddress; }
            set{ _craddress = value; }
        }        
		   
	}
}

