using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    private float time;

    [SerializeField] private GameObject bar;
    // Start is called before the first frame update
    void Start()
    {

        bar.SetActive(false);
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
            if (time >= 1f)
            {
                bar.SetActive(true);

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)

        {

            Debug.Log("ResetTime");
            time = 0f;
            bar.SetActive(false);
        }
    }
}
