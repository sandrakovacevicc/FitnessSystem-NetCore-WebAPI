﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.DTOs.MembershipPackage
{
    public class MembershipPackageDto
    {
        public int MembershipPackageId {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int NumberOfMonths { get; set; }

    }
}
