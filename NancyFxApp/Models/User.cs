using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NancyFxApp.Models
{
    [PetaPoco.TableName("user")]
    [PetaPoco.PrimaryKey("Id")]
    [PetaPoco.ExplicitColumns]
    public class User
    {
        public User()
        {
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }

        [PetaPoco.Column("id")]
        public int Id { get; set; }

        [Required, EmailAddress, StringLength(255)]
        [PetaPoco.Column("email")]
        public string Url { get; set; }

        [Required, StringLength(255)]
        [PetaPoco.Column("password")]
        public string Password { get; set; }
        
        [PetaPoco.Column("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [PetaPoco.Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}