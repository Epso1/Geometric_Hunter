using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTests
{
    private GameObject bulletSquarePrefab;
    private GameObject bulletCirclePrefab;
    private GameObject bulletTrianglePrefab;    
    private GameObject bulletDiamondPrefab;
    private GameObject playerGameObject;
    private Player player;

    [SetUp]
    public void SetUp()
    {
        // Asignar las referencias de los prefabs
        bulletSquarePrefab = Resources.Load<GameObject>("Bullet Square");
        bulletCirclePrefab = Resources.Load<GameObject>("Bullet Circle");
        bulletTrianglePrefab = Resources.Load<GameObject>("Bullet Triangle");
        bulletDiamondPrefab = Resources.Load<GameObject>("Bullet Diamond");    

        // Añadir el componente Player al GameObject
        playerGameObject = new GameObject();
        player = playerGameObject.AddComponent<Player>();

        // Añadir los prefabs a la lista bullets
        player.bullets = new GameObject[] { bulletSquarePrefab, bulletCirclePrefab, bulletTrianglePrefab, bulletDiamondPrefab };

        // Configurar shotOrigin
        var shotOriginObject = new GameObject();
        player.shotOrigin = shotOriginObject.transform;
    }



    [UnityTest]
    public IEnumerator ShootSquareEnum()
    {
        // Ejecutar la coroutine Shoot con índice 0
        yield return player.StartCoroutine(player.Shoot(0));

        // Verificar si un objeto instanciado en la escena corresponde al prefab Bullet Square
        GameObject instantiatedBulletSquare = GameObject.Find(bulletSquarePrefab.name + "(Clone)");
        Assert.IsNotNull(instantiatedBulletSquare, "Bullet Square prefab was not instantiated.");
        UnityEngine.Object.DestroyImmediate(instantiatedBulletSquare);
    }

    [UnityTest]
    public IEnumerator ShootCircleEnum()
    {
        // Ejecutar la coroutine Shoot con índice 1
        yield return player.StartCoroutine(player.Shoot(1));

        // Verificar si un objeto instanciado en la escena corresponde al prefab Bullet Circle
        GameObject instantiatedBulletCircle = GameObject.Find(bulletCirclePrefab.name + "(Clone)");
        Assert.IsNotNull(instantiatedBulletCircle, "Bullet Circle prefab was not instantiated.");
        UnityEngine.Object.DestroyImmediate(instantiatedBulletCircle);
    }


    [UnityTest]
    public IEnumerator ShootTriangleEnum()
    {
        // Ejecutar la coroutine Shoot con índice 2
        yield return player.StartCoroutine(player.Shoot(2));

        // Verificar si un objeto instanciado en la escena corresponde al prefab Bullet Triangle
        GameObject instantiatedBulletTriangle = GameObject.Find(bulletTrianglePrefab.name + "(Clone)");
        Assert.IsNotNull(instantiatedBulletTriangle, "Bullet Triangle prefab was not instantiated.");
        UnityEngine.Object.DestroyImmediate(instantiatedBulletTriangle);
    }

    [UnityTest]
    public IEnumerator ShootDiamondEnum()
    {
        // Ejecutar la coroutine Shoot con índice 3
        yield return player.StartCoroutine(player.Shoot(3));

        // Verificar si un objeto instanciado en la escena corresponde al prefab Bullet Diamond
        GameObject instantiatedBulletDiamond = GameObject.Find(bulletDiamondPrefab.name + "(Clone)");
        Assert.IsNotNull(instantiatedBulletDiamond, "Bullet Diamond prefab was not instantiated.");
        UnityEngine.Object.DestroyImmediate(instantiatedBulletDiamond);
    }

    [UnityTest]
    public IEnumerator ShootSquare()
    {
        player.ShootSquare();
        yield return new WaitForSeconds(0.05f);

        // Verificar si un objeto instanciado en la escena corresponde al prefab Bullet Square
        GameObject instantiatedBulletSquare = GameObject.Find(bulletSquarePrefab.name + "(Clone)");
        Assert.IsNotNull(instantiatedBulletSquare, "Bullet Square prefab was not instantiated.");
        UnityEngine.Object.DestroyImmediate(instantiatedBulletSquare);
    }

    [UnityTest]
    public IEnumerator ShootCircle()
    {
        player.ShootCircle();
        yield return new WaitForSeconds(0.05f);

        // Verificar si un objeto instanciado en la escena corresponde al prefab Bullet Circle
        GameObject instantiatedBulletCircle = GameObject.Find(bulletCirclePrefab.name + "(Clone)");
        Assert.IsNotNull(instantiatedBulletCircle, "Bullet Circle prefab was not instantiated.");
        UnityEngine.Object.DestroyImmediate(instantiatedBulletCircle);
    }


    [UnityTest]
    public IEnumerator ShootTriangle()
    {
        player.ShootTriangle();
        yield return new WaitForSeconds(0.05f);

        // Verificar si un objeto instanciado en la escena corresponde al prefab Bullet Triangle
        GameObject instantiatedBulletTriangle = GameObject.Find(bulletTrianglePrefab.name + "(Clone)");
        Assert.IsNotNull(instantiatedBulletTriangle, "Bullet Triangle prefab was not instantiated.");
        UnityEngine.Object.DestroyImmediate(instantiatedBulletTriangle);
    }

    [UnityTest]
    public IEnumerator ShootDiamond()
    {
        player.ShootDiamond();
        yield return new WaitForSeconds(0.05f);

        // Verificar si un objeto instanciado en la escena corresponde al prefab Bullet Diamond
        GameObject instantiatedBulletDiamond = GameObject.Find(bulletDiamondPrefab.name + "(Clone)");
        Assert.IsNotNull(instantiatedBulletDiamond, "Bullet Diamond prefab was not instantiated.");
        UnityEngine.Object.DestroyImmediate(instantiatedBulletDiamond);
    }




}

