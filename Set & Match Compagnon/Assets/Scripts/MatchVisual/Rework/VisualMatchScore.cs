using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TennisMatch
{
    /// <summary>
    /// ARD
    /// </summary>
    public class VisualMatchScore : MonoBehaviour
    {
        [Header("GameEvent")]
        private MatchEvents matchEvents;

        [Header("Component")]
        [SerializeField] private CornerScreen scoreScreen;
        [Space(15)]
        [SerializeField] private RectTransform ball;
        [SerializeField] private RectTransform ballSadow1, ballSadow2, ballSadow3, ballSadow4, ballSadow5;
        [Space(15)]
        [SerializeField] private TextMeshProUGUI aTeamName, bTeamName;
        [Space(15)]
        [SerializeField] private TextMeshProUGUI aTeam_Score, bTeam_Score;
        [Space(15)]
        [SerializeField] private TextMeshProUGUI aTeam_Set1;
        [SerializeField] private TextMeshProUGUI aTeam_Set2, aTeam_Set3;
        [SerializeField] private TextMeshProUGUI bTeam_Set1, bTeam_Set2, bTeam_Set3;

        [Header("Variable")]
        [SerializeField] private MatchData match;
        [Space(10)]
        [SerializeField] private bool baalInTeamA = true;
        [SerializeField] private float focusDur = 0.5f;
        [SerializeField] private float changeServDur = 1f;
        [SerializeField] private float ballTeamAPos, ballTeamBPos;
        [SerializeField] private Ease easeType = Ease.InOutCubic;

        private void Awake() => matchEvents = MatchEvents.Instance;
        private void OnEnable()
        {
            matchEvents.onMatchStart += Initialisation;
            matchEvents.onGameMarked += OnGameMarked;
            matchEvents.onVisualUpdate += UpdateVisual;
        }
        private void OnDisable()
        {
            matchEvents.onMatchStart -= Initialisation;
            matchEvents.onGameMarked -= OnGameMarked;
            matchEvents.onVisualUpdate -= UpdateVisual;
        }

        private void Initialisation()
        {
            //player name
            aTeamName.text = match.teamA_Player1;
            bTeamName.text = match.teamB_Player1;
            
            if (match.doubleMatch)
            {
                aTeamName.text = match.teamA_Player1 + " & " + match.teamA_Player2;
                bTeamName.text = match.teamB_Player1 + " & " + match.teamB_Player2;
            }

            //Point
            aTeam_Score.text = "0";
            bTeam_Score.text = "0";

            //Set affichage
            int setNumber = match.score.MatchSetNumber;

            aTeam_Set1.text = "0";
            bTeam_Set1.text = "0";
            if (setNumber >= 2)
            {
                aTeam_Set2.text = "0";
                bTeam_Set2.text = "0";

                if (setNumber >= 3)
                {
                    aTeam_Set3.text = "0";
                    bTeam_Set3.text = "0";
                }
                else
                {
                    aTeam_Set3.text = "";
                    bTeam_Set3.text = "";
                }
            }
            else
            {
                aTeam_Set2.text = "";
                bTeam_Set2.text = "";
                aTeam_Set3.text = "";
                bTeam_Set3.text = "";
            }
            
        }

        private void UpdateVisual()
        {
            int currentSet = match.score.actualSet;
            int currentGame = match.score.Sets[currentSet].actualGame;

            //Point
            int aTeamPoints = match.score.Sets[currentSet].Games[currentGame].aTeamPoint;
            int bTeamPoints = match.score.Sets[currentSet].Games[currentGame].bTeamPoint;

            aTeam_Score.text = IntPointIntoString(aTeamPoints, bTeamPoints);
            bTeam_Score.text = IntPointIntoString(bTeamPoints, aTeamPoints);

            //Set
            int setNumber = match.score.MatchSetNumber;

            aTeam_Set1.text = match.score.Sets[0].aTeamGames.ToString();
            bTeam_Set1.text = match.score.Sets[0].bTeamGames.ToString();
            
            if (setNumber >= 2)
            {
                if(currentSet>= 1)
                {
                    aTeam_Set2.text = match.score.Sets[1].aTeamGames.ToString();
                    bTeam_Set2.text = match.score.Sets[1].bTeamGames.ToString();
                }
                else
                {
                    aTeam_Set2.text = "0";
                    bTeam_Set2.text = "0";
                }

                if (setNumber >= 3)
                {
                    if (currentSet >= 2)
                    {
                        aTeam_Set3.text = match.score.Sets[2].aTeamGames.ToString();
                        bTeam_Set3.text = match.score.Sets[2].bTeamGames.ToString();
                    }
                    else
                    {
                        aTeam_Set3.text = "0";
                        bTeam_Set3.text = "0";
                    }
                }
                else
                {
                    aTeam_Set3.text = "";
                    bTeam_Set3.text = "";
                }
            }
            else
            {
                aTeam_Set2.text = "";
                bTeam_Set2.text = "";
                aTeam_Set3.text = "";
                bTeam_Set3.text = "";
            }
        }

        public string IntPointIntoString(int allPoints, int advPoints)
        {
            if(allPoints <= 3)
            {
                switch (allPoints)
                {
                    case 0:
                        return "0";
                    case 1:
                        return "15";
                    case 2:
                        return "30";
                    case 3:
                        return "40";
                    default:
                        return "XX";
                }

            }
            else
            {
                if(allPoints > advPoints)
                {
                    return "40A";
                }
                else
                {
                    return "40";
                }
            }
        }

        private void ServiceChange()
        {
            float delay = 0.02f;

            if (match.teamA_HaveService)
            {
                ball.DOAnchorPosY(ballTeamAPos, changeServDur).SetEase(easeType);

                ballSadow1.DOAnchorPosY(ballTeamAPos, changeServDur).SetEase(easeType).SetDelay(delay);
                ballSadow2.DOAnchorPosY(ballTeamAPos, changeServDur).SetEase(easeType).SetDelay(delay * 2);
                ballSadow3.DOAnchorPosY(ballTeamAPos, changeServDur).SetEase(easeType).SetDelay(delay * 3);
                ballSadow4.DOAnchorPosY(ballTeamAPos, changeServDur).SetEase(easeType).SetDelay(delay * 4);
                ballSadow5.DOAnchorPosY(ballTeamAPos, changeServDur).SetEase(easeType).SetDelay(delay * 5);
            }
            else
            {
                ball.DOAnchorPosY(ballTeamBPos, changeServDur).SetEase(easeType);

                ballSadow1.DOAnchorPosY(ballTeamBPos, changeServDur).SetEase(easeType).SetDelay(delay);
                ballSadow2.DOAnchorPosY(ballTeamBPos, changeServDur).SetEase(easeType).SetDelay(delay * 2);
                ballSadow3.DOAnchorPosY(ballTeamBPos, changeServDur).SetEase(easeType).SetDelay(delay * 3);
                ballSadow4.DOAnchorPosY(ballTeamBPos, changeServDur).SetEase(easeType).SetDelay(delay * 4);
                ballSadow5.DOAnchorPosY(ballTeamBPos, changeServDur).SetEase(easeType).SetDelay(delay * 5);
            }
        }
        private void OnGameMarked()
        {
            StartCoroutine(ScoreTemporaryFocus(focusDur));
        }
        IEnumerator ScoreTemporaryFocus(float duration)
        {
            scoreScreen.CenterViewport(duration);

            yield return new WaitForSecondsRealtime(duration);

            ServiceChange();

            yield return new WaitForSecondsRealtime(changeServDur);

            scoreScreen.CornerViewport(duration);

            yield return null;
        }
    }
}
