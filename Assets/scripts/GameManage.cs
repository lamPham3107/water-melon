using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;
using UnityEngine.EventSystems;

public class GameManage : MonoBehaviour
{
    //[SerializeField] private Transform FruitPrefab;
    [SerializeField] private Transform LinePrefab;
    [SerializeField] public Transform FruitHolder;

    [SerializeField] public List<Transform> FruitPrefabs;
    public List<Transform> currentFruits = new List<Transform>();

    public Transform newFruit = null;
    public Transform Line = null;

    private Rigidbody2D currentRigidbody;

    private float MoveSpeed = 0.35f;
    private float delayTime = 1.5f;
    public int FruitIndex;
    public int CountClick;
    public int CountCollision = 2;
    private int RamdomIndex;

    private bool isFrist = true;
    public bool isMoving = false;
    public bool isDown = false;
    public bool isReplace = false;
    public bool canDropNewFruit = false;
    public bool isHold;
    public bool isMouseHandle = true;

    public Vector2 enterPosition;
    private Vector3 TargetFruitPosition;
    public Vector3 TargetLinePosition;
    private Vector3 FruitStartPosition = new Vector3(0f, 4f, 0f );
    public Vector3 mousePosition;

    private FruitController FruitController;
    private ItemController ItemController;
    private ChangeFruit ChangeFruit;
    private Item_Delete_Select_Fruit Item_Delete_Select_Fruit;

    public float GamePoint;
    public TMP_Text Txt_GamePoint;
    public Image getNextImage;

    public Stack<GameObject> Fruit_Destroy_Stack = new Stack<GameObject>();
    public GameObject Destroy_Fruit_1;
    public GameObject Destroy_Fruit_2;



