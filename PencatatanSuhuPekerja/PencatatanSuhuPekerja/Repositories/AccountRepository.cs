using Microsoft.EntityFrameworkCore;
using PencatatanSuhuPekerjaAPI.Context;
using PencatatanSuhuPekerjaAPI.Models;
using PencatatanSuhuPekerjaAPI.Services;
using PencatatanSuhuPekerjaAPI.ViewModels.AccountVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PencatatanSuhuPekerjaAPI.Repositories
{
    public class AccountRepository
    {
        private readonly MyContext _myContext;
        private readonly SendEmailService _sendEmailService;
        private readonly TokenService _tokenService;

        public AccountRepository(MyContext myContext, SendEmailService sendEmailService,TokenService tokenService)
        {
            this._myContext = myContext;
            this._sendEmailService = sendEmailService;
            this._tokenService = tokenService;
        }

        public async Task<Employee> GetProfile(string employeeId)
        {
            var employee = await _myContext.Employees
                .Include(emp => emp.User)
                .Include(emp => emp.Division).ThenInclude(div => div.Department)
                .Where(q => q.EmployeeId == employeeId)
                .FirstOrDefaultAsync();

            return employee;
        }

        public async Task<List<Employee>> GetAllEmployee()
        {
            var employees = await _myContext.Employees.ToListAsync();
            return employees;
        }

        public async Task<string> EditEmployee(string id, EditEmployeeVM employee)
        {
            var errMessage = "";

            var isExist = await _myContext.Employees.Where(q => q.PhoneNumber == employee.PhoneNumber).AnyAsync();
            if (isExist)
            {
                errMessage = "Phone Number already registered !";
                return errMessage;
            }

            var existEmployee = await _myContext.Employees.Where(q => q.EmployeeId == id).FirstOrDefaultAsync();

            existEmployee.FirstName = employee.FirstName;
            existEmployee.LastName = employee.LastName;
            existEmployee.Salary = employee.Salary;
            existEmployee.PhoneNumber = employee.PhoneNumber;
            existEmployee.DivisionId = employee.DivisionId;

            existEmployee.User.Email = employee.Email;
            existEmployee.User.PhoneNumber = employee.PhoneNumber;
            //existEmployee.User.UserRoles = employee.PhoneNumber;
            existEmployee.User.NormalizedEmail = employee.Email.ToUpper();
            existEmployee.User.NormalizedUserName = employee.UserName.ToUpper();


            var result = await _myContext.SaveChangesAsync();
            if (result == 0)
            {
                errMessage = "Server error !";
                return errMessage;
            }
            return errMessage;
        }

        public async Task<string> DeactivateEmpoyee(string id)
        {
            var errMessage = "";

            var existEmployee = await _myContext.Employees.Where(q => q.EmployeeId == id).FirstOrDefaultAsync();

            existEmployee.IsActive = false;

            var result = await _myContext.SaveChangesAsync();
            if (result == 0)
            {
                errMessage = "Server error !";
                return errMessage;
            }
            return errMessage;
        }

        public async Task<string> AddEmployee(Employee employee)
        {
            var isExist = await _myContext.Employees.Where(q => q.PhoneNumber == employee.PhoneNumber).AnyAsync();
            if (isExist)
            {
                return "Phone Number already registered !";
            }

            Employee newEmployee = new Employee()
            {
                EmployeeId = Guid.NewGuid().ToString(),
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                PhoneNumber = employee.PhoneNumber,
                IsActive = true,
                DivisionId = employee.DivisionId
            };

            _myContext.Employees.Add(newEmployee);
            var result = await _myContext.SaveChangesAsync();
            if (result == 0)
            {
                return "Server Error !";
            }

            return null;
        }

        public async Task<string> ChangePassword(string id, ChangePassowordVM changePasswordVM)
        {
            var existUser = await _myContext.Users.Where(q => q.Id == id).FirstOrDefaultAsync();
            var isValid = BCrypt.Net.BCrypt.Verify(changePasswordVM.OldPassowrd, existUser.PasswordHash);
            if (isValid)
            {
                return "Old Passowrd is wrong !";
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(changePasswordVM.NewPassword, 12);

            existUser.PasswordHash = hashedPassword;
            var result = await _myContext.SaveChangesAsync();
            if (result == 0)
            {
                return "Server error !";
            }
            return null;
        }

        public async Task<string> EditProfile(string id, EditEmployeeVM employee)
        {
            var isPhoneNumberExist = await _myContext.Employees.Where(q => q.PhoneNumber == employee.PhoneNumber).AnyAsync();
            if (isPhoneNumberExist)
            {
                return "Phone Number already registered !";
            }

            var isEmailExist = await _myContext.Employees
                        .Include("User")
                        .Where(q => q.User.Email == employee.Email)
                        .AnyAsync();

            if (isEmailExist)
            {
                return "Email already exist !";
            }

            var isUserNameExist = await _myContext.Employees
                        .Include("User")
                        .Where(q => q.User.UserName == employee.UserName)
                        .AnyAsync();

            if (isUserNameExist)
            {
                return "User Name already registered !";
            }

            var existEmployee = await _myContext.Employees
                        .Include("User")
                        .Where(q => q.EmployeeId == id)
                        .FirstOrDefaultAsync();

            existEmployee.FirstName = employee.FirstName;
            existEmployee.LastName = employee.LastName;
            existEmployee.PhoneNumber = employee.PhoneNumber;

            existEmployee.User.Email = employee.Email;
            existEmployee.User.PhoneNumber = employee.PhoneNumber;
            //existEmployee.User.UserRoles = employee.PhoneNumber;
            existEmployee.User.NormalizedEmail = employee.Email.ToUpper();
            existEmployee.User.NormalizedUserName = employee.UserName.ToUpper();

            var result = await _myContext.SaveChangesAsync();
            if (result == 0)
            {
                return "Server error !";
            }
            return null;
        }

        //public async Task<string> RegisterUser()
        //{
        //    var isExist = _myContext.Users.Where(Q => Q.Email == model.Email).Any();
        //    if (isExist)
        //    {
        //        return BadRequest("Email Already Registered !");
        //    }

        //    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password, 12);
        //    var role = _myContext.Roles.Where(Q => Q.Name == "SALES").FirstOrDefault();
        //    if (role == null)
        //    {
        //        return BadRequest("Default Role Data Not Exist !");
        //    }
        //    var userId = Guid.NewGuid().ToString();

        //    var rand = new Random();
        //    var emailRandomCode = rand.Next(0, 9999).ToString("D4");

        //    sendEmailService.SendEmail(model.Email, emailRandomCode);

        //    User user = new User()
        //    {
        //        Id = model.Id,
        //        UserName = model.Email,
        //        Email = model.Email,
        //        NormalizedEmail = model.Email.ToUpper(),
        //        SecurityStamp = emailRandomCode,
        //        EmailConfirmed = false,
        //        PasswordHash = hashedPassword,
        //        PhoneNumber = null,
        //        PhoneNumberConfirmed = false,
        //        TwoFactorEnabled = false,
        //        LockoutEnabled = false,
        //        AccessFailedCount = 0
        //    };

        //    UserRole userRole = new UserRole()
        //    {
        //        UserId = model.Id,
        //        RoleId = role.Id
        //    };

        //    _myContext.Users.Add(user);
        //    _myContext.UserRoles.Add(userRole);
        //    var result = await _myContext.SaveChangesAsync();
        //    if (result == 0)
        //    {
        //        return BadRequest("Server Error !");
        //    }
        //    return Ok("Successfully Created");
        //}

        public async Task<(string errMessage, string token)> Login(LoginVM loginVM)
        {
            var existUser = await _myContext.Users.Where(Q => Q.Email == loginVM.Email || Q.UserName == loginVM.Email).FirstOrDefaultAsync();
            if (existUser == null)
            {
                return ("User not registered !",null);
            }

            var isValid = BCrypt.Net.BCrypt.Verify(loginVM.Password, existUser.PasswordHash);
            if (!isValid)
            {
                return ("Password not match !", null);
            }

            var userRole = await _myContext.UserRoles.Where(Q => Q.UserId == existUser.Id).Select(Q => Q.RoleId).ToListAsync();
            if (userRole == null)
            {
                return ("Retrieving User Role Data Failed !", null);
            }

            var role = await _myContext.Roles.Where(Q => userRole.Any(X => X == Q.Id)).ToListAsync();
            if (role == null)
            {
                return ("Retrieving Role Data Failed !", null);
            }
            var roleName = role.Select(Q => Q.Name).ToList();
            var stringRoles = String.Join(",", roleName);

            var claims = new List<Claim> {
                new Claim("Id",existUser.Id),
                new Claim("Role",stringRoles),
                new Claim("UserName", existUser.UserName),
                new Claim("Email", existUser.Email),
                new Claim("IsVerified", existUser.EmailConfirmed.ToString())
             };

            var JWToken = _tokenService.GenerateAccessToken(claims);

            return (null, JWToken);
        }

        public async Task<string> Verify(string id, string code)
        {
            var isTrue = await _myContext.Users.Where(Q => Q.SecurityStamp == code).AnyAsync();
            if (!isTrue)
            {
                return "Verification Code is Wrong !";
            }

            var user = await _myContext.Users.Where(Q => Q.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return "Retrieving User Data Failed !";
            }

            user.SecurityStamp = null;
            user.EmailConfirmed = true;

            var result = await _myContext.SaveChangesAsync();
            if (result == 0)
            {
                return "Server Error !";
            }

            return null;
        }

    }
}
