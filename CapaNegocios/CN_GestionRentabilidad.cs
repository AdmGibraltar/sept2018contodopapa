using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using System.Data;
namespace CapaNegocios
{
   public class CN_GestionRentabilidad
    {
        
        public void ConsultaGestionRentabilidad_Buscar(GestionRentabilidad gestionRentabilidad, string Conexion, ref List<GestionRentabilidad> List		    
            , int Id_Emp
            , int Id_Cd
            , string Id_Cte
            , string Id_Ter
            , int Id_Rik
            , string NombreCliente
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            , int UBPorCliente
            , int Categorias
            , int UBPorQuimicos
            , int UBPorPapelTradicional
            , int UBPorSistemaDiferenciado
            , int UBPorJarcieria
            , int UBPorAccesorios
            , int UBPorBolsaBasura
            )
        {  
            try
            {
                CD_GestionRentabilidad claseCapaDatos = new CD_GestionRentabilidad();

                claseCapaDatos.ConsultaGestionRentabilidad_Buscar(gestionRentabilidad, Conexion, ref List           
            , Id_Emp
            , Id_Cd
            , Id_Cte
            , Id_Ter
            , Id_Rik
            , NombreCliente
            , MesInicial
            , AnioInicial
            , MesFinal
            , AnioFinal
            , Id_U
            , UBPorCliente
            , Categorias
            , UBPorQuimicos
            , UBPorPapelTradicional
            , UBPorSistemaDiferenciado
            , UBPorJarcieria
            , UBPorAccesorios
            , UBPorBolsaBasura                        
            );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaGestionRentabilidadLista_Buscar(GestionRentabilidadLista gestionRentabilidadlista, string Conexion, ref List<GestionRentabilidadLista> List
            , int Id_Emp
            , int Id_Cd
            , string Id_Cte
            , string Id_Ter
            , int Id_Rik
            , string NombreCliente
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            )
        {
            try
            {
                CD_GestionRentabilidad claseCapaDatos = new CD_GestionRentabilidad();

                claseCapaDatos.ConsultaGestionRentabilidadLista_Buscar(gestionRentabilidadlista, Conexion, ref List
            , Id_Emp
            , Id_Cd
            , Id_Cte
            , Id_Ter
            , Id_Rik
            , NombreCliente
            , MesInicial
            , AnioInicial
            , MesFinal
            , AnioFinal
            , Id_U
            );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaGestionRentabilidadMonitoreo_Buscar(GestionRentabilidad gestionRentabilidad, string Conexion, ref List<GestionRentabilidad> List
            , int Id_Emp
            , int Id_Cd
            , string Id_Cte
            , string Id_Ter
            , int Id_Rik
            , string NombreCliente
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            )
        {
            try
            {
                CD_GestionRentabilidad claseCapaDatos = new CD_GestionRentabilidad();

                claseCapaDatos.ConsultaGestionRentabilidadMonitoreo_Buscar(gestionRentabilidad, Conexion, ref List
            , Id_Emp
            , Id_Cd
            , Id_Cte
            , Id_Ter
            , Id_Rik
            , NombreCliente
            , MesInicial
            , AnioInicial
            , MesFinal
            , AnioFinal
            , Id_U
            );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       /*
        public void ConsultaGestionRentabilidad_Buscar(GestionRentabilidad gestionRentabilidad, string p, ref List<GestionRentabilidad> listGestionRentabilidad, int p_2, int p_3, Telerik.Web.UI.RadTextBox radTextBox, Telerik.Web.UI.RadTextBox radTextBox_2, Telerik.Web.UI.RadTextBox radTextBox_3)
        {
            throw new NotImplementedException();
        }*/
    }
}
