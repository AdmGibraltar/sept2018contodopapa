using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_CapAcysDet
    {
        public CD_CapAcysDet(ICD_Contexto icdCtx)
        {
            _icdCtx = icdCtx;
        }

        public CD_CapAcysDet(string cadenaDeConexion)
        {
            _cadenaDeConexion = cadenaDeConexion;
        }

        #warning "Esta versión del método no cumple con el requerimiento. Su existencia tiene propósitos de compatiblidad y para la compilación exitosa de su implementación actual. Se recomienda retirar esta versión y sustituirla por su versión sobrecargada que acepta el identificador del tipo de garantía."

        /// <summary>
        /// Consulta la existencia de un producto asociado a un ACYS.
        /// </summary>
        /// <param name="idEmp"></param>
        /// <param name="idCd"></param>
        /// <param name="idPrd"></param>
        /// <param name="idTer"></param>
        /// <param name="idCte"></param>
        /// <param name="idRik"></param>
        /// <returns></returns>
        public CapAcysDet ConsultarPorProducto(int idEmp, int idCd, int idPrd, int idTer, int idCte, int idRik)
        {
            CapAcysDet ret = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(_cadenaDeConexion))
            {
                var res = ctx.spCapAcysDet_ConsultarPorProducto(idEmp, idCd, idPrd, idTer, idCte, idRik).ToList();
                if (res.Count > 0)
                {
                    ret = res[0];
                }
            }
            return ret;
        }
        
        /// <summary>
        /// Esta version reemplaza la de arriba sin utilizar EF.
        /// </summary>
        /// <param name="idEmp"></param>
        /// <param name="idCd"></param>
        /// <param name="idPrd"></param>
        /// <param name="idTer"></param>
        /// <param name="idCte"></param>
        /// <param name="idRik"></param>
        /// <returns></returns>
        public CapAcysDet Consultar_PorProducto(int idEmp, int idCd, int idPrd, int idTer, int idCte, int idRik)
        {           
            CapAcysDet obj = new CapAcysDet();

            SqlDataReader dr = null;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(_cadenaDeConexion);

            string[] Parametros = { "@Id_Emp", 
                                    "@Id_Cd", 
                                    "@Id_Prd",
                                    "@Id_Ter",
                                    "@Id_Cte",
                                    "@Id_Rik"};

            object[] Valores = { idEmp, idCd, idPrd, idTer,idCte,idRik };

            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcysDet_ConsultarPorProducto", ref dr, Parametros, Valores);

            if (dr.Read())
            {
                obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                obj.Id_Cd= Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                obj.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                obj.Id_AcsDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AcsDet")));
                obj.Id_Reg = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Reg")));
                obj.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));

                obj.Acs_Cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Cantidad")));
                obj.Acs_Frecuencia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")));

                obj.Acs_Lunes= Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Lunes"))) == 1 ? true : false;
                obj.Acs_Martes= Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Martes"))) == 1 ? true : false;
                obj.Acs_Miercoles= Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Miercoles"))) == 1 ? true : false;
                obj.Acs_Miercoles= Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Miercoles"))) == 1 ? true : false;
                obj.Acs_Jueves= Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Jueves"))) == 1 ? true : false;
                obj.Acs_Viernes= Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Viernes"))) == 1 ? true : false;
                obj.Acs_Sabado= Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Sabado")))== 1 ? true : false;

                obj.Acs_Documento= dr.GetValue(dr.GetOrdinal("Acs_Documento")).ToString();
                obj.Acs_Precio= Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Acs_Precio")));

                obj.Id_Ter= Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                obj.Acs_UltACpt= Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_UltACpt")));
                obj.Acs_UltSCpt= Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_UltSCpt")));

                obj.Acs_Modalidad = dr.GetValue(dr.GetOrdinal("Acs_Documento")).ToString();
                obj.Acs_ConsigFechaInicio = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_ConsigFechaInicio")));
                obj.Acs_ConsigFechaFin = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_ConsigFechaFin")));
                obj.Acs_canTTotal= Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_canTTotal")));
                obj.Id_AcsVersion= Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AcsVersion")));
                obj.Id_TG= Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_TG")));

                obj.Id_AcsVersion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AcsVersion")));

            } else {
                obj = null;
            }

            dr.Close();
            CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            return obj;
        }

        //
        public List<CapaEntidad.eCapAcysDet> Consulta_ProductosDeACYS(int idEmp, int idCd, int idCte, int idAcys, int idTer)
        {

            List<CapaEntidad.eCapAcysDet> Lst = new List<CapaEntidad.eCapAcysDet>();

            try
            {


                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(_cadenaDeConexion);

                string[] Parametros = { "@Id_Emp", 
                                    "@Id_Cd", 
                                    "@Id_Acys",                                    
                                    "@Id_Cte",
                                    "@Id_Ter"};

                object[] Valores = { idEmp,idCd,idAcys,idCte,idTer };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_Consulta_CapAcysDet_PorId", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CapaEntidad.eCapAcysDet obj = new CapaEntidad.eCapAcysDet();

                    obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    obj.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    obj.Id_AcsDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AcsDet")));
                    obj.Id_Reg = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Reg")));
                    obj.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));

                    obj.Acs_Cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Cantidad")));
                    obj.Acs_Frecuencia = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")));

                    obj.Acs_Lunes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Lunes"))) == 1 ? true : false;
                    obj.Acs_Martes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Martes"))) == 1 ? true : false;
                    obj.Acs_Miercoles = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Miercoles"))) == 1 ? true : false;
                    obj.Acs_Miercoles = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Miercoles"))) == 1 ? true : false;
                    obj.Acs_Jueves = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Jueves"))) == 1 ? true : false;
                    obj.Acs_Viernes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Viernes"))) == 1 ? true : false;
                    obj.Acs_Sabado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Sabado"))) == 1 ? true : false;

                    obj.Acs_Documento = dr.GetValue(dr.GetOrdinal("Acs_Documento")).ToString();
                    obj.Acs_Precio = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Acs_Precio")));

                    obj.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    obj.Acs_UltACpt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_UltACpt")));
                    obj.Acs_UltSCpt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_UltSCpt")));

                    obj.Acs_Modalidad = dr.GetValue(dr.GetOrdinal("Acs_Documento")).ToString();
                    obj.Acs_ConsigFechaInicio = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_ConsigFechaInicio")));
                    obj.Acs_ConsigFechaFin = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Acs_ConsigFechaFin")));
                    obj.Acs_canTTotal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_canTTotal")));
                    obj.Id_AcsVersion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AcsVersion")));
                    obj.Id_TG = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_TG")));

                    obj.Id_AcsVersion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AcsVersion")));

                    Lst.Add(obj);
                }

                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            } 
            catch (Exception ex)
            {
                Lst = null;
            }

            return Lst;
        }


        /// <summary>
        /// Consulta la existencia de un producto asociado al grupo de una garantía en particular del ACYS del cliente.
        /// </summary>
        /// <param name="idEmp"></param>
        /// <param name="idCd"></param>
        /// <param name="idPrd"></param>
        /// <param name="idTer"></param>
        /// <param name="idCte"></param>
        /// <param name="idRik"></param>
        /// <param name="idTg"></param>
        /// <returns></returns>
        public CapAcysDet ConsultarPorProducto(int idEmp, int idCd, int idPrd, int idTer, int idCte, int idRik, int idTg)
        {
            CapAcysDet ret = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(_cadenaDeConexion))
            {
                var res = ctx.spCapAcysDet_ConsultarPorProductoYGarantia(idEmp, idCd, idPrd, idTer, idCte, idRik, idTg).ToList();
                if (res.Count > 0)
                {
                    ret = res[0];
                }
            }
            return ret;
        }

        public CapAcysDet Insertar(CapAcysDet entity)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)_icdCtx).Contexto;
            entity = ctx.CapAcysDets.Add(entity);
            return entity;
        }

        public IEnumerable<CapAcysDet> Insertar(IEnumerable<CapAcysDet> entities)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)_icdCtx).Contexto;
            entities = ctx.CapAcysDets.AddRange(entities);
            return entities;
        }

        /// <summary>
        /// Regresa el resultado de la consulta sobre el respositorio CapAcysDet dado el identificador del ACYS, su cliente y territorio. Se infiere que se interesa trabajar sobre la última versión del resultado.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idAcys">Identificador del ACYS</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="idTerritorio">Identificador del territorio</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>IEnumerable[CapAcysDet]</returns>
        public IEnumerable<CapAcysDet> ConsultarPorAcys(int idEmp, int idCd, int idAcys, int idCte, int idTerritorio, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)_icdCtx).Contexto;
            var productos = from cad in ctx.CapAcysDets
                            join ca in ctx.CapAcys
                            on new { Id_Emp = cad.Id_Emp, Id_Cd = cad.Id_Cd, Id_Acs = cad.Id_Acs, Id_Ter = cad.Id_Ter, Id_AcsVersion = cad.Id_AcsVersion, Id_Cte = idCte } equals new { Id_Emp = ca.Id_Emp, Id_Cd = ca.Id_Cd, Id_Acs = ca.Id_Acs, Id_Ter = ca.Id_Ter, Id_AcsVersion = ca.Id_AcsVersion, Id_Cte = ca.Id_Cte.Value }
                            group cad by cad.Id_AcsVersion into grp
                            select grp.Where(det => det.Id_Acs == idAcys && det.Id_Cd == idCd && det.Id_Ter == idTerritorio && det.Id_Emp == idEmp && det.Id_AcsVersion == grp.Max(d => d.Id_AcsVersion));
            return productos.SelectMany(col => col);
        }

        private ICD_Contexto _icdCtx = null;
        private string _cadenaDeConexion;
    }
}
