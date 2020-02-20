using UnityEngine;

public class MenuInput : MonoBehaviour
{
    public GameObject Hammer;
    private Animator HammerAnim;

    private void Start()
    {
        HammerAnim = Hammer.GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                MenuMole mole = hit.transform.GetComponentInParent<MenuMole>();
                if (mole != null)
                {
                    HammerAnim.SetTrigger("SLAP");
                    StartCoroutine(mole.GetHit());
                }
            }
        }
    }
}
