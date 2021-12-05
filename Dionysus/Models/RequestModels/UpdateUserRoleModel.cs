using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.Models.RequestModels
{
    public class UpdateUserRoleModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
