using Core;
using Site.Models;
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
        [HttpPost]
        public ActionResult Validate(PuzzleViewModel vm)
        {
            byte[] data = vm.Values.Select(v => (byte)v).ToArray();
            Puzzle p = new Puzzle(data);
            return Json( new { valid = p.Validate() });
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