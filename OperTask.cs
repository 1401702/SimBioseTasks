using System;
using System.Collections.Generic;
using System.Text;

namespace SimBioseTasks
{
    public class OperTask
    {
        public string Operation { get; set; } // "save", "delete", "create", etc.
        public BaseTask Task { get; set; }
    }
}
