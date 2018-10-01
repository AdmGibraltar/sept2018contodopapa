using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIANWEB.WebAPI.Models.Post
{
    public class CatClienteDetContacto
    {
        public int Id_Emp
        {
            get;
            set;
        }
        public int Id_Cd
        {
            get;
            set;
        }
        public int Id_Cte
        {
            get;
            set;
        }
        public int Id_CteDet
        {
            get;
            set;
        }
        public int Id_Ter
        {
            get;
            set;
        }
        public int Id_Seg
        {
            get;
            set;
        }        
        
        public string Nombre
        {
            get;
            set;
        }
        public string Puesto
        {
            get;
            set;
        }
        public string Cumpleanios
        {
            get;
            set;
        }
        public string Correo
        {
            get;
            set;
        }
        public string Direccion1
        {
            get;
            set;
        }
        public string Direccion2
        {
            get;
            set;
        }
        public string TelNegocio
        {
            get;
            set;
        }
        public string TelCasa
        {
            get;
            set;
        }

        public int Id_Consecutivo
        {
            get;
            set;
        } 
    }
}