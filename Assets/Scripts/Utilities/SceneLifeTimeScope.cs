using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneLifeTimeScope : LifetimeScope
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private WavesManager _wavesManager;
    [SerializeField] private SceneManager _sceneManager;
    [SerializeField] private PathsManager _pathManager;

    [SerializeField] private UnitSpawner _unitSpawner;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<Factory>(Lifetime.Singleton);

        builder.RegisterComponent(_sceneManager).As<ISceneService>().AsSelf();
        builder.RegisterComponent(_wavesManager).As<IWavesService>().AsSelf();
        builder.RegisterComponent(_pathManager).As<IPathService>().AsSelf();
        builder.RegisterComponent(_playerManager).As<IPlayerService>().AsSelf();
        builder.RegisterComponent(_unitSpawner).AsSelf();

    }
}
