using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public Transform lookAt;
    public Transform nearestPos;
    public Transform farestPos;

    

    private void Update()
    {
        float scrollWheelChange = Input.GetAxis("Mouse ScrollWheel");


        if (scrollWheelChange > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, farestPos.position, scrollWheelChange);
        }
        else if (scrollWheelChange < 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, nearestPos.position, scrollWheelChange);
        }

        transform.LookAt(lookAt);



    }


}
