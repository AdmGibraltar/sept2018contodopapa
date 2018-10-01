using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class FacturaEntregaRuta
    {
        #region Variables
        /*Valores*/
        int Id_Fac;
        int pedido;
        int id_Cte;
        int id_Emb;
        int dia;
        int id_EmbDet;
        string estatus;
        string chofer;
        string camion;
        string Cte_NomComercial;
        DateTime fecha;
      
        /*Filtro*/        
        string _filtro_Embarque;
        string _filtro_Estatus;
        string _filtro_FecIni;
        string _filtro_FecFin;
        #endregion

        #region Refactorizacion
        /*Valores*/
        public int Id_Fac1
        {
            get { return Id_Fac; }
            set { Id_Fac = value; }
        }
        public int Pedido
        {
            get { return pedido; }
            set { pedido = value; }
        }
        public int Id_Cte
        {
            get { return id_Cte; }
            set { id_Cte = value; }
        }
        public int Id_Emb
        {
            get { return id_Emb; }
            set { id_Emb = value; }
        }
        public int Dia
        {
            get { return dia; }
            set { dia = value; }
        }
        public int Id_EmbDet
        {
            get { return id_EmbDet; }
            set { id_EmbDet = value; }
        }
        public string Estatus
        {
            get { return estatus; }
            set { estatus = value; }
        }
        public string Chofer
        {
            get { return chofer; }
            set { chofer = value; }
        }
        public string Camion
        {
            get { return camion; }
            set { camion = value; }
        }
        public string Cte_NomComercial1
        {
            get { return Cte_NomComercial; }
            set { Cte_NomComercial = value; }
        }
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }       

        /*Filtro*/
        public string Filtro_Estatus
        {
            get { return _filtro_Estatus; }
            set { _filtro_Estatus = value; }
        }
        public string Filtro_Embarque
        {
            get { return _filtro_Embarque; }
            set { _filtro_Embarque = value; }
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
        #endregion
    }
}
