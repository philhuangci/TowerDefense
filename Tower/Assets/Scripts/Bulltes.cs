using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulltes : MonoBehaviour
{
    public int damage = 50;
    public float speed = 20f;
    private Transform target;

    public GameObject explosionEffectPrefab;


    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Die();
            return;
        }
        else
        {
            transform.LookAt(target.position);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;

        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
            Destroy(effect, 1);
            Destroy(this.gameObject);
        }
    }

    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        // Destroy(effect, 1);
        Destroy(this.gameObject);
    }

}
