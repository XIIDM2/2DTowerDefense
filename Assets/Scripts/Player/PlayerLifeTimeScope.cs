using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerLifeTimeScope : LifetimeScope
{
    [SerializeField] private PlayerData _playerData;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_playerData).AsSelf().As<IHealthConfig>();
    }
}
