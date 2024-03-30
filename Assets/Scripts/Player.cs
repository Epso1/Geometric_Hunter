using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private Transform shotOrigin;
    private bool canShoot = true;
    private GameController gameController;

    public bool CanShoot
    {
        get => canShoot;
        set => canShoot = value;
    }
   
    void Start()
    {
       gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
       canShoot = true;
    }

    void Update()
    {
        // SHOOT INPUTS
        if (canShoot)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetButtonDown("Jump"))
            {
                StartCoroutine(Shoot(0));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetButtonDown("Fire1"))
            {
                StartCoroutine(Shoot(1));
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetButtonDown("Fire3"))
            {
                StartCoroutine(Shoot(2));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetButtonDown("Fire2"))
            {
                StartCoroutine(Shoot(3));
            }
        }
    }


    private IEnumerator Shoot(int bulletIndex)
    {
        canShoot = false;
        Instantiate(bullets[bulletIndex], shotOrigin.position, bullets[bulletIndex].transform.rotation);
        yield return new WaitForSeconds(.05f);
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
        gameController.Score += 10;
        gameController.StartCoroutine(gameController.ReloadScene());

        Destroy(gameObject);      
    } 
}
