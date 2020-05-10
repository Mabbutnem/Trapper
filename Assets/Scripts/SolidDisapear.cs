using System.Collections;
using UnityEngine;

public class SolidDisapear : MonoBehaviour
{
   [SerializeField] private float disapearTime = 1f;

   private new Collider collider;
   private new Renderer renderer;

   private void Awake()
   {
      collider = GetComponent<Collider>();
      renderer = GetComponent<Renderer>();
   }

   public void Disapear()
   {
      collider.enabled = false;
      renderer.enabled = false;
   }

   public void Apear()
   {
      collider.enabled = true;
      renderer.enabled = true;
   }

   public void DisapearForSeconds()
   {
      StartCoroutine(DisapearRoutine());
   }

   private IEnumerator DisapearRoutine()
   {
      Disapear();
      yield return new WaitForSeconds(disapearTime);
      Apear();
   }
}
