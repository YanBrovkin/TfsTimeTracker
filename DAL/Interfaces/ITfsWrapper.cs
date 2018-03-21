using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.DTO;

namespace DAL.Interfaces
{
    public interface ITfsWrapper
    {
        Task<IEnumerable<TfsWorkItem>> GetCurrentSprintAsync();
    }
}