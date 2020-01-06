using Autofac;
using Autofac.Core;
using DayAtTheRaces.Library.Commands;
using DayAtTheRaces.Library.Interfaces;

namespace DayAtTheRaces.Library.DependencyInjection
{
    public class ContainerConfig
    {
        private static IContainer container;
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<BettingBank>().As<IBettingBank>().SingleInstance();
            builder.RegisterType<CommandInvoker>().As<ICommandInvoker>();
            builder.RegisterType<CommandDepositMoneyInGuyPocket>().As<ICommandDepositMoneyInGuyPocket>();
            builder.RegisterType<CommandNewDogToRacetrack>().As<ICommandNewDogToRacetrack>();
            builder.RegisterType<CommandNewGuyToPalaceStadium>().As<ICommandNewGuyToPalaceStadium>();
            builder.RegisterType<CommandPlaceBet>().As<ICommandPlaceBet>();
            builder.RegisterType<CommandRaceGo>().As<ICommandRaceGo>();
            builder.RegisterType<Dog>().As<IDog>();
            builder.RegisterType<DogFactory>().As<IDogFactory>();
            builder.RegisterType<DogRacePalaceStadium>().As<IDogRacePalaceStadium>().SingleInstance();
            builder.RegisterType<DogState>().As<IDogState>();
            builder.RegisterType<Guy>().As<IGuy>();
            builder.RegisterType<Racetrack>().As<IRacetrack>().SingleInstance();
            builder.RegisterType<UIMediator>().As<IUIMediator>();

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
