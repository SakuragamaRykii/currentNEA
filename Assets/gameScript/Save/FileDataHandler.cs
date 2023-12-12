using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDataHandler 
{
    private string dataDirPath, dataFileName;
    public FileDataHandler(string ddp, string dfn)
    {
        dataDirPath = ddp;
        dataFileName = dfn;
    }
    
    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if(File.Exists(fullPath)) 
        {
            try
            {
                string toLoad = "";
                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                       toLoad = reader.ReadToEnd();
                    }
                }//at this point the serialized data is loaded in string

                loadedData = JsonUtility.FromJson<GameData>(toLoad); //deserialises specified string
                
            }catch(Exception e) 
            {
                Debug.LogError("Error when trying to load: " + fullPath + "\n" + e);
            }
        }
        Debug.Log(fullPath);
        PlayerStat.fileToDelete = fullPath;
        return loadedData;
    }

    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string toStore = JsonUtility.ToJson(data, true);//the bool formats the json

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))//opesn it now and closes when finished
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(toStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error when trying to save in: " + fullPath + "\n" + e );
        }
        PlayerStat.fileToDelete = fullPath;


    }

    public void Clear()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            File.Delete(fullPath);
        }catch(Exception e)
        {
            Debug.LogError("Error when deleting: " + fullPath + "\n" + e);
        }
    }

}
