using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerAPI.Controllers;
using TaskManagerAPI.Data; 
using TaskManagerAPI.Models;

namespace TaskManager.Tests
{
    public class TaskItemsControllerTests
    {
        private AppDbContext _context = null!;
        private TaskItemsController _controller = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique DB per test run
                .Options;

            _context = new AppDbContext(options);

            _context.TaskItems.AddRange(
                new TaskItem { TaskItemId = 1, Title = "Task 1", Description = "Desc 1", Priority = Priority.Medium, Status = Status.Pending, DueDate = DateTime.Now.AddDays(1) },
                new TaskItem { TaskItemId = 2, Title = "Task 2", Description = "Desc 2", Priority = Priority.High, Status = Status.Completed, DueDate = DateTime.Now.AddDays(2) }
            );
            _context.SaveChanges();

            _controller = new TaskItemsController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose(); 
        }

        [Test]
        public async Task GetTaskItems_Returns_AllTasks()
        {
            var result = await _controller.GetTasks();
            Assert.That(result.Value.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetTaskItem_WithInvalidId_ReturnsNotFound()
        {
            var result = await _controller.GetTask(999);
            Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task CreateTask_AddsNewTask()
        {
            var newTask = new TaskItem
            {
                Title = "New Task",
                Description = "New Desc",
                Priority = Priority.Low,
                Status = Status.Pending,
                DueDate = DateTime.Now.AddDays(3)
            };

            var result = await _controller.CreateTask(newTask);
            var createdResult = result.Result as CreatedAtActionResult;

            Assert.That(createdResult, Is.Not.Null);
            var createdTask = createdResult!.Value as TaskItem;
            Assert.That(createdTask!.Title, Is.EqualTo("New Task"));
            Assert.That(_context.TaskItems.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task UpdateTask_WithValidId_UpdatesTask()
        {
            var task = _context.TaskItems.First();
            task.Title = "Updated Title";

            var result = await _controller.UpdateTask(task.TaskItemId, task);
            Assert.That(result, Is.TypeOf<NoContentResult>());

            var updatedTask = _context.TaskItems.Find(task.TaskItemId);
            Assert.That(updatedTask!.Title, Is.EqualTo("Updated Title"));
        }

        [Test]
        public async Task DeleteTask_WithValidId_DeletesTask()
        {
            var result = await _controller.DeleteTask(1);
            Assert.That(result, Is.TypeOf<NoContentResult>());
            Assert.That(_context.TaskItems.Find(1), Is.Null);
        }

        [Test]
        public async Task DeleteTask_WithInvalidId_ReturnsNotFound()
        {
            var result = await _controller.DeleteTask(999);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }
    }
}
