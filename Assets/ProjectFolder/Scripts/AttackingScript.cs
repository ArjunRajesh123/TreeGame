using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.Physics;
public class AttackingScript : MonoBehaviour
{

    public float raycastDistance = 5f;
    public Animation anim;
    public GameObject animObjects;
    public float attackCooldown = 3f;
    public float attackAnimationWaitSec = 0.5f;
    public bool canAttack = true;
    public GameObject axeHitParticle;
    public GameObject animalHitParticle;
    private float fade;
    public bool IsAnimationPlaying = false;
    public GameObject axe;
    public bool isIdleAnimationActive = false;
    public bool isAttacking = false;
    private bool isMoving = false;
    private bool doingPartical = false;
    UnityEngine.RaycastHit raycast;
    private bool checkedRay = false;
    public bool hurtDone = false;
    public AttackingScript me;
    private float originalWaitTime;

    void Start()
    {
        Time.timeScale = 1.09f;
        me = GetComponent<AttackingScript>();
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        UnityEngine.Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if(isMoving == true)
        {
            axe.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (canAttack && !IsAnimationPlaying && !isAttacking)
            {
                PlayAnim();
                IsAnimationPlaying = true;
                checkedRay = true;
                if (Physics.Raycast(ray, out raycast, raycastDistance))
                {
                    canAttack = false;
                    StartCoroutine(attackCooldownWait());
                    if (raycast.transform.gameObject.tag == "Tree")
                    {
                        Particals(raycast);
                        if (raycast.transform.gameObject.GetComponent<Trees>())
                        {
                            StartCoroutine(attackAnimationWaitTree(raycast));
                        }
                    }
                    if (raycast.transform.gameObject.tag == "BigTree")
                    {
                        Particals(raycast);
                        if (raycast.transform.gameObject.GetComponent<Trees>())
                        {
                            StartCoroutine(attackAnimationWaitTree(raycast));
                        }
                    }
                    if (raycast.transform.gameObject.tag == "Animal")
                    {
                        Particals(raycast);
                        if (raycast.transform.gameObject.GetComponent<Animals>())
                        {
                            StartCoroutine(attackAnimationWaitAnimal(raycast));
                        }
                    }
                }
            }

        }
    }
    public void moveAnim()
    {
        isIdleAnimationActive = false;
        isMoving = true;
        if (IsAnimationPlaying == false)
        {
            axe.SetActive(false);
            Animation[] components = animObjects.transform.GetComponentsInChildren<Animation>();
            foreach (Animation animation in components)
            {
                string[] clipNames = { "AxeRun", "HandRun", "rightHandRun" };
                if (animation.gameObject.name == "HandAnimationsAxe")
                {
                    animation.transform.gameObject.SetActive(false);
                }
                if (animation.gameObject.name == "HandAnimationsLeft")
                {
                    animation.CrossFade(clipNames[1], fade);
                }
                if (animation.gameObject.name == "HandAnimationsRight")
                {
                    animation.CrossFade(clipNames[2], fade);
                }
            }
        }
    }
    public void PlayAnim()
    {

        isAttacking = true;
        isIdleAnimationActive = false;
        isMoving = false;
        axe.SetActive(true);
        Animation[] components = animObjects.transform.GetComponentsInChildren<Animation>();

            foreach (Animation animation in components)
            {
                string[] clipNames = { "AxeAction.001", "ArmActionNoWeapon", "ArmAction" };
                if (animation.gameObject.name == "HandAnimationsLeft")
                {
                    animation.CrossFade(clipNames[1], fade);
                }
            if (animation.gameObject.name == "HandAnimationsRight")
                {
                    animation.CrossFade(clipNames[2], fade);
                }
            if (animation.gameObject.name == "HandAnimationsAxe")
            {
                animation.CrossFade(clipNames[0], fade);
            }
            StartCoroutine(CheckForAttackAnimationDone(animation, "none"));
            }
    }

    IEnumerator CheckForAttackAnimationDone(Animation anim, String item)
    {
        if (isAttacking == true)
        {
            StartCoroutine(checkRay());
        }
        yield return new WaitForSeconds(0f);
        if (anim.isPlaying == true)
        {
            StartCoroutine(CheckForAttackAnimationDone(anim, item));
        }
        else if(anim.isPlaying == false)
        {  
            if (item.Equals("Axe"))
            {
                axe.SetActive(true);
                IsAnimationPlaying = false;
            }
            else
            {
                isAttacking = false;
                IdleAnim();
            }
        }
    }

