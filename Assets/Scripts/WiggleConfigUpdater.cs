using System;
using UnityEngine;

public class WiggleConfigUpdater : MonoBehaviour
{
    public float ShiftAmount = 0.16f;
    public float SwapInterval = 0.1f;
    public float TargetMaxDistance = 30f;
    public Action<WiggleConfig> OnConfigUpdate;
    private WiggleConfig _config = new WiggleConfig();

    private void Start() => UpdateConfig();
    private void OnValidate() => UpdateConfig();
    private void UpdateConfig()
    {
        _config.ShiftAmount = ShiftAmount;
        _config.SwapInterval = SwapInterval;
        _config.TargetMaxDistance = TargetMaxDistance;
        OnConfigUpdate?.Invoke(_config);
    }
}