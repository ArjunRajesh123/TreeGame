using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeakingScript : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI Text;
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] float WaitTime = 2.5f;
    [SerializeField] bool isPlaying;
    [SerializeField] bool called;

    String character;
    void Update()
    {
        if(character != null)
        {
           MovePage(character);

        }
    }
   public void beginText(String character)
    {
        called = true;
        if (!isPlaying)
        {
            StartCoroutine(ClosePage());
        }
        this.character = character;
        Name.text = character;
        if (character.Equals("Tofu"))
        {
            Text.text = "Stop touching me";
        }
        if (character.Equals("Riby"))
        {
            Text.text = "Hi";
        }
        if (character.Equals("Lil Bunny"))
        {
            Text.text = "What's up with the axe";
        }
        if (character.Equals("Nibble"))
        {
            Text.text = "Wanna pet me";
        }
        if (character.Equals("Carrot Jr"))
        {
            Text.text = "Feed Me";
        }

    }
   public void MovePage(String TalkCharacter)
    {
 
    }

    IEnumerator ClosePage()
    {
        called = false;
        isPlaying = true;
        yield return new WaitForSeconds(WaitTime);
        isPlaying = false;
        if (called)
        {
            called = false;
            Text.gameObject.transform.parent.gameObject.SetActive(true);
            StartCoroutine(ClosePage());

        } else
        {
            Text.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
   
