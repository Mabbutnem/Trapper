using System.Collections.Generic;
using UnityEngine;

public class BadPlayerController : MonoBehaviour
{

   //TrapCommand with input
   public InputTrapCommand Trap1 { get; private set; }
   public InputTrapCommand Trap2 { get; private set; }
   public InputTrapCommand Trap3 { get; private set; }
   private List<InputTrapCommand> inputTrapCommands;

   public ATrapController TrapController { get; set; }

   private void Awake()
   {
      //TrapCommand with input
      Trap1 = new InputTrapCommand(TrapManager.NothingTrapCommand, InputNames.TRAP_1);
      Trap2 = new InputTrapCommand(TrapManager.NothingTrapCommand, InputNames.TRAP_2);
      Trap3 = new InputTrapCommand(TrapManager.NothingTrapCommand, InputNames.TRAP_3);
      inputTrapCommands = new List<InputTrapCommand>() {
         Trap1,
         Trap2,
         Trap3
      };

      TrapController = TrapManager.NothingTrapController;
   }

   private void Update()
   {
      TrapController.HandleInput();

      foreach (InputTrapCommand inputTrapCommand in inputTrapCommands)
      {
         if (Input.GetButtonDown(inputTrapCommand.InputName)
            && inputTrapCommand.TrapCommand.IsDone
            && inputTrapCommand.TrapCommand.Ready)
         {
            inputTrapCommand.TrapCommand.IsDone = false;
            inputTrapCommand.TrapCommand.Duration = 0f;
            inputTrapCommand.TrapCommand.Ready = false;
         }
      }
   }

   private void FixedUpdate()
   {
      TrapController.FixedUpdate();

      foreach (InputTrapCommand inputTrapCommand in inputTrapCommands)
      {
         if (!inputTrapCommand.TrapCommand.IsDone)
         {
            inputTrapCommand.TrapCommand.Execute();
            inputTrapCommand.TrapCommand.IsDone = true;
         }
         if (!inputTrapCommand.TrapCommand.Ready)
         {
            inputTrapCommand.TrapCommand.Duration += Time.fixedDeltaTime;
            if (inputTrapCommand.TrapCommand.Duration > inputTrapCommand.TrapCommand.CoolDown)
            {
               inputTrapCommand.TrapCommand.Ready = true;
            }
         }
      }
   }
}