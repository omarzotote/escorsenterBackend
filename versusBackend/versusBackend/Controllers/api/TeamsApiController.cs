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
    public class TeamsApiController : ApiController
    {
        [HttpPost]
        [Route("api/teams/add")]
        public IHttpActionResult CreateTeam([FromBody]TeamDTO _team)
        {
            LeaguesApiController leaguesApiController = new LeaguesApiController();
            int leagueId = leaguesApiController.getLeagueIdByTeamId(_team.Id);
            team t = new team { Name = _team.Name, Description = _team.Description, League = leagueId, Enabled = true, Logo = _team.Logo };
            using (var context = new escorcenterdbEntities())
            {
                context.teams.Add(t);
                context.SaveChanges();
            }
            return Ok(t);
        }

        [HttpGet]
        [Route("api/teams/{id}/delete")]
        public IHttpActionResult DeleteTeams(long id)
        {
            team _team;
            using (var context = new escorcenterdbEntities())
            {
                _team = (from t in context.teams where t.Id == id select t).FirstOrDefault();
                context.teams.Remove(_team);
                context.SaveChanges();
            }
            return Ok(_team);
        }

        [HttpGet]
        [Route("api/teams/update")]
        public IHttpActionResult UpdateTeams([FromBody] TeamDTO teamDTO)
        {
            team teamMap = AutoMapper.Mapper.Map<TeamDTO, team>(teamDTO);
            team _team;
            using (var context = new escorcenterdbEntities())
            {
                _team = (from t in context.teams where t.Id == teamMap.Id select t).FirstOrDefault();
                _team = teamMap;
                context.SaveChanges();
            }
            return Ok(_team);
        }

        [HttpGet]
        [Route("api/teams")]
        public IHttpActionResult GetAllTeams()
        {
            team[] _teams = null;
            using (var context = new escorcenterdbEntities())
            {
                _teams = (from t in context.teams where t.Enabled select t).OrderBy(x => x.Name).ToArray<team>();
            }

            if (_teams == null)
                return Ok();
            TeamDTO[] _teamsDTO = AutoMapper.Mapper.Map<team[], TeamDTO[]>(_teams);
            TeamListDTO teamList = new TeamListDTO() { PageNumber = 1, PageTotal = 1 };
            teamList.items.AddRange(_teamsDTO);

            return Ok(teamList);
        }

        [HttpGet]
        [Route("api/teams/{id}")]
        public IHttpActionResult GetTeamById(long id)
        {
            team _team = null;

            using (var context = new escorcenterdbEntities())
            {
                _team = (from t in context.teams where t.Id == id && t.Enabled == true select t).FirstOrDefault<team>();
            }

            if (_team == null)
            {
                return NotFound();
            }

            TeamDTO team = AutoMapper.Mapper.Map<TeamDTO>(_team);
            return Ok(team);
        }

        [HttpGet]
        [Route("api/queryloco")]
        public IHttpActionResult insertarequipos()
        {
            team t = null;
            using (var context = new escorcenterdbEntities())
            {
                t = new team { Name = "", League = 2, Enabled = true, Description = "Omar es Joto", Logo = "http://pintarimagenes.org/wp-content/uploads/2014/04/toro5.jpg" };
                context.teams.Add(t);
            }
            return Ok(t);
        }



        [HttpGet]
        [Route("api/teams/{teamId}/score")]
        public IHttpActionResult GetScoreTableResult(int teamId)
        {
            scoretableview _scoreTableView = null;
            LeaguesApiController leagueApi = new LeaguesApiController();
            int position = 0;

            team _team = null;
            scoretableview[] _scoreTable = null;

            using (var context = new escorcenterdbEntities())
            {
                _scoreTableView = (from s in context.scoretableviews where s.team == teamId select s).FirstOrDefault<scoretableview>();
                _team = (from t in context.teams where t.Id == teamId && t.Enabled == true select t).FirstOrDefault<team>();
                long seasonId = leagueApi.GetCurrentSeasonId(_team.League, DateTime.Now);
                _scoreTable = (from st in context.scoretableviews where st.season == seasonId select st).OrderBy(st => st.GamesWon.Value * 3 + st.GamesDrawn.Value * 1).ToArray<scoretableview>();
            }
            if (_scoreTableView == null)
                return Ok();
            foreach (scoretableview st in _scoreTable)
            {
                if (st.team == teamId)
                    break;
                position++;
            }
            // scoretableview y ScoreTableResultDTO son incompatibles team del primero es Int32, team del segundo es TeamDTO
            TeamDTO teamDTO = AutoMapper.Mapper.Map<team, TeamDTO>(_team);
            ScoreTableResultDTO scoreTableResultDTO = new ScoreTableResultDTO()
            {
                League = leagueApi.getLeagueNameById(_team.League),
                Position = (long)position,
                GamesDrawn = (long)_scoreTableView.GamesDrawn,
                GamesLost = (long)_scoreTableView.GamesLost,
                GamesPlayed = (long)_scoreTableView.GamesPlayed,
                GamesWined = (long)_scoreTableView.GamesWon,
                Points = (long)_scoreTableView.ScoreFavor,
                ScoreFavor = (long)_scoreTableView.ScoreFavor,
                ScoreDifference = (long)_scoreTableView.ScoreDifference,
                ScoreAgainst = (long)_scoreTableView.ScoreAgainst,
                Team = teamDTO
            };
            return Ok(scoreTableResultDTO);
        }
    }
}
