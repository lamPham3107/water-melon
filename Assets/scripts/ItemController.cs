using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{

    public Button Bt_Delete_Top_Fruit;   
    private GameManage gameManage;
    private FruitController fruitController;
    private Item_Delete_Select_Fruit item_Delete_Select_Fruit;
    private List<GameObject> allObject = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        gameManage = FindObjectOfType<GameManage>();
        fruitController = FindAnyObjectByType<FruitController>();
        item_Delete_Select_Fruit = FindObjectOfType<Item_Delete_Select_Fruit>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Delete_Top_Fruit()
    {
        item_Delete_Select_Fruit.is_Lock_Item_Delete = true;
        Debug.Log(allObject.Count);
        if (allObject.Count > 0)
        {
            List<GameObject> objectsToDestroy = new List<GameObject>(allObject);

            foreach (GameObject obj in objectsToDestroy)
            {
                Destroy(obj);
            }
            allObject.Clear();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!allObject.Contains(collision.gameObject))
        {
            allObject.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (allObject.Contains(collision.gameObject))
        {
            allObject.Remove(collision.gameObject);
        }
    }



}
