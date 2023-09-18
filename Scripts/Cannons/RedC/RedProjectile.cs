using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedProjectile : MonoBehaviour
{

    private Transform localtrans;
    private GameObject projectile;

    [SerializeField] private LayerMask projectileLayer;
    private Vector3 direction = new Vector3(1, 0, 0);

    [SerializeField] private float speed;
    

    // Start is called before the first frame update
    void Start()
    {
        localtrans = transform;
        projectile = gameObject;
    }

    void FixedUpdate(){ localtrans.position += Time.fixedDeltaTime * direction * speed; }

    public void SetDirection(Vector3 value) { direction = value; }

    private void OnTriggerEnter(Collider other)
    {
        //The red projetile destroys any other projectile it touches

        if (other.gameObject.CompareTag("RWall"))
        {
            Destroy(projectile);
        }

        else if (other.gameObject.CompareTag("LWall"))
        {
            Destroy(projectile);
        }

        else if (other.gameObject.CompareTag("TWall"))
        {
            Destroy(projectile);
        }

        else if (other.gameObject.CompareTag("BWall"))
        {
            Destroy(projectile);
        }
        else if (other.gameObject.CompareTag("BlueCBall") || other.gameObject.CompareTag("YellowCBall"))
        {
            Destroy(other);
        }
    }
}
