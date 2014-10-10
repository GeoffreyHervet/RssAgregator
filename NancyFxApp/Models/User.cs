using Nancy.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NancyFxApp.Models
{
    [PetaPoco.TableName("user")]
    [PetaPoco.PrimaryKey("Id")]
    [PetaPoco.ExplicitColumns]
    public class User : IUserIdentity
    {
        public User()
        {
            this.CreatedAt = System.DateTime.Now;
            this.UpdatedAt = System.DateTime.Now;
        }

        [PetaPoco.Column("id")]
        public int Id { get; set; }

        [PetaPoco.Column("guid")]
        public string Guid { get; set; }
        [Required, EmailAddress, StringLength(255)]
        [PetaPoco.Column("login")]
        public string UserName { get; set; }

        [Required, StringLength(255)]
        [PetaPoco.Column("password")]
        public string Password { get; set; }

        
        [PetaPoco.Column("created_at")]
        public System.DateTime CreatedAt { get; set; }
        
        [PetaPoco.Column("updated_at")]
        public System.DateTime UpdatedAt { get; set; }

        public IEnumerable<string> Claims { get; set; }

        public System.Guid getRealGuid()
        {
            return new System.Guid(this.Guid);
        }
    }
}