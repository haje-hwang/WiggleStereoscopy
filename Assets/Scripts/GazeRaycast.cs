using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class GazeRaycast : MonoBehaviour
{
    [SerializeField] private CinemachineCamera vcam;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private bool isWorking = true;

    [Header("For Gaze Raycast")]
    [SerializeField] private LayerMask layerMask = ~0;
    [SerializeField] private bool debugDraw = true;

    private WiggleConfig _config;
    // private float lastHitDistance;
    private CinemachineThirdPersonFollow follow;
    private bool rightShoulder = true;

    private void Awake()
    {
        _config = new WiggleConfig();
        follow ??= vcam.GetComponent<CinemachineThirdPersonFollow>();
        FindAnyObjectByType<WiggleConfigUpdater>().OnConfigUpdate += UpdateConfig;
    }
    private void Start()
    {
        isWorking = true;
        StartCoroutine(Swap());
    }

    IEnumerator Swap()
    {
        while (isWorking)
        {
            GazeRay();

            // TODO: ShiftAmount를 타켓과의 거리에 따라 조정하면 3D효과가 커질까?

            if (rightShoulder)
            {
                follow.ShoulderOffset.x = _config.ShiftAmount;
                rightShoulder = false;
            }
            else
            {
                follow.ShoulderOffset.x = -_config.ShiftAmount;
                rightShoulder = true;
            }

            yield return new WaitForSeconds(_config.SwapInterval);
        }
    }

    private void GazeRay()
    {
        var ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _config.TargetMaxDistance, layerMask, QueryTriggerInteraction.Ignore))
        {
            // lastHitDistance = hit.distance;
            cameraTarget.position = hit.point;
        }
        else
        {
            Vector3 targetPoint = ray.origin + ray.direction * _config.TargetMaxDistance;
            // lastHitDistance = maxDistance;
            cameraTarget.position = targetPoint;
        }

        if (debugDraw)
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * _config.TargetMaxDistance, Color.green);
        }
    }
    public void UpdateConfig(WiggleConfig config) { _config = config; }
}
