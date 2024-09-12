using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Application.DTOs.Client;
using FitnessSystem.Application.DTOs.TrainingProgram;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ClientDto>> GetAllAsync()
        {
            var clients =  _unitOfWork.Clients.GetAll("MembershipPackage").ToList();
            return _mapper.Map<List<ClientDto>>(clients);
        }

        public async Task<ClientDto> GetByIdAsync(string jmbg)
        {
            var client = await _unitOfWork.Clients.GetByIdAsync(jmbg);
            return _mapper.Map<ClientDto>(client);
        }

        public async Task<ClientDto> UpdateClientAsync(string jmbg, ClientUpdateDto clientUpdateDto)
        {
            var client = await _unitOfWork.Clients.GetByIdAsync(jmbg);
            if (client == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            client.Name = clientUpdateDto.Name;
            client.Surname = clientUpdateDto.Surname;
            client.Email = clientUpdateDto.Email;
            client.Birthdate = clientUpdateDto.Birthdate;
            client.MembershipPackageId = clientUpdateDto.MembershipPackageId;
            client.IsPaid = clientUpdateDto.IsPaid;
            client.MobileNumber = clientUpdateDto.MobileNumber;
            await _unitOfWork.Clients.UpdateAsync(client, jmbg);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ClientDto>(client);
        }

        public async Task<IEnumerable<ClientDto>> SearchClientsAsync(string searchTerm)
        {
            var clients = await _unitOfWork.Clients.SearchClientsAsync(searchTerm);
            return _mapper.Map<IEnumerable<ClientDto>>(clients);
        }
    }
}
