using Microsoft.Data.Entity;
using System;
using JetBrains.Annotations;
using Microsoft.Framework.OptionsModel;

namespace vNextTodoApp
{
    public class TaskDbContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }

        private static bool _created = false;

        public TaskDbContext(IServiceProvider serviceProvider, IOptionsAccessor<DbContextOptions> options) : base(serviceProvider, options.Options)
        {
            if (!_created)
            {
                Database.EnsureCreated();
                _created = true;
            }
        }
    }
}