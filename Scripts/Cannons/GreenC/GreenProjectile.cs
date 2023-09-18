using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenProjectile : MonoBehaviour
{
    private Transform localtrans;

    private GameObject projectile;
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

        // This area determines how the cube projectiles bounce off of other cannon's projetiles. 

        if (other.gameObject.CompareTag("CannonTriggerArea") == true)
        {
            //do nothing
        }
        else if (other.gameObject.CompareTag("YellowCBall") == true)
        {
            direction = other.GetComponent<YellowProjectile>().direction;
            other.GetComponent<YellowProjectile>().direction = direction * -1;
        }
        else if (other.gameObject.CompareTag("BlueCBall") == true)
        {
            direction = other.GetComponent<BlueProjectile>().direction;
        }
        else
        {
            Destroy(projectile);
        }

    }
}
