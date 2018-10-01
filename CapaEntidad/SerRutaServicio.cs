using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class SerRutaServicio
    {
        #region Variables
        /*Filtro*/
        DateTime fechaInicial;
        DateTime fechaFinal;
        string ruta;
        string territorio;
        string cliente;
        int orden;
        bool detalle;
        string sfechaInicial;
        string sfechaFinal;
        string sruta;
        string sterritorio;
        string scliente;
        string sorden;
        string sfechaini;
        string sfechafin;
        int Reporte;
        int id_Cd;
        #endregion

        #region Refactorizar
        public int Id_Cd
        {
            get { return id_Cd; }
            set { id_Cd = value; }
        }
        public DateTime FechaInicial
        {
            get { return fechaInicial; }
            set { fechaInicial = value; }
        }
        public DateTime FechaFinal
        {
            get { return fechaFinal; }
            set { fechaFinal = value; }
        }
        public string Ruta
        {
            get { return ruta; }
            set { ruta = value; }
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
        public int Orden
        {
            get { return orden; }
            set { orden = value; }
        }
        public bool Detalle
        {
            get { return detalle; }
            set { detalle = value; }
        }
        public string SfechaInicial
        {
            get { return sfechaInicial; }
            set { sfechaInicial = value; }
        }
        public string SfechaFinal
        {
            get { return sfechaFinal; }
            set { sfechaFinal = value; }
        }
        public string Sruta
        {
            get { return sruta; }
            set { sruta = value; }
        }
        public string Sterritorio
        {
            get { return sterritorio; }
            set { sterritorio = value; }
        }
        public string Scliente
        {
            get { return scliente; }
            set { scliente = value; }
        }
        public string Sorden
        {
            get { return sorden; }
            set { sorden = value; }
        }
        public string Sfechaini
        {
            get { return sfechaini; }
            set { sfechaini = value; }
        }
        public string Sfechafin
        {
            get { return sfechafin; }
            set { sfechafin = value; }
        }
        public int Reporte1
        {
            get { return Reporte; }
            set { Reporte = value; }
        }

        #endregion
    }
}
