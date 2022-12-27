using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Animator))]
public class InputSystem : MonoBehaviour
{
    private Movement movement;

    [System.Serializable]
    public class InputSettings
    {
        public string forwardInput = "Vertical";
        public string strafeInput = "Horizontal";
        public string sprintInput = "Sprint";
        public string aim = "Fire2";
        public string fire = "Fire1";
        public string alt = "Alt";
    }

    [SerializeField]
    private InputSettings input;

    [Header("Camera and Character Syncing")]
    [SerializeField]
    private float lookDistance = 5;
    private float lookSpeed = 5;

    [Header("Aiming Settings")]
    private RaycastHit hit;
    [SerializeField]
    private float maxDistance = 300f; //Max aiming distance - khoang cach ngam toi da
    [SerializeField]
    private LayerMask aimLayer;
    private Ray ray;

    [Header("Spine Settings")]
    [SerializeField]
    private Transform spine;
    [SerializeField]
    private Vector3 spineOffset;

    [Header("Head Rotation Settings")]
    [SerializeField]
    private float lookAtPoint = 4f;


    private Transform camCenter;
    private Transform mainCam;
    private bool isAiming;

    [SerializeField]
    private Bow bow;

    public bool testAim;

    private bool hitDetected;


    private Animator playerAnim;
    [SerializeField]
    private VFXController vfxCc;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Movement>();
        camCenter = Camera.main.transform.parent;
        mainCam = Camera.main.transform;
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetAxis(input.forwardInput) != 0 && !Input.GetButton(input.alt)) || (Input.GetAxis(input.strafeInput) != 0 && !Input.GetButton(input.alt)))
        {
            RotateToCamView();
        }
        //movement
        movement.AnimatorCharacter(Input.GetAxis(input.forwardInput), Input.GetAxis(input.strafeInput));
        movement.SprintCharacter(Input.GetButton(input.sprintInput));
        //aiming
        isAiming = Input.GetButton(input.aim);
        if (testAim)
            isAiming = true;

        movement.CharacterAim(isAiming);

        if (isAiming)
        {
            Aim();
            bow.EquipBow();
            movement.CharacterPullString(Input.GetButton(input.fire));
            if (Input.GetButtonUp(input.fire))
            {
                movement.CharacterFireArrow();
                if (hitDetected)
                {
                    bow.Fire(hit.point);
                }
                else
                {
                    bow.Fire(ray.GetPoint(maxDistance));
                }
            }
        }
        else
        {
            bow.UnEquipBow();
            bow.RemoveCrosshair();
            DisableArrow();
            Release();

        }
      
    }

    void LateUpdate()
    {
        if (isAiming)
        {
            RotateCharacterSpine();
        }
    }

    void RotateToCamView()
    {
        Vector3 camCenterPos = camCenter.position;

        Vector3 lookPoint = camCenterPos + (camCenter.forward * lookDistance);
        Vector3 direction = lookPoint - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0;
        lookRotation.z = 0;

        Quaternion finalRotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * lookSpeed);
        transform.rotation = finalRotation;
    }

    //Does the aiming and sends a raycast to a target
    void Aim()
    {
        Vector3 camPosition = mainCam.position;
        Vector3 direction = mainCam.forward;
        ray = new Ray(camPosition, direction);
        if (Physics.Raycast(ray, out hit, maxDistance, aimLayer))
        {
            hitDetected = true;
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            bow.ShowCrosshair(hit.point);
        }
        else
        {
            hitDetected = false;
            bow.RemoveCrosshair();

        }

    }
    //character looking at the target
    void RotateCharacterSpine()
    {
        RotateToCamView();
        spine.LookAt(ray.GetPoint(maxDistance / 10));
        spine.Rotate(spineOffset);
    }

    public void Pull()
    {
        bow.PullString();
    }

    public void EnableArrow()
    {
        bow.PickArrow();
    }

    public void DisableArrow()
    {
        bow.DisableArrow();
    }
    public void Release()
    {
        bow.ReleaseString();
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (isAiming)
        {
            playerAnim.SetLookAtPosition(ray.GetPoint(lookAtPoint));
            playerAnim.SetLookAtWeight(1f);
        }
        else
        {
            playerAnim.SetLookAtWeight(0f);
        }
    }
}
