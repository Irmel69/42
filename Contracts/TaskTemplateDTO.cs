using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    internal class TaskTemplateDTO
    {
        public string TaskID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
