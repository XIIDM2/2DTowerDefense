using VContainer;
using VContainer.Unity;

public class ProjectLifeTimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<IAudioService, AudioSystem>(Lifetime.Singleton).As<IAudioService>().As<IStartable>().AsSelf();
    }
}
