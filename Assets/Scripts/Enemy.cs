using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 10f;
    public enum enemyType { Triangle, Square, Circle, Diamond };
    [SerializeField] private enemyType type;
    [SerializeField] private int pointsRight = 10;
    [SerializeField] private int pointsWrong = 5;

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Contains("Bullet"))
        {
            if (other.tag.Contains(type.ToString()))
            {
                ScoreManager.Instance.Score += pointsRight;
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            else
            {
                other.gameObject.GetComponent<Bullet>().SetDirection(Vector3.down);
                ScoreManager.Instance.Score -= pointsWrong;
            }
        }
        
    }

    // Se llama cuando el objeto se vuelve invisible para la cámara
    private void OnBecameInvisible()
    {
        // Destruye el objeto
        Destroy(gameObject);
    }

}
