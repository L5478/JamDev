using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public Transform lookAt;
    public Transform nearestPos;
    public Transform farestPos;

    public Transform farestTow;

    public float scrollSpeed = .1f;
    public float maxScrollSpeed = 1;
    public float minScrollSpeed = -1;
    public float zoomSpeed = .1f;

    private float time = 0;

    // THIS IS NOT WORKINIINGSDAAFDSAFe
    private void Update()
    {
        time += Time.deltaTime;

        float scrollWheelChange = -Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;

        scrollWheelChange = Mathf.Clamp(scrollWheelChange, minScrollSpeed, maxScrollSpeed);

        transform.LookAt(lookAt);

        Vector3 targetPos = Vector3.MoveTowards(transform.position, farestTow.position, scrollWheelChange);

        float farDistance = Vector3.Distance(targetPos, farestPos.position);
        float nearDistance = Vector3.Distance(targetPos, nearestPos.position);

        if (farDistance <= 5 || nearDistance <= 5)
        {
            return;
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, time * zoomSpeed);
    }


}
