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
namespace UNI_Management.Service;

[TransientDependency(ServiceType = typeof(ILoginRepository))]
public class LoginRepository : ILoginRepository
{

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
}
