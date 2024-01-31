using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")] 
    [SerializeField]
    private GameObject _chestPrefab;
    [SerializeField]
    private GameObject _chestWithKeyPrefab;

    [Header("Live Parameters")] public int currentLevel = 1;
    public AudioSource currentSoundtrack;


    //Singleton
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"There are more than ONE object of type {GetType()}");
        }

        Instance = this;
    }

    private void Start()
    {
        Play();
    }

    public void Play()
    {
        SpawnChests();
    }

    private void SpawnChests()
    {
        List<GameObject> spawnerList = GameObject.FindGameObjectsWithTag("Chest").ToList();
        List<GameObject> chestList = new();
        GameObject chestWithKey;
        foreach (GameObject obj in spawnerList)
        {
            GameObject chest = Instantiate(_chestPrefab, obj.transform, false);
            chestList.Add(chest);
        }
        int randomIndex = Random.Range(0, chestList.Count);
        Instantiate(_chestWithKeyPrefab, spawnerList[randomIndex].transform, false);
        Destroy(chestList[randomIndex]);
    }
}