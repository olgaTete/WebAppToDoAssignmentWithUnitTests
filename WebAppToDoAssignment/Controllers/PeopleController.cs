using WebAppToDoAssignment.Date;
using WebAppToDoAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace WebAppToDoAssignment.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PeopleService _peopleService;

        public PeopleController()
        {
            _peopleService = new PeopleService();
        }
        public IActionResult Index()
        {
            var people = _peopleService.FindAll();
            return View(people);
        }

        public IActionResult Create()
        {
            var people = _peopleService.FindAll();
            return View();
        }


        [HttpPost]
        public IActionResult Create(string firstName, string lastName)
        {

            _peopleService.CreatePerson(firstName, lastName);
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Details(int id)
        {
            var person = _peopleService.FindById(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }
        public IActionResult Delete(int id)
        {
            var person = _peopleService.FindById(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            _peopleService.RemovePerson(id);
            return RedirectToAction(nameof(Index));
        }
        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    var person = _peopleService.FindById(id);
        //    if (person == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(person);
        //}
    }
}