using System;
using System.ComponentModel.DataAnnotations;

namespace vNextTodoApp
{
    public class TaskInput
    {
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}