using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLimits : MonoBehaviour
{
   [SerializeField] private float destroyDangerDelay = 5f;

   private void OnTriggerExit(Collider other)
   {
      GameObject obj = other.gameObject;
      Danger danger = obj.GetComponent<Danger>();
      if(danger)
      {
         if (danger.removeOutOfLimit)
         {
            Destroy(obj, destroyDangerDelay);
         }
         return;
      }

      TrappedPlayer trappedPlayer = obj.GetComponent<TrappedPlayer>();
      if(trappedPlayer)
      {
         trappedPlayer.MustDie = true;
         return;
      }
   }
}
