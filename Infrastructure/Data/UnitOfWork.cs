using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Core.Interfaces;
using FitnessSystem.Data;
using FitnessSystem.Infrastructure.Repositories;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Admins = new AdminRepository(_context);
            Clients = new ClientRepository(_context);
            Trainers = new TrainerRepository(_context);
            MembershipPackages = new MembershipPackageRepository(_context);
            TrainingPrograms = new TrainingProgramRepository(_context);
            Sessions = new SessionRepository(_context);
            Rooms = new RoomRepository(_context);
            Reservations = new ReservationRepository(_context);
        }

        public IUserRepository Users { get; private set; }
        public IAdminRepository Admins { get; private set; }
        public IClientRepository Clients { get; private set; }
        public ITrainerRepository Trainers { get; private set; }
        public IMembershipPackageRepository MembershipPackages { get; private set; }
        public ITrainingProgramRepository TrainingPrograms { get; private set; }
        public ISessionRepository Sessions { get; private set; }
        public IRoomRepository Rooms { get; private set; }
        public IReservationRepository Reservations { get; private set; }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

