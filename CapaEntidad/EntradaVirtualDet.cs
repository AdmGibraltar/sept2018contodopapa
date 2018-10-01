using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class EntradaVirtualDet
    {
        int _Id_Emp;
        int _Id_Cd;
        int _Id_Env;        
        int _Id_Prd;
        string _Prd_Descripcion;
        string _GUID;
        int _Env_Cantidad;
        int _Env_CantDevuelta;
        decimal _Env_PreVta;
        decimal _Env_Costo;
        int _Id_EvPro;



        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }

        public int Id_EvPro
        {
            get { return _Id_EvPro; }
            set { _Id_EvPro = value; }
        }



        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        public int Id_Env
        {
            get { return _Id_Env; }
            set { _Id_Env = value; }
        }


        public string GUID
        {
            get { return _GUID; }
            set { _GUID = value; }
        }


        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }


        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd= value; }
        }

        public int Env_Cantidad
        {
            get { return _Env_Cantidad; }
            set { _Env_Cantidad = value; }
        }


        public int Env_CantDevuelta
        {
            get { return _Env_CantDevuelta; }
            set { _Env_CantDevuelta = value; }
        }

        public decimal Env_PreVta
        {
            get { return _Env_PreVta; }
            set { _Env_PreVta = value; }
        }

        public decimal Env_Costo
        {
            get { return _Env_Costo; }
            set { _Env_Costo = value; }
        }


    }
}
