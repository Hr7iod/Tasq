using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    class Tasq
    {
        [Column("TasqId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is requred field.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Tasq> SubTasqs { get; set; }
    }
}
