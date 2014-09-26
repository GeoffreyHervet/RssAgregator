using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NancyFxApp.Models
{
    
    [PetaPoco.TableName("item_information")]
    [PetaPoco.PrimaryKey("Id")]
    [PetaPoco.ExplicitColumns]
    public class ItemInformation
    {
        [PetaPoco.Column("id")]
        public int Id { get; set; }
        
        [PetaPoco.Column("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [PetaPoco.Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Required]
        [PetaPoco.Column("information_name_id")]
        public int InformationNameId { get; set; }

        [Required]
        [PetaPoco.Column("item_id")]
        public int ItemId { get; set; }
    }
}