using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class CumpVentaInstalada
    {
        #region variables
        /*Filtros*/
        int id_cd;
        int formato;
        string sformato;
        int cmbFormato;
        string sCmbFormato;
        string semana;
        string ssemana;
        string rik;
        string srik;
        string territorio;
        string sterritorio;
        string producto;
        string sproducto;
        int nivel;
        string snivel;
        bool detalle;
        string sDetalle;
        /*Valores*/
        double totalFacturado;
        double totalNca;
        double totalNcr;
        double totalDevParciales;
        double VtaDirecta;
        double VIF;
        double VIFFueraPeriodo;
        double VINueva;
        double VtaEsporadica; 
        /*encabezados*/
        string encabezado1;
        string encabezado2;
        string encabezado3;
        string encabezado4;
        string encabezado5;
        string encabezado6;
        string encabezado7;   
        #endregion

        #region factorizacion
        public int Id_cd
        {
            get { return id_cd; }
            set { id_cd = value; }
        }
        public int Formato
        {
            get { return formato; }
            set { formato = value; }
        }
        public string Sformato
        {
            get { return sformato; }
            set { sformato = value; }
        }
        public int CmbFormato
        {
            get { return cmbFormato; }
            set { cmbFormato = value; }
        }
        public string SCmbFormato
        {
            get { return sCmbFormato; }
            set { sCmbFormato = value; }
        }
        public string Semana
        {
            get { return semana; }
            set { semana = value; }
        }
        public string Ssemana
        {
            get { return ssemana; }
            set { ssemana = value; }
        }
        public string Rik
        {
            get { return rik; }
            set { rik = value; }
        }
        public string Srik
        {
            get { return srik; }
            set { srik = value; }
        }
        public string Territorio
        {
            get { return territorio; }
            set { territorio = value; }
        }
        public string Sterritorio
        {
            get { return sterritorio; }
            set { sterritorio = value; }
        }
        public string Producto
        {
            get { return producto; }
            set { producto = value; }
        }
        public string Sproducto
        {
            get { return sproducto; }
            set { sproducto = value; }
        }
        public int Nivel
        {
            get { return nivel; }
            set { nivel = value; }
        }
        public string Snivel
        {
            get { return snivel; }
            set { snivel = value; }
        }
        public bool Detalle
        {
            get { return detalle; }
            set { detalle = value; }
        }
        public string SDetalle
        {
            get { return sDetalle; }
            set { sDetalle = value; }
        }
        /*valores*/
        public double TotalFacturado
        {
            get { return totalFacturado; }
            set { totalFacturado = value; }
        }
        public double TotalNca
        {
            get { return totalNca; }
            set { totalNca = value; }
        }
        public double TotalNcr
        {
            get { return totalNcr; }
            set { totalNcr = value; }
        }
        public double TotalDevParciales
        {
            get { return totalDevParciales; }
            set { totalDevParciales = value; }
        }
        public double VtaDirecta1
        {
            get { return VtaDirecta; }
            set { VtaDirecta = value; }
        }
        public double VIF1
        {
            get { return VIF; }
            set { VIF = value; }
        }
        public double VIFFueraPeriodo1
        {
            get { return VIFFueraPeriodo; }
            set { VIFFueraPeriodo = value; }
        }
        public double VINueva1
        {
            get { return VINueva; }
            set { VINueva = value; }
        }
        public double VtaEsporadica1
        {
            get { return VtaEsporadica; }
            set { VtaEsporadica = value; }
        }
        /*encabezados*/
        public string Encabezado1
        {
            get { return encabezado1; }
            set { encabezado1 = value; }
        }
        public string Encabezado2
        {
            get { return encabezado2; }
            set { encabezado2 = value; }
        }
        public string Encabezado3
        {
            get { return encabezado3; }
            set { encabezado3 = value; }
        }
        public string Encabezado4
        {
            get { return encabezado4; }
            set { encabezado4 = value; }
        }
        public string Encabezado5
        {
            get { return encabezado5; }
            set { encabezado5 = value; }
        }
        public string Encabezado6
        {
            get { return encabezado6; }
            set { encabezado6 = value; }
        }
        public string Encabezado7
        {
            get { return encabezado7; }
            set { encabezado7 = value; }
        }
        #endregion
    }
}
