using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class VenEstadisticaVentas
    {
        #region Variables
        int id_Cd;
        int filtro;
        string sFiltro;
        string sucursal;
        string sSucursal;
        string territorio;
        string sTerritorio;
        string cliente;
        string sCliente;
        string producto;
        string sProducto;
        int anio;
        string sAnio;
        int mostrar;
        string sMostrar;
        int nivel;
        string sNivel;
        int nivel2;
        string sNivel2;
        string encabezado;
        string encabezado1;
        int reporte;
        #endregion

        int _Id_Prd;
        string _Prd_Descripcion;
        double _Ene;
	    double _Feb;
	    double _Mar;
	    double _Abr;
	    double _May;
	    double _Jun;
	    double _Jul;
	    double _Ago;
	    double _Sep;
	    double _Oct;
	    double _Nov;
	    double _Dic;
        double _Total;
        int _Mes;

        int _Id_Emp;

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }


        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }


        public int Mes
        {
            get { return _Mes; }
            set { _Mes = value; }
        }

        
        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }

        public double Ene
        {
            get { return _Ene; }
            set { _Ene = value; }
        }

        public double Feb
        {
            get { return _Feb; }
            set { _Feb = value; }
        }


        public double Mar
        {
            get { return _Mar; }
            set { _Mar = value; }
        }


        public double Abr
        {
            get { return _Abr; }
            set { _Abr = value; }
        }

        public double May
        {
            get { return _May; }
            set { _May = value; }
        }

        public double Jun
        {
            get { return _Jun; }
            set { _Jun = value; }
        }


        public double Jul
        {
            get { return _Jul; }
            set { _Jul = value; }
        }


        public double Ago
        {
            get { return _Ago; }
            set { _Ago = value; }
        }


        public double Sep
        {
            get { return _Sep; }
            set { _Sep = value; }
        }


        public double Oct
        {
            get { return _Oct; }
            set { _Oct = value; }
        }


        public double Nov
        {
            get { return _Nov; }
            set { _Nov = value; }
        }


        public double Dic
        {
            get { return _Dic; }
            set { _Dic = value; }
        }


       

        public double Total
        {
            get { return _Total; }
            set { _Total = value; }
        }


        #region Refactorizado
        public int Id_Cd
        {
            get { return id_Cd; }
            set { id_Cd = value; }
        }

      


        public int Filtro
        {
            get { return filtro; }
            set { filtro = value; }
        }
        public string SFiltro
        {
            get { return sFiltro; }
            set { sFiltro = value; }
        }
        public string Sucursal
        {
            get { return sucursal; }
            set { sucursal = value; }
        }
        public string Territorio
        {
            get { return territorio; }
            set { territorio = value; }
        }
        public string Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }
        public string Producto
        {
            get { return producto; }
            set { producto = value; }
        }
        public int Anio
        {
            get { return anio; }
            set { anio = value; }
        }
        public string SAnio
        {
            get { return sAnio; }
            set { sAnio = value; }
        }
        public int Mostrar
        {
            get { return mostrar; }
            set { mostrar = value; }
        }
        public string SMostrar
        {
            get { return sMostrar; }
            set { sMostrar = value; }
        }
        public int Nivel
        {
            get { return nivel; }
            set { nivel = value; }
        }
        public string SNivel
        {
            get { return sNivel; }
            set { sNivel = value; }
        }
        public int Nivel2
        {
            get { return nivel2; }
            set { nivel2 = value; }
        }
        public string SNivel2
        {
            get { return sNivel2; }
            set { sNivel2 = value; }
        }
        public string SSucursal
        {
            get { return sSucursal; }
            set { sSucursal = value; }
        }
        public string STerritorio
        {
            get { return sTerritorio; }
            set { sTerritorio = value; }
        }
        public string SCliente
        {
            get { return sCliente; }
            set { sCliente = value; }
        }
        public string SProducto
        {
            get { return sProducto; }
            set { sProducto = value; }
        }
        public string Encabezado
        {
            get { return encabezado; }
            set { encabezado = value; }
        }
        public string Encabezado1
        {
            get { return encabezado1; }
            set { encabezado1 = value; }
        }
        public int Reporte
        {
            get { return reporte; }
            set { reporte = value; }
        }
        #endregion
    }
}
