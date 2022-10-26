using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float initialForce;
    public float lifeTime;
    public float lifeTimer = 0f;
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //add the initial force to the rigidbody attached to this grenade
        GetComponent<Rigidbody>().AddRelativeForce(0,0,initialForce);
    }

    // Update is called once per frame
    void Update()
    {
        //update the timer
        lifeTimer += Time.deltaTime;

        //destroy Projectile (grenade) if time runs out
        if(lifeTimer >= lifeTime){
            Explode();
        }
    }

    private void Explode() {
            //instantiate the explosion prefab
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            //destroy the grenade
            Destroy(gameObject);
    }
}
