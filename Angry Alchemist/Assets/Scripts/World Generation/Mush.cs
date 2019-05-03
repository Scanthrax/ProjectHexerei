using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MushType { Null, Plant, Mineral, Creature, Demon}

public class Mush : MonoBehaviour, ISuckable
{

    MushType type;
    public AudioSource source;
    public Hover hover;
    public Rigidbody rigidBody;
    bool spewing;


    private void Start()
    {
        source.clip = AudioManager.instance.GetRandomSound(AudioManager.instance.SuckMush);
        spewing = true;
    }

    public void Init(MushType type)
    {
        this.type = type;
        GetComponent<SpriteRenderer>().sprite = SpriteManager.instance.GetSprite(type);
    }


    public void Consume()
    {
        if (!spewing)
        {
            switch (type)
            {
                case MushType.Plant:
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
                case MushType.Demon:
                    PlayerResource.instance.demonMush++;
                    UIManager.instance.demonMush.text = PlayerResource.instance.demonMush.ToString();
                    break;

                case MushType.Null:
                    print("Shouldn't be here!");
                    break;
                default:
                    print("Shouldn't be here!");
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
    }

    public void Suck(Vector3 pos, float power)
    {
        if (!spewing)
        {
            print("I am being sucked!");
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * power);
        }
    }

    public void Spew(float intensity)
    {
        spewing = true;
        Vector2 rand = new Vector2(Random.value * intensity, Random.value * intensity);
        rigidBody.AddForce(rand.x - (intensity * 0.5f), 0f, rand.y - (intensity * 0.5f));
        hover.SetHover(false);
    }

    private void Update()
    {
        if(spewing)
        {
            rigidBody.velocity = rigidBody.velocity * 0.85f;
        }
    }
}
