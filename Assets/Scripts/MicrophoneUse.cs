using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;

public class VoiceMovement : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    private Animator anim;
    private Rigidbody2D body;
    public Transform firePosition;
    public GameObject projectile;

    void Start()
    {
        actions.Add("shoot", Shoot);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();

        if (anim == null) Debug.LogError("Animator component is missing!");
        if (body == null) Debug.LogError("Rigidbody2D component is missing!");
    }

   

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log($"Recognized: {speech.text}");
        if (actions.ContainsKey(speech.text))
        {
            actions[speech.text].Invoke();
        }
        else
        {
            Debug.LogWarning($"Unrecognized command: {speech.text}");
        }
    }

    private void Shoot()
    {
        anim.SetBool("shoot", true);
        Debug.Log("Shoot command recognized.");
        Instantiate(projectile, firePosition.position, firePosition.rotation);
        StartCoroutine(ResetShootParameter());
    }

    private IEnumerator ResetShootParameter()
    {
        yield return new WaitForSeconds(0.1f); // Adjust the delay to match the length of the shooting animation
        anim.SetBool("shoot", false);
    }
}







