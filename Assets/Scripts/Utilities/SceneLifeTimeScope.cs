using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneLifeTimeScope : LifetimeScope
{
    [Header("Managers")]
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private WavesManager _wavesManager;
    [SerializeField] private SceneManager _sceneManager;
    [SerializeField] private PathsManager _pathManager;

    [Header("Data")]
    [SerializeField] private WavesData _wavesData;

    [Header("Spawners")]
    [SerializeField] private UnitSpawner _unitSpawner;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_playerManager).As<IPlayerService>().AsSelf();
        builder.RegisterComponent(_wavesManager).As<IWavesService>().AsSelf();
        builder.RegisterComponent(_sceneManager).As<ISceneService>().AsSelf();
        builder.RegisterComponent(_pathManager).As<IPathService>().AsSelf();

        builder.RegisterInstance(_wavesData);

        builder.RegisterComponent(_unitSpawner).AsImplementedInterfaces().AsSelf();

        builder.Register<UnitFactory>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
    }
}
