using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matricula.Core.Entities;

public class ClassRoomCourse : BaseEntity
{
    public Guid ClassRoomId { get; set; }
    [ForeignKey("ClassRoomId")]
    public ClassRoom? ClassRoom { get; set; }
    public Guid CourseId { get; set; }
    [ForeignKey("CourseId")]
    public Course? Course { get; set; }
    public Guid ShiftId { get; set; }
    [ForeignKey("ShiftId")]
    public Shift? Shift { get; set; }
    public int Cycle { get; set; }

    public int Capacity { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}