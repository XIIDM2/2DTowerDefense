using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ProjectLifeTimeScope : LifetimeScope
{
    [SerializeField] private UnitDatabase unitDatabase;
    [SerializeField] private PlayerData playerData;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(unitDatabase);
        builder.RegisterInstance(playerData);

        builder.Register<IAudioService, AudioSystem>(Lifetime.Singleton).As<IAudioService>().As<IStartable>().AsSelf();
    }
}
