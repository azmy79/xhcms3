using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//申办-申办业务
		public class Bid_BidBusiness
	{
   		     
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
		/// 申办业务ID
        /// </summary>		
		private int _bidbusinessid;
        public int BidBusinessID
        {
            get{ return _bidbusinessid; }
            set{ _bidbusinessid = value; }
        }        
		/// <summary>
		/// 证书样式ID
        /// </summary>		
		private int _certificatestyleid;
        public int CertificateStyleID
        {
            get{ return _certificatestyleid; }
            set{ _certificatestyleid = value; }
        }        
		   
	}
}

