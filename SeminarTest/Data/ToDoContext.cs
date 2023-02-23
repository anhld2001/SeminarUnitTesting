using Microsoft.EntityFrameworkCore;
using SeminarTest.Models;
using System.Collections.Generic;

namespace SeminarTest.Data
{
    public class ToDoContext : DbContext
    {
        public ToDoContext()
        {
        }

        public ToDoContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<ToDo>? ToDoList { get; set; }
    }
}
