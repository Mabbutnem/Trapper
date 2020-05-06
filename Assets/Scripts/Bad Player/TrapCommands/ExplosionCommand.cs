using UnityEngine;

public class ExplosionCommand : ATrapCommand
{
   private static readonly int nbBullets = 8;
   private static readonly Vector3 center = new Vector3(0f, 1f, 0f);
   private static readonly float distFromCenter = 0.5f;
   private static readonly float bulletSpeed = 5f;

   private GameObject[] previewBullets;

   public ExplosionCommand(string name, float coolDown) : base(name, coolDown) { }

   public override ATrapCommand Initialize()
   {
      return base.Initialize();
   }

   public override void Execute()
   {
      GameObject prefab = ConstantsManager.Bullet;
      for (int i = 0; i < nbBullets; i++)
      {
         float degAngle = ((float)i / (float)nbBullets)*360f;
         float radAngle = degAngle * Mathf.Deg2Rad;
         Vector3 delta = new Vector3(Mathf.Cos(radAngle), Mathf.Sin(radAngle), 0f);
         GameObject bullet = Object.Instantiate(prefab, center + distFromCenter * delta, Quaternion.Euler(0f, 0f, degAngle - 90f));
         bullet.GetComponent<Rigidbody>().velocity = bulletSpeed * delta;
      }
   }

   public override void StartPreview()
   {
      GameObject prefab = ConstantsManager.Bullet;
      previewBullets = new GameObject[nbBullets];
      for(int i = 0; i < previewBullets.Length; i++)
      {
         float degAngle = ((float)i / (float)nbBullets) * 360f;
         float radAngle = degAngle * Mathf.Deg2Rad;
         Vector3 position = center + new Vector3(distFromCenter*Mathf.Cos(radAngle), distFromCenter*Mathf.Sin(radAngle));
         previewBullets[i] = Object.Instantiate(prefab, position, Quaternion.Euler(0f, 0f, degAngle - 90f));
      }
   }

   public override void StopPreview()
   {
      for (int i = 0; i < previewBullets.Length; i++)
      {
         Object.Destroy(previewBullets[i]);
      }
      previewBullets = null;
   }
}
