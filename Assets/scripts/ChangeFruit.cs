using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFruit : MonoBehaviour
{
    public GameObject Panel_Select_Fruit;
    public Button Cherry;
    public Button Strawberry;
    public Button Grape;
    public Button Lemon;
    public Button Orange;

    public bool is_Lock_Item_Change;
    public int ChooseIndex;
    private bool moving;
    private bool down;
    private Vector3 TargetPosition;

    public Rigidbody2D Change_Fruit_Rb;
    public Transform Fruit;
    private Item_Delete_Select_Fruit item_Delete_Select_Fruit;
    private GameManage gameManage;

    // Start is called before the first frame update
    void Start()
    {
        Panel_Select_Fruit.SetActive(false);
        is_Lock_Item_Change = true;
        gameManage = FindObjectOfType<GameManage>();
        item_Delete_Select_Fruit = FindObjectOfType<Item_Delete_Select_Fruit>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Panel_Active()
    {
        item_Delete_Select_Fruit.is_Lock_Item_Delete = true;
        is_Lock_Item_Change =false;
        Panel_Select_Fruit.SetActive(true);
        Debug.Log("Active: " + is_Lock_Item_Change);
    }
    public void ChooseCherry()
    {
        ChooseIndex = 0;
        Change();
    }
    public void ChooseStrawberry()
    {
        ChooseIndex = 1;
        Change();
    }
    public void ChooseGrape()
    {

        ChooseIndex = 2;
        Change();
    }
    public void ChooseLemon()
    {
        ChooseIndex = 3;
        Change();
    }
    public void ChooseOrange()
    {
        ChooseIndex = 4;
        Change();
    }
    private void Change()
    {
        gameManage.SpawnNewFruit();
        Panel_Select_Fruit.SetActive(false);

    }
}
