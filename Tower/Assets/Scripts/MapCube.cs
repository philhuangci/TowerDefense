using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
   
    [HideInInspector]
    public GameObject turrentGo; // save current Turret on the cube
    public GameObject buildEffect;
    private Renderer render;
    [HideInInspector]
    public bool isUpgraded = false;
    [HideInInspector]
    public TurretData turretData;


    private void Start()
    {
        render = GetComponent<Renderer>();
    }
    public void BuildTurret(TurretData  turretPrefab)
    {
        this.turretData = turretPrefab;
        isUpgraded = false;
        Vector3 transmit1;
        transmit1.x = 0;
        transmit1.y = 0;
        transmit1.z = 0;
        Debug.LogError(transform.position);
        if (turretPrefab.turretType == TurretType.StanderdTurret)
        {
            transmit1.y += 0.5f;
        }

        turrentGo = GameObject.Instantiate(turretPrefab.turretPrefab, transform.position + transmit1, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);

    }
    private void OnMouseEnter()
    {
        if (this.gameObject.name.Contains("RoadCube"))
            return;
        // Debug.Log("OnMouseEnter");
        if (turrentGo == null 
            && EventSystem.current.IsPointerOverGameObject() == false)
        {
            render.material.color = Color.red;
        }

        
    }

    private void OnMouseExit()
    {
        if (this.gameObject.name.Contains("RoadCube"))
            return;

        render.material.color = Color.white;
    }

    public void Upgrade()
    {
        if (isUpgraded)
            return;

        Destroy(turrentGo);

        isUpgraded = true;
        Vector3 transmit1;
        transmit1.x = 0;
        transmit1.y = 0;
        transmit1.z = 0;
        Debug.LogError(transform.position);
        if (turretData.turretType == TurretType.StanderdTurret)
        {
            transmit1.y += 0.5f;
        }

        turrentGo = GameObject.Instantiate(turretData.turretUpgradedPrefab, transform.position + transmit1, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }

    public void DistoryTurret()
    {
        Destroy(turrentGo);
        turrentGo = null;
        turretData = null;
        isUpgraded = false;
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }
  
}
