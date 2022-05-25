using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//申办业务-证件类型
		public class BidBusiness_DocumentType
	{
   		     
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
		/// 证件类型ID
        /// </summary>		
		private int _documenttypeid;
        public int DocumentTypeID
        {
            get{ return _documenttypeid; }
            set{ _documenttypeid = value; }
        }        
		   
	}
}

