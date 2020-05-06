using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TrapManager : MonoBehaviour
{
   public static ATrapCommand NothingTrapCommand { get; } = new NothingTrapCommand("Nothing", 5f);
   public static ATrapCommand TaclerCommand { get; } = new TaclerCommand("Tacler", 5f);
   public static ATrapCommand FallingStonesCommand { get; } = new FallingStonesCommand("Falling Stones", 15f);
   public static ATrapCommand SpikesCommand { get; } = new SpikesCommand("Spikes", 10f);
   public static ATrapCommand HoleCommand { get; } = new HoleCommand("Holes", 15f);
   public static ATrapCommand ExplosionCommand { get; } = new ExplosionCommand("Explosion", 8f);
   public static ATrapCommand BouncingBallCommand { get; } = new BouncingBallCommand("Bouncing Ball", 15f);
   public static ATrapCommand StrikerCommand { get; } = new StrikerCommand("Striker", 15f);
   public static ATrapCommand PusherCommand { get; } = new PusherCommand("Pusher", 10f);
   private static ATrapCommand[] trapCommands;

   public static ATrapController NothingTrapController { get; } = new NothingTrapController("Nothing");
   public static ATrapController ZeroGravityCubeController { get; } = new ZeroGravityCubeController("Zero Gravity Cube");
   public static ATrapController SmasherController { get; } = new SmasherController("Smasher");
   public static ATrapController ShooterController { get; } = new ShooterController("Shooter");
   public static ATrapController SmugglerController { get; } = new SmugglerController("Smuggler");
   public static ATrapController MagnetController { get; } = new MagnetController("Magnet");
   private static ATrapController [] trapControllers;

   private BadPlayerController badPlayerController;

   private Dictionary<ATrapCommand, Button> trapChoiceButtons;
   private List<ATrapCommand> alreadyChoosenTraps;
   private List<ATrapCommand> trapCurrentChoices;

   private Dictionary<ATrapController, Button> controllerChoiceButtons;
   private List<ATrapController> controllerCurrentChoices;

   private void Awake()
   {
      //TRAPS
      trapCommands = new ATrapCommand[]
      {
         TaclerCommand.Initialize(),
         FallingStonesCommand.Initialize(),
         SpikesCommand.Initialize(),
         HoleCommand.Initialize(),
         ExplosionCommand.Initialize(),
         BouncingBallCommand.Initialize(),
         StrikerCommand.Initialize(),
         PusherCommand.Initialize()
      };

      trapChoiceButtons = new Dictionary<ATrapCommand, Button>();
      foreach (ATrapCommand trapCommand in trapCommands)
      {
         trapChoiceButtons.Add(trapCommand, GameObjectUtils.Find(trapCommand.Name + " Button").GetComponent<Button>());
      }
      foreach (Button button in trapChoiceButtons.Values) { button.gameObject.SetActive(false); }
      alreadyChoosenTraps = new List<ATrapCommand>();
      trapCurrentChoices = new List<ATrapCommand>();


      //CONTROLLERS
      trapControllers = new ATrapController[]
      {
         ZeroGravityCubeController,
         SmasherController,
         ShooterController,
         SmugglerController,
         MagnetController
      };

      controllerChoiceButtons = new Dictionary<ATrapController, Button>();
      foreach (ATrapController trapController in trapControllers)
      {
         controllerChoiceButtons.Add(trapController, GameObjectUtils.Find(trapController.Name + " Button").GetComponent<Button>());
      }
      foreach (Button button in controllerChoiceButtons.Values) { button.gameObject.SetActive(false); }
      controllerCurrentChoices = new List<ATrapController>();

      badPlayerController = GameObjectUtils.Find("Bad Player").GetComponent<BadPlayerController>();
   }

   public void Start()
   {
      ChooseTrap1(8);
   }

   public void ResetTrapsAndController()
   {
      badPlayerController.Trap1.TrapCommand = NothingTrapCommand;
      badPlayerController.Trap2.TrapCommand = NothingTrapCommand;
      badPlayerController.Trap3.TrapCommand = NothingTrapCommand;

      badPlayerController.TrapController.OnExit();
      badPlayerController.TrapController = NothingTrapController;
      badPlayerController.TrapController.OnEnter();

      alreadyChoosenTraps.Clear();
   }

   #region Trap
   public void ChooseTrap1(int nbChoice)
   {
      ChooseTrap(badPlayerController.Trap1, nbChoice);
   }

   public void ChooseTrap2(int nbChoice)
   {
      ChooseTrap(badPlayerController.Trap2, nbChoice);
   }

   public void ChooseTrap3(int nbChoice)
   {
      ChooseTrap(badPlayerController.Trap3, nbChoice);
   }

   private void ChooseTrap(InputTrapCommand inputTrapCommand , int nbChoice)
   {
      List<ATrapCommand> availableTraps = new List<ATrapCommand>(trapCommands);
      availableTraps.RemoveAll(trapCommand => alreadyChoosenTraps.Contains(trapCommand));
      if(availableTraps.Count < nbChoice)
      {
         Debug.LogError("Not enought avaible traps for " + nbChoice + " choices!");
         Debug.Break();
      }
      else
      {
         for(int i = 0; i < nbChoice; i++)
         {
            ATrapCommand randomTrap = availableTraps[Random.Range(0, availableTraps.Count)];
            randomTrap.StartPreview();
            availableTraps.Remove(randomTrap);
            trapCurrentChoices.Add(randomTrap);

            Button associatedButton = trapChoiceButtons[randomTrap];
            associatedButton.gameObject.SetActive(true);
            associatedButton.onClick.AddListener(() => {
               ATrapCommand trapCommand = trapChoiceButtons.FirstOrDefault(x => x.Value == associatedButton).Key;
               inputTrapCommand.TrapCommand = trapCommand;
               alreadyChoosenTraps.Add(trapCommand);
               ResetTrapChoices();
            });
         }
      }
   }

   private void ResetTrapChoices()
   {
      foreach (ATrapCommand trapCommand in trapCurrentChoices)
      {
         trapCommand.StopPreview();
         trapChoiceButtons[trapCommand].onClick.RemoveAllListeners();
         trapChoiceButtons[trapCommand].gameObject.SetActive(false);
      }
      trapCurrentChoices.Clear();
   }
   #endregion

   #region Controller
   public void ChooseController(int nbChoice)
   {
      if (trapControllers.Length < nbChoice)
      {
         Debug.LogError("Not enought avaible controllers for " + nbChoice + " choices!");
         Debug.Break();
      }
      else
      {
         List<ATrapController> availableController = new List<ATrapController>(trapControllers);
         for (int i = 0; i < nbChoice; i++)
         {
            ATrapController randomController = availableController[Random.Range(0, availableController.Count)];
            randomController.OnEnter();
            availableController.Remove(randomController);
            controllerCurrentChoices.Add(randomController);

            Button associatedButton = controllerChoiceButtons[randomController];
            associatedButton.gameObject.SetActive(true);
            associatedButton.onClick.AddListener(() => {
               ATrapController trapController = controllerChoiceButtons.FirstOrDefault(x => x.Value == associatedButton).Key;
               badPlayerController.TrapController = trapController;
               ResetControllerChoices(trapController);
            });
         }
      }
   }

   private void ResetControllerChoices(ATrapController choosenTrapController)
   {
      foreach (ATrapController trapController in controllerCurrentChoices)
      {
         if (trapController != choosenTrapController) { trapController.OnExit(); }
         controllerChoiceButtons[trapController].onClick.RemoveAllListeners();
         controllerChoiceButtons[trapController].gameObject.SetActive(false);
      }
      controllerChoiceButtons.Clear();
   }
   #endregion
}
