using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;

    // Range restrict from what value to what value
    [SerializeField][Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField][Range(0f, 1f)] float damageVolume = 1f;

    // static variables persist through all instances of a class, great for Singletons
    static AudioPlayer instance;

    // public AudioPlayer GetInstance()
    // {
    //     return instance;
    // }

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        // FindObjectsOfType check if AudioPlayer exists in loaded scene, it checks in Awake before loading scene
        // GetType() returns AudioPlayer this class, can use without chevrons<>
        // we can use this in other scripts also, we can copy it and amend
        // so it will find all AudioPlayer objects 
        // it will keep track of how many instances exists of our AudioPlayer exits when scene is loaded
        //int instanceCount = FindObjectsOfType(GetType()).Length;

        // if is more than one instance of Audio player we will destroy it 
        //if (instanceCount > 1)
        //new way to create Singleton===============================================================
        if (instance != null)
        //=============================================
        {
            // it is possible that other scenes will try to use this AudioPlayer so we need to make it inactive good to use in awake it should not grab it
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            //new way to create Singleton===============================================================
            instance = this;
            //=============================================
            // this will mark out our AudioPlayer and do not destroy and pass to next scenes
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamageClip()
    {
        PlayClip(damageClip, damageVolume);
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPosition = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPosition, volume);
        }
    }

}
