using UnityEngine;
using TMPro;
using TennisMatch;
using System;

public class Visualizer_UnfinishedMatch : MonoBehaviour
{
    [Header("Match to load")]
    [SerializeField] private PlayerWaitingMatchs waitingMatchs;

    [Header("Team Informations")]
    [SerializeField] private int indexUnfinishedMatch = 1;

    [Header("UI Component")]
    [SerializeField] private TextMeshProUGUI aTeam_Name;
    [SerializeField] private TextMeshProUGUI bTeam_Name;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI hourUI;
    [SerializeField] private TextMeshProUGUI dateUI;
    [Space(25)]
    [SerializeField] private TextMeshProUGUI lastGame_aPoint;
    [SerializeField] private TextMeshProUGUI Set1_aGames;
    [SerializeField] private TextMeshProUGUI Set2_aGames;
    [SerializeField] private TextMeshProUGUI Set3_aGames;
    [Space(5)]
    [SerializeField] private TextMeshProUGUI lastGame_bPoint;
    [SerializeField] private TextMeshProUGUI Set1_bGames;
    [SerializeField] private TextMeshProUGUI Set2_bGames;
    [SerializeField] private TextMeshProUGUI Set3_bGames;

    private void Start()
    {
        UnfinnishedMatch matchToDraw = waitingMatchs.matchs[indexUnfinishedMatch - 1];

        if (matchToDraw.teamA_Player1 == "")
        {
            aTeam_Name.text = "Unfinnished Match";
            bTeam_Name.text = "";
        }
        else if (matchToDraw.doubleMatch)
        {
            aTeam_Name.text = matchToDraw.teamA_Player1;
            bTeam_Name.text = matchToDraw.teamB_Player1;
        }
        else
        {
            aTeam_Name.text = matchToDraw.teamA_Player1 + " & " + matchToDraw.teamA_Player2;
            bTeam_Name.text = matchToDraw.teamB_Player1 + " & " + matchToDraw.teamB_Player2;
        }

        DateTime date = matchToDraw.date;
        if (date == new DateTime())
        {
            dateUI.text = "Empty";
            hourUI.text = "Game";
        }
        else
        {
            hourUI.text = date.Day + "/" + date.Month + "/" + date.Year;
            dateUI.text = date.Hour + ":" + date.Minute;
        }

        Score score = matchToDraw.score;

        int aTeamPointOfLastGame = score.Sets[score.actualSet].Games[score.Sets[score.actualSet].actualGame].aTeamPoint;
        int bTeamPointOfLastGame = score.Sets[score.actualSet].Games[score.Sets[score.actualSet].actualGame].bTeamPoint;

        lastGame_aPoint.text = DataVisualizer.IntPointIntoString(aTeamPointOfLastGame, bTeamPointOfLastGame).ToString();
        lastGame_bPoint.text = DataVisualizer.IntPointIntoString(bTeamPointOfLastGame, aTeamPointOfLastGame).ToString();



        Set1_aGames.text = score.Sets[0].aTeamGames.ToString();
        Set1_bGames.text = score.Sets[0].bTeamGames.ToString();

        Set2_bGames.text = score.actualSet >= 1 ? score.Sets[1].bTeamGames.ToString() : " ";
        Set2_aGames.text = score.actualSet >= 1 ? score.Sets[1].bTeamGames.ToString() : " ";

        Set3_aGames.text = score.actualSet >= 2 ? score.Sets[2].bTeamGames.ToString() : " ";
        Set3_bGames.text = score.actualSet >= 2 ? score.Sets[2].bTeamGames.ToString() : " ";

    }
}
