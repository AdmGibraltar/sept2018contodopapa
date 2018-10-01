using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Ctrl
    {
        public void InsertarCtrl(PermisoControl ctrl, string Conexion, ref int verificador)
        {
            try
            {
                 
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Ctrl", 
	                                    "@Ctrl_Descripcion", 
	                                    "@Sm_Cve",
	                                    "@Tipo",
                                        "@Label"

                                      };
                object[] Valores = { 
                                        ctrl.Id_Ctrl,
                                        ctrl.Descripcion,
                                        ctrl.Sm_Cve,
                                        ctrl.Tipo,
                                        ctrl.Ctrl_Label
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSysCtrl_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
