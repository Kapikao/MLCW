using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System; // dla Exception

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")] // ✅ poprawione z [Harder]

    [SerializeField] private string fileName;

    private GameData gamedata;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene");
        }

        Instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gamedata = new GameData();
    }

    public void LoadGame()
    {
        // Load any saved data from file using data handler
        this.gamedata = dataHandler.Load(); // ✅ poprawione (było gameData)

        if (this.gamedata == null)
        {
            Debug.Log("No data was found. Initializing data defaults");
            NewGame();
        }

        // Push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gamedata);
        }
    }

    public void SaveGame()
    {
        // Pass data to other scripts so they can update it 
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gamedata);
        }

        dataHandler.Save(gamedata);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects =
            FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
