using Discord;
using Discord.Commands;
using Discord.Audio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;


namespace Alpha_Test_42_Bot
{
    class MyBot
    {
        static int NbUsers = Properties.Settings.Default.NbUtilisateurs;
        List <Users> UsersId = new List <Users>();
        int SleepTime = 5;
        public IAudioClient VoiceClient;

        bool PlayingLoups = false; int StartingLoups = 0;
        Server Serv;
        Channel Chan, VillageoisChan, LoupsChan;
        int Etape;

        List<Loup_Garou_Players> Players = new List<Loup_Garou_Players>();
        

        private void Init()
        {
            for (int i = 0; i < NbUsers; i++)
            {
                UsersId.Add(CreateList(i));
            }

            Console.WriteLine(NbUsers);

            //Initialisation des Channels et Users
            
        }

        DiscordClient discord;
        CommandService commands;
        bool repeat = false;

        public MyBot()
        {
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Debug;
                x.LogHandler = Log;
            })
            .UsingAudio(x =>
            {
                x.Mode = AudioMode.Outgoing;
                //Console.WriteLine(discord.FindServers("Dev Test Chat").FirstOrDefault().Name);//.FirstOrDefault().VoiceChannels.FirstOrDefault().Name);
            })
            .UsingCommands(x =>
            {
                x.AllowMentionPrefix = true;
                x.PrefixChar = '!';
            });
            

            /*discord.Ready += async (s, e) =>
            {
                Console.WriteLine(discord.Servers.Any());
                discord.SetGame("42");
            };*/

            Init();
            

            commands = discord.GetService<CommandService>();
            

            FortyTwo();
            Prim();
            Pascal();
            MessageRecu();

            commands.CreateCommand("get")
                .Parameter("Variable", ParameterType.Required)
                .Do(async (e) =>
                {
                    try
                    {

                    }
                    catch (ArgumentException)
                    {

                        throw;
                    }
                    await e.Channel.SendMessage("42");
                });

            commands.CreateCommand("help")
                .Parameter("HelpFunc", ParameterType.Optional)
                .Do(async (e) =>
                {
                    string Func = e.GetArg("HelpFunc").ToLower();
                    if (Func.Length > 0 && !(Func[0] > 47 && Func[0] < 58) || Func == "42" || Func == "1337")
                    {
                        if (Func[0] == '!')
                            Func = Func.Remove(0, 1);
                        switch (Func)
                        {
                            case "help":
                                await e.Channel.SendMessage("!help : (+ 1 paramètre [numéro de page] optionnel) Affiche une page de la liste des fonctions d'*Alpha test 42* disponibles");
                                break;
                            case "42":
                                await e.Channel.SendMessage("!42 : (+ 1 paramètre [nombre]) Affiche *x* fois \"42\", puis la Grande Réponse");
                                break;
                            case "leet":
                            case "1337":
                                await e.Channel.SendMessage("!1337 / !leet (+ 1 paramètre [phrase] optionnel) Affiche \"Leet\", puis votre message en *Leet speak*");
                                break;
                            case "pascal":
                            case "sierp":
                                await e.Channel.SendMessage("!sierp / !pascal : Affiche un triangle de Sierpinski de très grande beauté");
                                break;
                            case "hello":
                                await e.Channel.SendMessage("!hello : *Alpha test 42* vous répond avec grande dignité");
                                break;
                            case "repeat":
                                await e.Channel.SendMessage("!repeat : Active le mode \"Repeat\" : *Alpha test 42* répète ce que tout le monde dit");
                                break;
                            case "time":
                                await e.Channel.SendMessage("!time : Affiche la date et l'heure (de mon PC, donc pas forcément très à l'heure)");
                                break;
                            case "caca":
                                await e.Channel.SendMessage("!caca / !poop / !shitty / !prout : Prout");
                                break;

                            default:
                                await e.Channel.SendMessage("Commande non trouvée (maybe *coming soon*)");
                                break;
                        }
                    }
                    else
                    {
                        if (Func.Length == 0)
                            Func = "1";

                        await e.Channel.SendMessage("`Commandes disponibles :`\n`### Page " + Func + " ###`");

                        switch (Func)
                        {
                            case "1":                       // Nombre de commandes par page max = 8
                                await e.Channel.SendMessage("```\n- !help\n- !1337\n- !42\n- !hello\n- !prim\n- !repeat\n- !sierp\n- !time\n```");
                                break;
                            case "2":
                                await e.Channel.SendMessage("```\n- !caca\n- *More coming soon*```");
                                break;
                            default:
                                await e.Channel.SendMessage("`Error 404: Page not Found`");
                                break;
                        }
                    }
                });



