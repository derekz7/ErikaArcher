using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private Flash flash;
    [SerializeField] private GameObject mapIntro;
    [SerializeField] private TextMeshProUGUI mapName;
    [SerializeField] private TextMeshProUGUI mapDescription;
    [SerializeField] private Map map;

    void Start()
    {
        
    }
    private async void OnTriggerEnter(Collider other)
    {
        mapName.text = map.MapName();
        mapDescription.text = map.Description();
        if (other.CompareTag("Player") && other.TryGetComponent<Player>(out var player))
        {
            Debug.Log(map.MapName());
            flash.StartAnimator();
            await Task.Delay(300);
            player.Teleport(destination.position);
            await Task.Delay(500);
            mapIntro.GetComponent<Animator>().SetTrigger("startIntro");
           
        }
    }
}
