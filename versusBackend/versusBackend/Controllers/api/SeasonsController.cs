using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using versusBackend.Models;

namespace versusBackend.Controllers.api
{
    public class SeasonsController : ApiController
    {
        [HttpPost]
        [Route("api/seasons/add")]
        public IHttpActionResult CreateSeason([FromBody]SeasonDTO _season)
        {
            season s = new season() { description = _season.Description, league = (int)_season.League, dateFrom = DateTime.Now, dateTo = new DateTime(2016, 04, 29), title = _season.Title };
            using (var context = new escorcenterdbEntities())
            {
                context.seasons.Add(s);
                context.SaveChanges();
            }
            return Ok(s);
        }

        [HttpGet]
        [Route("api/seasons")]
        public IHttpActionResult GetSeasons()
        {
            season[] _season = null;
            using (var context = new escorcenterdbEntities())
            {
                _season= (from s in context.seasons select s).ToArray<season>();
            }
            if (_season == null)
            {
                return Ok();
            }
            SeasonDTO[] seasons = AutoMapper.Mapper.Map<season[], SeasonDTO[]>(_season);
            return Ok(seasons);
        }
    }
}
