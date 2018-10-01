using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Menu
    {
        private string _StrCnx;
        public CD_Menu(string Conexion)
        {
            _StrCnx = Conexion;
        }

        public void LlenarMenu(ref DataTable Dt, Int32 Id_Cd, Int32 ID_U)
        {
            try
            {
                SqlConnection sqlcnx = default(SqlConnection);
                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataAdapter sqlda = default(SqlDataAdapter);
                Funciones DesEncr = new Funciones();

                sqlcnx = new SqlConnection(_StrCnx);
                sqlcmd = new SqlCommand("SpSysMenu", sqlcnx);
                sqlcmd.Parameters.Add(new SqlParameter("@Id_Cd", SqlDbType.NVarChar)).Value = Id_Cd;
                sqlcmd.Parameters.Add(new SqlParameter("@Id_U", SqlDbType.NVarChar)).Value = ID_U;
                sqlcmd.CommandTimeout = 0;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlda = new SqlDataAdapter(sqlcmd);
                Dt = new DataTable();
                sqlda.Fill(Dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenarGrafica( DataTable Dt, Sesion sesion, string Id_U, bool Especiales)
        {
            try
            {
                SqlConnection sqlcnx = default(SqlConnection);
                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataAdapter sqlda = default(SqlDataAdapter);
                Funciones DesEncr = new Funciones();

                sqlcnx = new SqlConnection(DesEncr.DesEncripta(_StrCnx));
                //If sesion.U_ConfigFiltro = 0 Then
                sqlcmd = new SqlCommand("SP_ConsultaPipelineFasesVta", sqlcnx);
                //Else
                //    sqlcmd = New SqlCommand("sp_ConsultaPipelineFasesVta_Oportunidades", sqlcnx)
                //End If
                sqlcmd.Parameters.Add(new SqlParameter("@Id_Ofi", SqlDbType.Int)).Value = sesion.Id_Cd;
                sqlcmd.Parameters.Add(new SqlParameter("@Id_OfiVer", SqlDbType.Int)).Value = sesion.Id_Cd_Ver;
                sqlcmd.Parameters.Add(new SqlParameter("@IdUserCombo", SqlDbType.NVarChar)).Value = Id_U;
                //sqlcmd.Parameters.Add(New SqlParameter("@FechaA", SqlDbType.NVarChar)).Value = FechaA
                //sqlcmd.Parameters.Add(New SqlParameter("@FechaB", SqlDbType.NVarChar)).Value = FechaB
                sqlcmd.Parameters.Add(new SqlParameter("@Especiales", SqlDbType.Bit)).Value = Especiales;
                //sqlcmd.Parameters.Add(New SqlParameter("@Id_User", SqlDbType.Int)).Value = sesion.Id_U
                sqlcmd.CommandTimeout = 0;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlda = new SqlDataAdapter(sqlcmd);
                Dt = new DataTable();
                sqlda.Fill(Dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenarCarpetas( DataTable Dt, Int32 Id_Ofi, Int32 IdUser)
        {
            try
            {
                SqlConnection sqlcnx = default(SqlConnection);
                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataAdapter sqlda = default(SqlDataAdapter);
                Funciones DesEncr = new Funciones();

                sqlcnx = new SqlConnection(DesEncr.DesEncripta(_StrCnx));
                sqlcmd = new SqlCommand("spCorreoCarpetas_Consulta", sqlcnx);
                sqlcmd.Parameters.Add(new SqlParameter("@Id_Ofi", SqlDbType.NVarChar)).Value = Id_Ofi;
                sqlcmd.Parameters.Add(new SqlParameter("@IdUserCreo", SqlDbType.NVarChar)).Value = IdUser;
                sqlcmd.CommandTimeout = 0;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlda = new SqlDataAdapter(sqlcmd);
                Dt = new DataTable();
                sqlda.Fill(Dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenarCaracNec( DataTable Dt, Grupos Grupo)
        {
            try
            {
                SqlConnection sqlcnx = default(SqlConnection);
                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataAdapter sqlda = default(SqlDataAdapter);
                Funciones DesEncr = new Funciones();

                sqlcnx = new SqlConnection(DesEncr.DesEncripta(_StrCnx));
                sqlcmd = new SqlCommand("spCatCaracteristicasNec_Tree", sqlcnx);
                sqlcmd.Parameters.Add(new SqlParameter("@Id_Ofi", SqlDbType.Int)).Value = Grupo.Id_Cd;
                sqlcmd.Parameters.Add(new SqlParameter("@Caracteristica_Necesidad", SqlDbType.Bit)).Value = Grupo.CaracteristicaNecesidad;
                sqlcmd.CommandTimeout = 0;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlda = new SqlDataAdapter(sqlcmd);
                Dt = new DataTable();
                sqlda.Fill(Dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenarCaracNecCliente( DataTable Dt, ExpClienteCaracNecesidad ClienteCN)
        {
            try
            {
                SqlConnection sqlcnx = default(SqlConnection);
                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataAdapter sqlda = default(SqlDataAdapter);
                Funciones DesEncr = new Funciones();

                sqlcnx = new SqlConnection(DesEncr.DesEncripta(_StrCnx));
                sqlcmd = new SqlCommand("spExpClienteCaracNecesidad_Tree", sqlcnx);
                sqlcmd.Parameters.Add(new SqlParameter("@Id_Ofi", SqlDbType.Int)).Value = ClienteCN.Id_Cd;
                sqlcmd.Parameters.Add(new SqlParameter("@Caracteristica_Necesidad", SqlDbType.Bit)).Value = ClienteCN.CaracteristicaNecesidad;
                sqlcmd.Parameters.Add(new SqlParameter("@Id_Cli", SqlDbType.Int)).Value = ClienteCN.Id_Cli;
                sqlcmd.CommandTimeout = 0;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlda = new SqlDataAdapter(sqlcmd);
                Dt = new DataTable();
                sqlda.Fill(Dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenarCaracNecExpedienteCliente( DataTable Dt, ExpClienteCaracNecesidad ClienteCN)
        {
            try
            {
                SqlConnection sqlcnx = default(SqlConnection);
                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataAdapter sqlda = default(SqlDataAdapter);
                Funciones DesEncr = new Funciones();

                sqlcnx = new SqlConnection(DesEncr.DesEncripta(_StrCnx));
                sqlcmd = new SqlCommand("spExpClienteCaracNecesidad_TreeExpCliente", sqlcnx);
                sqlcmd.Parameters.Add(new SqlParameter("@Id_Ofi", SqlDbType.Int)).Value = ClienteCN.Id_Cd;
                sqlcmd.Parameters.Add(new SqlParameter("@Caracteristica_Necesidad", SqlDbType.Bit)).Value = ClienteCN.CaracteristicaNecesidad;
                sqlcmd.Parameters.Add(new SqlParameter("@Id_Cli", SqlDbType.Int)).Value = ClienteCN.Id_Cli;
                sqlcmd.CommandTimeout = 0;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlda = new SqlDataAdapter(sqlcmd);
                Dt = new DataTable();
                sqlda.Fill(Dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenarGraficaDetalle( DataTable Dt, Sesion sesion, string Id_U, string Desc, Int32 Accion)
        {
            try
            {
                SqlConnection sqlcnx = default(SqlConnection);
                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataAdapter sqlda = default(SqlDataAdapter);
                Funciones DesEncr = new Funciones();
                sqlcnx = new SqlConnection(DesEncr.DesEncripta(_StrCnx));

                //If sesion.U_ConfigFiltro = 0 Then
                sqlcmd = new SqlCommand("SP_ConsultaPipelineDetalle", sqlcnx);
                //Else
                //    sqlcmd = New SqlCommand("sp_ConsultaPipelineDetalle_Oportunidades", sqlcnx)
                //End If

                sqlcmd.Parameters.Add(new SqlParameter("@Id_Ofi", SqlDbType.Int)).Value = sesion.Id_Cd;
                sqlcmd.Parameters.Add(new SqlParameter("@Id_OfiVer", SqlDbType.Int)).Value = sesion.Id_Cd_Ver;
                sqlcmd.Parameters.Add(new SqlParameter("@IdUserCombo", SqlDbType.NVarChar)).Value = Id_U;
                //sqlcmd.Parameters.Add(New SqlParameter("@FechaA", SqlDbType.NVarChar)).Value = FechaA
                //sqlcmd.Parameters.Add(New SqlParameter("@FechaB", SqlDbType.NVarChar)).Value = FechaB
                //sqlcmd.Parameters.Add(New SqlParameter("@Id_User", SqlDbType.Int)).Value = sesion.Id_U
                sqlcmd.Parameters.Add(new SqlParameter("@C_Descripcion", SqlDbType.NVarChar)).Value = Desc;
                sqlcmd.Parameters.Add(new SqlParameter("@Accion", SqlDbType.NVarChar)).Value = Accion;
                sqlcmd.CommandTimeout = 0;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlda = new SqlDataAdapter(sqlcmd);
                Dt = new DataTable();
                sqlda.Fill(Dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenarGridDetalle( DataTable Dt, Sesion sesion, string Id_U, string Desc, Int32 Accion)
        {
            try
            {
                SqlConnection sqlcnx = default(SqlConnection);
                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataAdapter sqlda = default(SqlDataAdapter);
                Funciones DesEncr = new Funciones();
                sqlcnx = new SqlConnection(DesEncr.DesEncripta(_StrCnx));
                sqlcmd = new SqlCommand("sp_ConsultaPipelineDetalleGrid", sqlcnx);
                sqlcmd.Parameters.Add(new SqlParameter("@Id_Ofi", SqlDbType.Int)).Value = sesion.Id_Cd;
                sqlcmd.Parameters.Add(new SqlParameter("@Id_OfiVer", SqlDbType.Int)).Value = sesion.Id_Cd_Ver;
                sqlcmd.Parameters.Add(new SqlParameter("@IdUserCombo", SqlDbType.NVarChar)).Value = Id_U;
                sqlcmd.Parameters.Add(new SqlParameter("@C_Descripcion", SqlDbType.NVarChar)).Value = Desc;

                sqlcmd.CommandTimeout = 0;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlda = new SqlDataAdapter(sqlcmd);
                Dt = new DataTable();
                sqlda.Fill(Dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenarGridDetalleOportunidades( DataTable Dt, Int32 Id_Ofi, string Id_U, Int32 mes, Int32 año, Int32 Tipo1)
        {
            try
            {
                SqlConnection sqlcnx = default(SqlConnection);
                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataAdapter sqlda = default(SqlDataAdapter);
                Funciones DesEncr = new Funciones();
                sqlcnx = new SqlConnection(DesEncr.DesEncripta(_StrCnx));
                sqlcmd = new SqlCommand("sp_ConsultaGraficaDistribuidaVentanaGrid", sqlcnx);
                sqlcmd.Parameters.Add(new SqlParameter("@Id_OfiVer", SqlDbType.Int)).Value = Id_Ofi;
                sqlcmd.Parameters.Add(new SqlParameter("@IdUserCombo", SqlDbType.NVarChar)).Value = Id_U;
                sqlcmd.Parameters.Add(new SqlParameter("@Mes", SqlDbType.Int)).Value = mes;
                sqlcmd.Parameters.Add(new SqlParameter("@Año", SqlDbType.Int)).Value = año;
                sqlcmd.Parameters.Add(new SqlParameter("@Tipo1", SqlDbType.Int)).Value = Tipo1;
                // sqlcmd.Parameters.Add(New SqlParameter("@C_Descripcion", SqlDbType.NVarChar)).Value = Desc

                sqlcmd.CommandTimeout = 0;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlda = new SqlDataAdapter(sqlcmd);
                Dt = new DataTable();
                sqlda.Fill(Dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void LlenarGridDetalle_Distribuido( DataTable Dt, Int32 Id_Ofi, Int32 Id_Op)
        {
            try
            {
                SqlConnection sqlcnx = default(SqlConnection);
                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataAdapter sqlda = default(SqlDataAdapter);
                Funciones DesEncr = new Funciones();
                sqlcnx = new SqlConnection(DesEncr.DesEncripta(_StrCnx));
                sqlcmd = new SqlCommand("sp_GraficaDistribuidaDetalladaGrid", sqlcnx);

                sqlcmd.Parameters.Add(new SqlParameter("@Id_OfiVer", SqlDbType.Int)).Value = Id_Ofi;
                sqlcmd.Parameters.Add(new SqlParameter("@Id_Op", SqlDbType.NVarChar)).Value = Id_Op;
                //sqlcmd.Parameters.Add(New SqlParameter("@FechaA", SqlDbType.NVarChar)).Value = FechaA
                //sqlcmd.Parameters.Add(New SqlParameter("@FechaB", SqlDbType.NVarChar)).Value = FechaB
                //sqlcmd.Parameters.Add(New SqlParameter("@Tipo1", SqlDbType.Int)).Value = Tipo1


                sqlcmd.CommandTimeout = 0;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlda = new SqlDataAdapter(sqlcmd);
                Dt = new DataTable();
                sqlda.Fill(Dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void LlenarGridDetalle_Prospectos( DataTable Dt, Int32 Id_Ofi, string Id_U, Int32 mes, Int32 año, Int32 Tipo)
        {
            try
            {
                SqlConnection sqlcnx = default(SqlConnection);
                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataAdapter sqlda = default(SqlDataAdapter);
                Funciones DesEncr = new Funciones();
                sqlcnx = new SqlConnection(DesEncr.DesEncripta(_StrCnx));
                sqlcmd = new SqlCommand("sp_GraficaDistribuidaProspectos_Grid", sqlcnx);

                sqlcmd.Parameters.Add(new SqlParameter("@Id_OfiVer", SqlDbType.Int)).Value = Id_Ofi;
                sqlcmd.Parameters.Add(new SqlParameter("@IdUserCombo", SqlDbType.NVarChar)).Value = Id_U;
                sqlcmd.Parameters.Add(new SqlParameter("@Mes", SqlDbType.Int)).Value = mes;
                sqlcmd.Parameters.Add(new SqlParameter("@Año", SqlDbType.Int)).Value = año;
                sqlcmd.Parameters.Add(new SqlParameter("@Tipo", SqlDbType.Int)).Value = Tipo;

                sqlcmd.CommandTimeout = 0;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlda = new SqlDataAdapter(sqlcmd);
                Dt = new DataTable();
                sqlda.Fill(Dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void LlenarGridDetalle_Distribuido_Detalle( DataTable Dt, Int32 Id_Ofi, Int32 Id_Op)
        {
            try
            {
                SqlConnection sqlcnx = default(SqlConnection);
                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataAdapter sqlda = default(SqlDataAdapter);
                Funciones DesEncr = new Funciones();
                sqlcnx = new SqlConnection(DesEncr.DesEncripta(_StrCnx));
                sqlcmd = new SqlCommand("sp_GraficaDistribuidaDetalladaGrid_Detalle", sqlcnx);

                sqlcmd.Parameters.Add(new SqlParameter("@Id_Ofi", SqlDbType.Int)).Value = Id_Ofi;
                sqlcmd.Parameters.Add(new SqlParameter("@Id_Op", SqlDbType.Int)).Value = Id_Op;


                sqlcmd.CommandTimeout = 0;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlda = new SqlDataAdapter(sqlcmd);
                Dt = new DataTable();
                sqlda.Fill(Dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void LlenarGrafica_CompromisosInicio( DataTable Dt, int Id_Ofi, int Id_User, Int32 Minutos)
        {
            try
            {
                SqlConnection sqlcnx = default(SqlConnection);
                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataAdapter sqlda = default(SqlDataAdapter);
                Funciones DesEncr = new Funciones();
                Funciones Fecha = new Funciones();
                sqlcnx = new SqlConnection(DesEncr.DesEncripta(_StrCnx));
                sqlcmd = new SqlCommand("SpInicioCompromisosConsultaGraficos", sqlcnx);
                sqlcmd.Parameters.Add(new SqlParameter("@Id_Ofi", SqlDbType.Int)).Value = Id_Ofi;
                sqlcmd.Parameters.Add(new SqlParameter("@Id_User", SqlDbType.Int)).Value = Id_User;
                sqlcmd.Parameters.Add(new SqlParameter("@Fecha", SqlDbType.NVarChar)).Value = Fecha.GetLocalTime(Minutos);
                //sqlcmd.Parameters.Add(New SqlParameter("@Id_OfiVer", SqlDbType.Int)).Value = sesion.Id_Cd_Ver
                //sqlcmd.Parameters.Add(New SqlParameter("@IdUserCombo", SqlDbType.Int)).Value = Id_U
                //sqlcmd.Parameters.Add(New SqlParameter("@FechaB", SqlDbType.NVarChar)).Value = FechaB
                //sqlcmd.Parameters.Add(New SqlParameter("@Especiales", SqlDbType.Bit)).Value = Especiales
                sqlcmd.CommandTimeout = 0;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlda = new SqlDataAdapter(sqlcmd);
                Dt = new DataTable();
                sqlda.Fill(Dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenarGraficaDistribuida( DataTable Dt, Int32 Id_Ofi, string Id_U, string FechaA, string FechaB, Int32 Tipo1)
        {
            try
            {
                SqlConnection sqlcnx = default(SqlConnection);
                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataAdapter sqlda = default(SqlDataAdapter);
                Funciones DesEncr = new Funciones();

                sqlcnx = new SqlConnection(DesEncr.DesEncripta(_StrCnx));
                sqlcmd = new SqlCommand("sp_GraficaDistribuida", sqlcnx);

                sqlcmd.Parameters.Add(new SqlParameter("@Id_OfiVer", SqlDbType.Int)).Value = Id_Ofi;
                sqlcmd.Parameters.Add(new SqlParameter("@IdUserCombo", SqlDbType.NVarChar)).Value = Id_U;
                sqlcmd.Parameters.Add(new SqlParameter("@FechaA", SqlDbType.NVarChar)).Value = FechaA;
                sqlcmd.Parameters.Add(new SqlParameter("@FechaB", SqlDbType.NVarChar)).Value = FechaB;
                sqlcmd.Parameters.Add(new SqlParameter("@Tipo1", SqlDbType.Int)).Value = Tipo1;

                sqlcmd.CommandTimeout = 0;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlda = new SqlDataAdapter(sqlcmd);
                Dt = new DataTable();
                sqlda.Fill(Dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenarGraficaOpDetalle( DataTable Dt, Int32 Id_Ofi, string Id_U, string FechaA, string FechaB, Int32 Tipo1)
        {
            try
            {
                SqlConnection sqlcnx = default(SqlConnection);
                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataAdapter sqlda = default(SqlDataAdapter);
                Funciones DesEncr = new Funciones();

                sqlcnx = new SqlConnection(DesEncr.DesEncripta(_StrCnx));
                sqlcmd = new SqlCommand("sp_GraficaDistribuidaDetallada", sqlcnx);

                sqlcmd.Parameters.Add(new SqlParameter("@Id_OfiVer", SqlDbType.Int)).Value = Id_Ofi;
                sqlcmd.Parameters.Add(new SqlParameter("@IdUserCombo", SqlDbType.NVarChar)).Value = Id_U;
                sqlcmd.Parameters.Add(new SqlParameter("@FechaA", SqlDbType.NVarChar)).Value = FechaA;
                sqlcmd.Parameters.Add(new SqlParameter("@FechaB", SqlDbType.NVarChar)).Value = FechaB;
                sqlcmd.Parameters.Add(new SqlParameter("@Tipo1", SqlDbType.Int)).Value = Tipo1;

                sqlcmd.CommandTimeout = 0;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlda = new SqlDataAdapter(sqlcmd);
                Dt = new DataTable();
                sqlda.Fill(Dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenarGraficaOpDetalleVentana( DataTable Dt, Int32 Id_Ofi, string Id_U, Int32 mes, Int32 año, Int32 Tipo1, Int32 Accion)
        {
            try
            {
                SqlConnection sqlcnx = default(SqlConnection);
                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataAdapter sqlda = default(SqlDataAdapter);
                Funciones DesEncr = new Funciones();

                sqlcnx = new SqlConnection(DesEncr.DesEncripta(_StrCnx));
                sqlcmd = new SqlCommand("sp_ConsultaGraficaDistribuidaVentana", sqlcnx);

                sqlcmd.Parameters.Add(new SqlParameter("@Id_OfiVer", SqlDbType.Int)).Value = Id_Ofi;
                sqlcmd.Parameters.Add(new SqlParameter("@IdUserCombo", SqlDbType.NVarChar)).Value = Id_U;
                sqlcmd.Parameters.Add(new SqlParameter("@Mes", SqlDbType.Int)).Value = mes;
                sqlcmd.Parameters.Add(new SqlParameter("@Año", SqlDbType.Int)).Value = año;
                sqlcmd.Parameters.Add(new SqlParameter("@Tipo1", SqlDbType.Int)).Value = Tipo1;
                sqlcmd.Parameters.Add(new SqlParameter("@Accion", SqlDbType.Int)).Value = Accion;

                sqlcmd.CommandTimeout = 0;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlda = new SqlDataAdapter(sqlcmd);
                Dt = new DataTable();
                sqlda.Fill(Dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenarGraficaProspectos( DataTable Dt, Int32 Id_Ofi, string Id_U, string FechaA, string FechaB)
        {
            try
            {
                SqlConnection sqlcnx = default(SqlConnection);
                SqlCommand sqlcmd = default(SqlCommand);
                SqlDataAdapter sqlda = default(SqlDataAdapter);
                Funciones DesEncr = new Funciones();

                sqlcnx = new SqlConnection(DesEncr.DesEncripta(_StrCnx));
                sqlcmd = new SqlCommand("sp_GraficaDistribuidaProspectos", sqlcnx);

                sqlcmd.Parameters.Add(new SqlParameter("@Id_Ofi", SqlDbType.Int)).Value = Id_Ofi;
                sqlcmd.Parameters.Add(new SqlParameter("@IdUserCombo", SqlDbType.NVarChar)).Value = Id_U;
                sqlcmd.Parameters.Add(new SqlParameter("@FechaA", SqlDbType.NVarChar)).Value = FechaA;
                sqlcmd.Parameters.Add(new SqlParameter("@FechaB", SqlDbType.NVarChar)).Value = FechaB;

                sqlcmd.CommandTimeout = 0;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlda = new SqlDataAdapter(sqlcmd);
                Dt = new DataTable();
                sqlda.Fill(Dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
