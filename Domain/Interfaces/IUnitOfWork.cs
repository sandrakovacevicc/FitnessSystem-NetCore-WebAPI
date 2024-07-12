using Core.Interfaces;
using System.Threading.Tasks;

namespace FitnessSystem.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IAdminRepository Admins { get; }
        IClientRepository Clients { get; }
        ITrainerRepository Trainers { get; }
        IMembershipPackageRepository MembershipPackages { get; }
        ITrainingProgramRepository TrainingPrograms { get; }
        ISessionRepository Sessions { get; }
        IRoomRepository Rooms { get; }
        IReservationRepository Reservations { get; }
        Task CompleteAsync();
    }
}
