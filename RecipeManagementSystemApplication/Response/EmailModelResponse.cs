using RecipeManagementSystemApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManagementSystemApplication.Response
{
    public class EmailModelResponse
    {
        public bool Success { get; set; }
        public EmailModel EmailModel { get; set; }
        public string Message { get; set; }
    }
}
