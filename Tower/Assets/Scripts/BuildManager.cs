using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class BuildManager : MonoBehaviour
{
    public TurretData laserTurretData;
    public TurretData missilTurretData;
    public TurretData standerTurretData;

    private MapCube selectedMapCube;
    // selected turret
    public Text moneyText;

    private int money = 1000;

    public GameObject UpgradeCanvas;
    public Button ButtonUpgrade;


    public Animator moneyAnimator;




    private Animator upgradeCanvasAnimator;


    void ChangeMoney(int change = 0)
    {
        money += change;
        moneyText.text = "$" + money;
    }


    private void Start()
    {
        upgradeCanvasAnimator = UpgradeCanvas.GetComponent<Animator>();
    }


    // currently selected the Turret
    private TurretData selectedTurret;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) )// left mouse button
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                // turret build
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (mapCube.name.Contains("RoadCube"))
                        return;
                    if (mapCube.turrentGo == null && selectedTurret != null)
                    {
                        // ok to build turret
                        if (money > selectedTurret.cost)
                        {
                            ChangeMoney(-selectedTurret.cost);
                            mapCube.BuildTurret(selectedTurret);
                        }
                        else
                        {
                            // TODO reminder money is not enough
                            moneyAnimator.SetTrigger("Flicker");



                        }
                    }
                    else if (selectedTurret != null)
                    {
                        


                        if (mapCube == selectedMapCube && UpgradeCanvas.activeInHierarchy)
                        {
                            StartCoroutine(HideUpgradeUI());
                            // HideUpgradeUI();
                        }
                        else
                        {
                            ShowlUpgradeUI(mapCube.transform.position,
                                        mapCube.isUpgraded);
                            // upgrade turret
                            
                        }
                        selectedMapCube = mapCube;



                    }
                }
            }
        }
    }

    public void OnLaserSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurret = laserTurretData;
        }
    }
    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurret = missilTurretData;
        }

    }
    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurret = standerTurretData;
            
        }

    }

    public void OnUpgradeButtonDown()
    {
        // Debug.Log("OnUpgrade");

        if (money >= selectedMapCube.turretData.costUpgrade)
        {
            ChangeMoney(-selectedMapCube.turretData.costUpgrade);
        }
        else
        {
            moneyAnimator.SetTrigger("Flicker");
            return;
        }


        selectedMapCube.Upgrade();
        StartCoroutine(HideUpgradeUI());
        // HideUpgradeUI();
  
    }

    public void OnDestoryButtonDown()
    {
        selectedMapCube.DistoryTurret();
        //HideUpgradeUI();
        StartCoroutine(HideUpgradeUI());
    }

    void ShowlUpgradeUI(Vector3 pos, bool isDisableUpgrade = false)
    {
        StopCoroutine("HideUpgradeUI");
        UpgradeCanvas.SetActive(false);

        UpgradeCanvas.transform.position = pos;
        ButtonUpgrade.interactable = !isDisableUpgrade;
        if (isDisableUpgrade)
            Debug.Log("ture");
        else
            Debug.Log("false");
        UpgradeCanvas.SetActive(true);
    }

    IEnumerator HideUpgradeUI()
    {
        upgradeCanvasAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(0.8f);
        UpgradeCanvas.SetActive(false);
    }


}

