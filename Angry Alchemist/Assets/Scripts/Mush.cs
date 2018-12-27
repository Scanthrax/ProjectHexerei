using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MushType { Null, Plant, Mineral, Creature, Demon}

public class Mush : MonoBehaviour, ISuckable
{

    MushType type;
    AudioSource source;

    public void Init(MushType type)
    {
        this.type = type;
        GetComponent<SpriteRenderer>().sprite = SpriteManager.instance.GetSprite(type);
        source = GetComponent<AudioSource>();
        source.clip = AudioManager.instance.GetRandomSound(AudioManager.instance.SuckMush);
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
            case MushType.Creature:
                PlayerResource.instance.creatureMush++;
                UIManager.instance.creatureMush.text = PlayerResource.instance.creatureMush.ToString();
                break;
            case MushType.Null:
                print("Shouldn't be here!");
                break;
            default:
                break;
        }

        foreach (var item in transform.GetComponents(typeof(Component)))
        {
            if (item.GetType() == typeof(AudioSource) || item.GetType() == typeof(Transform))
                continue;
            Destroy(item);
        }

        source.Play();
        Destroy(gameObject, source.clip.length);
    }

    public void Suck(Vector3 pos, float power)
    {
        print("I am being sucked!");
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * power);
    }

}
