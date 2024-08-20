using RecipeManagementSystemApplication.Models.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManagementSystemApplication.Interface
{
    public interface IEmailService
    {
        void SendEmail(EmailModel emailModel);
    }
}
