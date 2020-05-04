using System.Collections;
using UnityEngine;

public class SimpleMoveComponent : ATrapComponent
{
   private Vector3 initPosition;
   [SerializeField] private Vector3 delta = Vector3.zero;
   private Vector3 normalizedDelta;
   [SerializeField] private float openDelay = 0.5f;
   private float speed;

   private bool firstMove = true;

   private void Awake()
   {
      initPosition = transform.position;
      normalizedDelta = delta.normalized;
      speed = delta.magnitude / openDelay;
   }

   protected override void OnExecute()
   {
      StartCoroutine(Routine());
   }

   private IEnumerator Routine()
   {
      yield return new WaitForSeconds(openDelay);
      if (firstMove)
      {
         transform.position = initPosition + delta;
      }
      else
      {
         transform.position = initPosition;
      }
      firstMove = !firstMove;
      Stop();
   }

   private void FixedUpdate()
   {
      transform.Translate((firstMove ? 1 : -1) * speed * normalizedDelta * Time.fixedDeltaTime);
   }
}
