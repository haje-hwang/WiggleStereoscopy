using Unity.Cinemachine;
using UnityEngine;

public struct WiggleConfig
{
    public float ShiftAmount;
    public float SwapInterval;
    public float TargetMaxDistance;
    public WiggleConfig(float ShiftAmount = 0.36f, float SwapInterval = 0.1f, float TargetMaxDistance = 30f)
    {
        this.ShiftAmount = ShiftAmount;
        this.SwapInterval = SwapInterval;
        this.TargetMaxDistance = TargetMaxDistance;
    }
}