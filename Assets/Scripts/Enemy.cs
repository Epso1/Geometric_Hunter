using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyType { Triangle, Square, Circle, Diamond };

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 10f;    
    [SerializeField] private enemyType type;
    [SerializeField] private int pointsRight = 10;
    [SerializeField] private int pointsWrong = 5;
    private Animator anim;
    private Rigidbody2D rb2D;
    private GameController gameController;


    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        anim.SetFloat("Speed", speed / 10);
    }

    void FixedUpdate()
    {
        Vector2 velocity = new Vector2(speed, 0);
        rb2D.MovePosition(rb2D.position - velocity * Time.fixedDeltaTime);
        //transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Contains("Bullet"))
        {
            if (other.tag.Contains(type.ToString()))
            {
                gameController.PlayGoodShot();
                ScoreManager.Instance.Score += pointsRight;
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            else
            {
                gameController.PlayBadShot();
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
