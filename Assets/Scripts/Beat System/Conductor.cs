using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Conductor : MonoBehaviour
{
    public GameObject beatObject;

    public Transform spawnLocation;
    public Transform endLocation;
    public Transform targetLocation;
    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;

    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    //the number of beats in each loop
    public float beatsPerLoop;

    //the total number of loops completed since the looping clip first started
    public int completedLoops = 0;

    //The current position of the song within the loop in beats.
    public float loopPositionInBeats;
    public float loopPositionInAnalog;


    # region Singleton

    public static Conductor instance;

    void Awake(){
        instance = this;
    }

    # endregion

    public static class TimingThresholds{
        public static float perfect = 0.25f;
        public static float great = 0.35f;
        public static float good = 0.45f;

    }

    public static class TimingMultipliers{
        public static int perfect = 5;
        public static int great = 3;
        public static int good = 1;

    }

    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        musicSource.Play();

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;

        if (songPositionInBeats >= (completedLoops + 1) * beatsPerLoop)
            completedLoops++;
        loopPositionInBeats = songPositionInBeats - completedLoops * beatsPerLoop;

        loopPositionInAnalog = loopPositionInBeats / beatsPerLoop;
    }

    public void SpawnBeat(){
        Debug.Log("Beat Spawned");
        Instantiate(beatObject, spawnLocation);
    }

    public bool isBeatInRange(float threshold, float currentBeat, float nearestBeat){
        if (Math.Abs(currentBeat - nearestBeat) < threshold){
            return true;
        } else {
            return false;
        }
    }

    public int getBeatMultiplier(){
        float nearestBeat = (float) Math.Round(instance.songPositionInBeats);
        if (isBeatInRange(TimingThresholds.perfect, instance.songPositionInBeats, nearestBeat)){
            return TimingMultipliers.perfect;
        } else if (isBeatInRange(TimingThresholds.great, instance.songPositionInBeats, nearestBeat)){
            return TimingMultipliers.great;
        }  else if (isBeatInRange(TimingThresholds.good, instance.songPositionInBeats, nearestBeat)){
            return TimingMultipliers.good;
        } else {
            return 0;
        }
    }
}
