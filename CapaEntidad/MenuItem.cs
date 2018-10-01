using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
  public  class MenuItem
    {
        private string _cve;
        private string _cve_p;
        private string _ord;
        private string _desc;
        private string _href;
        private int _nivel;
        int _Tipo;
        string _img;
        private string _path;

        public string path
        {
            get { return _path; }
            set { _path = value; }
        }

        public string Img
        {
            get { return _img; }
            set { _img = value; }
        }
        public int Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }

        public string cve
        {
            get { return _cve; }
            set { _cve = value; }
        }
        public string cve_p
        {
            get { return _cve_p; }
            set { _cve_p = value; }
        }
        public string ord
        {
            get { return _ord; }
            set { _ord = value; }
        }
        public string desc
        {
            get { return _desc; }
            set { _desc = value; }
        }
        public string href
        {
            get { return _href; }
            set { _href = value; }
        }
        public int nivel
        {
            get { return _nivel; }
            set { _nivel = value; }
        }


        public MenuItem()
        {
            _cve = "0";
            _cve_p = "NULL";
            _ord = "0";
            _desc = "";
            _href = "NULL";
            _nivel = 0;
        }

    }
}
