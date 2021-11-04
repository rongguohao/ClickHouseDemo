using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    [Table(Name = "student")]
    public class Student
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
