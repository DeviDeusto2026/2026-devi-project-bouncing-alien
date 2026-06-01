using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("Acceleration Settings")]
    [Tooltip("Multiplies the gravity force inside this tunnel.")]
    public float forceMultiplier = 2f;

    [Header("Braking Settings")]
    [Tooltip("Braking percentage upon reaching the end of the tunnel (0 = none, 1 = total stop).")]
    [Range(0f, 1f)]
    public float brakeFactor = 0.5f;
}