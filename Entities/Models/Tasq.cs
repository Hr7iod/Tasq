using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Tasq
    {
        [Column("TasqId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is a requred field.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Tasq> Children { get; set; }

        [ForeignKey(nameof(Tasq))]
        //[Required(ErrorMessage = "ParentId is required field. Set NULL to make root tasq.")]
        public Guid? ParentId { get; set; }
        public Tasq Parent { get; set; }
    }
}
