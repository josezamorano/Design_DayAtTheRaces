using Autofac;
using DayAtTheRaces.WpfApp.Interfaces;
using DayAtTheRaces.WpfApp.Validation;

namespace DayAtTheRaces.WpfApp.DependencyInjection
{
    public class ContainerConfig
    {
        private static IContainer container;
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UIClientValidation>().As<IUIClientValidation>();

            container = builder.Build();
        }
        public static T GetInstance<T>()
        {
            if (container == null)
                Configure();

            return container.Resolve<T>();
        }
    }
}
