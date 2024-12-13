using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Delete_Select_Fruit : MonoBehaviour
{
    public bool is_Lock_Item_Delete;
    private GameManage gameManage;
    
    // Start is called before the first frame update
    void Start()
    {
        is_Lock_Item_Delete = true;
        gameManage = FindObjectOfType<GameManage>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!is_Lock_Item_Delete) 
        {
            if (Input.GetMouseButtonDown(0) && !gameManage.isMouseOverUI()) 
            {
                Delete_Select_Fruit(); 
            }
        }
    }
    private void Delete_Select_Fruit()
    {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // kiem tra xem co click vo phan tu nao k
        RaycastHit2D hit = Physics2D.Raycast(MousePos, Vector2.zero);

        if (hit.collider != null && !hit.rigidbody.isKinematic)
        {
            Destroy(hit.collider.gameObject);
            StartCoroutine(DelayUnlock());
        }
    }
    private IEnumerator DelayUnlock()
    {

        yield return new WaitForSeconds(0.5f);
        is_Lock_Item_Delete = true;
    }
    public void Bt_Select_Delelte()
    {
        is_Lock_Item_Delete = false;
    }
}
