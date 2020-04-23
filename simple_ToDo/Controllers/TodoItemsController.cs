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
        public async Task<ActionResult> Index(string status)
        {
            var user = await GetCurrentUserAsync();
            List<TodoItem> items = new List<TodoItem>();
            if (status == "finished")
            {
             items = await _context.TodoItem
                    .Include(s => s.TodoStatus)
                    .Where(s => s.ApplicationUserId == user.Id)
                    .Where(s => s.TodoStatusId == 3)
                    .ToListAsync();
            }
            else if (status == "progress")
            {
                items = await _context.TodoItem
                    .Include(s => s.TodoStatus)
                    .Where(s => s.ApplicationUserId == user.Id)
                    .Where(s => s.TodoStatusId == 2)
                    .ToListAsync();
            }
            else if (status == "notStarted")
            {
                items = await _context.TodoItem
                    .Include(s => s.TodoStatus)
                    .Where(s => s.ApplicationUserId == user.Id)
                    .Where(s => s.TodoStatusId == 1)
                    .ToListAsync();
            }
            else
            {
                items = await _context.TodoItem
                    .Include(s => s.TodoStatus)
                    .Where(s => s.ApplicationUserId == user.Id)
                    .ToListAsync();
            }

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
        public async Task<ActionResult> Edit(int id)
        {
            var allStatuses = await _context.ToDoStatus
              .Select(d => new SelectListItem() { Text = d.Title, Value = d.Id.ToString() })
              .ToListAsync();
            var item = await _context.TodoItem.FirstOrDefaultAsync(i => i.Id == id);
            var loggedInUser = await GetCurrentUserAsync();

            if (item.ApplicationUserId != loggedInUser.Id)
            {
                return NotFound();
            }
            var viewModel = new TodoItemViewModel()
            {
                Id = item.Id,
                Title = item.Title,
                TodoStatusId = item.TodoStatusId,
                ToDoStatusOptions = allStatuses
            };

            return View(viewModel);
        }

        // POST: TodoItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TodoItemViewModel viewModel)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                var item = new TodoItem()
                {
                    Id = viewModel.Id,
                    Title = viewModel.Title,
                    TodoStatusId = viewModel.TodoStatusId,
                    ApplicationUserId = user.Id
                };

                _context.TodoItem.Update(item);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoItems/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var item = await _context.TodoItem.Include(s => s.TodoStatus).FirstOrDefaultAsync(i => i.Id == id);

            return View(item);
        }

        // POST: TodoItems/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, TodoItem item)
        {
            try
            {
                _context.TodoItem.Remove(item);
                await _context.SaveChangesAsync();

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