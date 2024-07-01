using AutoMapper;
using Core.Entities;
using FitnessSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Room,RoomDto>().ReverseMap();
            CreateMap<Session,SessionDto>().ReverseMap();
            CreateMap<MembershipPackage,MembershipPackageDto>().ReverseMap();
            CreateMap<TrainingProgram,TrainingProgramDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Admin, AdminDto>().ReverseMap();
            CreateMap<Trainer, TrainerDto>().ReverseMap();
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<Reservation, ReservationDto>().ReverseMap();
        }
    }
}
