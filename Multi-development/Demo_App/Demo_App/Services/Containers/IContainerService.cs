using Demo_App.Models;
using AssociationBusiness.Handlers;

namespace Demo_App.Services
{
    public interface IContainerService
    {
        Task<int> CreateContainer(CreateContainerRequest createContainerRequest);
    }
}
