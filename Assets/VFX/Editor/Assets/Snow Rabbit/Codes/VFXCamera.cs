using UnityEngine;

public class VFXCamera : MonoBehaviour
{
    public Transform target;
    public float cameraWidth = 17.8f;
    public float cameraHeight = 10f;
    public float offsetY = 0f;
    public float fixedY = 0f;  // Y축 고정 값

    Vector3 currentRoom;

    void Start()
    {
        currentRoom = GetRoomPosition(target.position);
        transform.position = currentRoom;
    }

    void Update()
    {
        Vector3 targetRoom = GetRoomPosition(target.position);

        if (targetRoom != currentRoom)
        {
            currentRoom = targetRoom;
            transform.position = currentRoom;
        }
    }

    Vector3 GetRoomPosition(Vector3 playerPos)
    {
        float x = Mathf.Floor(playerPos.x / cameraWidth) * cameraWidth + cameraWidth / 2f;
        return new Vector3(x, fixedY, transform.position.z);  // Y는 fixedY로 고정
    }
}