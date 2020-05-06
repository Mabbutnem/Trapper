using UnityEngine;

public class BouncingBallCommand : ATrapCommand
{
   private Vector3 initSpeed = new Vector3(5f, -2f, 0f);

   private OpenAndClose rightTrap;
   private GameObject previewBouncingBall;

   public BouncingBallCommand(string name, float coolDown) : base(name, coolDown) { }

   public override ATrapCommand Initialize()
   {
      rightTrap = GameObjectUtils.Find("Falling Stones Right").GetComponent<OpenAndClose>();
      return base.Initialize();
   }

   public override void Execute()
   {
      GameObject prefab = ConstantsManager.BouncingBall;
      GameObject bouncingBall = Object.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
      bouncingBall.GetComponent<Rigidbody>().velocity = initSpeed;
      rightTrap.OpenWaitClose();
   }

   public override void StartPreview()
   {
      GameObject prefab = ConstantsManager.BouncingBall;
      previewBouncingBall = Object.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
      previewBouncingBall.GetComponent<Rigidbody>().velocity = initSpeed;
      rightTrap.Open();
   }

   public override void StopPreview()
   {
      Object.Destroy(previewBouncingBall);
      rightTrap.Close();
   }
}
