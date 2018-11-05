using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace stenoapp.core
{
    public class IoCHelper
    {
        private static object lockObject = new object();

        private static IoCHelper _iocHelper;

        private Dictionary<Type, object> Objects { get; set; }

        public static IoCHelper Instance
        {
            get
            {
                if (_iocHelper == null)
                {
                    lock (lockObject)
                    {
                        if (_iocHelper == null)
                        {
                            _iocHelper = new IoCHelper();
                        }
                    }
                }
                return _iocHelper;
            }
            set
            {
                _iocHelper = value;
            }
        }

        public void Resolve<TSource, TDestination>()
        {
            object dependencyForInstance, implementedByInstance;
            dependencyForInstance = Activator.CreateInstance(typeof(TSource));
            implementedByInstance = Activator.CreateInstance(typeof(TDestination));

            foreach (var pi in dependencyForInstance.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Static))
            {
                var piList = implementedByInstance.GetType().GetInterfaces().Where(x => x.Name.Equals(pi.FieldType.Name)).ToList();
                if (piList.Count > 0)
                {
                    pi.SetValue(dependencyForInstance, implementedByInstance);
                    break;
                }
            }
        }
    }
}