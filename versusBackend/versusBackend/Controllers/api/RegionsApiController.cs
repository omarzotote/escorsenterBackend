using Common.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using versusBackend.Models;

namespace versusBackend.Controllers.api
{
    public class RegionsApiController : ApiController
    {
        private escorcenterdbEntities db = new escorcenterdbEntities();

        // GET: api/Regions
        [HttpGet]
        [Route("api/regions")]
        public IHttpActionResult GetRegions()
        {
            IQueryable<region> regions = db.regions;
            List<RegionDTO> regionDTOs = new List<RegionDTO>();
            foreach (region r in regions)
            {
                regionDTOs.Add(AutoMapper.Mapper.Map<RegionDTO>(r));
            }
            RegionListDTO regionList = new RegionListDTO();
            regionList.RegionList.AddRange(regionDTOs);
            return Ok(regionList);
        }
        
        [HttpGet]
        [Route("api/regions/add")]
        public IHttpActionResult AddRegion()
        {
            region r = new region() { Name = "Aguascalientes", Description = "[Insertar descripción]", AreaLimitsText = "[Insertar Area Límite]", Enabled = true };
            db.regions.Add(r);
            db.SaveChanges();
            return Ok(r);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}