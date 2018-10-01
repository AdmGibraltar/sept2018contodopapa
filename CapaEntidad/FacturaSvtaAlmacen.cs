using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad 
{
    public class FacturaSvtaAlmacen
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

        private int _Id_Fva;
        public int Id_Fva
        {
            get { return _Id_Fva; }
            set { _Id_Fva = value; }
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

        private string _Fva_Entrego;
        public string Fva_Entrego
        {
            get { return _Fva_Entrego; }
            set { _Fva_Entrego = value; }
        }

        private string _Fva_Recibio;
        public string Fva_Recibio
        {
            get { return _Fva_Recibio; }
            set { _Fva_Recibio = value; }
        }

        private DateTime _Fva_Fecha;
        public DateTime Fva_Fecha
        {
            get { return _Fva_Fecha; }
            set { _Fva_Fecha = value; }
        }

        private DateTime? _Fva_FecEnvio;
        public DateTime? Fva_FecEnvio
        {
            get { return _Fva_FecEnvio; }
            set { _Fva_FecEnvio = value; }
        }

        private DateTime? _Fva_FecRecibio;
        public DateTime? Fva_FecRecibio
        {
            get { return _Fva_FecRecibio; }
            set { _Fva_FecRecibio = value; }
        }

        private string _Fva_Estatus;
        public string Fva_Estatus
        {
            get { return _Fva_Estatus; }
            set { _Fva_Estatus = value; }
        }

        private string _Fva_EstatusStr;
        public string Fva_EstatusStr
        {
            get { return _Fva_EstatusStr; }
            set { _Fva_EstatusStr = value; }
        }

        private List<FacturaSvtaAlmacenDet> _ListaFacturaSvtaAlmacenDet;
        public string DbName;
        public List<FacturaSvtaAlmacenDet> ListaFacturaSvtaAlmacenDet
        {
            get { return _ListaFacturaSvtaAlmacenDet; }
            set { _ListaFacturaSvtaAlmacenDet = value; }
        }

        public DateTime Fva_FechaFin { get; set; }
    }
}
