using System;
using System.Collections.Generic;

namespace Services{
    public class AllServices{
        private static AllServices instance;
        public static AllServices Instance => instance ?? (instance = new AllServices());

        private Dictionary<Type, IService> Container = new Dictionary<Type, IService>();

        public void Register<T>(T service) where T : IService{
            Container.Add(typeof(T), service);
        }

        public T Get<T>() where T : IService{
            return (T) Container[typeof(T)];
        }
    }
}