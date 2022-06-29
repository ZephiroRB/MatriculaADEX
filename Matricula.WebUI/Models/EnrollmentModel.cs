using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matricula.WebUI.Models;

public class EnrollmentModel
{
    public Guid studentSelect{ get; set; }
    public Guid shiftSelect{ get; set; }
    public string listCourses{ get; set; }
}
