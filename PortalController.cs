using UnityEngine;
using UnityEngine.UI;

public class PortalController : MonoBehaviour
{
    [SerializeField]
    private Text teleLocationName;
    private Portal[] Listportals;
    private Player player;
    [SerializeField]
    private GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        //if (Input.GetKey(KeyCode.Escape))
        //{
        //    Hide();
        //}
        //if (Input.GetKey(KeyCode.J))
        //{
        //    OnPortalButtonClick(Listportals[0]);
        //}
        //if (Input.GetKey(KeyCode.K))
        //{
        //    OnPortalButtonClick(Listportals[1]);
        //}

    }

  

    void Hide()
    {
        panel.SetActive(false);
    }
}
