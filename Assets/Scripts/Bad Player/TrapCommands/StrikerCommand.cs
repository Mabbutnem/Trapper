using UnityEngine;

public class StrikerCommand : ATrapCommand
{
   private static readonly float initSpeed = 30f;

   private OpenAndClose ground2;
   private OpenAndClose ground5;
   private SolidDisapear realGround;

   public StrikerCommand(string name, float coolDown) : base(name, coolDown) { }

   public override ATrapCommand Initialize()
   {
      ground2 = GameObjectUtils.Find("Ground 2").GetComponent<OpenAndClose>();
      ground5 = GameObjectUtils.Find("Ground 5").GetComponent<OpenAndClose>();
      realGround = GameObjectUtils.Find("Real Ground").GetComponent<SolidDisapear>();
      return base.Initialize();
   }

   public override void Execute()
   {
      GameObject prefab = ConstantsManager.Striker;
      GameObject striker1 = Object.Instantiate(prefab, new Vector3(-5f, -20f, 0f), prefab.transform.rotation);
      GameObject striker2 = Object.Instantiate(prefab, new Vector3(5f, -20f, 0f), prefab.transform.rotation);
      striker1.GetComponent<Rigidbody>().velocity = initSpeed * Vector3.up;
      striker2.GetComponent<Rigidbody>().velocity = initSpeed * Vector3.up;

      ground2.OpenWaitClose();
      ground5.OpenWaitClose();
      realGround.DisapearForSeconds();
   }

   public override void StartPreview()
   {
      ground2.Open();
      ground5.Open();
      realGround.Disapear();
   }

   public override void StopPreview()
   {
      ground2.Close();
      ground5.Close();
      realGround.Apear();
   }
}
