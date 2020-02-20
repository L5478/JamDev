using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMole : MonoBehaviour
{
    public float health;

    public GameObject normalMole;
    public GameObject eliteMole;

    public GameObject normalDamageEffect;
    public GameObject eliteDamageEffect;
    public GameObject helmetEffect;
    public GameObject helmet;

    private GameObject currentMole;
    private GameObject currentDamageEffect;

    private void Start()
    {
        currentMole = normalMole;
        currentMole.SetActive(true);
        health = currentMole == normalMole ? 1f : 2f;
        currentDamageEffect = currentMole == normalMole ? normalDamageEffect : eliteDamageEffect;
    }

    public IEnumerator GetHit()
    {
        health--;

        if (currentMole == eliteMole && helmet.activeInHierarchy)
        {
            helmetEffect.SetActive(true);
            helmet.SetActive(false);
        }

        if (health <= 0)
        {
            currentDamageEffect.SetActive(true);
            currentMole.GetComponentInChildren<Animator>().SetTrigger("Hit");
        } 

        yield return new WaitForSeconds(.5f);

        if (health <= 0)
        {
            currentMole.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(2);

        if (health <= 0)
        {
            currentMole = currentMole == normalMole ? eliteMole : normalMole;
            health = currentMole == normalMole ? 1f : 2f;
            currentDamageEffect = currentMole == normalMole ? normalDamageEffect : eliteDamageEffect;
            currentDamageEffect.SetActive(false);
            currentMole.SetActive(true);
            if (helmet.activeInHierarchy == false)
            {
                helmet.SetActive(true);
            }
        }
    }
}
