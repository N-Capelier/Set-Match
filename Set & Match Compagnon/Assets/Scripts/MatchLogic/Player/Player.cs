using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Encryption;

namespace Player
{
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

        public bool Register(string name, string surname, string username, string mail, string password)
        {
            //First, check if email address and username do not exist in database
            //if()
            //{
            //    return false;
            //}

            //create ID and login
            id = new PlayerID(name, surname, username, mail, PlayerPassword.CreateEncryptedPassword(password));
            Login(mail, password);
            return true;
        }

        public void Login(string mail, string inputPassword)
        {
            //PlayerID player;
            //find corresponding mail in database and store playerID in player

            //Compare given password with database password (decrypted)
            //if(PlayerPassword.CreateEncryptedPassword(inputPassword, player.password.key).password == player.password.password)

            //Then log the player in
            isLoggedIn = true;

            //And go to main menus
        }

        public void Logout()
        {
            //log the player out
            isLoggedIn = false;

            //goto main menu
        }

        public void ResetPassword(string mail, string newPassword)
        {
            //Use this method only if the user's verification email code as been given

            id = new PlayerID(id.name, id.surname, id.username, id.mail, PlayerPassword.CreateEncryptedPassword(newPassword));
        }

        #endregion

        #region Stats and data Methods

        public void ResetAllAccountData()
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