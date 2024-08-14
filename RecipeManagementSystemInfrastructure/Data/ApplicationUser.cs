using Microsoft.AspNetCore.Identity;
using RecipeManagementSystemDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManagementSystemInfrastructure.Data
{
    public class ApplicationUser : IdentityUser
    {
        public Roles? Role { get; set; }
    }
}
