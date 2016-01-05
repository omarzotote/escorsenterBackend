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
    public class NotificationApiController : ApiController
    {
        [HttpPost]
        [Route("api/notification/add")]
        public IHttpActionResult CreateNotification([FromBody]NotificationDTO _notification, TeamListDTO _teamList, LeagueDTO _league)
        {
            String teamsId = "";
            List<TeamDTO> teams = _teamList.items;
            foreach (TeamDTO t in teams)
            {
                teamsId = teamsId + "<" + t.Id + ">";
            }
            notification n = new notification { Content = _notification.Content, Date = DateTime.Parse(_notification.Date), Enabled = true, Title = _notification.Title, TeamsId = teamsId, LeagueId = (int)_league.Id };
            using (var context = new escorcenterdbEntities())
            {
                context.notifications.Add(n);
                context.SaveChanges();
            }
            return Ok();
        }

        [HttpGet]
        [Route("api/notification/{id}/delete")]
        public IHttpActionResult DeleteNotification(long id)
        {
            notification _notification;
            using (var context = new escorcenterdbEntities())
            {
                _notification = (from n in context.notifications where n.Id == id select n).FirstOrDefault();
                context.notifications.Remove(_notification);
                context.SaveChanges();
            }
            return Ok(_notification);
        }

        [HttpGet]
        [Route("api/notification/update")]
        public IHttpActionResult UpdateNotification([FromBody] NotificationDTO notificationDTO)
        {
            notification notificationMap = AutoMapper.Mapper.Map<NotificationDTO, notification>(notificationDTO);
            notification _notification;
            using (var context = new escorcenterdbEntities())
            {
                _notification = (from n in context.notifications where n.Id == notificationMap.Id select n).FirstOrDefault();
                _notification = notificationMap;
                context.SaveChanges();
            }
            return Ok(_notification);
        }


        [HttpGet]
        [Route("api/notifications/leagues/{*AllValues}")]
        public IHttpActionResult GetNotifications(string AllValues)
        {
            string[] values = AllValues.Split('/');
            HashSet<int> leagueId = new HashSet<int>();
            HashSet<int> teamId = new HashSet<int>();
            bool teamAppeared = false;

            foreach (string value in values)
            {
                if (value.Equals("teams"))
                {
                    teamAppeared = true;
                }
                else if (teamAppeared)
                {
                    teamId.Add(int.Parse(value));
                }
                else
                {
                    leagueId.Add(int.Parse(value));
                }
            }

            List<notification> _notifications;

            using (var context = new escorcenterdbEntities())
            {
                _notifications = ((from n in context.notifications.AsEnumerable()
                                   where IncludeNotification(n, teamId, leagueId)
                                   select n).OrderBy(n => n.Date).ToList<notification>());
            }

            if (_notifications == null)
            {
                return Ok();
            }

            _notifications = _notifications.OrderBy(n => n.Id).ToList().ToList<notification>();
            NotificationDTO[] notifications = AutoMapper.Mapper.Map<notification[], NotificationDTO[]>(_notifications.ToArray());

            NotificationListDTO notificationList = new NotificationListDTO();
            notificationList.items.AddRange(notifications);
            if (notificationList.items.Count == 0)
            {
                return Ok();
            }
            return Ok(notificationList);
        }

        private Boolean IncludeNotification(notification n, HashSet<int> teamIds, HashSet<int> leagueIds)
        {
            bool hasTeam = false;
            foreach (int t in teamIds)
            {
                if (n.TeamsId.Contains("<" + t.ToString() + ">"))
                {
                    hasTeam = true;
                }
            }
            return n.Enabled == true && (hasTeam || leagueIds.Contains(n.LeagueId));
        }      
    }
}
