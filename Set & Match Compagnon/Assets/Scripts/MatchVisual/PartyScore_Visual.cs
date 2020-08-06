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
        [Header("GameEvent")]
        private MatchEvents matchEvents;

        [Header("Component")]
        [SerializeField] private PartyScore score;
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
            matchEvents.onGameMarked += OnGameMarked;
            matchEvents.onVisualUpdate += UpdateVisual;
        }
        private void OnDisable()
        {
            matchEvents.onGameMarked -= OnGameMarked;
            matchEvents.onVisualUpdate -= UpdateVisual;
        }

        private void Start()
        {
            aTeamName.text = match.teamA_Player1;
            bTeamName.text = match.teamB_Player1;
        }

        private void UpdateVisual()
        {
            switch (match.teamA_Score.point)
            {
                case 0:
                    aTeam_Score.text = "0";
                    break;
                case 1:
                    aTeam_Score.text = "15";
                    break;
                case 2:
                    aTeam_Score.text = "30";
                    break;
                case 3:
                    aTeam_Score.text = "40";
                    break;
                case 4:
                    aTeam_Score.text = "40A";
                    break;
                default:
                    aTeam_Score.text = "error";
                    break;
            }

            switch (match.teamB_Score.point)
            {
                case 0:
                    bTeam_Score.text = "0";
                    break;
                case 1:
                    bTeam_Score.text = "15";
                    break;
                case 2:
                    bTeam_Score.text = "30";
                    break;
                case 3:
                    bTeam_Score.text = "40";
                    break;
                case 4:
                    bTeam_Score.text = "40A";
                    break;
                default:
                    bTeam_Score.text = "error";
                    break;
            }


            aTeam_Set1.text = match.teamA_Score.gamePerSet[0].ToString();
            bTeam_Set1.text = match.teamB_Score.gamePerSet[0].ToString();

            if (match.MatchSetNumber >= 2)
            {
                aTeam_Set2.text = match.teamA_Score.gamePerSet[1].ToString();
                bTeam_Set2.text = match.teamB_Score.gamePerSet[1].ToString();

                if (match.MatchSetNumber >= 3)
                {
                    aTeam_Set3.text = match.teamA_Score.gamePerSet[2].ToString();
                    bTeam_Set3.text = match.teamB_Score.gamePerSet[2].ToString();
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
        private void OnGameMarked(bool aTeamAction)
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
    }
}
