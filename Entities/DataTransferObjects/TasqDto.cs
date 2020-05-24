using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class TasqDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Progress { get; set; }
        public Guid? ParentId { get; set; }
        public string Status { get; set; }
        public string Author { get; set; }
        public string AppointedTo { get; set; }
        public DateTime createDate { get; set; }
        public DateTime dueDate { get; set; }
    }
}
