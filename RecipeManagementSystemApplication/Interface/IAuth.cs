﻿using RecipeManagementSystemApplication.Models;
using RecipeManagementSystemApplication.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManagementSystemApplication.Interface
{
    public interface IAuth
    {
        Task<SignUpResponse> SignUp(SignUpModel signUpModel);
    }
}
