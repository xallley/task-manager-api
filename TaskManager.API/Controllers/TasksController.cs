using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Data;
using TaskManager.API.DTOs;
using TaskManager.API.Models;

namespace TaskManager.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TasksController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks(
            [FromQuery] Priority? priority = null,
            [FromQuery] bool? isCompleted = null,
            [FromQuery] DateTime? deadline = null)
        {
            var userId = GetUserId();
            var query = _context.Tasks.Where(t => t.UserId == userId);

            if (priority.HasValue)
                query = query.Where(t => t.Priority == priority.Value);

            if (isCompleted.HasValue)
                query = query.Where(t => t.IsCompleted == isCompleted.Value);

            if (deadline.HasValue)
                query = query.Where(t => t.Deadline.Date == deadline.Value.Date);

            var tasks = await query.OrderByDescending(t => t.CreatedAt).ToListAsync();
            return _mapper.Map<List<TaskDto>>(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTask(Guid id)
        {
            var userId = GetUserId();
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task == null)
                return NotFound();

            return _mapper.Map<TaskDto>(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskDto>> CreateTask(CreateTaskDto createTaskDto)
        {
            var userId = GetUserId();
            var task = _mapper.Map<TaskItem>(createTaskDto);
            task.Id = Guid.NewGuid();
            task.UserId = userId;
            task.CreatedAt = DateTime.UtcNow;
            task.UpdatedAt = DateTime.UtcNow;

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, _mapper.Map<TaskDto>(task));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, UpdateTaskDto updateTaskDto)
        {
            var userId = GetUserId();
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task == null)
                return NotFound();

            _mapper.Map(updateTaskDto, task);
            task.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public async Task<IActionResult> CompleteTask(Guid id)
        {
            var userId = GetUserId();
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task == null)
                return NotFound();

            task.IsCompleted = true;
            task.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var userId = GetUserId();
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task == null)
                return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new InvalidOperationException("User ID not found in claims");

            return Guid.Parse(userIdClaim.Value);
        }
    }
} 