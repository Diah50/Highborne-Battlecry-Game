using System;
using System.Collections.Generic;
using System.Reflection;
using Highborne.Common.Interfaces;
using Zenject;

namespace Highborne.DIInstallers
{
    public class AppInstaller : Installer<AppInstaller>
    {
        private enum BindingScope
        {
            AsSingle,
            AsCached,
            AsTransient
        }

        public override void InstallBindings()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            //Use Cases
            SetupBindings<IUseCase>(executingAssembly);

            //Repositories
            SetupBindings<IRepository>(executingAssembly);

            //Gateways
            SetupBindings<IGateway>(executingAssembly);

            //Common
            SetupBindings<IService>(executingAssembly);

            //Events
            SignalBusInstaller.Install(Container);
            DeclareSignals(executingAssembly);
        }

        private void DeclareSignals(Assembly assembly)
        {
            foreach (var implementation in assembly.GetTypes())
            {
                if (typeof(ISignalEvent).IsAssignableFrom(implementation) && implementation != typeof(ISignalEvent))
                    Container.DeclareSignal(implementation);
            }
        }

        private void SetupBindings<TBindingType>(Assembly assembly, BindingScope scope = BindingScope.AsCached)
        {
            Type bindType = typeof(TBindingType);
            IList<Type> interfaces = new List<Type>();
            IList<Type> implementations = new List<Type>();
            foreach (var type in assembly.GetTypes())
            {
                if (bindType.IsAssignableFrom(type))
                {
                    if (type.GetTypeInfo().IsInterface)
                    {
                        if (type == bindType)
                            continue;

                        interfaces.Add(type);
                    }
                    else if (type.GetTypeInfo().IsClass)
                    {
                        implementations.Add(type);
                    }
                }
            }

            var implementationCount = implementations.Count;
            foreach (var interfaceToBind in interfaces)
            {
                for (int implementationIndex = 0; implementationIndex < implementationCount; implementationIndex++)
                {
                    if (interfaceToBind.IsAssignableFrom(implementations[implementationIndex]))
                    {
                        SetScope(Container.Bind(interfaceToBind).To(implementations[implementationIndex]), scope);
                        implementations.RemoveAt(implementationIndex);
                        implementationCount--;
                        break;
                    }
                }
            }
        }

        private void SetScope(FromBinderNonGeneric binder, BindingScope scope)
        {
            switch(scope)
            {
                case BindingScope.AsSingle:
                    binder.AsSingle();
                    break;
                case BindingScope.AsCached:
                    binder.AsCached();
                    break;
                case BindingScope.AsTransient:
                    binder.AsTransient();
                    break;
                default:
                    throw new Exception("unknown binding type");
            }
        }
    }
}