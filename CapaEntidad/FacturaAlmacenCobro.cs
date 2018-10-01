using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad 
{
    public class FacturaAlmacenCobro
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

        private int _Id_Fac;
        public int Id_Fac
        {
            get { return _Id_Fac; }
            set { _Id_Fac = value; }
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

        private string _Fac_Entrego;
        public string Fac_Entrego
        {
            get { return _Fac_Entrego; }
            set { _Fac_Entrego = value; }
        }

        private string _Fac_Recibio;
        public string Fac_Recibio
        {
            get { return _Fac_Recibio; }
            set { _Fac_Recibio = value; }
        }

        private DateTime _Fac_Fecha;
        public DateTime Fac_Fecha
        {
            get { return _Fac_Fecha; }
            set { _Fac_Fecha = value; }
        }

        private DateTime? _Fac_FecEnvio;
        public DateTime? Fac_FecEnvio
        {
            get { return _Fac_FecEnvio; }
            set { _Fac_FecEnvio = value; }
        }

        private DateTime? _Fac_FecRecibio;
        public DateTime? Fac_FecRecibio
        {
            get { return _Fac_FecRecibio; }
            set { _Fac_FecRecibio = value; }
        }

        private string _Fac_Estatus;
        public string Fac_Estatus
        {
            get { return _Fac_Estatus; }
            set { _Fac_Estatus = value; }
        }

        private string _Fac_EstatusStr;
        public string Fac_EstatusStr
        {
            get { return _Fac_EstatusStr; }
            set { _Fac_EstatusStr = value; }
        }

        private List<FacturaAlmacenCobroDet> _ListaFacturaAlmacenCobroDet;
        public string DbName;
        public List<FacturaAlmacenCobroDet> ListaFacturaAlmacenCobroDet
        {
            get { return _ListaFacturaAlmacenCobroDet; }
            set { _ListaFacturaAlmacenCobroDet = value; }
        }

        public DateTime Fac_FechaFin { get; set; }

        public int Id_AlmCob { get; set; }

     
    }
}
