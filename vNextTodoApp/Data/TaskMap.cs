using FluentNHibernate.Mapping;
using System;

namespace vNextTodoApp
{
    public class TaskMap : ClassMap<Task>
    {
	    public TaskMap()
	    {
            Id(p => p.TaskId);
            Map(p => p.Description);
            Map(p => p.IsCompleted);
	    }
    }
}