            commands.CreateCommand("hello")
                .Do(async (e) =>
                {
                    //await e.Channel.SendMessage("Yes, my Lord");
                    string userVC = e.User.VoiceChannel.Name;

                    /*var voiceChannel = discord.FindServers(e.Server.Name).FirstOrDefault().FindChannels(userVC).FirstOrDefault();
                    Console.WriteLine(voiceChannel.Name);
                    VoiceClient = await discord.GetService<AudioService>()
                            .Join(voiceChannel); */

                    /*Console.WriteLine(discord.PrivateChannels.Count());
                    for (int i = 0; i < discord.PrivateChannels.Count(); i++)
                    {
                        Console.WriteLine(discord.PrivateChannels.ElementAt(i).Name);
                        if (discord.PrivateChannels.ElementAt(i).Name == "@SiennaMuffin219#4737")
                        {
                            await discord.PrivateChannels.ElementAt(i).SendMessage("42");
                        }
                    }*/
                    

                    try
                    {
                        var voiceChannel = e.Server.FindUsers(e.User.Name).FirstOrDefault().VoiceChannel;
                        //await e.Channel.SendMessage(voiceChannel.Name);
                        Console.WriteLine(voiceChannel.Name);
                        VoiceClient = await discord.GetService<AudioService>().Join(voiceChannel);
                        await e.Channel.SendMessage(VoiceClient.Channel.Type.ToString());
                        await e.Channel.SendMessage($"👌");
                    }
                    catch (Exception a)
                    {
                        await e.Channel.SendMessage(a.Message);
                    }
                });


            commands.CreateCommand("blabla")
                .Do(async (e) =>
                {
                    string str = "blabla";
                    while (str.Length < 1980)
                        str += "blabla";
                    for (int i = 0; i < 50; i++)
                    {
                        await e.Channel.SendMessage(str);
                        Thread.Sleep(1100);
                    }
                });


            commands.CreateCommand("caca")
                .Alias("poop", "shitty", "proot")
                .Do(async (e) =>
                {
                    await e.Channel.SendTTSMessage(":poop:");
                });


            commands.CreateCommand("repeat")
                .Parameter("Param", ParameterType.Optional)
                .Do(async (e) =>
                {
                    if (e.GetArg("Param") == "?")
                        await e.Channel.SendMessage("Repeat mode is *" + (repeat ? "activated*" : "deactivated*"));
                    else
                    {
                        repeat = !repeat;
                        if (repeat)
                            await e.Channel.SendMessage("*Repeat Mode Activated!*");
                        else
                            await e.Channel.SendMessage("*Repeat Mode " + (repeat ? "A" : "Dea") + "ctivated*");
                    }
                });



