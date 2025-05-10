using UnityEngine;
using System.Collections;
public class Animals : MonoBehaviour
{
    public int animalHealth = 5;
    AttackingScript attack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(animalHealth == 0)
        {
            Dead();
        }
    }

    public int Hurt(AttackingScript attackingScript)
    {
        animalHealth = animalHealth - 1;
        StartCoroutine(HurtWait(attackingScript));
        return animalHealth;
    }
    IEnumerator HurtWait(AttackingScript attackingScript)
    {
        attack = attackingScript;
        yield return new WaitForSeconds(0.3f);
        if (attackingScript)
        {
            attackingScript.resetHurt(false);
        }
    }
    public void Dead()
    {
        if (gameObject.transform.parent != null)
        {
            if (gameObject.transform.parent.gameObject.tag.Equals("Animal"))
            {
                attack.resetHurt(false);
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
        else
        {
            attack.resetHurt(false);
            Destroy(gameObject);
        }
    }


}
