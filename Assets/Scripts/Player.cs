using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private float horizMove;
    private float vertMove;
    [SerializeField] private float horizontalSpeed = 20f;  
    [SerializeField] private float verticalShift = 2f;
    [SerializeField] GameObject[] bullets;
    [SerializeField] Transform shotOrigin;
    [SerializeField] private float horizontalLimit = 7f;
    [SerializeField] private float verticalLimit = 4f;
    private bool canMoveVertical = true;
    private bool canShoot= true;


    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
       
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
        yield return new WaitForSeconds(.1f);
        canShoot = true;
    }
}
