using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : DamageableParent
{
    //protected override void CheckForDeath()
    //{
    //    // check if we have died
    //    if (health <= 0)
    //    {
    //        foreach (var item in transform.GetComponents(typeof(Component)))
    //        {
    //            if (item.GetType() == typeof(AudioSource) || item.GetType() == typeof(Transform))
    //                continue;
    //            Destroy(item);
    //        }

    //        var source = GetComponent<AudioSource>();
    //        if (source != null)
    //            Destroy(gameObject, source.clip.length);
    //        else
    //            Destroy(gameObject);
    //    }
    //}
}