    IEnumerator checkRay()
    {
        Vector2 mousePos = Input.mousePosition;
        UnityEngine.Ray newRay = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(newRay, out raycast, raycastDistance))
        {
            if (raycast.transform != null)
            {
                if (raycast.transform.gameObject.tag == "Tree")
                {
                    if (checkedRay)
                    {
                        Particals(raycast);
                        if (raycast.transform.gameObject.GetComponent<Trees>())
                        {
                            checkedRay = false;
                            attackAnimationWaitSec = attackAnimationWaitSec / 1.05f;
                            originalWaitTime = attackAnimationWaitSec;
                            StartCoroutine(attackAnimationWaitTree(raycast));

                        }
                    }
                }
                if (raycast.transform.gameObject.tag == "BigTree")
                {
                    if (checkedRay)
                    {
                        Particals(raycast);
                        if (raycast.transform.gameObject.GetComponent<Trees>())
                        {
                            checkedRay = false;
                            attackAnimationWaitSec = attackAnimationWaitSec / 1.05f;
                            originalWaitTime = attackAnimationWaitSec;
                            StartCoroutine(attackAnimationWaitTree(raycast));
                        }
                    }
                }
                if (raycast.transform.gameObject.tag == "Animal")
                {
                    if (checkedRay)
                    {
                        Particals(raycast);
                        if (raycast.transform.gameObject.GetComponent<Animals>())
                        {
                            checkedRay = false;
                            attackAnimationWaitSec = attackAnimationWaitSec / 1.05f;
                            originalWaitTime = attackAnimationWaitSec;
                            StartCoroutine(attackAnimationWaitAnimal(raycast));

                        }
                    }
                }
            }
        }
        yield break;
    }

    public void IdleAnim()
    {
        if (isIdleAnimationActive == false && !isAttacking)
        {
            isMoving = false;
            isIdleAnimationActive = true;
            Animation[] components = animObjects.transform.GetComponentsInChildren<Animation>();
            foreach (Animation animation in components)
            {
                string[] clipNames = { "AxeDown", "HandDown", "RightHandDown" };
                if (animation.gameObject.name == "HandAnimationsAxe")
                {
                    animation.CrossFade(clipNames[0], fade);
                }
                if (animation.gameObject.name == "HandAnimationsLeft")
                {
                    animation.CrossFade(clipNames[1], fade);
                }
                if (animation.gameObject.name == "HandAnimationsRight")
                {
                    animation.CrossFade(clipNames[2], fade);
                }
                StartCoroutine(CheckForAttackAnimationDone(animation, "Axe"));
            }
        }
    }

    public void Particals(UnityEngine.RaycastHit raycast)
    {
        if (!doingPartical)
        {
            doingPartical = true;
            GameObject vfx;
            if (raycast.transform.gameObject.tag.Equals("Animal"))
            {
                ParticleSystem ps = animalHitParticle.GetComponent<ParticleSystem>();
                ParticleSystem.MainModule main = ps.main;

                vfx = Instantiate(animalHitParticle, raycast.point, Quaternion.identity);
                Destroy(vfx, 2f);
            }
            else
            {
                ParticleSystem ps = axeHitParticle.GetComponent<ParticleSystem>();
                ParticleSystem.MainModule main = ps.main;

                var mat = raycast.transform.GetComponent<Renderer>().material;
                if (mat.GetColor("_BarkColor") != null)
                {
                    main.startColor = new Color(mat.GetColor("_BarkColor").r, mat.GetColor("_BarkColor").g, mat.GetColor("_BarkColor").b, 1f);
                    vfx = Instantiate(axeHitParticle, raycast.point, Quaternion.identity);
                    Destroy(vfx, 1f);
                }
            }
            doingPartical = false;
        }
    }

   public void resetHurt(bool hurtDone)
    {
        this.hurtDone = hurtDone;
         attackAnimationWaitSec = 0.67f;
    }
    IEnumerator attackCooldownWait()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    IEnumerator attackAnimationWaitTree(UnityEngine.RaycastHit raycast)
    {
        Trees Trees = raycast.transform.gameObject.GetComponent<Trees>();
        yield return new WaitForSeconds(attackAnimationWaitSec);
        if (Trees && hurtDone == false)
        {
            hurtDone = true;
            Trees.Hurt(me);
        }
    }
    IEnumerator attackAnimationWaitAnimal(UnityEngine.RaycastHit raycast)
    {
        Animals AnimalScript = raycast.transform.gameObject.GetComponent<Animals>();
        yield return new WaitForSeconds(attackAnimationWaitSec);
        if (AnimalScript && hurtDone == false)
        {
            hurtDone = true;
            AnimalScript.Hurt(me);
        }
    }

}
