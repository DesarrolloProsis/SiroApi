using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Clases
{
    public class GraficasMetodos
    {

        Context db = new Context();
        PlazasMetodos PlazasMetodos = new PlazasMetodos();
        TramosMetodos TramosMetodos = new TramosMetodos();
        ConcentradoMetodos ConcentradosMetodos = new ConcentradoMetodos();


        public object GraficaFull(DateTime FechaInicio, DateTime FechaFin)
        {
            var Plazas = PlazasMetodos.GetPlazasyNumeros();
            string[] columns = { "nombrePlaza", "totalCruces" };

            List<GraficaCruce> Lista = new List<GraficaCruce>();

            foreach (var item in Plazas) {


                if (FechaInicio == FechaFin)
                {
                    Lista.Add(new GraficaCruce
                    {
                        NombrePlaza = item.NombrePlaza,
                        TotalCruces = ConcentradosMetodos.GetCrucesCount(item.NumeroPlaza, FechaInicio)

                    });

                }
                else
                {
                    Lista.Add(new GraficaCruce
                    {
                        NombrePlaza = item.NombrePlaza,
                        TotalCruces = ConcentradosMetodos.GetCrucesCount(item.NumeroPlaza, FechaInicio, FechaFin)

                    });

                }
            }

            object Json = new { columns, Lista };
            return Json;
        }

        ///<summary>
        /// Obtiene un Objeto para la Grafica de Tramos Todas las Plazas     
        /// </summary>
        public object GraficaTramos(string nombrePlaza, DateTime FechaInicio, DateTime FechaFin)
        {

            var Plazas = PlazasMetodos.ConvertNombrePlaza(nombrePlaza);
            List<GraficaTramoCruce> Lista = new List<GraficaTramoCruce>();
            string[] colns = { "nombrePlaza", "cuerpoA", "cuerpoB", "cuerpoC", "cuerpoD" };
            List<string> columns = new List<string>();


            
                var Tramos = TramosMetodos.GetTramos(Plazas);
                int[] Cuerpos = new int[4];
                for (int i = 0; i < Tramos.Count(); i++)
                {
                    if (FechaInicio == FechaFin) Cuerpos[i] = ConcentradosMetodos.GetCrucesTramo(Tramos[i].IdGare, FechaInicio);
                    else Cuerpos[i] = ConcentradosMetodos.GetCrucesTramo(Tramos[i].IdGare, FechaInicio, FechaFin);
                }


                int CuerpoA = Cuerpos[0];
                int CuerpoB = Cuerpos[1];
                int CuerpoC = Cuerpos[2];
                int CuerpoD = Cuerpos[3];


                Lista.Add(new GraficaTramoCruce
                {

                    NombrePlaza = nombrePlaza,
                    CuerpoA = CuerpoA,
                    CuerpoB = CuerpoB,
                    CuerpoC = CuerpoC,
                    CuerpoD = CuerpoD

                });

            columns.Add("nombrePlaza");
            if (Lista[0].CuerpoA > 0) columns.Add("cuerpoA");
            if (Lista[0].CuerpoB > 0) columns.Add("cuerpoB");
            if (Lista[0].CuerpoC > 0) columns.Add("cuerpoC");
            if (Lista[0].CuerpoD > 0) columns.Add("cuerpoD");

            
            object Json = new { columns, Lista };

            return Json;

        }
        public object GraficaTramosFull(DateTime FechaInicio, DateTime FechaFin)
        {

            var Plazas = PlazasMetodos.GetPlazasyNumeros();
            List<GraficaTramoCruce> Lista = new List<GraficaTramoCruce>();
            string[] columns = { "nombrePlaza", "cuerpoA", "cuerpoB", "cuerpoC", "cuerpoD" };


            foreach (var item in Plazas)
            {


                var Tramos = TramosMetodos.GetTramos(item.NumeroPlaza);
                int[] Cuerpos = new int[4];
                for (int i = 0; i < Tramos.Count(); i++)
                {
                    if (FechaInicio == FechaFin) Cuerpos[i] = ConcentradosMetodos.GetCrucesTramo(Tramos[i].IdGare, FechaInicio);
                    else Cuerpos[i] = ConcentradosMetodos.GetCrucesTramo(Tramos[i].IdGare, FechaInicio, FechaFin);
                }


                int CuerpoA = Cuerpos[0];
                int CuerpoB = Cuerpos[1];
                int CuerpoC = Cuerpos[2];
                int CuerpoD = Cuerpos[3];


                Lista.Add(new GraficaTramoCruce
                {

                    NombrePlaza = item.NombrePlaza,
                    CuerpoA = CuerpoA,
                    CuerpoB = CuerpoB,
                    CuerpoC = CuerpoC,
                    CuerpoD = CuerpoD

                });

            }
            object Json = new { columns, Lista };

            return Json;

        }




        public object GraficaTurnosFull(DateTime FechaInicio, DateTime FechaFin)
        {

            var Plazas = PlazasMetodos.GetPlazasyNumeros();

            var Turnos = db.Turnos.Where(x => x.TurnoId != 0).ToList();


            List<GraficaTurnoCruce> Lista = new List<GraficaTurnoCruce>();
            string NombrePlaza = string.Empty;
            string NombreTramo = string.Empty;
            string[] columns = new[] { "nombrePlaza", "turnoMatutino", "turnoVespertino", "turnoNocturno" };
            int[] CrucesTurno = new int[4];




            foreach (var item in Plazas)
            {
                for (int i = 1; i <= 3; i++)
                {

                    int AcumuladorCruces = 0;

                    if (FechaInicio == FechaFin)
                    { AcumuladorCruces += ConcentradosMetodos.GetCrucesTurnosCount(item.NumeroPlaza, i, FechaInicio); }
                    if (FechaInicio != FechaFin)
                    { AcumuladorCruces += ConcentradosMetodos.GetCrucesTurnosCount(item.NumeroPlaza, i, FechaInicio, FechaFin); }

                    CrucesTurno[i] = AcumuladorCruces;

                }

                Lista.Add(new GraficaTurnoCruce
                {
                    NombrePlaza = item.NombrePlaza,
                    TurnoMatutino = CrucesTurno[1],
                    TurnoVespertino = CrucesTurno[2],
                    TurnoNocturno = CrucesTurno[3]

                });


            }

            object json = new { Lista, columns };

            return json;
        }

        public object GraficaTurnos(string nombrePlaza, DateTime FechaInicio, DateTime FechaFin)
        {

            var NumeroPlazas = PlazasMetodos.ConvertNombrePlaza(nombrePlaza);

            var Turnos = db.Turnos.Where(x => x.TurnoId != 0).ToList();


            List<GraficaTurnoCruce> Lista = new List<GraficaTurnoCruce>();                 
            string[] columns = new[] { "nombrePlaza", "turnoMatutino", "turnoVespertino", "turnoNocturno" };
            int[] CrucesTurno = new int[4];




                for (int i = 1; i <= 3; i++)
                {

                    int AcumuladorCruces = 0;

                    if (FechaInicio == FechaFin)
                    { AcumuladorCruces += ConcentradosMetodos.GetCrucesTurnosCount(NumeroPlazas, i, FechaInicio); }
                    if (FechaInicio != FechaFin)
                    { AcumuladorCruces += ConcentradosMetodos.GetCrucesTurnosCount(NumeroPlazas, i, FechaInicio, FechaFin); }

                    CrucesTurno[i] = AcumuladorCruces;

                }

                Lista.Add(new GraficaTurnoCruce
                {
                    NombrePlaza = nombrePlaza,
                    TurnoMatutino = CrucesTurno[1],
                    TurnoVespertino = CrucesTurno[2],
                    TurnoNocturno = CrucesTurno[3]

                });



            object json = new { Lista, columns };

            return json;
        }

        ///<summary>
        /// Obtiene un Objeto para la Grafica de Tramos Todas las Plazas     
        /// </summary>
        public object GraficaTramosPlaza(string Plaza, DateTime FechaInicio, DateTime FechaFin)
        {

            var Plazas = TramosMetodos.GetTramos(Plaza);
            List<GraficaTramoCruce> Lista = new List<GraficaTramoCruce>();
            string[] columns = { "cuerpoA", "CuerpoB", "CuerpoC", "CuerpoD" };


            foreach (var item in Plazas)
            {

                int[] Cuerpos = new int[4];

                for (int i = 0; i < Cuerpos.Count(); i++)
                {
                    Cuerpos[i] = ConcentradosMetodos.GetCrucesCount(item.NumeroPlaza, FechaInicio, FechaFin);
                }

                int CuerpoA = Cuerpos[0];
                int CuerpoB = Cuerpos[1];
                int CuerpoC = Cuerpos[2];
                int CuerpoD = Cuerpos[3];


                Lista.Add(new GraficaTramoCruce
                {

                    NombrePlaza = Plaza,
                    CuerpoA = CuerpoA,
                    CuerpoB = CuerpoB,
                    CuerpoC = CuerpoC,
                    CuerpoD = CuerpoD

                });

            }
            object Json = new { columns, Lista };

            return Json;

        }

        public object GraficaTramoTypePagoFull(string nombrePlaza, DateTime FechaInicio, DateTime FechaFin)
        {
            var Plazas = PlazasMetodos.ConvertNombrePlaza(nombrePlaza);
            var TypePagos = db.TipoPago.ToList();
            List<string> columns = new List<string>();
            List<GraficasTypePago> Lista = new List<GraficasTypePago>();
            //List<string> columns = new List<string>();

            if(FechaInicio == FechaFin)
            {
                Lista.Add(new GraficasTypePago
                {
                    NombrePlaza = nombrePlaza,
                    NoPago = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 0, FechaInicio),
                    Efectivo = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 1, FechaInicio),
                    EfectivoCRE = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 2, FechaInicio),
                    Valores = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 9, FechaInicio),
                    Residente = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 10, FechaInicio),
                    TarjetadeCredito = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 12, FechaInicio),
                    Violacion = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 13, FechaInicio),
                    TarjetadeDebito = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 14, FechaInicio),
                    PrepagoTag = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 15, FechaInicio),
                    ExentoVSC = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 27, FechaInicio),
                    ResidenteRP1 = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 71, FechaInicio),
                    ResidenteRP2 = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 72, FechaInicio),
                    ResidenteRP3 = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 73, FechaInicio),
                    ResidenteRP4 = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 74, FechaInicio),
                    ResidenteRP4_ = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 75, FechaInicio),
                });

            }
            else
            {
                Lista.Add(new GraficasTypePago
                {
                    NombrePlaza = nombrePlaza,
                    NoPago = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 0, FechaInicio, FechaFin),
                    Efectivo = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 1, FechaInicio, FechaFin),
                    EfectivoCRE = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 2, FechaInicio, FechaFin),
                    Valores = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 9, FechaInicio, FechaFin),
                    Residente = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 10, FechaInicio, FechaFin),
                    TarjetadeCredito = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 12, FechaInicio, FechaFin),
                    Violacion = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 13, FechaInicio, FechaFin),
                    TarjetadeDebito = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 14, FechaInicio, FechaFin),
                    PrepagoTag = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 15, FechaInicio, FechaFin),
                    ExentoVSC = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 27, FechaInicio, FechaFin),
                    ResidenteRP1 = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 71, FechaInicio, FechaFin),
                    ResidenteRP2 = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 72, FechaInicio, FechaFin),
                    ResidenteRP3 = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 73, FechaInicio, FechaFin),
                    ResidenteRP4 = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 74, FechaInicio, FechaFin),
                    ResidenteRP4_ = ConcentradosMetodos.GetCrucesTypePagoCount(Plazas, 75, FechaInicio, FechaFin),
                });

            }

             columns.Add("nombrePlaza");
                if (Lista[0].NoPago != 0)
                    columns.Add("noPago");
                if (Lista[0].Efectivo != 0)
                    columns.Add("efectivo");
                if (Lista[0].EfectivoCRE != 0)
                    columns.Add("efectivoCRE");
                if (Lista[0].Valores != 0)
                    columns.Add("valores");
                if (Lista[0].Residente != 0)
                    columns.Add("residente");
                if (Lista[0].TarjetadeCredito != 0)
                    columns.Add("tarjetadeCredito");
                if (Lista[0].Violacion != 0)
                    columns.Add("violacion");
                if (Lista[0].TarjetadeDebito != 0)
                    columns.Add("tarjetadeDebito");
                if (Lista[0].PrepagoTag != 0)
                    columns.Add("prepagoTag");
                if (Lista[0].ExentoVSC != 0)
                    columns.Add("exentoVSC");
                if (Lista[0].ResidenteRP1 != 0)
                    columns.Add("residenteRP1");
                if (Lista[0].ResidenteRP2 != 0)
                    columns.Add("residenteRP2");
                if (Lista[0].ResidenteRP3 != 0)
                    columns.Add("residenteRP3");
                if (Lista[0].ResidenteRP4 != 0)
                    columns.Add("residenteRP4");
                if (Lista[0].ResidenteRP4_ != 0)
                    columns.Add("residenteRP4_");
        

            object json = new { Lista, columns };

            return json;
        }
        public object GraficaTramoTypeVehiculoFull(string nombrePlaza, DateTime FechaInicio, DateTime FechaFin)
        {
            var Plazas = PlazasMetodos.ConvertNombrePlaza(nombrePlaza);
            var TypePagos = db.TipoPago.ToList();
            List<string> columns = new List<string>();
            List<GraficasTypeVehiculo> Lista = new List<GraficasTypeVehiculo>();
            //List<string> columns = new List<string>();

            if (FechaInicio == FechaFin)
            {
                Lista.Add(new GraficasTypeVehiculo
                {
                    nombrePlaza = nombrePlaza,
                    SinID = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 0, FechaInicio),
                    T01A = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 1, FechaInicio),
                    T02C = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 2, FechaInicio),
                    T03C = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 3, FechaInicio),
                    T04C = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 4, FechaInicio),
                    T05C = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 5, FechaInicio),
                    T06C = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 6, FechaInicio),
                    T07C = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 7, FechaInicio),
                    T08C = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 8, FechaInicio),
                    T09C = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 9, FechaInicio),
                    TL01A = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 10, FechaInicio),
                    TL02A = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 11, FechaInicio),
                    T02B = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 12, FechaInicio),
                    T03B = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 13, FechaInicio),
                    T04B = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 14, FechaInicio),
                    T01M = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 15, FechaInicio),
                    TPnnC = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 16, FechaInicio),
                    TLnnA = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 17, FechaInicio),
                    T01T = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 18, FechaInicio),
                    T01P = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 19, FechaInicio),

                });

            }
            else
            {
                Lista.Add(new GraficasTypeVehiculo
                {
                    nombrePlaza = nombrePlaza,
                    SinID = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 0, FechaInicio,FechaFin),
                    T01A = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 1, FechaInicio, FechaFin),
                    T02C = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 2, FechaInicio, FechaFin),
                    T03C = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 3, FechaInicio, FechaFin),
                    T04C = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 4, FechaInicio, FechaFin),
                    T05C = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 5, FechaInicio, FechaFin),
                    T06C = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 6, FechaInicio, FechaFin),
                    T07C = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 7, FechaInicio, FechaFin),
                    T08C = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 8, FechaInicio, FechaFin),                   
                    T09C = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 9, FechaInicio, FechaFin),
                    TL01A = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 10, FechaInicio, FechaFin),
                    TL02A = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 11, FechaInicio, FechaFin),
                    T02B = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 12, FechaInicio, FechaFin),
                    T03B = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 13, FechaInicio, FechaFin),
                    T04B = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 14, FechaInicio, FechaFin),
                    T01M = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 15, FechaInicio, FechaFin),
                    TPnnC = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 16, FechaInicio, FechaFin),
                    TLnnA = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 17, FechaInicio, FechaFin),
                    T01T = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 18, FechaInicio, FechaFin),
                    T01P = ConcentradosMetodos.GetCrucesTypeVehiculoCount(Plazas, 19, FechaInicio, FechaFin),

                });

            }

            columns.Add("nombrePlaza");
            if (Lista[0].SinID != 0)
                columns.Add("sinID");
            if (Lista[0].T01A != 0)
                columns.Add("t01A");
            if (Lista[0].T02C != 0)
                columns.Add("t02C");
            if (Lista[0].T03C != 0)
                columns.Add("t03C");
            if (Lista[0].T04C != 0)
                columns.Add("t04C");
            if (Lista[0].T05C != 0)
                columns.Add("t05C");
            if (Lista[0].T06C != 0)
                columns.Add("t06C");
            if (Lista[0].T07C != 0)
                columns.Add("t07C");
            if (Lista[0].T08C != 0)
                columns.Add("t08C");
            if (Lista[0].T09C != 0)
                columns.Add("t09C");
            if (Lista[0].TL01A != 0)
                columns.Add("tL01A");
            if (Lista[0].TL02A != 0)
                columns.Add("tL02A");
            if (Lista[0].T02B != 0)
                columns.Add("t02B");
            if (Lista[0].T03B != 0)
                columns.Add("t03B");
            if (Lista[0].T04B != 0)
                columns.Add("t04B");
            if (Lista[0].T01M != 0)
                columns.Add("t01M");
            if (Lista[0].TPnnC != 0)
                columns.Add("tPnnC");
            if (Lista[0].TLnnA != 0)
                columns.Add("tLnnA");
            if (Lista[0].T01T != 0)
                columns.Add("t01T");
            if (Lista[0].T01P != 0)                    
                columns.Add("t01P");

            object json = new { Lista, columns };

            return json;
        }
        private class GraficaCruce
        {
            public string NombrePlaza { get; set; }
            public int TotalCruces { get; set; }
        }
        private class GraficaTramoCruce
        {
            public string NombrePlaza { get; set; }
            public int CuerpoA { get; set; }
            public int CuerpoB { get; set; }
            public int CuerpoC { get; set; }
            public int CuerpoD { get; set; }

        }
        private class GraficaTurnoCruce
        {
            public string NombrePlaza { get; set; }
            public int TurnoMatutino { get; set; }
            public int TurnoVespertino { get; set; }
            public int TurnoNocturno { get; set; }

        }
        private class GraficasTypePago
        {
            public string NombrePlaza { get; set; }
            public int NoPago { get; set; }
            public int Efectivo { get; set; }
            public int EfectivoCRE { get; set; }
            public int Valores { get; set; }
            public int Residente { get; set; }
            public int TarjetadeCredito { get; set; }
            public int Violacion { get; set; }
            public int TarjetadeDebito { get; set; }
            public int PrepagoTag { get; set; }
            public int ExentoVSC { get; set; }
            public int ResidenteRP1 { get; set; }
            public int ResidenteRP2 { get; set; }
            public int ResidenteRP3 { get; set; }
            public int ResidenteRP4 { get; set; }
            public int ResidenteRP4_ { get; set; }


        }
        private class GraficasTypeVehiculo
        {
            public string nombrePlaza { get; set; }
            public int SinID { get; set; }
            public int T01A { get; set; }
            public int T02C { get; set; }
            public int T03C { get; set; }
            public int T04C { get; set; }
            public int T05C { get; set; }
            public int T06C { get; set; }
            public int T07C { get; set; }
            public int T08C { get; set; }
            public int T09C { get; set; }
            public int TL01A { get; set; }
            public int TL02A { get; set; }
            public int T02B { get; set; }
            public int T03B { get; set; }
            public int T04B { get; set; }
            public int T01M { get; set; }
            public int TPnnC { get; set; }
            public int TLnnA { get; set; }
            public int T01T { get; set; }
            public int T01P { get; set; }
        }


    }
 
}
