using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // Referência direta ao objeto "Player"
    [SerializeField] private Vector3 offset;

    void Start()
    {
        if (playerTransform == null)
        {
            // Busca o objeto "Player" por tag (opcional)
            playerTransform = GameObject.FindWithTag("Player").transform;
            offset = playerTransform.position - transform.position;
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position - offset;
        }
    }
}