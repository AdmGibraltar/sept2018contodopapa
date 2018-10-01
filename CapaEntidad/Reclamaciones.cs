using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Reclamaciones
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

        private int _Id_Rec;
        public int Id_Rec
        {
            get { return _Id_Rec; }
            set { _Id_Rec = value; }
        }

        private int _Id_Reg;
        public int Id_Reg
        {
            get { return _Id_Reg; }
            set { _Id_Reg = value; }
        }

        private DateTime _Rec_Fecha;
        public DateTime Rec_Fecha
        {
            get { return _Rec_Fecha; }
            set { _Rec_Fecha = value; }
        }

        private int _Id_Cte;
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }

        private string _Rec_Usuario;
        public string Rec_Usuario
        {
            get { return _Rec_Usuario; }
            set { _Rec_Usuario = value; }
        }

        private string _Rec_Telefono;
        public string Rec_Telefono
        {
            get { return _Rec_Telefono; }
            set { _Rec_Telefono = value; }
        }

        private int _Id_Ter;
        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }

        private int _Id_Tipo;
        public int Id_tipo
        {
            get { return _Id_Tipo; }
            set { _Id_Tipo = value; }
        }

        private int _Id_NoConf;
        public int Id_NoConf
        {
            get { return _Id_NoConf; }
            set { _Id_NoConf = value; }
        }

        private string _Rec_Descripcion;
        public string Rec_Descripcion
        {
            get { return _Rec_Descripcion; }
            set { _Rec_Descripcion = value; }
        }

        private string _Rec_CausaRaiz;        
        public string Rec_CausaRaiz
        {
            get { return _Rec_CausaRaiz; }
            set { _Rec_CausaRaiz = value; }
        }

        private DateTime? _Rec_FecAccion;
        public DateTime? Rec_FecAccion
        {
            get { return _Rec_FecAccion; }
            set { _Rec_FecAccion = value; }
        }

        private string _Rec_AcAccion1;
        public string Rec_AcAccion1
        {
            get { return _Rec_AcAccion1; }
            set { _Rec_AcAccion1 = value; }
        }

        private string _Rec_AcAccion2;
        public string Rec_AcAccion2
        {
            get { return _Rec_AcAccion2; }
            set { _Rec_AcAccion2 = value; }
        }

        private string _Rec_AcResponsable;
        public string Rec_AcResponsable
        {
            get { return _Rec_AcResponsable; }
            set { _Rec_AcResponsable = value; }
        }
        
        private DateTime? _Rec_FecConformidad;
        public DateTime? Rec_FecConformidad
        {
            get { return _Rec_FecConformidad; }
            set { _Rec_FecConformidad = value; }
        }
        
        private string _Rec_ConNombre;
        public string Rec_ConNombre
        {
            get { return _Rec_ConNombre; }
            set { _Rec_ConNombre = value; }
        }
        
        private string _Rec_ConDepartamento;
        public string Rec_ConDepartamento
        {
            get { return _Rec_ConDepartamento; }
            set { _Rec_ConDepartamento = value; }
        }
        
        private string _Rec_Comentarios;
        public string Rec_Comentarios
        {
            get { return _Rec_Comentarios; }
            set { _Rec_Comentarios = value; }
        }
        
        private string _Rec_Estatus;
        public string Rec_Estatus
        {
            get { return _Rec_Estatus; }
            set { _Rec_Estatus = value; }
        }

        private string _Cte_NomComercial;
        public string Cte_NomComercial
        {
            get { return _Cte_NomComercial; }
            set { _Cte_NomComercial = value; }
        }

        private string _Accion;
        public string Accion
        {
            get { return _Accion; }
            set { _Accion = value; }
        }

        private string _Estatus;
        public string Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }

        private string _Nco_Descripcion;
        public string Nco_Descripcion
        {
            get { return _Nco_Descripcion; }
            set { _Nco_Descripcion = value; }
        }
    }
}

