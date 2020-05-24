using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public abstract class TasqForManipulationDto
    {
        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Range(0, 100, ErrorMessage = "Percent completed can't be less than 0 or greater than 100")]
        public int Progress { get; set; }

        public Guid? ParentId { get; set; }

        public string Status { get; set; }

        public string Author { get; set; }

        public string AppointedTo { get; set; }

        public DateTime createDate { get; set; }

        public DateTime dueDate { get; set; }

        public IEnumerable<TasqForCreationDto> Children { get; set; }
    }
}
