using System;
using UnityEngine;

public class DuckCatSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] posiblePointToSpawn;
    [SerializeField] private LevelChanger levelChanger;

    [SerializeField] private GameObject testDuckCat;
    [SerializeField] private DuckCat testduckCat;

    public void Spawn(Action openDuckCatCallback)
    {
        testDuckCat.transform.position = posiblePointToSpawn[UnityEngine.Random.Range(0, posiblePointToSpawn.Length)].position;
        if (UnityEngine.Random.Range(0, 101) > 85 && levelChanger.Level >= 5)
        {
            testDuckCat.SetActive(true);
            testduckCat.ChangeIsClicked();
            testduckCat.Initialize(openDuckCatCallback);
            testduckCat.OnOpened();
        }
        else
        {
            testduckCat.Initialize(openDuckCatCallback);
            testduckCat.DontOpened();
        }
    }
}
