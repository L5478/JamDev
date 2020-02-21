using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [Header("Camera will look at this position")]
    public Transform lookAt;

    [Header("Camera will move between these positions")]
    public Transform nearestPos;
    public Transform midPos;
    public Transform farestPos;

    public float zoomSpeed = .1f;

    private Vector3 targetPos = new Vector3();
    private float time = 0;

    private void Start()
    {
        transform.position = nearestPos.position;
        targetPos = nearestPos.position;
        Hole.HoleStatusChange += HoleStatusHasChanged;
    }

    private void Update()
    {
        time += Time.deltaTime;

        transform.LookAt(lookAt);

        transform.position = Vector3.Lerp(transform.position, targetPos, time * zoomSpeed);
    }

    private void HoleStatusHasChanged()
    {

        if (FieldController.Instance.Field.IsThereHolesInRow(0))
        {
            targetPos = farestPos.position;
            time = 0;
            return;
        }

        if (FieldController.Instance.Field.IsThereHolesInRow(1))
        {
            targetPos = midPos.position;
            time = 0;
            return;
        }

        targetPos = nearestPos.position;
        time = 0;
    }
}
