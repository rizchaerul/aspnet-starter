using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.DB.Entities;

namespace WebApp.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public TestController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet("test")]
    public async Task<ActionResult> GetTest()
    {
        return Ok(await _db.Users.CountAsync());
    }
}
