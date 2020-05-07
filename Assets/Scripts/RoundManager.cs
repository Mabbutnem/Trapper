using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
   private static readonly float timeToWaitBetweenChoices = 1f;
   private static readonly string BAD_VICTORY_TEXT = "Bad Player Wins !";
   private static readonly string GOOD_VICTORY_TEXT = "Good Player Wins !";

   [SerializeField] private int nbTrapByChoice = 2;
   [SerializeField] private int roundNumber = 3;
   [SerializeField] private float roundTime = 120f;

   private int currentRound = 0;

   private TrappedPlayer trappedPlayer;
   private TrapManager trapManager;

   private Text victoryText;
   private Text trapChoiceText;

   private void Awake()
   {
      trappedPlayer = GameObjectUtils.Find("Player").GetComponent<TrappedPlayer>();
      trapManager = GameObjectUtils.Find("Trap Manager").GetComponent<TrapManager>();

      victoryText = GameObjectUtils.Find("Victory Text").GetComponent<Text>();
      victoryText.enabled = false;
      trapChoiceText = GameObjectUtils.Find("Trap Choice Text").GetComponent<Text>();
      trapChoiceText.enabled = false;
   }

   private void Start()
   {
      NextRound();
   }

   public void BadPlayerWins()
   {
      trappedPlayer.MustWait = true;
      StopAllCoroutines();
      victoryText.enabled = true;
      victoryText.text = BAD_VICTORY_TEXT;
   }

   public void GoodPlayerWins()
   {
      victoryText.enabled = true;
      victoryText.text = GOOD_VICTORY_TEXT;
   }

   public void NextRound()
   {
      currentRound++;
      if(currentRound <= roundNumber)
      {
         trapChoiceText.enabled = true;
         trapManager.ResetTrapsAndController();
         trapManager.ChooseController(nbTrapByChoice, StartChooseTrap1Routine);
      }
      else
      {
         GoodPlayerWins();
      }
   }

   private void StartChooseTrap1Routine()
   {
      StartCoroutine(ChooseTrap1Routine());
   }

   private IEnumerator ChooseTrap1Routine()
   {
      yield return new WaitForSeconds(timeToWaitBetweenChoices);
      trapManager.ChooseTrap1(nbTrapByChoice, StartChooseTrap2Routine);
   }

   private void StartChooseTrap2Routine()
   {
      StartCoroutine(ChooseTrap2Routine());
   }

   private IEnumerator ChooseTrap2Routine()
   {
      yield return new WaitForSeconds(timeToWaitBetweenChoices);
      trapManager.ChooseTrap2(nbTrapByChoice, StartRoundRoutine);
   }

   private void StartRoundRoutine()
   {
      StartCoroutine(RoundRoutine());
   }

   private IEnumerator RoundRoutine()
   {
      trapChoiceText.enabled = false;
      trappedPlayer.MustWait = false;
      yield return new WaitForSeconds(roundTime);
      trappedPlayer.MustWait = true;
      NextRound();
   }
}
