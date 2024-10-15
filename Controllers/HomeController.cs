using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ToDoApp.Models;

namespace ToDoApp.Controllers //Del klassen op, så det her er startsiden og den implenter et compoment der er toDOListen, frem for at alt sker på denne
{
    public class HomeController : Controller
    {
        private ToDoContext context;
        public HomeController(ToDoContext ctx) => context = ctx;


        public IActionResult Index(string id)
        {
            var filters = new Filters(id);
            ViewBag.Filters = filters;
            ViewBag.Categories = context.Categories.ToList();
            ViewBag.Statuses = context.StatusSet.ToList();
            ViewBag.DueFilterValues = Filters.DueFilterValues;
            IQueryable<ToDo> query = context.ToDoSet
                .Include(t => t.Category)
                .Include(t => t.Status);

            if (filters.HasCategory)
            { query = query.Where(t => t.CategoryId == filters.CategoryId); }
            if (filters.HasStatus)
            { query = query.Where(t => t.StatusId == filters.StatusId); }
            if (filters.HasDueAt)
            {
                var today = DateTime.Today;
                if (filters.IsPast)
                { query = query.Where(t => t.DueDate < today); }
                else if (filters.IsFuture)
                { query = query.Where(t => t.DueDate > today); }
                else if (filters.IsToday)
                { query = query.Where(t => t.DueDate == today); }
            }
            var tasks = query.OrderBy(t => t.DueDate).ToList();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = context.Categories.ToList();
            ViewBag.Statuses = context.StatusSet.ToList();
            var task = new ToDo { StatusId = "open" };
            return View(task);
        }
        [HttpPost]
        public IActionResult Add(ToDo task)
        {
            if (ModelState.IsValid)
            {
                context.ToDoSet.Add(task);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categories = context.Categories.ToList();
                ViewBag.Statuses = context.StatusSet.ToList();
                return View(task);
            }
        }
        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            var id = string.Join("-", filter);
            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
        public IActionResult MarkComplete([FromRoute]string id, ToDo selected)
        {
            selected = context.ToDoSet.Find(selected.Id);
            if (selected != null)
            {
                { selected.StatusId = "closed"; }
                context.SaveChanges();
            }
            return RedirectToAction("Index", new { ID = id });
        }
        [HttpPost]
        public  IActionResult DeleteComplete(string id)
        {
            var toDelete = context.ToDoSet.Where(t => t.StatusId == "closed");
            foreach (var task in toDelete)
            {
                context.ToDoSet.Remove(task);
            }
            context.SaveChanges();
            return RedirectToAction("Index", new { ID = id });
        }


        /*
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}
