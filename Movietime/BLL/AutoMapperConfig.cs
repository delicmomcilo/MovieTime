using AutoMapper;
using DAL;
using Model;
using Model.ViewModel;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


namespace BLL
{
    [ExcludeFromCodeCoverage]
    public class AutoMapperConfig
    {
        public static string FraListSjangerTilString(List<DBSjanger> Sjangre)
        {
            var stringSjanger = new List<string>();
            foreach( var sjanger in Sjangre)
            {
                stringSjanger.Add(sjanger.sjanger);
            }
            return string.Join(", ", stringSjanger);

        }

        
        public static void Initialize()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<DBOrdre, Ordre>();
                cfg.CreateMap<DBFilm, Film>()
                    .ForMember(felt => felt.Sjanger, opt => opt.ResolveUsing(dbfilm => FraListSjangerTilString(dbfilm.Sjanger)));
                cfg.CreateMap<DBOrdrelinje, OrdreLinje>();
                cfg.CreateMap<DBSjanger, Sjanger>();
                cfg.CreateMap<DBEndring, Endring>();

                cfg.CreateMap<KundeEndreViewModel, Kunde>();


                cfg.CreateMap<DBKunde, Kunde>()
                    .ForMember(felt => felt.Postnummer, opt => opt.MapFrom(dbkunde => dbkunde.Poststed.Postnr))
                    .ForMember(felt => felt.Poststed, opt => opt.MapFrom(dbkunde => dbkunde.Poststed.Poststed))
                    .ForMember(felt => felt.Ordrer, opt => opt.MapFrom(dbkunde => dbkunde.Ordrer));

                cfg.CreateMap<DBKunde, KundeEndreViewModel>()
                    .ForMember(felt => felt.Postnummer, opt => opt.MapFrom(dbkunde => dbkunde.Poststed.Postnr))
                    .ForMember(felt => felt.Poststed, opt => opt.MapFrom(dbkunde => dbkunde.Poststed.Poststed));
                   

                cfg.CreateMap<Kunde, DBKunde>();
                cfg.CreateMap<Film, DBFilm>();
                cfg.CreateMap<OrdreLinje, DBOrdrelinje>();
                cfg.CreateMap<Ordre, DBOrdre>();
                cfg.CreateMap<Sjanger, DBSjanger>();
                cfg.CreateMap<Endring, DBEndring>();
                cfg.CreateMap<Kunde, KundeEndreViewModel>();


            });
        }
    }
}