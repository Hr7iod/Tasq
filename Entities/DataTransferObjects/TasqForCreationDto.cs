using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class TasqForCreationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }

        public IEnumerable<TasqForCreationDto> Children { get; set; }
    }
}
