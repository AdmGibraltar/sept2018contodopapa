using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class FacturaRevisionCobro
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

        private int _Id_Frc;
        public int Id_Frc
        {
            get { return _Id_Frc; }
            set { _Id_Frc = value; }
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

        private string _Frc_Entrego;
        public string Frc_Entrego
        {
            get { return _Frc_Entrego; }
            set { _Frc_Entrego = value; }
        }

        private string _Frc_Recibio;
        public string Frc_Recibio
        {
            get { return _Frc_Recibio; }
            set { _Frc_Recibio = value; }
        }

        private DateTime _Frc_Fecha;
        public DateTime Frc_Fecha
        {
            get { return _Frc_Fecha; }
            set { _Frc_Fecha = value; }
        }

        private DateTime? _Frc_FecEnvio;
        public DateTime? Frc_FecEnvio
        {
            get { return _Frc_FecEnvio; }
            set { _Frc_FecEnvio = value; }
        }

        private DateTime? _Frc_FecRecibio;
        public DateTime? Frc_FecRecibio
        {
            get { return _Frc_FecRecibio; }
            set { _Frc_FecRecibio = value; }
        }

        private string _Frc_Estatus;
        public string Frc_Estatus
        {
            get { return _Frc_Estatus; }
            set { _Frc_Estatus = value; }
        }

        private string _Frc_EstatusStr;
        public string Frc_EstatusStr
        {
            get { return _Frc_EstatusStr; }
            set { _Frc_EstatusStr = value; }
        }

        private List<FacturaRevisionCobroDet> _ListaFacturaRevisionCobroDet;
        public string DbName;
        public List<FacturaRevisionCobroDet> ListaFacturaRevisionCobroDet
        {
            get { return _ListaFacturaRevisionCobroDet; }
            set { _ListaFacturaRevisionCobroDet = value; }
        }
    }
}
