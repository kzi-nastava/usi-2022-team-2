﻿using HealthCare_System.Core.Anamneses.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Anamneses.Repository
{
    public interface IAnamnesisRepo
    {
        string Path { get; set; }

        List<Anamnesis> Anamneses { get; set; }
        Anamnesis FindById(int id);
        int GenerateId();
        void Serialize();
        void Update(int id, string description);

        public void Add(Anamnesis anamnesis);
    }
}