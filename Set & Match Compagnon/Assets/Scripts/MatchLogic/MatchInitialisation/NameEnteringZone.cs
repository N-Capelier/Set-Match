using UnityEngine;
using UnityEngine.UI;

namespace TennisMatch
{
    public class NameEnteringZone : MonoBehaviour
    {
        [Header("GameMeta")]
        [SerializeField] private MatchData match;
        [SerializeField] private InputField[] inputFields = new InputField[4];

        public void UpdateNameEnter(int playerNumber)
        {
            switch (playerNumber)
            {
                case 1:
                    match.teamA_Player1 = inputFields[playerNumber - 1].text;
                    break;
                case 2:
                    match.teamB_Player1 = inputFields[playerNumber - 1].text;
                    break;
                case 3:
                    match.teamA_Player2 = inputFields[playerNumber - 1].text;
                    break;
                case 4:
                    match.teamB_Player2 = inputFields[playerNumber - 1].text;
                    break;
                default:
                    Debug.Log("trouve pas le player");
                    break;
            }
        }

    }
}
