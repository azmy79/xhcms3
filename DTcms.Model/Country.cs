﻿using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//国家
		public class Country
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
		/// 名称
        /// </summary>		
		private string _name;
        public string Name
        {
            get{ return _name; }
            set{ _name = value; }
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
		/// <summary>
		/// 是否需要译源相符
        /// </summary>		
		private bool _ists;
        public bool IsTS
        {
            get{ return _ists; }
            set{ _ists = value; }
        }        
		   
	}
}