            commands.CreateCommand("1337")
                .Alias("leet")
                .Parameter("Sentence", ParameterType.Optional)
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("Leet");
                    await e.Channel.SendMessage(ToLeet(e.GetArg("Sentence")));
                });


            commands.CreateCommand("time")
                .Description("Affiche la date et l'heure (de mon PC, donc pas forcément très à l'heure)")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage(DateTime.Now.ToString("dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo));
                    await e.Channel.SendMessage(DateTime.Now.ToString("HH:mm:ss", DateTimeFormatInfo.InvariantInfo));
                });


            commands.CreateCommand("test")
                .Do(async (e) =>
                {
                    Random rand = new Random(DateTime.Now.Millisecond);
                    for (int i = 0; i < 5; i++)
                        await e.Channel.SendMessage(rand.Next(2).ToString());
                });


            commands.CreateCommand("disconnect")
                .Alias("dis", "dc")
                .Do(async (e) =>
                {
                    Console.WriteLine("Disconnecting...");
                    await e.Channel.SendMessage("Disconnecting...");
                    discord.SetStatus(UserStatus.Invisible);
                    if (e.User.Name == "SiennaMuffin219")
                    {
                        Thread.Sleep(500);
                        await discord.Disconnect();
                    }
                });

            commands.CreateCommand("Ah")
                .Do(async (e) =>
                {
                    for (int i = 66; i < 91; i++)
                        await e.Channel.SendMessage(((char)i).ToString() + "h");
                });

            commands.CreateCommand("DM")
                .Do(async (e) =>
                {
                    float a = 0, b = 1, m;
                    while (b-a > 0.1)
                    {
                        m = (a + b) / 2;
                        if ((2 + 2 * Math.Log(m)) / (m * m) < 1)
                            a = m;
                        else
                            b = m;
                        await e.Channel.SendMessage(a.ToString() + " " + b.ToString());
                    }
                    await e.Channel.SendMessage("Sortie : " + a.ToString() + " " + b.ToString());
                });

            commands.CreateCommand("salons")
                .Do(async (e) =>
                {
                    try
                    {
                        if (e.Message.User.Name == "SiennaMuffin219")
                        {
                            await VillageoisChan.Delete();
                            await LoupsChan.Delete();
                        }
                    }
                    catch (Exception a)
                    {
                        await e.Channel.SendMessage(a.Message);
                    }
                });

            commands.CreateCommand("Loup")
                .Do(async (e) =>
                {
                    Chan = e.Channel;
                    Serv = e.Server;
                    await Chan.SendMessage("Commencer une partie de Loups Garous ?");
                    StartingLoups = 2;
                });

            commands.CreateCommand("info")
                .Do(async (e) =>
                {
                    try
                    {
                        if (e.Channel.IsPrivate && PlayingLoups && Players.Any(x => x.Name == e.User.Name))
                        {
                            await e.Channel.SendMessage(Players.Find(x => x.Name == e.User.Name).SendInfo());
                        }
                    }
                    catch (Exception a)
                    {
                        await e.Channel.SendMessage(a.Message);
                    }
                });


            XPCommands();

            /*discord.UserJoined += async (s, e) =>
            {
                Console.WriteLine(42);
                await e.User.Server.TextChannels.FirstOrDefault().SendMessage("42");
                Console.WriteLine(4242);
            };*/

            discord.RoleCreated += async (s, e) =>
            {
                await e.Server.DefaultChannel.SendMessage("42");
            };
            

            /*discord.ServerAvailable += async (s, e) =>
            {
                await e.Server.DefaultChannel.SendMessage("Héhé, me revoilà !");
            };*/


            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MjM0NDU0NTgyNjU3NDE3MjMz.Ctu_5w.KHdULfybSExIoh-u4lxadpmrYVU", TokenType.Bot);
                //discord.SetStatus(UserStatus.Invisible);
                discord.SetGame("Être un MJ");
                //Chan = discord.PrivateChannels.ToList().Find(x => x.Name == "@SiennaMuffin219#4737");
                //Console.WriteLine(Chan.Name);
            });
        }
        

        private void Loups(Message e)
        {
            int Etape2;
            do
            {
                Etape2 = Etape;
                switch (Etape)
                {
                    case 0:     // Recherche de joueurs
                        if (e.Text.ToLower().Contains("joue") && e.Channel == Chan && !Players.Any(x => x.Name == e.User.Name))
                        {
                            Players.Add(new Loup_Garou_Players(e.User));
                            Chan.SendMessage("Recherche de participants : " + Players.Count.ToString() + (Players.Count > 1 ? " joueurs" : " joueur"));
                        }
                        if (Players.Any(x => x.Name == e.User.Name) && e.Text.ToLower().Contains("go") && e.Channel == Chan)
                        {
                            Etape = 1;
                        }
                        break;

                    case 1:     // Attente pour que tous les joueurs soient prêts
                        if (Players.Any(x => x.Name == e.User.Name) && e.Text.ToLower().Contains("no go") && e.Channel == Chan)
                            Players.Find(x => x.Name == e.User.Name).IsReady = false;

                        if (Players.Any(x => x.Name == e.User.Name) && e.Text.ToLower().Contains("go") && e.Channel == Chan)
                            Players.Find(x => x.Name == e.User.Name).IsReady = true;

                        Chan.SendMessage(Players.Count(x => x.IsReady == true).ToString() + " joueur" + (Players.Count(x => x.IsReady == true) > 1 ? "s " : " ") + "prêt" + (Players.Count(x => x.IsReady == true) > 1 ? "s " : " ") + "sur " + Players.Count);
                        if (!Players.Any(x => x.IsReady == false))
                        {
                            e.Channel.SendMessage("Tous les joueurs sont prêts, lancement de la partie en cours.");
                            List<String> Roles = new List<string>();
                            try { Roles = Loup_Garou_Players.CreateRoles(Players.Count); }
                            catch (Exception a) { e.Channel.SendMessage(a.Message); }
                            /*foreach (var i in Players)
                                i.SetRole(Roles);*/
                            try { Players.ForEach(x => x.SetRole(Roles)); }
                            catch (Exception a) { e.Channel.SendMessage(a.Message); }
                            Chan.SendMessage("!salons");
                        }
                        break;

                    case 2:

                    case 3:     // Voleur
                        if (e.Channel == Players.Find(x => x.Role == "Voleur").Utilisateur.PrivateChannel)
                        {
                            var vol = Players.Find(x => x.Role == "Voleur");
                            if (e.Text.ToLower().Contains(Loup_Garou_Players.RolesRestants[0].ToLower()) || e.Text.ToLower().Contains(Loup_Garou_Players.RolesRestants[1].ToLower()))
                            {
                                vol.SetRole(Loup_Garou_Players.RolesRestants, Loup_Garou_Players.RolesRestants.FindIndex(x => e.Text.ToLower().Contains(x)));
                                vol.SendRole();
                                VillageoisChan.SendMessage("Le voleur se rendort.");
                                Etape = 3;
                            }
                            else
                            {
                                if (e.Text.ToLower().Contains("rester"))
                                {
                                    VillageoisChan.SendMessage("Le voleur se rendort.");
                                    Etape = 3;
                                }
                                else
                                    vol.Utilisateur.PrivateChannel.SendMessage("Aucun rôle ne correspond à votre message.");
                            }
                        }
                        else
                        {
                            if (e.Channel == VillageoisChan)
                            {
                                Random rand = new Random(DateTime.Now.Millisecond);
                                if (rand.Next(2) == 0)
                                    e.Edit("Chut ! C'est la nuit !");
                                else
                                    e.Edit("J'ai dit \"C'est la nuit\" !");
                                Thread.Sleep(1000);
                                e.Delete();
                            }
                        }
                        break;

                    case 4:
                        if (Players.Any(x => x.Role == "Cupidon"))
                        {
                            Thread.Sleep(750);
                            VillageoisChan.SendMessage("Cupidon se réveille");
                            var cup = Players.Find(x => x.Role == "Cupidon");
                            cup.Utilisateur.PrivateChannel.SendMessage("Vous êtes Cupidon : vous devez choisir 2 personnes pour les faire tomber amoureuses jusqu'à la fin de la partie.\n");
                            string personnes = "";
                            Players.ForEach(x => personnes += x.Name + "\n");
                            cup.Utilisateur.PrivateChannel.SendMessage(personnes);
                            cup.Utilisateur.PrivateChannel.SendMessage("Choisissez 2 personnes en envoyant leur nom l\'un après l\'autre.");
                            Etape = 4;
                        }
                        else
                        {
                            Etape = 5;
                        }
                        break;

                    case 5:
                        if (e.Channel == Players.Find(x => x.Role == "Cupidon").Utilisateur.PrivateChannel)
                        {
                            var cup = Players.Find(x => x.Role == "Cupidon");
                            if (Players.Any(x => x.Name.ToLower() == e.Text.ToLower()))
                            {
                                if (Players.Any(x => x.IsAmoureux == 2))
                                {
                                    var amour1 = Players.Find(x => x.Name.ToLower() == e.Text.ToLower()); amour1.IsAmoureux = 1;
                                    var amour2 = Players.Find(x => x.IsAmoureux == 2); amour2.IsAmoureux = 1;
                                    amour1.Utilisateur.PrivateChannel.SendMessage("Vous avez été choisi par Cupidon pour brûler d'un amour fou avec " + amour2.Name + " !\nVotre but est à présent de survivre avec lui/elle.");
                                    amour2.Utilisateur.PrivateChannel.SendMessage("Vous avez été choisi par Cupidon pour brûler d'un amour fou avec " + amour1.Name + " !\nVotre but est à présent de survivre avec lui/elle\nS'il/si elle meurt, vous mourez avec lui/elle.");
                                    cup.Utilisateur.PrivateChannel.SendMessage("Vous avez fait votre travail, les amoureux se sont rencontrés et ils ne se quitteront plus.");
                                    Etape = 5;
                                    VillageoisChan.SendMessage("Cupidon se rendort.");
                                }
                                else
                                {
                                    Players.Find(x => x.Name.ToLower() == e.Text.ToLower()).IsAmoureux = 2;
                                }
                            }
                            else
                            {
                                cup.Utilisateur.PrivateChannel.SendMessage("Aucun nom ne correspond à votre message.");
                            }
                        }
                        else
                        {
                            if (e.Channel == VillageoisChan)
                            {
                                Random rand = new Random(DateTime.Now.Millisecond);
                                if (rand.Next(2) == 0)
                                    e.Edit("Chut ! C'est la nuit !");
                                else
                                    e.Edit("J'ai dit \"C'est la nuit\" !");
                                Thread.Sleep(1000);
                                e.Delete();
                            }
                        }
                        break;

                    case 6:
                        if (Players.Any(x => x.Role == "Voyante"))
                        {
                            Thread.Sleep(750);
                            VillageoisChan.SendMessage("La voyante se réveille");
                            var voy = Players.Find(x => x.Role == "Voyante");
                            voy.Utilisateur.PrivateChannel.SendMessage("Vous êtes la voyante : vous pouvez découvrir le rôle d'un habitant.\n");
                            string personnes = "";
                            Players.ForEach(x => personnes += x.Name + "\n");
                            voy.Utilisateur.PrivateChannel.SendMessage(personnes);
                            voy.Utilisateur.PrivateChannel.SendMessage("Choisissez 1 personne en envoyant son nom.");
                            Etape = 6;
                        }
                        else
                        {
                            Etape = 7;
                        }
                        break;

                    case 7:
                        if (e.Channel == Players.Find(x => x.Role == "Voyante").Utilisateur.PrivateChannel)
                        {
                            var voy = Players.Find(x => x.Role == "Voyante");
                            if (Players.Any(x => x.Name.ToLower() == e.Text.ToLower()))
                            {
                                voy.Utilisateur.SendMessage("Votre boule de cristal magique (ou électronique) vous permet de découvrir le rôle de " + Players.Find(x => x.Name.ToLower() == e.Text.ToLower()).Name + " : il/elle est " + Players.Find(x => x.Name.ToLower() == e.Text.ToLower()).Role);
                                if (Players.Find(x => x.Name.ToLower() == e.Text.ToLower()).IsDead)
                                    voy.Utilisateur.SendMessage("Et d'ailleurs il est mort");
                                Etape = 7;
                                VillageoisChan.SendMessage("La voyante se rendort.");
                            }
                            else
                            {
                                voy.Utilisateur.PrivateChannel.SendMessage("Aucun nom ne correspond à votre message.");
                            }
                        }
                        else
                        {
                            if (e.Channel == VillageoisChan)
                            {
                                Random rand = new Random(DateTime.Now.Millisecond);
                                if (rand.Next(2) == 0)
                                    e.Edit("Chut ! C'est la nuit !");
                                else
                                    e.Edit("J'ai dit \"C'est la nuit\" !");
                                Thread.Sleep(1000);
                                e.Delete();
                            }
                        }
                        break;

                    case -1:
                        VillageoisChan.SendMessage("Hum... Y a un petit problème on dirai, je vais demander à @SiennaMuffin219#4737 ce qu'il se passe et je reviens.");
                        break;
                }
            } while (Etape != Etape2);
        }



        private void MessageRecu()
        {
            discord.MessageReceived += async (s, e) =>
            {
                
                if (PlayingLoups && !e.Message.IsAuthor)
                    Loups(e.Message);
                
                if (StartingLoups == 1)
                {
                    if (!e.Message.IsAuthor && e.Channel == Chan)
                    {
                        if (e.Message.Text.Contains("Oui"))
                        {
                            await Chan.SendMessage("*Let's go!!*");
                            StartingLoups = 0; PlayingLoups = true;
                            Etape = 0;
                        }
                        else
                        {
                            StartingLoups = 0;
                        }
                    }
                }
                if (StartingLoups == 2)
                    StartingLoups = 1;



                if (e.Message.IsAuthor && e.Message.Text == "!salons")
                {
                    try
                    {
                        VillageoisChan = await Serv.CreateChannel("Le_Village", ChannelType.Text);
                        await e.Channel.SendMessage(VillageoisChan.Mention);
                        LoupsChan = await Serv.CreateChannel("La_Nuit", ChannelType.Text);
                        await e.Channel.SendMessage(LoupsChan.Mention);
                        ChannelPermissions allow = new ChannelPermissions(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
                        ChannelPermissions deny = new ChannelPermissions(true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                        foreach (var Joue in Players.FindAll(x => x.Role != "Loup Garou"))
                        {
                            await LoupsChan.AddPermissionsRule(Joue.Utilisateur, allow, deny);
                        }
                    }
                    catch (Exception a)
                    {
                        Etape = -1;
                        await e.Channel.SendMessage("1" + a.Message);
                    }

                    try
                    {
                        Players.ForEach(x => x.SetChannels(VillageoisChan, LoupsChan));

                        Players.ForEach(async (x) => { if (x.Utilisateur.PrivateChannel == null) await x.Utilisateur.SendMessage(" "); });
                        Players.ForEach(x => x.SendRole());
                        await LoupsChan.SendMessage("Si vous avez accès à ce salon, c'est que vous êtes parmi les loups.\nAttendez la nuit pour choisir votre prochaine victime et faites votre possible pour ne pas vous faire démasquer la journée.\nBonne chance.");
                        await VillageoisChan.SendMessage("Ce salon est la place du village, vous y avez accès la journée, pour discuter et débattre de choses et d'autres entre villageois.\nMais depuis quelques temps un malheur s'abat sur votre village : certain villageois les plus dignes de confiance se réveillent la nuit et se transforment en loups garous, pour tuer d'autres villageois !\nFaites votre possible pour éliminer ceux qui pourraient vous trahir d'une nuit à l'autre et utilisez votre pouvoir (si vous avez la chance d'en avoir un) pour trouver qui dit la vérité et qui ment.\nEt souvenez-vous, la confiance est une chose dont il vaut parfois mieux se douter :unamused:");

                        Thread.Sleep(5000);
                        await VillageoisChan.SendMessage("C'est à présent la nuit, tous les habitants s'endorment... :sleeping:");
                    }
                    catch (Exception a)
                    {
                        Etape = -1;
                        await e.Channel.SendMessage("2" + a.Message);
                    }

                    try
                    {
                        if (Players.Any(x => x.Role == "Voleur"))
                        {
                            Etape = 2;
                            Thread.Sleep(750);

                            await VillageoisChan.SendMessage("Le voleur se réveille.");
                            var vol = Players.Find(x => x.Role == "Voleur");
                            await vol.Utilisateur.PrivateChannel.SendMessage("Vous êtes le voleur : cela vous permet de prendre connaissance des rôles restants au début de la partie (c'est à dire maintenant) pour en changer.\nVoici les 2 rôles restants :\n" + Loup_Garou_Players.RolesRestants[0] + "\n" + Loup_Garou_Players.RolesRestants[1]);
                            if (Loup_Garou_Players.RolesRestants.Count(x => x == "Loup Garou") == 2)
                            {
                                await vol.Utilisateur.PrivateChannel.SendMessage("Les 2 cartes restantes étant des loups garous, vous êtes obligé de vous transformer en l'un d'eux.");
                                vol.SetRole(Loup_Garou_Players.RolesRestants, 0);
                                Thread.Sleep(5000);
                                vol.SendRole();
                                await VillageoisChan.SendMessage("Le voleur se rendort.");
                                Etape = 3;
                            }
                            else
                            {
                                await vol.Utilisateur.PrivateChannel.SendMessage("Vous avez le choix entre rester le voleur (votre pouvoir n'aura alors plus d'utilité pour le reste de la partie) en envoyant \"Rester\", ou bien prendre un des deux rôles restants en envoyant son nom.");
                            }
                        }
                        else
                            Etape = 3;
                    }
                    catch (Exception a)
                    {
                        Etape = -1;
                        await e.Channel.SendMessage("3" + a.Message);
                    }
                }

                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss", DateTimeFormatInfo.InvariantInfo) + " - " + e.User + "  on  " + e.Message.Channel);
                Console.WriteLine("         \"" + e.Message.Text + "\"");

                
                int index = UsersId.FindIndex(x => x.Name == e.User.ToString());
                Console.WriteLine(index);
                if (index == -1)
                {
                    Console.WriteLine("Adding {0} as User{1}", e.User.ToString(), NbUsers);
                    //UsersId.Add(CreateUser(NbUsers, e.User.ToString()));
                    UsersId.Add(new Users(NbUsers, e.User.ToString()));
                    NbUsers++;
                    Properties.Settings.Default.NbUtilisateurs++;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Console.WriteLine(e.User.ToString() + " is already saved as User" + index);
                    UsersId[index].MessageEnvoye(e.Message.Text.Length, 5);
                }

                /*if (e.User.Name == "Inferno")
                {
                    if (e.Message.Text[0] == '!' && e.Message.Text[1] == 't')
                        SleepTime = e.Message.Text.Length - 2;
                    Thread.Sleep(1000 * SleepTime);
                    await e.Message.Delete();
                }*/

                /*if (!e.Message.IsAuthor)
                    await e.Channel.SendMessage(e.User.);*/

                /*string mess = e.Message.Text.ToLower();
                for (int i = 0; i < mess.Length - 3; i++)
                {
                    if (mess[i] == 'd' && mess[i + 1] == 'i')
                    {
                        if (mess.Length > i + 4)
                        {
                            if ((mess[i + 2] == 's' || mess[i + 2] == 't') && mess[i + 3] == ' ')
                            {
                                await e.Channel.SendTTSMessage(mess.Substring(i + 4));
                            }
                            else
                            {
                                await e.Channel.SendTTSMessage(mess.Substring(i + 2));
                            }
                        }
                    }
                }*/

                if (!e.Message.IsAuthor && repeat)
                    await e.Channel.SendMessage(e.Message.Text);
            };
        }
        
        

        private void XPCommands()
        {
            commands.CreateCommand("info")
                .Parameter("User", ParameterType.Required)
                .Do(async (e) =>
                {
                    int Id;
                    if (e.GetArg("User") == "me")
                        Id = UsersId.FindIndex(x => x.Name == e.User.ToString());
                    else
                        Id = Convert.ToInt32(e.GetArg("User"));
                    await e.Channel.SendMessage(
                        "```   =====  " + UsersId[Id].Name.ToString() + "  =====" +
                        "\n Nombre de caractères envoyés : " + UsersId[Id].TotalChar.ToString() +
                        "\n Nombre de messages : " + (UsersId[Id].TotalWritingTime / 5).ToString() + "```");
                });

            commands.CreateCommand("listall")
                .Do(async (e) =>
                {
                    for (int i = 0; i < NbUsers; i++)
                    {
                        await e.Channel.SendMessage(
                        "```   =====  " + UsersId[i].Name + "  =====" +
                        "\n Saved as User" + UsersId[i].i +
                        "\n Nombre de caractères envoyés : " + UsersId[i].TotalChar.ToString() +
                        "\n Nombre de messages : " + (UsersId[i].TotalWritingTime/5).ToString() + "```");
                    }
                });
        }

        private void FortyTwo()
        {
            commands.CreateCommand("42")
                .Parameter("max", ParameterType.Required)
                .Do(async (e) =>
                {
                    for (int i = 0; i < Convert.ToInt32(e.GetArg("max")); i++)
                        await e.Channel.SendMessage("42");
                    await e.Channel.SendMessage("The answer to life, the Universe and everything");
                });
        }

        private void Pascal()
        {
            commands.CreateCommand("sierp")
                .Alias("pascal")
                .Do(async (e) =>
                {
                    int max = 8;
                    bool[] list1 = new bool[max];
                    list1[0] = true;
                    for (int i = 1; i < max; i++)
                        list1[i] = false;

                    string spaces = ".";
                    for (int s = 0; s < max; s++)
                        spaces += " ";

                    await e.Channel.SendMessage("``" + spaces + "#``");
                    spaces = spaces.Remove(spaces.Length - 1);

                    for (int i = 1; i <= max - 1; i++)
                    {
                        bool[] list2 = new bool[max];
                        list2[0] = true;
                        for (int j = 1; j < max; j++)
                            list2[j] = false;

                        string str = " #";

                        int spa = spaces.Length;
                        spaces = ".";
                        for (int x = 0; x < spa - 2; x++)
                            spaces += " ";

                        for (int j = 1; j < max; j++)
                        {
                            if ((list1[j - 1] && !list1[j]) || (!list1[j - 1] && list1[j]))
                            {
                                list2[j] = true;
                                str += " #";
                            }
                            else
                            {
                                str += "  ";
                            }
                        }


                        await e.Channel.SendMessage("``" + spaces + str + "``");

                        for (int j = 0; j < max; j++)
                            list1[j] = list2[j];
                    }
                });
        }

        private void Prim()
        {
            commands.CreateCommand("prim")
                .Parameter("max", ParameterType.Required)
                .Do(async (e) =>
                {
                    string prim = "2, 3";
                    for (int i = 5; i < Convert.ToInt32(e.GetArg("max")); i += 2)
                    {
                        bool b = true;
                        for (int j = 3; j * j <= i; j += 2)
                        {
                            if (i % j == 0)
                            {
                                b = false;
                                break;
                            }
                        }
                        if (b)
                        {
                            if (prim.Length != 0)
                                prim += ", ";
                            prim += i.ToString();
                            if (prim.Length > 1750)
                            {
                                await e.Channel.SendMessage(prim);
                                prim = "";
                            }
                        }
                    }
                    await e.Channel.SendMessage(prim);
                });
        }

        private string ToLeet(string str)
        {
            string phrase = "";
            for (int i = 0; i < str.Length; i++)
            {
                switch (str[i])
                {
                    case 'O':
                    case 'o':
                        phrase += '0';
                        break;
                    case 'I':
                    case 'i':
                    case 'L':
                    case 'l':
                        phrase += '1';
                        break;
                    case 'Z':
                    case 'z':
                        phrase += '2';
                        break;
                    case 'E':
                    case 'e':
                        phrase += '3';
                        break;
                    case 'A':
                    case 'a':
                        phrase += '4';
                        break;
                    case 'S':
                    case 's':
                        phrase += '5';
                        break;
                    case 'G':
                    case 'g':
                        phrase += '6';
                        break;
                    case 'T':
                    case 't':
                        phrase += '7';
                        break;
                    case 'B':
                    case 'b':
                        phrase += '8';
                        break;
                    case 'C':
                    case 'c':
                        phrase += '<';
                        break;
                    case 'H':
                    case 'h':
                        phrase += "|-|";
                        break;
                    case 'V':
                    case 'v':
                        phrase += "\\/";
                        break;
                    case 'X':
                    case 'x':
                        phrase += "><";
                        break;

                    default:
                        phrase += str[i];
                        break;
                }
            }

            return phrase;
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss", DateTimeFormatInfo.InvariantInfo) + " - " + e.Message);
        }

        private Users CreateList(int i)
        {
            switch (i)
            {
                case 0:
                    Users utilisateur0 = new Users(i, Properties.Settings.Default.User0Name, Properties.Settings.Default.User0XP, Properties.Settings.Default.User0TotalChar, Properties.Settings.Default.User0TotalWritingTime);
                    return utilisateur0;
                case 1:
                    Users utilisateur1 = new Users(i, Properties.Settings.Default.User1Name, Properties.Settings.Default.User1XP, Properties.Settings.Default.User1TotalChar, Properties.Settings.Default.User1TotalWritingTime);
                    return utilisateur1;
                case 2:
                    Users utilisateur2 = new Users(i, Properties.Settings.Default.User2Name, Properties.Settings.Default.User2XP, Properties.Settings.Default.User2TotalChar, Properties.Settings.Default.User2TotalWritingTime);
                    return utilisateur2;
                case 3:
                    Users utilisateur3 = new Users(i, Properties.Settings.Default.User3Name, Properties.Settings.Default.User3XP, Properties.Settings.Default.User3TotalChar, Properties.Settings.Default.User3TotalWritingTime);
                    return utilisateur3;
                case 4:
                    Users utilisateur4 = new Users(i, Properties.Settings.Default.User4Name, Properties.Settings.Default.User4XP, Properties.Settings.Default.User4TotalChar, Properties.Settings.Default.User4TotalWritingTime);
                    return utilisateur4;
                case 5:
                    Users utilisateur5 = new Users(i, Properties.Settings.Default.User5Name, Properties.Settings.Default.User5XP, Properties.Settings.Default.User5TotalChar, Properties.Settings.Default.User5TotalWritingTime);
                    return utilisateur5;
                case 6:
                    Users utilisateur6 = new Users(i, Properties.Settings.Default.User6Name, Properties.Settings.Default.User6XP, Properties.Settings.Default.User6TotalChar, Properties.Settings.Default.User6TotalWritingTime);
                    return utilisateur6;
                case 7:
                    Users utilisateur7 = new Users(i, Properties.Settings.Default.User7Name, Properties.Settings.Default.User7XP, Properties.Settings.Default.User7TotalChar, Properties.Settings.Default.User7TotalWritingTime);
                    return utilisateur7;
                case 8:
                    Users utilisateur8 = new Users(i, Properties.Settings.Default.User8Name, Properties.Settings.Default.User8XP, Properties.Settings.Default.User8TotalChar, Properties.Settings.Default.User8TotalWritingTime);
                    return utilisateur8;
                case 9:
                    Users utilisateur9 = new Users(i, Properties.Settings.Default.User9Name, Properties.Settings.Default.User9XP, Properties.Settings.Default.User9TotalChar, Properties.Settings.Default.User9TotalWritingTime);
                    return utilisateur9;
                default:
                    Users utilisateurDefault = new Users(i, "Default", 0, 0, 0);
                    return utilisateurDefault;
            }
        }

        private Users CreateUser(int i, string UserName)
        {
            Users utilisateur = new Users(i, UserName);

            /*switch (i)
            {
                case 0:
                    Properties.Settings.Default.User0Name = UserName; Properties.Settings.Default.User0TotalWritingTime = 0;
                    Properties.Settings.Default.User0TotalChar = 0; Properties.Settings.Default.User0TotalWritingTime = 0;
                    break;
                case 1:
                    Properties.Settings.Default.User1Name = UserName; Properties.Settings.Default.User1TotalWritingTime = 0;
                    Properties.Settings.Default.User1TotalChar = 0; Properties.Settings.Default.User1TotalWritingTime = 0;
                    break;
                case 2:
                    Properties.Settings.Default.User2Name = UserName; Properties.Settings.Default.User2TotalWritingTime = 0;
                    Properties.Settings.Default.User2TotalChar = 0; Properties.Settings.Default.User2TotalWritingTime = 0;
                    break;
                case 3:
                    Properties.Settings.Default.User3Name = UserName; Properties.Settings.Default.User3TotalWritingTime = 0;
                    Properties.Settings.Default.User3TotalChar = 0; Properties.Settings.Default.User3TotalWritingTime = 0;
                    break;
                case 4:
                    Properties.Settings.Default.User4Name = UserName; Properties.Settings.Default.User4TotalWritingTime = 0;
                    Properties.Settings.Default.User4TotalChar = 0; Properties.Settings.Default.User4TotalWritingTime = 0;
                    break;
                case 5:
                    Properties.Settings.Default.User5Name = UserName; Properties.Settings.Default.User5TotalWritingTime = 0;
                    Properties.Settings.Default.User5TotalChar = 0; Properties.Settings.Default.User5TotalWritingTime = 0;
                    break;
                case 6:
                    Properties.Settings.Default.User6Name = UserName; Properties.Settings.Default.User6TotalWritingTime = 0;
                    Properties.Settings.Default.User6TotalChar = 0; Properties.Settings.Default.User6TotalWritingTime = 0;
                    break;
                case 7:
                    Properties.Settings.Default.User7Name = UserName; Properties.Settings.Default.User7TotalWritingTime = 0;
                    Properties.Settings.Default.User7TotalChar = 0; Properties.Settings.Default.User7TotalWritingTime = 0;
                    break;
                case 8:
                    Properties.Settings.Default.User8Name = UserName; Properties.Settings.Default.User8TotalWritingTime = 0;
                    Properties.Settings.Default.User8TotalChar = 0; Properties.Settings.Default.User8TotalWritingTime = 0;
                    break;
                case 9:
                    Properties.Settings.Default.User9Name = UserName; Properties.Settings.Default.User9TotalWritingTime = 0;
                    Properties.Settings.Default.User9TotalChar = 0; Properties.Settings.Default.User9TotalWritingTime = 0;
                    break;
                default:
                    Properties.Settings.Default.User0Name = "Default";
                    break;
            }*/
            return utilisateur;
        }
    }
}
