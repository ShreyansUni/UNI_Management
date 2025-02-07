using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNI_Management.DataModels;
using UNI_Management.Domain.DataContext;

namespace UNI_Management.Service
{
    public interface IClientRepository
    {
        List<Client> GetClientList();
        Task DeleteClientAsync(int Id);
        public void AddClient(Client model);
        public void UpdateClient(Client model);
        public Client GetClientDetails(int Id);
        List<Client> GetClientListfilter(string filterName, string filterBusinessName, DateTime? filterBirthDate, string filterIsActive);
    }

}
