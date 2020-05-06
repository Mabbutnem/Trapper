using UnityEngine;

public class PusherCommand : ATrapCommand
{
   private static readonly float dangerousTime = 3f;

   private OpenAndClose pusher1;
   private OpenAndClose pusher2;
   private PusherDanger pusherDanger1;
   private PusherDanger pusherDanger2;
   private Danger danger1;
   private Danger danger2;

   public PusherCommand(string name, float coolDown) : base(name, coolDown) { }

   public override ATrapCommand Initialize()
   {
      GameObject p1 = GameObjectUtils.Find("Pusher 1");
      GameObject p2 = GameObjectUtils.Find("Pusher 2");
      pusher1 = p1.GetComponent<OpenAndClose>();
      pusher2 = p2.GetComponent<OpenAndClose>();
      pusherDanger1 = p1.GetComponent<PusherDanger>();
      pusherDanger2 = p2.GetComponent<PusherDanger>();
      danger1 = p1.GetComponent<Danger>();
      danger2 = p2.GetComponent<Danger>();
      return base.Initialize();
   }

   public override void Execute()
   {
      pusher1.OpenWaitClose();
      pusher2.OpenWaitClose();
      pusherDanger1.MakeDangerousFor(dangerousTime);
      pusherDanger2.MakeDangerousFor(dangerousTime);
   }

   public override void StartPreview()
   {
      pusher1.Open();
      pusher2.Open();
      danger1.MakeDangerousNoColor();
      danger2.MakeDangerousNoColor();
   }

   public override void StopPreview()
   {
      pusher1.Close();
      pusher2.Close();
      danger1.MakeHarmlessBorder();
      danger2.MakeHarmlessBorder();
   }
}
