using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class SubFamProducto
    {
        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        private int _Id_Fam;
        public int Id_Fam
        {
            get { return _Id_Fam; }
            set { _Id_Fam = value; }
        }

        private string _Id_Fam_Str;
        public string Id_Fam_Str
        {
            get { return _Id_Fam_Str; }
            set { _Id_Fam_Str = value; }
        }

        private int _Id_Sub;
        public int Id_Sub
        {
            get { return _Id_Sub; }
            set { _Id_Sub = value; }
        }

        string _Sub_Descripcion;
        public string Sub_Descripcion
        {
            get { return _Sub_Descripcion; }
            set { _Sub_Descripcion = value; }
        }

        bool _Sub_Activo;
        public bool Sub_Activo
        {
            get { return _Sub_Activo; }
            set { _Sub_Activo = value; }
        }

        string _Sub_ActivoStr;
        public string Sub_ActivoStr
        {
            get { return _Sub_ActivoStr; }
            set { _Sub_ActivoStr = value; }
        }
    }
}
