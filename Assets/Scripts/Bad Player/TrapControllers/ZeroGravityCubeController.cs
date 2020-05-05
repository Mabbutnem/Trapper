using UnityEngine;

public class ZeroGravityCubeController : ATrapController
{
   private static readonly float speed = 400f;
   private static readonly float moveSmoothing = 0.05f;

   private GameObject zeroGravityCube;
   private Rigidbody rigidbody;
   private Vector2 move = Vector2.zero;
   private Vector3 velocity = Vector3.zero;

   public ZeroGravityCubeController(string name) : base(name) { }

   private void LoadZeroGravityCube()
   {
      GameObject prefab = ConstantsManager.ZeroGravityCube;
      zeroGravityCube = Object.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
      rigidbody = zeroGravityCube.GetComponent<Rigidbody>();
   }

   public override void OnEnter()
   {
      LoadZeroGravityCube();
   }

   public override void OnExit()
   {
      Object.Destroy(zeroGravityCube);
   }

   public override void HandleInput()
   {
      move.x = Input.GetAxisRaw(InputNames.BAD_HORIZONTAL);
      move.y = Input.GetAxisRaw(InputNames.BAD_VERTICAL);
   }

   public override void FixedUpdate()
   {
      if(!zeroGravityCube) { LoadZeroGravityCube(); }
      move = move.normalized;
      Vector3 targetVelocity = new Vector3(move.x * speed * Time.fixedDeltaTime, move.y * speed * Time.fixedDeltaTime, rigidbody.velocity.z);
      rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref velocity, moveSmoothing);
   }
}
