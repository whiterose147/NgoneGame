using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BlueProjectile : MonoBehaviour
{
    private Transform localtrans;
    private GameObject projectile;

    public Vector3 direction = new Vector3(1, 0, 0); // needs to be public so other projectiles can interact.

    [SerializeField] private float speed;
    private float localTime;


    // Start is called before the first frame update
    void Start()
    {
        localtrans = transform;
        projectile = gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        localTime += Time.deltaTime;
        localTime = Mathf.Clamp(localTime, 0, 4);
        if (localTime >= 4)
        {
            localTime = 0;
            Destroy(projectile);
        }

        localtrans.position += Time.fixedDeltaTime * direction * speed;
    }

    public void SetDirection(Vector3 value) { direction = value; }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("RedCBall"))
        {
            Destroy(projectile);
        }
        
        // This will determine how the blue projectiles bounce around the rooms
        else if (other.gameObject.CompareTag("RWall"))
        {
            direction = Vector3.Reflect(direction, new Vector3(-1, 0, 0));
        }

        else if (other.gameObject.CompareTag("LWall"))
        {
            direction = Vector3.Reflect(direction, new Vector3(1, 0, 0));
        }

        else if (other.gameObject.CompareTag("TWall"))
        {
            direction = Vector3.Reflect(direction, new Vector3(0, 0, -1));
        }

        else if (other.gameObject.CompareTag("BWall"))
        {
            direction = Vector3.Reflect(direction, new Vector3(0, 0, 1));
        }

    }
}
