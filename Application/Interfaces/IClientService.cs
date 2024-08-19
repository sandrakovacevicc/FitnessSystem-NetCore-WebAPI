using FitnessSystem.Application.DTOs.Client;
using FitnessSystem.Application.DTOs.TrainingProgram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Interfaces
{
    public interface IClientService
    {
        Task<List<ClientDto>> GetAllAsync();
        Task<ClientDto> GetByIdAsync(string JMBG);
        Task<ClientDto> UpdateClientAsync(string jmbg, ClientUpdateDto clientUpdateDto);
        Task<IEnumerable<ClientDto>> SearchClientsAsync(string searchTerm);


    }
}
