using Microsoft.AspNetCore.Mvc;
using ConnectDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace ConnectDatabase.Controllers
{
    [Route("api/demo")]
    [ApiController]
    public class ApiController : Controller
    {
        private readonly ApiDbContext _apiDbContext;

        public ApiController(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        // Lấy danh sách người dùng
        [HttpGet]
        [Route("get-users-list")]
        public async Task<IActionResult> GetAsync()
        {
            var users = await _apiDbContext.users.ToListAsync();
            return Ok(users);
        }

        // Lấy người dùng theo ID
        [HttpGet]
        [Route("get-user-by-id/{UserId}")]
        public async Task<IActionResult> GetUserByIdAsync(int UserId)
        {
            var user = await _apiDbContext.users.FindAsync(UserId);
            if (user == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy người dùng
            }
            return Ok(user);
        }

        // Thêm người dùng mới
        [HttpPost]
        [Route("add-user")]
        public async Task<IActionResult> PostAsync(Users user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Trả về lỗi 400 nếu dữ liệu không hợp lệ
            }

            _apiDbContext.users.Add(user);
            await _apiDbContext.SaveChangesAsync();
            return Created($"/get-user-by-id/{user.StudentID}", user);
        }

        // Cập nhật thông tin người dùng
        [HttpPut]
        [Route("update-user/{UserId}")]
        public async Task<IActionResult> PutAsync(int UserId, Users userToUpdate)
        {
            if (UserId != userToUpdate.StudentID || !_apiDbContext.users.Any(u => u.StudentID == UserId))
            {
                return BadRequest("Invalid user ID"); // Trả về lỗi 400 nếu ID không hợp lệ hoặc không tìm thấy người dùng
            }

            _apiDbContext.users.Update(userToUpdate);
            await _apiDbContext.SaveChangesAsync();
            return NoContent();
        }

        // Xóa người dùng
        [HttpDelete]
        [Route("delete-user/{UserId}")]
        public async Task<IActionResult> DeleteAsync(int UserId)
        {
            var userToDelete = await _apiDbContext.users.FindAsync(UserId);
            if (userToDelete == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy người dùng
            }

            _apiDbContext.users.Remove(userToDelete);
            await _apiDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
