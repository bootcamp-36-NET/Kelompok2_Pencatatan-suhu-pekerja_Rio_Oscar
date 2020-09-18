using Microsoft.EntityFrameworkCore;
using PencatatanSuhuPekerjaAPI.Context;
using PencatatanSuhuPekerjaAPI.Models;
using PencatatanSuhuPekerjaAPI.ViewModels.AccountVM;
using PencatatanSuhuPekerjaAPI.ViewModels.EmployeeVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PencatatanSuhuPekerjaAPI.Repositories
{
    public class EmployeeRepository
    {
        private readonly MyContext _myContext;

        public EmployeeRepository(MyContext myContext)
        {
            this._myContext = myContext;
        }

        public async Task<UserProfileVM> GetProfile(string id)
        {
            var user = await _myContext.Users
                .Include("Employee")
                .Include("UserRole")
                .Include(emp => emp.Employee.Division).ThenInclude(div => div.Department)
                .Where(q => q.Id == id)
                .Select(q => new UserProfileVM()
                {
                    FirstName = q.Employee.FirstName,
                    LastName = q.Employee.LastName,
                    Email = q.Email,
                    Department = q.Employee.Division.Department.Name,
                    Division = q.Employee.Division.Name,
                    PhoneNumber = q.PhoneNumber,
                    UserName = q.UserName
                })
                .FirstOrDefaultAsync();

            var userRole = await _myContext.UserRoles.Where(Q => Q.UserId == id).Select(Q => Q.RoleId).ToListAsync();
            var role = await _myContext.Roles.Where(Q => userRole.Any(X => X == Q.Id)).ToListAsync();
            var roleNames = role.Select(Q => Q.Name).ToList();

            user.Roles = roleNames;

            return user;
        }

        public async Task<List<EmployeeVM>> GetAllEmployee()
        {
            var employees = await _myContext.Users
                .Include("Employee")
                .Include(u => u.Employee.Division).ThenInclude(div => div.Department)
                .Where(q=>q.Employee.IsActive != false)
                .Select(q=> new EmployeeVM()
                {
                    Id = q.Id,
                    FirstName = q.Employee.FirstName,
                    LastName = q.Employee.LastName,
                    Email = q.Email,
                    DepartmentName = q.Employee.Division.Department.Name,
                    DepartmentId = q.Employee.Division.Department.Id,
                    DivisionName = q.Employee.Division.Name,
                    DivisionId = q.Employee.Division.Id,
                    PhoneNumber = q.PhoneNumber,
                    UserName = q.UserName,
                    Salary = q.Employee.Salary 
                })
                .ToListAsync();

            return employees;
        }

        //Admin Edit other employee Profile
        public async Task<string> EditEmployee(string id, EditEmployeeVM employee)
        {

            var isExist = await _myContext.Employees.Where(q => q.PhoneNumber == employee.PhoneNumber).AnyAsync();
            if (isExist)
            {
                return "Phone Number already registered !";
            }

            var existEmployee = await _myContext.Employees.Include("User").Where(q => q.EmployeeId == id).FirstOrDefaultAsync();

            existEmployee.FirstName = employee.FirstName;
            existEmployee.LastName = employee.LastName;
            existEmployee.Salary = employee.Salary;
            existEmployee.PhoneNumber = employee.PhoneNumber;
            existEmployee.DivisionId = employee.DivisionId;

            existEmployee.User.Email = employee.Email;
            existEmployee.User.PhoneNumber = employee.PhoneNumber;
            //existEmployee.User.UserRoles = ;
            existEmployee.User.NormalizedEmail = employee.Email.ToUpper();
            existEmployee.User.NormalizedUserName = employee.UserName.ToUpper();


            var result = await _myContext.SaveChangesAsync();
            if (result == 0)
            {
                return "Server error !";
            }
            return null;
        }

        public async Task<string> DeactivateEmpoyee(string id)
        {
            var existEmployee = await _myContext.Employees.Where(q => q.EmployeeId == id).FirstOrDefaultAsync();

            existEmployee.IsActive = false;

            var result = await _myContext.SaveChangesAsync();
            if (result == 0)
            {
                return "Server error !";
            }
            return null;
        }
    }
}
