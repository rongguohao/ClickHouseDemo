using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    [Table(Name = "teacher")]
    public class Teacher
    {
        [Column(IsPrimary =true)]
        public Guid Id { get; set; }

        public string Name { get; set; }


        public DateTime Birthday { get; set; }
    }
}
