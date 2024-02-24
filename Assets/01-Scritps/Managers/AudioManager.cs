using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public static AudioSource AudioSource;
    public static float MusicVolume = 0.3f;
    public static float AmbientVolume = 0.6f;
    public static float StepsVolume = 0.4f;
    public static List<GameObject> MusicInstances;
    public static List<GameObject> AmbientInstances;
    public static List<GameObject> StepsInstances;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        AudioSource = gameObject.GetComponent<AudioSource>();
    }

    public static void LoadAudioSources()
    {
        MusicInstances = GameObject.FindGameObjectsWithTag("Music").ToList();
        AmbientInstances = GameObject.FindGameObjectsWithTag("Ambient").ToList();
        StepsInstances = GameObject.FindGameObjectsWithTag("Steps").ToList();
    }
    
    public static void UpdateVolume()
    {
        LoadAudioSources();
        foreach (GameObject go in AmbientInstances)
        {
            go.GetComponent<AudioSource>().volume = AmbientVolume;
        }
        foreach (GameObject go in MusicInstances)
        {
            go.GetComponent<AudioSource>().volume = MusicVolume;
        }
        foreach (GameObject go in StepsInstances)
        {
            go.GetComponent<AudioSource>().volume = StepsVolume;
        }
    }
}
