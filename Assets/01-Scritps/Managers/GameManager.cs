using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")] 
    [SerializeField] private GameObject _chestPrefab;
    [SerializeField] private GameObject _chestWithKeyPrefab;

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
        SpawnCharacters();
    }

    private void SpawnChests()
    {
        List<GameObject> spawnerList = GameObject.FindGameObjectsWithTag("Chest").ToList();
        List<GameObject> chestList = new();
        foreach (GameObject obj in spawnerList)
        {
            GameObject chest = Instantiate(_chestPrefab, obj.transform, false);
            chestList.Add(chest);
        }

        int randomIndex = Random.Range(0, chestList.Count);
        Instantiate(_chestWithKeyPrefab, spawnerList[randomIndex].transform, false);
        Destroy(chestList[randomIndex]);
    }

    private void SpawnCharacters()
    {
        List<GameObject> spawnerList = GameObject.FindGameObjectsWithTag("Character").ToList();
        List<GameObject> characterList = new();
        List<int> loadedCharacters = new();
        string[] pathList = Directory.GetFiles("Assets/03-Prefabs/CharProps/LVL1/", "*.prefab", SearchOption.TopDirectoryOnly);
        foreach (string path in pathList)
        {
            characterList.Add((GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)));
        }

        for (int i = 0; i < spawnerList.Count; i++)
        {
            int charIndex;
            bool isFree;
            do
            {
                isFree = true;
                charIndex = Random.Range(0, characterList.Count);
                for (int j = 0; j < loadedCharacters.Count; j++)
                {
                    if (charIndex == loadedCharacters[j])
                    {
                        isFree = false;
                        break;
                    }
                }
            } while (!isFree);
            loadedCharacters.Add(charIndex);
        }

        foreach (int index in loadedCharacters)
        {
            Debug.Log(index);
        }
    }
}