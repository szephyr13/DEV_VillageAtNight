using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour, IInteractuable
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Player player;
    [SerializeField] private float lifeRestoring;

    private void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    public void Interact()
    {
        AudioManager.instance.PlaySFX("PowerUp");
        player.UpdateLifes(player.GetComponent<LifeSystem>().Lifes + lifeRestoring);
        Destroy(this.gameObject);
    }
}
