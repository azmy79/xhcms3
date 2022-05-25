using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//预约
		public class Appointment
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
		/// 公证员ID
        /// </summary>		
		private int _managerid;
        public int ManagerID
        {
            get{ return _managerid; }
            set{ _managerid = value; }
        }        
		/// <summary>
		/// 预约时间
        /// </summary>		
		private DateTime _date;
        public DateTime Date
        {
            get{ return _date; }
            set{ _date = value; }
        }        
		/// <summary>
		/// 姓名
        /// </summary>		
		private string _name;
        public string Name
        {
            get{ return _name; }
            set{ _name = value; }
        }        
		/// <summary>
		/// 联系方式
        /// </summary>		
		private string _contact;
        public string Contact
        {
            get{ return _contact; }
            set{ _contact = value; }
        }        
		/// <summary>
		/// 内容
        /// </summary>		
		private string _content;
        public string Content
        {
            get{ return _content; }
            set{ _content = value; }
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
		   
	}
}

