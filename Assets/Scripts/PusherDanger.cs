using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusherDanger : MonoBehaviour
{
   private Danger danger;
   private float time;

   private void Awake()
   {
      danger = GetComponent<Danger>();
   }

   public void MakeDangerousFor(float time)
   {
      this.time = time;
      StartCoroutine(Routine());
   }

   public IEnumerator Routine()
   {
      danger.MakeDangerousNoColor();
      yield return new WaitForSeconds(time);
      danger.MakeHarmlessBorder();
   }
}
