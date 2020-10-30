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

        [Header("Team Informations")]
        [SerializeField] private GameObject aTeamP2Obj;
        [SerializeField] private GameObject bTeamP2Obj;
        [Space(25)]
        [SerializeField] private Text aTeamNameP1;
        [SerializeField] private Text bTeamNameP1;
        [SerializeField] private TextMeshProUGUI TMPaTeamNameP1;
        [SerializeField] private TextMeshProUGUI TMPbTeamNameP1;
        [Space(10)]
        [SerializeField] private Text aTeamNameP2;
        [SerializeField] private Text bTeamNameP2;
        [SerializeField] private TextMeshProUGUI TMPaTeamNameP2;
        [SerializeField] private TextMeshProUGUI TMPbTeamNameP2;

        [Space(25)]
        public Image teamA_P1img;
        public Image teamB_P1img;
        [Space(10)]
        public Image teamA_P2img;
        public Image teamB_P2img;

        private void OnEnable()
        {
            MatchEvents.onMatchStart += UpdateOnEvent;
            UpdateOnEvent();
        }
        private void OnDisable()
        {
            MatchEvents.onMatchStart -= UpdateOnEvent;
        }

        private void UpdateOnEvent()
        {
            UpdateNameAndImange(match.doubleMatch);
        }

        public void UpdateNameAndImange(bool isDouble)
        {
            teamA_P1img.sprite = match.teamA_P1img;
            teamB_P1img.sprite = match.teamB_P1img;

            if (this.aTeamNameP1 != null) { aTeamNameP1.text = match.teamA_Player1; }
            if (this.bTeamNameP1 != null) { bTeamNameP1.text = match.teamB_Player1; }

            if (this.TMPaTeamNameP1 != null) { TMPaTeamNameP1.text = match.teamA_Player1; }
            if (this.TMPbTeamNameP1 != null) { TMPbTeamNameP1.text = match.teamB_Player1; }


            if (isDouble)
            {
                aTeamP2Obj.SetActive(true);
                bTeamP2Obj.SetActive(true);

                teamA_P2img.sprite = match.teamA_P2img;
                teamB_P2img.sprite = match.teamB_P2img;

                if (this.aTeamNameP2 != null) { aTeamNameP2.text = match.teamA_Player2; }
                if (this.bTeamNameP2 != null) { bTeamNameP2.text = match.teamB_Player2; }

                if (this.TMPaTeamNameP2 != null) { TMPaTeamNameP2.text = match.teamA_Player2; }
                if (this.TMPbTeamNameP2 != null) { TMPbTeamNameP2.text = match.teamB_Player2; }
            }
            else
            {
                aTeamP2Obj.SetActive(false);
                bTeamP2Obj.SetActive(false);
            }
        }
    }
}
