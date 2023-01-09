using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.UI;

public class SaveGameData : MonoBehaviour
{

    public SaveData AutoSave;

    public bool HasLoaded { get; private set; }

    private void Awake()
    {
        Load();
    }

    public void Save()
    {
        string dataPath = Application.persistentDataPath;
        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/" + AutoSave.SaveName + ".save", FileMode.Create);
        serializer.Serialize(stream, AutoSave);
        stream.Close();
    }

    public void Load()
    {
        string dataPath = Application.persistentDataPath;

        if (File.Exists(dataPath + "/" + AutoSave.SaveName + ".save"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + AutoSave.SaveName + ".save", FileMode.Open);
            AutoSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();
            HasLoaded = true;
        }   
    }
}

[System.Serializable]
public class SaveData
{
    public string SaveName;
    public int Level;
    public int BossCount;
    public bool FirstBossFight;
    public bool TutorialMode;
    public bool Tutorial;
    public bool TutorialEnd;
    public int TapSpawnedCatLvl;
    public int AutoSpawnedCatLvl;
    public int Money;
    public int BossLevel;

}
