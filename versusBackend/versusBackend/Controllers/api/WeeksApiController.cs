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
    public class WeeksApiController : ApiController
    {
        [HttpPost]
        [Route("api/week/add")]
        public IHttpActionResult CreateWeek(WeekDTO _week)
        {
            week w = new week { dateFrom = DateTime.Now, dateTo = new DateTime(2016, 02, 29), enabled = true, description = _week.Description, season = (int)_week.Season, title = _week.Title };
            using (var context = new escorcenterdbEntities())
            {
                context.weeks.Add(w);
                context.SaveChanges();
            }
            return Ok(w);
        }

        [HttpGet]
        [Route("api/week/{id}/delete")]
        public IHttpActionResult DeleteWeek(long id)
        {
            week w;
            using (var context = new escorcenterdbEntities())
            {
                w = (from r in context.weeks where r.id == id select r).FirstOrDefault();
                context.SaveChanges();
            }
            return Ok(w);
        }

        [HttpGet]
        [Route("api/week/update")]
        public IHttpActionResult UpdateWeeks([FromBody]WeekDTO _week)
        {
            week weekMap = AutoMapper.Mapper.Map<WeekDTO, week>(_week);
            week w;
            using (var context = new escorcenterdbEntities())
            {
                w = (from r in context.weeks where r.id == weekMap.id select r).FirstOrDefault();
                w = weekMap;
                context.SaveChanges();
            }
            return Ok(w);
        }

        [HttpGet]
        [Route("api/week/matches/{id}")]
        public IHttpActionResult GetWeekId(long id)
        {
            int weekId = getWeekId(id);
            week _week;
            using (var context = new escorcenterdbEntities())
            {
                _week = (from w in context.weeks where w.id == weekId select w).FirstOrDefault<week>();
            }
            return Ok(_week);
        }

        public static int getWeekId(long id)
        {
            int weekId = 0;
            using (var context = new escorcenterdbEntities())
            {
                weekId = (from w in context.weeks where w.id == id select w.id).FirstOrDefault<int>();
            }
            return weekId;

        }
    }
}
