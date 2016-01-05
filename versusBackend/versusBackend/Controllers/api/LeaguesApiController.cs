using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using versusBackend.Models;

namespace versusBackend.Controllers
{
    public class LeaguesApiController : ApiController
    {
        [HttpPost]
        [Route("api/leagues/add")]
        public IHttpActionResult CreateLeague([FromBody]LeagueDTO _league)
        {
            league l = new league() { Name = _league.Name, Description = _league.Description, Enabled = true, Region = _league.Region };
            using (var context = new escorcenterdbEntities())
            {
                context.leagues.Add(l);
                context.SaveChanges();
            }
            return Ok(l);
        }

        [HttpGet]
        [Route("api/leagues/{id}/delete")]
        public IHttpActionResult DeleteLeague(long id)
        {
            league l;
            using (var context = new escorcenterdbEntities())
            {
                l = (from r in context.leagues where r.Id == id select r).FirstOrDefault();
                context.leagues.Remove(l);
                context.SaveChanges();
            }
            return Ok(l);
        }

        [HttpGet]
        [Route("api/leagues/update")]
        public IHttpActionResult UpdateLeague([FromBody] LeagueDTO leagueDto)
        {
            league leagueMap = AutoMapper.Mapper.Map<LeagueDTO, league>(leagueDto);
            league l;
            using (var context = new escorcenterdbEntities())
            {
                l = (from r in context.leagues where r.Id == leagueMap.Id select r).FirstOrDefault();
                l = leagueMap;
                context.SaveChanges();
            }
            return Ok(l);
        }

        [HttpGet]
        [Route("api/leagues")]
        public IHttpActionResult GetLeagues(int pageSize = 0, int start = 0)
        {
            league[] _leagues = null;
            using (var context = new escorcenterdbEntities())
            {
                _leagues = (from l in context.leagues where l.Enabled == true select l).OrderBy(x => x.Name).ToArray<league>();
            }

            if (_leagues == null)
            {
                return Ok();
            }

            LeagueDTO[] leagues = AutoMapper.Mapper.Map<league[], LeagueDTO[]>(_leagues);
            LeagueListDTO leagueList = new LeagueListDTO() { PageNumber = pageSize, PageTotal = 1 };
            leagueList.items.AddRange(leagues);
            return Ok(leagueList);
        }


        [HttpGet]
        [Route("api/leagues/{id}")]
        public IHttpActionResult GetLeagueById(long id = 0)
        {
            league _league = null;
            team[] _teams;
            using (var context = new escorcenterdbEntities())
            {
                _league = (from l in context.leagues where l.Id == id && l.Enabled == true select l).FirstOrDefault<league>();
                _teams = (from t in context.teams where t.League == id && t.Enabled == true select t).ToArray<team>();
            }

            if (_league == null)
            {
                return NotFound();
            }

            LeagueDTO league = AutoMapper.Mapper.Map<LeagueDTO>(_league);
            TeamDTO[] teams = AutoMapper.Mapper.Map<team[], TeamDTO[]>(_teams.ToArray());
            league.teams.AddRange(teams);
            return Ok(league);
        }

        [HttpGet]
        [Route("api/leagues/teams/{id}")]
        public IHttpActionResult GetLeagueByTeamId(long id)
        {
            int leagueId = getLeagueIdByTeamId(id);
            league league;
            using (var context = new escorcenterdbEntities())
            {
                league = (from l in context.leagues where l.Id == leagueId select l).FirstOrDefault<league>();
            }
            return Ok(league);
        }

        [HttpGet]
        [Route("api/leagues/{leagueId}/futureMatches")]
        public IHttpActionResult GetFutureMatchesByLeagueId(long leagueId)
        {
            DateTime now = DateTime.Now;
            long seasonId = GetCurrentSeasonId(leagueId, now);
            if (seasonId < 0)
                return Ok();

            season _season = null;
            List<WeekDTO> weeks = new List<WeekDTO>();
            using (var context = new escorcenterdbEntities())
            {
                week[] _weeks = (from w in context.weeks where w.enabled == true && w.season == seasonId && now <= w.dateTo select w).OrderBy(w => w.dateFrom).ToArray<week>();
                foreach (week _week in _weeks)
                {
                    WeekDTO week = GetFutureMatchesByWeekId(_week.id, now);
                    if (week != null)
                    {
                        weeks.Add(week);
                    }
                }
                _season = (from s in context.seasons where s.id == seasonId select s).FirstOrDefault<season>();
            }
            if (_season == null)
            {
                return Ok();
            }
            SeasonDTO season = new SeasonDTO();
            season.Title = _season.title;
            season.DateFrom = _season.dateFrom.ToString();
            season.DateTo = _season.dateTo.ToString();
            season.Description = _season.description;
            season.Id = _season.id;
            season.League = _season.league;
            season.Weeks.AddRange(weeks);
            return Ok(season);
        }

