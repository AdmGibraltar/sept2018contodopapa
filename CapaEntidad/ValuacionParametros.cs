using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ValuacionParametros
    {
        int _Id_Emp;

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        int _Id_Cd;

        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        int _Id_Vap;

        public int Id_Vap
        {
            get { return _Id_Vap; }
            set { _Id_Vap = value; }
        }
        int _Vap_Vigencia;

        public int Vap_Vigencia
        {
            get { return _Vap_Vigencia; }
            set { _Vap_Vigencia = value; }
        }
        double _Vap_Participacion;

        public double Vap_Participacion
        {
            get { return _Vap_Participacion; }
            set { _Vap_Participacion = value; }
        }
        double _Vap_Mano_Obra;

        public double Vap_Mano_Obra
        {
            get { return _Vap_Mano_Obra; }
            set { _Vap_Mano_Obra = value; }
        }
        double _Vap_Amortizacion;

        public double Vap_Amortizacion
        {
            get { return _Vap_Amortizacion; }
            set { _Vap_Amortizacion = value; }
        }
        int _Vap_Numero_Entregas;

        public int Vap_Numero_Entregas
        {
            get { return _Vap_Numero_Entregas; }
            set { _Vap_Numero_Entregas = value; }
        }
        double _Vap_Costo_Entregas;

        public double Vap_Costo_Entregas
        {
            get { return _Vap_Costo_Entregas; }
            set { _Vap_Costo_Entregas = value; }
        }
        double _Vap_Comision_Factoraje;

        public double Vap_Comision_Factoraje
        {
            get { return _Vap_Comision_Factoraje; }
            set { _Vap_Comision_Factoraje = value; }
        }
        double _Vap_Comision_Anden;

        public double Vap_Comision_Anden
        {
            get { return _Vap_Comision_Anden; }
            set { _Vap_Comision_Anden = value; }
        }
        double _Vap_Gasto_Flete_Locales;

        public double Vap_Gasto_Flete_Locales
        {
            get { return _Vap_Gasto_Flete_Locales; }
            set { _Vap_Gasto_Flete_Locales = value; }
        }
        double _Vap_IVA;

        public double Vap_IVA
        {
            get { return _Vap_IVA; }
            set { _Vap_IVA = value; }
        }
        double _Vap_Plazo_Pago_Cliente;

        public double Vap_Plazo_Pago_Cliente
        {
            get { return _Vap_Plazo_Pago_Cliente; }
            set { _Vap_Plazo_Pago_Cliente = value; }
        }
        int _Vap_Inventario_Key;

        public int Vap_Inventario_Key
        {
            get { return _Vap_Inventario_Key; }
            set { _Vap_Inventario_Key = value; }
        }
        int _Vap_Inventario_Consignacion;

        public int Vap_Inventario_Consignacion
        {
            get { return _Vap_Inventario_Consignacion; }
            set { _Vap_Inventario_Consignacion = value; }
        }
        int _Vap_Inventario_Papel;

        public int Vap_Inventario_Papel
        {
            get { return _Vap_Inventario_Papel; }
            set { _Vap_Inventario_Papel = value; }
        }
        int _Vap_Consignacion_Papel;

        public int Vap_Consignacion_Papel
        {
            get { return _Vap_Consignacion_Papel; }
            set { _Vap_Consignacion_Papel = value; }
        }
        int _Vap_Credito_Key;

        public int Vap_Credito_Key
        {
            get { return _Vap_Credito_Key; }
            set { _Vap_Credito_Key = value; }
        }
        int _Vap_Credito_Papel;

        public int Vap_Credito_Papel
        {
            get { return _Vap_Credito_Papel; }
            set { _Vap_Credito_Papel = value; }
        }
        double _Vap_ISR;

        public double Vap_ISR
        {
            get { return _Vap_ISR; }
            set { _Vap_ISR = value; }
        }
        double _Vap_Ucs;

        public double Vap_Ucs
        {
            get { return _Vap_Ucs; }
            set { _Vap_Ucs = value; }
        }
        double _Vap_Cetes;

        public double Vap_Cetes
        {
            get { return _Vap_Cetes; }
            set { _Vap_Cetes = value; }
        }
        double _Vap_Adicional_Cetes;

        public double Vap_Adicional_Cetes
        {
            get { return _Vap_Adicional_Cetes; }
            set { _Vap_Adicional_Cetes = value; }
        }

        double _Vap_Costos_Fijos_No_Papel;

        public double Vap_Costos_Fijos_No_Papel
        {
            get { return _Vap_Costos_Fijos_No_Papel; }
            set { _Vap_Costos_Fijos_No_Papel = value; }
        }
        double _Vap_Costos_Fijos_Papel;

        public double Vap_Costos_Fijos_Papel
        {
            get { return _Vap_Costos_Fijos_Papel; }
            set { _Vap_Costos_Fijos_Papel = value; }
        }
        double _Vap_Gastos_Admin;

        public double Vap_Gastos_Admin
        {
            get { return _Vap_Gastos_Admin; }
            set { _Vap_Gastos_Admin = value; }
        }

        int _Vap_Inversion_Activos;

        public int Vap_Inversion_Activos
        {
            get { return _Vap_Inversion_Activos; }
            set { _Vap_Inversion_Activos = value; }
        }

        double _Cd_ComisionRik;

        public double Cd_ComisionRik
        {
            get { return _Cd_ComisionRik; }
            set { _Cd_ComisionRik = value; }
        }

        double _Cd_FactorConvActFijo;

        public double Cd_FactorConvActFijo
        {
            get { return _Cd_FactorConvActFijo; }
            set { _Cd_FactorConvActFijo = value; }
        }

        double _Cd_DiasFinanciaProv;
        public double Cd_DiasFinanciaProv
        {
            get { return _Cd_DiasFinanciaProv; }
            set { _Cd_DiasFinanciaProv = value; }
        }

        double _Cd_TasaIncCostoCapital;
        public double Cd_TasaIncCostoCapital
        {
            get { return _Cd_TasaIncCostoCapital; }
            set { _Cd_TasaIncCostoCapital = value; }
        }

        double _txtGastosVarAplTerr;
        public double txtGastosVarAplTerr
        {
            get { return _txtGastosVarAplTerr; }
            set { _txtGastosVarAplTerr = value; }
        }

        double _txtFletesPagadosalCliente;
        public double txtFletesPagadosalCliente
        {
            get { return _txtFletesPagadosalCliente; }
            set { _txtFletesPagadosalCliente = value; }
        }


    }
}
