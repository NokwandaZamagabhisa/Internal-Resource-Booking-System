using ResourceBooking.Core.Entities;

namespace ResourceBooking.Core.Gateways
{
    public interface IResourceGateway
    {
        Task Add(Resource resource);
        Task<Resource> GetById(int id);
        Task<IEnumerable<Resource>> GetAll();
        Task<IEnumerable<Resource>> GetAllAvailable();
        Task Update(Resource person);
        Task Delete(int id);
        Task<bool> Exists(int id);
    }
}
