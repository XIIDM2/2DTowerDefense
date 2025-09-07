using UnityEngine;
using VContainer;
using VContainer.Unity;

public class UnitLifeTimeSccope : LifetimeScope
{
    // register dependencies, do not forget to set prefab in autoInjection field
    [SerializeField] private UnitData _unitData;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_unitData).AsSelf().As<IHealthConfig>();
    }
}
