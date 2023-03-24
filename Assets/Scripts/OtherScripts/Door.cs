using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip audioScrip;
    public AudioClip audioLongScrip;

    public string Scene;

    public SceneLoader.Position position = SceneLoader.Position.Custom;
    public Vector3 locationOnNextScene = Vector3.zero;
    public CheckpointManager.Checkpoint checkpoint = CheckpointManager.Checkpoint.None;

    public bool open = false;

    public string text = "Íאזלטעו [E], קעמבû חאיעט ג המל";
    private TextMeshProUGUI hint;
    private bool triggered;

    private void Awake()
    {
        hint = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGameObjectHolder>().gameObjects[0].GetComponent<TextMeshProUGUI>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && open)
        {
            CheckpointManager.SaveCheckpoint(checkpoint);
            if (position == SceneLoader.Position.Custom)
                SceneLoader.LoadScene(Scene, locationOnNextScene);
            else
                SceneLoader.LoadScene(Scene, position);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hint.text = "";
            triggered = false;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hint.text = text;
            triggered = true;
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && triggered)
        {
            if (checkpoint == CheckpointManager.Checkpoint.VillageToStartLocation ||
                checkpoint == CheckpointManager.Checkpoint.StartLocationToVillage) audioSource.PlayOneShot(audioScrip);
            else if(checkpoint == CheckpointManager.Checkpoint.VillageToBarn) audioSource.PlayOneShot(audioLongScrip);
            open = true;
        }
    }
}
