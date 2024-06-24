using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Player : MonoBehaviour
{
    InputSystem controls;
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private Transform shotOrigin;
    private GameController gameController;
    private bool canShoot = true;
    [HideInInspector] public bool playerIsDead = false;
    private Animator animator;
   
    void Awake()
    {
        controls = new InputSystem();
        controls.Enable();
        controls.Land.NorthBtn.performed += ctx => ShootTriangle();
        controls.Land.SouthBtn.performed += ctx => ShootDiamond();
        controls.Land.WestBtn.performed += ctx => ShootSquare();
        controls.Land.EastBtn.performed += ctx => ShootCircle();

    }
    
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerIsDead)
        {
            canShoot = false;
        }
        /*
        // SHOOT INPUTS
        if (canShoot == true)
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
        */
    }


    private IEnumerator Shoot(int bulletIndex)
    {
        if (canShoot)
        {
            canShoot = false;
            animator.SetTrigger("Shoot");
            gameController.PlayPlayerShoots();
            Instantiate(bullets[bulletIndex], shotOrigin.position, bullets[bulletIndex].transform.rotation);
            yield return new WaitForSeconds(.1f);
            canShoot = true;
        }        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!playerIsDead)
            {
                PlayerDies();
            }
           
        }
    }

    private void PlayerDies()
    {
        //gameController.SaveGameData();
        playerIsDead = true;
        gameController.PlayPlayerDies();
        gameController.StopBGMusic();
        animator.SetTrigger("Die");
        gameController.StartCoroutine(gameController.ReloadScene());
    } 

    private void ShootTriangle()
    {
        StartCoroutine(Shoot(2));
    }

    private void ShootSquare()
    {
        StartCoroutine(Shoot(0));
    }

    private void ShootCircle()
    {
        StartCoroutine(Shoot(1));
    }

    private void ShootDiamond()
    {
        StartCoroutine(Shoot(3));
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
