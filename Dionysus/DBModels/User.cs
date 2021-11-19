using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Dionysus.DBModels
{
    [Table("_user")]
    public partial class User
    {
        public User()
        {
            Ratings = new HashSet<Rating>();
        }

        [Key]
        [Column("username")]
        [StringLength(30)]
        public string Username { get; set; }
        [Required]
        [Column("password")]
        [StringLength(30)]
        public string Password { get; set; }
        [Required]
        [Column("role")]
        [StringLength(30)]
        public string Role { get; set; }
        [Required]
        [Column("name")]
        [StringLength(40)]
        public string Name { get; set; }

        [InverseProperty(nameof(Rating.UsernameNavigation))]
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
