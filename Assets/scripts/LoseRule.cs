using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseRule : MonoBehaviour
{
    private float time;
    //public bool isEndGame;

    public TMP_Text Txt_EndGamePoint;
    public GameObject Pn_EndGame;
    private GameManage gameManage;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        gameManage = FindObjectOfType<GameManage>();
        Pn_EndGame.SetActive(false);
        //isEndGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            time += Time.deltaTime;
            if(time >= 1.5f)
            {
                Debug.Log("lose");
                GameOver();

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
            
        {
            Debug.Log("ResetTime");
            time = 0f;
        }
    }
    private void GameOver()
    {
        time = 0f;
        Time.timeScale = 0f;
        Txt_EndGamePoint.text = gameManage.GamePoint.ToString();
        Pn_EndGame.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

}
