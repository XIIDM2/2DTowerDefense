using UnityEngine.Events;

public class SlimeAnimation : UnitAnimation
{
    public event UnityAction OnSlimeSplit;

    /// <summary>
    /// Special Animation event method for slime controller to create 2 baby slimes (assigned in slime split clip)
    /// </summary>
    private void AESlimeSplit()
    {
        OnSlimeSplit?.Invoke();
    }
}
