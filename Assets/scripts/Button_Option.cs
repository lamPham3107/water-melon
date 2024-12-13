using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Option : MonoBehaviour
{
    public GameObject Pn_Question;
    private void Start()
    {
        Pn_Question.SetActive(false);
    }

    public void Back_To_Menu()
    {
        Pn_Question.SetActive(true);

    }
    public void Yes()
    {
        SceneManager.LoadScene(0);
    }
    public void No() 
    {
        Pn_Question.SetActive(false);
    }
}
