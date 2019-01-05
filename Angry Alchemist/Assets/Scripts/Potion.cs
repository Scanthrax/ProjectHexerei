using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public PotionObject potion;

    Vector3 start, end;
    float dist;

    public AnimationCurve overhandCurve;
    public AnimationCurve underhandCurve;

    public float speed;

    public bool launchPotion = false;
    public bool overHand;

    public GameObject potionExplode;

    public AudioSource source;

    private void Start()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = potion.image;
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(launchPotion)
        {
            transform.position += transform.forward * 0.1f * speed;

            var tempDist = Vector3.Distance(transform.position, start);

            if (tempDist >= dist) 
                Explode();



            if (overHand)
            {
                transform.localScale = Vector3.one + ((Vector3.one * 1f) * overhandCurve.Evaluate(tempDist / dist));
            }
            else
            {
                transform.localScale = Vector3.one + ((Vector3.one * 1f) * underhandCurve.Evaluate(tempDist/dist));
            }

        }


    }


    public void SetStartAndEnd(Vector3 end, bool overHand)
    {
        start = transform.position;
        this.end = end;
        dist = Vector3.Distance(start, end);
        launchPotion = true;
        this.overHand = overHand;

        transform.LookAt(end);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if(overHand)
        {
            gameObject.layer = LayerMask.NameToLayer("Non-solid");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Explode();
    }

    public void Explode()
    {
        var temp = Instantiate(potion.potionSplash, new Vector3(transform.position.x,0,transform.position.z), Quaternion.Euler(90,0,0));
        //temp.GetComponent<SimpleAnimate>().potion = potion;

        foreach (var item in transform.GetComponents(typeof(Component)))
        {
            if (item.GetType() == typeof(AudioSource) || item.GetType() == typeof(Transform))
                continue;
            Destroy(item);
        }
        foreach (var item in transform.GetComponentsInChildren(typeof(Component)))
        {
            if (item.GetType() == typeof(AudioSource) || item.GetType() == typeof(Transform))
                continue;
            Destroy(item);
        }

        source.Play();
        Destroy(gameObject, source.clip.length);
    }
}
