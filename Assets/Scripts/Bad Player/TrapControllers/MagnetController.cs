using UnityEngine;

public class MagnetController : ATrapController
{
   private static readonly float speed = 400f;
   private static readonly float moveSmoothing = 0.05f;

   private GameObject magnet;
   private Rigidbody rigidbody;
   private Vector2 move = Vector2.zero;
   private Vector3 velocity = Vector3.zero;

   public MagnetController(string name) : base(name) { }

   private void LoadMagnet()
   {
      GameObject prefab = ConstantsManager.Magnet;
      magnet = Object.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
      rigidbody = magnet.GetComponent<Rigidbody>();
   }

   public override void OnEnter()
   {
      LoadMagnet();
   }

   public override void OnExit()
   {
      Object.Destroy(magnet);
   }

   public override void HandleInput()
   {
      move.x = Input.GetAxisRaw(InputNames.BAD_HORIZONTAL);
      move.y = Input.GetAxisRaw(InputNames.BAD_VERTICAL);
   }

   public override void FixedUpdate()
   {
      if (!magnet) { LoadMagnet(); }
      move = move.normalized;
      Vector3 targetVelocity = new Vector3(move.x * speed * Time.fixedDeltaTime, move.y * speed * Time.fixedDeltaTime, rigidbody.velocity.z);
      rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref velocity, moveSmoothing);
   }
}
