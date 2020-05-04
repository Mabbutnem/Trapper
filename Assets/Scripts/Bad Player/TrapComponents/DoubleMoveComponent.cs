using System.Collections;
using UnityEngine;

public class DoubleMoveComponent : ATrapComponent
{
   private Vector3 initPosition;
   [SerializeField] private Vector3 delta = Vector3.zero;
   private Vector3 normalizedDelta;
   [SerializeField] private float openDelay = 0.5f;
   private float speed;
   [SerializeField] private float waitDelay = 1.2f;

   private bool isOpening = false;
   private bool isClosing = false;

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
      isOpening = true;
      yield return new WaitForSeconds(openDelay);
      isOpening = false;
      transform.position = initPosition + delta;
      yield return new WaitForSeconds(waitDelay);
      isClosing = true;
      yield return new WaitForSeconds(openDelay);
      isClosing = false;
      transform.position = initPosition;
      Stop();
   }

   private void FixedUpdate()
   {
      if(isOpening)
      {
         transform.Translate(speed * normalizedDelta * Time.fixedDeltaTime);
      }
      else if(isClosing)
      {
         transform.Translate(-speed * normalizedDelta * Time.fixedDeltaTime);
      }
   }
}
