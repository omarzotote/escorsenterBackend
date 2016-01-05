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
    public class FieldApiController : ApiController
    {
        [HttpPost]
        [Route("api/fields/add")]
        public IHttpActionResult CreateField([FromBody]FieldDTO field)
        {
            field f = new field { address = field.Address, description = field.Description, enabled = true, latitude = (float)field.latitude, longitude = (float)field.longitude, name = field.Name, sport = (int)field.sport };
            using (var context = new escorcenterdbEntities())
            {
                context.fields.Add(f);
                context.SaveChanges();
            }
            return Ok(f);
        }

        [HttpGet]
        [Route("api/fields/{id}/delete")]
        public IHttpActionResult DeleteField(long id)
        {
            field f;
            using (var context = new escorcenterdbEntities())
            {
                f = (from r in context.fields where r.Id == id select r).FirstOrDefault();
                context.SaveChanges();
            }
            return Ok(f);
        }

        [HttpGet]
        [Route("api/fields/update")]
        public IHttpActionResult UpdateField([FromBody] FieldDTO _field)
        {
            field fieldMap = AutoMapper.Mapper.Map<FieldDTO, field>(_field);
            field f;
            using (var context = new escorcenterdbEntities())
            {
                f = (from r in context.fields where r.Id == fieldMap.Id select r).FirstOrDefault();
                f = fieldMap;
                context.SaveChanges();
            }
            return Ok(f);
        }
    }
}
