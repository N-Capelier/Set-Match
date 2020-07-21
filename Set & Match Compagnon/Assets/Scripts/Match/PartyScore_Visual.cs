using TMPro;
using UnityEngine;

namespace TennisMatch
{
    public class PartyScore_Visual : MonoBehaviour
    {
        [Header("Component")]
        [SerializeField] private PartyScore score;
        [Space(15)]
        [SerializeField] private TextMeshProUGUI TeamAName, TeamBName;
        [SerializeField] private TextMeshProUGUI TeamA_Set1, TeamA_Set2, TeamA_Set3;
        [SerializeField] private TextMeshProUGUI TeamB_Set1, TeamB_Set2, TeamB_Set3;

    }
}
