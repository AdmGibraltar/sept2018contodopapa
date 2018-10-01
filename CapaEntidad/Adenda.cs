using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
   public class Adenda
    {
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Id_Ade;
        private string _Tco_Descripcion;
        private bool _Tco_Activo;
        private string _Tco_ActivoStr;
        private bool _Tco_Requerido;

        public bool Tco_Requerido
        {
            get { return _Tco_Requerido; }
            set { _Tco_Requerido = value; }
        }

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        public int Id_Ade
        {
            get { return _Id_Ade; }
            set { _Id_Ade = value; }
        }
        public string Tco_Descripcion
        {
            get { return _Tco_Descripcion; }
            set { _Tco_Descripcion = value; }
        }
        public bool Tco_Activo
        {
            get { return _Tco_Activo; }
            set { _Tco_Activo = value; }
        }
        public string Tco_ActivoStr
        {
            get { return _Tco_ActivoStr; }
            set { _Tco_ActivoStr = value; }
        }
    }
}
