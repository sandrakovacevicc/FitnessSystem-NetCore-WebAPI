﻿using FitnessSystem.Application.DTOs.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Interfaces
{
    public interface IAdminService
    {
        Task<List<AdminDto>> GetAllAsync();
        Task<AdminDto> GetByIdAsync(string JMBG);
        

    }
}
