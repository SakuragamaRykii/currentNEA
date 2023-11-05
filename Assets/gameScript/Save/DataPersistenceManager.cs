using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File storage config")]
    [SerializeField] private string fileName;

    private FileDataHandler dh;

    private DynList<IDataPersistence> IDPObjects;

    private GameData data;
    public static DataPersistenceManager instance { get; private set; }


    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;

    }

    private void Start()
    {
        dh = new FileDataHandler(Application.persistentDataPath, fileName) ;
        IDPObjects = FindAllDPOs();
        LoadGame();
    }

    public void NewGame()
    {
        data = new GameData();
    }
    public void LoadGame()
    {
        data = dh.Load();


        if (data == null)
        {
            Debug.Log("No data available starting a new one");
            NewGame();
        }

        foreach (IDataPersistence p in IDPObjects)
        {
            p.LoadData(data);
        }
    }
    public void SaveGame()
    {
        Debug.Log("saving");
        foreach (IDataPersistence p in IDPObjects)
        {
            p.SaveData(ref data);
        }

        dh.Save(data);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private DynList<IDataPersistence> FindAllDPOs()
    {
        IEnumerable<IDataPersistence> objs = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>(); 
        DynList<IDataPersistence> result = new DynList<IDataPersistence>();
        foreach (IDataPersistence obj in objs) result.add(obj);
        return result;
    }
}
