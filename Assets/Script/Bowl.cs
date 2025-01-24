using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour, IInteractuable
{
    [SerializeField] private UnityEngine.UI.Image bowlHUD;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private string infoText;
    [SerializeField] private Player player;

    public void Interact()
    {
        AudioManager.instance.PlaySFX("PowerUp");
        player.AttackPower *= 1.5f;
        bowlHUD.color = Color.white;
        uiManager.showUIText(infoText);
        Destroy(this.gameObject);
    }
}
