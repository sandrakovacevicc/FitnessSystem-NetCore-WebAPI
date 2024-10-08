﻿using Core.Entities;
using FitnessSystem.Application.DTOs.Room;
using FitnessSystem.Application.DTOs.Trainer;
using FitnessSystem.Application.DTOs.TrainingProgram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.DTOs.Session
{
    public class SessionDto
    {
        public int SessionId {  get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int Duration { get; set; }
        public int Capacity { get; set; }
        public TrainingProgramDto TrainingProgram { get; set; }
        public TrainerDto Trainer { get; set; }
        public RoomDto Room { get; set; }

    }
}
