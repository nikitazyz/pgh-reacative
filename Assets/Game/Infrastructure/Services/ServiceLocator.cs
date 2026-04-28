using System;
using System.Collections.Generic;

namespace Reacative.Infrastructure.Services
{
    public class ServiceLocator
    {
        private Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        private static ServiceLocator _instance;

        private ServiceLocator(){}

        public static void RegisterService<T>(T service) where T : IService
        {
            _instance ??= new ServiceLocator();
            _instance._services.Add(typeof(T), service);
        }

        public static T GetService<T>() where T : IService
        {
            return (T)_instance?._services.GetValueOrDefault(typeof(T), null);
        }
    }
}