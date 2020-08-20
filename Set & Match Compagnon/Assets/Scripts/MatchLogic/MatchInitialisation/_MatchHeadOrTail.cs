using UnityEngine;

namespace TennisMatch
{
    /// <summary> ARD script <para>
    /// Jeu de pile ou face
    /// </para></summary>
    public class _MatchHeadOrTail : MonoBehaviour
    {
        [Header("Match")]
        [SerializeField] private MatchEvents matchEvents;
        [SerializeField] private MatchData match;

        [Header("Variable")]
        public bool aTeamChooseHead = true;
        public bool bTeamChooseHead = true;
        public bool coinhaveBeenLauch = false;
        public bool coinResultIsHead = false;
        
        // pour etre utilisable avec un toggle
        public void TeamA_ChooseHead(bool head)
        {
            if (head)
            {
                aTeamChooseHead = true;
            }
            else
            {
                aTeamChooseHead = false;
            }
        }
        public void TeamB_ChooseHead(bool head)
        {
            if (head)
            {
                bTeamChooseHead = true;
            }
            else
            {
                bTeamChooseHead = false;
            }
        }

        /// <summary> <list type="Results">
        /// <item> <description> True = Head </description> </item>
        /// <item> <description> False = Tail </description> </item>
        /// </list> </summary>
        private bool CoinFLauch()
        {
            // 0-1-2-3-4 means Head win 
            // 5-6-7-8-9 means Tail win
            return UnityEngine.Random.Range(0, 10) < 5 ? true : false;
        }

        /// <summary> 
        /// return si l'équipe A commence selon les paris pris
        /// </summary>
        public void WillTeamAStartGame()
        {
            //Si les deux choix sont opposé
            if (aTeamChooseHead != bTeamChooseHead)
            {
                if (!coinhaveBeenLauch)
                {
                    bool resultIsHead = CoinFLauch();

                    if (aTeamChooseHead)
                    {
                        match.teamA_StartGame = resultIsHead ? true : false;
                    }
                    else
                    {
                        match.teamA_StartGame = resultIsHead ? false : true;
                    }

                    coinhaveBeenLauch = true;
                }
                else 
                {
                    //Debug.Log("coin have been already lauch");
                }
            }
            //si ils ont fait le même choix
            else
            {
                //Debug.Log("choices aren't valid");
            }
        }
    
    }
}
