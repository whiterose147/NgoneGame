using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowSpawner : MonoBehaviour
{
    public GameObject yellowProjectilePrefab;
    private Transform localTrans;

    private bool bEnableSpawner = true;

    // Start is called before the first frame update
    void Start(){ localTrans = transform; }

    // Update is called once per frame
    void Update()
    {
        if(bEnableSpawner == true)
            StartCoroutine(spawnBall());
    }

    IEnumerator spawnBall()
    {
        bEnableSpawner = false;

        yield return new WaitForSeconds(3);
        Instantiate(yellowProjectilePrefab, localTrans.position,
                Quaternion.Euler(localTrans.rotation.x, localTrans.rotation.y, localTrans.rotation.z))
            .GetComponent<YellowProjectile>().SetDirection(localTrans.right);

        Debug.DrawRay(localTrans.position, localTrans.right,Color.green, 5); // Keep this to help devs see the direction the cannon bone is firing.

        bEnableSpawner = true;
    }
}
