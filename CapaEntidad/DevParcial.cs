using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class DevParcial
    {
        int _Id_Nca;
        int _Id_Nca2;
        int _Tipo;
        string _Es_Estatus;
        string _Estatus;
        DateTime _Fecha;       
        int _Pedido;
        int _Num_Cliente;
        int _Factura;
        string _Cliente;        
        double _Nca_Subtotal;
        double _Nca_Iva;
        double _Nca_Total;
        string _Nca_Pagos;
        string _TipoMov;
        int _Id_Ter;
        int _Id_Rik;

        int _Id_U;

        /*datos Usuario*/
        string Cte_FacCalle;
        string Cte_FacNumero;
        string Cte_FacCp;
        string Cte_FacColonia;
        string Cte_FacMunicipio;
        string Cte_FacCiudad;
        string Cte_FacTel;
        string Cte_FacRfc;
        string datoFactura;

        /*FILTRO*/
        string _filtro_Nombre;
        string _filtro_Id_Cte;
        string _filtro_Id_Cte2;
        string _filtro_Id_Devini;
        string _filtro_Id_Devfin;
        string _filtro_Estatus;        
        string _filtro_FecIni;
        string _filtro_FecFin;

        public int Id_Nca
        {
            get { return _Id_Nca; }
            set { _Id_Nca = value; }
        }


        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }

        

        public int Id_Nca2
        {
            get { return _Id_Nca2; }
            set { _Id_Nca2 = value; }
        }

        public int Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }

        public string Es_Estatus
        {
            get { return _Es_Estatus; }
            set { _Es_Estatus = value; }
        }

        public string Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }

        private DateTime _Dev_FechaHr;
        public DateTime Dev_FechaHr
        {
            get { return _Dev_FechaHr; }
            set { _Dev_FechaHr = value; }
        }

        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }                

        public int Pedido
        {
            get { return _Pedido; }
            set { _Pedido = value; }
        }

        public int Num_Cliente
        {
            get { return _Num_Cliente; }
            set { _Num_Cliente = value; }
        }

        public int Factura
        {
            get { return _Factura; }
            set { _Factura = value; }
        }

        public string Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }

        public double Nca_Subtotal
        {
            get { return _Nca_Subtotal; }
            set { _Nca_Subtotal = value; }
        }
        
        public double Nca_Iva
        {
            get { return _Nca_Iva; }
            set { _Nca_Iva = value; }
        }
        
        public double Nca_Total
        {
            get { return _Nca_Total; }
            set { _Nca_Total = value; }
        }

        public string Nca_Pagos
        {
            get { return _Nca_Pagos; }
            set { _Nca_Pagos = value; }
        }

        public string TipoMov
        {
            get { return _TipoMov; }
            set { _TipoMov = value; }
        }

        public int Id_Rik
        {
            get { return _Id_Rik; }
            set { _Id_Rik = value; }
        }

        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }
        
        /*Usuario*/
        public string Cte_FacCalle1
        {
            get { return Cte_FacCalle; }
            set { Cte_FacCalle = value; }
        }
        public string Cte_FacNumero1
        {
            get { return Cte_FacNumero; }
            set { Cte_FacNumero = value; }
        }
        public string Cte_FacCp1
        {
            get { return Cte_FacCp; }
            set { Cte_FacCp = value; }
        }
        public string Cte_FacColonia1
        {
            get { return Cte_FacColonia; }
            set { Cte_FacColonia = value; }
        }
        public string Cte_FacMunicipio1
        {
            get { return Cte_FacMunicipio; }
            set { Cte_FacMunicipio = value; }
        }
        public string Cte_FacCiudad1
        {
            get { return Cte_FacCiudad; }
            set { Cte_FacCiudad = value; }
        }
        public string Cte_FacTel1
        {
            get { return Cte_FacTel; }
            set { Cte_FacTel = value; }
        }
        public string Cte_FacRfc1
        {
            get { return Cte_FacRfc; }
            set { Cte_FacRfc = value; }
        }

        public string DatoFactura
        {
            get { return datoFactura; }
            set { datoFactura = value; }
        }


        /*filtro*/
        public string Filtro_Nombre
        {
            get { return _filtro_Nombre; }
            set { _filtro_Nombre = value; }
        }

        public string Filtro_Id_Cte
        {
            get { return _filtro_Id_Cte; }
            set { _filtro_Id_Cte = value; }
        }

        public string Filtro_Id_Cte2
        {
            get { return _filtro_Id_Cte2; }
            set { _filtro_Id_Cte2 = value; }
        }

        public string Filtro_Id_Devini
        {
            get { return _filtro_Id_Devini; }
            set { _filtro_Id_Devini = value; }
        }

        public string Filtro_Id_Devfin
        {
            get { return _filtro_Id_Devfin; }
            set { _filtro_Id_Devfin = value; }
        }

        public string Filtro_Estatus
        {
            get { return _filtro_Estatus; }
            set { _filtro_Estatus = value; }
        }

        public string Filtro_FecIni
        {
            get { return _filtro_FecIni; }
            set { _filtro_FecIni = value; }
        }

        public string Filtro_FecFin
        {
            get { return _filtro_FecFin; }
            set { _filtro_FecFin = value; }
        }
    }
}
