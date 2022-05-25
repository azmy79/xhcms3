using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//View_Document
		public class View_Document
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
		/// BidID
        /// </summary>		
		private int _bidid;
        public int BidID
        {
            get{ return _bidid; }
            set{ _bidid = value; }
        }        
		/// <summary>
		/// DocumentTypeID
        /// </summary>		
		private int _documenttypeid;
        public int DocumentTypeID
        {
            get{ return _documenttypeid; }
            set{ _documenttypeid = value; }
        }        
		/// <summary>
		/// Path
        /// </summary>		
		private string _path;
        public string Path
        {
            get{ return _path; }
            set{ _path = value; }
        }        
		/// <summary>
		/// AddTime
        /// </summary>		
		private DateTime _addtime;
        public DateTime AddTime
        {
            get{ return _addtime; }
            set{ _addtime = value; }
        }        
		/// <summary>
		/// Name
        /// </summary>		
		private string _name;
        public string Name
        {
            get{ return _name; }
            set{ _name = value; }
        }        
		/// <summary>
		/// Sort
        /// </summary>		
		private int _sort;
        public int Sort
        {
            get{ return _sort; }
            set{ _sort = value; }
        }        
		   
	}
}

