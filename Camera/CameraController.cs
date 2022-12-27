using UnityEngine;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{
    [System.Serializable]
    public class CameraSettings
    {
        [Header("Camera Move Settings")]
        public float zoomSpeed = 5;
        public float moveSpeed = 5;
        public float rotationSpeed = 5;
        public float originalFieldofView = 70;
        public float zoomFieldofView = 30;
        public float MouseX_Sensitivity = 5;
        public float MouseY_Sensitivity = 5;
        public float MaxClampAngle = 90;
        public float MinClampAngle = -30;

        [Header("Camera Collision")]
        public Transform camPosition;
        public LayerMask camCollisionLayer;



    }
    [SerializeField]
    private CameraSettings cameraSettings;

    [System.Serializable]
    public class CamSettings
    {
        public string MouseXAxis = "Mouse X";
        public string MouseYAxis = "Mouse Y";
        public string AimingInput = "Fire2";
    }

    [SerializeField]
    private CamSettings inputSettings;
    private Transform center;
    private Transform target; //player
    private Camera mainCam;
    //private Camera UICam;

    private float cameraXrotation = 0;
    private float cameraYrotation = 0;
    private Vector3 InitialCamPos;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
       // UICam = mainCam.GetComponentInChildren<Camera>();
        center = transform.GetChild(0);
        InitialCamPos = mainCam.transform.localPosition;
        FindPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
            return;
        if (!Application.isPlaying)
            return;

        RotateCamera();
        ZoomCamera();
        HandleCameraCollision();
    }

    void LateUpdate()
    {
        if (target)
        {
            FollowPlayer();
        }
        else
        {
            FindPlayer();
        }


    }

    void FindPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FollowPlayer()
    {
        Vector3 moveVector = Vector3.Lerp(transform.position, target.position, cameraSettings.moveSpeed * Time.deltaTime);

        transform.position = moveVector;
    }
    private void RotateCamera()
    {
        cameraXrotation += Input.GetAxis(inputSettings.MouseYAxis) * cameraSettings.MouseY_Sensitivity * -1;
        cameraYrotation += Input.GetAxis(inputSettings.MouseXAxis) * cameraSettings.MouseX_Sensitivity;

        //Make sure the camera can rotate only between MinAngle and MaxAngle
        cameraXrotation = Mathf.Clamp(cameraXrotation, cameraSettings.MinClampAngle, cameraSettings.MaxClampAngle);
        cameraYrotation = Mathf.Repeat(cameraYrotation, 360);

        Vector3 rotatingAngle = new Vector3(cameraXrotation, cameraYrotation, 0);
        Quaternion rotation = Quaternion.Slerp(center.transform.localRotation, Quaternion.Euler(rotatingAngle), cameraSettings.rotationSpeed * Time.deltaTime);

        center.transform.localRotation = rotation;

    }
    void ZoomCamera()
    {
        if (Input.GetButton(inputSettings.AimingInput))
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, cameraSettings.zoomFieldofView, cameraSettings.zoomSpeed * Time.deltaTime);
            //UICam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, cameraSettings.zoomFieldofView, cameraSettings.zoomSpeed * Time.deltaTime);
        }
        else
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, cameraSettings.originalFieldofView, cameraSettings.zoomSpeed * Time.deltaTime);
           // UICam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, cameraSettings.originalFieldofView, cameraSettings.zoomSpeed * Time.deltaTime);
        }
    }

    void HandleCameraCollision()
    {
        if (!Application.isPlaying)
        {
            return;
        }
        if (Physics.Linecast(target.transform.position + target.transform.up, cameraSettings.camPosition.position, out hit, cameraSettings.camCollisionLayer))
        {
            Vector3 newCamPos = new Vector3(hit.point.x + hit.normal.x * .2f, hit.point.y + hit.normal.y * .8f, hit.point.z + hit.normal.z * .2f);
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, newCamPos, Time.deltaTime * cameraSettings.moveSpeed);
        }
        else
        {
            mainCam.transform.localPosition = Vector3.Lerp(mainCam.transform.localPosition, InitialCamPos, Time.deltaTime * cameraSettings.moveSpeed);
        }
        Debug.DrawLine(target.transform.position + target.transform.up, cameraSettings.camPosition.position, Color.blue);
    }
}
