using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class FamProducto
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

        string _Fam_Descripcion;
        public string Fam_Descripcion
        {
            get { return _Fam_Descripcion; }
            set { _Fam_Descripcion = value; }
        }

        bool _Fam_Activo;
        public bool Fam_Activo
        {
            get { return _Fam_Activo; }
            set { _Fam_Activo = value; }
        }

        string _Fam_ActivoStr;
        public string Fam_ActivoStr
        {
            get { return _Fam_ActivoStr; }
            set { _Fam_ActivoStr = value; }
        }
    }
}
