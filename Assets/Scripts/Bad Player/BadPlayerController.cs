using System.Collections.Generic;
using UnityEngine;

public class BadPlayerController : MonoBehaviour
{
   //Manage all TrapCommand
   public static ATrapCommand NothingTrapCommand { get; } = new NothingTrapCommand();
   public static ATrapCommand Trap1Command { get; } = new Trap1Command();

   public static ATrapCommand TrapSpec1Command { get; } = new TrapSpec1Command();
   public static ATrapCommand TrapSpec2Command { get; } = new TrapSpec2Command();

   //TrapCommand with input
   public InputTrapCommand TrapSpecial { get; set; }
   public InputTrapCommand Trap1 { get; set; }
   public InputTrapCommand Trap2 { get; set; }
   public InputTrapCommand Trap3 { get; set; }
   private List<InputTrapCommand> inputTrapCommands;

   public ASpecialTrapController SpecialTrapController { get; private set; }

   private void Awake()
   {
      //Manage All TrapCommand
      NothingTrapCommand.Initialize();
      Trap1Command.Initialize();
      TrapSpec1Command.Initialize();
      TrapSpec2Command.Initialize();

      //TrapCommand with input
      TrapSpecial = new InputTrapCommand(TrapSpec1Command, InputNames.TRAP_SPECIAL);
      Trap1 = new InputTrapCommand(Trap1Command, InputNames.TRAP_1);
      Trap2 = new InputTrapCommand(NothingTrapCommand, InputNames.TRAP_2);
      Trap3 = new InputTrapCommand(NothingTrapCommand, InputNames.TRAP_3);
      inputTrapCommands = new List<InputTrapCommand>() {
         TrapSpecial,
         Trap1,
         Trap2,
         Trap3
      };

      SpecialTrapController = new NothingTrapController();
   }

   private void Update()
   {
      SpecialTrapController.HandleInput();

      foreach (InputTrapCommand inputTrapCommand in inputTrapCommands)
      {
         if (Input.GetButtonDown(inputTrapCommand.InputName) && inputTrapCommand.TrapCommand.IsDone)
         {
            inputTrapCommand.TrapCommand.IsDone = false;
         }
      }
   }

   private void FixedUpdate()
   {
      SpecialTrapController.FixedUpdate();

      foreach (InputTrapCommand inputTrapCommand in inputTrapCommands)
      {
         if (!inputTrapCommand.TrapCommand.IsDone)
         {
            inputTrapCommand.TrapCommand.Execute();
            inputTrapCommand.TrapCommand.IsDone = true;
         }
      }
   }

   public void SetSpecialTrapController(ASpecialTrapController specialTrapController)
   {
      SpecialTrapController.OnExit();
      SpecialTrapController = specialTrapController;
      SpecialTrapController.OnEnter();
   }
}