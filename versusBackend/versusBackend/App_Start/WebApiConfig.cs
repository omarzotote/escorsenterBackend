using AutoMapper;
using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using versusBackend.Models;
using WebApiContrib.Formatting;

namespace versusBackend
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.Formatters.Add(new ProtoBufFormatter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            Mapper.CreateMap<region, RegionDTO>();
            Mapper.CreateMap<league, LeagueDTO>();
            Mapper.CreateMap<team, TeamDTO>();
            Mapper.CreateMap<season, SeasonDTO>();
            Mapper.CreateMap<scoretableview, ScoreTableResultDTO>();
            Mapper.CreateMap<notification, NotificationDTO>();
            Mapper.CreateMap<field, FieldDTO>();
        }
    }
}
