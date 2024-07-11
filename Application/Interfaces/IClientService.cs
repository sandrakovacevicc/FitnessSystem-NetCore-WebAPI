using FitnessSystem.Application.DTOs.Client;
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
        Task<ClientAddDto> CreateClientAsync(ClientAddDto clientAddDto);
        Task<ClientDeleteDto> DeleteClientAsync(string JMBG);
        Task<ClientDto> UpdateClientAsync(string jmbg, ClientUpdateDto clientUpdateDto);
    }
}
