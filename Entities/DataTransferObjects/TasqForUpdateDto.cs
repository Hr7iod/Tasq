using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class TasqForUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ParentId { get; set; }

        public IEnumerable<TasqForCreationDto> Children { get; set; }
    }
}
