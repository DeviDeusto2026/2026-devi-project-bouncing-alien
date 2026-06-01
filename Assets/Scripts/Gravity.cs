using UnityEngine;

public class Gravity : MonoBehaviour
{
    [Header("Gravity Settings")]
    public float gravityStrength = 35f;
    public float rotationSpeed = 8f;

    private Gravity[] allPlanetsGravity;

    void Start()
    {
        CachePlanets();
    }

    public void CachePlanets()
    {
        allPlanetsGravity = Object.FindObjectsByType<Gravity>(FindObjectsSortMode.None);
    }

    void OnTriggerStay(Collider collider)
    {
        Rigidbody rb = collider.GetComponent<Rigidbody>();
        Jump playerJump = collider.GetComponent<Jump>();

        if (rb == null) return;

        Vector3 gravityDirection;

        if (playerJump != null && playerJump.currentPlatformZone != null)
        {
            if (GetComponent<Platform>() == null) return;

            gravityDirection = playerJump.currentPlatformZone.up;

            Platform platformScript = playerJump.currentPlatformZone.GetComponent<Platform>();
            float finalForce = gravityStrength;

            if (platformScript != null)
            {
                finalForce *= platformScript.forceMultiplier;
            }

            rb.AddForce(gravityDirection * finalForce, ForceMode.Acceleration);

            Quaternion invertedRotation = Quaternion.FromToRotation(collider.transform.up, gravityDirection) * collider.transform.rotation;
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, invertedRotation, rotationSpeed * 2f * Time.fixedDeltaTime));
            return;
        }
        else
        {
            if (GetComponent<Platform>() != null) return;

            if (allPlanetsGravity == null || allPlanetsGravity.Length == 0)
            {
                CachePlanets();
            }

            Gravity nearestPlanet = this;
            float closestDistance = Vector3.Distance(transform.position, collider.transform.position);

            foreach (Gravity planet in allPlanetsGravity)
            {
                if (planet == null || planet.GetComponent<Platform>() != null)
                {
                    continue;
                }

                float distance = Vector3.Distance(planet.transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestPlanet = planet;
                }
            }

            if (nearestPlanet != this) return;

            gravityDirection = (transform.position - collider.transform.position).normalized;
            rb.AddForce(gravityDirection * gravityStrength, ForceMode.Acceleration);
        }

        Quaternion targetRotation = Quaternion.FromToRotation(collider.transform.up, -gravityDirection) * collider.transform.rotation;
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
    }
}