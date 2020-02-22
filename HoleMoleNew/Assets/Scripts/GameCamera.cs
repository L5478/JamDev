using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [Header("Camera will look at this position")]
    public Transform lookAt;

    [Header("Camera will move between these positions")]
    public Transform nearestPos;
    public Transform midPos;
    public Transform midFarPos;
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

        if (FieldController.Instance.Field.IsThereHolesInColum(0) || FieldController.Instance.Field.IsThereHolesInColum(14))
        {
            PowerUpSelector.instance.ShowEndScreen();
            return;
        }

        if (FieldController.Instance.Field.IsThereHolesInColum(1) || FieldController.Instance.Field.IsThereHolesInColum(13))
        {
            targetPos = farestPos.position;
            time = 0;
            return;
        }

        if (FieldController.Instance.Field.IsThereHolesInRow(0))
        {
            targetPos = midFarPos.position;
            time = 0;
            return;
        }

        if (FieldController.Instance.Field.IsThereHolesInRow(1) || FieldController.Instance.Field.IsThereHolesInRow(5))
        {
            targetPos = midPos.position;
            time = 0;
            return;
        }

        targetPos = nearestPos.position;
        time = 0;
    }
}
