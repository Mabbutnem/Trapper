using UnityEngine;

public class GroundSmasherController : ASpecialTrapController
{
   private int currentSmasher = 3;
   private int nextSmasher = 3;
   private GameObject[] smashers = new GameObject[5];
   private SimpleMoveComponent[] smashersMove = new SimpleMoveComponent[5];

   public override void OnEnter()
   {
      for(int i = 0; i < smashers.Length; i++)
      {
         smashers[i] = GameObjectUtils.Find("Ground Smasher " + (i+1));
         smashers[i].GetComponent<Renderer>().material.color = ConstantsManager.Yellow;
         smashersMove[i] = smashers[i].GetComponent<SimpleMoveComponent>();
      }
      smashersMove[3].Execute();
   }

   public override void OnExit()
   {
      for (int i = 0; i < smashers.Length; i++)
      {
         smashers[i].GetComponent<Renderer>().material.color = ConstantsManager.Border;
      }
      smashersMove[currentSmasher].Execute();
   }

   public override void HandleInput()
   {
      if (Input.GetKeyDown(KeyCode.LeftArrow)) { DecrSmasher(); }
      if (Input.GetKeyDown(KeyCode.RightArrow)) { IncrSmasher(); }
   }

   public override void FixedUpdate()
   {
      if (nextSmasher != currentSmasher)
      {
         smashersMove[currentSmasher].Execute();
         smashersMove[nextSmasher].Execute();
         currentSmasher = nextSmasher;
      }
   }

   private void IncrSmasher()
   {
      if(currentSmasher < 4 &&
         Mathf.Abs(currentSmasher - nextSmasher) == 0
         && !smashersMove[currentSmasher].enabled)
      {
         nextSmasher++;
      }
   }
   private void DecrSmasher()
   {
      if(currentSmasher > 0 &&
         Mathf.Abs(currentSmasher - nextSmasher) == 0
         && !smashersMove[currentSmasher].enabled)
      {
         nextSmasher--;
      }
   }
}
