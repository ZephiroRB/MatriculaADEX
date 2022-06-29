
using Matricula.Core.Interfaces;

namespace Matricula.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {


        void SaveChanges();

        Task SaveChangesAsync();

    }
}
