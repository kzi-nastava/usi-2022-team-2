﻿using HealthCare_System.Repository.AnamnesisRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Model;

namespace HealthCare_System.Services.AnamnesisService
{
    class AnamnesisService
    {
        AnamnesisRepo anamnesisRepo;

        public AnamnesisService()
        {
            AnamnesisRepoFactory anamnesisRepoFactory = new();
            anamnesisRepo = anamnesisRepoFactory.CreateAnamnesisRepository();
        }

        internal AnamnesisRepo AnamnesisRepo { get => anamnesisRepo; }

        public List<Anamnesis> Anamneses()
        {
            return anamnesisRepo.Anamneses;
        }
    }
}
