﻿using System;
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
        public Guid? ParentId { get; set; }

        public IEnumerable<TasqForCreationDto> Children { get; set; }
    }
}