using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NancyFxApp.Source
{
    [PetaPoco.TableName("Source")]
    [PetaPoco.PrimaryKey("Id")]
    [PetaPoco.ExplicitColumns]
    public class Source
    {
        public Source()
        {
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }

        [PetaPoco.Column("id")]
        public int Id { get; set; }

        [Required, RegularExpression(@"(https?://)?([\da-z.-]+)\.([a-z.]{2,6})([/\w .-]*)*/?"), StringLength(255)]
        [PetaPoco.Column("url")]
        public string Url { get; set; }
        
        [PetaPoco.Column("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [PetaPoco.Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}