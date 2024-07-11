using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Application.DTOs.Client;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace FitnessSystem.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IClientRepository clientRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ClientAddDto> CreateClientAsync(ClientAddDto clientAddDto)
        {
            var client = _mapper.Map<Client>(clientAddDto);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _clientRepository.CreateAsync(client);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }

            return _mapper.Map<ClientAddDto>(client);
        }

        public async Task<ClientDeleteDto> DeleteClientAsync(string jmbg)
        {
            var client = await _clientRepository.GetByIdAsync(jmbg);
            if (client == null)
            {
                return null;
            }

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var deletedClient = await _clientRepository.DeleteAsync(jmbg);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<ClientDeleteDto>(deletedClient);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<List<ClientDto>> GetAllAsync()
        {
            var clients = _clientRepository.GetAll("MembershipPackage").ToList();

            var clientsDto = _mapper.Map<List<ClientDto>>(clients);

            return clientsDto;
        }
         
        public async Task<ClientDto> GetByIdAsync(string jmbg)
        {
            var client = await _clientRepository.GetByIdAsync(jmbg);
            return _mapper.Map<ClientDto>(client);
        }

        public async Task<ClientDto> UpdateClientAsync(string jmbg, ClientUpdateDto clientUpdateDto)
        {
            var client = await _clientRepository.GetByIdAsync(jmbg);
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

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _clientRepository.UpdateAsync(client, jmbg);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                var clientDto = _mapper.Map<ClientDto>(client);

                return clientDto;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

    }
}
