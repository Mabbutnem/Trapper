using UnityEngine;

public class ShooterController : ATrapController
{
   private static readonly float speed = 400f;
   private static readonly float moveSmoothing = 0.05f;
   private static readonly float reloadDelay = 2f;
   private static readonly float bulletSpeed = 40f;

   private GameObject shooter;
   private Rigidbody rigidbody;
   private float horizontal = 0f;
   private Vector3 velocity = Vector3.zero;
   private bool shoot = false;
   private bool reloading = false;
   private float reloadTime = 0f;

   public ShooterController(string name) : base(name) { }

   private void LoadShooter()
   {
      GameObject prefab = ConstantsManager.Shooter;
      shooter = Object.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
      rigidbody = shooter.GetComponent<Rigidbody>();
   }

   public override void OnEnter()
   {
      LoadShooter();
   }

   public override void OnExit()
   {
      Object.Destroy(shooter);
   }

   public override void HandleInput()
   {
      horizontal = Input.GetAxisRaw(InputNames.BAD_HORIZONTAL);
      if(!reloading && Input.GetButtonDown(InputNames.TRAP_SPECIAL))
      {
         shoot = true;
         reloading = true;
         reloadTime = 0f;
      }
   }

   public override void FixedUpdate()
   {
      if (!shooter) { LoadShooter(); }

      Vector3 targetVelocity = new Vector3(horizontal * speed * Time.fixedDeltaTime, rigidbody.velocity.y, rigidbody.velocity.z);
      rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref velocity, moveSmoothing);

      if(shoot)
      {
         shoot = false;
         GameObject prefab = ConstantsManager.Bullet;
         Vector3 position = shooter.transform.position - new Vector3(0f, (shooter.transform.localScale.y + prefab.transform.localScale.y)/2f, 0f);
         GameObject bullet = Object.Instantiate(prefab, position, prefab.transform.rotation);
         bullet.GetComponent<Rigidbody>().velocity = new Vector3(0f, -bulletSpeed, 0f);
      }
      if(reloading)
      {
         reloadTime += Time.fixedDeltaTime;
         if(reloadTime >= reloadDelay)
         {
            reloading = false;
         }
      }
   }
}
