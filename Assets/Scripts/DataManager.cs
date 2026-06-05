using UnityEngine;
using System.IO;

public class PlayData
{
    public Vector2 lastSpawnPoint = new Vector2(-12f, -5.13f);
    public int lastSceneIndex = 3;
}

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance {  get { return instance; } }

    public PlayData NowPlayData { get; set; } = new PlayData();

    private string path;
    public int SaveSlot { private get; set; } = -1;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        path = Application.persistentDataPath + "/saveFile";
    }

    public void SaveData()
    {
        string filePath = path + SaveSlot.ToString();
        string data = JsonUtility.ToJson(NowPlayData);

        if (!File.Exists(filePath))
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            fileStream.Dispose();
            File.WriteAllText(filePath, data);
        }
        else
        {
            File.WriteAllText(filePath, data);
        }
}

    public void LoadData()
    {
        string filePath = path + SaveSlot.ToString();

        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            NowPlayData = JsonUtility.FromJson<PlayData>(data);
        }
    }
}
