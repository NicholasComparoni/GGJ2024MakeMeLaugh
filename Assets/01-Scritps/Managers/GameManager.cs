using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(SceneManager.GetActiveScene().name != "Menu")
            Play();
    }

    public void Play()
    {
        SpawnChests();
        SpawnCharacters();
    }

    private void SpawnChests()
    {
        List<AudioClip> soundList = Resources.LoadAll<AudioClip>("ObjSounds").ToList();
        List<GameObject> spawnerList = GameObject.FindGameObjectsWithTag("Chest").ToList();
        List<GameObject> chestList = new();
        foreach (GameObject obj in spawnerList)
        {
            GameObject chest = Instantiate(_chestPrefab, obj.transform, false);
            chest.GetComponent<Chest>()._chestSound = soundList[Random.Range(0, soundList.Count)];
            chestList.Add(chest);
        }

        int randomIndex = Random.Range(0, chestList.Count);
        Instantiate(_chestWithKeyPrefab, spawnerList[randomIndex].transform, false);
        Destroy(chestList[randomIndex]);
    }

    private void SpawnCharacters()
    {
        List<GameObject> spawnerList = GameObject.FindGameObjectsWithTag("Character").ToList();
        List<GameObject> characterList = Resources.LoadAll<GameObject>("CharProps/DynamicCharaters").ToList();
        List<int> loadedCharactersIndexes = new();

        for (int i = 0; i < spawnerList.Count; i++)
        {
            int charIndex;
            bool isFree;
            do
            {
                isFree = true;
                charIndex = Random.Range(0, characterList.Count);
                foreach (int index in loadedCharactersIndexes)
                {
                    isFree &= index != charIndex;
                    if (!isFree)
                    {
                        break;
                    }
                }
                //Il ciclo continua finchè non viene selezionato un numero non ancora comparso
            } while (!isFree);
            loadedCharactersIndexes.Add(charIndex);
        }
        for (int i = 0; i < spawnerList.Count; i++)
        {
            GameObject chest = Instantiate(characterList[loadedCharactersIndexes[i]], spawnerList[i].transform, false);
        }
    }
}