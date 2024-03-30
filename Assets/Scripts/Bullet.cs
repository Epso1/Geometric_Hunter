using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 40f;

    void Update()
    {
        // Mueve la bala hacia adelante
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    // Se llama cuando el objeto se vuelve invisible para la cámara
    private void OnBecameInvisible()
    {
        // Destruye el objeto
        Destroy(gameObject);
    }
}
