using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Cross : MonoBehaviour
{

    public Transform smallExp;

    void Start()
    {
        var temp = Instantiate(smallExp);
        temp.SetParent(transform);
        temp.position = transform.position;
        temp.localEulerAngles = new Vector3(0, 0, 0);

        Transform temp2;

        for (int i = 0; i < 4; i++)
        {

            for (int j = 0; j < 2; j++)
            {
                temp2 = Instantiate(temp, transform);

                Vector2 direction;

                switch(i)
                {
                    case 0:
                        direction = new Vector2(0,1);
                        break;
                    case 1:
                        direction = new Vector2(1,0);
                        break;
                    case 2:
                        direction = new Vector2(0, -1);
                        break;
                    case 3:
                        direction = new Vector2(-1, 0);
                        break;
                    default:
                        direction = Vector2.zero;
                        break;
                }

                temp2.position = transform.position + new Vector3(direction.x * (j + 1), 0, direction.y * (j + 1));
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
