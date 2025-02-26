using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNI_Management.Domain.DataModels;

namespace UNI_Management.Service
{
    public interface ILoginRepository
    {
        public Employee? GetUser(string email, string password);

        bool ChangeUserPassword(string Email, string oldPassword, string newpassword, string confirmpassword);

        Employee? GetUserByEmail(string email);

        bool checkEmail(string email, string randomPassword, string passwordHash);
    }
}
