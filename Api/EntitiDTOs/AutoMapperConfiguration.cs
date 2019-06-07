using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Api.Models;

namespace Api.EntitiDTOs
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
           {
               cfg.CreateMap<Carriles, CarrilesDTO>()
               .ForMember(x => x.ConcentradoTransacciones, o => o.Ignore())
               .ForMember(x => x.EficienciasCarriles, o => o.Ignore())
               .ForMember(x => x.Transacciones, o => o.Ignore())
               .ForMember(x => x.IdGareNavigation, o => o.Ignore())
               .ForMember(x => x.TipoCarril, o => o.Ignore())
               .ReverseMap();

               cfg.CreateMap<ConcentradoTransacciones, ConcentradoTransaccionesDTO>()
               .ReverseMap();

               cfg.CreateMap<Delegaciones, DelegacionesDTO>()
               .ForMember(x => x.Plazas, o => o.Ignore())
               .ReverseMap();

               cfg.CreateMap<EficienciasCarriles, EficienciasCarrilesDTO>()
               .ReverseMap();

               cfg.CreateMap<Operadores, OperadoresDTO>()
               .ForMember(x => x.OperadorPlaza, o => o.Ignore())
               .ReverseMap();

               cfg.CreateMap<OperadorPlaza, OperadorPlazaDTO>()
               .ReverseMap();

               cfg.CreateMap<Plazas, PlazasDTO>()
               .ForMember(x => x.Delegacion, o => o.Ignore())
               .ForMember(x => x.OperadorPlaza, o => o.Ignore())
               .ForMember(x => x.Tramos, o => o.Ignore())               
               .ReverseMap();

               cfg.CreateMap<Puestos, PuestosDTO>()
               .ForMember(x => x.Operadores, o => o.Ignore())
               .ReverseMap();

               cfg.CreateMap<TipoCarril, TipoCarrilDTO>()
               .ForMember(x => x.Carriles, o => o.Ignore())
               .ForMember(x => x.EficienciasCarriles, o => o.Ignore())
               .ReverseMap();

               cfg.CreateMap<TipoPago, TipoPagoDTO>()
               .ForMember(x => x.ConcentradoTransacciones, o => o.Ignore())
               .ForMember(x => x.Transacciones, o => o.Ignore())
               .ReverseMap();

               cfg.CreateMap<TipoVehiculo, TipoVehiculoDTO>()
               .ForMember(x => x.ConcentradoTransacciones, o => o.Ignore())
               .ReverseMap();

               cfg.CreateMap<Tramos, TramosDTO>()
               .ForMember(x => x.Carriles, o => o.Ignore())
               .ForMember(x => x.ConcentradoTransacciones, o => o.Ignore())
               .ForMember(x => x.Transacciones, o => o.Ignore())
               .ReverseMap();

               cfg.CreateMap<Transacciones, TransaccionesDTO>()
               .ReverseMap();

               cfg.CreateMap<Turnos, TurnosDTO>()
               .ForMember(x => x.ConcentradoTransacciones, o => o.Ignore())
               .ForMember(x => x.Transacciones, o => o.Ignore())
               .ReverseMap();



           });
        }
    }
}
