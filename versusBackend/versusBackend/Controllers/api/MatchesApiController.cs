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
    public class MatchesApiController : ApiController
    {
        [HttpPost]
        [Route("api/matches/add")]
        public IHttpActionResult CreateMatch(MatchDTO _match)
        {
            int weekId = WeeksApiController.getWeekId(_match.Id);
            match m = new match { date = DateTime.Parse(_match.Date), details = _match.Details, enabled = true, field = (int)_match.Field.Id, scoreTeam1 = (sbyte)_match.ScoreTeam1, scoreTeam2 = (sbyte)_match.ScoreTeam2, team1 = (int)_match.Team1.Id, team2 = (int)_match.Team2.Id, week = weekId };
            using (var context = new escorcenterdbEntities())
            {
                context.matches.Add(m);
                context.SaveChanges();
            }
            return Ok(m);
        }

        [HttpGet]
        [Route("api/matches/{id}/delete")]
        public IHttpActionResult DeleteMatch(long id)
        {
            match _match;
            using (var context = new escorcenterdbEntities())
            {
                _match = (from m in context.matches where m.Id == id select m).FirstOrDefault();
                context.matches.Remove(_match);
                context.SaveChanges();
            }
            return Ok(_match);
        }

        [HttpGet]
        [Route("api/matches/update")]
        public IHttpActionResult UpdateMatch([FromBody] MatchDTO matchDTO)
        {
            match matchMap = AutoMapper.Mapper.Map<MatchDTO, match>(matchDTO);
            match _match;
            using (var context = new escorcenterdbEntities())
            {
                _match = (from m in context.matches where m.Id == matchMap.Id select m).FirstOrDefault();
                _match = matchMap;
                context.SaveChanges();
            }
            return Ok(_match);
        }
    }
}
