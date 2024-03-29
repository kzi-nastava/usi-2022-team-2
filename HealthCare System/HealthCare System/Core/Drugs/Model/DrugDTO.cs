﻿using HealthCare_System.Core.Ingredients.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Core.Drugs.Model
{
    public class DrugDto
    {
        int id;
        string name;
        List<Ingredient> ingredients;
        DrugStatus status;
        string message;

        public DrugDto(int id, string name, List<Ingredient> ingredients, DrugStatus status, string message)
        {
            this.id = id;
            this.name = name;
            this.ingredients = ingredients;
            this.status = status;
            this.message = message;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public List<Ingredient> Ingredients { get => ingredients; set => ingredients = value; }
        public DrugStatus Status { get => status; set => status = value; }
        public string Message { get => message; set => message = value; }
    }
}
