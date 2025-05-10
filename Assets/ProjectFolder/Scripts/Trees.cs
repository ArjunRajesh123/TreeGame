using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    public int Treehealth = 3;
    AttackingScript attack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Treehealth == 0)
        {
            StumpTree();
            Dead();
        }
    }

    public int Hurt(AttackingScript attackingScript)
    {
        Treehealth = Treehealth - 1;
        StartCoroutine(HurtWait(attackingScript));
        return Treehealth;
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
    public void StumpTree()
    {
        Transform stumpLocRotSiz = transform;
    }
    public void Dead()
    {
        attack.resetHurt(false);
        Destroy(gameObject);
    }
}
