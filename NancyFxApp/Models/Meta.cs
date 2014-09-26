using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NancyFxApp.Models
{
    
    [PetaPoco.TableName("meta")]
    [PetaPoco.PrimaryKey("Id")]
    [PetaPoco.ExplicitColumns]
    public class Meta
    {
        [PetaPoco.Column("id")]
        public int Id { get; set; }
        
        [PetaPoco.Column("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [PetaPoco.Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
  
        [Required, StringLength(255)]
        [PetaPoco.Column("name")]
        public String Name { get; set; }
    }
}