using System.Threading.Tasks;

namespace Business.Consul.Register
{
    public interface IConsulRegister
    {
        Task ConsulRegistAsync();
        Task ConsulDeregistAsync(string id);
        bool ExistServiceId(string clientId);
    }
}
