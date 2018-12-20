using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MushType { Null, Plant, Mineral, Creature, Demon}

public class Mush : MonoBehaviour, ISuckable
{
    public PlayerResource pr;

    MushType type;


    public void Init(MushType type)
    {
        this.type = type;
        GetComponent<SpriteRenderer>().sprite = SpriteManager.instance.GetSprite(type);
    }


    public void Consume()
    {
        switch(type)
        {
            case MushType.Plant:
                print("consuming plant mush!!");
                PlayerResource.instance.plantMush++;
                UIManager.instance.plantMush.text = PlayerResource.instance.plantMush.ToString();
                break;
            case MushType.Mineral:
                PlayerResource.instance.mineralMush++;
                UIManager.instance.mineralMush.text = PlayerResource.instance.mineralMush.ToString();
                break;
            case MushType.Null:
                print("Shouldn't be here!");
                break;
            default:
                break;
        }

        Destroy(gameObject);
    }

    public void Suck(Vector2 pos, float power)
    {
        print("I am being sucked!");
        transform.position = Vector2.MoveTowards(transform.position, pos, Time.deltaTime * power);
    }

}
