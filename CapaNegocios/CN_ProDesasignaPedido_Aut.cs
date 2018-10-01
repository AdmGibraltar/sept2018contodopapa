using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_ProDesasignaPedido_Aut
    {

        public void DesasignaPedido_Aut(int Id_Emp, int Id_Cd, int Credito, ref  int verificador, string Conexion)
        {
            try
            {
                CD_ProDesasignaPedido_Aut claseCapaDatos = new CD_ProDesasignaPedido_Aut();
                claseCapaDatos.DesasignaPedido_Aut(Id_Emp, Id_Cd, Credito, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void AsignacionPedido_Aut(int Id_Emp, int Id_Cd, DateTime Fecha, int Id_U, int Credito, ref  int verificador, string Conexion)
        {
            try
            {
                CD_ProDesasignaPedido_Aut claseCapaDatos = new CD_ProDesasignaPedido_Aut();
                claseCapaDatos.AsignacionPedido_Aut(Id_Emp, Id_Cd, Fecha, Id_U, Credito, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AsignacionPedido_CteTerr(CapaEntidad.Pedido ped, int Credito, ref int verificador, string Conexion)
        {
            try
            {
                CD_ProDesasignaPedido_Aut claseCapaDatos = new CD_ProDesasignaPedido_Aut();
                claseCapaDatos.AsignacionPedido_CteTerr(ped, Credito, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AsignacionPedido_Aut(int Id_Emp, int Id_Cd, DateTime Fecha, int Id_U, string Id_Ped, ref int verificador, string Conexion)
        {
            try
            {
                CD_ProDesasignaPedido_Aut claseCapaDatos = new CD_ProDesasignaPedido_Aut();
                claseCapaDatos.AsignacionPedido_Aut(Id_Emp, Id_Cd, Fecha, Id_U, Id_Ped, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DesasignacionPedido_Aut(int Id_Emp, int Id_Cd, string Id_Ped, ref int verificador, string Conexion)
        {
            try
            {
                CD_ProDesasignaPedido_Aut claseCapaDatos = new CD_ProDesasignaPedido_Aut();
                claseCapaDatos.DesasignacionPedido_Aut(Id_Emp, Id_Cd,   Id_Ped, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
