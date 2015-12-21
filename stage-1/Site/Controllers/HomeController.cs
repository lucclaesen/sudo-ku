using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        private Generator _generator;

        public HomeController(Generator generator)
        {
            this._generator = generator;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Play()
        {
            var puzzle = this._generator.CreatePuzzle();   
            return View(puzzle.MatrixData);
        }

        // I am forced to use a viewmodel (no anonymous objects) and post (where a get would be more appropriate) and a cast to int[] instead of byte[] here, 
        // just in order to make the parameter binding work. That's quite a lot of concessions to make.
        // http:.../Home/Validate?{%22values%22:[1,4,0,0,2,0,9,0,3,0,2,0,0,5,9,0,0,8,0,0,0,0,0,0,0,0,0,0,8,0,5,7,3,0,6,0,0,9,4,2,8,0,0,0,7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,9,1,0,0,4,0,0,3,5,0,0,0,8,0,2]}
        [HttpGet]
        public ActionResult Validate(int[] values)
        {
            byte[] data = values.Select(v => (byte)v).ToArray();
            Puzzle p = new Puzzle(data);
            return Json( new { valid = p.Validate() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}