using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class RemisionDet
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

        private int _Id_Rem;
        public int Id_Rem
        {
            get { return _Id_Rem; }
            set { _Id_Rem = value; }
        }

        private int _Id_RemDet;
        public int Id_RemDet
        {
            get { return _Id_RemDet; }
            set { _Id_RemDet = value; }
        }

        private int? _Id_Ter;
        public int? Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }

        private int _Id_Prd;
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        private int _Rem_Cant;
        public int Rem_Cant
        {
            get { return _Rem_Cant; }
            set { _Rem_Cant = value; }
        }

        private int? _Rem_Asignar;
        public int? Rem_Asignar
        {
            get { return _Rem_Asignar; }
            set { _Rem_Asignar = value; }
        }

        private int? _Rem_CantE;
        public int? Rem_CantE
        {
            get { return _Rem_CantE; }
            set { _Rem_CantE = value; }
        }

        private int? _Rem_CantF;
        public int? Rem_CantF
        {
            get { return _Rem_CantF; }
            set { _Rem_CantF = value; }
        }

        private double _Rem_Precio;
        public double Rem_Precio
        {
            get { return _Rem_Precio; }
            set { _Rem_Precio = value; }
        }

        private double _Prd_Pesos;
        public double Prd_Pesos
        {
            get { return _Prd_Pesos; }
            set { _Prd_Pesos = value; }
        }        

        private bool _Ped_Pertenece;
        public bool Ped_Pertenece
        {
            get { return _Ped_Pertenece; }
            set { _Ped_Pertenece = value; }
        }

        private string _Prd_Descripcion;
        private string _Ter_Nombre;
        private string _Prd_Presentacion;
        private string _Prd_UniNe;

        public string Ter_Nombre
        {
            get { return _Ter_Nombre; }
            set { _Ter_Nombre = value; }
        }
        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }


        public string Prd_Presentacion
        {
            get { return _Prd_Presentacion; }
            set { _Prd_Presentacion = value; }
        }

        public string Prd_UniNe
        {
            get { return _Prd_UniNe; }
            set { _Prd_UniNe = value; }
        }

        private int _Id_CteExt;
        public int Id_CteExt
        {
            get { return _Id_CteExt; }
            set { _Id_CteExt = value; }
        }

        private int _Id_Cte;
        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }


        private string _Cte_NomComercial;
        public string Cte_NomComercial
        {
            get { return _Cte_NomComercial; }
            set { _Cte_NomComercial = value; }
        }

        private Producto _producto;
        public Producto Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }
        private string _Clp_Release;
        public string Clp_Release
        {
            get { return _Clp_Release; }
            set { _Clp_Release = value; }
        }

        private double _Rem_Importe;
        public double Rem_Importe
        {
            get { return _Rem_Importe; }
            set { _Rem_Importe = value; }
        }

        private DateTime _Rem_Fecha;
        public DateTime Rem_Fecha
        {
            get { return _Rem_Fecha; }
            set { _Rem_Fecha = value; }
        }

        private int _TipoSalida;
        public int TipoSalida
        {
            get { return _TipoSalida; }
            set { _TipoSalida = value; }
        }


        private int _ConceptoTipoSalida;
        public int ConceptoTipoSalida
        {
            get { return _ConceptoTipoSalida; }
            set { _ConceptoTipoSalida = value; }
        }


        private string _DescTipoSalida;
        public string DescTipoSalida
        {
            get { return _DescTipoSalida; }
            set { _DescTipoSalida = value; }
        }


        private string _DescConceptoTipoSalida;
        public string DescConceptoTipoSalida
        {
            get { return _DescConceptoTipoSalida; }
            set { _DescConceptoTipoSalida = value; }
        }
        private string _Rem_Estatus;
        public string Rem_Estatus
        {
            get { return _Rem_Estatus; }
            set { _Rem_Estatus = value; }
        }

        public int bTieneEspecial { get; set; }
    }
}
