using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NancyFxApp.Models
{
    
    [PetaPoco.TableName("source_meta")]
    [PetaPoco.PrimaryKey("Id")]
    [PetaPoco.ExplicitColumns]
    public class SourceMeta
    {
        [PetaPoco.Column("id")]
        public int Id { get; set; }
        
        [PetaPoco.Column("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [PetaPoco.Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        
        [PetaPoco.Column("meta_id")]
        [Required]
        public int MetaId { get; set; }

        [Required]
        [PetaPoco.Column("source_id")]
        public int SourceId { get; set; }
        
        [Required, StringLength(255)]
        [PetaPoco.Column("value")]
        public String Value { get; set; }
    }
}