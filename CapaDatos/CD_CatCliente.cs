using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;
using CapaModelo;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;

namespace CapaDatos
{
    public class CD_CatCliente
    {
        public void ConsultarClienteSigCentroDist(ref int verificador, int Id_Emp, int Id_Cd_Ver, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { Id_Emp, Id_Cd_Ver };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteClaveSig_Consulta", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarClientes(Clientes clientes, string Conexion, ref int verificador, ICD_Contexto idcCtx)
        {
            //CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            ICD_Contexto<sianwebmty_gEntities> ctx = ((ICD_Contexto<sianwebmty_gEntities>)idcCtx);
            IDbTransaction transaction = ctx.Contexto.Database.CurrentTransaction.UnderlyingTransaction;
            SqlCommand sqlcmd = transaction.Connection.CreateCommand() as SqlCommand;
            sqlcmd.Transaction = (System.Data.SqlClient.SqlTransaction)transaction;
            try
            {
                //CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp",
		                                "@Id_Cd", 
                                        "@Id_Cte", 
                                        "@Id_Cfe", 
                                        "@Id_Corp",
		                                "@Cte_NomComercial", 
                                        "@Cte_NomCorto",
		                                "@Cte_FacCalle", 
		                                "@Cte_FacNumero", 
                                        "@Cte_FacNumeroInterior", 
		                                "@Cte_FacCp", 
		                                "@Cte_FacColonia", 
		                                "@Cte_FacMunicipio", 
		                                "@Cte_FacTel", 
		                                "@Cte_FacRfc", 
		                                "@Cte_FacEstado", 
		                                "@Cte_Calle", 
		                                "@Cte_Numero", 
                                        "@Cte_NumeroInterior", 
		                                "@Cte_Cp", 
		                                "@Cte_Colonia", 
		                                "@Cte_Municipio", 
		                                "@Cte_Estado", 
		                                "@Cte_Telefono", 
		                                "@Cte_Fax",
                                        "@Cte_Rfc",
		                                "@Cte_Contacto", 
		                                "@Cte_Tipo", 
		                                "@Cte_Email", 
		                                "@Cte_Credito", 
		                                "@Cte_Facturacion", 
		                                "@Id_Mon", 
		                                "@Cte_LimCobr", 
		                                "@Cte_RHoraam1", 
                                        "@Cte_RHoraam2", 
                                        "@Cte_RHorapm1", 
                                        "@Cte_RHorapm2", 
		                                "@Cte_RLunes", 
		                                "@Cte_RMartes", 
		                                "@Cte_RMiercoles", 
		                                "@Cte_RJueves", 
		                                "@Cte_RViernes", 
		                                "@Cte_RSabado", 
		                                "@Cte_RDomingo", 
		                                "@Cte_CondPago", 
		                                "@Cte_CPLunes", 
		                                "@Cte_CPMartes", 
		                                "@Cte_CPMiercoles", 
		                                "@Cte_CPJueves", 
		                                "@Cte_CPViernes", 
		                                "@Cte_CPSabado", 
		                                "@Cte_CPDomingo", 
		                                "@Cte_Comisiones", 
		                                "@Cte_DesgIva", 
		                                "@Cte_RetIva", 
		                                "@Cte_AsignacionPed", 
		                                "@Id_Ade", 
                                        "@Cte_SerieNcre",
                                        "@Cte_SerieNca",
		                                "@Cte_Activo",
                                        "@Cte_CreditoSuspendido",
                                        "@Cte_PHoraam1",
                                        "@Cte_PHoraam2",
                                        "@Cte_PHorapm1",
                                        "@Cte_PHorapm2",
                                        "@Cte_SemRec",
                                        "@Cte_RecLunes",
                                        "@Cte_RecMartes",
                                        "@Cte_RecMiercoles",
                                        "@Cte_RecJueves",
                                        "@Cte_RecViernes",
                                        "@Cte_RecSabado",
                                        "@Cte_RecDomingo",
                                        "@Cte_Efectivo",
                                        "@Cte_Factoraje",
                                        "@Cte_Cheque",
                                        "@Cte_Transferencia",
                                        "@Cte_ReqOrdenCompra",
                                        "@Cte_Documentos",
                                        "@Cte_TelCobranza1",
                                        "@Cte_TelCobranza2",
                                        "@Cte_RemisionElect",
                                        "@Cte_BPorcNotaCredito",
                                        "@Cte_PorcNotaCredito",
                                        "@Cte_PorcientoRetencion",
                                        "@Cte_BPorcientoIVA",                                       
                                        "@Cte_PorcientoIVA",
                                        "@Cte_UDigitos",
                                        "@Cte_Referencia",
                                        "@Cte_AutorizaPlazo_IdU",
                                        "@Cte_AutorizaPlazo_IdCd",
                                        "@Cte_Correo1",
                                        "@Cte_Correo2",
                                        "@Cte_Correo3",
                                        "@Cte_NumCuentaContNacional",
                                        "@Cte_SemRev",
                                        "@Cte_SemRev2",
                                        "@Cte_SemCob",
                                        "@Id_TCte",
                                        "@Cte_NumeroCuenta",
                                        "@Cte_ReferenciaTecleada",
                                        "@Cte_Portal",
                                        "@Id_Ban",
                                        "@Id_UMod",
                                        "@Cte_UsoCFDI",
                                        "@Cte_MetodoPago",
										"@Id_Rik",
                                        "@Cte_PagoUsoCFDI",
                                        "@Cte_PagoMetodoPago"
                                      };
                object[] Valores = { 
                                        clientes.Id_Emp,   
                                        clientes.Id_Cd, 
                                        clientes.Id_Cte, 
                                        clientes.Id_Cfe == -1 ? (object)null : clientes.Id_Cfe,
                                        clientes.Id_Corp == -1 ? (object)null : clientes.Id_Corp,
                                        clientes.Cte_NomComercial,
                                        clientes.Cte_NomCorto,
                                        clientes.Cte_FacCalle, 
                                        clientes.Cte_FacNumero, 
                                        clientes.Cte_FacNumeroInterior, 
                                        clientes.Cte_FacCp, 
                                        clientes.Cte_FacColonia, 
                                        clientes.Cte_FacMunicipio, 
                                        clientes.Cte_FacTel, 
                                        clientes.Cte_FacRfc, 
                                        clientes.Cte_FacEstado, 
                                        clientes.Cte_Calle, 
                                        clientes.Cte_Numero, 
                                        clientes.Cte_NumeroInterior, 
                                        clientes.Cte_Cp, 
                                        clientes.Cte_Colonia, 
                                        clientes.Cte_Municipio, 
                                        clientes.Cte_Estado, 
                                        clientes.Cte_Telefono, 
                                        clientes.Cte_Fax,
                                        clientes.Cte_DRfc,
                                        clientes.Cte_Contacto, 
                                        clientes.Cte_Tipo, 
                                        clientes.Cte_Email, 
                                        clientes.Cte_Credito, 
                                        clientes.Cte_Facturacion, 
                                    
                                        clientes.Id_Mon == -1 ? (object)null : clientes.Id_Mon,
                                        clientes.Cte_LimCobr, 
                                        clientes.Cte_RHoraam1, 
                                        clientes.Cte_RHoraam2, 
                                        clientes.Cte_RHorapm1, 
                                        clientes.Cte_RHorapm2, 
                                        clientes.Cte_RLunes, 
                                        clientes.Cte_RMartes, 
                                        clientes.Cte_RMiercoles, 
                                        clientes.Cte_RJueves, 
                                        clientes.Cte_RViernes, 
                                        clientes.Cte_RSabado, 
                                        clientes.Cte_RDomingo, 
                                        clientes.Cte_CondPago, 
                                        clientes.Cte_CPLunes, 
                                        clientes.Cte_CPMartes, 
                                        clientes.Cte_CPMiercoles, 
                                        clientes.Cte_CPJueves, 
                                        clientes.Cte_CPViernes, 
                                        clientes.Cte_CPSabado, 
                                        clientes.Cte_CPDomingo, 
                                        clientes.Cte_Comisiones, 
                                        clientes.Cte_DesgIva, 
                                        clientes.Cte_RetIva, 
                                        clientes.Cte_AsignacionPed == -1 ? (object)null : clientes.Cte_AsignacionPed,
                                        clientes.Id_Ade == -1 ? (object)null : clientes.Id_Ade,
                                        clientes.Cte_SerieNCre == -1 ? (object)null : clientes.Cte_SerieNCre,
                                        clientes.Cte_SerieNCa == -1 ? (object)null : clientes.Cte_SerieNCa,
                                        clientes.Estatus ,

                                        clientes.Cte_CreditoSuspendido ,
                                        clientes.Cte_PHoraam1,
                                        clientes.Cte_PHoraam2,
                                        clientes.Cte_PHorapm1,
                                        clientes.Cte_PHorapm2,
                                        clientes.Cte_SemRec ,
                                        clientes.Cte_RecLunes ,
                                        clientes.Cte_RecMartes ,
                                        clientes.Cte_RecMiercoles ,
                                        clientes.Cte_RecJueves,
                                        clientes.Cte_RecViernes,
                                        clientes.Cte_RecSabado,
                                        clientes.Cte_RecDomingo,
                                        clientes.Cte_Efectivo,
                                        clientes.Cte_Factoraje,
                                        clientes.Cte_Cheque,
                                        clientes.Cte_Transferencia,
                                        clientes.Cte_ReqOrdenCompra,
                                        clientes.Cte_Documentos,
                                        clientes.Cte_TelCobranza1,
                                        clientes.Cte_TelCobranza2,
                                        clientes.Cte_RemisionElectronica,
                                        clientes.BPorcNotaCredito,
                                        clientes.PorcientoNotaCredito,
                                        clientes.PorcientoRetencion,
                                        clientes.BPorcientoIVA,
                                        clientes.PorcientoIVA,
                                        clientes.Cte_UDigitos,
                                        clientes.Cte_Referencia,
                                        clientes.Cte_AutorizaPlazo_IdU,
                                        clientes.Cte_AutorizaPlazo_IdCd,
                                        clientes.Cte_CorreoEdoCuenta1,
                                        clientes.Cte_CorreoEdoCuenta2,
                                        clientes.Cte_CorreoEdoCuenta3,
                                        clientes.Cte_NumCuentaContNacional,
                                        clientes.Cte_SemRev,
                                        clientes.Cte_SemRev2,
                                        clientes.Cte_SemCob,
                                        clientes.Id_TCte,
                                        clientes.Cte_NumeroCuenta,
                                        clientes.Cte_ReferenciaTecleada,
                                        clientes.Cte_Portal,
                                        clientes.Id_Ban,
                                        clientes.Id_UMod,
                                        clientes.Cte_UsoCFDI,
                                        clientes.Cte_MetodoPago,
										clientes.Id_Rik,
                                        clientes.Cte_PagoUsoCFDI,
                                        clientes.Cte_PagoMetodoPago

                                   };

                //SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCliente_Insertar", ref verificador, Parametros, Valores);
                sqlcmd = CD_Datos.GenerarSqlCommand("spCatCliente_Insertar", ref verificador, Parametros, Valores, null, sqlcmd);
                sqlcmd.Dispose();

                //CapaDatos.CommitTrans();
                //CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void InsertarClientes(Clientes clientes, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

            try
            {
                //CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp",
		                                "@Id_Cd", 
                                        "@Id_Cte", 
                                        "@Id_Cfe", 
                                        "@Id_Corp",
		                                "@Cte_NomComercial", 
                                        "@Cte_NomCorto",
		                                "@Cte_FacCalle", 
		                                "@Cte_FacNumero", 
                                        "@Cte_FacNumeroInterior", 
		                                "@Cte_FacCp", 
		                                "@Cte_FacColonia", 
		                                "@Cte_FacMunicipio", 
		                                "@Cte_FacTel", 
		                                "@Cte_FacRfc", 
		                                "@Cte_FacEstado", 
		                                "@Cte_Calle", 
		                                "@Cte_Numero", 
                                        "@Cte_NumeroInterior", 
		                                "@Cte_Cp", 
		                                "@Cte_Colonia", 
		                                "@Cte_Municipio", 
		                                "@Cte_Estado", 
		                                "@Cte_Telefono", 
		                                "@Cte_Fax",
                                        "@Cte_Rfc",
		                                "@Cte_Contacto", 
		                                "@Cte_Tipo", 
		                                "@Cte_Email", 
		                                "@Cte_Credito", 
		                                "@Cte_Facturacion", 
		                                "@Id_Mon", 
		                                "@Cte_LimCobr", 
		                                "@Cte_RHoraam1", 
                                        "@Cte_RHoraam2", 
                                        "@Cte_RHorapm1", 
                                        "@Cte_RHorapm2", 
		                                "@Cte_RLunes", 
		                                "@Cte_RMartes", 
		                                "@Cte_RMiercoles", 
		                                "@Cte_RJueves", 
		                                "@Cte_RViernes", 
		                                "@Cte_RSabado", 
		                                "@Cte_RDomingo", 
		                                "@Cte_CondPago", 
		                                "@Cte_CPLunes", 
		                                "@Cte_CPMartes", 
		                                "@Cte_CPMiercoles", 
		                                "@Cte_CPJueves", 
		                                "@Cte_CPViernes", 
		                                "@Cte_CPSabado", 
		                                "@Cte_CPDomingo", 
		                                "@Cte_Comisiones", 
		                                "@Cte_DesgIva", 
		                                "@Cte_RetIva", 
		                                "@Cte_AsignacionPed", 
		                                "@Id_Ade", 
                                        "@Cte_SerieNcre",
                                        "@Cte_SerieNca",
		                                "@Cte_Activo",
                                        "@Cte_CreditoSuspendido",
                                        "@Cte_PHoraam1",
                                        "@Cte_PHoraam2",
                                        "@Cte_PHorapm1",
                                        "@Cte_PHorapm2",
                                        "@Cte_SemRec",
                                        "@Cte_RecLunes",
                                        "@Cte_RecMartes",
                                        "@Cte_RecMiercoles",
                                        "@Cte_RecJueves",
                                        "@Cte_RecViernes",
                                        "@Cte_RecSabado",
                                        "@Cte_RecDomingo",
                                        "@Cte_Efectivo",
                                        "@Cte_Factoraje",
                                        "@Cte_Cheque",
                                        "@Cte_Transferencia",
                                        "@Cte_ReqOrdenCompra",
                                        "@Cte_Documentos",
                                        "@Cte_TelCobranza1",
                                        "@Cte_TelCobranza2",
                                        "@Cte_RemisionElect",
                                        "@Cte_BPorcNotaCredito",
                                        "@Cte_PorcNotaCredito",
                                        "@Cte_PorcientoRetencion",
                                        "@Cte_BPorcientoIVA",                                       
                                        "@Cte_PorcientoIVA",
                                        "@Cte_UDigitos",
                                        "@Cte_Referencia",
                                        "@Cte_AutorizaPlazo_IdU",
                                        "@Cte_AutorizaPlazo_IdCd",
                                        "@Cte_Correo1",
                                        "@Cte_Correo2",
                                        "@Cte_Correo3",
                                        "@Cte_NumCuentaContNacional",
                                        "@Cte_SemRev",
                                        "@Cte_SemRev2",
                                        "@Cte_SemCob",
                                        "@Id_TCte",
                                        "@Cte_NumeroCuenta",
                                        "@Cte_ReferenciaTecleada",
                                        "@Cte_Portal",
                                        "@Id_Ban",
                                        "@Id_UMod"
                                      };
                object[] Valores = { 
                                        clientes.Id_Emp,   
                                        clientes.Id_Cd, 
                                        clientes.Id_Cte, 
                                        clientes.Id_Cfe == -1 ? (object)null : clientes.Id_Cfe,
                                        clientes.Id_Corp == -1 ? (object)null : clientes.Id_Corp,
                                        clientes.Cte_NomComercial,
                                        clientes.Cte_NomCorto,
                                        clientes.Cte_FacCalle, 
                                        clientes.Cte_FacNumero, 
                                        clientes.Cte_FacNumeroInterior, 
                                        clientes.Cte_FacCp, 
                                        clientes.Cte_FacColonia, 
                                        clientes.Cte_FacMunicipio, 
                                        clientes.Cte_FacTel, 
                                        clientes.Cte_FacRfc, 
                                        clientes.Cte_FacEstado, 
                                        clientes.Cte_Calle, 
                                        clientes.Cte_Numero, 
                                        clientes.Cte_NumeroInterior, 
                                        clientes.Cte_Cp, 
                                        clientes.Cte_Colonia, 
                                        clientes.Cte_Municipio, 
                                        clientes.Cte_Estado, 
                                        clientes.Cte_Telefono, 
                                        clientes.Cte_Fax,
                                        clientes.Cte_DRfc,
                                        clientes.Cte_Contacto, 
                                        clientes.Cte_Tipo, 
                                        clientes.Cte_Email, 
                                        clientes.Cte_Credito, 
                                        clientes.Cte_Facturacion, 
                                    
                                        clientes.Id_Mon == -1 ? (object)null : clientes.Id_Mon,
                                        clientes.Cte_LimCobr, 
                                        clientes.Cte_RHoraam1, 
                                        clientes.Cte_RHoraam2, 
                                        clientes.Cte_RHorapm1, 
                                        clientes.Cte_RHorapm2, 
                                        clientes.Cte_RLunes, 
                                        clientes.Cte_RMartes, 
                                        clientes.Cte_RMiercoles, 
                                        clientes.Cte_RJueves, 
                                        clientes.Cte_RViernes, 
                                        clientes.Cte_RSabado, 
                                        clientes.Cte_RDomingo, 
                                        clientes.Cte_CondPago, 
                                        clientes.Cte_CPLunes, 
                                        clientes.Cte_CPMartes, 
                                        clientes.Cte_CPMiercoles, 
                                        clientes.Cte_CPJueves, 
                                        clientes.Cte_CPViernes, 
                                        clientes.Cte_CPSabado, 
                                        clientes.Cte_CPDomingo, 
                                        clientes.Cte_Comisiones, 
                                        clientes.Cte_DesgIva, 
                                        clientes.Cte_RetIva, 
                                        clientes.Cte_AsignacionPed == -1 ? (object)null : clientes.Cte_AsignacionPed,
                                        clientes.Id_Ade == -1 ? (object)null : clientes.Id_Ade,
                                        clientes.Cte_SerieNCre == -1 ? (object)null : clientes.Cte_SerieNCre,
                                        clientes.Cte_SerieNCa == -1 ? (object)null : clientes.Cte_SerieNCa,
                                        clientes.Estatus ,

                                        clientes.Cte_CreditoSuspendido ,
                                        clientes.Cte_PHoraam1,
                                        clientes.Cte_PHoraam2,
                                        clientes.Cte_PHorapm1,
                                        clientes.Cte_PHorapm2,
                                        clientes.Cte_SemRec ,
                                        clientes.Cte_RecLunes ,
                                        clientes.Cte_RecMartes ,
                                        clientes.Cte_RecMiercoles ,
                                        clientes.Cte_RecJueves,
                                        clientes.Cte_RecViernes,
                                        clientes.Cte_RecSabado,
                                        clientes.Cte_RecDomingo,
                                        clientes.Cte_Efectivo,
                                        clientes.Cte_Factoraje,
                                        clientes.Cte_Cheque,
                                        clientes.Cte_Transferencia,
                                        clientes.Cte_ReqOrdenCompra,
                                        clientes.Cte_Documentos,
                                        clientes.Cte_TelCobranza1,
                                        clientes.Cte_TelCobranza2,
                                        clientes.Cte_RemisionElectronica,
                                        clientes.BPorcNotaCredito,
                                        clientes.PorcientoNotaCredito,
                                        clientes.PorcientoRetencion,
                                        clientes.BPorcientoIVA,
                                        clientes.PorcientoIVA,
                                        clientes.Cte_UDigitos,
                                        clientes.Cte_Referencia,
                                        clientes.Cte_AutorizaPlazo_IdU,
                                        clientes.Cte_AutorizaPlazo_IdCd,
                                        clientes.Cte_CorreoEdoCuenta1,
                                        clientes.Cte_CorreoEdoCuenta2,
                                        clientes.Cte_CorreoEdoCuenta3,
                                        clientes.Cte_NumCuentaContNacional,
                                        clientes.Cte_SemRev,
                                        clientes.Cte_SemRev2,
                                        clientes.Cte_SemCob,
                                        clientes.Id_TCte,
                                        clientes.Cte_NumeroCuenta,
                                        clientes.Cte_ReferenciaTecleada,
                                        clientes.Cte_Portal,
                                        clientes.Id_Ban,
                                        clientes.Id_UMod
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCliente_Insertar", ref verificador, Parametros, Valores);

                //CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ModificarClientes(Clientes clientes, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros = { 
	                                    "@Id_Emp",
		                                "@Id_Cd", 
		                                "@Id_Cte", 
                                        "@Id_Cfe", 
                                        "@Id_Corp",
		                                "@Cte_NomComercial", 
                                        "@Cte_NomCorto", 
		                                "@Cte_FacCalle", 
		                                "@Cte_FacNumero", 
                                        "@Cte_FacNumeroInterior", 
		                                "@Cte_FacCp", 
		                                "@Cte_FacColonia", 
		                                "@Cte_FacMunicipio", 
		                                "@Cte_FacTel", 
		                                "@Cte_FacRfc", 
		                                "@Cte_FacEstado", 
		                                "@Cte_Calle", 
		                                "@Cte_Numero", 
                                        "@Cte_NumeroInterior", 
		                                "@Cte_Cp", 
		                                "@Cte_Colonia", 
		                                "@Cte_Municipio", 
		                                "@Cte_Estado", 
		                                "@Cte_Telefono", 
		                                "@Cte_Fax",
                                        "@Cte_Rfc",
		                                "@Cte_Contacto", 
		                                "@Cte_Tipo", 
		                                "@Cte_Email", 
		                                "@Cte_Credito", 
		                                "@Cte_Facturacion", 
		                                "@Id_Mon", 
		                                "@Cte_LimCobr", 
		                                "@Cte_RHoraam1", 
                                        "@Cte_RHoraam2", 
                                        "@Cte_RHorapm1", 
                                        "@Cte_RHorapm2", 
		                                "@Cte_RLunes", 
		                                "@Cte_RMartes", 
		                                "@Cte_RMiercoles", 
		                                "@Cte_RJueves", 
		                                "@Cte_RViernes", 
		                                "@Cte_RSabado", 
		                                "@Cte_RDomingo", 
		                                "@Cte_CondPago", 
		                                "@Cte_CPLunes", 
		                                "@Cte_CPMartes", 
		                                "@Cte_CPMiercoles", 
		                                "@Cte_CPJueves", 
		                                "@Cte_CPViernes", 
		                                "@Cte_CPSabado", 
		                                "@Cte_CPDomingo", 
		                                "@Cte_Comisiones", 
		                                "@Cte_DesgIva", 
		                                "@Cte_RetIva", 
		                                "@Cte_AsignacionPed", 
		                                "@Id_Ade", 
                                        "@Cte_SerieNcre",
                                        "@Cte_SerieNca",
		                                "@Cte_Activo",
                                        "@Cte_CreditoSuspendido",
                                        "@Cte_PHoraam1",
                                        "@Cte_PHoraam2",
                                        "@Cte_PHorapm1",
                                        "@Cte_PHorapm2",
                                        "@Cte_SemRec",
                                        "@Cte_RecLunes",
                                        "@Cte_RecMartes",
                                        "@Cte_RecMiercoles",
                                        "@Cte_RecJueves",
                                        "@Cte_RecViernes",
                                        "@Cte_RecSabado",
                                        "@Cte_RecDomingo",
                                        "@Cte_Efectivo",
                                        "@Cte_Factoraje",
                                        "@Cte_Cheque",
                                        "@Cte_Transferencia",
                                        "@Cte_ReqOrdenCompra",
                                        "@Cte_Documentos",
                                        "@Cte_TelCobranza1",
                                        "@Cte_TelCobranza2",
                                        "@Cte_RemisionElect",
                                        "@Cte_BPorcNotaCredito",
                                        "@Cte_PorcNotaCredito",
                                        "@Cte_PorcientoRetencion",
                                        "@Cte_BPorcientoIVA",                                       
                                        "@Cte_PorcientoIVA",
                                        "@Cte_UDigitos",
                                        "@Cte_Referencia",
                                        "@Id_U",
                                        "@Id_UCd",
                                        "@Db",
                                        "@Db_Cobranza",
                                        "@Cte_AutorizaPlazo_IdU",
                                        "@Cte_AutorizaPlazo_IdCd",
                                        "@Cte_Correo1",
                                        "@Cte_Correo2",
                                        "@Cte_Correo3",
                                        "@Cte_NumCuentaContNacional",
                                        "@Cte_SemRev",
                                        "@Cte_SemRev2",
                                        "@Cte_SemCob",
                                        "@Id_TCte",
                                        "@Cte_NumeroCuenta",
                                        "@Cte_ReferenciaTecleada",
                                        "@Cte_Portal",
                                        "@Id_Ban",
                                        "@Id_UMod",
                                         "@Cte_UsoCFDI" ,
                                         "@Cte_MetodoPago",
                                        "@Cte_PagoUsoCFDI",
                                        "@Cte_PagoMetodoPago"
                                      };
                object[] Valores = { 
                                        clientes.Id_Emp,   
                                        clientes.Id_Cd, 
                                        clientes.Id_Cte, 
                                        clientes.Id_Cfe == -1 ? (object)null : clientes.Id_Cfe,                                      
                                        clientes.Id_Corp == -1 ? (object)null : clientes.Id_Corp,
                                        clientes.Cte_NomComercial, 
                                        clientes.Cte_NomCorto, 
                                        clientes.Cte_FacCalle, 
                                        clientes.Cte_FacNumero, 
                                        clientes.Cte_FacNumeroInterior, 
                                        clientes.Cte_FacCp, 
                                        clientes.Cte_FacColonia, 
                                        clientes.Cte_FacMunicipio, 
                                        clientes.Cte_FacTel, 
                                        clientes.Cte_FacRfc, 
                                        clientes.Cte_FacEstado, 
                                        clientes.Cte_Calle, 
                                        clientes.Cte_Numero, 
                                        clientes.Cte_NumeroInterior,
                                        clientes.Cte_Cp, 
                                        clientes.Cte_Colonia, 
                                        clientes.Cte_Municipio, 
                                        clientes.Cte_Estado, 
                                        clientes.Cte_Telefono, 
                                        clientes.Cte_Fax,
                                        clientes.Cte_DRfc,
                                        clientes.Cte_Contacto, 
                                        clientes.Cte_Tipo, 
                                        clientes.Cte_Email, 
                                        clientes.Cte_Credito, 
                                        clientes.Cte_Facturacion, 
                                        clientes.Id_Mon == -1 ? (object)null : clientes.Id_Mon,
                                        clientes.Cte_LimCobr, 
                                        clientes.Cte_RHoraam1, 
                                        clientes.Cte_RHoraam2,
                                        clientes.Cte_RHorapm1,
                                        clientes.Cte_RHorapm2,
                                        clientes.Cte_RLunes, 
                                        clientes.Cte_RMartes, 
                                        clientes.Cte_RMiercoles, 
                                        clientes.Cte_RJueves, 
                                        clientes.Cte_RViernes, 
                                        clientes.Cte_RSabado, 
                                        clientes.Cte_RDomingo, 
                                        clientes.Cte_CondPago, 
                                        clientes.Cte_CPLunes, 
                                        clientes.Cte_CPMartes, 
                                        clientes.Cte_CPMiercoles, 
                                        clientes.Cte_CPJueves, 
                                        clientes.Cte_CPViernes, 
                                        clientes.Cte_CPSabado, 
                                        clientes.Cte_CPDomingo, 
                                        clientes.Cte_Comisiones, 
                                        clientes.Cte_DesgIva, 
                                        clientes.Cte_RetIva, 
                                        clientes.Cte_AsignacionPed == -1 ? (object)null : clientes.Cte_AsignacionPed,
                                        clientes.Id_Ade == -1 ? (object)null : clientes.Id_Ade,
                                        clientes.Cte_SerieNCre == -1 ? (object)null : clientes.Cte_SerieNCre,
                                        clientes.Cte_SerieNCa == -1 ? (object)null : clientes.Cte_SerieNCa,
                                        clientes.Estatus,
                                        clientes.Cte_CreditoSuspendido ,
                                        clientes.Cte_PHoraam1 ,
                                        clientes.Cte_PHoraam2 ,
                                        clientes.Cte_PHorapm1 ,
                                        clientes.Cte_PHorapm2 ,
                                        clientes.Cte_SemRec ,
                                        clientes.Cte_RecLunes ,
                                        clientes.Cte_RecMartes ,
                                        clientes.Cte_RecMiercoles ,
                                        clientes.Cte_RecJueves,
                                        clientes.Cte_RecViernes,
                                        clientes.Cte_RecSabado,
                                        clientes.Cte_RecDomingo,
                                        clientes.Cte_Efectivo,
                                        clientes.Cte_Factoraje,
                                        clientes.Cte_Cheque,
                                        clientes.Cte_Transferencia,
                                        clientes.Cte_ReqOrdenCompra,
                                        clientes.Cte_Documentos,
                                        clientes.Cte_TelCobranza1,
                                        clientes.Cte_TelCobranza2,
                                        clientes.Cte_RemisionElectronica,
                                        clientes.BPorcNotaCredito,
                                        clientes.PorcientoNotaCredito,
                                        clientes.PorcientoRetencion,
                                        clientes.BPorcientoIVA,
                                        clientes.PorcientoIVA,
                                        clientes.Cte_UDigitos,
                                        clientes.Cte_Referencia,
                                        clientes.Id_U,
                                        clientes.Id_UCd,
                                        clientes.Db,
                                        clientes.Db_Cobranza,
                                        clientes.Cte_AutorizaPlazo_IdU,
                                        clientes.Cte_AutorizaPlazo_IdCd,
                                        clientes.Cte_CorreoEdoCuenta1,
                                        clientes.Cte_CorreoEdoCuenta2,
                                        clientes.Cte_CorreoEdoCuenta3,
                                        clientes.Cte_NumCuentaContNacional,
                                        clientes.Cte_SemRev,
                                        clientes.Cte_SemRev2,
                                        clientes.Cte_SemCob,
                                        clientes.Id_TCte,
                                        clientes.Cte_NumeroCuenta,
                                        clientes.Cte_ReferenciaTecleada,
                                        clientes.Cte_Portal,
                                        clientes.Id_Ban,
                                        clientes.Id_UMod,
                                        clientes.Cte_UsoCFDI,
                                        clientes.Cte_MetodoPago,
                                        clientes.Cte_PagoUsoCFDI,
                                        clientes.Cte_PagoMetodoPago

                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCliente_Modificar", ref verificador, Parametros, Valores);

                int verificador2 = 0;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Fpa", "@Contador" };
                int contador = 0;
                foreach (FormaPago dr in clientes.FormasPago)
                {
                    Valores = new object[] { clientes.Id_Emp, clientes.Id_Cd, clientes.Id_Cte, dr.Id_Fpa, contador };
                    sqlcmd = CapaDatos.GenerarSqlCommand("CatClienteFPago_Insertar", ref verificador2, Parametros, Valores);
                    contador = 1;
                }


                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ConsultarCliente(Clientes cte, string Conexion)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Rik", "@Id_Ter", "@Ignora_Activo" };
                object[] Valores = { 
                                       cte.Id_Emp, 
                                       cte.Id_Cd, 
                                       cte.Id_Cte, 
                                       cte.Id_Rik <= 0 ? (object)null : cte.Id_Rik, 
                                       cte.Id_Terr <= 0 ? (object)null : cte.Id_Terr,
                                       cte.Ignora_Inactivo
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCliente_Consulta", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    cte.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    cte.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    cte.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    cte.Id_Rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rik"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    cte.Id_Cfe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cfe"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cfe")));
                    cte.Id_Corp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Corp"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Corp")));
                    cte.FacSerie = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FacSerie"))) ? "" : dr.GetValue(dr.GetOrdinal("FacSerie")).ToString();
                    cte.Cte_NomComercial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_NomComercial"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    cte.Cte_NomCorto = dr.IsDBNull(dr.GetOrdinal("Cte_NomCorto")) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_NomCorto"));
                    cte.Cte_FacCalle = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacCalle"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacCalle"));
                    cte.Cte_FacNumero = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacNumero"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacNumero"));
                    cte.Cte_FacNumeroInterior = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacNumeroInterior"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacNumeroInterior"));
                    cte.Cte_FacCp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacCp"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacCp"));
                    cte.Cte_FacColonia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacColonia"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacColonia"));
                    cte.Cte_FacMunicipio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacMunicipio"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacMunicipio"));
                    cte.Cte_FacTel = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacTel"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacTel"));
                    cte.Cte_FacRfc = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacRfc"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacRfc"));
                    cte.Cte_FacEstado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacEstado"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacEstado"));
                    cte.Cte_Calle = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Calle"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Calle"));
                    cte.Cte_Numero = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Numero"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Numero"));
                    cte.Cte_NumeroInterior = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_NumeroInterior"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_NumeroInterior"));
                    string cp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Cp"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Cp"));
                    cte.Cte_Cp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Cp"))) ? "" : dr.GetValue(dr.GetOrdinal("Cte_Cp")).ToString();
                    cte.Cte_Colonia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Colonia"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Colonia"));
                    cte.Cte_Municipio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Municipio"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Municipio"));
                    cte.Cte_Estado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Estado"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Estado"));
                    cte.Cte_DRfc = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Rfc"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Rfc"));
                    cte.Cte_Telefono = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Telefono"))) ? "" : dr.GetValue(dr.GetOrdinal("Cte_Telefono")).ToString();
                    cte.Cte_Fax = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Fax"))) ? "" : dr.GetValue(dr.GetOrdinal("Cte_Fax")).ToString();
                    cte.Cte_Contacto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Contacto"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Contacto"));
                    cte.Cte_Tipo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Tipo"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_Tipo")));
                    cte.Cte_Email = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Email"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Email"));
                    cte.Cte_Credito = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Credito"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Credito"));
                    cte.Cte_Facturacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Facturacion"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Facturacion"));
                    cte.Id_Mon = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Mon"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Mon")));
                    cte.Cte_LimCobr = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_LimCobr"))) ? 0 : (double)dr.GetValue(dr.GetOrdinal("Cte_LimCobr"));
                    cte.Cte_RHoraam1 = dr.IsDBNull(dr.GetOrdinal("Cte_RHoraam1")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_RHoraam1"));
                    cte.Cte_RHoraam2 = dr.IsDBNull(dr.GetOrdinal("Cte_RHoraam2")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_RHoraam2"));
                    cte.Cte_RHorapm1 = dr.IsDBNull(dr.GetOrdinal("Cte_RHorapm1")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_RHorapm1"));
                    cte.Cte_RHorapm2 = dr.IsDBNull(dr.GetOrdinal("Cte_RHorapm2")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_RHorapm2"));
                    cte.Cte_RLunes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RLunes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RLunes"));
                    cte.Cte_RMartes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RMartes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RMartes"));
                    cte.Cte_RMiercoles = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RMiercoles"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RMiercoles"));
                    cte.Cte_RJueves = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RJueves"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RJueves"));
                    cte.Cte_RViernes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RViernes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RViernes"));
                    cte.Cte_RSabado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RSabado"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RSabado"));
                    cte.Cte_RDomingo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RDomingo"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RDomingo"));
                    cte.Cte_CondPago = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CondPago"))) ? 0 : (int)dr.GetValue(dr.GetOrdinal("Cte_CondPago"));
                    cte.Cte_CPLunes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CPLunes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CPLunes"));
                    cte.Cte_CPMartes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CPMartes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CPMartes"));
                    cte.Cte_CPMiercoles = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CPMiercoles"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CPMiercoles"));
                    cte.Cte_CPJueves = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CPJueves"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CPJueves"));
                    cte.Cte_CPViernes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CPViernes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CPViernes"));
                    cte.Cte_CPSabado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CPSabado"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CPSabado"));
                    cte.Cte_CPDomingo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CPDomingo"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CPDomingo"));
                    cte.Cte_Comisiones = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Comisiones"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Comisiones"));
                    cte.Cte_DesgIva = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_DesgIva"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_DesgIva"));
                    cte.Cte_RetIva = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RetIva"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RetIva"));
                    cte.Cte_AsignacionPed = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_AsignacionPed"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_AsignacionPed")));
                    cte.Id_Ade = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ade"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ade")));
                    cte.Cte_SerieNCre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_SerieNCre"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_SerieNCre")));
                    cte.Cte_SerieNCa = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_SerieNCa"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_SerieNCa")));
                    cte.Estatus = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Activo"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Activo"));
                    cte.Cte_EsSucursal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_EsSucursal"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_EsSucursal"));

                    cte.Cte_CreditoSuspendido = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido"));
                    cte.Cte_PHoraam1 = dr.IsDBNull(dr.GetOrdinal("Cte_PHoraam1")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_PHoraam1"));
                    cte.Cte_PHoraam2 = dr.IsDBNull(dr.GetOrdinal("Cte_PHoraam2")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_PHoraam2"));
                    cte.Cte_PHorapm1 = dr.IsDBNull(dr.GetOrdinal("Cte_PHorapm1")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_PHorapm1"));
                    cte.Cte_PHorapm2 = dr.IsDBNull(dr.GetOrdinal("Cte_PHorapm2")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_PHorapm2"));
                    cte.Cte_SemRec = dr.IsDBNull(dr.GetOrdinal("Cte_SemRec")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_SemRec")));
                    cte.Cte_SemRev = dr.IsDBNull(dr.GetOrdinal("Cte_SemRev")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_SemRev")));
                    cte.Cte_SemRev2 = Convert.ToInt32(dr["Cte_SemRev2"]);
                    cte.Cte_SemCob = dr.IsDBNull(dr.GetOrdinal("Cte_SemCob")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_SemCob")));
                    cte.Cte_RecLunes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RecLunes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RecLunes"));
                    cte.Cte_RecMartes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RecMartes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RecMartes"));
                    cte.Cte_RecMiercoles = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RecMiercoles"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RecMiercoles"));
                    cte.Cte_RecJueves = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RecJueves"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RecJueves"));
                    cte.Cte_RecViernes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RecViernes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RecViernes"));
                    cte.Cte_RecSabado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RecSabado"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RecSabado"));
                    cte.Cte_RecDomingo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RecDomingo"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RecDomingo"));
                    cte.Cte_Efectivo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Efectivo"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Efectivo"));
                    cte.Cte_Factoraje = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Factoraje"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Factoraje"));
                    cte.Cte_Cheque = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Cheque"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Cheque"));
                    cte.Cte_Transferencia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Transferencia"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Transferencia"));
                    cte.Cte_ReqOrdenCompra = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_ReqOrdenCompra"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_ReqOrdenCompra"));
                    cte.Cte_Documentos = dr.IsDBNull(dr.GetOrdinal("Cte_Documentos")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_Documentos")).ToString();
                    cte.Ade_Nombre = dr.IsDBNull(dr.GetOrdinal("Ade_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("Ade_Nombre")).ToString();
                    cte.Cte_TelCobranza1 = dr.IsDBNull(dr.GetOrdinal("Cte_TelCobranza1")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_TelCobranza1")).ToString();
                    cte.Cte_TelCobranza2 = dr.IsDBNull(dr.GetOrdinal("Cte_TelCobranza2")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_TelCobranza2")).ToString();
                    cte.Cte_RemisionElectronica = dr.IsDBNull(dr.GetOrdinal("Cte_RemisionElectronica")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_RemisionElectronica")));
                    cte.Cte_NumCuentaContNacional = dr.IsDBNull(dr.GetOrdinal("Cte_NumCuentaContNacional")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_NumCuentaContNacional")));
                    cte.BPorcNotaCredito = dr.IsDBNull(dr.GetOrdinal("Cte_NCredito")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_NCredito")));
                    cte.PorcientoNotaCredito = dr.IsDBNull(dr.GetOrdinal("Cte_PorcNCredito")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cte_PorcNCredito")));
                    cte.PorcientoRetencion = dr.IsDBNull(dr.GetOrdinal("Cte_PorcRetencion")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cte_PorcRetencion")));
                    cte.BPorcientoIVA = dr.IsDBNull(dr.GetOrdinal("Cte_BPorcientoIVA")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_BPorcientoIVA")));
                    cte.PorcientoIVA = dr.IsDBNull(dr.GetOrdinal("Cte_PorcientoIVA")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_PorcientoIVA")));
                    cte.Cte_UDigitos = dr.IsDBNull(dr.GetOrdinal("Cte_UDigitos")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_UDigitos")).ToString();
                    cte.Cte_Referencia = dr.IsDBNull(dr.GetOrdinal("Cte_Referencia")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_Referencia")).ToString();


                    cte.Cte_MetodoPago = dr.IsDBNull(dr.GetOrdinal("Cte_MetodoPago")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_MetodoPago")).ToString();

                    cte.Cte_PagoUsoCFDI = dr.IsDBNull(dr.GetOrdinal("Cte_PagoUsoCFDI")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_PagoUsoCFDI")).ToString();
                    cte.Cte_PagoMetodoPago = dr.IsDBNull(dr.GetOrdinal("Cte_PagoMetodoPago")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_PagoMetodoPago")).ToString();
                    cte.Cte_UsoCFDI = dr.IsDBNull(dr.GetOrdinal("Cte_UsoCFDI")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_UsoCFDI")).ToString();



                    //TODO: QUITAR COMENTARIOS
                    try { cte.Cte_DiasVencidos = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DiasVencidos"))); }
                    catch { }

                    try { cte.Cte_MotCreditoSuspendido = dr.GetValue(dr.GetOrdinal("Cte_MotCreditoSuspendido")).ToString(); }
                    catch { }

                    try { cte.Cte_Referencia = dr.IsDBNull(dr.GetOrdinal("Cte_Referencia")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_Referencia")).ToString(); }
                    catch { }

                    try { cte.UPlazo = dr.IsDBNull(dr.GetOrdinal("UPlazo")) ? "" : dr.GetValue(dr.GetOrdinal("UPlazo")).ToString(); }
                    catch { }

                    if (Convert.ToBoolean(cte.Estatus))
                        cte.EstatusStr = "Activo";
                    else
                        cte.EstatusStr = "Inactivo";

                    cte.Cte_CorreoEdoCuenta1 = dr.GetValue(dr.GetOrdinal("Cte_Correo1")).ToString();
                    cte.Cte_CorreoEdoCuenta2 = dr.GetValue(dr.GetOrdinal("Cte_Correo2")).ToString();
                    cte.Cte_CorreoEdoCuenta3 = dr.GetValue(dr.GetOrdinal("Cte_Correo3")).ToString();

                    cte.Id_Ban = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ban"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ban")));
                    cte.Id_TCte = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_TCte"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_TCte")));
                    cte.Cte_NumeroCuenta = dr.GetValue(dr.GetOrdinal("Cte_NumeroCuenta")).ToString();
                    cte.Cte_Portal = dr.GetValue(dr.GetOrdinal("Cte_Portal")).ToString();
                    cte.Cte_ReferenciaTecleada = dr.GetValue(dr.GetOrdinal("Cte_ReferenciaTecleada")).ToString();
                    cte.U_Nombre = dr["U_Nombre"].ToString();
                    cte.Cte_Modfecha = Convert.ToDateTime(dr["Cte_ModFecha"]);

                    cte.ClienteSIAN = dr.GetValue(dr.GetOrdinal("ClienteSIAN")).ToString();

                }



                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarClienteOtraBD(Clientes cte, string serie, string Conexion)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Serie" };
                object[] Valores = { 
                                       cte.Id_Emp, 
                                       cte.Id_Cd, 
                                       cte.Id_Cte, 
                                       serie
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCliente_ConsultaOtraBD", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    cte.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    cte.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    cte.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    cte.Id_Rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rik"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    cte.Id_Cfe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cfe"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cfe")));
                    cte.Id_Corp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Corp"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Corp")));
                    cte.FacSerie = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("FacSerie"))) ? "" : dr.GetValue(dr.GetOrdinal("FacSerie")).ToString();
                    cte.Cte_NomComercial = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_NomComercial"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    cte.Cte_NomCorto = dr.IsDBNull(dr.GetOrdinal("Cte_NomCorto")) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_NomCorto"));
                    cte.Cte_FacCalle = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacCalle"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacCalle"));
                    cte.Cte_FacNumero = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacNumero"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacNumero"));
                    cte.Cte_FacNumeroInterior = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacNumeroInterior"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacNumeroInterior"));
                    cte.Cte_FacCp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacCp"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacCp"));
                    cte.Cte_FacColonia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacColonia"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacColonia"));
                    cte.Cte_FacMunicipio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacMunicipio"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacMunicipio"));
                    cte.Cte_FacTel = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacTel"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacTel"));
                    cte.Cte_FacRfc = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacRfc"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacRfc"));
                    cte.Cte_FacEstado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_FacEstado"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_FacEstado"));
                    cte.Cte_Calle = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Calle"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Calle"));
                    cte.Cte_Numero = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Numero"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Numero"));
                    cte.Cte_NumeroInterior = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_NumeroInterior"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_NumeroInterior"));
                    string cp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Cp"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Cp"));
                    cte.Cte_Cp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Cp"))) ? "" : dr.GetValue(dr.GetOrdinal("Cte_Cp")).ToString();
                    cte.Cte_Colonia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Colonia"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Colonia"));
                    cte.Cte_Municipio = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Municipio"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Municipio"));
                    cte.Cte_Estado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Estado"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Estado"));
                    cte.Cte_DRfc = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Rfc"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Rfc"));
                    cte.Cte_Telefono = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Telefono"))) ? "" : dr.GetValue(dr.GetOrdinal("Cte_Telefono")).ToString();
                    cte.Cte_Fax = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Fax"))) ? "" : dr.GetValue(dr.GetOrdinal("Cte_Fax")).ToString();
                    cte.Cte_Contacto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Contacto"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Contacto"));
                    cte.Cte_Tipo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Tipo"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_Tipo")));
                    cte.Cte_Email = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Email"))) ? "" : (string)dr.GetValue(dr.GetOrdinal("Cte_Email"));
                    cte.Cte_Credito = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Credito"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Credito"));
                    cte.Cte_Facturacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Facturacion"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Facturacion"));
                    cte.Id_Mon = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Mon"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Mon")));
                    cte.Cte_LimCobr = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_LimCobr"))) ? 0 : (double)dr.GetValue(dr.GetOrdinal("Cte_LimCobr"));
                    cte.Cte_RHoraam1 = dr.IsDBNull(dr.GetOrdinal("Cte_RHoraam1")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_RHoraam1"));
                    cte.Cte_RHoraam2 = dr.IsDBNull(dr.GetOrdinal("Cte_RHoraam2")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_RHoraam2"));
                    cte.Cte_RHorapm1 = dr.IsDBNull(dr.GetOrdinal("Cte_RHorapm1")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_RHorapm1"));
                    cte.Cte_RHorapm2 = dr.IsDBNull(dr.GetOrdinal("Cte_RHorapm2")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_RHorapm2"));
                    cte.Cte_RLunes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RLunes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RLunes"));
                    cte.Cte_RMartes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RMartes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RMartes"));
                    cte.Cte_RMiercoles = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RMiercoles"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RMiercoles"));
                    cte.Cte_RJueves = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RJueves"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RJueves"));
                    cte.Cte_RViernes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RViernes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RViernes"));
                    cte.Cte_RSabado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RSabado"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RSabado"));
                    cte.Cte_RDomingo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RDomingo"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RDomingo"));
                    cte.Cte_CondPago = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CondPago"))) ? 0 : (int)dr.GetValue(dr.GetOrdinal("Cte_CondPago"));
                    cte.Cte_CPLunes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CPLunes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CPLunes"));
                    cte.Cte_CPMartes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CPMartes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CPMartes"));
                    cte.Cte_CPMiercoles = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CPMiercoles"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CPMiercoles"));
                    cte.Cte_CPJueves = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CPJueves"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CPJueves"));
                    cte.Cte_CPViernes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CPViernes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CPViernes"));
                    cte.Cte_CPSabado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CPSabado"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CPSabado"));
                    cte.Cte_CPDomingo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CPDomingo"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CPDomingo"));
                    cte.Cte_Comisiones = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Comisiones"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Comisiones"));
                    cte.Cte_DesgIva = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_DesgIva"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_DesgIva"));
                    cte.Cte_RetIva = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RetIva"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RetIva"));
                    cte.Cte_AsignacionPed = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_AsignacionPed"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_AsignacionPed")));
                    cte.Id_Ade = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ade"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ade")));
                    cte.Cte_SerieNCre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_SerieNCre"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_SerieNCre")));
                    cte.Cte_SerieNCa = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_SerieNCa"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_SerieNCa")));
                    cte.Estatus = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Activo"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Activo"));
                    cte.Cte_EsSucursal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_EsSucursal"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_EsSucursal"));

                    cte.Cte_CreditoSuspendido = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido"));
                    cte.Cte_PHoraam1 = dr.IsDBNull(dr.GetOrdinal("Cte_PHoraam1")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_PHoraam1"));
                    cte.Cte_PHoraam2 = dr.IsDBNull(dr.GetOrdinal("Cte_PHoraam2")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_PHoraam2"));
                    cte.Cte_PHorapm1 = dr.IsDBNull(dr.GetOrdinal("Cte_PHorapm1")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_PHorapm1"));
                    cte.Cte_PHorapm2 = dr.IsDBNull(dr.GetOrdinal("Cte_PHorapm2")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_PHorapm2"));
                    cte.Cte_SemRec = dr.IsDBNull(dr.GetOrdinal("Cte_SemRec")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_SemRec")));
                    cte.Cte_SemRev = dr.IsDBNull(dr.GetOrdinal("Cte_SemRev")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_SemRev")));
                    cte.Cte_SemRev2 = Convert.ToInt32(dr["Cte_SemRev2"]);
                    cte.Cte_SemCob = dr.IsDBNull(dr.GetOrdinal("Cte_SemCob")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_SemCob")));
                    cte.Cte_RecLunes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RecLunes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RecLunes"));
                    cte.Cte_RecMartes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RecMartes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RecMartes"));
                    cte.Cte_RecMiercoles = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RecMiercoles"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RecMiercoles"));
                    cte.Cte_RecJueves = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RecJueves"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RecJueves"));
                    cte.Cte_RecViernes = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RecViernes"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RecViernes"));
                    cte.Cte_RecSabado = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RecSabado"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RecSabado"));
                    cte.Cte_RecDomingo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_RecDomingo"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_RecDomingo"));
                    cte.Cte_Efectivo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Efectivo"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Efectivo"));
                    cte.Cte_Factoraje = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Factoraje"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Factoraje"));
                    cte.Cte_Cheque = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Cheque"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Cheque"));
                    cte.Cte_Transferencia = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_Transferencia"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_Transferencia"));
                    cte.Cte_ReqOrdenCompra = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_ReqOrdenCompra"))) ? false : (bool)dr.GetValue(dr.GetOrdinal("Cte_ReqOrdenCompra"));
                    cte.Cte_Documentos = dr.IsDBNull(dr.GetOrdinal("Cte_Documentos")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_Documentos")).ToString();
                    cte.Ade_Nombre = dr.IsDBNull(dr.GetOrdinal("Ade_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("Ade_Nombre")).ToString();
                    cte.Cte_TelCobranza1 = dr.IsDBNull(dr.GetOrdinal("Cte_TelCobranza1")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_TelCobranza1")).ToString();
                    cte.Cte_TelCobranza2 = dr.IsDBNull(dr.GetOrdinal("Cte_TelCobranza2")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_TelCobranza2")).ToString();
                    cte.Cte_RemisionElectronica = dr.IsDBNull(dr.GetOrdinal("Cte_RemisionElectronica")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_RemisionElectronica")));
                    cte.Cte_NumCuentaContNacional = dr.IsDBNull(dr.GetOrdinal("Cte_NumCuentaContNacional")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_NumCuentaContNacional")));
                    cte.BPorcNotaCredito = dr.IsDBNull(dr.GetOrdinal("Cte_NCredito")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_NCredito")));
                    cte.PorcientoNotaCredito = dr.IsDBNull(dr.GetOrdinal("Cte_PorcNCredito")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cte_PorcNCredito")));
                    cte.PorcientoRetencion = dr.IsDBNull(dr.GetOrdinal("Cte_PorcRetencion")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cte_PorcRetencion")));
                    cte.BPorcientoIVA = dr.IsDBNull(dr.GetOrdinal("Cte_BPorcientoIVA")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_BPorcientoIVA")));
                    cte.PorcientoIVA = dr.IsDBNull(dr.GetOrdinal("Cte_PorcientoIVA")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_PorcientoIVA")));
                    cte.Cte_UDigitos = dr.IsDBNull(dr.GetOrdinal("Cte_UDigitos")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_UDigitos")).ToString();
                    cte.Cte_Referencia = dr.IsDBNull(dr.GetOrdinal("Cte_Referencia")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_Referencia")).ToString();


                    cte.Cte_MetodoPago = dr.IsDBNull(dr.GetOrdinal("Cte_MetodoPago")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_MetodoPago")).ToString();

                    cte.Cte_PagoUsoCFDI = dr.IsDBNull(dr.GetOrdinal("Cte_PagoUsoCFDI")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_PagoUsoCFDI")).ToString();
                    cte.Cte_PagoMetodoPago = dr.IsDBNull(dr.GetOrdinal("Cte_PagoMetodoPago")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_PagoMetodoPago")).ToString();
                    cte.Cte_UsoCFDI = dr.IsDBNull(dr.GetOrdinal("Cte_UsoCFDI")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_UsoCFDI")).ToString();



                    //TODO: QUITAR COMENTARIOS
                    try { cte.Cte_DiasVencidos = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DiasVencidos"))); }
                    catch { }

                    try { cte.Cte_MotCreditoSuspendido = dr.GetValue(dr.GetOrdinal("Cte_MotCreditoSuspendido")).ToString(); }
                    catch { }

                    try { cte.Cte_Referencia = dr.IsDBNull(dr.GetOrdinal("Cte_Referencia")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_Referencia")).ToString(); }
                    catch { }

                    try { cte.UPlazo = dr.IsDBNull(dr.GetOrdinal("UPlazo")) ? "" : dr.GetValue(dr.GetOrdinal("UPlazo")).ToString(); }
                    catch { }

                    if (Convert.ToBoolean(cte.Estatus))
                        cte.EstatusStr = "Activo";
                    else
                        cte.EstatusStr = "Inactivo";

                    cte.Cte_CorreoEdoCuenta1 = dr.GetValue(dr.GetOrdinal("Cte_Correo1")).ToString();
                    cte.Cte_CorreoEdoCuenta2 = dr.GetValue(dr.GetOrdinal("Cte_Correo2")).ToString();
                    cte.Cte_CorreoEdoCuenta3 = dr.GetValue(dr.GetOrdinal("Cte_Correo3")).ToString();

                    cte.Id_Ban = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ban"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ban")));
                    cte.Id_TCte = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_TCte"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_TCte")));
                    cte.Cte_NumeroCuenta = dr.GetValue(dr.GetOrdinal("Cte_NumeroCuenta")).ToString();
                    cte.Cte_Portal = dr.GetValue(dr.GetOrdinal("Cte_Portal")).ToString();
                    cte.Cte_ReferenciaTecleada = dr.GetValue(dr.GetOrdinal("Cte_ReferenciaTecleada")).ToString();
                    cte.U_Nombre = dr["U_Nombre"].ToString();
                    cte.Cte_Modfecha = Convert.ToDateTime(dr["Cte_ModFecha"]);

                    cte.ClienteSIAN = dr.GetValue(dr.GetOrdinal("ClienteSIAN")).ToString();

                }



                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarClienteFormaPago(ref Clientes cte, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Cte" };
                object[] Valores = new object[] { cte.Id_Emp, cte.Id_Cd, cte.Id_Cte };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("CatClienteFPago_Consultar", ref dr, Parametros, Valores);
                List<FormaPago> listFP = new List<FormaPago>();
                FormaPago FP;
                while (dr.Read())
                {
                    FP = new FormaPago();
                    FP.Id_Fpa = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fpa")));
                    listFP.Add(FP);
                }
                cte.FormasPago = listFP;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultarClienteDet(ClienteDet clientedet, string Conexion, ref DataTable dt)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte" };
                object[] Valores = { clientedet.Id_Emp, clientedet.Id_Cd, clientedet.Id_Cte };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteDet_Consulta_Exp", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    dt.Rows.Add(new object[] {
                    dr.GetValue(dr.GetOrdinal("Id_CteDet")),
                    dr.GetValue(dr.GetOrdinal("Ter_Tipo")),
                    dr.GetValue(dr.GetOrdinal("DescTer_Tipo")),
                    dr.GetValue(dr.GetOrdinal("Id_Ter")),
                    dr.GetValue(dr.GetOrdinal("Ter_Nombre")),
                    dr.GetValue(dr.GetOrdinal("Id_Seg")),
                    dr.GetValue(dr.GetOrdinal("Seg_Descripcion")),
                    dr.GetValue(dr.GetOrdinal("Cte_UnidadDim")),
                    Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cte_Dimension"))).ToString("#,##0.00"),
                    dr.GetValue(dr.GetOrdinal("Cte_Pesos")),
                    dr.GetValue(dr.GetOrdinal("Cte_Potencial")),
                    dr.GetValue(dr.GetOrdinal("Cte_Activo")),
                    dr.GetValue(dr.GetOrdinal("Id_Rik")),
                    dr.GetValue(dr.GetOrdinal("Rik_Nombre")),

                    //SAUL GUERRA 20150507 BEGIN
                    dr.GetValue(dr.GetOrdinal("Id_TerServ")),
                    dr.GetValue(dr.GetOrdinal("TerServ")),
                    dr.GetValue(dr.GetOrdinal("Id_RIKServ")),
                    dr.GetValue(dr.GetOrdinal("RIKServ")),
                    //SAUL GUERRA 20150507 END

                    dr.GetValue(dr.GetOrdinal("Uen_Descripcion")),
                    dr.GetValue(dr.GetOrdinal("Cte_ManoObra")),
                    dr.GetValue(dr.GetOrdinal("Cte_GastoTerritorio")),
                    dr.GetValue(dr.GetOrdinal("Cte_FletePaga")),
                    dr.GetValue(dr.GetOrdinal("Cte_PorcComision")),
                    dr.GetValue(dr.GetOrdinal("Id_Uen")),
                    dr.GetValue(dr.GetOrdinal("Editable")),
                    //dr.GetValue(dr.GetOrdinal("ModalidadOP")),
                    //dr.GetValue(dr.GetOrdinal("Meta")),
                    //dr.GetValue(dr.GetOrdinal("ModalidadOP_Desc")),
                    dr.GetValue(dr.GetOrdinal("Cte_Tradicional")),
                    dr.GetValue(dr.GetOrdinal("Cte_Garantia")),
                    dr.GetValue(dr.GetOrdinal("Id_Cte"))
                    });
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFolioFactEle(Sesion sesion, int Tipo, ref string Folio)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(sesion.Emp_Cnx);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Tipo" };
                object[] Valores = { sesion.Id_Emp, sesion.Id_Cd_Ver, Tipo };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatConsFactEle_Consulta", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    Folio = dr["Cfe_NombreAcuse"].ToString().Trim();
                }

                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultarClienteDirEntrega(ClienteDirEntrega clienteDirEntrega, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_CteDirEntrega" };
                object[] Valores = { clienteDirEntrega.Id_Emp, clienteDirEntrega.Id_Cd, clienteDirEntrega.Id_Cte, clienteDirEntrega.Id_CteDirEntrega };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteDirEntrega_Consulta", ref dr, Parametros, Valores);
                Comun cliente;
                while (dr.Read())
                {
                    clienteDirEntrega.Id_Cte = Int32.Parse(dr["Id_CteDirEntrega"].ToString());
                    clienteDirEntrega.Id_Emp = Int32.Parse(dr["Id_Emp"].ToString());
                    clienteDirEntrega.Id_Cd = Int32.Parse(dr["Id_Cd"].ToString());
                    clienteDirEntrega.Id_CteDirEntrega = Int32.Parse(dr["Id_CteDirEntrega"].ToString());
                    clienteDirEntrega.Cte_Calle = dr["Cte_Calle"].ToString();
                    clienteDirEntrega.Cte_Numero = dr["Cte_Numero"].ToString();
                    clienteDirEntrega.Cte_Cp = dr["Cte_Cp"].ToString();
                    clienteDirEntrega.Cte_Colonia = dr["Cte_Colonia"].ToString();
                    clienteDirEntrega.Cte_Municipio = dr["Cte_Municipio"].ToString();
                    clienteDirEntrega.Cte_Estado = dr["Cte_Estado"].ToString();
                    clienteDirEntrega.Cte_Sector = dr["Cte_Sector"].ToString();
                    clienteDirEntrega.Cte_Telefono = dr["Cte_Telefono"].ToString();
                    clienteDirEntrega.Cte_Fax = dr["Cte_Fax"].ToString();
                    clienteDirEntrega.Cte_Telefono = dr["Cte_Telefono"].ToString();
                    clienteDirEntrega.Cte_HoraAm1 = dr["Cte_HoraAm1"].ToString();
                    clienteDirEntrega.Cte_HoraAm2 = dr["Cte_HoraAm2"].ToString();
                    clienteDirEntrega.Cte_HoraPm1 = dr["Cte_HoraPm1"].ToString();
                    clienteDirEntrega.Cte_HoraPm1 = dr["Cte_HoraPm2"].ToString();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarClienteDirEntrega(ClienteDirEntrega clienteDirEntrega, string Conexion, ref List<Comun> lista)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte" };
                object[] Valores = { clienteDirEntrega.Id_Emp, clienteDirEntrega.Id_Cd, clienteDirEntrega.Id_Cte };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteDirEntrega_Consulta", ref dr, Parametros, Valores);
                Comun cliente;
                while (dr.Read())
                {
                    cliente = new Comun();
                    cliente.IdStr = String.Format("{0}", (Int32.Parse(dr.GetValue(dr.GetOrdinal("Id_CteDirEntrega")).ToString()) + 1));
                    cliente.Descripcion = dr.GetValue(dr.GetOrdinal("Cte_Calle")).ToString() + ", " + dr.GetValue(dr.GetOrdinal("Cte_Numero")).ToString() + ", " + dr.GetValue(dr.GetOrdinal("Cte_Colonia")).ToString() + ", " + dr.GetValue(dr.GetOrdinal("Cte_Municipio")).ToString() + ", " + dr.GetValue(dr.GetOrdinal("Cte_Estado")).ToString();
                    lista.Add(cliente);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarClienteDirEntrega(ClienteDirEntrega clienteDirEntrega, string Conexion, ref DataTable dt)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte" };
                object[] Valores = { clienteDirEntrega.Id_Emp, clienteDirEntrega.Id_Cd, clienteDirEntrega.Id_Cte };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteDirEntrega_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    dt.Rows.Add(new object[] {
                    dr.GetValue(dr.GetOrdinal("Id_Cte")),
                    dr.GetValue(dr.GetOrdinal("Id_CteDirEntrega")),
                    dr.GetValue(dr.GetOrdinal("Cte_Calle")),
                    dr.GetValue(dr.GetOrdinal("Cte_Numero")),
                    dr.GetValue(dr.GetOrdinal("Cte_Cp")),
                    dr.GetValue(dr.GetOrdinal("Cte_Colonia")),
                    dr.GetValue(dr.GetOrdinal("Cte_Municipio")),
                    dr.GetValue(dr.GetOrdinal("Cte_Estado")),
                    dr.GetValue(dr.GetOrdinal("Cte_Sector")),
                    dr.GetValue(dr.GetOrdinal("Cte_Telefono")),
                    dr.GetValue(dr.GetOrdinal("Cte_Fax")),
                    dr.GetValue(dr.GetOrdinal("Cte_HoraAm1")),
                    dr.GetValue(dr.GetOrdinal("Cte_HoraAm2")),
                    dr.GetValue(dr.GetOrdinal("Cte_HoraPm1")),
                    dr.GetValue(dr.GetOrdinal("Cte_HoraPm2"))
                    });
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarClienteDet(Clientes clientes, DataTable dt, string Conexion)
        {
            if (dt.Rows.Count == 0) return;

            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            int verificador = 0;
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Cte", 
                                        "@Id_CteDet",
	                                    "@Id_Ter", 
                                        "@Ter_Tipo", 
	                                    "@Id_Seg", 
	                                    "@Cte_UnidadDim",
                                        "@Cte_Dimension",
                                        "@Cte_Pesos",
                                        "@Cte_Potencial",
                                        "@Cte_CarMP", 
		                                "@Cte_GasVarT", 
                                        "@Cte_FletePaga",
                                        "@Cte_PorcComision",
                                        "@Cte_Activo",
                                        "@Accion",
                                        "@Id_TerServ"/*,
                                        "@ModalidadOP",
                                        "@Meta"*/
                                      };

                object[] Valores = null;
                SqlCommand sqlcmd = null;

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    Valores = new object[] { 
                                        clientes.Id_Emp,
                                        clientes.Id_Cd,
                                        clientes.Id_Cte,
                                        x,
                                        dt.Rows[x]["Id_Ter"],
                                        dt.Rows[x]["Ter_Tipo"],
                                        dt.Rows[x]["Id_Seg"],
                                        dt.Rows[x]["Cte_UnidadDim"],
                                        dt.Rows[x]["Cte_Dimension"],
                                        dt.Rows[x]["Cte_Pesos"],
                                        dt.Rows[x]["Cte_Potencial"],
                                        dt.Rows[x]["Cte_ManoObra"],
                                        dt.Rows[x]["Cte_GastoTerritorio"],
                                        dt.Rows[x]["Cte_FletePaga"],
                                        dt.Rows[x]["Cte_PorcComision"],
                                        dt.Rows[x]["Cte_Activo"],
                                        x,
                                        dt.Rows[x]["Id_TerServ"]/*,
                                        dt.Rows[x]["ModalidadOP"],
                                        dt.Rows[x]["Meta"]*/
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteDet_Insertar", ref verificador, Parametros, Valores);
                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void InsertarClienteDet(Clientes clientes, DataTable dt, string Conexion, DataTable catClienteDet, DataTable catClienteDetGarantia, string efConexion)
        {
            if (dt.Rows.Count == 0) return;

            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            //System.Data.EntityClient.EntityConnection ec=new System.Data.EntityClient.EntityConnection(
            CD_CatClienteDetGarantia garantiasCD = new CD_CatClienteDetGarantia(efConexion);
            int verificador = 0;
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_Cte", 
                                        "@Id_CteDet",
	                                    "@Id_Ter", 
                                        "@Ter_Tipo", 
	                                    "@Id_Seg", 
	                                    "@Cte_UnidadDim",
                                        "@Cte_Dimension",
                                        "@Cte_Pesos",
                                        "@Cte_Potencial",
                                        "@Cte_CarMP", 
		                                "@Cte_GasVarT", 
                                        "@Cte_FletePaga",
                                        "@Cte_PorcComision",
                                        "@Cte_Activo",
                                        "@Accion",
                                        "@Id_TerServ",
                                        "@ModalidadOP",
                                        "@Meta",
                                        "@Cte_Tradicional",
                                        "@Cte_Garantia"
                                      };

                object[] Valores = null;
                SqlCommand sqlcmd = null;

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    var tradicionales = (from dr in catClienteDet.AsEnumerable()
                                         where dr["Id_CteDet"].ToString().CompareTo(dt.Rows[x]["Id_CteDet"].ToString()) == 0
                                         select dr).ToList();
                    int tradicional = 0;
                    if (tradicionales.Count > 0)
                    {
                        if (tradicionales[0]["Tradicional"] != null)
                        {
                            try
                            {
                                tradicional = int.Parse(tradicionales[0]["Tradicional"].ToString());
                            }
                            catch (Exception ex)
                            {
                                if (tradicionales[0]["Tradicional"].ToString().CompareTo("False") == 0)
                                {
                                    tradicional = 0;
                                }
                                else if (tradicionales[0]["Tradicional"].ToString().CompareTo("True") == 0)
                                {
                                    tradicional = 1;
                                }
                            }
                        }

                    }

                    int garantia = 0;
                    if (tradicionales.Count > 0)
                    {
                        if (tradicionales[0]["Garantia"] != null)
                        {
                            try
                            {
                                garantia = int.Parse(tradicionales[0]["Garantia"].ToString());
                            }
                            catch (Exception ex)
                            {
                                if (tradicionales[0]["Garantia"].ToString().CompareTo("False") == 0)
                                {
                                    garantia = 0;
                                }
                                else if (tradicionales[0]["Garantia"].ToString().CompareTo("True") == 0)
                                {
                                    garantia = 1;
                                }
                            }
                        }

                    }
                    Valores = new object[] { 
                                        clientes.Id_Emp,
                                        clientes.Id_Cd,
                                        clientes.Id_Cte,
                                        x,
                                        dt.Rows[x]["Id_Ter"],
                                        dt.Rows[x]["Ter_Tipo"],
                                        dt.Rows[x]["Id_Seg"],
                                        dt.Rows[x]["Cte_UnidadDim"],
                                        dt.Rows[x]["Cte_Dimension"],
                                        dt.Rows[x]["Cte_Pesos"],
                                        dt.Rows[x]["Cte_Potencial"],
                                        dt.Rows[x]["Cte_ManoObra"],
                                        dt.Rows[x]["Cte_GastoTerritorio"],
                                        dt.Rows[x]["Cte_FletePaga"],
                                        dt.Rows[x]["Cte_PorcComision"],
                                        dt.Rows[x]["Cte_Activo"],
                                        x,
                                        dt.Rows[x]["Id_TerServ"],
                                        0,//dt.Rows[x]["ModalidadOP"],
                                        0,//dt.Rows[x]["Meta"],
                                        tradicional,
                                        garantia
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteDet_Insertar_Exp", ref verificador, Parametros, Valores);
                    int id_cteDetGenerada = (int)verificador;
                    var garantias = (from dr in catClienteDetGarantia.AsEnumerable()
                                     where dr["Id_CteDet"].ToString().CompareTo(dt.Rows[x]["Id_CteDet"].ToString()) == 0
                                     select dr).ToList();
                    /*garantiasCD.Eliminar(clientes.Id_Emp, clientes.Id_Cd, clientes.Id_Cte, id_cteDetGenerada, CapaDatos.CurrentTransaction, CapaDatos.CurrentConnection);
                    foreach (var dr in garantias)
                    {
                        garantiasCD.Insertar(clientes.Id_Emp, clientes.Id_Cd, clientes.Id_Cte, id_cteDetGenerada, int.Parse(dr["Id_TG"].ToString()), CapaDatos.CurrentTransaction, CapaDatos.CurrentConnection);
                    }*/

                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        public void InsertarClienteDirEntrega(Clientes clientes, DataTable dt, string Conexion)
        {
            if (dt.Rows.Count == 0) return;

            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            int verificador = 0;
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
		                                "@Id_Emp",
		                                "@Id_Cd",
		                                "@Id_Cte",
		                                "@Id_CteDirEntrega",
		                                "@Cte_Calle",
		                                "@Cte_Numero",
		                                "@Cte_Cp",
		                                "@Cte_Colonia",
		                                "@Cte_Municipio",
		                                "@Cte_Estado",
		                                "@Cte_Sector",
		                                "@Cte_Telefono",
		                                "@Cte_Fax",                                        
                                        "@Cte_HoraAm1",
                                        "@Cte_HoraAm2",
                                        "@Cte_HoraPm1",
                                        "@Cte_HoraPm2",
                                        "@Accion"
                                      };

                object[] Valores = null;
                SqlCommand sqlcmd = null;

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    Valores = new object[] { 
                                        clientes.Id_Emp,
                                        clientes.Id_Cd,
                                        clientes.Id_Cte,
                                        x,
                                        dt.Rows[x]["Cte_Calle"],
                                        dt.Rows[x]["Cte_Numero"],
                                        dt.Rows[x]["Cte_Cp"],
                                        dt.Rows[x]["Cte_Colonia"],
                                        dt.Rows[x]["Cte_Municipio"],
                                        dt.Rows[x]["Cte_Estado"],
                                        dt.Rows[x]["Cte_Sector"],
                                        dt.Rows[x]["Cte_Telefono"],
                                        dt.Rows[x]["Cte_Fax"],
                                        dt.Rows[x]["Cte_HoraAm1"],
                                        dt.Rows[x]["Cte_HoraAm2"],
                                        dt.Rows[x]["Cte_HoraPm1"],
                                        dt.Rows[x]["Cte_HoraPm2"],
                                        x
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteDirEntrega_Insertar", ref verificador, Parametros, Valores);
                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        /// <summary>
        /// Recibe un id de cliente, sesion(id_emp,id_cd_ver) y devulve una lista de los territorios a los que pertenece 
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="sesion"></param>
        /// <param name="territorios"></param>
        public void ConsultaTerritoriosDelCliente(int id_cliente, CapaEntidad.Sesion sesion, ref List<Territorios> territorios)
        {//RM
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] parametros = { 
                                          "@Id1",
                                          "@Id2",
                                          "@Id3"
                                      };
                string[] Valores = {
                                       sesion.Id_Emp.ToString(),
                                       sesion.Id_Cd_Ver.ToString(),
                                       id_cliente.ToString()
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorioCte_Combo", ref dr, parametros, Valores);

                Territorios territorio = new Territorios();
                while (dr.Read())
                {
                    territorio = new Territorios();
                    territorio.Id_Ter = dr.GetInt32(dr.GetOrdinal("Id"));
                    territorio.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    territorio.Id_Rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rik"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    territorio.Rik_Nombre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rik_Nombre"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    territorios.Add(territorio);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Recibe un id de cliente, sesion(id_emp,id_cd_ver) y devulve una lista de los territorios a los que pertenece 
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="sesion"></param>
        /// <param name="territorios"></param>
        public void ConsultaTodosTerritoriosDelCliente(int id_cliente, CapaEntidad.Sesion sesion, ref List<Territorios> territorios)
        {//RM
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] parametros = { 
                                          "@Id1",
                                          "@Id2",
                                          "@Id3"
                                      };
                string[] Valores = {
                                       sesion.Id_Emp.ToString(),
                                       sesion.Id_Cd_Ver.ToString(),
                                       id_cliente.ToString()
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorioCteTodos_Combo", ref dr, parametros, Valores);

                Territorios territorio = new Territorios();
                while (dr.Read())
                {
                    territorio = new Territorios();
                    territorio.Id_Ter = dr.GetInt32(dr.GetOrdinal("Id"));
                    territorio.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    territorio.Id_Rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rik"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    territorio.Rik_Nombre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rik_Nombre"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    territorios.Add(territorio);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Recibe un id de cliente, sesion(id_emp,id_cd_ver) y devulve una lista de los territorios a los que pertenece 
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="sesion"></param>
        /// <param name="territorios"></param>
        public void ConsultaTodosTerritoriosDelClienteBI(int id_cliente, CapaEntidad.Sesion sesion, ref List<Territorios> territorios)
        {//RM
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] parametros = { 
                                          "@Id1",
                                          "@Id2",
                                          "@Id3"
                                      };
                string[] Valores = {
                                       sesion.Id_Emp.ToString(),
                                       sesion.Id_Cd_Ver.ToString(),
                                       id_cliente.ToString()
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTerritorioCteTodos_ComboBI", ref dr, parametros, Valores);

                Territorios territorio = new Territorios();
                while (dr.Read())
                {
                    territorio = new Territorios();
                    territorio.Id_Ter = dr.GetInt32(dr.GetOrdinal("Id"));
                    territorio.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    territorio.Id_Rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rik"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    territorio.Rik_Nombre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rik_Nombre"))) ? string.Empty : dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    territorios.Add(territorio);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReporteRentabilidad_ConsultarEstadistica(int Id_Emp, int Id_Cd_Ver, int Id_Cte, int? Id_Ter, string Anio, String Mes, ref List<EstadisticaRentabilidad> List, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd_Ver"
                                          ,"@Id_Cte"
                                          ,"@Id_Ter"
                                          ,"@Anio"
                                          ,"@Mes"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Cd_Ver
                                       ,Id_Cte
                                       ,Id_Ter
                                       ,Anio
                                       ,Mes
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRep_Estadistica_Rentabilidad", ref dr, Parametros, Valores);

                EstadisticaRentabilidad estadisticaRentabilidad;
                while (dr.Read())
                {
                    estadisticaRentabilidad = new EstadisticaRentabilidad();
                    estadisticaRentabilidad.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    estadisticaRentabilidad.Id_Cd_Ver = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd_Ver")));
                    estadisticaRentabilidad.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    estadisticaRentabilidad.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    estadisticaRentabilidad.CtaCobrarPorc = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("CtaCobrarPorc")));
                    estadisticaRentabilidad.InvDiasCant = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("InvDiasCant")));
                    estadisticaRentabilidad.InvConsigDiasCant = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("InvConsigDiasCant")));
                    estadisticaRentabilidad.UtilidadRemanentePorc = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UtilidadRemanentePorc")));
                    estadisticaRentabilidad.CtaCobrar = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("CtaCobrar")));
                    estadisticaRentabilidad.InvDias = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("InvDias")));
                    estadisticaRentabilidad.InvComodatoOtrosProdCant = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("InvComodatoOtrosProdCant")));
                    estadisticaRentabilidad.InvComodatoOtrosProd = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("InvComodatoOtrosProd")));
                    estadisticaRentabilidad.InvConsigDias = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("InvConsigDias")));
                    estadisticaRentabilidad.FinanProv = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("FinanProv")));
                    estadisticaRentabilidad.InvActivosNetosOPN = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("InvActivosNetosOPN")));
                    estadisticaRentabilidad.InvTotalActivos = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("InvTotalActivos")));
                    estadisticaRentabilidad.InvActivosFijos = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("InvActivosFijos")));
                    estadisticaRentabilidad.UtilidadRemanente = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UtilidadRemanente")));
                    estadisticaRentabilidad.UafirActivos = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UafirActivos")));
                    estadisticaRentabilidad.CostoCapital = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("CostoCapital")));
                    estadisticaRentabilidad.VentaNetaMon = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VentaNetaMon")));
                    estadisticaRentabilidad.CostoMaterialMon = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("CostoMaterialMon")));
                    estadisticaRentabilidad.FleteMon = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("FleteMon")));
                    estadisticaRentabilidad.ManoObraMon = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ManoObraMon")));
                    estadisticaRentabilidad.UtilidadMon = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UtilidadMon")));
                    estadisticaRentabilidad.CostoServEquipoMon = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("CostoServEquipoMon")));
                    estadisticaRentabilidad.AmortizacionMon = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("AmortizacionMon")));
                    estadisticaRentabilidad.ComisionRepMon = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ComisionRepMon")));
                    estadisticaRentabilidad.ContribucionGastosFijosOtrosMon = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ContribucionGastosFijosOtrosMon")));
                    estadisticaRentabilidad.ContribucionGastosFijosPapelMon = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ContribucionGastosFijosPapelMon")));
                    estadisticaRentabilidad.UafirMensualMon = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UafirMensualMon")));
                    estadisticaRentabilidad.CargoUCSMon = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("CargoUCSMon")));
                    estadisticaRentabilidad.FletesPagadosMon = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("FletesPagadosMon")));
                    estadisticaRentabilidad.OtrosGastosVariablesMon = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("OtrosGastosVariablesMon")));
                    estadisticaRentabilidad.GastosVariablesMon = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("GastosVariablesMon")));
                    estadisticaRentabilidad.UafirAnualMon = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UafirAnualMon")));
                    estadisticaRentabilidad.CostoCapitalMon = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("CostoCapitalMon")));
                    estadisticaRentabilidad.UtilidadRemanenteMon = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UtilidadRemanenteMon")));
                    estadisticaRentabilidad.UafirDespuesImpMon = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UafirDespuesImpMon")));
                    estadisticaRentabilidad.ISRyPTU = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ISRyPTU")));
                    estadisticaRentabilidad.ISRyPTUMon = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ISRyPTUMon")));
                    estadisticaRentabilidad.GastosVariablesPorc = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("GastosVariablesPorc")));
                    estadisticaRentabilidad.OtrosGastosVariablesPorc = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("OtrosGastosVariablesPorc")));
                    estadisticaRentabilidad.FletesPagadosPorc = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("FletesPagadosPorc")));
                    estadisticaRentabilidad.CargoUCSPorc = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("CargoUCSPorc")));
                    estadisticaRentabilidad.UafirMensualPorc = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UafirMensualPorc")));
                    estadisticaRentabilidad.ContribucionGastosFijosPapelPorc = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ContribucionGastosFijosPapelPorc")));
                    estadisticaRentabilidad.ContribucionGastosFijosOtrosPorc = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ContribucionGastosFijosOtrosPorc")));
                    estadisticaRentabilidad.AmortizacionPorc = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("AmortizacionPorc")));
                    estadisticaRentabilidad.CostoServEquipoPorc = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("CostoServEquipoPorc")));
                    estadisticaRentabilidad.ComisionRepPorc = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ComisionRepPorc")));
                    estadisticaRentabilidad.UtilidadPorc = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UtilidadPorc")));
                    estadisticaRentabilidad.ManoObraPorc = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ManoObraPorc")));
                    estadisticaRentabilidad.FletePorc = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("FletePorc")));
                    estadisticaRentabilidad.FletePorc2 = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("FletePorc2")));
                    estadisticaRentabilidad.CostoMaterialPorc = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("CostoMaterialPorc")));
                    estadisticaRentabilidad.UtilidadMarginalMon = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UtilidadMarginalMon")));
                    estadisticaRentabilidad.UtilidadMarginalPorc = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UtilidadMarginalPorc")));
                    estadisticaRentabilidad.UtilidadBruta = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UtilidadBruta")));
                    List.Add(estadisticaRentabilidad);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReporteRentabilidad_ConsultarTotales(int Id_Emp, int Id_Cd_Ver, int Id_Cte, int? Id_Ter, string periodo, string ventas, ref DataTable dt, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd_Ver"
                                          ,"@Id_Cte"
                                          ,"@Id_Ter"
                                          ,"@periodo"
                                          ,"@ventas"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Cd_Ver
                                       ,Id_Cte
                                       ,Id_Ter
                                       ,periodo
                                       ,ventas
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRep_VenRentabilidad_Ventas_Ultimo", "tabla", ref dt, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Lista(Clientes cte, ref List<Clientes> List, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" ,
                                          "@Id_Cd",
                                          "@Id_Uen",
                                          "@Id_Seg",
                                          "@Id_Ter",
                                          "@Id_Rik",
                                          "@Id_Cte",
                                          "@Cte_Nombre"
                                      };
                object[] Valores = { 
                                       cte.Id_Emp,
                                       cte.Id_Cd,
                                       cte.Id_Uen,
                                       cte.Id_Seg,
                                       cte.Id_Terr,
                                       cte.Id_Rik,
                                       cte.Id_Cte,
                                       cte.Cte_NomComercial
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCliente_Lista", ref dr, Parametros, Valores);

                Clientes cliente;
                while (dr.Read())
                {
                    cliente = new Clientes();
                    cliente.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    cliente.Cte_NomComercial = Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));
                    cliente.UnidadDimension = Convert.ToString(dr.GetValue(dr.GetOrdinal("UnidadDimension")));
                    cliente.Dimension = Convert.ToString(dr.GetValue(dr.GetOrdinal("Dimension")));
                    cliente.VPTeorico = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VPTeorico")));
                    cliente.VPObservado = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VPObservado")));
                    cliente.Id_Uen = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Uen")));
                    cliente.Id_Seg = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Seg")));
                    cliente.Id_Terr = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    List.Add(cliente);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //  RFH
        // 6 Sep 2018  
        // 
        public List<eClienteBuscar> ListarBusqueda(int Id_Emp, int Id_Cd, int Id_Uen, int Id_Seg, int Id_Terr, int Id_Rik,
            string TextoBuscar, string Conexion)
        {
            List<eClienteBuscar> lst = new List<eClienteBuscar>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" ,
                                          "@Id_Cd",
                                          "@Id_Uen",
                                          "@Id_Seg",
                                          "@Id_Ter",
                                          "@Id_Rik",                                          
                                          "@TextoBuscar"
                                      };
                object[] Valores = { 
                                       Id_Emp,
                                       Id_Cd,
                                       (object)null,
                                       (object)null,
                                       (object)null,
                                       (object)null,
                                       TextoBuscar
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCliente_ListaBusqueda", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    eClienteBuscar obj = new eClienteBuscar();
                    obj.Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte")));
                    obj.RFC = Convert.ToString(dr.GetValue(dr.GetOrdinal("RFC")));
                    obj.NomComercial = Convert.ToString(dr.GetValue(dr.GetOrdinal("NomComercial")));
                    obj.VPObservado = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VPObservado")));
                    obj.UEN = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UEN")));
                    obj.Segmento = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Segmento")));
                    obj.IdTer = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdTer")));
                    obj.TerNombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("TerritorioNombre")));
                    obj.RegistrosEcontrados = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RegistrosEncontrados")));
                    lst.Add(obj);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                lst = null;
            }
            return lst;
        }


        public void ConsultaClienteTerritorio(ref Clientes cte, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Cte" ,
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Ter"
                                      };

                object[] Valores = { 
                                       cte.Id_Cte,
                                       cte.Id_Emp,
                                       cte.Id_Cd,
                                       cte.Id_Terr
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMClienteTerritorio", ref dr, Parametros, Valores);


                if (dr.HasRows)
                {
                    dr.Read();

                    cte.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    cte.Cte_NomComercial = Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));
                    cte.Ter_Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("Ter_Nombre")));
                    cte.Seg_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Seg_Descripcion")));
                    cte.Uen_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Uen_Descripcion")));
                    cte.Seg_Unidades = Convert.ToString(dr.GetValue(dr.GetOrdinal("Seg_Unidades")));
                    cte.Seg_ValUniDim = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Seg_ValUniDim")));
                    cte.Cte_Dimension = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cte_Dimension")));
                    cte.Id_Seg = (int?)dr.GetValue(dr.GetOrdinal("Id_Seg"));
                    cte.VPObservado = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VPObservado")));
                    cte.VPTeorico = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VPTeorico")));
                    dr.Close();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPermisosUEN(ref DataTable dt, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsulta_Permisos_UEN", ref dr);

                while (dr.Read())
                {
                    dt.Rows.Add(new object[] {
                    dr.GetValue(dr.GetOrdinal("Id_UenPermiso")),
                    dr.GetValue(dr.GetOrdinal("Id_UenPotencial")),                    
                    dr.GetValue(dr.GetOrdinal("Id_UenDimension"))
                    
                    });
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTipoCDC(int Id_Cd_Ver, ref int Tipo_CDC, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cd_Ver" };
                object[] valores = { Id_Cd_Ver };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsulta_TipoCDC", ref Tipo_CDC, Parametros, valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClienteTieneCuentaNacional(ref Clientes cte, ref int TieneCuentaNacional, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                TieneCuentaNacional = 0;

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte" };
                object[] valores = { cte.Id_Emp, cte.Id_Cd, cte.Id_Cte };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsulta_ClienteCuentaNacional", ref TieneCuentaNacional, Parametros, valores);


                cte.Cte_RemisionElectronica = Convert.IsDBNull(TieneCuentaNacional) ? -1 : Convert.ToInt32(TieneCuentaNacional);
                if (cte.Cte_RemisionElectronica != -1)
                {
                    TieneCuentaNacional = 1;
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFactura_ConsecutivoFacElectronica(int Id_Emp, int Id_Cd, int Id_Cfe, int Cfe_TMov, ref int verificador, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cfe", "@Cfe_TMov" };
                object[] Valores = { Id_Emp, Id_Cd, Id_Cfe == -1 ? (int?)null : Id_Cfe, Cfe_TMov };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_ConsultarConsFacElectronica", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EstructuraSegmento(ref DataSet dsEstructuraSegmento, Clientes cte, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Seg", "@Id_Cte" };
                object[] Valores = { cte.Id_Emp, cte.Id_Cd, cte.Id_Seg, cte.Id_Cte };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMEstructuraSegmento", ref dr, Parametros, Valores);

                //creamos esquema
                DataTable estructura;

                //creamos tabla para guardar los datos
                DataTable dataTable;

                for (int x = 0; x < 4; x++)
                {
                    estructura = dr.GetSchemaTable();
                    dataTable = new DataTable();

                    //generemos la estructura de columnas
                    for (int i = 0; i <= estructura.Rows.Count - 1; i++)
                    {
                        DataRow dataRow = estructura.Rows[i];
                        string columnName = dataRow["ColumnName"].ToString();
                        DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                        dataTable.Columns.Add(column);
                    }
                    dsEstructuraSegmento.Tables.Add(dataTable);

                    while (dr.Read())
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i <= dr.FieldCount - 1; i++)
                        {
                            dataRow[i] = dr.GetValue(i);
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                    if (!dr.NextResult())
                    {
                        break;
                    }
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizaPotencial(Clientes cte, double NuevoVPObservado, string NuevoVPObservadoApp, ref int verificador, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Id_Seg", 
                                          "@Id_Ter",
                                          "@Id_Cte",
                                          "@Id_Apl",
                                          "@NuevoVPObservado",
                                          "@NuevoVPObservadoApp"
                                      };
                object[] Valores = { 
                                       cte.Id_Emp, 
                                       cte.Id_Cd, 
                                       cte.Id_Seg,
                                       cte.Id_Terr,
                                       cte.Id_Cte,
                                       cte.Id_Apl,
                                       NuevoVPObservado,
                                       NuevoVPObservadoApp
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMVPObservadoCliente_Modificar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizaDimension(Clientes cte, int Dimension, double? VPTeorico, DateTime Fecha, ref int verificador, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Id_Ter",
                                          "@Id_Cte",
                                          "@Dimension",
                                          "@VPTeorico",
                                          "@Fecha"
                                      };
                object[] Valores = { 
                                       cte.Id_Emp, 
                                       cte.Id_Cd, 
                                       cte.Id_Terr,
                                       cte.Id_Cte,
                                       Dimension,
                                       VPTeorico,
                                       Fecha
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMDimensionCliente_Modificar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaContactos(Clientes cte, ref DataSet dsContactosClientes, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Seg", "@Id_Cte" };
                object[] Valores = { cte.Id_Emp, cte.Id_Cd, cte.Id_Seg, cte.Id_Cte };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCrmContactosCliente_Lista", ref dr, Parametros, Valores);

                //creamos esquema
                DataTable estructura; //= dr.GetSchemaTable();

                //creamos tabla para guardar los datos
                DataTable dataTable; //= new DataTable();

                for (int x = 0; x < 10; x++)
                {
                    estructura = dr.GetSchemaTable();
                    dataTable = new DataTable();
                    //generemos la estructura de columnas
                    for (int i = 0; i <= estructura.Rows.Count - 1; i++)
                    {
                        DataRow dataRow = estructura.Rows[i];
                        string columnName = dataRow["ColumnName"].ToString();
                        DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                        dataTable.Columns.Add(column);
                    }
                    dsContactosClientes.Tables.Add(dataTable);

                    while (dr.Read())
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i <= dr.FieldCount - 1; i++)
                        {
                            dataRow[i] = dr.GetValue(i);
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                    if (!dr.NextResult())
                    {
                        break;
                    }
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarContacto(Contacto cont, ref int verificador, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp", 
                                          "@Id_Cd", 
                                          "@Id_Con"
                                      };
                object[] Valores = { 
                                       
                                       cont.Id_Emp, 
                                       cont.Id_Cd, 
                                       cont.Id_Con,
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMContacto_Eliminar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAdenda(int Id_Emp, int Id_Cd_Ver, string Ade_Descripcion, ref List<AdendaDet> listCab, ref List<AdendaDet> listDet, ref List<AdendaDet> listCabR, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" ,
                                          "@Id_Cd",
                                          "@Ade_Descripcion"
                                      };
                object[] Valores = { 
                                       Id_Emp,
                                       Id_Cd_Ver,
                                       Ade_Descripcion
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAdendaDetNacional_Consultar", ref dr, Parametros, Valores);
                AdendaDet adendaDet;
                while (dr.Read())
                {
                    adendaDet = new AdendaDet();
                    adendaDet.Id_AdeDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AdeDet")));
                    adendaDet.Campo = Convert.ToString(dr.GetValue(dr.GetOrdinal("Ade_Campo")));
                    adendaDet.Nodo = Convert.ToString(dr.GetValue(dr.GetOrdinal("Ade_Nodo")));
                    adendaDet.Longitud = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ade_Longitud")));
                    adendaDet.Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ade_Tipo")));
                    adendaDet.Requerido = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ade_Requerido")));
                    switch (adendaDet.Tipo)
                    {
                        case 1:
                        case 3:
                        case 5:
                            listCab.Add(adendaDet);
                            break;
                        case 2:
                        case 4:
                        case 6:
                        case 8:
                            listDet.Add(adendaDet);
                            break;
                        case 7:
                            listCabR.Add(adendaDet);
                            break;
                        default:
                            break;
                    }
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAdenda(int Id_Emp, int Id_Cd_Ver, int Id_cte, string Tipo, ref List<AdendaDet> listCab, ref List<AdendaDet> listDet, ref List<AdendaDet> listCabR, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" ,
                                          "@Id_Cd",
                                          "@Id_Cte",
                                          "@Ade_Tipo"
                                      };
                object[] Valores = { 
                                       Id_Emp,
                                       Id_Cd_Ver,
                                       Id_cte,
                                       Tipo,
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAdendaDet_Consultar", ref dr, Parametros, Valores);
                AdendaDet adendaDet;
                while (dr.Read())
                {
                    adendaDet = new AdendaDet();
                    adendaDet.Id_AdeDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_AdeDet")));
                    adendaDet.Campo = Convert.ToString(dr.GetValue(dr.GetOrdinal("Ade_Campo")));
                    adendaDet.Nodo = Convert.ToString(dr.GetValue(dr.GetOrdinal("Ade_Nodo")));
                    adendaDet.Longitud = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ade_Longitud")));
                    adendaDet.Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ade_Tipo")));
                    adendaDet.Requerido = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ade_Requerido")));
                    switch (adendaDet.Tipo)
                    {
                        case 1:
                        case 3:
                        case 5:
                            listCab.Add(adendaDet);
                            break;
                        case 2:
                        case 4:
                        case 6:
                        case 8:
                            listDet.Add(adendaDet);
                            break;
                        case 7:
                            listCabR.Add(adendaDet);
                            break;
                        default:
                            break;
                    }
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClientes(Clientes cte, string Conexion, ref List<Comun> List, object FiltroId, object FiltroDesc)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ter", "@FiltroId", "@FiltroDesc" };
                object[] Valores = { cte.Id_Emp, cte.Id_Cd, cte.Id_Terr, FiltroId, FiltroDesc };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCliente_Lista", ref dr, Parametros, Valores);
                Comun cliente;
                while (dr.Read())
                {
                    cliente = new Comun();
                    cliente.IdStr = dr.GetValue(dr.GetOrdinal("Id")).ToString();
                    cliente.Descripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    List.Add(cliente);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClientesTerAsesor(Clientes cte, string pConexion, object pFiltroId, object pFiltroDesc, ref List<Comun> pList)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(pConexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Rik", "@FiltroId", "@FiltroDesc" };
                object[] Valores = { cte.Id_Emp, cte.Id_Cd, cte.Id_Rik, pFiltroId, pFiltroDesc };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteTerAsesor_Lista", ref dr, Parametros, Valores);
                Comun cliente;
                while (dr.Read())
                {
                    cliente = new Comun();
                    cliente.IdStr = dr.GetValue(dr.GetOrdinal("Id")).ToString();
                    cliente.Descripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    pList.Add(cliente);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPrecios(Clientes cte, string Conexion, ref List<Comun> List, object FiltroId, object FiltroDesc)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@FiltroId", "@FiltroDesc" };
                object[] Valores = { cte.Id_Emp, cte.Id_Cd, cte.Id_Cte, FiltroId, FiltroDesc };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_UltimosPrecios", ref dr, Parametros, Valores);
                Comun cliente;
                while (dr.Read())
                {
                    cliente = new Comun();
                    cliente.IdStr = dr.GetValue(dr.GetOrdinal("Id")).ToString();
                    cliente.Descripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    cliente.ValorDoble = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Precio")));
                    List.Add(cliente);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarCteFormaPago(Clientes clientes, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Fpa", "@Contador" };
                object[] Valores;
                SqlCommand sqlcmd = default(SqlCommand);
                int contador = 0;
                foreach (FormaPago dr in clientes.FormasPago)
                {
                    Valores = new object[] { clientes.Id_Emp, clientes.Id_Cd, clientes.Id_Cte, dr.Id_Fpa, contador };
                    sqlcmd = CapaDatos.GenerarSqlCommand("CatClienteFPago_Insertar", ref verificador, Parametros, Valores);
                    contador = 1;
                }
                CapaDatos.CommitTrans();
                if (sqlcmd != null)
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ConsultaEstadistica(Clientes cte, string Conexion, ref List<Comun> List, object FiltroId, object FiltroDesc)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@FiltroId", "@FiltroDesc" };
                object[] Valores = { cte.Id_Emp, cte.Id_Cd, cte.Id_Cte, FiltroId, FiltroDesc };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spEstVentaCliente_Ventana", ref dr, Parametros, Valores);
                Comun cliente;
                while (dr.Read())
                {
                    cliente = new Comun();
                    cliente.IdStr2 = dr.GetValue(dr.GetOrdinal("Id2")).ToString();
                    cliente.IdStr = dr.GetValue(dr.GetOrdinal("Id")).ToString();
                    cliente.Descripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    cliente.ValorDoble = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Val1")));
                    cliente.ValorDoble2 = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Val2")));
                    cliente.ValorDoble3 = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Val3")));

                    cliente.ValorInt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Val4")));
                    cliente.ValorInt2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Val5")));
                    cliente.ValorInt3 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Val6")));

                    cliente.ValorStr = (dr.GetValue(dr.GetOrdinal("Col1"))).ToString();
                    cliente.ValorStr2 = (dr.GetValue(dr.GetOrdinal("Col2"))).ToString();
                    cliente.ValorStr3 = (dr.GetValue(dr.GetOrdinal("Col3"))).ToString();
                    List.Add(cliente);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaIndicadores(Clientes cte, string Conexion, ref List<Producto> List, object FiltroId, object FiltroDesc)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@FiltroId", "@FiltroDesc" };
                object[] Valores = { cte.Id_Emp, cte.Id_Cd, cte.Id_Cte, FiltroId, FiltroDesc };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spClienteIndicadores_Ventana", ref dr, Parametros, Valores);
                Producto producto;
                while (dr.Read())
                {
                    producto = new Producto();
                    producto.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    producto.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    producto.Prd_Presentacion = dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString();
                    producto.Uni_Descripcion = dr.GetValue(dr.GetOrdinal("Uni_Descripcion")).ToString();
                    producto.Prd_InvInicial = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_InvInicial")));
                    producto.Prd_InvFinal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_InvFinal")));

                    producto.Prd_Fisico = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Fisico")));
                    producto.Prd_Ordenado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Ordenado")));
                    producto.Prd_Asignado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Asignado")));

                    producto.Prd_Transito = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Transito")));


                    List.Add(producto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarClienteTransf(Clientes cte, string Conexion)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_Rik", "@Id_Ter", "@Ignora_Activo" };
                object[] Valores = { 
                                       cte.Id_Emp, 
                                       cte.Id_Cd, 
                                       cte.Id_Cte, 
                                       cte.Id_Rik <= 0 ? (object)null : cte.Id_Rik, 
                                       cte.Id_Terr <= 0 ? (object)null : cte.Id_Terr,
                                       cte.Ignora_Inactivo
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCliente_ConsultaTransf", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    cte.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    cte.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    cte.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    cte.Id_Rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rik"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    cte.Id_Cfe = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cfe"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cfe")));
                    cte.Id_Corp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Corp"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Corp")));
                    cte.FacSerie = dr.GetValue(dr.GetOrdinal("FacSerie")).ToString();
                    cte.Cte_NomComercial = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    cte.Cte_NomCorto = dr.IsDBNull(dr.GetOrdinal("Cte_NomCorto")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_NomCorto"));
                    cte.Cte_FacCalle = (string)dr.GetValue(dr.GetOrdinal("Cte_FacCalle"));
                    cte.Cte_FacNumero = (string)dr.GetValue(dr.GetOrdinal("Cte_FacNumero"));
                    cte.Cte_FacNumeroInterior = (string)dr.GetValue(dr.GetOrdinal("Cte_FacNumeroInterior"));
                    cte.Cte_FacCp = (string)dr.GetValue(dr.GetOrdinal("Cte_FacCp"));
                    cte.Cte_FacColonia = (string)dr.GetValue(dr.GetOrdinal("Cte_FacColonia"));
                    cte.Cte_FacMunicipio = (string)dr.GetValue(dr.GetOrdinal("Cte_FacMunicipio"));
                    cte.Cte_FacTel = (string)dr.GetValue(dr.GetOrdinal("Cte_FacTel"));
                    cte.Cte_FacRfc = (string)dr.GetValue(dr.GetOrdinal("Cte_FacRfc"));
                    cte.Cte_FacEstado = (string)dr.GetValue(dr.GetOrdinal("Cte_FacEstado"));
                    cte.Cte_Calle = (string)dr.GetValue(dr.GetOrdinal("Cte_Calle"));
                    cte.Cte_Numero = (string)dr.GetValue(dr.GetOrdinal("Cte_Numero"));
                    cte.Cte_NumeroInterior = (string)dr.GetValue(dr.GetOrdinal("Cte_NumeroInterior"));
                    string cp = (string)dr.GetValue(dr.GetOrdinal("Cte_Cp"));
                    cte.Cte_Cp = dr.GetValue(dr.GetOrdinal("Cte_Cp")).ToString();
                    cte.Cte_Colonia = (string)dr.GetValue(dr.GetOrdinal("Cte_Colonia"));
                    cte.Cte_Municipio = (string)dr.GetValue(dr.GetOrdinal("Cte_Municipio"));
                    cte.Cte_Estado = (string)dr.GetValue(dr.GetOrdinal("Cte_Estado"));
                    cte.Cte_DRfc = (string)dr.GetValue(dr.GetOrdinal("Cte_Rfc"));
                    cte.Cte_Telefono = dr.GetValue(dr.GetOrdinal("Cte_Telefono")).ToString();
                    cte.Cte_Fax = dr.GetValue(dr.GetOrdinal("Cte_Fax")).ToString();
                    cte.Cte_Contacto = (string)dr.GetValue(dr.GetOrdinal("Cte_Contacto"));
                    cte.Cte_Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_Tipo")));
                    cte.Cte_Email = (string)dr.GetValue(dr.GetOrdinal("Cte_Email"));
                    cte.Cte_Credito = (bool)dr.GetValue(dr.GetOrdinal("Cte_Credito"));
                    cte.Cte_Facturacion = (bool)dr.GetValue(dr.GetOrdinal("Cte_Facturacion"));
                    cte.Id_Mon = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Mon"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Mon")));
                    cte.Cte_LimCobr = (double)dr.GetValue(dr.GetOrdinal("Cte_LimCobr"));
                    cte.Cte_RHoraam1 = dr.IsDBNull(dr.GetOrdinal("Cte_RHoraam1")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_RHoraam1"));
                    cte.Cte_RHoraam2 = dr.IsDBNull(dr.GetOrdinal("Cte_RHoraam2")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_RHoraam2"));
                    cte.Cte_RHorapm1 = dr.IsDBNull(dr.GetOrdinal("Cte_RHorapm1")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_RHorapm1"));
                    cte.Cte_RHorapm2 = dr.IsDBNull(dr.GetOrdinal("Cte_RHorapm2")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_RHorapm2"));
                    cte.Cte_RLunes = (bool)dr.GetValue(dr.GetOrdinal("Cte_RLunes"));
                    cte.Cte_RMartes = (bool)dr.GetValue(dr.GetOrdinal("Cte_RMartes"));
                    cte.Cte_RMiercoles = (bool)dr.GetValue(dr.GetOrdinal("Cte_RMiercoles"));
                    cte.Cte_RJueves = (bool)dr.GetValue(dr.GetOrdinal("Cte_RJueves"));
                    cte.Cte_RViernes = (bool)dr.GetValue(dr.GetOrdinal("Cte_RViernes"));
                    cte.Cte_RSabado = (bool)dr.GetValue(dr.GetOrdinal("Cte_RSabado"));
                    cte.Cte_RDomingo = (bool)dr.GetValue(dr.GetOrdinal("Cte_RDomingo"));
                    cte.Cte_CondPago = (int)dr.GetValue(dr.GetOrdinal("Cte_CondPago"));
                    cte.Cte_CPLunes = (bool)dr.GetValue(dr.GetOrdinal("Cte_CPLunes"));
                    cte.Cte_CPMartes = (bool)dr.GetValue(dr.GetOrdinal("Cte_CPMartes"));
                    cte.Cte_CPMiercoles = (bool)dr.GetValue(dr.GetOrdinal("Cte_CPMiercoles"));
                    cte.Cte_CPJueves = (bool)dr.GetValue(dr.GetOrdinal("Cte_CPJueves"));
                    cte.Cte_CPViernes = (bool)dr.GetValue(dr.GetOrdinal("Cte_CPViernes"));
                    cte.Cte_CPSabado = (bool)dr.GetValue(dr.GetOrdinal("Cte_CPSabado"));
                    cte.Cte_CPDomingo = (bool)dr.GetValue(dr.GetOrdinal("Cte_CPDomingo"));
                    cte.Cte_Comisiones = (bool)dr.GetValue(dr.GetOrdinal("Cte_Comisiones"));
                    cte.Cte_DesgIva = (bool)dr.GetValue(dr.GetOrdinal("Cte_DesgIva"));
                    cte.Cte_RetIva = (bool)dr.GetValue(dr.GetOrdinal("Cte_RetIva"));
                    cte.Cte_AsignacionPed = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_AsignacionPed"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_AsignacionPed")));
                    cte.Id_Ade = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ade"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ade")));
                    cte.Cte_SerieNCre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_SerieNCre"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_SerieNCre")));
                    cte.Cte_SerieNCa = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cte_SerieNCa"))) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_SerieNCa")));
                    cte.Estatus = (bool)dr.GetValue(dr.GetOrdinal("Cte_Activo"));

                    cte.Cte_CreditoSuspendido = (bool)dr.GetValue(dr.GetOrdinal("Cte_CreditoSuspendido"));
                    cte.Cte_PHoraam1 = dr.IsDBNull(dr.GetOrdinal("Cte_PHoraam1")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_PHoraam1"));
                    cte.Cte_PHoraam2 = dr.IsDBNull(dr.GetOrdinal("Cte_PHoraam2")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_PHoraam2"));
                    cte.Cte_PHorapm1 = dr.IsDBNull(dr.GetOrdinal("Cte_PHorapm1")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_PHorapm1"));
                    cte.Cte_PHorapm2 = dr.IsDBNull(dr.GetOrdinal("Cte_PHorapm2")) ? (string)null : (string)dr.GetValue(dr.GetOrdinal("Cte_PHorapm2"));
                    cte.Cte_SemRec = dr.IsDBNull(dr.GetOrdinal("Cte_SemRec")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_SemRec")));
                    cte.Cte_RecLunes = (bool)dr.GetValue(dr.GetOrdinal("Cte_RecLunes"));
                    cte.Cte_RecMartes = (bool)dr.GetValue(dr.GetOrdinal("Cte_RecMartes"));
                    cte.Cte_RecMiercoles = (bool)dr.GetValue(dr.GetOrdinal("Cte_RecMiercoles"));
                    cte.Cte_RecJueves = (bool)dr.GetValue(dr.GetOrdinal("Cte_RecJueves"));
                    cte.Cte_RecViernes = (bool)dr.GetValue(dr.GetOrdinal("Cte_RecViernes"));
                    cte.Cte_RecSabado = (bool)dr.GetValue(dr.GetOrdinal("Cte_RecSabado"));
                    cte.Cte_RecDomingo = (bool)dr.GetValue(dr.GetOrdinal("Cte_RecDomingo"));
                    cte.Cte_Efectivo = (bool)dr.GetValue(dr.GetOrdinal("Cte_Efectivo"));
                    cte.Cte_Factoraje = (bool)dr.GetValue(dr.GetOrdinal("Cte_Factoraje"));
                    cte.Cte_Cheque = (bool)dr.GetValue(dr.GetOrdinal("Cte_Cheque"));
                    cte.Cte_Transferencia = (bool)dr.GetValue(dr.GetOrdinal("Cte_Transferencia"));
                    cte.Cte_ReqOrdenCompra = (bool)dr.GetValue(dr.GetOrdinal("Cte_ReqOrdenCompra"));
                    cte.Cte_Documentos = dr.IsDBNull(dr.GetOrdinal("Cte_Documentos")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_Documentos")).ToString();
                    cte.Ade_Nombre = dr.IsDBNull(dr.GetOrdinal("Ade_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("Ade_Nombre")).ToString();
                    cte.Cte_TelCobranza1 = dr.IsDBNull(dr.GetOrdinal("Cte_TelCobranza1")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_TelCobranza1")).ToString();
                    cte.Cte_TelCobranza2 = dr.IsDBNull(dr.GetOrdinal("Cte_TelCobranza2")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_TelCobranza2")).ToString();
                    cte.Cte_RemisionElectronica = dr.IsDBNull(dr.GetOrdinal("Cte_RemisionElectronica")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_RemisionElectronica")));
                    cte.BPorcNotaCredito = dr.IsDBNull(dr.GetOrdinal("Cte_NCredito")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_NCredito")));
                    cte.PorcientoNotaCredito = dr.IsDBNull(dr.GetOrdinal("Cte_PorcNCredito")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cte_PorcNCredito")));
                    cte.PorcientoRetencion = dr.IsDBNull(dr.GetOrdinal("Cte_PorcRetencion")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cte_PorcRetencion")));
                    cte.BPorcientoIVA = dr.IsDBNull(dr.GetOrdinal("Cte_BPorcientoIVA")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_BPorcientoIVA")));
                    cte.PorcientoIVA = dr.IsDBNull(dr.GetOrdinal("Cte_PorcientoIVA")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_PorcientoIVA")));
                    cte.Cte_UDigitos = dr.IsDBNull(dr.GetOrdinal("Cte_UDigitos")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_UDigitos")));
                    if (Convert.ToBoolean(cte.Estatus))
                        cte.EstatusStr = "Activo";
                    else
                        cte.EstatusStr = "Inactivo";
                }



                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaTipoCliente(Clientes cte, string Conexion, ref List<Comun> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_TCte" };
                object[] Valores = { cte.Id_Emp, cte.Id_Cd, cte.Id_TCte };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTCliente_Consulta", ref dr, Parametros, Valores);
                Comun cliente;
                while (dr.Read())
                {
                    cliente = new Comun();
                    cliente.IdStr = dr.GetValue(dr.GetOrdinal("Id_TCte")).ToString();
                    cliente.Descripcion = dr.GetValue(dr.GetOrdinal("TCte_Descripcion")).ToString();
                    cliente.ValorBool = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("TCte_ConCuentaCorporativa")));
                    cliente.ValorStr = dr["TCte_Autorizadores"].ToString();
                    List.Add(cliente);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCuentaNacional(int? idCuenta, string Conexion, ref List<Comun> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@IdCuenta" };
                object[] Valores = { idCuenta };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCuentaNacional_Consulta", ref dr, Parametros, Valores);
                Comun cliente;
                while (dr.Read())
                {
                    cliente = new Comun();
                    cliente.IdStr = dr.GetValue(dr.GetOrdinal("CliNum")).ToString();
                    cliente.Descripcion = dr.GetValue(dr.GetOrdinal("CliNom")).ToString();
                    cliente.ValorInt = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CtaCont")));
                    List.Add(cliente);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClienteCorrreos(int Id_Cd, int Id_Fac, ref Clientes cte, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
                                          "@Id_Cd",
                                          "@Id_Fac"
                                      };
                object[] Valores = {
                                       Id_Cd ,
                                       Id_Fac
                                   };
                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapFactura_ConsultaCliente", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    cte.Cte_NomComercial = dr["Cte_NomComercial"].ToString();
                    cte.Cte_Email = dr["Cte_Email"].ToString();
                }

                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaClienteCorrreosOtraBD(int Id_Emp, int Id_Cd, int Id_Fac, string serie, ref Clientes cte, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Fac",
                                          "@Serie"
                                      };
                object[] Valores = {
                                       Id_Emp,
                                       Id_Cd ,
                                       Id_Fac,
                                       serie
                                   };
                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapFactura_ConsultaClienteOtraDB", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    cte.Cte_NomComercial = dr["Cte_NomComercial"].ToString();
                    cte.Cte_Email = dr["Cte_Email"].ToString();
                }

                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ClienteConsultaNombre(int Id_Cte, ref string Cte_NomComercial, Sesion sesion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(sesion.Emp_Cnx);
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte"};
                object[] Valores = {   sesion .Id_Emp,
                                       sesion.Id_Cd_Ver,
                                       Id_Cte 
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatCliente_ConsultaNombre", ref dr, Parametros, Valores);

                if (dr.Read())
                {

                    Cte_NomComercial = dr["Cte_NomComercial"].ToString();
                }

                dr.Close();

                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void CatClienteCondPago(int Id_Emp, int Id_Cd_Ver, int Id_Cte, ref double DiasRotacion, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Cte"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Cd_Ver
                                       ,Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteCondPago", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    DiasRotacion = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("DiasRotacion")));
                }
                else
                {
                    DiasRotacion = 30;
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Versión que toma un contexto de conexión a la fuente de datos.
        /// </summary>
        /// <param name="Id_Emp"></param>
        /// <param name="Id_Cd_Ver"></param>
        /// <param name="Id_Cte"></param>
        /// <param name="DiasRotacion"></param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        public void CatClienteCondPago(int Id_Emp, int Id_Cd_Ver, int Id_Cte, ref double DiasRotacion, ICD_Contexto icdCtx)
        {
            try
            {
                SqlDataReader dr = null;

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Cte"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Cd_Ver
                                       ,Id_Cte
                                   };

                SqlCommand sqlcmd = CD_Datos.GenerarSqlCommand("spCatClienteCondPago", ref dr, Parametros, Valores, icdCtx);

                if (dr.Read())
                {
                    DiasRotacion = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("DiasRotacion")));
                }
                else
                {
                    DiasRotacion = 30;
                }
                dr.Close();
                sqlcmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ConsultaModalidadOP(string Conexion, ref List<TipoVenta> modOperList)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { };
                object[] Valores = { };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spComboModadlidadOp", ref dr, Parametros, Valores);

                TipoVenta modOp;

                while (dr.Read())
                {
                    modOp = new TipoVenta();
                    modOp.id = Convert.ToInt32(dr["id"]);
                    modOp.nombre = dr["nombre"].ToString();

                    modOperList.Add(modOp);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ClienteDetGarantia> ConsultarClienteTerr_EsGarantia(ClienteDet clientedet, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Cte"
                                          , "@Id_Ter"
                                      };
                object[] Valores = { 
                                       clientedet.Id_Emp
                                       ,clientedet.Id_Cd
                                       ,clientedet.Id_Cte
                                       ,clientedet.Id_Ter
                                   };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSelGarantias", ref dr, Parametros, Valores);

                List<ClienteDetGarantia> lista = new List<ClienteDetGarantia>();

                int mod = 0;
                while (dr.Read())
                {
                    var item = new ClienteDetGarantia();

                    item.Id_Cte = Convert.ToInt32(dr["Id_Cte"]);
                    item.Id_CteDet = Convert.ToInt32(dr["Id_CteDet"]);
                    item.id_TG = Convert.ToInt32(dr["id_TG"]);

                    lista.Add(item);
                }

                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CatCliente> ConsultarPorRFC(int idEmp, int idCd, string RFC, string conexionEF)
        {
            IEnumerable<CatCliente> result = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexionEF))
            {
                var ctes = (from c in ctx.CatClientes
                            where c.Id_Emp == idEmp && c.Id_Cd == idCd && c.Cte_Rfc == RFC
                            select c).ToList();
                result = ctes;
            }
            return result;
        }

        public List<CatCliente> Consultar_PorRFC(int idEmp, int idCd, string RFC, string conexion)
        {
            List<CatCliente> lst = new List<CatCliente>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Cte_RFC" };
                object[] Valores = { idEmp, idCd, RFC };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCliente_BuscarRFC", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CatCliente obj = new CatCliente();
                    obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    obj.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    obj.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    obj.Cte_Contacto = dr.GetValue(dr.GetOrdinal("Cte_Contacto")).ToString();
                    obj.Cte_Email = dr.GetValue(dr.GetOrdinal("Cte_Email")).ToString();
                    obj.Cte_FacCalle = dr.GetValue(dr.GetOrdinal("Cte_FacCalle")).ToString();
                    obj.Cte_Telefono = dr.GetValue(dr.GetOrdinal("Cte_Telefono")).ToString();
                    lst.Add(obj);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                lst = null;
            }
            return lst;
        }


        //
        // 8 Sep 2018

        public Clientes Consultar_PorId_Cte(int Id_Emp, int Id_Cd, int Id_Cte, string conexion)
        {
            Clientes obj = new Clientes();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte" };
                object[] Valores = { Id_Emp, Id_Cd, Id_Cte };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCliente_BuscarPorIdCte", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    obj.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    obj.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    obj.Cte_Contacto = dr.GetValue(dr.GetOrdinal("Cte_Contacto")).ToString();
                    obj.Cte_Email = dr.GetValue(dr.GetOrdinal("Cte_Email")).ToString();
                    obj.Cte_FacCalle = dr.GetValue(dr.GetOrdinal("Cte_FacCalle")).ToString();
                    obj.Cte_Telefono = dr.GetValue(dr.GetOrdinal("Cte_Telefono")).ToString();
                    obj.Cte_FacRfc = dr.GetValue(dr.GetOrdinal("Cte_FAcRfc")).ToString();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                obj = null;
            }
            return obj;
        }

        public IEnumerable<CatCliente> ConsultarPorRFC(int idEmp, int idCd, string RFC, ICD_Contexto icdCtx)
        {
            IEnumerable<CatCliente> result = null;
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var ctes = (from c in ctx.CatClientes
                        where c.Id_Emp == idEmp && c.Id_Cd == idCd && c.Cte_Rfc == RFC
                        select c).ToList();
            result = ctes;
            return result;
        }

        public IEnumerable<CatCliente> ConsultarPorNombre(int idEmp, int idCd, string nombre, string conexionEF)
        {
            IEnumerable<CatCliente> result = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(conexionEF))
            {
                var ctes = (from c in ctx.CatClientes
                            where c.Id_Emp == idEmp && c.Id_Cd == idCd && c.Cte_NomComercial == nombre
                            select c).ToList();
                result = ctes;
            }
            return result;
        }

        internal void EliminarCliente(int idEmp, int idCd, int idCte, DbTransaction tran, DbConnection connection, sianwebmty_gEntities outterCtx)
        {
            MetadataWorkspace mdw = ((IObjectContextAdapter)outterCtx).ObjectContext.MetadataWorkspace;
            EntityConnection ec = new EntityConnection(mdw, connection);
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(ec, false))
            {
                ctx.Database.UseTransaction(tran);
                var clientes = (from c in ctx.CatClientes
                                where c.Id_Emp == idEmp && c.Id_Cd == idCd && c.Id_Cte == idCte
                                select c).ToList();
                if (clientes.Count > 0)
                {
                    ctx.CatClientes.Remove(clientes[0]);
                    ctx.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Elimina un cliente ejecutando la consulta desde un contexto que ha sido inicializado asociándolo a una transacción. Este método no llama a SaveChanges() del contexto.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del Centro de Distribución</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="ctx">Contexto de acceso a datos</param>
        internal void EliminarCliente(int idEmp, int idCd, int idCte, sianwebmty_gEntities ctx)
        {
            var clientes = (from c in ctx.CatClientes
                            where c.Id_Emp == idEmp && c.Id_Cd == idCd && c.Id_Cte == idCte
                            select c).ToList();
            if (clientes.Count > 0)
            {
                ctx.CatClientes.Remove(clientes[0]);
            }
        }

        public IEnumerable<CatCliente> Consultar(int idEmp, int idCd, string terminoDeBusqueda, string cadenaConexionEF)
        {
            IEnumerable<CatCliente> resultado = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                var clientes = (from c in ctx.CatClientes
                                where c.Id_Emp == idEmp && c.Id_Cd == idCd && (SqlFunctions.PatIndex(terminoDeBusqueda, c.Cte_NomComercial) > 0 || SqlFunctions.PatIndex(terminoDeBusqueda, c.Cte_NomCorto) > 0 || SqlFunctions.PatIndex(terminoDeBusqueda, c.Cte_Rfc) > 0)
                                select c).ToList();
                if (clientes.Count > 0)
                {
                    resultado = clientes;
                }
                else
                {
                    resultado = new List<CatCliente>();
                }
            }
            return resultado;
        }

        /// <summary>
        /// Devuelve el resultado de la consulta al repositorio CatClientes, condicionando a que el [terminoBusqueda] se encuentre como patrón en Cte_NomComercial, Cte_NomCorto o Cte_Rfc.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="terminoDeBusqueda">Término de búsqueda</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns>IEnumerable<CatCliente></returns>
        public IEnumerable<CatCliente> Consultar(int idEmp, int idCd, string terminoDeBusqueda, ICD_Contexto icdCtx)
        {
            IEnumerable<CatCliente> resultado = null;
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var clientes = (from c in ctx.CatClientes
                            where c.Id_Emp == idEmp && c.Id_Cd == idCd && (SqlFunctions.PatIndex(terminoDeBusqueda, c.Cte_NomComercial) > 0 || SqlFunctions.PatIndex(terminoDeBusqueda, c.Cte_NomCorto) > 0 || SqlFunctions.PatIndex(terminoDeBusqueda, c.Cte_Rfc) > 0)
                            select c).ToList();
            if (clientes.Count > 0)
            {
                resultado = clientes;
            }
            else
            {
                resultado = new List<CatCliente>();
            }
            return resultado;
        }

        /// <summary>
        /// Actualiza el valor del atributo [Cte_Activo] del repositorio [CatCliente].
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa a la que pertenece el cliente idCte.</param>
        /// <param name="idCd">Identificador del centro de distribución al que pertenece el cliente idCte</param>
        /// <param name="idCte">Identificador del cliente al que interesa actualizar el valor del atributo [Cte_Activo]</param>
        /// <param name="val">Valor a actualizar del atributo [Cte_Activo]</param>
        /// <param name="cadenaConexionEF">Cadena de conexión a la fuente de datos con formato compatible con Entity Framework</param>
        public void ActualizarCampo_Cte_Activo(int idEmp, int idCd, int idCte, bool val, string cadenaConexionEF)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                var clientes = (from c in ctx.CatClientes
                                where c.Id_Emp == idEmp && c.Id_Cd == idCd && c.Id_Cte == idCte
                                select c).ToList();
                if (clientes.Count > 0)
                {
                    clientes[0].Cte_Activo = val;
                    ctx.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Actualiza el valor del atributo [Cte_Activo] del repositorio [CatCliente].
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa a la que pertenece el cliente idCte.</param>
        /// <param name="idCd">Identificador del centro de distribución al que pertenece el cliente idCte</param>
        /// <param name="idCte">Identificador del cliente al que interesa actualizar el valor del atributo [Cte_Activo]</param>
        /// <param name="val">Valor a actualizar del atributo [Cte_Activo]</param>
        /// <param name="idcCtx">Contexto de la conexión a la fuente de datos</param>
        public void ActualizarCampo_Cte_Activo(int idEmp, int idCd, int idCte, bool val, ICD_Contexto idcCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)idcCtx).Contexto;
            var clientes = (from c in ctx.CatClientes
                            where c.Id_Emp == idEmp && c.Id_Cd == idCd && c.Id_Cte == idCte
                            select c).ToList();
            if (clientes.Count > 0)
            {
                clientes[0].Cte_Activo = val;
            }
        }

        /// <summary>
        /// Regresa la consulta a la entidad [CatCliente] mediante el identificador del cliente.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa a la que pertenece el cliente</param>
        /// <param name="idCd">Identificador del centro de distribución a la que pertenece el cliente</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="cadenaConexionEF">Cadena de conexión a la fuente de datos con formato compatible con Entity Framework</param>
        /// <returns>CatCliente. Entidad de datos del repositorio [CatCliente]. </returns>
        public CatCliente ConsultarPorId(int idEmp, int idCd, int idCte, string cadenaConexionEF)
        {
            CatCliente resultado = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                var clientes = (from c in ctx.CatClientes
                                where c.Id_Emp == idEmp && c.Id_Cd == idCd && c.Id_Cte == idCte
                                select c).ToList();
                if (clientes.Count > 0)
                {
                    resultado = clientes[0];
                }
            }
            return resultado;
        }

        /// <summary>
        /// Regresa la consulta a la entidad [CatCliente] mediante el identificador del cliente.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa a la que pertenece el cliente</param>
        /// <param name="idCd">Identificador del centro de distribución a la que pertenece el cliente</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="cadenaConexionEF">Cadena de conexión a la fuente de datos con formato compatible con Entity Framework</param>
        /// <returns>CatCliente. Entidad de datos del repositorio [CatCliente]. </returns>
        public CatCliente ConsultarPorId(int idEmp, int idCd, int idCte, ICD_Contexto icdCtx)
        {
            CatCliente resultado = null;
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var clientes = (from c in ctx.CatClientes
                            where c.Id_Emp == idEmp && c.Id_Cd == idCd && c.Id_Cte == idCte
                            select c).ToList();
            if (clientes.Count > 0)
            {
                resultado = clientes[0];
            }
            return resultado;
        }

        //RBM 
        //Actualiza CatClienteDet el campo de Fec_Solicitud y las garantias
        //inicio
        public void UpdateClientesDet(Clientes clientes, DataTable dt, string Conexion)
        {
            if (dt.Rows.Count == 0) return;

            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            int verificador = 0;
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                     "@Id_Emp"
	                                    ,"@Id_Cd" 
	                                    ,"@Id_Cte" 
                                        ,"@Id_Ter" 
                                        ,"@Fec_Solicitud"
                                      };

                object[] Valores = null;
                SqlCommand sqlcmd = null;

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    Valores = new object[] { 
                                         clientes.Id_Emp
                                        ,clientes.Id_Cd
                                        ,clientes.Id_Cte
                                        ,dt.Rows[x]["Id_Ter"]
                                        ,dt.Rows[x]["Fec_Solicitud"]
                                        
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteDet_Actualizar", ref verificador, Parametros, Valores);
                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void UpdateClientesDet(Clientes clientes, DataTable dt, string Conexion, DataTable catClienteDet, DataTable catClienteDetGarantia, string efConexion)
        {
            if (dt.Rows.Count == 0) return;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            CD_CatClienteDetGarantia garantiasCD = new CD_CatClienteDetGarantia(efConexion);
            int verificador = 0;
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { 
	                                     "@Id_Emp"
	                                    ,"@Id_Cd" 
	                                    ,"@Id_Cte" 
                                        ,"@Id_CteDet"
                                        ,"@Id_Ter" 
                                        ,"@Fec_Solicitud"
                                        ,"@Accion"
                                        ,"@Cte_Tradicional"
                                        ,"@Cte_Garantia"
                                      };

                object[] Valores = null;
                SqlCommand sqlcmd = null;

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    var tradicionales = (from dr in catClienteDet.AsEnumerable()
                                         where dr["Id_CteDet"].ToString().CompareTo(dt.Rows[x]["Id_CteDet"].ToString()) == 0
                                         select dr).ToList();
                    int tradicional = 0;
                    if (tradicionales.Count > 0)
                    {
                        if (tradicionales[0]["Tradicional"] != null)
                        {
                            try
                            {
                                tradicional = int.Parse(tradicionales[0]["Tradicional"].ToString());
                            }
                            catch (Exception ex)
                            {
                                if (tradicionales[0]["Tradicional"].ToString().CompareTo("False") == 0)
                                {
                                    tradicional = 0;
                                }
                                else if (tradicionales[0]["Tradicional"].ToString().CompareTo("True") == 0)
                                {
                                    tradicional = 1;
                                }
                            }
                        }

                    }

                    int garantia = 0;
                    if (tradicionales.Count > 0)
                    {
                        if (tradicionales[0]["Garantia"] != null)
                        {
                            try
                            {
                                garantia = int.Parse(tradicionales[0]["Garantia"].ToString());
                            }
                            catch (Exception ex)
                            {
                                if (tradicionales[0]["Garantia"].ToString().CompareTo("False") == 0)
                                {
                                    garantia = 0;
                                }
                                else if (tradicionales[0]["Garantia"].ToString().CompareTo("True") == 0)
                                {
                                    garantia = 1;
                                }
                            }
                        }

                    }
                    Valores = new object[] { 
                                         clientes.Id_Emp
                                        ,clientes.Id_Cd
                                        ,clientes.Id_Cte
                                        ,x
                                        ,dt.Rows[x]["Id_Ter"]
                                        ,DateTime.Now //dt.Rows[x]["Fec_Solicitud"]
                                        ,x
                                        ,tradicional
                                        ,garantia
                                        
                                   };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteDet_Actualizar_Exp", ref verificador, Parametros, Valores);
                    int id_cteDetGenerada = (int)verificador;
                    var garantias = (from dr in catClienteDetGarantia.AsEnumerable()
                                     where dr["Id_CteDet"].ToString().CompareTo(dt.Rows[x]["Id_CteDet"].ToString()) == 0
                                     select dr).ToList();
                    garantiasCD.Eliminar(clientes.Id_Emp, clientes.Id_Cd, clientes.Id_Cte, id_cteDetGenerada, CapaDatos.CurrentTransaction, CapaDatos.CurrentConnection);
                    foreach (var dr in garantias)
                    {
                        garantiasCD.Insertar(clientes.Id_Emp, clientes.Id_Cd, clientes.Id_Cte, id_cteDetGenerada, int.Parse(dr["Id_TG"].ToString()), CapaDatos.CurrentTransaction, CapaDatos.CurrentConnection);
                    }
                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        //fin


    }

}
