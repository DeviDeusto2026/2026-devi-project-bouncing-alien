using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("Ajustes de Aceleración")]
    [Tooltip("Multiplica la fuerza de gravedad dentro de este túnel")]
    public float forceMultiplier = 2f;

    [Header("Ajustes de Frenado")]
    [Tooltip("Porcentaje de freno al llegar al final del túnel (0 = nada, 1 = parada total)")]
    [Range(0f, 1f)]
    public float brakeFactor = 0.5f;
}