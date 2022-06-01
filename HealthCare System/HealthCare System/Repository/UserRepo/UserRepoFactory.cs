﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.UserRepo
{
    class UserRepoFactory
    {
        private UserRepo userRepo;

        public UserRepo CreateUserRepository()
        {
            if (userRepo == null)
                userRepo = new UserRepo();

            return userRepo;
        }
    }
}