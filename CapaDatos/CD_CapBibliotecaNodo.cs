using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_CapBibliotecaNodo
    {

        public List<eCapBibliotecaNodo> Inicializa_LibreriaEvidencias(int id_Emp, int id_Cd, int Id_U, int id_Op, string _conexion)
        {
            List<eCapBibliotecaNodo> Lst = new List<eCapBibliotecaNodo>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(_conexion);
                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd", 
                                        "@Id_U",
                                        "@Id_Op" };
                object[] Valores = { id_Emp, id_Cd, Id_U, id_Op };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_InicializarLibreriaEvidencias", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    var Obj = new eCapBibliotecaNodo();
                    Obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    Obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    Obj.Id_Biblioteca = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Biblioteca")));
                    Obj.Id_BiblioNodo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_BiblioNodo")));
                    Obj.BiblioNodo_Nombre = dr.GetValue(dr.GetOrdinal("BiblioNodo_Nombre")).ToString();
                    Obj.Id_BiblioNodo_Padre = dr.GetValue(dr.GetOrdinal("Id_BiblioNodo_Padre")).ToString();
                    Obj.Id_Recurso = dr.GetValue(dr.GetOrdinal("Id_Recurso")).ToString();
                    Lst.Add(Obj);
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

        public List<eCapBibliotecaNodo> ConsultaNodos(int idEmp, int idCd, int IdU, string _conexion)
        {
            List<eCapBibliotecaNodo> Lst = new List<eCapBibliotecaNodo>();

            try
            {

                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(_conexion);

                string[] Parametros = { "@Id_Emp", 
                                        "@Id_Cd", 
                                        "@Id_U" };

                object[] Valores = { idEmp, idCd, IdU };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_LibreriaEvidencias_ConsultaNodos", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    var Obj = new eCapBibliotecaNodo();

                    Obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    Obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    Obj.Id_Biblioteca = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Biblioteca")));
                    Obj.Id_BiblioNodo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_BiblioNodo")));
                    Obj.BiblioNodo_Nombre = dr.GetValue(dr.GetOrdinal("BiblioNodo_Nombre")).ToString();
                    Obj.Id_BiblioNodo_Padre = dr.GetValue(dr.GetOrdinal("Id_BiblioNodo_Padre")).ToString();
                    Obj.Id_Recurso = dr.GetValue(dr.GetOrdinal("Id_Recurso")).ToString();

                    Lst.Add(Obj);
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
        /// Devuelve el resultado de la consulta sobre la entidad CapBibliotecaNodo condicionando las entradas por el identificador del usuario.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idU">Identificador del usuario asociado a las entradas de la entidad</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>IQueryable[CapBibliotecaNodo]</returns>
        public IQueryable<CapBibliotecaNodo> ConsultarPorUsuario(int idEmp, int idCd, int idU, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var entradas = from cbu in ctx.CapBibliotecaUsuarios
                           where cbu.Id_Emp == idEmp && cbu.Id_Cd == idCd && cbu.Id_U == idU
                           select cbu.CatBiblioteca;
            return entradas.SelectMany(cbu => cbu.CapBibliotecaNodoes);
        }

        /// <summary>
        /// Inserta una entrada en el repositorio CapBibliotecaNodo.
        /// </summary>
        /// <param name="entrada">Instancia de datos del repositorio CapBibliotecaNodo</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>CapBibliotecaNodo</returns>
        public CapBibliotecaNodo Insertar(CapBibliotecaNodo entrada, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            return ctx.CapBibliotecaNodoes.Add(entrada);
        }

        /// <summary>
        /// Regresa el resultado de consultar el repositorio CapBiblioNodo condicionado por el identificador de la entidad
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idU">Identificador del usuario</param>
        /// <param name="idBiblioteca">Identificador de la biblioteca del usuario</param>
        /// <param name="idBiblioNodo">Identificador del nivel del repositorio</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>CapBibliotecaNodo</returns>
        public CapBibliotecaNodo ConsultarPorIdentificador(int idEmp, int idCd, int idU, int idBiblioteca, int idBiblioNodo, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var entradas = (from cbu in ctx.CapBibliotecaUsuarios
                            where cbu.Id_Emp == idEmp && cbu.Id_Cd == idCd && cbu.Id_U == idU && cbu.Id_Biblioteca == idBiblioteca
                            select cbu);
            if (entradas.Count() > 0)
            {
                var resultado = entradas.First();
                var nodos = resultado.CatBiblioteca.CapBibliotecaNodoes.Where(cbn => cbn.Id_BiblioNodo == idBiblioNodo);
                if (nodos.Count() > 0)
                {
                    return nodos.First();
                }
            }
            return null;
        }

        /// <summary>
        /// Regresa la consulta a la entidad CapBibliotecaNodo condicionando el nombre del nodo de interés.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idBiblioteca">Identificador de la biblioteca por defecto del usuario en sesión</param>
        /// <param name="idBiblioNodoPadre">Identificador del nodo padre del nodo a buscar</param>
        /// <param name="nombre">Nombre del nodo de interés</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>CapBibliotecaNodo</returns>
        public CapBibliotecaNodo ObtenerPorNombre(int idEmp, int idCd, int idBiblioteca, int? idBiblioNodoPadre, string nombre, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var nodos = from ibn in ctx.CapBibliotecaNodoes
                        where ibn.Id_BiblioNodo_Padre == idBiblioNodoPadre && ibn.Id_Emp == idEmp && ibn.Id_Cd == idCd && ibn.Id_Biblioteca == idBiblioteca && ibn.BiblioNodo_Nombre.CompareTo(nombre) == 0
                        select ibn;
            if (nodos.Count() > 0)
            {
                return nodos.First();
            }
            return null;
        }
    }
}
