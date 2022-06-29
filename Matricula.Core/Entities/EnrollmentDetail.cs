using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matricula.Core.Entities;

public class EnrollmentDetail : BaseEntity
{
    public Guid ClassRoomCourseId { get; set; }
    [ForeignKey("ClassRoomCourseId")]
    public ClassRoomCourse? ClassRoomCourse { get; set; }
    public Guid EnrollmentId { get; set; }
    [ForeignKey("EnrollmentId")]
    public Enrollment? Enrollment { get; set; }
}
