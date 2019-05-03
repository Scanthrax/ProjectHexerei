using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResource : MonoBehaviour
{
    public static PlayerResource instance;

    public GameObject mush;
    public GameObject mushSpew;

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

    public static Color GetColor(MushType type)
    {
        return type == MushType.Plant ? Color.green :
            type == MushType.Creature ? Color.yellow :
            type == MushType.Mineral ? Color.blue :
            type == MushType.Demon ? Color.red :
            type == MushType.Null ? Color.gray : Color.black;
    }
}
