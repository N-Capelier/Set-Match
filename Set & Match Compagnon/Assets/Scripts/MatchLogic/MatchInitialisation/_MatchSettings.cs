using UnityEngine;

namespace TennisMatch
{
    /// <summary> ARD script <para>
    /// Permet de régler les paramètres du match
    /// </para></summary>
    public class _MatchSettings : MonoBehaviour
    {
        [Header("Variable")]
        [SerializeField] private MatchData match;

        public void SetMatchSetNumber(int numberOfSet)
        {
            //Au cas où
            Mathf.Clamp(numberOfSet, 1, 3);

            match.score.MatchSetNumber = numberOfSet;
        }
        public void SetMatchDouble(bool IsDouble)
        {
            match.doubleMatch = IsDouble;
        }
    }
}
