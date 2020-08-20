using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace TennisMatch
{
    /// <summary>
    /// ARD
    /// </summary>
    public class NameImageOfPlayerSetter : MonoBehaviour
    {
        [Header("GameEvent")]
        [SerializeField] private MatchData match;
        private MatchEvents matchEvents;

        [Header("Team Informations")]
        [SerializeField] private GameObject aTeamP2Obj;
        [SerializeField] private GameObject bTeamP2Obj;
        [Space(25)]
        [SerializeField] private TextMeshProUGUI aTeamNameP1;
        [SerializeField] private TextMeshProUGUI bTeamNameP1;
        [Space(10)]
        [SerializeField] private TextMeshProUGUI aTeamNameP2;
        [SerializeField] private TextMeshProUGUI bTeamNameP2;
        [Space(25)]
        public Image teamA_P1img;
        public Image teamB_P1img;
        [Space(10)]
        public Image teamA_P2img;
        public Image teamB_P2img;

        private void Awake() => matchEvents = MatchEvents.Instance;
        private void OnEnable()
        {
            matchEvents.onMatchStart += UpdateOnEvent;
            UpdateOnEvent();
        }
        private void OnDisable()
        {
            matchEvents.onMatchStart -= UpdateOnEvent;
        }

        private void UpdateOnEvent()
        {
            UpdateNameAndImange(match.doubleMatch);
        }

        public void UpdateNameAndImange(bool isDouble)
        {
            teamA_P1img.sprite = match.teamA_P1img;
            teamB_P1img.sprite = match.teamB_P1img;

            aTeamNameP1.text = match.teamA_Player1;
            bTeamNameP1.text = match.teamB_Player1;

            if (isDouble)
            {
                aTeamP2Obj.SetActive(true);
                bTeamP2Obj.SetActive(true);

                teamA_P2img.sprite = match.teamA_P2img;
                teamB_P2img.sprite = match.teamB_P2img;

                aTeamNameP2.text = match.teamA_Player2;
                bTeamNameP2.text = match.teamB_Player2;
            }
            else
            {
                aTeamP2Obj.SetActive(false);
                bTeamP2Obj.SetActive(false);
            }
        }
    }
}
