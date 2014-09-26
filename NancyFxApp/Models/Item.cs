using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NancyFxApp.Models
{
    
    [PetaPoco.TableName("items")]
    [PetaPoco.PrimaryKey("Id")]
    [PetaPoco.ExplicitColumns]
    public class Item
    {
        [PetaPoco.Column("id")]
        public int Id { get; set; }
        
        [PetaPoco.Column("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [PetaPoco.Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
  
        [PetaPoco.Column("source_id")]
        public int SourceId { get; set; }
        
        [PetaPoco.Column("pub_date")]
        public DateTime PubDate { get; set; }
        
        [PetaPoco.Column("readed_at")]
        public DateTime? ReadedAt { get; set; }

        [Required, StringLength(255)]
        [PetaPoco.Column("name")]
        public String Name { get; set; }
    }
}
