using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad 
{
    public class RemisionSvtaAlmacen
    {
        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        private int _Id_Cd;
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        private string _Cd_Nombre;
        public string Cd_Nombre
        {
            get { return _Cd_Nombre; }
            set { _Cd_Nombre = value; }
        }

        private int _Id_Rva;
        public int Id_Rva
        {
            get { return _Id_Rva; }
            set { _Id_Rva = value; }
        }

        private int? _Id_Reg;
        public int? Id_Reg
        {
            get { return _Id_Reg; }
            set { _Id_Reg = value; }
        }

        private int _Id_U;
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }

        private string _Rva_Entrego;
        public string Rva_Entrego
        {
            get { return _Rva_Entrego; }
            set { _Rva_Entrego = value; }
        }

        private string _Rva_Recibio;
        public string Rva_Recibio
        {
            get { return _Rva_Recibio; }
            set { _Rva_Recibio = value; }
        }

        private DateTime _Rva_Fecha;
        public DateTime Rva_Fecha
        {
            get { return _Rva_Fecha; }
            set { _Rva_Fecha = value; }
        }

        private DateTime? _Rva_FecEnvio;
        public DateTime? Rva_FecEnvio
        {
            get { return _Rva_FecEnvio; }
            set { _Rva_FecEnvio = value; }
        }

        private DateTime? _Rva_FecRecibio;
        public DateTime? Rva_FecRecibio
        {
            get { return _Rva_FecRecibio; }
            set { _Rva_FecRecibio = value; }
        }

        private string _Rva_Estatus;
        public string Rva_Estatus
        {
            get { return _Rva_Estatus; }
            set { _Rva_Estatus = value; }
        }

        private string _Rva_EstatusStr;
        public string Rva_EstatusStr
        {
            get { return _Rva_EstatusStr; }
            set { _Rva_EstatusStr = value; }
        }

        private List<RemisionSvtaAlmacenDet> _ListaRemisionSvtaAlmacenDet;
        public string DbName;
        public List<RemisionSvtaAlmacenDet> ListaRemisionSvtaAlmacenDet
        {
            get { return _ListaRemisionSvtaAlmacenDet; }
            set { _ListaRemisionSvtaAlmacenDet = value; }
        }

        public DateTime Rva_FechaFin { get; set; }
    }
}
