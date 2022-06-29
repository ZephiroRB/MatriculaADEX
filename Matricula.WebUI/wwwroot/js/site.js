// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
  $("#registro_matricula").validate({
    ignore: [],
    rules: {
      studentSelect: {
        required: true,
      },
      shiftSelect: {
        required: true,
      },
      listCourses: "required",
    },
  });

  $("#register").click(function () {
    $("#registro_matricula").submit();
  });

  function loadCourse(cycle = 0, shiftId = "") {
    if (cycle > 0 && shiftId != "" && shiftId != "0") {
      $.ajax({
        url: "/Home/GetCourses",
        type: "POST",
        dataType: "json",
        data: {
          cycle: cycle,
          shiftId: shiftId,
        },
        success: function (data) {
          var html = "";
          $.each(data, function (key, value) {
            html +=
              "<option data-endtime='" + value.endTime + "' data-starttime='" + value.startTime + "' data-classroom='" + value.classRoom.name + "' data-capacity='" + value.capacity + "' data-shift='" + value.shift.name + "' value='" +
              value.id +
              "'>" +
              value.course.name +
              "</option>";
          });
          $("#courseSelect").html(html);
        },
      });
    } else {
      $("#courseSelect").html("<option value='0'>Seleccionar Curso</option>");
    }
  }

  $("#studentSelect").change(function () {
    var studentId = $(this).val();

    $.ajax({
      url: "/Home/GetStudent",
      type: "POST",
      data: {
        studentId: studentId,
      },
      success: function (data) {
        loadCourse(data.cycle, $("#shiftSelect option:selected").val());
      },
    });
  });

  $("#shiftSelect").change(function () {
    var shiftId = $(this).val();

    loadCourse($("#studentSelect option:selected").data("cycle"), shiftId);
  });

  $(document).on("click", ".no", function () {
    var tr = $(this).closest("tr");
    var value = tr.find(".listCoursesTemp").val();
    var title = tr.find(".titleCourse").html();

    $("#courseSelect").append(
      "<option value='" + value + "'>" + title + "</option>"
    );

    $(this).closest("tr").remove();
    updateListCourses();
  });

  function updateListCourses() {
    var values = $("input[name='listCoursesTemp[]']")
      .map(function () {
        return $(this).val();
      })
      .get();

    $("#listCourses").val(values);
  }

  var row;

  $(".btn_add").click(function () {
    $(".error_course").hide();
    $("#courseSelect").removeClass("error");
    if ($("#courseSelect option:selected").val()) {


      var scheduleInStorage = $("input[name='schedule[]']")
      .map(function () {
        return $(this).val();
      })
      .toArray();

      var scheduleCurrent = $("#courseSelect option:selected").data("starttime") + '-' + $("#courseSelect option:selected").data("endtime");

      console.log(jQuery.inArray(scheduleCurrent, scheduleInStorage));

      if (jQuery.inArray(scheduleCurrent, scheduleInStorage) == 0) {
        alert("La hora seleccionada ya esta asignada");
        return false;
      };


      row = $(
        '<tr><td class="text-left"><input type="hidden" name="schedule[]" value="' + scheduleCurrent + '"><input type="hidden" name="listCoursesTemp[]" id="listCoursesTemp[]" class="listCoursesTemp" value="' +
          $("#courseSelect option:selected").val() +
          '"> <h3 class="titleCourse">' +
          $("#courseSelect option:selected").text() +
          '</h3>Capacidad: <span class="capacity">' + $("#courseSelect option:selected").data("capacity") + '</span> Aula: <span class="classRoom">' + $("#courseSelect option:selected").data("classroom") + '</span> Horario: <span class="startTime">' + $("#courseSelect option:selected").data("starttime") + '</span> - <span class="endTime">' + $("#courseSelect option:selected").data("endtime") + '</span></td><td class="no"><i class="fa fa-trash-can"></i></td></tr>'
      );
      $("#courses").append(row);
      $("#courseSelect option:selected").remove();
      updateListCourses();
    } else {
      $("#courseSelect").addClass("error");
      $(".error_course").show();
    }
  });
});
