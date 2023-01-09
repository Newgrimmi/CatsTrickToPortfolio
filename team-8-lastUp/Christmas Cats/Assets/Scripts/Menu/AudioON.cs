using UnityEngine;
using UnityEngine.UI;

public class AudioON : MonoBehaviour
{
    [SerializeField] private Slider volume;

    public void Volume()
    {
        AudioListener.volume = volume.value;
    }
}
