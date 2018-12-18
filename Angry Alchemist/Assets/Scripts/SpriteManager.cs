using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager instance;

    Dictionary<string, Sprite> tileSprites;

    public List<Sprite> sprites;

    private void Awake()
    {
        instance = this;

        tileSprites = new Dictionary<string, Sprite>();

        foreach (var item in sprites)
        {
            tileSprites.Add(item.name, item);
        }

    }

    public Sprite GetSprite(Tile tile)
    {
        if(!tileSprites.ContainsKey(tile.type.ToString()))
        {
            print("The sprite could not be found in the dictionary: " + tile.type.ToString());
            return null;
        }

        return tileSprites[tile.type.ToString()];
    }
}
