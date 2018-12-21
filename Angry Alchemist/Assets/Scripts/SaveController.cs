using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class SaveController : MonoBehaviour
{
    public static SaveController instance;

    Save save;
    BinaryFormatter bf;
    FileStream fileStream;
    string path;


    public Transform player;
    public World world;

    //private void Awake()
    //{
    //    #region Singleton
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else if (instance != this)
    //    {
    //        Destroy(this);
    //    }
    //    DontDestroyOnLoad(gameObject);
    //    #endregion

    //    bf = new BinaryFormatter();
    //    path = Application.persistentDataPath + "/gamesave.save";
    //    DeleteSaveFile();
    //}



    public void LoadGame()
    {
        #region Return if there is no save file
        if (!File.Exists(path))
        {
            print("No file to load from!");
            return;
        }
        #endregion
        #region Open the file & retrieve the save
        fileStream = File.Open(path, FileMode.Open);
        save = bf.Deserialize(fileStream) as Save;
        #endregion

        #region Make use of the save data here
        player.position = save.playerPosition.ToVector2();
        world.chunkMap = save.chunkMap;
        world.chunkTransform = save.chunkTransform;
        #endregion

        #region Serialize & close the file
        bf.Serialize(fileStream, save);
        fileStream.Close();
        print("Loaded from file");
        #endregion
    }



    public void SaveGame()
    {
        #region Create new file if one does not already exist
        if (!File.Exists(path))
        {
            print("Creating save file");
            fileStream = new FileStream(path, FileMode.Create);
            save = new Save();
        }
        #endregion
        #region Otherwise, open an existing save file
        else
        {
            print("Save file already exists");
            fileStream = File.Open(path, FileMode.Open);
            save = bf.Deserialize(fileStream) as Save;
        }
        #endregion

        #region Save any necessary content here

        save.playerPosition = new Position(player);
        save.chunkMap = world.chunkMap;
        save.chunkTransform = world.chunkTransform;
        #endregion

        #region Serialize & close the file
        bf.Serialize(fileStream, save);
        fileStream.Close();
        Debug.Log("Game Saved");
        #endregion
    }



    /// <summary>
    /// Deletes the save file
    /// </summary>
    public void DeleteSaveFile()
    {
        print("Deleting save file");
        File.Delete(path);
    }
}
