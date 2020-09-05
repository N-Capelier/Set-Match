using UnityEngine;

namespace TennisMatch
{
    /// <summary> ARD script <para>
    /// Permet de régler les paramètres du match
    /// </para></summary>
    public class _MatchSettings : MonoBehaviour
    {
        [Header("Variable")]
        [SerializeField] private MatchData matchData;

        public void SetMatchSetNumber(int numberOfSet)
        {
            //Au cas où
            Mathf.Clamp(numberOfSet, 1, 3);

            matchData.score.MatchSetNumber = numberOfSet;
        }
        public void SetMatchDouble(bool IsDouble)
        {
            matchData.doubleMatch = IsDouble;
        }
    }
}
