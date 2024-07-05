using FitnessSystem.Application.DTOs;
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
        Task<ClientAddDto> UpdateClientAsync(string jmbg, ClientUpdateDto clientUpdateDto);
    }
}
