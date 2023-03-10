using Consul;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System;
using Business.Consul;

namespace Business.Consul.Register
{
    public class ConsulRegister : IConsulRegister
    {
        private readonly ConsulRegisterOptions _consulRegisterOptions;
        private IConsulClient _client;
        private AgentServiceRegistration _registration;

        public ConsulRegister(IOptionsMonitor<ConsulRegisterOptions> consulRegisterOptions)
        {
            _consulRegisterOptions = consulRegisterOptions.CurrentValue;
        }

        public async Task ConsulRegistAsync()
        {
            _client = new ConsulClient(options =>
            {
                options.Address = new Uri(_consulRegisterOptions.Address); // Consul客户端地址
            });

            var id = Guid.NewGuid().ToString();

            _registration = new AgentServiceRegistration
            {
                ID = id, // 唯一Id
                Name = _consulRegisterOptions.Name, // 服务名(分组--多个实例组成的集群)
                Address = _consulRegisterOptions.Ip, // 服务绑定IP
                Port = Convert.ToInt32(_consulRegisterOptions.Port), // 服务绑定端口
                //Tag 标签
                Check = new AgentServiceCheck
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5), // 服务启动多久后注册
                    Interval = TimeSpan.FromSeconds(10), // 健康检查时间间隔
                    HTTP = $"http://{_consulRegisterOptions.Ip}:{_consulRegisterOptions.Port}{_consulRegisterOptions.HealthCheck}?id={id}", // 健康检查地址
                    Timeout = TimeSpan.FromSeconds(5) // 超时时间
                }
            };

            await _client.Agent.ServiceRegister(_registration);
        }

        public async Task ConsulDeregistAsync(string id)
        {
            try
            {
                await _client?.Agent.ServiceDeregister(id);
            }
            catch (Exception)
            {
            }
        }

        public bool ExistServiceId(string clientId)
        {
            return _registration?.ID == clientId;
        }
    }
}
