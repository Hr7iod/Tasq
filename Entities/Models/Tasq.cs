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

        [MinLength(0, ErrorMessage = "Percent completed can't be less than 0")]
        [MaxLength(100, ErrorMessage = "Percent completed can't be greater than 100")]
        public int Progress { get; set; }

        public string Status { get; set; }

        public string Author { get; set; }

        public string AppointedTo { get; set; }

        public DateTime createDate { get; set; }

        public DateTime dueDate { get; set; }

        public ICollection<Tasq> Children { get; set; }

        [ForeignKey(nameof(Tasq))]
        //[Required(ErrorMessage = "ParentId is required field. Set NULL to make root tasq.")]
        public Guid? ParentId { get; set; }
        public Tasq Parent { get; set; }
    }
}
