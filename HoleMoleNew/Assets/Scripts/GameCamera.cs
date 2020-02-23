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

        transform.LookAt(lookAt);

        transform.position = Vector3.Lerp(transform.position, targetPos, time * zoomSpeed);
    }

    private void HoleStatusHasChanged()
    {
        if (nearestPos == null)
            nearestPos = GameObject.FindGameObjectWithTag("NearCam").transform;
        if (midPos == null)
            midPos = GameObject.FindGameObjectWithTag("MidCam").transform;
        if (farestPos == null)
            farestPos = GameObject.FindGameObjectWithTag("FarCam").transform;

        if (FieldController.Instance.Field.IsThereHolesInColum(0) && !gameEnded || FieldController.Instance.Field.IsThereHolesInColum(FieldController.Instance.Field.Width-1) && !gameEnded ||
            FieldController.Instance.Field.IsThereHolesInRow(0) && !gameEnded || FieldController.Instance.Field.IsThereHolesInRow(FieldController.Instance.Field.Depth - 1) && !gameEnded)
        {
            PowerUpSelector.instance.ShowEndScreen();
            gameEnded = true;
            return;
        }

        if (FieldController.Instance.Field.IsThereHolesInRow(1))
        {
            targetPos = farestPos.position;
            time = 0;
            return;
        }

        if (FieldController.Instance.Field.IsThereHolesInRow(2) || FieldController.Instance.Field.IsThereHolesInRow(FieldController.Instance.Field.Depth - 2))
        {
            targetPos = midPos.position;
            time = 0;
            return;
        }

        targetPos = nearestPos.position;
        time = 0;
    }
}