        [HttpGet]
        [Route("api/leagues/{leagueId}/pastMatches")]
        public IHttpActionResult GetPastMatchesByLeagueId(long leagueId)
        {
            DateTime now = DateTime.Now;
            long seasonId = GetCurrentSeasonId(leagueId, now);
            if (seasonId < 0)
                return Ok();

            season _season = null;
            List<WeekDTO> weeks = new List<WeekDTO>();
            using (var context = new escorcenterdbEntities())
            {
                week[] _weeks = (from w in context.weeks where w.enabled == true && w.season == seasonId && w.dateFrom <= now select w).OrderByDescending(w => w.dateFrom).ToArray<week>();
                foreach (week _week in _weeks)
                {
                    WeekDTO week = GetPastMatchesByWeekId(_week.id, now);
                    if (week != null)
                    {
                        weeks.Add(week);
                    }
                }
                _season = (from s in context.seasons where s.id == seasonId select s).FirstOrDefault<season>();
            }
            if (_season == null)
            {
                return Ok();
            }
            SeasonDTO season = new SeasonDTO
            {
                Title = _season.title,
                DateFrom = _season.dateFrom.ToString(),
                DateTo = _season.dateTo.ToString(),
                Description = _season.description,
                Id = _season.id,
                League = _season.league
            };
            season.Weeks.AddRange(weeks);
            return Ok(season);
        }

        [HttpGet]
        [Route("api/leagues/{leagueId}/table")]
        public IHttpActionResult GetResultTable(int leagueId)
        {
            long seasonId = GetCurrentSeasonId(leagueId, DateTime.Now);
            if (seasonId == 0)
            {
                return Ok();
            }

            scoretableview[] _scoreTable = null;
            season _season = null;
            List<ScoreTableResultDTO> scoreTable = new List<ScoreTableResultDTO>();

            using (var context = new escorcenterdbEntities())
            {
                _scoreTable = (from st in context.scoretableviews where st.season == seasonId select st).OrderBy(st => st.GamesWon.Value * 3 + st.GamesDrawn.Value * 1).ToArray<scoretableview>();
                _season = (from s in context.seasons where s.id == seasonId && s.enabled == true select s).FirstOrDefault<season>();

                if (_scoreTable == null || _season == null)
                {
                    return NotFound();
                }

                foreach (scoretableview st in _scoreTable)
                {
                    team _team = (from t in context.teams where t.Id == st.team select t).FirstOrDefault<team>();

                    String leagueName = getLeagueNameById(_team.League);
                    ScoreTableResultDTO result = new ScoreTableResultDTO
                    {
                        Team = AutoMapper.Mapper.Map<team, TeamDTO>(_team),
                        GamesDrawn = st.GamesDrawn.Value,
                        GamesLost = st.GamesLost.Value,
                        GamesPlayed = st.GamesPlayed.Value,
                        GamesWined = st.GamesWon.Value,
                        //Cambiar esto a hacerlo dinamico, no solo para el fut
                        Points = st.GamesWon.Value * 3 + st.GamesDrawn.Value * 1,
                        ScoreAgainst = (long)st.ScoreAgainst.Value,
                        ScoreDifference = (long)st.ScoreDifference.Value,
                        ScoreFavor = (long)st.ScoreFavor.Value,
                        League = leagueName
                    };

                    scoreTable.Add(result);
                }
            }
            if (_season == null)
            {
                return Ok();
            }

            scoreTable.OrderBy(r => r.Points);

            SeasonDTO season = new SeasonDTO
            {
                DateFrom = _season.dateFrom.ToString(),
                DateTo = _season.dateTo.ToString(),
                Description = _season.description,
                Id = _season.id,
                League = _season.league,
                Title = _season.title
            };

            season.ScoreTableResult.AddRange(scoreTable);
            return Ok(season);
        }

