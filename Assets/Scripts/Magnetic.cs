using UnityEngine;

public class Magnetic : MonoBehaviour
{
   [SerializeField] float magneticForce = 20f;
   [SerializeField] LayerMask whatIsMagnetic;

   private void OnTriggerStay(Collider other)
   {
      if (whatIsMagnetic == (whatIsMagnetic | (1 << other.gameObject.layer)))
      {
         Rigidbody rigidbody = other.GetComponent<Rigidbody>();
         if(rigidbody)
         {
            Vector3 dir = (gameObject.transform.position - other.gameObject.transform.position).normalized;
            float dist = (gameObject.transform.position - other.gameObject.transform.position).magnitude;
            rigidbody.AddForce((magneticForce / (dist * dist)) * dir);
         }
      }
   }
}
