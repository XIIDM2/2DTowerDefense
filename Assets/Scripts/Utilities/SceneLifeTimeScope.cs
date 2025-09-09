using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneLifeTimeScope : LifetimeScope
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private WavesManager _wavesManager;
    [SerializeField] private SceneManager _sceneManager;
    [SerializeField] private PathsManager _pathManager;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_sceneManager).As<ISceneController>().AsSelf();
        builder.RegisterComponent(_wavesManager).As<IWavesController>().AsSelf();
        builder.RegisterComponent(_pathManager).As<IPathController>().AsSelf();
        builder.RegisterComponent(_playerManager).As<IPlayerController>().AsSelf();
    }
}
