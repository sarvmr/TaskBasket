using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBasket.Data;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly TaskContext _context;

    public TaskController(TaskContext context)
    {
        _context = context;
    }

    // GET: api/Task
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskBasket.Models.Task>>> GetTasks()
    {
        return await _context.Tasks.ToListAsync();
    }

    // GET: api/Task/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskBasket.Models.Task>> GetTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        return task;
    }

    

    // POST: api/Task
    [HttpPost]
    public async Task<ActionResult<TaskBasket.Models.Task>> PostTask(TaskBasket.Models.Task task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTask", new { id = task.Id }, task);
    }

    // DELETE: api/Task/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<TaskBasket.Models.Task>> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return task;
    }

    private bool TaskExists(int id)
    {
        return _context.Tasks.Any(e => e.Id.Equals(id));
    }
}