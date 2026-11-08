using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Recipe")]
public class SoundSO : ScriptableObject
{
  public AudioClip[] chop;
  public AudioClip[] deliveryFail;
  public AudioClip[] deliverySuccess;
  public AudioClip[] footsteps;
  public AudioClip[] objectDrop;
  public AudioClip[] objectPickup;
  public AudioClip[] stoveSizzle;
  public AudioClip[] trash;
  public AudioClip[] warning;

}
