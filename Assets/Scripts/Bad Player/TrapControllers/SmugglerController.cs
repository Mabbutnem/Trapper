using UnityEngine;

public class SmugglerController : ATrapController
{
   private static readonly float speed = 400f;
   private static readonly float moveSmoothing = 0.05f;
   private static readonly float centerSpeed = 5f;
   private static readonly float positionLimit = 8.75f;

   private GameObject smuggler;
   private Rigidbody rigidbody;
   private GameObject centerSmugler;
   private Transform centerTransform;

   private float vertical = 0f;
   private Vector3 velocity = Vector3.zero;

   private bool leftSide = false;
   private bool travelling = true;

   public SmugglerController(string name) : base(name) { }

   private void LoadSmuggler()
   {
      GameObject prefab = ConstantsManager.Smuggler;
      smuggler = Object.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
      rigidbody = smuggler.GetComponent<Rigidbody>();
      centerSmugler = smuggler.transform.GetChild(1).gameObject;
      centerTransform = centerSmugler.transform;
   }

   public override void OnEnter()
   {
      LoadSmuggler();
      leftSide = Random.Range(0, 2) == 0;
   }

   public override void OnExit()
   {
      Object.Destroy(smuggler);
   }

   public override void HandleInput()
   {
      vertical = Input.GetAxisRaw(InputNames.BAD_VERTICAL);

      if (!travelling && Input.GetButtonDown(InputNames.TRAP_SPECIAL))
      {
         travelling = true;
      }
   }

   public override void FixedUpdate()
   {
      if (!smuggler) { LoadSmuggler(); }

      Vector3 targetVelocity = new Vector3(rigidbody.velocity.x, vertical * speed * Time.fixedDeltaTime, rigidbody.velocity.z);
      rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref velocity, moveSmoothing);

      if (travelling)
      {
         float dir = leftSide ? -1 : 1;
         centerTransform.Translate(new Vector3(dir * centerSpeed * Time.fixedDeltaTime, 0f, 0f));
         if (leftSide)
         {
            if (centerTransform.position.x <= -positionLimit)
            {
               centerTransform.position = new Vector3(-positionLimit, centerTransform.position.y, centerTransform.position.z);
               travelling = false;
               leftSide = false;
            }
         }
         else
         {
            if (centerTransform.position.x >= positionLimit)
            {
               centerTransform.position = new Vector3(positionLimit, centerTransform.position.y, centerTransform.position.z);
               travelling = false;
               leftSide = true;
            }
         }
      }
   }
}
