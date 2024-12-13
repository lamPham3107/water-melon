using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private GameManage gameManage;

    public float GamePoint;
    public Text Txt_GamePoint;
    public Image NextFruitImage;

    void Start()
    {
        gameManage = FindObjectOfType<GameManage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void getNextImage()
    {
        
    }
}
