using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matricula.Core.Entities;

public class Enrollment : BaseEntity
{
    public Guid StudentId { get; set; }
    [ForeignKey("StudentId")]
    public Student Student { get; set; }
}
