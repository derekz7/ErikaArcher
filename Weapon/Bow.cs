using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [System.Serializable]
    public class BowSettings
    {
        [Header("Arrow Setting")]
        public float arrowCount;
        public Rigidbody arrowPrefab;
        public Transform arrowPos;
        public Transform spawnArrowPos;
        public Transform arrowEquipParent;
        public float arrowForce = 30f;

        [Header("Bow Equip & UnEquip Settings")]
        public Transform EquipPos;
        public Transform UnEquipPos;

        public Transform UnEquipParent;
        public Transform EquipParent;

        [Header("Bow String Settings")]
        public Transform bowStrings;
        public Transform stringInitialPos;
        public Transform stringHandPullPos;
        public Transform stringInitialParent;

        [Header("Bow Audio Setting")]
        public AudioClip releaseStringAudio;
        public AudioClip drawArrowAudio;
        public AudioClip fireAudio;

    }
    [SerializeField]
    private BowSettings bowSettings;

    [Header("Crosshair Settings")]
    [SerializeField]
    private GameObject crossHairPrefab;
    private GameObject currentCrossHair;
    private GameObject crosshair;

    //Arrow
    private Rigidbody currentArrow;
    private AudioSource bowAudio;
    [SerializeField]
    private ParticleSystem muzzleFlash;

    private bool canPullString = false;
    private bool canFireArrow = false;

    // Start is called before the first frame update
    void Start()
    {
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        bowAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PickArrow()
    {
        canPullString = true;
        bowAudio.PlayOneShot(bowSettings.drawArrowAudio);
        bowSettings.arrowPos.gameObject.SetActive(true);
    }
    public void DisableArrow()
    {
        bowSettings.arrowPos.gameObject.SetActive(false);
    }

    public void PullString()
    {
        if (canPullString)
        {
            //gan vi tri day cung = vi tri dat tay keo day cung
            bowSettings.bowStrings.transform.position = bowSettings.stringHandPullPos.position;
            bowSettings.bowStrings.transform.parent = bowSettings.stringHandPullPos;
            canPullString = false;
            canFireArrow = true;
        }

    }

    public void ReleaseString()
    {
        canPullString = true;
        bowSettings.bowStrings.transform.position = bowSettings.stringInitialPos.position;
        bowSettings.bowStrings.transform.parent = bowSettings.stringInitialParent;
    }

    public void EquipBow()
    {
        this.transform.position = bowSettings.EquipPos.position;
        this.transform.rotation = bowSettings.EquipPos.rotation;
        this.transform.parent = bowSettings.EquipParent;

    }
    public void UnEquipBow()
    {
        this.transform.position = bowSettings.UnEquipPos.position;
        this.transform.rotation = bowSettings.UnEquipPos.rotation;
        this.transform.parent = bowSettings.UnEquipParent;

    }

    public void ShowCrosshair(Vector3 crosshairPos)
    {
        if (!currentCrossHair)
        {
            currentCrossHair = Instantiate(crossHairPrefab) as GameObject;

        }

        currentCrossHair.transform.position = crosshairPos;
        currentCrossHair.transform.LookAt(Camera.main.transform);
        crosshair.SetActive(true);

    }

    public void RemoveCrosshair()
    {
        if (currentCrossHair)
        {
            Destroy(currentCrossHair);
        }
        crosshair.SetActive(false);
    }
    public void Fire(Vector3 hitPoint)
    {
        if (canFireArrow)
        {
            muzzleFlash.Play();
            bowAudio.PlayOneShot(bowSettings.fireAudio);
            Vector3 dir = hitPoint - bowSettings.spawnArrowPos.position;
            currentArrow = Instantiate(bowSettings.arrowPrefab, bowSettings.spawnArrowPos.position, bowSettings.spawnArrowPos.rotation);
            currentArrow.AddForce(dir * bowSettings.arrowForce, ForceMode.Force);
            canFireArrow = false;
        }

    }
  

}
