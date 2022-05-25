using System;

namespace DTcms.Model
{
    [Serializable]
    public class link
    {
        private DateTime _add_time = DateTime.Now;
        private string _email;
        private int _id;
        private string _img_url;
        private int _is_image = 0;
        private int _is_lock;
        private int _is_red = 0;
        private string _site_url;
        private int _sort_id = 0x63;
        private string _title;
        private string _user_name;
        private string _user_tel;

        public DateTime add_time
        {
            get
            {
                return this._add_time;
            }
            set
            {
                this._add_time = value;
            }
        }

        public string email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
            }
        }

        public int id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string img_url
        {
            get
            {
                return this._img_url;
            }
            set
            {
                this._img_url = value;
            }
        }

        public int is_image
        {
            get
            {
                return this._is_image;
            }
            set
            {
                this._is_image = value;
            }
        }

        public int is_lock
        {
            get
            {
                return this._is_lock;
            }
            set
            {
                this._is_lock = value;
            }
        }

        public int is_red
        {
            get
            {
                return this._is_red;
            }
            set
            {
                this._is_red = value;
            }
        }

        public string site_url
        {
            get
            {
                return this._site_url;
            }
            set
            {
                this._site_url = value;
            }
        }

        public int sort_id
        {
            get
            {
                return this._sort_id;
            }
            set
            {
                this._sort_id = value;
            }
        }

        public string title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }

        public string user_name
        {
            get
            {
                return this._user_name;
            }
            set
            {
                this._user_name = value;
            }
        }

        public string user_tel
        {
            get
            {
                return this._user_tel;
            }
            set
            {
                this._user_tel = value;
            }
        }
    }
}