    // Start is called before the first frame update 
    void Start()
    {
        FruitController  = FindObjectOfType<FruitController>();

        ItemController = FindObjectOfType<ItemController>();

        ChangeFruit = FindObjectOfType<ChangeFruit>();

        Item_Delete_Select_Fruit = FindObjectOfType<Item_Delete_Select_Fruit>();

        SpawnNewFruit();
        Line = Instantiate(LinePrefab);
        Line.position = new Vector2(newFruit.position.x , newFruit.position.y -2f);      
        TargetFruitPosition = newFruit.position;
        isFrist = false;
        CountClick = 0;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {

        if (isReplace)
        {
            if (Destroy_Fruit_1 != null) 
            {
                int.TryParse(Destroy_Fruit_1.gameObject.tag, out FruitIndex);
                enterPosition = (Destroy_Fruit_2.transform.position + Destroy_Fruit_1.transform.position)/2;  
                StartCoroutine(DeLayedReplace());
            }
        }
            MouseInput();       

        if (isMoving ) {
            MoveFruitToTarget();
        }
    }
    private void MouseInput()
    {
        //Debug.Log("UI " +isMouseOverUI());
        if (Input.GetMouseButton(0) && !isMoving && !isDown && !isMouseOverUI() && isMouseHandle && Item_Delete_Select_Fruit.is_Lock_Item_Delete && ChangeFruit.is_Lock_Item_Change)
        {
            Debug.Log("Hold: " + isHold);
            MouseHold();
        }

        else if (Input.GetMouseButtonDown(0) && CountClick < 1 && !isMoving && !isMouseOverUI() && isMouseHandle && Item_Delete_Select_Fruit.is_Lock_Item_Delete && ChangeFruit.is_Lock_Item_Change)
        {
            Debug.Log("Click: " + isHold);
            MouseClick();
        }
        else if (Input.GetMouseButtonUp(0) && !isMouseOverUI() && isHold && Item_Delete_Select_Fruit.is_Lock_Item_Delete && ChangeFruit.is_Lock_Item_Change)
        {
            Debug.Log("Up: " + isHold);
            MouseUp();
        }
    }

    public void SpawnNewFruit()
    {
        if (isFrist)
        {           
            newFruit = Instantiate(FruitPrefabs[RamdomIndex], FruitHolder);
            newFruit.position = FruitStartPosition;
            currentRigidbody = newFruit.GetComponent<Rigidbody2D>();
            TargetFruitPosition = newFruit.position;
            currentFruits.Add(newFruit);
            GetNewFruit();
        }
        else
        {
            if (!ChangeFruit.is_Lock_Item_Change && !isReplace)
            {
                Destroy(newFruit.gameObject);
                newFruit = Instantiate(FruitPrefabs[ChangeFruit.ChooseIndex], FruitHolder);
                ChangeFruit.is_Lock_Item_Change = true;
            }
            else
            {
                newFruit = Instantiate(FruitPrefabs[RamdomIndex], FruitHolder);
                GetNewFruit();
            }
            currentRigidbody = newFruit.GetComponent<Rigidbody2D>();   
            
            newFruit.position = TargetFruitPosition;

            TargetFruitPosition = newFruit.position;

            currentFruits.Add(newFruit);

        }
        isMouseHandle = true;
        CountClick = 0;
    }

    public IEnumerator DeLayedSpawn()
    {
        yield return new WaitForSeconds(delayTime); 
        SpawnNewFruit();
    }
    public void ReplaceFruit()
    {
            Debug.Log("Index: " + FruitIndex);
            Transform ReplaceFruit = Instantiate(FruitPrefabs[FruitIndex + 1], enterPosition, FruitPrefabs[FruitIndex + 1].rotation);
            Debug.Log("Replace: " + ReplaceFruit.name);
            GamePoint += (FruitIndex + 1) * 10;
            Txt_GamePoint.text = GamePoint.ToString();
            ReplaceFruit.GetComponent<Rigidbody2D>().isKinematic = false;

            if (ReplaceFruit.tag == "8")
            {
                Debug.Log("win");
            }       
    }

    private void GetNewFruit()
    {

        RamdomIndex = UnityEngine.Random.Range(0, 4);

        Sprite nextFruitSprite = FruitPrefabs[RamdomIndex].GetComponent<SpriteRenderer>().sprite;
        
        getNextImage.sprite = nextFruitSprite;
       // getNextImage.rectTransform.localPosition = new Vector3(0.5f, 0.5f, 1f);
    }
    private IEnumerator DeLayedReplace()
    {
        float delayReplace = 0.2f;
        Debug.Log("destroy : " + Destroy_Fruit_1.name);
        Destroy(Destroy_Fruit_1);
        Destroy(Destroy_Fruit_2);
        yield return new WaitForSeconds(delayReplace);
        ReplaceFruit() ;
        isReplace = false;

    }

    public bool isMouseOverUI()
    {
        if (Input.touchCount > 0)
        {
            return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
        }
        else
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }

    private void MouseClick()
    {
        isMouseHandle = false;
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(newFruit.position).z));
        if (mousePosition.y > -4.8f && Item_Delete_Select_Fruit.is_Lock_Item_Delete && ChangeFruit.is_Lock_Item_Change)
        {
            CountClick++;
            mousePosition.x = Mathf.Clamp(mousePosition.x, -3.1f, 2.7f);
            currentRigidbody.isKinematic = true;
            isMoving = true;
            isDown = true;
            TargetFruitPosition = new Vector3(mousePosition.x, FruitStartPosition.y, FruitStartPosition.z);
            TargetLinePosition = new Vector3(mousePosition.x, FruitStartPosition.y - 2f, FruitStartPosition.z);
            ChangeFruit.is_Lock_Item_Change = false;
        }
        
    }

    private void MouseHold()
    {
            //isMouseHandle = false;
            isHold = true;           
            CountClick++;
            if (newFruit != null)
            {
                mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(newFruit.position).z));
                mousePosition.x = Mathf.Clamp(mousePosition.x, -3.1f, 2.7f);
                TargetFruitPosition = new Vector3(mousePosition.x, FruitStartPosition.y, FruitStartPosition.z);
                TargetLinePosition = new Vector3(mousePosition.x, FruitStartPosition.y - 2f, FruitStartPosition.z);
                isMoving = true;
            }
        
    }
    private void MouseUp()
    {
        
        if (newFruit != null)
        {        
            isMouseHandle = false;
            isDown = true;
            isMoving = true;
            isHold = false;
        }
    }
    private void MoveFruitToTarget()
    {
        newFruit.position = Vector3.MoveTowards(newFruit.position, TargetFruitPosition, MoveSpeed);
        Line.position = Vector3.MoveTowards(Line.position, TargetLinePosition, MoveSpeed);

        if (Vector3.Distance(newFruit.position, TargetFruitPosition) < 0.01f)
        {
            MoveSpeed = 0.2f;
            isMoving = false;
            ChangeFruit.is_Lock_Item_Change = true;
            if (isDown && ChangeFruit.is_Lock_Item_Change)
            {

                isDown = false;
                currentRigidbody.isKinematic = false;
                //currentRigidbody.gravityScale = 1f;   
                ChangeFruit.is_Lock_Item_Change = true;
                StartCoroutine(DeLayedSpawn());
            }
        }
    }
}
