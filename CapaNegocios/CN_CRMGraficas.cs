using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_CRMGraficas
    {
        public void GraficaActividad(ref System.Data.DataSet dsGraficaActividad, string Id_Cd, int Id_Emp, int? Id_U, int? GerSeg_Id, int? GerUen_Id, string Conexion, int intDdl)
        {
            try
            {
                CD_CRMGraficas claseCapaDatos = new CD_CRMGraficas();
                claseCapaDatos.GraficaActividad(ref dsGraficaActividad, Id_Cd, Id_Emp, Id_U, GerSeg_Id, GerUen_Id, Conexion, intDdl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GraficaDistribucion(int Id_Emp, int Id_Cd, string Estatus, int? Id_U, int? GerSeg_Id, int? GerUen_Id, int intDdl, ref DataSet dsGraficaDistribucion, string Conexion)
        {
            try
            {
                CD_CRMGraficas claseCapaDatos = new CD_CRMGraficas();
                claseCapaDatos.GraficaDistribucion(Id_Emp, Id_Cd, Estatus, Id_U, GerSeg_Id, GerUen_Id, intDdl, dsGraficaDistribucion, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
