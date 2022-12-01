using PTOManager.Domain.Models;

namespace PTOManager.Domain.Services.Interfaces;
    public interface IRequestService
    {
         IEnumerable<Request> List();
         IEnumerable<Request> Find(int id);
         String Add(Request request);
    }