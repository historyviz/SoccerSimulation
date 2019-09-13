using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceController : MonoBehaviour
{
    [Header("Assign These!")]
    [Tooltip("Assign me from the scene!")]
    public SoccerPlayerController soccerPlayerController;

    [Tooltip("Assign me from the scene!")]
    public Transform slideTackleStartLocation;

    [Tooltip("Assign me from the scene!")]
    public Transform opponenetStartLocation;

    [Tooltip("Assign me from the scene!")]
    public AudioSource sceneAudioSource;

    // TODO: Want to remove eventually
    [Tooltip("Assign me from the scene!")]
    public Button startButton;

    [Header("Sound")]
    public AudioClip startSound;
    public AudioClip ambientTrack;
    public bool mute = false;

    private bool evaluateSoccerPlayerLocation = false;
    private float slideTackleDistanceThreshoold = 0.5f;

    void Start()
    {
        // Set up audio stuff
        sceneAudioSource.loop = true;
        sceneAudioSource.clip = ambientTrack;
        if (!mute) sceneAudioSource.Play();

        // Set up button events
        // TODO: remove eventually
        startButton.onClick.AddListener(() => { startTackleExperience(); startButton.interactable = false; });
    }
    void Update()
    {
        if (evaluateSoccerPlayerLocation) evaluateSoccerPlayerLocationLoop();
    }

    [ContextMenu("startTackleExperience")]
    public void startTackleExperience()
    {
        soccerPlayerController.IsRunning = true;
        soccerPlayerController.Target = slideTackleStartLocation.position;
        evaluateSoccerPlayerLocation = true;
        if (!mute) sceneAudioSource.PlayOneShot(startSound);
    }

    private void evaluateSoccerPlayerLocationLoop()
    {
        if (Vector3.Distance(soccerPlayerController.gameObject.transform.position, slideTackleStartLocation.position) < slideTackleDistanceThreshoold)
        {
            soccerPlayerController.StartTackle();
            evaluateSoccerPlayerLocation = false;
            // Wait unitl tackle is over, then return to start
            // TODO: Come up with a better system than chain invokes lol
            Invoke("returnToStart", 2);
        }
    }

    // TODO: Come up with a better system than chain invokes lol (remove)
    private void returnToStart()
    {
        soccerPlayerController.Target = opponenetStartLocation.position;
        soccerPlayerController.IsRunning = true;
        Invoke("facePlayer", 2);
    }

    // TODO: Come up with a better system than chain invokes lol (remove)
    private void facePlayer()
    {
        soccerPlayerController.IsRunning = false;
        soccerPlayerController.FaceDirection(opponenetStartLocation.forward);

        // ugh, I have to put this here :(
        // TODO: remove
        startButton.interactable = true;
    }
}
