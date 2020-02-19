using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public float waitTime = 3f;
    public float spawnTime = 2f;

    private Hole hole;
    private Animator animator;
    private bool isActive = false;

    private float health = 1f;

    private void Start()
    {
        hole = FieldController.Instance.Field.GetRandomHole();
        hole.Status = Hole.HoleStatus.Mole;

        animator = GetComponentInChildren<Animator>();

        StartCoroutine(SetNewHole());

        PlayerInput.MoleHitted += NormalHit;
        PlayerInput.WaterPowerUp += WaterHit;
    }

    bool temp = false;
    private IEnumerator SetNewHole()
    {
        while (true)
        {
            switch (hole.Status)
            {
                case Hole.HoleStatus.None:
                    hole.Status = Hole.HoleStatus.NewHole;
                    break;
                case Hole.HoleStatus.Plank:
                    Debug.Log("Plank hole founded");
                    temp = true;
                    hole = FieldController.Instance.Field.GetRandomHole();
                    break;
                case Hole.HoleStatus.Water:
                    WaterHit();
                    break;
                default:
                    hole.Status = Hole.HoleStatus.Mole;
                    break;
            }

            if (temp )
            {
                Debug.Log("temp went through");
            }
            yield return new WaitForSeconds(spawnTime);
            isActive = true;

            FieldController.Instance.SwitchHoleGFX(hole);

            transform.position = hole.Position;

            animator.SetTrigger("Normal");

            yield return new WaitForSeconds(waitTime);

            hole.Status = Hole.HoleStatus.Empty;

            isActive = false;

            health = 1;

            transform.position = Vector3.one * -3;

            FieldController.Instance.SwitchHoleGFX(hole);

            hole = FieldController.Instance.Field.GetNewHole();

            if (hole == null)
                hole = FieldController.Instance.Field.GetRandomHole();
        }
    }

    private void NormalHit(Mole mole)
    {
        health--;

        if (mole == this && isActive == true && health <= 0)
        {
            hole.Status = Hole.HoleStatus.Empty;

            isActive = false;
            StopAllCoroutines();

            animator.SetTrigger("Hit");

            FieldController.Instance.SwitchHoleGFX(hole);

            hole = FieldController.Instance.Field.GetRandomHole();

            StartCoroutine(SetNewHole());
        }
    }

    private void WaterHit()
    {
        if (isActive == true)
        {
            hole.Status = Hole.HoleStatus.Water;

            isActive = false;
            StopAllCoroutines();

            animator.SetTrigger("Water");

            FieldController.Instance.SwitchHoleGFX(hole);

            hole = FieldController.Instance.Field.GetRandomHole();

            StartCoroutine(SetNewHole());

        }
    }
}
