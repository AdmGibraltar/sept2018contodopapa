using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class TipoCosto
    {
        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        private int _Id_Tco;
        public int Id_Tco
        {
            get { return _Id_Tco; }
            set { _Id_Tco = value; }
        }
        private string _Tco_Descripcion;
        public string Tco_Descripcion
        {
            get { return _Tco_Descripcion; }
            set { _Tco_Descripcion = value; }
        }
        private bool _Tco_Activo;
        public bool Tco_Activo
        {
            get { return _Tco_Activo; }
            set { _Tco_Activo = value; }
        }

        private string _Tco_ActivoStr;
        public string Tco_ActivoStr
        {
            get { return _Tco_ActivoStr; }
            set { _Tco_ActivoStr = value; }
        }
    }
}
