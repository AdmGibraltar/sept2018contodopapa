using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Embarques
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
        private int _Id_Emb;
        public int Id_Emb
        {
            get { return _Id_Emb; }
            set { _Id_Emb = value; }
        }

        private int _Id_U;
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }

        private string _Emb_Estatus;
        public string Emb_Estatus
        {
            get { return _Emb_Estatus; }
            set { _Emb_Estatus = value; }
        }

        private string _Emb_EstatusStr;
        public string Emb_EstatusStr
        {
            get { return _Emb_EstatusStr; }
            set { _Emb_EstatusStr = value; }
        }

        private string _U_Nombre;
        public string U_Nombre
        {
            get { return _U_Nombre; }
            set { _U_Nombre = value; }
        }

        private DateTime _Emb_Fec;
        public DateTime Emb_Fec
        {
            get { return _Emb_Fec; }
            set { _Emb_Fec = value; }
        }

        private DateTime _Emb_Dia;
        public DateTime Emb_Dia
        {
            get { return _Emb_Dia; }
            set { _Emb_Dia = value; }
        }

        private string _Emb_Chofer;
        public string Emb_Chofer
        {
            get { return _Emb_Chofer; }
            set { _Emb_Chofer = value; }
        }

        private string _Emb_Camioneta;
        public string Emb_Camioneta
        {
            get { return _Emb_Camioneta; }
            set { _Emb_Camioneta = value; }
        }

    }
}
