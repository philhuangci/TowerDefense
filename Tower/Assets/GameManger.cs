using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GameManger Instance;
    private EnemySpanwner enemySpanwer;


    private void Awake()
    {
        Instance = this;
        enemySpanwer = GetComponent<EnemySpanwner>();
    }




    public GameObject endUI;
    public Text endLMessage;

    public void Win()
    {
        endUI.SetActive(true);
        endLMessage.text = "WIN";
    }

    public void Failed()
    {
        enemySpanwer.Stop(); 
        endUI.SetActive(true);
        endLMessage.text = "LOST";
    }

    public void OnButtonRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnButtonMenu()
    {
        SceneManager.LoadScene(0);
    }

}
