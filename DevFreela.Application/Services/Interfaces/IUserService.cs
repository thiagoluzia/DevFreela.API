﻿using DevFreela.Application.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Interfaces
{
    public  interface IUserService
    {
        UserViewModel GetUser(int id);
        int Create(CreateUserInputModel inputModel);
    }
}
