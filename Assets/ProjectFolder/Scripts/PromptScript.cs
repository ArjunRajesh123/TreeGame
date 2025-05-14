using JetBrains.Annotations;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class PromptScript : MonoBehaviour
{
    public GameObject player;
    private Vector3 distance;

    Transform mainCamera;
    Transform unit;
    Transform worldSpaceCanvas;

    public Vector3 offset;

    public GameObject canvas;
    public GameObject bunnyBone;

    public GameObject SpeakingText;

    public float TalkRange = 1.4f;

    SpeakingScript speakScript;
    void Start()
    {
        mainCamera = Camera.main.transform;
        unit = transform.parent;
        worldSpaceCanvas = canvas.transform;

        transform.SetParent(worldSpaceCanvas);

        speakScript = GetComponent<SpeakingScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bunnyBone)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
            transform.position = unit.position + offset;

            if (bunnyBone.transform.position != null)
            {
                distance = bunnyBone.transform.position - player.gameObject.transform.position;
            }


            if (distance.magnitude < TalkRange)
            {
                transform.gameObject.GetComponent<Image>().enabled = true;
                transform.gameObject.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
                RawImage[] components = transform.gameObject.GetComponentsInChildren<RawImage>();
                foreach (RawImage component in components)
                {
                    component.enabled = true;
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SpeakingText.SetActive(true);
                    speakScript = SpeakingText.GetComponentInChildren<SpeakingScript>();
                    if (speakScript)
                    {
                        speakScript.beginText(bunnyBone.transform.parent.gameObject.name);
                    }
                   

                }
            }
            else
            {
                transform.gameObject.GetComponent<Image>().enabled = false;
                transform.gameObject.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                RawImage[] components = transform.gameObject.GetComponentsInChildren<RawImage>();
                foreach (RawImage component in components)
                {
                    component.enabled = false;
                }
            }
        }else
        {
            transform.gameObject.SetActive(false);
        }
    }

}