        public long GetCurrentSeasonId(long leagueId, DateTime date)
        {
            season _season = null;
            using (var context = new escorcenterdbEntities())
            {
                _season = (from s in context.seasons where s.league == leagueId && s.dateFrom <= date && s.dateTo >= date && s.enabled == true select s).FirstOrDefault<season>();
            }
            if (_season == null)
            {
                return 0;
            }
            return _season.id;
        }

        WeekDTO GetFutureMatchesByWeekId(long weekId, DateTime date)
        {
            match[] _matches = null;
            List<MatchDTO> matches = new List<MatchDTO>();
            week _week = null;

            using (var context = new escorcenterdbEntities())
            {
                _week = (from w in context.weeks where w.enabled == true && w.id == weekId select w).FirstOrDefault<week>();
                _matches = (from m in context.matches where m.enabled == true && m.week == _week.id && m.date >= date select m).OrderBy(m => m.date).ToArray<match>();
            }
            matches = ParseMatches(_matches);
            if (matches == null || _week == null)
            {
                return null;
            }

            WeekDTO week = new WeekDTO()
            {
                DateFrom = _week.dateFrom.ToString(),
                DateTo = _week.dateTo.ToString(),
                Description = _week.description,
                Id = _week.id,
                Season = _week.season,
                Title = _week.title
            };

            week.matches.AddRange(matches);
            return week;
        }

        WeekDTO GetPastMatchesByWeekId(long weekId, DateTime date)
        {
            match[] _matches = null;
            List<MatchDTO> matches = new List<MatchDTO>();
            week _week = null;

            using (var context = new escorcenterdbEntities())
            {
                _week = (from w in context.weeks where w.enabled == true && w.id == weekId select w).FirstOrDefault<week>();
                _matches = (from m in context.matches where m.enabled == true && m.week == _week.id && m.date < date select m).OrderByDescending(m => m.date).ToArray<match>();
            }
            matches = ParseMatches(_matches);
            if (matches == null || _week == null)
            {
                return null;
            }

            WeekDTO week = new WeekDTO()
            {
                DateFrom = _week.dateFrom.ToString(),
                DateTo = _week.dateTo.ToString(),
                Description = _week.description,
                Id = _week.id,
                Season = _week.season,
                Title = _week.title
            };

            week.matches.AddRange(matches);
            return week;
        }

        List<MatchDTO> ParseMatches(match[] _matches)
        {
            List<MatchDTO> matches = new List<MatchDTO>();
            using (var context = new escorcenterdbEntities())
            {
                foreach (match _match in _matches)
                {
                    team team1 = (from t in context.teams where t.Id == _match.team1 select t).FirstOrDefault<team>();
                    team team2 = (from t in context.teams where t.Id == _match.team2 select t).FirstOrDefault<team>();
                    field field = (from f in context.fields where f.Id == _match.field select f).FirstOrDefault<field>();
                    MatchDTO match = new MatchDTO
                    {
                        Team1 = AutoMapper.Mapper.Map<team, TeamDTO>(team1),
                        ScoreTeam1 = _match.scoreTeam1,
                        Team2 = AutoMapper.Mapper.Map<team, TeamDTO>(team2),
                        ScoreTeam2 = _match.scoreTeam2,
                        Date = _match.date.ToString(),
                        Details = _match.details,
                        Field = AutoMapper.Mapper.Map<field, FieldDTO>(field),
                        Finished = _match.finished,
                        Forfeit = _match.forfeit,
                        Id = _match.Id,
                        ScoreExtraTimeTeam1 = 0,
                        ScoreExtraTimeTeam2 = 0
                    };
                    matches.Add(match);
                }
            }
            return matches;
        }

        public int getLeagueIdByTeamId(long id)
        {
            int leagueId = 0;
            using (var context = new escorcenterdbEntities())
            {
                leagueId = (from t in context.teams where t.Id == id select t.League).FirstOrDefault<int>();
            }
            return leagueId;
        }
        public String getLeagueNameById(long id)
        {
            league l;
            using (var context = new escorcenterdbEntities())
            {
                l = (from t in context.leagues where t.Id == id select t).FirstOrDefault();
            }
            return l.Name;
        }
    }
}
