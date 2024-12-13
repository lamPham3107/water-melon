using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class FruitController : MonoBehaviour
{
    private ItemController itemController;
    public string fruitTag;
    public float Stay;

    private GameManage gameManage;

    public Stack<GameObject> Fruit_Destroy_Stack = new Stack<GameObject>();
    public GameObject Destroy_Fruit_1;
    public GameObject Destroy_Fruit_2;

    void Start()
    {
        fruitTag = gameObject.tag;
        gameManage = FindObjectOfType<GameManage>();
        itemController = FindObjectOfType<ItemController>();

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == fruitTag)
        {
                if (!gameManage.Fruit_Destroy_Stack.Contains(gameObject) && !gameManage.Fruit_Destroy_Stack.Contains(collision.gameObject) && !gameManage.isReplace)
                {
                    gameManage.Fruit_Destroy_Stack.Push(gameObject);
                    gameManage.Fruit_Destroy_Stack.Push(collision.gameObject);
                    gameManage.Destroy_Fruit_1 = gameManage.Fruit_Destroy_Stack.Pop();
                    gameManage.Destroy_Fruit_2 = gameManage.Fruit_Destroy_Stack.Pop();
                    gameManage.isReplace = true;
                }          
        }
    }
    

}

