using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library1.Models;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Reflection.Metadata;
using Library1.Helpers;
using System.Text;
using System.Text.RegularExpressions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;

namespace Library1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BooksDBContext _context;

        public UsersController(BooksDBContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'BooksDBContext.Users'  is null.");
          }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // Authentication 
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userObj.UserName);

            if (user == null)
                return NotFound(new {Message="المستخدم غير موجود"});

            if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
                return BadRequest(new { Message = "كلمة المرور غير صحيحه" });

            user.Token = CreateJWT(user);

            return Ok(new { 
                Token=user.Token,
                Message = "تم تسجيل الدخول بنجاح" });


        }
        // register
        [HttpPost("register")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if(userObj == null)
                return BadRequest();

            // موجود مسبقاً ؟ userName التاكد من ان
            if(await CheckUserNameExistAsync(userObj.UserName))
                return BadRequest(new {Message="! إسم المستخدم موجود بالفعل"});

            // موجود مسبقاً ؟ Email التاكد من ان
            if (await CheckEmailExistAsync(userObj.Email))
                return BadRequest(new { Message = "! البريد الالكتروني موجود بالفعل" });

            //password  موجود مسبقاً ؟  ومعرفة قوة ال password التاكد من ان
            var pass= CheckPasswordStrength(userObj.Password);
            if(!string.IsNullOrEmpty(pass))
                return BadRequest(new {Message=pass.ToString()});



            userObj.Password=PasswordHasher.HashPassword(userObj.Password);
            userObj.Role = "User";
            userObj.Token = "";
            await _context.Users.AddAsync(userObj);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Message = "تم تسجيل المستخدم"
            });
        }

        private Task<bool> CheckUserNameExistAsync(string userName)
            => _context.Users.AnyAsync(x => x.UserName == userName);

        private Task<bool> CheckEmailExistAsync(string email)
            => _context.Users.AnyAsync(x => x.Email == email);


        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb= new StringBuilder();
            if(password.Length<8)
                sb.Append("يجب ان تحتوي كلمة المرور على اكثر من 8 احرف او ارقام"+Environment.NewLine);
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
                sb.Append("يجب ان تحتوي كلمة المرور على احرف صغيره واحرف كبيره و ارقام" + Environment.NewLine);
            if (!Regex.IsMatch(password, "[<,>,@,!,#,$,%,&,*,(,),_,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]"))
                sb.Append("يجب ان تحتوي كلمة المرور على رموز " + Environment.NewLine);

            return sb.ToString();
        }

        private string CreateJWT(User user)
        {
            var jwtTokenHandlar = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Ahmed A. Alabdli - 772189175 - eng.ahmedalabdli@gmail.com");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.Name,$"{user.FullName}"),
            });

            var credentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials,
            };

            var token=jwtTokenHandlar.CreateToken(tokenDescriptor);
            return jwtTokenHandlar.WriteToken(token);
        }
        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
