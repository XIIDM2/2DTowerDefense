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

    // public getters to animationClips

    [Header("Down Direction")]
    public AnimationClip Down_Attack => D_Attack;
    public AnimationClip Down_Death => D_Death;
    public AnimationClip Down_Disolve => D_Disolve;
    public AnimationClip Down_Walk => D_Walk;

    [Header("Side Direction")]
    public AnimationClip Side_Attack => S_Attack;
    public AnimationClip Side_Death => S_Death;
    public AnimationClip Side_Disolve => S_Disolve;
    public AnimationClip Side_Walk => S_Walk;

    [Header("Up Direction")]
    public AnimationClip Up_Attack => U_Attack;
    public AnimationClip Up_Death => U_Death;
    public AnimationClip Up_Disolve => U_Disolve;
    public AnimationClip Up_Walk => U_Walk;

}
