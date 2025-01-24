using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Foot : MonoBehaviour, IInteractuable
{
    [SerializeField] private UnityEngine.UI.Image footHUD;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private string infoText;
    [SerializeField] private Player player;

    public void Interact()
    {
        player.DoubleJumpSkill = true;
        footHUD.color = Color.white;
        uiManager.showUIText(infoText);
        Destroy(this.gameObject);
    }
}
