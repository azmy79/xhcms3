using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//申办
		public class Bid
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
		/// 编号
        /// </summary>		
		private string _number;
        public string Number
        {
            get{ return _number; }
            set{ _number = value; }
        }        
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
		/// 用途ID
        /// </summary>		
		private int _purposeid;
        public int PurposeID
        {
            get{ return _purposeid; }
            set{ _purposeid = value; }
        }        
		/// <summary>
		/// 前往国家ID
        /// </summary>		
		private int _countryid;
        public int CountryID
        {
            get{ return _countryid; }
            set{ _countryid = value; }
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
		/// 姓名
        /// </summary>		
		private bool _sex;
        public bool Sex
        {
            get{ return _sex; }
            set{ _sex = value; }
        }        
		/// <summary>
		/// 联系电话
        /// </summary>		
		private string _tel;
        public string Tel
        {
            get{ return _tel; }
            set{ _tel = value; }
        }        
		/// <summary>
		/// 地址
        /// </summary>		
		private string _address;
        public string Address
        {
            get{ return _address; }
            set{ _address = value; }
        }        
		/// <summary>
		/// 创建时间
        /// </summary>		
		private DateTime _addtime;
        public DateTime AddTime
        {
            get{ return _addtime; }
            set{ _addtime = value; }
        }        
		/// <summary>
		/// 总价格
        /// </summary>		
		private decimal _price;
        public decimal Price
        {
            get{ return _price; }
            set{ _price = value; }
        }        
		/// <summary>
		/// 副本数量
        /// </summary>		
		private int _copycount;
        public int CopyCount
        {
            get{ return _copycount; }
            set{ _copycount = value; }
        }        
		/// <summary>
		/// 状态（0：正在审核；1：审核通过）
        /// </summary>		
		private int _status;
        public int Status
        {
            get{ return _status; }
            set{ _status = value; }
        }        
		/// <summary>
		/// Birthday
        /// </summary>		
		private DateTime _birthday;
        public DateTime Birthday
        {
            get{ return _birthday; }
            set{ _birthday = value; }
        }        
		/// <summary>
		/// CartType
        /// </summary>		
		private int _carttype;
        public int CartType
        {
            get{ return _carttype; }
            set{ _carttype = value; }
        }        
		/// <summary>
		/// CartNum
        /// </summary>		
		private string _cartnum;
        public string CartNum
        {
            get{ return _cartnum; }
            set{ _cartnum = value; }
        }        
		   
	}
}

