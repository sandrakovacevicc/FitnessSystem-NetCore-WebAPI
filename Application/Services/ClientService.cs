using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Application.DTOs.Client;
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

        public async Task<ClientAddDto> CreateClientAsync(ClientAddDto clientAddDto)
        {
            var client = _mapper.Map<Client>(clientAddDto);
            await _unitOfWork.Clients.CreateAsync(client);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ClientAddDto>(client);
        }

        public async Task<ClientDeleteDto> DeleteClientAsync(string jmbg)
        {
            var client = await _unitOfWork.Clients.GetByIdAsync(jmbg);
            if (client == null)
            {
                return null;
            }

            await _unitOfWork.Clients.DeleteAsync(jmbg);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ClientDeleteDto>(client);
        }

        public async Task<List<ClientDto>> GetAllAsync()
        {
            var clients = _unitOfWork.Clients.GetAll().ToList();
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
            client.MobileNumber = clientUpdateDto.MobileNumber;
            await _unitOfWork.Clients.UpdateAsync(client, jmbg);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ClientDto>(client);
        }
    }
}
