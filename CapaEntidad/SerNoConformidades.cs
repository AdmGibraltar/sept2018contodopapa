using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class SerNoConformidades
    {
        int id_Cd;
        string clientes;
        string sClientes;
        string territorio;
        string sTerritorio;
        int reclamacion;
        string sReclamacion;
        string fechaini;
        string fechafin;
        int tipoRecl;
        int estatusRecl;
        string sTipoRecl;
        string SEstatusRecl;
        string sFecha;

        public int Id_Cd
        {
            get { return id_Cd; }
            set { id_Cd = value; }
        }
        public string Clientes
        {
            get { return clientes; }
            set { clientes = value; }
        }
        public string SClientes
        {
            get { return sClientes; }
            set { sClientes = value; }
        }
        public string Territorio
        {
            get { return territorio; }
            set { territorio = value; }
        }
        public string STerritorio
        {
            get { return sTerritorio; }
            set { sTerritorio = value; }
        }
        public int Reclamacion
        {
            get { return reclamacion; }
            set { reclamacion = value; }
        }
        public string SReclamacion
        {
            get { return sReclamacion; }
            set { sReclamacion = value; }
        }
        public string Fechaini
        {
            get { return fechaini; }
            set { fechaini = value; }
        }
        public string Fechafin
        {
            get { return fechafin; }
            set { fechafin = value; }
        }
        public int TipoRecl
        {
            get { return tipoRecl; }
            set { tipoRecl = value; }
        }
        public int EstatusRecl
        {
            get { return estatusRecl; }
            set { estatusRecl = value; }
        }
        public string STipoRecl
        {
            get { return sTipoRecl; }
            set { sTipoRecl = value; }
        }
        public string SEstatusRecl1
        {
            get { return SEstatusRecl; }
            set { SEstatusRecl = value; }
        }
        public string SFecha
        {
            get { return sFecha; }
            set { sFecha = value; }
        }
    }
}
