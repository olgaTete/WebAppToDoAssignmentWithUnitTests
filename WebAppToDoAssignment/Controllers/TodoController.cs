using WebAppToDoAssignment.Date;
using WebAppToDoAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace WebAppToDoAssignment.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoService _todoService;
        private readonly PeopleService _peopleService;

        public TodoController(TodoService todoService, PeopleService peopleService)
        {
            _todoService = todoService;
            _peopleService = peopleService;
        }

        public IActionResult Index()
        {
            var todos = _todoService.FindAll();
            ViewBag.People = _peopleService.FindAll();  // Add this line to load people for dropdown
            return View(todos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var people = _peopleService.FindAll();
            ViewBag.People = people;
            return View();
        }

        [HttpPost]
        public IActionResult Create(string description, bool done, int assigneeId)
        {
            var assignee = _peopleService.FindById(assigneeId);
            if (assignee == null)
            {
                ModelState.AddModelError("", "Invalid assignee.");
                var people = _peopleService.FindAll();
                ViewBag.People = people;
                return View();
            }

            _todoService.CreateTodoItem(description, done, assignee);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var todo = _todoService.FindById(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        public IActionResult Delete(int id)
        {
            var todo = _todoService.FindById(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            _todoService.RemoveTodoItem(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ByDoneStatus(bool? doneStatus)
        {
            Todo[] todos;
            if (doneStatus.HasValue)
            {
                todos = _todoService.FindByDoneStatus(doneStatus.Value);  // Filter by done status
            }
            else
            {
                todos = _todoService.FindAll();  // If no status selected, return all todos
            }

            ViewBag.People = _peopleService.FindAll();  // Load the list of people for assignee filter, if needed
            return View("Index", todos);  // Reuse the Index view to display filtered todos
        }

        public IActionResult ByAssignee(int? assigneeId)
        {
            var todos = assigneeId.HasValue && assigneeId.Value > 0
                ? _todoService.FindByAssignee(assigneeId.Value)  // Filter by selected assignee
                : _todoService.FindAll();  // No assignee selected, return all todos

            // Pass list of people for the dropdown
            ViewBag.People = _peopleService.FindAll();

            return View("Index", todos);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var todo = _todoService.FindById(id);
            if (todo == null)
            {
                return NotFound();
            }
            var people = _peopleService.FindAll();
            ViewBag.People = people;
            return View(todo);
        }

        [HttpPost]
        public IActionResult Edit(int id, string description, bool done, int assigneeId)
        {
            var todo = _todoService.FindById(id);
            if (todo == null)
            {
                return NotFound();
            }

            var assignee = _peopleService.FindById(assigneeId);
            if (assignee == null)
            {
                ModelState.AddModelError("", "Invalid assignee.");
                var people = _peopleService.FindAll();
                ViewBag.People = people;
                return View(todo);
            }

            todo.Description = description;
            todo.Done = done;
            todo.Assignee = assignee;

            _todoService.UpdateTodoItem(todo);
            return RedirectToAction(nameof(Index));
        }
    }
}
