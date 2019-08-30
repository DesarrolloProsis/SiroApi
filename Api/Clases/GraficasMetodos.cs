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

        public object GraficasTurnosTypePago(string Plaza, DateTime FechaInicio, DateTime FechaFin)
        {

            var NumeroPlaza = PlazasMetodos.ConvertNombrePlaza(Plaza);

            List<GraficasTypePagoT> Lista = new List<GraficasTypePagoT>();
            List<string> columnsOld = new List<string>();
            string[] NombreTurnos = new[] { "turnoMatutino", "turnoVespertino", "turnoNocturno" };


            if (FechaInicio == FechaFin)
            {


                for (int i = 1; i <= 3; i++)
                {

                    Lista.Add(new GraficasTypePagoT
                    {
                        Tramo = NombreTurnos[i-1],
                        NoPago = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 0, i, FechaInicio),
                        Efectivo = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 1, i, FechaInicio),
                        EfectivoCRE = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 2, i, FechaInicio),
                        Valores = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 9, i, FechaInicio),
                        Residente = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 10, i, FechaInicio),
                        TarjetadeCredito = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 12, i, FechaInicio),
                        Violacion = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 13, i, FechaInicio),
                        TarjetadeDebito = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 14, i, FechaInicio),
                        PrepagoTag = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 15, i, FechaInicio),
                        ExentoVSC = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 27, i, FechaInicio),
                        ResidenteRP1 = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 71, i, FechaInicio),
                        ResidenteRP2 = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 72, i, FechaInicio),
                        ResidenteRP3 = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 73, i, FechaInicio),
                        ResidenteRP4 = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 74, i, FechaInicio),
                        ResidenteRP4_ = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 75, i, FechaInicio),
                    });
                }


            }
            else
            {

                for (int i = 1; i <= 3; i++)
                {
                    Lista.Add(new GraficasTypePagoT
                    {
                        Tramo = NombreTurnos[i-1],
                        NoPago = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 0, i, FechaInicio, FechaFin),
                        Efectivo = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 1, i, FechaInicio, FechaFin),
                        EfectivoCRE = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 2, i, FechaInicio, FechaFin),
                        Valores = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 9, i, FechaInicio, FechaFin),
                        Residente = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 10, i, FechaInicio, FechaFin),
                        TarjetadeCredito = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 12, i, FechaInicio, FechaFin),
                        Violacion = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 13, i, FechaInicio, FechaFin),
                        TarjetadeDebito = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 14, i, FechaInicio, FechaFin),
                        PrepagoTag = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 15, i, FechaInicio, FechaFin),
                        ExentoVSC = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 27, i, FechaInicio, FechaFin),
                        ResidenteRP1 = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 71, i, FechaInicio, FechaFin),
                        ResidenteRP2 = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 72, i, FechaInicio, FechaFin),
                        ResidenteRP3 = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 73, i, FechaInicio, FechaFin),
                        ResidenteRP4 = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 74, i, FechaInicio, FechaFin),
                        ResidenteRP4_ = ConcentradosMetodos.GetCrucesTurnosTypePago(NumeroPlaza, 75, i,  FechaInicio, FechaFin),
                    });

                }
            }


            columnsOld.Add("tramo");

            foreach (var item in Lista)
            {

                if (item.NoPago != 0)
                    columnsOld.Add("noPago");
                if (item.Efectivo != 0)
                    columnsOld.Add("efectivo");
                if (item.EfectivoCRE != 0)
                    columnsOld.Add("efectivoCRE");
                if (item.Valores != 0)
                    columnsOld.Add("valores");
                if (item.Residente != 0)
                    columnsOld.Add("residente");
                if (item.TarjetadeCredito != 0)
                    columnsOld.Add("tarjetadeCredito");
                if (item.Violacion != 0)
                    columnsOld.Add("violacion");
                if (item.TarjetadeDebito != 0)
                    columnsOld.Add("tarjetadeDebito");
                if (item.PrepagoTag != 0)
                    columnsOld.Add("prepagoTag");
                if (item.ExentoVSC != 0)
                    columnsOld.Add("exentoVSC");
                if (item.ResidenteRP1 != 0)
                    columnsOld.Add("residenteRP1");
                if (item.ResidenteRP2 != 0)
                    columnsOld.Add("residenteRP2");
                if (item.ResidenteRP3 != 0)
                    columnsOld.Add("residenteRP3");
                if (item.ResidenteRP4 != 0)
                    columnsOld.Add("residenteRP4");
                if (item.ResidenteRP4_ != 0)
                    columnsOld.Add("residenteRP4_");
            }

            var columns = columnsOld.Distinct();

            object json = new { Lista, columns };

            return json;


     
        }

        public object GraficasTurnosTypeVehiculo(string Plaza, DateTime FechaInicio, DateTime FechaFin)
        {

            var NumeroPlaza = PlazasMetodos.ConvertNombrePlaza(Plaza);

            List<GraficasTypeVehiculoT> Lista = new List<GraficasTypeVehiculoT>();
            List<string> columnsOld = new List<string>();
            string[] NombreTurnos = new[] { "turnoMatutino", "turnoVespertino", "turnoNocturno" };


            if (FechaInicio == FechaFin)
            {


                for (int i = 1; i <= 3; i++)
                {


                        Lista.Add(new GraficasTypeVehiculoT
                        {
                            tramo = NombreTurnos[i - 1],
                            SinID = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 0, i, FechaInicio),
                            T01A = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 1, i,FechaInicio),
                            T02C = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 2, i, FechaInicio),
                            T03C = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 3,i, FechaInicio),
                            T04C = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 4, i, FechaInicio),
                            T05C = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 5,i, FechaInicio),
                            T06C = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 6, i, FechaInicio),
                            T07C = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 7, i, FechaInicio),
                            T08C = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 8, i, FechaInicio),
                            T09C = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 9, i, FechaInicio),
                            TL01A = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 10, i, FechaInicio),
                            TL02A = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 11, i, FechaInicio),
                            T02B = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 12, i, FechaInicio),
                            T03B = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 13, i, FechaInicio),
                            T04B = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 14, i, FechaInicio),
                            T01M = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 15, i, FechaInicio),
                            TPnnC = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 16, i, FechaInicio),
                            TLnnA = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 17, i, FechaInicio),
                            T01T = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 18, i, FechaInicio),
                            T01P = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 19, i, FechaInicio),

                        });
                    
                }


            }
            else
            {

                for (int i = 1; i <= 3; i++)
                {
                    Lista.Add(new GraficasTypeVehiculoT
                    {
                        tramo = NombreTurnos[i - 1],
                        SinID = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 0, i, FechaInicio, FechaFin),
                        T01A = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 1, i, FechaInicio, FechaFin),
                        T02C = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 2, i, FechaInicio, FechaFin),
                        T03C = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 3, i, FechaInicio, FechaFin),
                        T04C = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 4, i, FechaInicio, FechaFin),
                        T05C = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 5, i, FechaInicio, FechaFin),
                        T06C = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 6, i, FechaInicio, FechaFin),
                        T07C = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 7, i, FechaInicio, FechaFin),
                        T08C = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 8, i, FechaInicio, FechaFin),
                        T09C = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 9, i, FechaInicio, FechaFin),
                        TL01A = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 10, i, FechaInicio, FechaFin),
                        TL02A = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 11, i, FechaInicio, FechaFin),
                        T02B = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 12, i, FechaInicio, FechaFin),
                        T03B = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 13, i, FechaInicio, FechaFin),
                        T04B = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 14, i, FechaInicio, FechaFin),
                        T01M = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 15, i, FechaInicio, FechaFin),
                        TPnnC = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 16, i, FechaInicio, FechaFin),
                        TLnnA = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 17, i, FechaInicio, FechaFin),
                        T01T = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 18, i, FechaInicio, FechaFin),
                        T01P = ConcentradosMetodos.GetCrucesTurnosTypeVehiculo(NumeroPlaza, 19, i, FechaInicio, FechaFin),

                    });

                }
            }


            columnsOld.Add("tramo");

            foreach (var item in Lista)
            {

                if (item.SinID != 0)
                    columnsOld.Add("sinID");
                if (item.T01A != 0)
                    columnsOld.Add("t01A");
                if (item.T02C != 0)
                    columnsOld.Add("t02C");
                if (item.T03C != 0)
                    columnsOld.Add("t03C");
                if (item.T04C != 0)
                    columnsOld.Add("t04C");
                if (item.T05C != 0)
                    columnsOld.Add("t05C");
                if (item.T06C != 0)
                    columnsOld.Add("t06C");
                if (item.T07C != 0)
                    columnsOld.Add("t07C");
                if (item.T08C != 0)
                    columnsOld.Add("t08C");
                if (item.T09C != 0)
                    columnsOld.Add("t09C");
                if (item.TL01A != 0)
                    columnsOld.Add("tL01A");
                if (item.TL02A != 0)
                    columnsOld.Add("tL02A");
                if (item.T02B != 0)
                    columnsOld.Add("t02B");
                if (item.T03B != 0)
                    columnsOld.Add("t03B");
                if (item.T04B != 0)
                    columnsOld.Add("t04B");
                if (item.T01M != 0)
                    columnsOld.Add("t01M");
                if (item.TPnnC != 0)
                    columnsOld.Add("tPnnC");
                if (item.TLnnA != 0)
                    columnsOld.Add("tLnnA");
                if (item.T01T != 0)
                    columnsOld.Add("t01T");
                if (item.T01P != 0)
                    columnsOld.Add("t01P");

            }


            var columns = columnsOld.Distinct();

            object json = new { Lista, columns };

            return json;



        }

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
                    SinID = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 0, FechaInicio),
                    T01A = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 1, FechaInicio),
                    T02C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 2, FechaInicio),
                    T03C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 3, FechaInicio),
                    T04C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 4, FechaInicio),
                    T05C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 5, FechaInicio),
                    T06C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 6, FechaInicio),
                    T07C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 7, FechaInicio),
                    T08C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 8, FechaInicio),
                    T09C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 9, FechaInicio),
                    TL01A = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 10, FechaInicio),
                    TL02A = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 11, FechaInicio),
                    T02B = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 12, FechaInicio),
                    T03B = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 13, FechaInicio),
                    T04B = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 14, FechaInicio),
                    T01M = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 15, FechaInicio),
                    TPnnC = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 16, FechaInicio),
                    TLnnA = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 17, FechaInicio),
                    T01T = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 18, FechaInicio),
                    T01P = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 19, FechaInicio),

                });

            }
            else
            {
                Lista.Add(new GraficasTypeVehiculo
                {
                    nombrePlaza = nombrePlaza,
                    SinID = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 0, FechaInicio,FechaFin),
                    T01A = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 1, FechaInicio, FechaFin),
                    T02C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 2, FechaInicio, FechaFin),
                    T03C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 3, FechaInicio, FechaFin),
                    T04C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 4, FechaInicio, FechaFin),
                    T05C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 5, FechaInicio, FechaFin),
                    T06C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 6, FechaInicio, FechaFin),
                    T07C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 7, FechaInicio, FechaFin),
                    T08C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 8, FechaInicio, FechaFin),                   
                    T09C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 9, FechaInicio, FechaFin),
                    TL01A = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 10, FechaInicio, FechaFin),
                    TL02A = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 11, FechaInicio, FechaFin),
                    T02B = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 12, FechaInicio, FechaFin),
                    T03B = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 13, FechaInicio, FechaFin),
                    T04B = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 14, FechaInicio, FechaFin),
                    T01M = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 15, FechaInicio, FechaFin),
                    TPnnC = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 16, FechaInicio, FechaFin),
                    TLnnA = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 17, FechaInicio, FechaFin),
                    T01T = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 18, FechaInicio, FechaFin),
                    T01P = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(Plazas, 19, FechaInicio, FechaFin),

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

        public object GraficaTramoTypePago(string nombrePlaza, DateTime FechaInicio, DateTime FechaFin)
        {
            var Plazas = PlazasMetodos.ConvertNombrePlaza(nombrePlaza);
            var Tramos = TramosMetodos.GetTramos(Plazas);
            List<string> columnsOld = new List<string>();
            List<GraficasTypePagoT> Lista = new List<GraficasTypePagoT>();


            if (FechaInicio == FechaFin)
            {
                foreach (var item in Tramos)
                {

                    Lista.Add(new GraficasTypePagoT
                    {
                        Tramo = item.NombreTramo,
                        NoPago = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 0, FechaInicio),
                        Efectivo = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 1, FechaInicio),
                        EfectivoCRE = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 2, FechaInicio),
                        Valores = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 9, FechaInicio),
                        Residente = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 10, FechaInicio),
                        TarjetadeCredito = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 12, FechaInicio),
                        Violacion = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 13, FechaInicio),
                        TarjetadeDebito = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 14, FechaInicio),
                        PrepagoTag = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 15, FechaInicio),
                        ExentoVSC = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 27, FechaInicio),
                        ResidenteRP1 = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 71, FechaInicio),
                        ResidenteRP2 = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 72, FechaInicio),
                        ResidenteRP3 = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 73, FechaInicio),
                        ResidenteRP4 = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 74, FechaInicio),
                        ResidenteRP4_ = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 75, FechaInicio),
                    });
                }

            }
            else
            {
                foreach (var item in Tramos)
                {

                    Lista.Add(new GraficasTypePagoT
                    {
                        Tramo = item.NombreTramo,
                        NoPago = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 0, FechaInicio, FechaFin),
                        Efectivo = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 1, FechaInicio, FechaFin),
                        EfectivoCRE = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 2, FechaInicio, FechaFin),
                        Valores = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 9, FechaInicio, FechaFin),
                        Residente = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 10, FechaInicio, FechaFin),
                        TarjetadeCredito = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 12, FechaInicio, FechaFin),
                        Violacion = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 13, FechaInicio, FechaFin),
                        TarjetadeDebito = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 14, FechaInicio, FechaFin),
                        PrepagoTag = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 15, FechaInicio, FechaFin),
                        ExentoVSC = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 27, FechaInicio, FechaFin),
                        ResidenteRP1 = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 71, FechaInicio, FechaFin),
                        ResidenteRP2 = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 72, FechaInicio, FechaFin),
                        ResidenteRP3 = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 73, FechaInicio, FechaFin),
                        ResidenteRP4 = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 74, FechaInicio, FechaFin),
                        ResidenteRP4_ = ConcentradosMetodos.GetCrucesTramoTypePago(item.IdGare, 75, FechaInicio, FechaFin),
                    });
                }

            }

            columnsOld.Add("tramo");

            foreach (var item in Lista)
            {

                if (item.NoPago != 0)
                    columnsOld.Add("noPago");
                if (item.Efectivo != 0)
                    columnsOld.Add("efectivo");
                if (item.EfectivoCRE != 0)
                    columnsOld.Add("efectivoCRE");
                if (item.Valores != 0)
                    columnsOld.Add("valores");
                if (item.Residente != 0)
                    columnsOld.Add("residente");
                if (item.TarjetadeCredito != 0)
                    columnsOld.Add("tarjetadeCredito");
                if (item.Violacion != 0)
                    columnsOld.Add("violacion");
                if (item.TarjetadeDebito != 0)
                    columnsOld.Add("tarjetadeDebito");
                if (item.PrepagoTag != 0)
                    columnsOld.Add("prepagoTag");
                if (item.ExentoVSC != 0)
                    columnsOld.Add("exentoVSC");
                if (item.ResidenteRP1 != 0)
                    columnsOld.Add("residenteRP1");
                if (item.ResidenteRP2 != 0)
                    columnsOld.Add("residenteRP2");
                if (item.ResidenteRP3 != 0)
                    columnsOld.Add("residenteRP3");
                if (item.ResidenteRP4 != 0)
                    columnsOld.Add("residenteRP4");
                if (item.ResidenteRP4_ != 0)
                    columnsOld.Add("residenteRP4_");
            }

            var columns = columnsOld.Distinct();

            object json = new { Lista, columns };

            return json;
        }

        public object GraficaTramoTypeVehiculo(string nombrePlaza, DateTime FechaInicio, DateTime FechaFin)
        {
            var Plazas = PlazasMetodos.ConvertNombrePlaza(nombrePlaza);
            var TypePagos = db.TipoPago.ToList();
            var Tramos = TramosMetodos.GetTramos(Plazas);
            List<string> columnsOld = new List<string>();
            List<GraficasTypeVehiculoT> Lista = new List<GraficasTypeVehiculoT>();
            //List<string> columns = new List<string>();

            if (FechaInicio == FechaFin)
            {
                foreach (var item in Tramos)
                {

                    Lista.Add(new GraficasTypeVehiculoT
                    {
                        tramo = item.NombreTramo,
                        SinID = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare,  0, FechaInicio),
                        T01A = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 1, FechaInicio),
                        T02C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 2, FechaInicio),
                        T03C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 3, FechaInicio),
                        T04C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 4, FechaInicio),
                        T05C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 5, FechaInicio),
                        T06C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 6, FechaInicio),
                        T07C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 7, FechaInicio),
                        T08C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 8, FechaInicio),
                        T09C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 9, FechaInicio),
                        TL01A = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 10, FechaInicio),
                        TL02A = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 11, FechaInicio),
                        T02B = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 12, FechaInicio),
                        T03B = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 13, FechaInicio),
                        T04B = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 14, FechaInicio),
                        T01M = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 15, FechaInicio),
                        TPnnC = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 16, FechaInicio),
                        TLnnA = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 17, FechaInicio),
                        T01T = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 18, FechaInicio),
                        T01P = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 19, FechaInicio),

                    });
                }

            }
            else
            {
                foreach (var item in Tramos)
                {

                    Lista.Add(new GraficasTypeVehiculoT
                    {
                        tramo = item.NombreTramo,
                        SinID = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 0, FechaInicio, FechaFin),
                        T01A = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 1, FechaInicio, FechaFin),
                        T02C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 2, FechaInicio, FechaFin),
                        T03C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 3, FechaInicio, FechaFin),
                        T04C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 4, FechaInicio, FechaFin),
                        T05C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 5, FechaInicio, FechaFin),
                        T06C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 6, FechaInicio, FechaFin),
                        T07C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 7, FechaInicio, FechaFin),
                        T08C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 8, FechaInicio, FechaFin),
                        T09C = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 9, FechaInicio, FechaFin),
                        TL01A = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 10, FechaInicio, FechaFin),
                        TL02A = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 11, FechaInicio, FechaFin),
                        T02B = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 12, FechaInicio, FechaFin),
                        T03B = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 13, FechaInicio, FechaFin),
                        T04B = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 14, FechaInicio, FechaFin),
                        T01M = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 15, FechaInicio, FechaFin),
                        TPnnC = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 16, FechaInicio, FechaFin),
                        TLnnA = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 17, FechaInicio, FechaFin),
                        T01T = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 18, FechaInicio, FechaFin),
                        T01P = ConcentradosMetodos.GetCrucesTramoTypeVehiculo(item.IdGare, 19, FechaInicio, FechaFin),

                    });
                }

            }

            columnsOld.Add("tramo");

            foreach (var item in Lista)
            {

                if (item.SinID != 0)
                    columnsOld.Add("sinID");
                if (item.T01A != 0)
                    columnsOld.Add("t01A");
                if (item.T02C != 0)
                    columnsOld.Add("t02C");
                if (item.T03C != 0)
                    columnsOld.Add("t03C");
                if (item.T04C != 0)
                    columnsOld.Add("t04C");
                if (item.T05C != 0)
                    columnsOld.Add("t05C");
                if (item.T06C != 0)
                    columnsOld.Add("t06C");
                if (item.T07C != 0)
                    columnsOld.Add("t07C");
                if (item.T08C != 0)
                    columnsOld.Add("t08C");
                if (item.T09C != 0)
                    columnsOld.Add("t09C");
                if (item.TL01A != 0)
                    columnsOld.Add("tL01A");
                if (item.TL02A != 0)
                    columnsOld.Add("tL02A");
                if (item.T02B != 0)
                    columnsOld.Add("t02B");
                if (item.T03B != 0)
                    columnsOld.Add("t03B");
                if (item.T04B != 0)
                    columnsOld.Add("t04B");
                if (item.T01M != 0)
                    columnsOld.Add("t01M");
                if (item.TPnnC != 0)
                    columnsOld.Add("tPnnC");
                if (item.TLnnA != 0)
                    columnsOld.Add("tLnnA");
                if (item.T01T != 0)
                    columnsOld.Add("t01T");
                if (item.T01P != 0)
                    columnsOld.Add("t01P");

            }

            var columns = columnsOld.Distinct();
           
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
        private class GraficasTypePagoT
        {
            public string Tramo { get; set; }
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
        private class GraficasTypeVehiculoT
        {
            public string tramo { get; set; }
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
