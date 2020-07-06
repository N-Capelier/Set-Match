using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// NCO
    /// </summary>
    public struct PlayerID
    {
        public PlayerStats stats;
        public PlayerData data;
        public string name { get; private set; }
        public string surname { get; private set; }
        public string username { get; private set; }
        public string mail { get; private set; }
        public string password { get; private set; }
        public bool checkedTOS { get; private set; }

        public PlayerID(string name, string surname, string username, string mail, string password, bool checkedTOS)
        {
            stats = new PlayerStats();
            data = new PlayerData();

            this.name = name;
            this.surname = surname;
            this.username = username;
            this.mail = mail;
            this.password = password;
            this.checkedTOS = checkedTOS;
        }
    }

    /// <summary>
    /// NCO
    /// </summary>
    public class Player : MonoBehaviour
    {
        #region Variables

        PlayerID id;
        bool isLoggedIn = false;
        bool isInMatch = false;

        #endregion

        #region Login Methods

        public bool Register(string name, string surname, string username, string mail, string password, bool checkedTOS)
        {
            //First, check if email address and username do not exist in database
            //if()
            //{
            //    return false;
            //}

            //create ID and login
            if(checkedTOS)
            {
                id = new PlayerID(name, surname, username, mail, password, checkedTOS);
                Login(mail, password);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Login(string mail, string password)
        {
            isLoggedIn = true;

            //goto main menu
        }

        public void Logout()
        {
            isLoggedIn = false;

            //goto main menu
        }

        #endregion

        #region Stats and data Methods

        public void ResetAllData()
        {
            ResetStats();
            ResetData();
        }

        public PlayerStats ResetStats()
        {
            return id.stats = new PlayerStats();
        }

        public PlayerData ResetData()
        {
            return id.data = new PlayerData();
        }

        #endregion

    }

}