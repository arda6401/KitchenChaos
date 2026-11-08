using System;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{

 private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
    //    player.OnPickedSomething += Player_OnPickedSomething;
      //  player.OnPickedUpSomething += Player_OnPickedUpSomething;
        //player.OnDroppedSomething += Player_OnDroppedSomething;
    }

    private void Player_OnDroppedSomething(object sender, EventArgs e)
    {
   //     SoundManager.Instance.PlaySound(SoundManager.Sound.Drop);
    }

    private void Player_OnPickedUpSomething(object sender, EventArgs e)
    {
    //    SoundManager.Instance.PlaySound(SoundManager.Sound.PickUp);
    }

    private void Player_OnPickedSomething(object sender, EventArgs e)
    {
      //  SoundManager.Instance.PlaySound(SoundManager.Sound.Chop);
    }
}
