using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using System.Data;
namespace CapaNegocios
{
    public class CN_MonitoreoIndicadoresUtilidad
    {



        public void MonitoreoGestionRentabildiadRIK_Buscar(MonitoreoGestionRentabilidadRIK monitoreogestionrentabilidadRIK, string Conexion, ref List<MonitoreoGestionRentabilidadRIK> List
            , int Id_Emp
            , int Id_Cd
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            , int Id_Rik
            , ref string Grafica
            )
        {


            try
            {
                CD_MonitoreoIndicadoresUtilidad claseCapaDatos = new CD_MonitoreoIndicadoresUtilidad();

                claseCapaDatos.MonitoreoGestionRentabildiadRIK_Buscar(monitoreogestionrentabilidadRIK, Conexion, ref List
            , Id_Emp
            , Id_Cd
            , MesInicial
            , AnioInicial
            , MesFinal
            , AnioFinal
            , Id_U
            , Id_Rik
            , ref Grafica
            );
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public void Reporte_Monitore_Territorio(string Conexion
            , int Id_Emp
            , int Id_Cd
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            , ref String NombreArchivo
            )
        {


            try
            {
                CD_MonitoreoIndicadoresUtilidad claseCapaDatos = new CD_MonitoreoIndicadoresUtilidad();

                claseCapaDatos.Reporte_Monitore_Representante_Territorio(Conexion
            , Id_Emp
            , Id_Cd
            , MesInicial
            , AnioInicial
            , MesFinal
            , AnioFinal
            , Id_U
            , ref NombreArchivo
            );
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public void Reporte_Monitore_Representante(string Conexion
            , int Id_Emp
            , int Id_Cd
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            , ref String NombreArchivo
            )
        {


            try
            {
                CD_MonitoreoIndicadoresUtilidad claseCapaDatos = new CD_MonitoreoIndicadoresUtilidad();

                claseCapaDatos.Reporte_Monitore_Representante(Conexion
            , Id_Emp
            , Id_Cd
            , MesInicial
            , AnioInicial
            , MesFinal
            , AnioFinal
            , Id_U
            ,ref NombreArchivo
            );
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public void Reporte_Monitore_Cliente(string Conexion
            , int Id_Emp
            , int Id_Cd
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            , ref String NombreArchivo
            )
        {


            try
            {
                CD_MonitoreoIndicadoresUtilidad claseCapaDatos = new CD_MonitoreoIndicadoresUtilidad();

                claseCapaDatos.Reporte_Monitore_Representante_Cliente(Conexion
            , Id_Emp
            , Id_Cd
            , MesInicial
            , AnioInicial
            , MesFinal
            , AnioFinal
            , Id_U
            , ref NombreArchivo
            );
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        
        public void Reporte_Monitore_Acciones_Producto_Cumplimiento(string Conexion
            , int Id_Emp
            , int Id_Cd
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            , ref String NombreArchivo
            )
        {


            try
            {
                CD_MonitoreoIndicadoresUtilidad claseCapaDatos = new CD_MonitoreoIndicadoresUtilidad();

                claseCapaDatos.Reporte_Monitore_Acciones_Producto_Cumplimiento(Conexion
            , Id_Emp
            , Id_Cd
            , MesInicial
            , AnioInicial
            , MesFinal
            , AnioFinal
            , Id_U
            , ref NombreArchivo
            );
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public void Reporte_Monitore_Acciones(string Conexion
            , int Id_Emp
            , int Id_Cd
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            , ref String NombreArchivo
            )
        {


            try
            {
                CD_MonitoreoIndicadoresUtilidad claseCapaDatos = new CD_MonitoreoIndicadoresUtilidad();

                claseCapaDatos.Reporte_Monitore_Acciones_Producto(Conexion
            , Id_Emp
            , Id_Cd
            , MesInicial
            , AnioInicial
            , MesFinal
            , AnioFinal
            , Id_U
            , ref NombreArchivo
            );
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        
        

        public void MonitoreoGestionRentabildiad_Buscar(MonitoreoGestionRentabilidad monitoreogestionrentabilidad, string Conexion, ref List<MonitoreoGestionRentabilidad> List
            , int Id_Emp
            , int Id_Cd
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            , ref string Grafica
            )
        {


            try
            {
                CD_MonitoreoIndicadoresUtilidad claseCapaDatos = new CD_MonitoreoIndicadoresUtilidad();

                claseCapaDatos.MonitoreoGestionRentabildiad_Buscar(monitoreogestionrentabilidad, Conexion, ref List
            , Id_Emp
            , Id_Cd
            , MesInicial
            , AnioInicial
            , MesFinal
            , AnioFinal
            , Id_U
            , ref Grafica
            );
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }



        public void MonitoreoIndicadoresUtilidad_Buscar(MonitoreoIndicadoresUtilidad monitoreoIndicadoresUtilidad, string Conexion, ref List<MonitoreoIndicadoresUtilidad> List
            , int Id_Emp
            , int Id_Cd
            , int Id_Ter
            , int Id_Rik
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            , ref string Grafica
            )
        {


            try
            {
                CD_MonitoreoIndicadoresUtilidad claseCapaDatos = new CD_MonitoreoIndicadoresUtilidad();

                claseCapaDatos.MonitoreoIndicadoresUtilidad_Buscar(monitoreoIndicadoresUtilidad, Conexion, ref List
            , Id_Emp
            , Id_Cd
            , Id_Ter
            , Id_Rik
            , MesInicial
            , AnioInicial
            , MesFinal
            , AnioFinal
            , Id_U
            , ref Grafica
            );
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

    }
}
