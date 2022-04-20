﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare_System.entities
{
    class Anamnesis
    {
        int id;
        string description;

        public Anamnesis() { }

        public Anamnesis(int id, string description)
        {
            this.id = id;
            this.description = description;
        }

        public Anamnesis(Anamnesis anamnesis)
        {
            id = anamnesis.id;
            description = anamnesis.description;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("description")]
        public string Description { get => description; set => description = value; }

        public override string ToString()
        {
            return "Anamnesis[" + "id: " + id + ", description: " + description + "]";
        }
    }
}