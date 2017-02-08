using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha_Test_42_Bot
{
    class Users
    {
        public int i;              // i = index de l'instance Users dans la List UserId
        public string Name;
        public int XP;                  // Toutes les variables doivent être initialisées depuis user.config et sauvegardées si modifiées
        public int TotalChar;
        public float TotalWritingTime;  // Vitesse moyenne d'écriture des messages : (Nb de char du message) / (temps d'écriture)

        public Users(int i, string Name, int XP, int TotalChar, float TotalWritingTime)
        {
            this.i = i;
            this.Name = Name;
            this.XP = XP;
            this.TotalChar = TotalChar;
            this.TotalWritingTime = TotalWritingTime;
        }

        public Users(int i, string Name)
        {
            this.i = i;
            this.Name = Name;
            XP = 0;
            TotalChar = 0;
            TotalWritingTime = 0;

            bool[] list = { true, true, true, true };
            Update(true, true, true, true, Name, XP, TotalChar, TotalWritingTime);
        }

        public void MessageEnvoye(int CharNb, float Time)
        {
            TotalChar += CharNb;
            Console.WriteLine(TotalChar);
            TotalWritingTime += Time;
            Update(false, false, true, true, "", 0, TotalChar, TotalWritingTime);
        }

        public void Update(bool a, bool b, bool c, bool d, string Name, int XP, int TotalChar, float TotalWritingTime)
        {
            if (a) UpdateName(Name);
            if (b) UpdateXP(XP);
            if (c) UpdateTotalChar(TotalChar);
            if (d) UpdateTotalWritingTime(TotalWritingTime);
        }

        public void UpdateName(string Name)
        {
            switch (i)
            {
                case 0:
                    Properties.Settings.Default.User0Name = Name;
                    break;
                case 1:
                    Properties.Settings.Default.User1Name = Name;
                    break;
                case 2:
                    Properties.Settings.Default.User2Name = Name;
                    break;
                case 3:
                    Properties.Settings.Default.User3Name = Name;
                    break;
                case 4:
                    Properties.Settings.Default.User4Name = Name;
                    break;
                case 5:
                    Properties.Settings.Default.User5Name = Name;
                    break;
                case 6:
                    Properties.Settings.Default.User6Name = Name;
                    break;
                case 7:
                    Properties.Settings.Default.User7Name = Name;
                    break;
                case 8:
                    Properties.Settings.Default.User8Name = Name;
                    break;
                case 9:
                    Properties.Settings.Default.User9Name = Name;
                    break;
            }
        }

        public void UpdateXP(int XP)
        {
            switch (i)
            {
                case 0:
                    Properties.Settings.Default.User0XP = XP;
                    break;
                case 1:
                    Properties.Settings.Default.User1XP = XP;
                    break;
                case 2:
                    Properties.Settings.Default.User2XP = XP;
                    break;
                case 3:
                    Properties.Settings.Default.User3XP = XP;
                    break;
                case 4:
                    Properties.Settings.Default.User4XP = XP;
                    break;
                case 5:
                    Properties.Settings.Default.User5XP = XP;
                    break;
                case 6:
                    Properties.Settings.Default.User6XP = XP;
                    break;
                case 7:
                    Properties.Settings.Default.User7XP = XP;
                    break;
                case 8:
                    Properties.Settings.Default.User8XP = XP;
                    break;
                case 9:
                    Properties.Settings.Default.User9XP = XP;
                    break;
            }
        }

        public void UpdateTotalChar(int TotalChar)
        {
            switch (i)
            {
                case 0:
                    Properties.Settings.Default.User0TotalChar = TotalChar;
                    break;
                case 1:
                    Properties.Settings.Default.User1TotalChar = TotalChar;
                    break;
                case 2:
                    Properties.Settings.Default.User2TotalChar = TotalChar;
                    break;
                case 3:
                    Properties.Settings.Default.User3TotalChar = TotalChar;
                    break;
                case 4:
                    Properties.Settings.Default.User4TotalChar = TotalChar;
                    break;
                case 5:
                    Properties.Settings.Default.User5TotalChar = TotalChar;
                    break;
                case 6:
                    Properties.Settings.Default.User6TotalChar = TotalChar;
                    break;
                case 7:
                    Properties.Settings.Default.User7TotalChar = TotalChar;
                    break;
                case 8:
                    Properties.Settings.Default.User8TotalChar = TotalChar;
                    break;
                case 9:
                    Properties.Settings.Default.User9TotalChar = TotalChar;
                    break;
            }
        }

        public void UpdateTotalWritingTime(float TotalWritingTime)
        {
            switch (i)
            {
                case 0:
                    Properties.Settings.Default.User0TotalWritingTime = TotalWritingTime;
                    break;
                case 1:
                    Properties.Settings.Default.User1TotalWritingTime = TotalWritingTime;
                    break;
                case 2:
                    Properties.Settings.Default.User2TotalWritingTime = TotalWritingTime;
                    break;
                case 3:
                    Properties.Settings.Default.User3TotalWritingTime = TotalWritingTime;
                    break;
                case 4:
                    Properties.Settings.Default.User4TotalWritingTime = TotalWritingTime;
                    break;
                case 5:
                    Properties.Settings.Default.User5TotalWritingTime = TotalWritingTime;
                    break;
                case 6:
                    Properties.Settings.Default.User6TotalWritingTime = TotalWritingTime;
                    break;
                case 7:
                    Properties.Settings.Default.User7TotalWritingTime = TotalWritingTime;
                    break;
                case 8:
                    Properties.Settings.Default.User8TotalWritingTime = TotalWritingTime;
                    break;
                case 9:
                    Properties.Settings.Default.User9TotalWritingTime = TotalWritingTime;
                    break;
            }
        }
    }
}
