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

    public Vector3 offset = new Vector3(-2, 2, -2);

    private Vector3 targetPos = new Vector3();
    private float time = 0;

    private bool gameEnded = false;

    private void Start()
    {
        transform.position = nearestPos.position;
        targetPos = nearestPos.position;

        Hole.HoleStatusChange += HoleStatusHasChanged;
    }

    private void Update()
    {
        time += Time.deltaTime;

        Quaternion targetRot = Quaternion.LookRotation(lookAt.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, time * zoomSpeed);

        transform.position = Vector3.Lerp(transform.position, targetPos, time * zoomSpeed);

        if (gameEnded && Vector3.Distance(transform.position, targetPos) <= 1)
        {
            PowerUpSelector.Instance.ShowLostScreen();
        }
    }

    private void OnDestroy()
    {
        Hole.HoleStatusChange -= HoleStatusHasChanged;
    }

    private void HoleStatusHasChanged()
    {
        if (gameEnded)
            return;

        if (EndGame())
            return;

        if (FieldController.Instance.Field.IsThereHolesInRow(1) != null)
        {
            targetPos = farestPos.position;
            time = 0;
            return;
        }

        if (FieldController.Instance.Field.IsThereHolesInRow(2) != null ||
            FieldController.Instance.Field.IsThereHolesInRow(FieldController.Instance.Field.Depth - 2) != null)
        {
            targetPos = midPos.position;
            time = 0;
            return;
        }

        targetPos = nearestPos.position;
        time = 0;
    }

    private bool EndGame()
    {
        Hole hole = FieldController.Instance.Field.IsThereHolesInColum(0);

        if (hole == null)
            hole = FieldController.Instance.Field.IsThereHolesInColum(FieldController.Instance.Field.Width - 1);

        if (hole == null)
            hole = FieldController.Instance.Field.IsThereHolesInRow(0);

        if (hole == null)
            hole = FieldController.Instance.Field.IsThereHolesInRow(FieldController.Instance.Field.Depth - 1);

        if (hole == null)
            return false;

        targetPos = hole.Position + offset;
        lookAt.position = hole.Position + Vector3.up;
        gameEnded = true;
        time = 0;
        return true;
    }
}
