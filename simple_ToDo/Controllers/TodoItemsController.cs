using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using simple_ToDo.Data;
using simple_ToDo.Models;
using simple_ToDo.Models.ViewModels;

namespace simple_ToDo.Controllers
{
    [Authorize]
    public class TodoItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoItemsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TodoItems
        public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var items = await _context.TodoItem
                .Include(s => s.TodoStatus)
                .Where(s => s.ApplicationUserId == user.Id)
                .ToListAsync();

            return View(items);
        }

        // GET: TodoItems/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TodoItems/Create
        public async Task<ActionResult> Create()
        {
            var allStatuses = await _context.ToDoStatus
               .Select(d => new SelectListItem() { Text = d.Title, Value = d.Id.ToString() })
               .ToListAsync();

            var viewModel = new TodoItemViewModel();
            viewModel.ToDoStatusOptions = allStatuses;
            return View(viewModel);
        }

        // POST: TodoItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TodoItemViewModel viewItem)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                var item = new TodoItem()
                {
                    Title = viewItem.Title,
                    TodoStatusId = viewItem.TodoStatusId
                };
                item.ApplicationUserId = user.Id;

                _context.TodoItem.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoItems/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TodoItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoItems/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TodoItems/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}