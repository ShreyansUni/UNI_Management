using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNI_Management.DataModels;

namespace UNI_Management.Service
{
    public interface ILoginRepository
    {
        public Employee? GetUser(string email, string password);
    }
}
