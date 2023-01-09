using UnityEngine;

[CreateAssetMenu(fileName = "New AudioData", menuName = "Audio Data", order = 51)]
public class AudioData : ScriptableObject
{

    public AudioClipsList[] AudioClips;

}

[System.Serializable]
public class AudioClipsList
{
    public string name;
    public AudioClip AudioClip;
}
