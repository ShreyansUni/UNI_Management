using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNI_Management.Domain.DataContext;
using Microsoft.AspNetCore.Http;
using UNI_Management.Common.DependencyInjection;
using UNI_Management.Service.Authentication;
using UNI_Management.Domain.DataModels;
using UNI_Management.Common.Email;
using System.Security.Policy;
using Microsoft.AspNetCore.Identity;
namespace UNI_Management.Service;

[TransientDependency(ServiceType = typeof(ILoginRepository))]
public class LoginRepository : ILoginRepository
{
    private static readonly PasswordHasher<string> hasher = new PasswordHasher<string>();

    #region Constructor
    private readonly ApplicationDbContext _context;
    public LoginRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    #endregion
    public Employee? GetUser(string email, string password)
    {
        Employee user = _context.Employees.FirstOrDefault(emp => emp.Email == email && emp.Password == password);
        return user;
    }
    
    public Employee? GetUserByEmail(string email)
    {
        Employee user = _context.Employees.FirstOrDefault(emp => emp.Email == email);
        return user;
    }

    public bool checkEmail(string email, string randomPassword, string passwordHash)
    {
        try
        {
            Employee checkEmail = _context.Employees.FirstOrDefault(emp => emp.Email == email);
            if (checkEmail != null)
            {
                var user = GetUserByEmail(email);
                if(user != null)
                {
                    user.Password = randomPassword;
                    _context.Employees.Update(user);
                    _context.SaveChanges();
                }
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool ChangeUserPassword(string Email, string oldPassword, string newpassword, string confirmpassword)
    {
        var User = GetUserByEmail(Email);
        if (User != null)
        {
            if(User.Password == oldPassword)
            {
                if (newpassword == confirmpassword)
                {
                    var newPasswordHash = hasher.HashPassword(null, newpassword);
                    User.Password = newPasswordHash;
                    _context.Employees.Update(User);
                    _context.SaveChanges();
                    return true;
                }
            }
        }
        return false ;
    }
}
