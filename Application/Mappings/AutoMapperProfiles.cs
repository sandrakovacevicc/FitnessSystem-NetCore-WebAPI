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
            CreateMap<AdminDto, Admin>();
            CreateMap<Trainer, TrainerDto>().ReverseMap();
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<Reservation, ReservationDto>().ReverseMap();
            CreateMap<RoomDto, Room>();
            CreateMap<MembershipPackageDto, MembershipPackage>();
            CreateMap<TrainingProgramDto, TrainingProgram>();
            CreateMap<UserDto, User>();
            CreateMap<TrainerDto, Trainer>();
            CreateMap<ClientDto, Client>();
            CreateMap<ClientAddDto, Client>();
            CreateMap<Client,ClientAddDto>();
            CreateMap<TrainerAddDto, Trainer>();
            CreateMap<Trainer, TrainerAddDto>();
            CreateMap<ReservationAddDto, Reservation>();
            CreateMap<Reservation, ReservationAddDto>();
            CreateMap<SessionAddDto, Session>();
            CreateMap<Session, SessionAddDto>();
            CreateMap<User, UserDeleteDto>();
            CreateMap<Trainer, TrainerDeleteDto>();
            CreateMap<Admin, AdminDeleteDto>();
            CreateMap<Session, SessionDeleteDto>();
            CreateMap<Client, ClientDeleteDto>();
            CreateMap<MembershipPackage, MembershipPackageDeleteDto>();
            CreateMap<Room, RoomDeleteDto>();
            CreateMap<Reservation, ReservationDeleteDto>();
            CreateMap<TrainingProgram, TrainingProgramDeleteDto>();
            
         

        }
    }
}
