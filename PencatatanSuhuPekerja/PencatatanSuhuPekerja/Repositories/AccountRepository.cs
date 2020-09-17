using Microsoft.EntityFrameworkCore;
using PencatatanSuhuPekerjaAPI.Context;
using PencatatanSuhuPekerjaAPI.Models;
using PencatatanSuhuPekerjaAPI.Services;
using PencatatanSuhuPekerjaAPI.ViewModels.AccountVM;
using PencatatanSuhuPekerjaAPI.ViewModels.EmployeeVM;
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

        public AccountRepository(MyContext myContext, SendEmailService sendEmailService, TokenService tokenService)
        {
            this._myContext = myContext;
            this._sendEmailService = sendEmailService;
            this._tokenService = tokenService;
        }

        public async Task<string> Register(RegisterVM registerVM)
        {
            var isEmailExist = await _myContext.Users.Where(Q => Q.Email == registerVM.Email).AnyAsync();
            if (isEmailExist)
            {
                return "Email Already Registered !";
            }

            var isPhoneNumberExist = await _myContext.Users.Where(Q => Q.PhoneNumber == registerVM.PhoneNumber).AnyAsync();
            if (isPhoneNumberExist)
            {
                return "Phone Number Already Registered !";
            }

            var isUserNameExist = await _myContext.Users.Where(Q => Q.UserName == registerVM.UserName).AnyAsync();
            if (isUserNameExist)
            {
                return "User Name Already Registered !";
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerVM.Password, 12);
            var role = _myContext.Roles.Where(Q => Q.Name == "EMPLOYEE").FirstOrDefault();
            if (role == null)
            {
                return "Default Role Data Not Exist !";
            }

            var division = _myContext.Divisions.Where(Q => Q.Name == "NOT PLACED YET").FirstOrDefault();
            if (division == null)
            {
                return "Default Division Data Not Exist !";
            }

            var userId = Guid.NewGuid().ToString();

            var rand = new Random();
            var emailRandomCode = rand.Next(0, 9999).ToString("D4");

            _sendEmailService.SendEmail(registerVM.Email, emailRandomCode);

            Employee employee = new Employee()
            {
                EmployeeId = userId,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                IsActive = true,
                PhoneNumber = registerVM.PhoneNumber,
                Salary = 0,
                DivisionId = division.Id
            };

            User user = new User()
            {
                Id = userId,
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                NormalizedEmail = registerVM.Email.ToUpper(),
                SecurityStamp = emailRandomCode,
                EmailConfirmed = false,
                PasswordHash = hashedPassword,
                PhoneNumber = registerVM.PhoneNumber,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            };

            UserRole userRole = new UserRole()
            {
                UserId = userId,
                RoleId = role.Id
            };

            _myContext.Employees.Add(employee);
            _myContext.Users.Add(user);
            _myContext.UserRoles.Add(userRole);

            var result = await _myContext.SaveChangesAsync();
            if (result == 0)
            {
                return "Server Error !";
            }
            return null;
        }

        //public async Task<string> AddEmployee(Employee employee)
        //{
        //    var isExist = await _myContext.Employees.Where(q => q.PhoneNumber == employee.PhoneNumber).AnyAsync();
        //    if (isExist)
        //    {
        //        return "Phone Number already registered !";
        //    }

        //    Employee newEmployee = new Employee()
        //    {
        //        EmployeeId = Guid.NewGuid().ToString(),
        //        FirstName = employee.FirstName,
        //        LastName = employee.LastName,
        //        Salary = employee.Salary,
        //        PhoneNumber = employee.PhoneNumber,
        //        IsActive = true,
        //        DivisionId = employee.DivisionId
        //    };

        //    _myContext.Employees.Add(newEmployee);
        //    var result = await _myContext.SaveChangesAsync();
        //    if (result == 0)
        //    {
        //        return "Server Error !";
        //    }

        //    return null;
        //}


        //Edit User Profile
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
            existEmployee.User.NormalizedEmail = employee.Email.ToUpper();
            existEmployee.User.NormalizedUserName = employee.UserName.ToUpper();

            var result = await _myContext.SaveChangesAsync();
            if (result == 0)
            {
                return "Server error !";
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
