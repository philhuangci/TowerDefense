using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject explosionEffect;
    private Transform[] positions;
    private int index = 0;
    public float speed = 10f;
    public float hp = 150;
    private Slider hpSlider;
    private float totalhp;


    // Start is called before the first frame update
    void Start()
    {
        positions = Wappoints.positions;
        totalhp = hp;
        hpSlider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Move();
    }


    void Move()
    {
        if (index > positions.Length - 1)
            return;
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2)
        {
            index++;
        }

        if (index > positions.Length - 1)
        {
            ReachDestination();
        }


    }

    void ReachDestination()
    {
        GameManger.Instance.Failed();
        GameObject.Destroy(this.gameObject);
    }


    private void OnDestroy()
    {
        EnemySpanwner.countEnemyAlive--;
    }

    public void TakeDamage(float damage)
    {
        if (hp <= 0)
            return;

        hp -= damage;
        hpSlider.value = (float)hp / totalhp;

        if (hp <= 0)
        {
            Die();
        }

    }
    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 1);
        Destroy(this.gameObject);

    }

}
