using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit Animations", menuName = "ScriptableObjects/Unit/UnitAnimations")]
public class UnitAnimations : ScriptableObject
{
    // Animation clips separated by unit`s direction. Animation Controller blends clips in blend tree

    [Header("Down Direction")]
    [SerializeField] private AnimationClip D_Attack;
    [SerializeField] private AnimationClip D_Death;
    [SerializeField] private AnimationClip D_Disolve;
    [SerializeField] private AnimationClip D_Walk;

    [Header("Side Direction")]
    [SerializeField] private AnimationClip S_Attack;
    [SerializeField] private AnimationClip S_Death;
    [SerializeField] private AnimationClip S_Disolve;
    [SerializeField] private AnimationClip S_Walk;

    [Header("Up Direction")]
    [SerializeField] private AnimationClip U_Attack;
    [SerializeField] private AnimationClip U_Death;
    [SerializeField] private AnimationClip U_Disolve;
    [SerializeField] private AnimationClip U_Walk;

    private Dictionary<string, AnimationClip> _animationsDict;

    private bool _isInit = false;

    private void OnEnable()
    {
        DictInit();
    }

    public AnimationClip GetClip(string clipName)
    {
        if (!_isInit || _animationsDict == null)
        {
            Debug.LogError("Unit animations dictionary not initialized check unit animations SO, initializing again");
            DictInit();
        }

        if (_animationsDict.TryGetValue(clipName, out var clip))
        {
            return clip;
        }
        else
        {
            Debug.LogWarning("Unit Animation SO dictionary failed to find requested animation clip");
            return null;
        }
    }


    // i decided to init dict manually for QoL in inspector (avoid array). In future will add autoread from fields in inspector if will be more animations
    public void DictInit()
    {
        _isInit = false;

        _animationsDict = new Dictionary<string, AnimationClip>()
        {
            { "D_Attack", D_Attack },
            { "D_Death", D_Death },
            { "D_Disolve", D_Disolve },
            { "D_Walk", D_Walk },

            { "S_Attack", S_Attack },
            { "S_Death", S_Death },
            { "S_Disolve", S_Disolve },
            { "S_Walk", S_Walk },

            { "U_Attack", U_Attack },
            { "U_Death", U_Death },
            { "U_Disolve", U_Disolve },
            { "U_Walk", U_Walk },
        };

        _isInit = true;
    }

}
