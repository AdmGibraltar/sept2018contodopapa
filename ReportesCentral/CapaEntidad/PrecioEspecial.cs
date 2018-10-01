using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class PrecioEspecial
    {

        int _Id_Emp;
        int _Id_Cd;
        int _Id_Ape;
        DateTime? _Ape_Fecha;
        string _Ape_Estatus;
        int? _Ape_Sustituye;
        int? _Ape_Sustituida;
        int _Ape_Tipo;
        int _Ape_Solicitar;
        string _Ape_Nota;
        int? _Ape_NumProveedor;
       
        private int _Id_U;
        private int? _Id_Cte;
        private string _U_Nombre;
        private string _Cd_Nombre;
        private string _Ape_NotaResp;
        private string _Ape_Unique;
        private string _Ape_Convenio;
        private string _Ape_NumUsuario;

        
        public string Accion;
        public string Ape_Naturaleza;

        private int _Ano;
        public int Ano
        {
            get { return _Ano; }
            set { _Ano = value; }
        }


        private int _Mes;
        public int Mes
        {
            get { return _Mes; }
            set { _Mes = value; }
        }

        
        
        public string Ape_Unique
        {
            get { return _Ape_Unique; }
            set { _Ape_Unique = value; }
        }

        public string Ape_Convenio
        {
            get { return _Ape_Convenio; }
            set { _Ape_Convenio = value; }
        }

        public string Ape_NumUsuario
        {
            get { return _Ape_NumUsuario; }
            set { _Ape_NumUsuario = value; }
        }

        public string Ape_NotaResp
        {
            get { return _Ape_NotaResp; }
            set { _Ape_NotaResp = value; }
        }
       
        public string U_Nombre
        {
            get { return _U_Nombre; }
            set { _U_Nombre = value; }
        }
        public string Cd_Nombre
        {
            get { return _Cd_Nombre; }
            set { _Cd_Nombre = value; }
        }
        public int? Ape_NumProveedor
        {
            get { return _Ape_NumProveedor; }
            set { _Ape_NumProveedor = value; }
        }

        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }


        public int? Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
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
        public int Id_Ape
        {
            get { return _Id_Ape; }
            set { _Id_Ape = value; }
        }
        public DateTime? Ape_Fecha
        {
            get { return _Ape_Fecha; }
            set { _Ape_Fecha = value; }
        }
        public string Ape_Estatus
        {
            get { return _Ape_Estatus; }
            set { _Ape_Estatus = value; }
        }
        public int? Ape_Sustituye
        {
            get { return _Ape_Sustituye; }
            set { _Ape_Sustituye = value; }
        }
        public int? Ape_Sustituida
        {
            get { return _Ape_Sustituida; }
            set { _Ape_Sustituida = value; }
        }
        public int Ape_Tipo
        {
            get { return _Ape_Tipo; }
            set { _Ape_Tipo = value; }
        }
        public int Ape_Solicitar
        {
            get { return _Ape_Solicitar; }
            set { _Ape_Solicitar = value; }
        }
        public string Ape_Nota
        {
            get { return _Ape_Nota; }
            set { _Ape_Nota = value; }
        }


        private float _Ape_PrecioEspecial;
        public float Ape_PrecioEspecial
        {
            get { return _Ape_PrecioEspecial; }
            set { _Ape_PrecioEspecial = value; }
        }



        private float _Ape_FechaInicio;
        public float Ape_FechaInicio
        {
            get { return _Ape_FechaInicio; }
            set { _Ape_FechaInicio = value; }
        }


        private float _Ape_FechaFin;
        public float Ape_FechaFin
        {
            get { return _Ape_FechaFin; }
            set { _Ape_FechaFin = value; }
        }

        


       



        
    }
}
