using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResource : MonoBehaviour
{
    public static PlayerResource instance;

    public GameObject mush;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public int plantMush, mineralMush, creatureMush, demonMush;



    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    ISuckable temp = collision.GetComponent<ISuckable>();
    //    if(temp != null)
    //    {
    //        temp.Consume();
    //        plantMush++;
    //        UIManager.instance.plantMush.text = plantMush.ToString();
    //    }
    //}
}
