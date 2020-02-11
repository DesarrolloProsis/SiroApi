using Api.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Clases
{
    public class Eficiencias
    {
        PlazasMetodos PlazasMetodos = new PlazasMetodos();
        Context db = new Context();
        public object DiscrepanciasDia(string numeroPlaza, string fechaInicio, string fechaFin, string carril)
        {

            DataTable dt = new DataTable();

            if (carril != "null")
            {

                if (numeroPlaza != "null")
                {

                    dt = ConsultasInternas("select p.NombrePlaza, e.Fecha, a.LineaCarril, e.EficienciaDiaPRE, e.EficienciaDiaPOST, r.Descripcion " +
                                               "from EficienciasCarriles e " +
                                               "inner join Tramos t on e.IdGare = t.IdGare " +
                                               "inner join Plazas p on p.NumeroPlazaCapufe = t.NumeroPlazaCapufe " +
                                               "inner join Carriles a on e.NumeroCapufeCarril = a.NumeroCapufeCarril " +
                                               "inner join TipoCarril r on r.TipoCarrilId = a.TipoCarrilId " +
                                               "where e.Fecha >= '" + fechaInicio + "' " +
                                               "and e.Fecha < '" + fechaFin + "' " +
                                               "and  t.NumeroPlazaCapufe = '" + numeroPlaza + "' " +
                                               "and a.LineaCarril = '" + carril + "'" +
                                               "and e.EficienciaDiaPRE <> 0 " +
                                               "and e.EficienciaDiaPRE <> 100 " +
                                               "and e.EficienciaDiaPOST <> 100 " +
                                               "and e.EficienciaDiaPOST <> 0 ");

                }
                else
                {
                    dt = ConsultasInternas("select p.NombrePlaza, e.Fecha, a.LineaCarril, e.EficienciaDiaPRE, e.EficienciaDiaPOST, r.Descripcion " +
                                              "from EficienciasCarriles e " +
                                              "inner join Tramos t on e.IdGare = t.IdGare " +
                                              "inner join Plazas p on p.NumeroPlazaCapufe = t.NumeroPlazaCapufe " +
                                              "inner join Carriles a on e.NumeroCapufeCarril = a.NumeroCapufeCarril " +
                                              "inner join TipoCarril r on r.TipoCarrilId = a.TipoCarrilId " +
                                              "where e.Fecha >= '" + fechaInicio + "' " +
                                              "and e.Fecha < '" + fechaFin + "' " +
                                               "and e.EficienciaDiaPRE <> 0 " +
                                               "and e.EficienciaDiaPRE <> 100 " +
                                               "and e.EficienciaDiaPOST <> 100 " +
                                               "and e.EficienciaDiaPOST <> 0 ");
                }




            }
            else
            {

                if (numeroPlaza != "null")
                {

                    dt = ConsultasInternas("select p.NombrePlaza, e.Fecha, a.LineaCarril, e.EficienciaDiaPRE, e.EficienciaDiaPOST, r.Descripcion " +
                                              "from EficienciasCarriles e " +
                                              "inner join Tramos t on e.IdGare = t.IdGare " +
                                              "inner join Plazas p on p.NumeroPlazaCapufe = t.NumeroPlazaCapufe " +
                                              "inner join Carriles a on e.NumeroCapufeCarril = a.NumeroCapufeCarril " +
                                              "inner join TipoCarril r on r.TipoCarrilId = a.TipoCarrilId " +
                                              "where e.Fecha >= '" + fechaInicio + "' " +
                                              "and e.Fecha < '" + fechaFin + "' " +
                                              "and  t.NumeroPlazaCapufe = '" + numeroPlaza + "' " +
                                               "and e.EficienciaDiaPRE <> 0 " +
                                               "and e.EficienciaDiaPRE <> 100 " +
                                               "and e.EficienciaDiaPOST <> 100 " +
                                               "and e.EficienciaDiaPOST <> 0 ");
                }
                else
                {
                    dt = ConsultasInternas("select p.NombrePlaza, e.Fecha, a.LineaCarril, e.EficienciaDiaPRE, e.EficienciaDiaPOST, r.Descripcion " +
                                            "from EficienciasCarriles e " +
                                            "inner join Tramos t on e.IdGare = t.IdGare " +
                                            "inner join Plazas p on p.NumeroPlazaCapufe = t.NumeroPlazaCapufe " +
                                            "inner join Carriles a on e.NumeroCapufeCarril = a.NumeroCapufeCarril " +
                                            "inner join TipoCarril r on r.TipoCarrilId = a.TipoCarrilId " +
                                            "where e.Fecha >= '" + fechaInicio + "' " +
                                            "and e.Fecha < '" + fechaFin + "' " +
                                            "and e.EficienciaDiaPRE <> 0 " +
                                            "and e.EficienciaDiaPRE <> 100 " +
                                            "and e.EficienciaDiaPOST <> 100 " +
                                            "and e.EficienciaDiaPOST <> 0 ");
                }






            }


            var eficienciaDiaTabla = (from DataRow item in dt.Rows
                                      select new EficienciasDiaTabla
                                      {
                                          nombrePlaza = Convert.ToString(item["NombrePlaza"]),
                                          fecha = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd"),
                                          lineaCarril = Convert.ToString(item["LineaCarril"]),
                                          descripcion = Convert.ToString(item["Descripcion"]),
                                          eficienciaDiaPre = Convert.ToString(item["EficienciaDiaPRE"]) + "%",
                                          eficienciaDiaPost = Convert.ToString(item["EficienciaDiaPOST"]) + "%",



                                      }).ToList();



            object eficiencias = new { eficienciaDiaTabla };
            return eficiencias;
        }
        public object DiscrepanciasDia(string numeroPlaza, string fechaInicio, string carril)
        {
            DataTable dt = new DataTable();

            if (carril != "null")
            {

                if (numeroPlaza != "null")
                {

                    dt = ConsultasInternas("select p.NombrePlaza, e.Fecha, a.LineaCarril, e.EficienciaDiaPRE, e.EficienciaDiaPOST, r.Descripcion " +
                                               "from EficienciasCarriles e " +
                                               "inner join Tramos t on e.IdGare = t.IdGare " +
                                               "inner join Plazas p on p.NumeroPlazaCapufe = t.NumeroPlazaCapufe " +
                                               "inner join Carriles a on e.NumeroCapufeCarril = a.NumeroCapufeCarril " +
                                               "inner join TipoCarril r on r.TipoCarrilId = a.TipoCarrilId " +
                                               "where e.Fecha >= '" + fechaInicio + "' " +
                                               "and e.Fecha < '" + Convert.ToDateTime(fechaInicio).AddDays(1).ToString("yyyy-MM-dd") + "' " +
                                               "and  t.NumeroPlazaCapufe = " + numeroPlaza + " " +
                                               "and a.LineaCarril = '" + carril + "'" +
                                               "and e.EficienciaDiaPRE <> 0 " +
                                               "and e.EficienciaDiaPRE <> 100 " +
                                               "and e.EficienciaDiaPOST <> 100 " +
                                               "and e.EficienciaDiaPOST <> 0 ");
                }
                else
                {
                    dt = ConsultasInternas("select p.NombrePlaza, e.Fecha, a.LineaCarril, e.EficienciaDiaPRE, e.EficienciaDiaPOST, r.Descripcion " +
                                             "from EficienciasCarriles e " +
                                             "inner join Tramos t on e.IdGare = t.IdGare " +
                                             "inner join Plazas p on p.NumeroPlazaCapufe = t.NumeroPlazaCapufe " +
                                             "inner join Carriles a on e.NumeroCapufeCarril = a.NumeroCapufeCarril " +
                                             "inner join TipoCarril r on r.TipoCarrilId = a.TipoCarrilId " +
                                             "where e.Fecha >= '" + fechaInicio + "' " +
                                             "and e.Fecha < '" + Convert.ToDateTime(fechaInicio).AddDays(1).ToString("yyyy-MM-dd") + "' " +
                                             "and e.EficienciaDiaPRE <> 0 " +
                                             "and e.EficienciaDiaPRE <> 100 " +
                                             "and e.EficienciaDiaPOST <> 100 " +
                                             "and e.EficienciaDiaPOST <> 0 ");

                }




            }
            else
            {

                if (numeroPlaza != "null")
                {

                    dt = ConsultasInternas("select p.NombrePlaza, e.Fecha, a.LineaCarril, e.EficienciaDiaPRE, e.EficienciaDiaPOST, r.Descripcion " +
                                              "from EficienciasCarriles e " +
                                              "inner join Tramos t on e.IdGare = t.IdGare " +
                                              "inner join Plazas p on p.NumeroPlazaCapufe = t.NumeroPlazaCapufe " +
                                              "inner join Carriles a on e.NumeroCapufeCarril = a.NumeroCapufeCarril " +
                                              "inner join TipoCarril r on r.TipoCarrilId = a.TipoCarrilId " +
                                              "where e.Fecha >= '" + fechaInicio + "' " +
                                              "and e.Fecha < '" + Convert.ToDateTime(fechaInicio).AddDays(1).ToString("yyyy-MM-dd") + "' " +
                                              "and  t.NumeroPlazaCapufe = '" + numeroPlaza + "' " +
                                              "and e.EficienciaDiaPRE <> 0 " +
                                              "and e.EficienciaDiaPRE <> 100 " +
                                              "and e.EficienciaDiaPOST <> 100 " +
                                              "and e.EficienciaDiaPOST <> 0 ");
                }
                else
                {
                    dt = ConsultasInternas("select p.NombrePlaza, e.Fecha, a.LineaCarril, e.EficienciaDiaPRE, e.EficienciaDiaPOST, r.Descripcion " +
                                         "from EficienciasCarriles e " +
                                         "inner join Tramos t on e.IdGare = t.IdGare " +
                                         "inner join Plazas p on p.NumeroPlazaCapufe = t.NumeroPlazaCapufe " +
                                         "inner join Carriles a on e.NumeroCapufeCarril = a.NumeroCapufeCarril " +
                                         "inner join TipoCarril r on r.TipoCarrilId = a.TipoCarrilId " +
                                         "where e.Fecha >= '" + fechaInicio + "' " +
                                         "and e.Fecha < '" + Convert.ToDateTime(fechaInicio).AddDays(1).ToString("yyyy-MM-dd") + "' " +
                                         "and e.EficienciaDiaPRE <> 0 " +
                                         "and e.EficienciaDiaPRE <> 100 " +
                                         "and e.EficienciaDiaPOST <> 100 " +
                                         "and e.EficienciaDiaPOST <> 0 ");
                }




            }

            var eficienciaDiaTabla = (from DataRow item in dt.Rows
                                      select new EficienciasDiaTabla
                                      {
                                          nombrePlaza = Convert.ToString(item["NombrePlaza"]),
                                          fecha = Convert.ToDateTime(item["Fecha"]).ToString("yyyy-MM-dd"),
                                          lineaCarril = Convert.ToString(item["LineaCarril"]),
                                          descripcion = Convert.ToString(item["Descripcion"]),
                                          eficienciaDiaPre = Convert.ToString(item["EficienciaDiaPRE"]) + "%",
                                          eficienciaDiaPost = Convert.ToString(item["EficienciaDiaPOST"]) + "%",

                                      }).ToList();



            object eficiencias = new { eficienciaDiaTabla };

            return eficiencias;
        }

        public List<GraficaDiaEficiencia> GraficaDia(string plaza, string fecha, string carril)
        {

            var numweoPlazaCapufe = PlazasMetodos.ConvertNombrePlaza(plaza);

            var dtPRE = ConsultasInternas("select t.PRE, count(*) as Total from Transacciones t " +
                                             "inner join Tramos c on t.IdGare = c.IdGare " +
                                             "inner join Plazas p on c.NumeroPlazaCapufe = p.NumeroPlazaCapufe " +
                                             "where ((Fecha = '" + Convert.ToDateTime(fecha).AddDays(-1).ToString("yyyy-MM-dd") + "' and TurnoId = 1 and hora >= '21:30:00') " +
                                             "or (Fecha = '" + fecha + "' and TurnoId = 1 and hora <= '08:00:00') " +
                                             "or (Fecha = '" + fecha + "' and TurnoId NOT IN(1, 0))) " +
                                             "and p.NumeroPlazaCapufe = '" + numweoPlazaCapufe + "' " +
                                             "and t.LineaCarril = '" + carril + "' " +
                                             "and t.PRE <> t.Cajero " +
                                             "group by PRE ");

            var listaPRE = (from DataRow item in dtPRE.Rows
                                      select new dtConsulta
                                      {
                                          typeVehiculo = Convert.ToInt32(item["PRE"]),
                                          total = Convert.ToInt32(item["Total"]),

                                      }).ToList();



            var dtPOST = ConsultasInternas("select t.POST, count(*) as Total from Transacciones t " +
                                          "inner join Tramos c on t.IdGare = c.IdGare " +
                                          "inner join Plazas p on c.NumeroPlazaCapufe = p.NumeroPlazaCapufe " +
                                          "where ((Fecha = '" + Convert.ToDateTime(fecha).AddDays(-1).ToString("yyyy-MM-dd") + "' and TurnoId = 1 and hora >= '21:30:00') " +
                                          "or (Fecha = '" + fecha + "' and TurnoId = 1 and hora <= '08:00:00') " +
                                          "or (Fecha = '" + fecha + "' and TurnoId NOT IN(1, 0))) " +
                                          "and p.NumeroPlazaCapufe = '" + numweoPlazaCapufe + "' " +
                                          "and t.LineaCarril = '" + carril + "' " +
                                          "and t.POST <> t.Cajero " +
                                          "group by POST ");


            var listaPOST = (from DataRow item in dtPOST.Rows
                            select new dtConsulta
                            {
                                typeVehiculo = Convert.ToInt32(item["POST"]),
                                total = Convert.ToInt32(item["Total"]),

                            }).ToList();

            var tipoVehiculo = db.TipoVehiculo.ToList();
            List<GraficaDiaEficiencia> graficaDia = new List<GraficaDiaEficiencia>();

            foreach (var item in tipoVehiculo)
            {
                int Pre = listaPRE.Where(x => x.typeVehiculo == item.TipoVehiculoId).Count();
                int Post = listaPOST.Where(x => x.typeVehiculo == item.TipoVehiculoId).Count();
                var Pre_ = listaPRE.Where(x => x.typeVehiculo == item.TipoVehiculoId).ToList();
                var Post_ = listaPOST.Where(x => x.typeVehiculo == item.TipoVehiculoId).ToList();


                if (Pre > 0 && Post > 0)
                {
                    graficaDia.Add(new GraficaDiaEficiencia
                    {
                        typeVehiculo = item.ClaveVehiculo,
                        PRE = Pre_[0].total,
                        POST = Post_[0].total
                    });
                }
                if(Pre > 0 && Post == 0)
                {
                    graficaDia.Add(new GraficaDiaEficiencia
                    {
                        typeVehiculo = item.ClaveVehiculo,
                        PRE = Pre_[0].total,
                        POST = 0
                    });
                }
                if(Pre == 0 && Post > 0)
                {
                    graficaDia.Add(new GraficaDiaEficiencia
                    {
                        typeVehiculo = item.ClaveVehiculo,
                        PRE = 0,
                        POST = Post_[0].total
                    });
                }

            }

            return graficaDia;
        }

        public List<GraficaDiaEficiencia>  GraficaMatutino(string plaza, string fecha, string carril)
        {

            var numweoPlazaCapufe = PlazasMetodos.ConvertNombrePlaza(plaza);

            var dtPRE = ConsultasInternas("select t.PRE, count(*) as Total from Transacciones t " +
                                             "inner join Tramos c on t.IdGare = c.IdGare " +
                                             "inner join Plazas p on c.NumeroPlazaCapufe = p.NumeroPlazaCapufe " +
                                             "where ((Fecha = '" + Convert.ToDateTime(fecha).AddDays(-1).ToString("yyyy-MM-dd") + "' and TurnoId = 1 and hora >= '21:30:00') " +
                                             "or (Fecha = '" + fecha + "' and TurnoId = 1 and hora <= '08:00:00') " +
                                             "or (Fecha = '" + fecha + "' and TurnoId NOT IN(1, 0))) " +
                                             "and p.NumeroPlazaCapufe = '" + numweoPlazaCapufe + "' " +
                                             "and t.LineaCarril = '" + carril + "' " +
                                             "and t.TurnoId = '1' " +
                                             "and t.PRE <> t.Cajero " +
                                             "group by PRE ");

            var listaPRE = (from DataRow item in dtPRE.Rows
                            select new dtConsulta
                            {
                                typeVehiculo = Convert.ToInt32(item["PRE"]),
                                total = Convert.ToInt32(item["Total"]),

                            }).ToList();



            var dtPOST = ConsultasInternas("select t.POST, count(*) as Total from Transacciones t " +
                                          "inner join Tramos c on t.IdGare = c.IdGare " +
                                          "inner join Plazas p on c.NumeroPlazaCapufe = p.NumeroPlazaCapufe " +
                                          "where ((Fecha = '" + Convert.ToDateTime(fecha).AddDays(-1).ToString("yyyy-MM-dd") + "' and TurnoId = 1 and hora >= '21:30:00') " +
                                          "or (Fecha = '" + fecha + "' and TurnoId = 1 and hora <= '08:00:00') " +
                                          "or (Fecha = '" + fecha + "' and TurnoId NOT IN(1, 0))) " +
                                          "and p.NumeroPlazaCapufe = '" + numweoPlazaCapufe + "' " +
                                          "and t.LineaCarril = '" + carril + "' " +
                                          "and t.TurnoId = '1' " +
                                          "and t.POST <> t.Cajero " +
                                          "group by POST ");


            var listaPOST = (from DataRow item in dtPOST.Rows
                             select new dtConsulta
                             {
                                 typeVehiculo = Convert.ToInt32(item["POST"]),
                                 total = Convert.ToInt32(item["Total"]),

                             }).ToList();

            var tipoVehiculo = db.TipoVehiculo.ToList();
            List<GraficaDiaEficiencia> graficaMatutino = new List<GraficaDiaEficiencia>();

            foreach (var item in tipoVehiculo)
            {
                int Pre = listaPRE.Where(x => x.typeVehiculo == item.TipoVehiculoId).Count();
                int Post = listaPOST.Where(x => x.typeVehiculo == item.TipoVehiculoId).Count();
                var Pre_ = listaPRE.Where(x => x.typeVehiculo == item.TipoVehiculoId).ToList();
                var Post_ = listaPOST.Where(x => x.typeVehiculo == item.TipoVehiculoId).ToList();


                if (Pre > 0 && Post > 0)
                {
                    graficaMatutino.Add(new GraficaDiaEficiencia
                    {
                        typeVehiculo = item.ClaveVehiculo,
                        PRE = Pre_[0].total,
                        POST = Post_[0].total
                    });
                }
                if (Pre > 0 && Post == 0)
                {
                    graficaMatutino.Add(new GraficaDiaEficiencia
                    {
                        typeVehiculo = item.ClaveVehiculo,
                        PRE = Pre_[0].total,
                        POST = 0
                    });
                }
                if (Pre == 0 && Post > 0)
                {
                    graficaMatutino.Add(new GraficaDiaEficiencia
                    {
                        typeVehiculo = item.ClaveVehiculo,
                        PRE = 0,
                        POST = Post_[0].total
                    });
                }

            }

            return graficaMatutino;
        }
        public List<GraficaDiaEficiencia> GraficaVespertino(string plaza, string fecha, string carril)
        {

            var numweoPlazaCapufe = PlazasMetodos.ConvertNombrePlaza(plaza);

            var dtPRE = ConsultasInternas("select t.PRE, count(*) as Total from Transacciones t " +
                                             "inner join Tramos c on t.IdGare = c.IdGare " +
                                             "inner join Plazas p on c.NumeroPlazaCapufe = p.NumeroPlazaCapufe " +
                                             "where ((Fecha = '" + Convert.ToDateTime(fecha).AddDays(-1).ToString("yyyy-MM-dd") + "' and TurnoId = 1 and hora >= '21:30:00') " +
                                             "or (Fecha = '" + fecha + "' and TurnoId = 1 and hora <= '08:00:00') " +
                                             "or (Fecha = '" + fecha + "' and TurnoId NOT IN(1, 0))) " +
                                             "and p.NumeroPlazaCapufe = '" + numweoPlazaCapufe + "' " +
                                             "and t.LineaCarril = '" + carril + "' " +
                                             "and t.TurnoId = '2' " +
                                             "and t.PRE <> t.Cajero " +
                                             "group by PRE ");

            var listaPRE = (from DataRow item in dtPRE.Rows
                            select new dtConsulta
                            {
                                typeVehiculo = Convert.ToInt32(item["PRE"]),
                                total = Convert.ToInt32(item["Total"]),

                            }).ToList();



            var dtPOST = ConsultasInternas("select t.POST, count(*) as Total from Transacciones t " +
                                          "inner join Tramos c on t.IdGare = c.IdGare " +
                                          "inner join Plazas p on c.NumeroPlazaCapufe = p.NumeroPlazaCapufe " +
                                          "where ((Fecha = '" + Convert.ToDateTime(fecha).AddDays(-1).ToString("yyyy-MM-dd") + "' and TurnoId = 1 and hora >= '21:30:00') " +
                                          "or (Fecha = '" + fecha + "' and TurnoId = 1 and hora <= '08:00:00') " +
                                          "or (Fecha = '" + fecha + "' and TurnoId NOT IN(1, 0))) " +
                                          "and p.NumeroPlazaCapufe = '" + numweoPlazaCapufe + "' " +
                                          "and t.LineaCarril = '" + carril + "' " +
                                          "and t.TurnoId = '2' " +
                                          "and t.POST <> t.Cajero " +
                                          "group by POST ");


            var listaPOST = (from DataRow item in dtPOST.Rows
                             select new dtConsulta
                             {
                                 typeVehiculo = Convert.ToInt32(item["POST"]),
                                 total = Convert.ToInt32(item["Total"]),

                             }).ToList();

            var tipoVehiculo = db.TipoVehiculo.ToList();
            List<GraficaDiaEficiencia> graficaVespertino = new List<GraficaDiaEficiencia>();

            foreach (var item in tipoVehiculo)
            {
                int Pre = listaPRE.Where(x => x.typeVehiculo == item.TipoVehiculoId).Count();
                int Post = listaPOST.Where(x => x.typeVehiculo == item.TipoVehiculoId).Count();
                var Pre_ = listaPRE.Where(x => x.typeVehiculo == item.TipoVehiculoId).ToList();
                var Post_ = listaPOST.Where(x => x.typeVehiculo == item.TipoVehiculoId).ToList();


                if (Pre > 0 && Post > 0)
                {
                    graficaVespertino.Add(new GraficaDiaEficiencia
                    {
                        typeVehiculo = item.ClaveVehiculo,
                        PRE = Pre_[0].total,
                        POST = Post_[0].total
                    });
                }
                if (Pre > 0 && Post == 0)
                {
                    graficaVespertino.Add(new GraficaDiaEficiencia
                    {
                        typeVehiculo = item.ClaveVehiculo,
                        PRE = Pre_[0].total,
                        POST = 0
                    });
                }
                if (Pre == 0 && Post > 0)
                {
                    graficaVespertino.Add(new GraficaDiaEficiencia
                    {
                        typeVehiculo = item.ClaveVehiculo,
                        PRE = 0,
                        POST = Post_[0].total
                    });
                }

            }

            return graficaVespertino;
        }
        public List<GraficaDiaEficiencia> GraficaNocturno(string plaza, string fecha, string carril)
        {

            var numweoPlazaCapufe = PlazasMetodos.ConvertNombrePlaza(plaza);

            var dtPRE = ConsultasInternas("select t.PRE, count(*) as Total from Transacciones t " +
                                             "inner join Tramos c on t.IdGare = c.IdGare " +
                                             "inner join Plazas p on c.NumeroPlazaCapufe = p.NumeroPlazaCapufe " +
                                             "where ((Fecha = '" + Convert.ToDateTime(fecha).AddDays(-1).ToString("yyyy-MM-dd") + "' and TurnoId = 1 and hora >= '21:30:00') " +
                                             "or (Fecha = '" + fecha + "' and TurnoId = 1 and hora <= '08:00:00') " +
                                             "or (Fecha = '" + fecha + "' and TurnoId NOT IN(1, 0))) " +
                                             "and p.NumeroPlazaCapufe = '" + numweoPlazaCapufe + "' " +
                                             "and t.LineaCarril = '" + carril + "' " +
                                             "and t.TurnoId = '2' " +
                                             "and t.PRE <> t.Cajero " +
                                             "group by PRE ");

            var listaPRE = (from DataRow item in dtPRE.Rows
                            select new dtConsulta
                            {
                                typeVehiculo = Convert.ToInt32(item["PRE"]),
                                total = Convert.ToInt32(item["Total"]),

                            }).ToList();



            var dtPOST = ConsultasInternas("select t.POST, count(*) as Total from Transacciones t " +
                                          "inner join Tramos c on t.IdGare = c.IdGare " +
                                          "inner join Plazas p on c.NumeroPlazaCapufe = p.NumeroPlazaCapufe " +
                                          "where ((Fecha = '" + Convert.ToDateTime(fecha).AddDays(-1).ToString("yyyy-MM-dd") + "' and TurnoId = 1 and hora >= '21:30:00') " +
                                          "or (Fecha = '" + fecha + "' and TurnoId = 1 and hora <= '08:00:00') " +
                                          "or (Fecha = '" + fecha + "' and TurnoId NOT IN(1, 0))) " +
                                          "and p.NumeroPlazaCapufe = '" + numweoPlazaCapufe + "' " +
                                          "and t.LineaCarril = '" + carril + "' " +
                                          "and t.TurnoId = '2' " +
                                          "and t.POST <> t.Cajero " +
                                          "group by POST ");


            var listaPOST = (from DataRow item in dtPOST.Rows
                             select new dtConsulta
                             {
                                 typeVehiculo = Convert.ToInt32(item["POST"]),
                                 total = Convert.ToInt32(item["Total"]),

                             }).ToList();

            var tipoVehiculo = db.TipoVehiculo.ToList();
            List<GraficaDiaEficiencia> graficaNocturno = new List<GraficaDiaEficiencia>();

            foreach (var item in tipoVehiculo)
            {
                int Pre = listaPRE.Where(x => x.typeVehiculo == item.TipoVehiculoId).Count();
                int Post = listaPOST.Where(x => x.typeVehiculo == item.TipoVehiculoId).Count();
                var Pre_ = listaPRE.Where(x => x.typeVehiculo == item.TipoVehiculoId).ToList();
                var Post_ = listaPOST.Where(x => x.typeVehiculo == item.TipoVehiculoId).ToList();


                if (Pre > 0 && Post > 0)
                {
                    graficaNocturno.Add(new GraficaDiaEficiencia
                    {
                        typeVehiculo = item.ClaveVehiculo,
                        PRE = Pre_[0].total,
                        POST = Post_[0].total
                    });
                }
                if (Pre > 0 && Post == 0)
                {
                    graficaNocturno.Add(new GraficaDiaEficiencia
                    {
                        typeVehiculo = item.ClaveVehiculo,
                        PRE = Pre_[0].total,
                        POST = 0
                    });
                }
                if (Pre == 0 && Post > 0)
                {
                    graficaNocturno.Add(new GraficaDiaEficiencia
                    {
                        typeVehiculo = item.ClaveVehiculo,
                        PRE = 0,
                        POST = Post_[0].total
                    });
                }

            }

            return graficaNocturno;
        }
        public List<tablePrePost> TablaPrePost(string plaza, string fecha, string carril)
        {

            var numeroPlazaCapufe = PlazasMetodos.ConvertNombrePlaza(plaza);

            var dtPrePost = ConsultasInternas("select p.NombrePlaza, t.Fecha, t.Hora, t.TurnoId, t.TipoPagoId, t.PRE, t.Cajero, t.POST " +
                                              "from Transacciones t " +
                                              "inner join Tramos c on t.IdGare = c.IdGare " +
                                              "inner join Plazas p on c.NumeroPlazaCapufe = p.NumeroPlazaCapufe " +
                                              "where ((Fecha = '" + Convert.ToDateTime(fecha).AddDays(-1).ToString("yyyy-MM-dd") + "' and TurnoId = 1 and hora >= '21:30:00') " +
                                              "or (Fecha = '" + fecha + "' and TurnoId = 1 and hora <= '08:00:00') " +
                                              "or (Fecha = '" + fecha + "' and TurnoId NOT IN(1, 0))) " +
                                              "and p.NumeroPlazaCapufe = '" + numeroPlazaCapufe + "' " +
                                              "and t.LineaCarril = '" + carril + "' " +
                                              "and t.PRE <> t.Cajero " +
                                              "UNION ALL " +
                                            "select p.NombrePlaza, t.Fecha, t.Hora, t.TurnoId, t.TipoPagoId, t.PRE, t.Cajero, t.POST " +
                                              "from Transacciones t " +
                                              "inner join Tramos c on t.IdGare = c.IdGare " +
                                              "inner join Plazas p on c.NumeroPlazaCapufe = p.NumeroPlazaCapufe " +
                                              "where ((Fecha = '" + Convert.ToDateTime(fecha).AddDays(-1).ToString("yyyy-MM-dd") + "' and TurnoId = 1 and hora >= '21:30:00') " +
                                              "or (Fecha = '" + fecha + "' and TurnoId = 1 and hora <= '08:00:00') " +
                                              "or (Fecha = '" + fecha + "' and TurnoId NOT IN(1, 0))) " +
                                              "and p.NumeroPlazaCapufe = '" + numeroPlazaCapufe + "' " +
                                              "and t.LineaCarril = '" + carril + "' " +
                                              "and t.POST <> t.Cajero ");


            var tablaPrePost = (from DataRow item in dtPrePost.Rows
                                select new tablePrePost
                                {
                                    nombrePlaza = Convert.ToString(item["NombrePlaza"]),
                                    fecha = Convert.ToDateTime(item["Fecha"]).ToString("dd-MM-yyyy"),
                                    hora = Convert.ToString(item["Hora"]),
                                    turno = Convert.ToString(item["TurnoId"]),
                                    tipoPago = Convert.ToString(item["TipoPagoId"]),
                                    Pre = Convert.ToString(item["PRE"]),
                                    Cajero = Convert.ToString(item["Cajero"]),
                                    Post = Convert.ToString(item["POST"]),

                                }).ToList();


   

            var listTurno = db.Turnos.ToList();
            foreach(var item in listTurno)
            {
                foreach(var item2 in tablaPrePost)
                {
                    if (item2.turno == Convert.ToString(item.TurnoId))
                        item2.turno = item.NombreTurno.ToString();
                }
            }

            var listPago = db.TipoPago.ToList();

            foreach (var item in listPago)
            {
                foreach (var item2 in tablaPrePost)
                {
                    if (item2.tipoPago == Convert.ToString(item.TipoPagoId))
                        item2.tipoPago = item.NombrePago.ToString();
                }
            }

            var listVehiculo = db.TipoVehiculo.ToList();

            foreach (var item in listVehiculo)
            {
                foreach (var item2 in tablaPrePost)
                {
                    if (item2.Pre == Convert.ToString(item.TipoVehiculoId))
                        item2.Pre = item.ClaveVehiculo.ToString();
                    if (item2.Cajero == Convert.ToString(item.TipoVehiculoId))
                        item2.Cajero = item.ClaveVehiculo.ToString();
                    if (item2.Post == Convert.ToString(item.TipoVehiculoId))
                        item2.Post = item.ClaveVehiculo.ToString();
                }
            }



            return tablaPrePost;
        }


        private DataTable ConsultasInternas(string Query)
        {

            DataTable dt = new DataTable();
            SqlConnection cn = new SqlConnection("Data Source = 192.168.0.88; Initial Catalog = CLR_2; User ID = sa; password = CAPUFE; MultipleActiveResultSets = true;");
            using (SqlCommand cmd = new SqlCommand("", cn))
            {
                try
                {
                    cmd.CommandText = Query;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    cn.Close();
                }

            }
            return dt;
        }


    }

    public class EficienciasDiaTabla
    {
        public string nombrePlaza { get; set; }
        public string fecha { get; set; }
        public string lineaCarril { get; set; }
        public string descripcion { get; set; }
        public string eficienciaDiaPre { get; set; }
        public string eficienciaDiaPost { get; set; }
    }
   public class GraficaDiaEficiencia
    {
        public string typeVehiculo { get; set; }
        public int PRE { get; set; }
        public int POST { get; set; }
    }
    public class dtConsulta
    {
        public int typeVehiculo { get; set; }
        public int total { get; set; }
    }
    public class tablePrePost
    {
        public string nombrePlaza { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string turno { get; set; }
        public string tipoPago { get; set; }
        public string Pre { get; set; }
        public string Cajero { get; set; }
        public string Post { get; set; }

    }
}
