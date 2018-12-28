using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour, IDamageable
{
    public float health;

    public void DealDamage()
    {
        health--;

        if (Random.Range(0, 2) == 0)
        {
            var temp = Instantiate(PlayerResource.instance.mush, transform.position + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)), Quaternion.Euler(90f, 0, 0)).GetComponent<Mush>();
            temp.Init(MushType.Plant);
        }

        if (health <= 0)
        {

            foreach (var item in transform.GetComponents(typeof(Component)))
            {
                if (item.GetType() == typeof(AudioSource) || item.GetType() == typeof(Transform))
                    continue;
                Destroy(item);
            }
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
