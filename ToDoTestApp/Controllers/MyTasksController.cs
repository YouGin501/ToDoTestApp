﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoTestApp.Data;
using ToDoTestApp.DTO;
using ToDoTestApp.Models;
using ToDoTestApp.Services.Contracts;

namespace ToDoTestApp.Controllers
{
    public class MyTasksController : Controller
    {
        private readonly IMyTaskService _service;
        public MyTasksController(IMyTaskService service)
        {
            _service = service;
        }

        // GET: MyTasks
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(await _service.GetTasksForUser(userId, null));
        }

        // GET: MyTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myTaskDTO = await _service.GetTaskById(id.Value);
            if (myTaskDTO == null)
            {
                return NotFound();
            }

            return View(myTaskDTO);
        }

        // GET: MyTasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MyTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,Done,Deleted,LevelOfImportance")] MyTaskDTO myTaskDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _service.AddTask(myTaskDTO, userId);
            return RedirectToAction("Index");
        }

        // GET: MyTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myTaskDTO = await _service.GetTaskById(id.Value);
            if (myTaskDTO == null)
            {
                return NotFound();
            }
            return View(myTaskDTO);
        }

        // POST: MyTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,Done,Deleted,LevelOfImportance")] MyTaskDTO myTaskDTO)
        {
            if (id != myTaskDTO.ID)
            {
                return NotFound();
            }

            var result = await _service.UpdateTask(id, myTaskDTO);
            return RedirectToAction("Index");
        }

        // GET: MyTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myTaskDTO = await _service.GetTaskById(id.Value);
            if (myTaskDTO == null)
            {
                return NotFound();
            }

            return View(myTaskDTO);
        }

        // POST: MyTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteTask(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
