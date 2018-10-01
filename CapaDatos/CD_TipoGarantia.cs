using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Core;
using CapaModelo;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_TipoGarantia
    {
        private String _cadenaDeConexion;

        public CD_TipoGarantia(String cadenaDeConexion)
        {
            _cadenaDeConexion = cadenaDeConexion;
        }

        public List<CatTipoGarantia> ObtenerTodos()
        {
            List<CatTipoGarantia> ret = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(_cadenaDeConexion))
            {
                ret=ctx.spCatTipoGarantia_Consulta().ToList();
            }
            return ret;
        }

        public List<CatTipoGarantia> ObtenerTodos_NOEF()
        {
            List<CatTipoGarantia> lst = new List<CatTipoGarantia>();
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(_cadenaDeConexion);
                SqlDataReader dr = null;
                string[] Parametros = { };
                object[] Valores = { };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoGarantia_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CatTipoGarantia obj = new CatTipoGarantia();
                    obj.Id_TG = dr.GetOrdinal("Id_TG");
                    obj.TG_Nombre = dr.GetValue(dr.GetOrdinal("TG_Nombre")).ToString();
                    lst.Add(obj);
                }
                //CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                lst = null;
            }
            return lst;
        }
        
        public CatTipoGarantia Consultar(int idTg)
        {
            CatTipoGarantia ret = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(_cadenaDeConexion))
            {
                var res = ctx.spCatTipoGarantia_ConsultarPorId(idTg).ToList();
                if (res.Count > 0)
                {
                    ret = res[0];
                }
            }
            return ret;
        }

        
    }
}
