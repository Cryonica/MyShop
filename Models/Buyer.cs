using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyShop.Models
{
    [JsonObject(IsReference = true)]
    public class Buyer
    {
        public int? Id { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        [NotMapped] 
        public virtual ICollection<int> SalesIds
        {
            get { return Sales?.Select(s => s.Id).ToList(); }
            set { }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
