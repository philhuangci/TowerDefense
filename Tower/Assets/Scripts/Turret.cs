using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();
    public float attactRateTime = 1f;
    public float timer = 0;

    public bool useLaser = false;
    public float damageRate = 60f;

    public GameObject laserEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Enemy")
        {
            enemys.Add(other.gameObject);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Remove(other.gameObject);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        timer = attactRateTime;
        
        
    }

    public GameObject bulletPrefab;
    public Transform firePosition;
    public Transform head;

    public LineRenderer laserRender;

    void Attack()
    {
        if (enemys[0] == null)
        {
            UpdateEnemys();
        }
        if (enemys.Count > 0)
        {
            // init with object and location and rotation
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bulltes>().SetTarget(enemys[0].transform);
        }
        else
        {
            timer = attactRateTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }

        if (useLaser == false)
        {
            timer += Time.deltaTime;
            if (timer >= attactRateTime && enemys.Count > 0)
            {
                timer = 0;
                Attack();
            }
        }
        else if (enemys.Count > 0)
        {
            // use laser
            if (enemys[0] == null)
            {
                UpdateEnemys();
            }
            laserEffect.SetActive(true);
            if (enemys.Count > 0)
            { 

                laserRender.enabled = true;
                laserRender.SetPositions(new Vector3[] { firePosition.position, enemys[0].transform.position });
                enemys[0].GetComponent<Enemy>().TakeDamage(damageRate * Time.deltaTime);
                laserEffect.transform.position = enemys[0].transform.position;
                Vector3 pos = transform.position;
                pos.y = enemys[0].transform.position.y;
                laserEffect.transform.LookAt(pos);
            }

        }
        else
        {
            laserEffect.SetActive(false);
            laserRender.enabled = false;
        }



    }

    void UpdateEnemys()
    {
        enemys.RemoveAll(item => item == (null));

    }
    



}
