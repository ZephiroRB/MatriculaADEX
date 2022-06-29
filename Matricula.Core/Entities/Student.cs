using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matricula.Core.Entities;

public class Student : BaseEntity
{
    public string FullName { get; set; }
    public int Cycle { get; set; }
}
