using Business.Consul.Extend.ClienExtend;
using Business.Consul.Register;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Consul.Extend
{
    public static class ConsulExtend
    {
        public static void AddConsulRegister(this IServiceCollection services)
        {
            services.AddSingleton<IConsulRegister, ConsulRegister>();
        }

        /// <summary>
        /// 注册Consul调度策略
        /// </summary>
        /// <param name="services"></param>
        /// <param name="consulDispatcherType"></param>
        public static void AddConsulDispatcher(this IServiceCollection services, ConsulDispatcherType consulDispatcherType)
        {
            switch (consulDispatcherType)
            {
                case ConsulDispatcherType.Average:
                    services.AddTransient<AbstractConsulDispatcher, AverageDispatcher>();
                    break;
                case ConsulDispatcherType.Polling:
                    services.AddTransient<AbstractConsulDispatcher, PollingDispatcher>();
                    break;
                case ConsulDispatcherType.Weight:
                    services.AddTransient<AbstractConsulDispatcher, WeightDispatcher>();
                    break;
                default:
                    break;
            }
        }

        public enum ConsulDispatcherType
        {
            Average = 0,
            Polling = 1,
            Weight = 2
        }
    }
}
