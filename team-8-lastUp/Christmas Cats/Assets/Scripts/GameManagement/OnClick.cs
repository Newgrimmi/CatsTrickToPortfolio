using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClick : MonoBehaviour
{
    [SerializeField] private GameObject setMenu;
    [SerializeField] private SimpleCatFactory simpleCatFactory;
    [SerializeField] private AutoCatFactory autoCatFactory;
    [SerializeField] private AudioDataCollection currentAudio;
    [SerializeField] private Tutorial tutorialScript;
    [SerializeField] private DestroyOnTriggerEnter destroyCat;
    [SerializeField] private List<Transform> _allCats;

    private PoolCats poolCatSimple;
    private PoolCats poolCatAutoSpawned;
    private bool firstTap;

    public bool CanSpawn { get; private set; } = true;
    
    readonly int poolSimpleCatCount = 80;
    readonly int poolCatAutoSpawnedCount = 6;

    private void Start()
    {
        this.poolCatSimple = new PoolCats(poolSimpleCatCount, simpleCatFactory);
        this.poolCatAutoSpawned = new PoolCats(poolCatAutoSpawnedCount, autoCatFactory);
    }

    private void OnEnable()
    {
        destroyCat.CatDied += RemoveList;
    }

    private void OnDisable()
    {
        destroyCat.CatDied -= RemoveList;
    }

    /// <summary>
    /// Создаем котика
    /// </summary>
    public void SpawnCat()
    {
        if (CanSpawn == true && !setMenu.activeInHierarchy)
        {
            if (firstTap == false)
            {
                tutorialScript.HideFingerSprite(0);
                firstTap = true;
            }

            currentAudio.PlayAudio(AudioDataCollection.AudioType.SpawnCat);

            _allCats.Add(poolCatSimple.GetFreeElement(simpleCatFactory));
        }
    }

    /// <summary>
    /// Создаем второй тип котика
    /// </summary>
    public void AutoSpawnCat()
    {
        if (CanSpawn == true)
        {
            currentAudio.PlayAudio(AudioDataCollection.AudioType.SpawnCat);
            _allCats.Add(poolCatAutoSpawned.GetFreeElement(autoCatFactory));
        }
    }

    public void CanSpawnCats()
    {
        CanSpawn = true;
    }

    public void CantSpawnCats()
    {
        CanSpawn = false;
    }

    public IEnumerator AutoSpawnCatForTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            AutoSpawnCat();         
        }
    }

    public void KillAllCats()
    {
        foreach (var cat in _allCats)
        {
            cat.gameObject.SetActive(false);
        }
        _allCats.Clear();
    }

    private void RemoveList(Transform curCat)
    {
        _allCats.Remove(curCat);
    }
} 
