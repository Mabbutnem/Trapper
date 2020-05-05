using System.Collections;
using UnityEngine;

public class OpenAndClose : MonoBehaviour
{
   private Vector3 initPosition;
   [SerializeField] private Vector3 delta = Vector3.zero;
   private Vector3 normalizedDelta;
   [SerializeField] private float openDelay = 0.5f;
   private float openSpeed;
   [SerializeField] private float waitDelay = 1.2f;
   [SerializeField] private float closeDelay = 0.5f;
   private float closeSpeed;

   public bool IsExecuting { get; private set; } = false;
   private bool isOpen = false;
   private bool isOpening = false;
   private bool isClosing = false;

   private void Awake()
   {
      initPosition = transform.position;
      normalizedDelta = delta.normalized;
      openSpeed = delta.magnitude / openDelay;
      closeSpeed = delta.magnitude / closeDelay;
   }

   public void OpenWaitClose()
   {
      if(!IsExecuting && !isOpen)
      {
         StartCoroutine(OpenWaitCloseRoutine());
      }
   }

   public void Open()
   {
      if (!IsExecuting && !isOpen)
      {
         StartCoroutine(OpenRoutine());
      }
   }

   public void Close()
   {
      if (!IsExecuting && isOpen)
      {
         StartCoroutine(CloseRoutine());
      }
   }

   private IEnumerator OpenWaitCloseRoutine()
   {
      IsExecuting = true;
      isOpening = true;
      yield return new WaitForSeconds(openDelay);
      isOpening = false;
      isOpen = true;
      transform.position = initPosition + delta;
      yield return new WaitForSeconds(waitDelay);
      isOpen = false;
      isClosing = true;
      yield return new WaitForSeconds(closeDelay);
      isClosing = false;
      transform.position = initPosition;
      IsExecuting = false;
   }

   private IEnumerator OpenRoutine()
   {
      IsExecuting = true;
      isOpening = true;
      yield return new WaitForSeconds(openDelay);
      isOpening = false;
      isOpen = true;
      transform.position = initPosition + delta;
      IsExecuting = false;
   }

   private IEnumerator CloseRoutine()
   {
      IsExecuting = true;
      isOpen = false;
      isClosing = true;
      yield return new WaitForSeconds(closeDelay);
      isClosing = false;
      transform.position = initPosition;
      IsExecuting = false;
   }

   private void FixedUpdate()
   {
      if(!IsExecuting) { return; }

      if(isOpening)
      {
         transform.Translate(openSpeed * normalizedDelta * Time.fixedDeltaTime);
      }
      else if(isClosing)
      {
         transform.Translate(-closeSpeed * normalizedDelta * Time.fixedDeltaTime);
      }
   }
}
