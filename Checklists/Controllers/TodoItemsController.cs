﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Checklists.Models;
using Checklists.ViewModels;

namespace Checklists.Controllers
{
    public class TodoItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TodoItemsController()
        {
            _context = new ApplicationDbContext();
        }

        [Route("checklists/{id}/items")]
        public ActionResult Index(int? id)
        {
            if (id == null)
                return HttpNotFound();

            var checklist = _context.Checklists
                .Include(c => c.TodoItems)
                .SingleOrDefault(c => c.Id == id);

            if (checklist == null)
                return HttpNotFound();

            return View(checklist);
        }

        [Route("checklists/{id}/items/new")]
        public ActionResult New(int? id)
        {
            if (id == null)
                return HttpNotFound();

            var isChecklistExists = _context.Checklists.Any(c => c.Id == id);

            if (!isChecklistExists)
                return HttpNotFound();

            var todoItem = new TodoItem
            {
                ChecklistId = (int)id
            };

            return View(todoItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TodoItem item)
        {
            if (!ModelState.IsValid)
            {
                return View("New", item);
            }

            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return RedirectToAction("Index", new { Id = item.ChecklistId });
        }
    }
}