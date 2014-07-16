using System;
using System.ComponentModel.DataAnnotations;

namespace vNextTodoApp
{
    public class TaskInput
    {
        [Required]
        public string Description { get; set; }
    }
}