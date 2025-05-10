using UnityEngine;

public class ReplayAnimation : MonoBehaviour
{
     private Animation anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.isPlaying)
        {
            return;
        }else
        {
            anim.Play();
        }
    }
}
