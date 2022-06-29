using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Matricula.WebUI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Matricula.WebUI.Controllers;

public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;

  private MatriculaContext _context;
  public SelectList? StudentSelect { get; set; }

  public HomeController(ILogger<HomeController> logger, MatriculaContext context)
  {
    _logger = logger;
    _context = context;
  }

  public SelectList getPopulateShifts()
  {
    List<SelectListItem> selectListItems = _context.Shifts.Select(a => new SelectListItem()
    {
      Text = a.Name,
      Value = a.Id.ToString()
    }).ToList();

    selectListItems.Insert(0, new SelectListItem() { Text = "Seleccionar Turno", Value = "", Selected = true });

    return (SelectList)(ViewData["Shifts"] = new SelectList(selectListItems, "Value", "Text"));
  }

  public SelectList getPopulateCourses(int cycle = 0)
  {
    List<SelectListItem> selectListItems = _context.ClassRoomCourses.Where(_ => _.Cycle == cycle).Select(a => new SelectListItem()
    {
      Text = a.Course.Name,
      Value = a.Id.ToString()
    }).ToList();

    selectListItems.Insert(0, new SelectListItem() { Text = "Seleccionar Curso", Value = "", Selected = true });

    return (SelectList)(ViewData["Courses"] = new SelectList(selectListItems, "Value", "Text"));
  }

  public SelectList getPopulateStudents()
  {
    List<SelectListItem> selectListItems = _context.Students.Select(a => new SelectListItem()
    {
      Text = a.FullName,
      Value = a.Id.ToString(),
      Selected = false
    }).ToList();

    selectListItems.Insert(0, new SelectListItem() { Text = "Seleccionar Estudiante", Value = "" });

    ViewData["Students2"] = _context.Students.Select(_ => new { _.Id, _.FullName, _.Cycle }).ToList();
    return (SelectList)(ViewData["Students"] = new SelectList(selectListItems, "Value", "Text"));
  }

  public IActionResult Index()
  {
    getPopulateStudents();
    getPopulateCourses();
    getPopulateShifts();
    return View();
  }

  [HttpPost]
  public IActionResult GetStudent(Guid studentId)
  {
    var student = _context.Students.Where(a => a.Id == studentId).FirstOrDefault();
    return Json(student);
  }

  [HttpPost]
  public IActionResult GetCourses(int cycle, Guid shiftId)
  {
    var courses = _context.ClassRoomCourses.Include(_ => _.ClassRoom).Include(_ => _.Shift).Include(_ => _.Course).Where(a => a.Cycle == cycle && a.ShiftId == shiftId && a.Capacity > 0).OrderBy(_ => _.StartTime).ToList();
    return Json(courses);
  }


  [HttpPost, ValidateAntiForgeryToken]
  public IActionResult Index(EnrollmentModel model)
  {
    if (ModelState.IsValid)
    {
      var student = _context.Students.Where(a => a.Id == model.studentSelect).FirstOrDefault();
      var shift = _context.Shifts.Where(a => a.Id == model.shiftSelect).FirstOrDefault();

      Guid[] courseIds = model.listCourses.Split(',').Select(a => Guid.Parse(a)).ToArray();

      var enrollment = new Enrollment
      {
        StudentId = student.Id
      };

      _context.Enrollments.Add(enrollment);

      _context.SaveChanges();


      foreach (var course in courseIds)
      {
        var classRoomCourse = _context.ClassRoomCourses.Where(a => a.Id == course).FirstOrDefault();

        classRoomCourse.Capacity = classRoomCourse.Capacity - 1;
        _context.ClassRoomCourses.Update(classRoomCourse);

        var enrollmentDetail = new EnrollmentDetail
        {
          EnrollmentId = enrollment.Id,
          ClassRoomCourseId = classRoomCourse.Id
        };

        _context.EnrollmentDetails.Add(enrollmentDetail);
      }

      _context.SaveChanges();
      return RedirectToAction("Gracias");
    }
    else
    {
      return RedirectToAction("Index");
    }

  }

  public IActionResult Gracias()
  {
    return View();
  }
  public IActionResult Privacy()
  {
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
