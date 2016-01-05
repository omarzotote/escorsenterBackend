using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using versusBackend.Models;

namespace versusBackend.Controllers
{
    public class RegionController : Controller
    {
        // GET: Regions
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult League(int regionId)
        {
            region region = null;
            using(var context = new escorcenterdbEntities() )
            {
                region = context.regions.Find(regionId);
            }
            if(region != null)
            {
                ViewBag.regionName = region.Name;
            }
            return View();
        }
    }
}