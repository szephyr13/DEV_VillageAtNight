using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractuable
{
    [SerializeField] private UnityEngine.UI.Image keyHUD;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private string infoText;
    [SerializeField] private Player player;

    public void Interact()
    {
        AudioManager.instance.PlaySFX("PowerUp");
        player.CanOpenDoor = true;
        keyHUD.color = Color.white;
        uiManager.showUIText(infoText);
        Destroy(this.gameObject);
    }
}
