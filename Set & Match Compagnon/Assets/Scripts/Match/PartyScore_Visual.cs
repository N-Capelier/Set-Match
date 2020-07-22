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
    public class PartyScore_Visual : MonoBehaviour
    {
        [Header("Component")]
        [SerializeField] private PartyScore score;
        [SerializeField] private ScoreScreen scoreScreen;
        [Space(15)]
        [SerializeField] private RectTransform ball;
        [SerializeField] private RectTransform ballSadow1, ballSadow2, ballSadow3, ballSadow4, ballSadow5;
        [Space(15)]
        [SerializeField] private TextMeshProUGUI TeamAName;
        [SerializeField] private TextMeshProUGUI TeamBName;
        [Space(15)]
        [SerializeField] private TextMeshProUGUI TeamA_Score;
        [SerializeField] private TextMeshProUGUI TeamB_Score;
        [Space(15)]
        [SerializeField] private TextMeshProUGUI TeamA_Set1;
        [SerializeField] private TextMeshProUGUI TeamA_Set2, TeamA_Set3;
        [SerializeField] private TextMeshProUGUI TeamB_Set1, TeamB_Set2, TeamB_Set3;


        [Header("Variable")]
        [SerializeField] private MatchData match;
        [Space(10)]
        [SerializeField] private float focusDur = 0.5f;
        [SerializeField] private float changeServDur = 1f;
        [SerializeField] private float ballTeamAPos, ballTeamBPos;
        [SerializeField] private Ease easeType = Ease.InOutCubic;

        private void OnEnable()
        {
            score.onTeamA_GameMarked += OnGameMarked;
            score.onTeamB_GameMarked += OnGameMarked;

        }
        private void OnDisable()
        {
            score.onTeamA_GameMarked -= OnGameMarked;
            score.onTeamB_GameMarked -= OnGameMarked;
        }

        private void Start()
        {
            TeamAName.text = match.teamA_Player1;
            TeamBName.text = match.teamB_Player1;
        }

        private void Update()
        {
            switch (match.teamA_Score.point)
            {
                case 0:
                    TeamA_Score.text = "0";
                    break;
                case 1:
                    TeamA_Score.text = "15";
                    break;
                case 2:
                    TeamA_Score.text = "30";
                    break;
                case 3:
                    TeamA_Score.text = "40";
                    break;
                case 4:
                    TeamA_Score.text = "40A";
                    break;
                default:
                    TeamA_Score.text = "error";
                    break;
            }

            switch (match.teamB_Score.point)
            {
                case 0:
                    TeamB_Score.text = "0";
                    break;
                case 1:
                    TeamB_Score.text = "15";
                    break;
                case 2:
                    TeamB_Score.text = "30";
                    break;
                case 3:
                    TeamB_Score.text = "40";
                    break;
                case 4:
                    TeamB_Score.text = "40A";
                    break;
                default:
                    TeamB_Score.text = "error";
                    break;
            }


            TeamA_Set1.text = match.teamA_Score.gamePerSet[1].ToString();
            TeamB_Set1.text = match.teamB_Score.gamePerSet[1].ToString();

            if (match.MatchSetNumber >= 2)
            {
                TeamA_Set2.text = match.teamA_Score.gamePerSet[2].ToString();
                TeamB_Set2.text = match.teamB_Score.gamePerSet[2].ToString();

                if (match.MatchSetNumber >= 3)
                {
                    TeamA_Set3.text = match.teamA_Score.gamePerSet[3].ToString();
                    TeamB_Set3.text = match.teamB_Score.gamePerSet[3].ToString();
                }
                else
                {
                    TeamA_Set3.text = "";
                    TeamB_Set3.text = "";
                }
            }
            else
            {
                TeamA_Set2.text = "";
                TeamB_Set2.text = "";
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
