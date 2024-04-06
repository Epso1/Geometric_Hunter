using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private Transform shotOrigin;
    private GameController gameController;
    private bool canShoot = true;
    private Animator animator;

    public bool CanShoot
    {
        get => canShoot;
        set => canShoot = value;
    }
    
    void Start()
    {
        canShoot = true;
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // SHOOT INPUTS
        if (canShoot)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetButtonDown("Jump"))
            {
                StartCoroutine(Shoot(2));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetButtonDown("Fire1"))
            {
                StartCoroutine(Shoot(3));
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetButtonDown("Fire3"))
            {
                StartCoroutine(Shoot(0));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetButtonDown("Fire2"))
            {
                StartCoroutine(Shoot(1));
            }
        }
    }


    private IEnumerator Shoot(int bulletIndex)
    {
        animator.SetTrigger("Shoot");
        canShoot = false;
        Instantiate(bullets[bulletIndex], shotOrigin.position, bullets[bulletIndex].transform.rotation);
        yield return new WaitForSeconds(.25f);
        canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(PlayerDies());
        }
    }

    private IEnumerator PlayerDies()
    {
        yield return new WaitForSeconds(0.01f);
        
        canShoot = false;
        animator.SetTrigger("Die");
        gameController.StartCoroutine(gameController.ReloadScene());     
    } 
}
