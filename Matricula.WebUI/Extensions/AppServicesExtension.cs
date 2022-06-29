using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matricula.Core.Interfaces;
using Matricula.Infrastructure.Repositories;

namespace Matricula.WebUI.Extensions;

public static class AppServicesExtension
{
  public static void RegisterAppServices(this WebApplicationBuilder builder)
  {
    builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
    builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
  }
}
