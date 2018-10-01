using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;


namespace CapaNegocios
{
    public class CN_CrmCatSoluciones
    {
        public void ComboUen(Sesion sesion, ref List<CrmCatSolucion> list)
        {
            try
            {
                CD_CrmCatSoluciones claseCRM = new CD_CrmCatSoluciones();
                claseCRM.ComboUen(sesion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboSegmento(Sesion sesion, int eun,  ref List<CrmCatSolucion> list)
        {
            try
            {
                CD_CrmCatSoluciones claseCRM = new CD_CrmCatSoluciones();
                claseCRM.ComboSegmento(sesion, eun, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboArea(Sesion sesion, int segmento, ref List<CrmCatSolucion> list)
        {
            try
            {
                CD_CrmCatSoluciones claseCRM = new CD_CrmCatSoluciones();
                claseCRM.ComboArea(sesion, segmento, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaCatSolucion(Sesion sesion, int area,  ref List<CrmCatSolucion> List)
        {
            try
            {
                CD_CrmCatSoluciones claseCapaDatos = new CD_CrmCatSoluciones();
                claseCapaDatos.ConsultaCatSolucion(sesion, area, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertCatSolucion(Sesion sesion, CrmCatSolucion solucion, ref int valido)
        {
            try
            {
                CD_CrmCatSoluciones claseCapaDatos = new CD_CrmCatSoluciones();
                claseCapaDatos.InsertCatSolucion(sesion, solucion, ref valido);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarCatSolucion(Sesion sesion, CrmCatSolucion solucion, ref int valido)
        {
            try
            {
                CD_CrmCatSoluciones claseCapaDatos = new CD_CrmCatSoluciones();
                claseCapaDatos.EliminarCatSolucion(sesion, solucion, ref valido);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarSolucion(CrmCatSolucion soluciones, string Conexion, int Id_Emp, ref int valido)
        {
            try
            {
                CD_CrmCatSoluciones claseCapaDatos = new CD_CrmCatSoluciones();
                claseCapaDatos.ModificarSolucion(soluciones, Conexion,Id_Emp,  ref valido);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
