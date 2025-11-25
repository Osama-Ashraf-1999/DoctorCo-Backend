using ClinicApi.Data;
using ClinicApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepo;

        public UsersController(AppDbContext context, IUserRepository userRepo)
        {
            _context = context;
            _userRepo = userRepo;
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // GET: api/users/doctors
        [HttpGet("doctors")]
        public async Task<IActionResult> GetDoctors()
        {
            var doctors = await _userRepo.GetDoctorsAsync();

            return Ok(doctors.Select(doc => new
            {
                doc.userId,
                doc.fullName,
                doc.speciality,
                doc.city,
                doc.image,
                doc.role
            }));
        }
    }
}
