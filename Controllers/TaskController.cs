using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBasket.Data;
using System.Security.Claims;


[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class TaskController : ControllerBase
{
    private readonly TaskContext _context;

    public TaskController(TaskContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskBasket.Models.Task>>> GetTasks()
    {
        // Get the logged-in user's ID
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (claim == null || string.IsNullOrEmpty(claim.Value))
        {
            return Unauthorized("User is not authenticated or NameIdentifier claim is missing.");
        }

        var userId = int.Parse(claim.Value);

        // Fetch tasks belonging to the user
        var tasks = await _context.Tasks
            .Where(t => t.UserId == userId)
            .ToListAsync();

        return Ok(tasks);
    }

    // GET: api/Task/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskBasket.Models.Task>> GetTask(int id)
    {
        // Get the logged-in user's ID
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (task == null)
        {
            return NotFound("Task not found or you don't have permission to view it.");
        }

        return Ok(task);
    }



    // POST: api/Task
    [HttpPost]
    public async Task<ActionResult<TaskBasket.Models.Task>> PostTask(TaskBasket.Models.Task task)
    {
        // Get the logged-in user's ID using the correct claim type
        var claim = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
        if (claim == null || string.IsNullOrEmpty(claim.Value))
        {
            return Unauthorized("User is not authenticated or NameIdentifier claim is missing.");
        }
        var userId = int.Parse(claim.Value);

        // Associate the task with the user
        task.UserId = userId;

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
    }

    // DELETE: api/Task/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<TaskBasket.Models.Task>> DeleteTask(int id)
    {
        // Get the logged-in user's ID
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (task == null)
        {
            return NotFound("Task not found or you don't have permission to delete it.");
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TaskExists(int id)
    {
        return _context.Tasks.Any(e => e.Id.Equals(id));
    }

    // PUT: api/Task/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTask(int id, TaskBasket.Models.Task updatedTask)
    {
        // Get the logged-in user's ID
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (task == null)
        {
            return NotFound("Task not found or you don't have permission to update it.");
        }

        task.Title = updatedTask.Title;
        task.Description = updatedTask.Description;
        task.Status = updatedTask.Status;
        task.Priority = updatedTask.Priority;
        task.DueDate = updatedTask.DueDate;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}