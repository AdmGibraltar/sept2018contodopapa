using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ProveedorInternoTipo
    {
        private int _Id_Emp;
        private int _Id_Pvd;
        private int _Id_Tpvd;
        private string _Tpvd_Descripcion;
        private bool _Tpvd_Activo;
        private bool _Tpvd_Valida;

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        public int Id_Tpvd
        {
            get { return _Id_Tpvd; }
            set { _Id_Tpvd = value; }
        }

        public int Id_Pvd
        {
            get { return _Id_Pvd; }
            set { _Id_Pvd = value; }
        }

        public string Tpvd_Descripcion
        {
            get { return _Tpvd_Descripcion; }
            set { _Tpvd_Descripcion = value; }
        }

        public bool Tpvd_Activo
        {
            get { return _Tpvd_Activo; }
            set { _Tpvd_Activo = value; }
        }

        public bool Tpvd_Valida
        {
            get { return _Tpvd_Valida; }
            set { _Tpvd_Valida = value; }
        }

    }
}
