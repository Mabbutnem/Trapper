using UnityEngine;

public class TrappedPlayerController : MonoBehaviour
{
   private ATrappedPlayerState playerState;

   private void Awake()
   {
      playerState = new FlyState(GetComponent<TrappedPlayer>());
      playerState.OnEnterState();
   }

   private void Update()
   {
      playerState.HandleInput();
   }

   void FixedUpdate()
   {
      playerState.FixedUpdate();
      ATrappedPlayerState nextState = playerState.GetNextState();
      if(playerState != nextState)
      {
         playerState.OnExitState();
         nextState.OnEnterState();
         playerState = nextState;
      }
   }

   void OnCollisionEnter(Collision collision)
   {
      playerState.OnCollisionEnter(collision);
   }

   void OnCollisionExit(Collision collision)
   {
      playerState.OnCollisionExit(collision);
   }
}