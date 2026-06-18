using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineCamera cineCam;

    CinemachineFollow cineFollow;
    Vector3 targetFollowOffset;

    const float MIN_FOLLOW_OFFSET_Y = 2f;
    const float MAX_FOLLOW_OFFSET_Y = 12f;

    void Start()
    {
        cineFollow = cineCam.GetComponent<CinemachineFollow>();
        targetFollowOffset = cineFollow.FollowOffset;
    }

    void Update()
    {
        HandleCamMovement();
        HandleCamRotation();
        HandleCamZoom();
    }    

    private void HandleCamMovement()
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = +1f;
        }

        float camMoveSpeed = 10f;

        Vector3 camMoveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += camMoveVector * camMoveSpeed * Time.deltaTime;
    }

    private void HandleCamRotation()
    {
        Vector3 rotationVector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = +1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }

        float camRotateSpeed = 100f;
        transform.eulerAngles += rotationVector * camRotateSpeed * Time.deltaTime;
    }

    private void HandleCamZoom()
    {
        float camZoomAmount = 1f;

        if (Input.mouseScrollDelta.y > 0)
        {
            targetFollowOffset.y += camZoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFollowOffset.y -= camZoomAmount;
        }

        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_OFFSET_Y, MAX_FOLLOW_OFFSET_Y);

        float camZoomSpeed = 5f;
        cineFollow.FollowOffset = Vector3.Lerp(cineFollow.FollowOffset, targetFollowOffset, Time.deltaTime * camZoomSpeed);
    }
}
