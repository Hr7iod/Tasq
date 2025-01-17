﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public class TasqParameters : RequestParameters
    {
        public TasqParameters()
        {
            OrderBy = "name";
        }
        public uint MinProgress { get; set; }
        public uint MaxProgress { get; set; } = int.MaxValue;

        public bool ValidProgressRange => MaxProgress >= MinProgress;

        public string SearchName { get; set; }

        public string SearchParent { get; set; }
    }
}
