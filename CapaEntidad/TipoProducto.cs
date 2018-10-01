using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class TipoProducto
    {
        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        private int _Id_Ptp;
        public int Id_Ptp
        {
            get { return _Id_Ptp; }
            set { _Id_Ptp = value; }
        }

        private string _Ptp_Descripcion;
        public string Ptp_Descripcion
        {
            get { return _Ptp_Descripcion; }
            set { _Ptp_Descripcion = value; }
        }

        private string _Ptp_Tipo;
        public string Ptp_Tipo
        {
            get { return _Ptp_Tipo; }
            set { _Ptp_Tipo = value; }
        }

        private string _Ptp_Tipo_Str;
        public string Ptp_Tipo_Str
        {
            get { return _Ptp_Tipo_Str; }
            set { _Ptp_Tipo_Str = value; }
        }

        private bool _Ptp_Activo;
        public bool Ptp_Activo
        {
            get { return _Ptp_Activo; }
            set { _Ptp_Activo = value; }
        }

        private string _Ptp_ActivoStr;
        public string Ptp_ActivoStr
        {
            get { return _Ptp_ActivoStr; }
            set { _Ptp_ActivoStr = value; }
        }

    }
}
