using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 40f;
    private Vector3 direction;
    void Start()
    {
        direction = Vector3.right;
    }

    void Update()
    {
        // Mueve la bala hacia adelante
        transform.Translate(direction * speed * Time.deltaTime);
    }

    // Se llama cuando el objeto se vuelve invisible para la cámara
    private void OnBecameInvisible()
    {
        // Destruye el objeto
        Destroy(gameObject);
    }

   
    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
}
