using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSpawner : MonoBehaviour
{
    public GameObject blueProjectilePrefab;
    private Transform localTrans;

    private bool bEnableSpawner = true;

    void Start() { localTrans = transform; }

    // Update is called once per frame
    void Update()
    {
        if (bEnableSpawner == true)
            StartCoroutine(spawnBall());
    }
    IEnumerator spawnBall()
    {
        bEnableSpawner = false;

        yield return new WaitForSeconds(2);

        Instantiate(blueProjectilePrefab, localTrans.position,
                Quaternion.Euler(localTrans.rotation.x, localTrans.rotation.y, localTrans.rotation.z))
            .GetComponent<BlueProjectile>().SetDirection(localTrans.right);

        Debug.DrawRay(localTrans.position, localTrans.right, Color.green, 5); // Keep this to help devs see the direction the cannon bone is firing.

        bEnableSpawner = true;
    }
}
