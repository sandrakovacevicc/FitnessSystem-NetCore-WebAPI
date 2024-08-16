using AutoMapper;
using Core.Entities;
using FitnessSystem.Application.DTOs.Admin;
using FitnessSystem.Application.DTOs.Client;
using FitnessSystem.Application.DTOs.MembershipPackage;
using FitnessSystem.Application.DTOs.Reservation;
using FitnessSystem.Application.DTOs.Room;
using FitnessSystem.Application.DTOs.Session;
using FitnessSystem.Application.DTOs.Trainer;
using FitnessSystem.Application.DTOs.TrainingProgram;
using FitnessSystem.Application.DTOs.User;
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
            CreateMap<SessionDto, Session>().ReverseMap();
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
            CreateMap<MembershipPackageUpdateDto, MembershipPackage>();
            CreateMap<MembershipPackage, MembershipPackageUpdateDto>();
            CreateMap<TrainingProgramDto, TrainingProgram>();
            CreateMap<UserDto, User>();
            CreateMap<TrainerDto, Trainer>();
            CreateMap<TrainerUpdateDto, Trainer>();
            CreateMap<Trainer, TrainerUpdateDto>();
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
