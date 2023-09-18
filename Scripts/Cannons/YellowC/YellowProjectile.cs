using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Cinemachine;
using UnityEngine;

public class YellowProjectile : MonoBehaviour
{
    private Transform localtrans;
    private GameObject projectile;

    public Vector3 direction = new Vector3(1, 0, 0);

  [SerializeField]  private float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        localtrans = transform;
        projectile = gameObject;
    }

    void FixedUpdate() { localtrans.position += Time.fixedDeltaTime * direction * speed; }

    public  void SetDirection(Vector3 value) { direction = value;}

    private void OnTriggerEnter(Collider other)
    {
     
        if (other.gameObject.CompareTag("CannonTriggerArea") == true || other.gameObject.CompareTag("GreenCBall") == true)
        {
            //only destroy the projectile when the projectile exits these conditions
        }
        else
        {
            Destroy(projectile);
        }
       

    }
}
