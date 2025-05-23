using System;
using System.Collections.Generic;

internal class CreateCharacterScene : IScene
{
    public GameState SceneType => GameState.CreateCharacter;

    public GameState Run(int phase)
    {
        PrintDB.box1Data = box1Text.ContainsKey(phase) ? box1Text[phase] : new List<string>();
        PrintDB.box3Data = box3Text.ContainsKey(phase) ? box3Text[phase] : new List<string>();

        PrintUtil.CreateBox();
        string input = Console.ReadLine()?.Trim();


        switch (phase)
        {

            case 0:
                BGM_Player.Instance().Play_Job_Select_Loop();
                if (string.IsNullOrWhiteSpace(input))
                    return GameState.Pop;

                Player.Instance.Name = input;
                SceneManager.SetPhase(1);
                return GameState.Retry;

            case 1:

                if (input == "0")
                {
                    SceneManager.SetPhase(0);
                    return GameState.Retry;
                }

                switch (input)
                {

                    case "1":
                        Player.Instance.PlayerJob = (Player.Job)0;
                        Player.Instance.PlayerSkills.Add(Skill.HuntingInPlace); //    제자리 사냥
                        Player.Instance.PlayerSkills.Add(Skill.PostInInven); //    인벤에 글쓰기
                        Player.Instance.PlayerSkills.Add(Skill.WWE); //    WWE
                        Player.Instance.PlayerSkills.Add(Skill.Origin); //    오리진
                        break;
                    case "2":
                        Player.Instance.PlayerJob = (Player.Job)1;
                        Player.Instance.PlayerSkills.Add(Skill.AllInStock); //    올인
                        Player.Instance.PlayerSkills.Add(Skill.StrongHand); //    존버
                        Player.Instance.PlayerSkills.Add(Skill.PanicSell); //    패닉셀
                        Player.Instance.PlayerSkills.Add(Skill.CEONomalAtk); //    영웅호걸의 시간이다

                        break;
                    case "3":
                        Player.Instance.PlayerJob = (Player.Job)2;
                        Player.Instance.PlayerSkills.Add(Skill.Click); //    딸깍
                        Player.Instance.PlayerSkills.Add(Skill.ChatMute); //    채팅 금지

                        Player.Instance.PlayerSkills.Add(Skill.POTG); //    MVP각 ㅇㅈ //    MVP각 ㅇㅈ

                        Player.Instance.PlayerSkills.Add(Skill.Multikill);
                        break;
                    default:
                        return GameState.Retry;
                }
                BGM_Player.Instance().Play_SaveLoad_Loop();
                SceneManager.SetPhase(2);
                return GameState.Retry;

            case 2:
                if (input == "0")
                {
                    SceneManager.SetPhase(0);
                    return GameState.Retry;
                }

                switch (input)
                {

                    case "1":
                        SaveLoad.SavePlayer(Player.Instance, "player1.dat");
                        break;
                    case "2":
                        SaveLoad.SavePlayer(Player.Instance, "player2.dat");
                        break;
                    case "3":
                        SaveLoad.SavePlayer(Player.Instance, "player3.dat");
                        break;
                    default:
                        return GameState.Retry;
                }



                return GameState.Main;

            default:
                return GameState.Retry;
        }
    }

    public Dictionary<int, List<string>> box1Text = new Dictionary<int, List<string>>();
    public Dictionary<int, List<string>> box3Text = new Dictionary<int, List<string>>();

    Player player1 = SaveLoad.LoadPlayer("player1.dat");
    Player player2 = SaveLoad.LoadPlayer("player2.dat");
    Player player3 = SaveLoad.LoadPlayer("player3.dat");

    public CreateCharacterScene()
    {

        box1Text[0] = new List<string>()
            {
                "⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠛⡛⠛⡛⠻⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
                "⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢋⢡⠔⠄⢙⠭⣚⠪⠵⣒⠈⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
                "⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢁⠬⠒⡨⡁⢈⣊⣠⣬⣤⡈⠻⡄⡘⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
                "⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡃⠆⣰⣿⢿⠿⡿⣛⢯⣿⣿⣷⠀⠱⡔⠨⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
                "⣿⣿⣿⣿⣿⣿⣿⣿                  ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢐⠁⣺⣿⣽⡽⢿⠷⠟⠟⠛⠯⣳⠀⢝⡄⠙⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⡿",
                "⣿⣿⣿⣿⣿⣿⣿⣿  Hey You         ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⠨⠀⣪⡡⠀⡀⢼⡇⠀⣄⡩⣝⣾⡇⠣⡚⡄⠹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠿⠛⣛⣩⣡⣥⣶⣶",
                "⣿⣿⣿⣿⣿⣿⣿⣿   Finally Awake  ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠨⠀⢺⣾⡧⣡⢸⣿⣷⣬⣾⣿⣿⡇⢀⡸⡐⠈⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠿⠟⡛⣉⣥⣥⣶⠾⠿⠛⣛⣙⣭⣽⣿",
                "⣿⣿⣿⣿⣿⣿⣿⣿                  ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢐⠘⡜⢿⣿⢇⠿⠟⠻⣨⣻⣿⣟⡄⠳⠇⡃⠀⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⡿⠟⣛⣙⣩⣴⡶⡷⠟⡛⣋⣉⣥⣴⢶⠷⠟⢟⢋⣋⣍⣭",
                "⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢌⠄⠃⢪⣿⠛⣁⡠⡄⣉⠙⢿⣿⡃⢀⠀⢱⠈⡄⢹⣿⣿⣿⣿⣿⣿⢿⠿⢛⢛⣩⣩⡤⡶⣶⣿⣿⣭⢽⠶⠺⣚⣛⣭⣭⣭⢶⠶⠶⠟⠟⣛⣛⣙⠩⠁",
                "⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡅⢒⠀⠘⠏⢰⣥⠴⠔⢶⣷⠀⠙⢀⣮⠠⠈⡧⢑⠀⠛⠛⣋⢍⠥⠖⢒⣊⣍⣭⡶⠶⠞⡛⣙⣉⣤⠴⠖⠞⠛⣉⣉⠤⠤⠔⠔⠒⠃⠉⠉⠀⠀⠀⠀⣀",
                "⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⢸⢀⠠⠐⠘⠽⠢⢐⢉⠓⠀⢐⣼⡷⢨⠀⡇⡂⠀⣿⣦⣤⣀⠐⠙⠉⡡⠥⠔⠒⠚⢉⣉⠬⠤⠒⠒⠃⠋⠉⠀⠀⢀⢀⣀⣠⣠⣤⣴⣶⣶⣷⣿⣿⣿",
                "⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⠋⡀⠇⢐⠀⢢⡀⢀⠀⠀⠀⣠⡾⣿⡏⢐⠀⠂⠁⢠⡏⠽⣱⠟⠛⣶⣤⣀⠀⠀⢀⠀⠀⠀⠀⡀⢠⣤⣴⣴⣶⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
                "⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠟⢋⣠⣾⠂⡧⠁⠀⢠⠀⣿⡄⠠⣤⣷⣿⢱⡿⢀⠀⠀⠀⡀⡗⢀⡞⠁⢠⡼⠿⣻⣿⡷⡎⠀⢞⠂⠀⠄⡪⣰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⡿⠿⠿⠿⠟⠟⠛",
                "⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⣠⣶⣿⠁⢺⠀⢹⠀⡀⠸⡀⠘⠻⢷⢼⠿⠗⠋⡠⠎⠀⡜⠀⡸⠀⡗⠀⠈⠊⡠⡞⠎⠁⣀⣠⣤⣠⠀⠁⠀⠃⢚⢛⢛⣙⣙⣉⣍⣍⣬⣬⣤⣤⣦⣶⣶⣶⣾⣾⣿⣿",
                "⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠁⠾⣯⡉⠙⠆⠈⢇⠈⡇⢰⠀⠙⠢⣄⣀⡀⣀⡴⠎⠁⠀⡼⠀⠐⠀⡜⠀⠀⠠⡸⠊⠀⠰⠙⠉⠁⠁⠈⠑⠈⠟⢛⢛⢛⢛⡛⣛⣛⣫⣻⣿⣿⣿⣿⣟⣋⣏⣋⣍⣍⣍⣍",
                "⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣟⠀⡀⠀⠙⢦⡀⠀⢘⠀⠐⠀⡇⠀⠠⡀⠈⠉⠁⢀⠤⠀⡼⠁⠀⠀⡀⢀⠀⢀⠎⠀⠀⡀⢔⣐⡱⣘⢜⢌⠄⠈⠉⠉⠉⠉⠉⡉⡉⣉⣉⣉⣉⣉⣁⣁⣅⣡⣡⣥⣥⣥⣭⣬",
                "⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠄⢕⠢⡀⠀⠑⠁⠀⠀⠐⡀⠹⡀⠀⠈⠙⠚⠚⠉⢀⡞⠁⡐⠀⡀⠀⢔⠀⠠⠀⠠⡡⡢⡣⣒⢜⢌⢖⠅⠀⠐⠉⠉⠉⠍⡉⠍⡉⣉⢉⣉⣉⣙⣉⣋⣋⣋⣛⣙⣛⣛⣛⣻",
                "⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠠⡑⡕⢌⡢⢀⠀⠁⠨⠀⢢⠀⢣⠐⠈⠐⠤⠴⠜⠃⠀⡰⠁⢰⠁⠀⠐⠀⢀⢐⠕⡜⡜⡜⢔⢪⠜⢌⠄⠁⠀⡀⢈⢉⠁⡉⠉⡈⠈⠁⠁⠁⠁⠁⠁⠁⠉⠈⠁⠉⠈⠁⠉",
                "⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠟⠛⠙⠉⣁⢀⢪⠪⡱⡘⢔⠠⠀⠈⣃⠈⡆⠀⢣⠀⠀⢀⠐⢀⠄⢰⠃⠀⠅⠀⠀⠠⠀⠜⡌⡎⡎⡜⡜⡪⠲⡘⠔⠀⠀⠀⣎⢨⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣿⣾⣾⣾",
                "⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢿⠿⠛⢉⢁⣄⣤⠶⠚⡚⡉⠈⠀⡢⡓⢌⢎⠢⡣⠑⡀⠈⡂⠸⡄⠀⢳⠀⠐⠚⠋⢠⠃⠀⠀⢀⠸⠀⠀⠰⠸⡰⡱⡘⡪⡪⡜⡜⠬⡁⠐⠈⠀⡇⢼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",
                "⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠿⠛⠋⡁⣄⣤⡴⠞⣛⣩⣥⣶⠚⠉⠁⠀⠀⠈⠌⠊⡂⡁⢊⠠⠡⠀⠀⠀⠀⠱⡀⠀⠳⡀⠀⠠⠃⠀⡠⠈⢠⠃⠀⢈⡨⡘⣈⠘⡈⠒⠥⢣⢑⠅⠄⠠⠠⠤⠤⣅⣍⣉⣉⣋⡛⡛⢛⠛⡛⠟⠟⠿⠻⠿⠿⠿⠿⡿⡿⣿",
                "⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠿⠛⠋⢉⣀⣤⣴⣶⠿⢛⣩⣴⣶⡿⠿⣛⣫⠵⠑⠉⣈⠠⠈⠀⣾⣿⣿⣿⣿⣾⣶⠆⠈⠀⠐⠀⠑⡄⠀⠙⢆⠀⡀⡢⠃⢀⠇⠀⠂⢰⣿⣿⣿⣿⣿⣶⣶⣈⡐⠐⠀⠈⠈⠉⠉⢁⣉⣙⡙⠛⠻⠯⠽⡹⣙⣛⢛⠚⠞⠶⠶⠶⢶⢶⣦⣦",
                "⣿⣿⣿⣿⣿⣿⣿⠿⠟⠛⠉⣀⣤⣦⣶⡿⠿⣛⣭⣵⣶⠿⠟⠋⣩⡴⠞⢉⣁⡤⠴⠚⠉⠀⠀⠀⡀⣿⣿⣿⣿⣿⣿⡿⠁⠀⡄⠀⠄⠀⠈⠢⡀⠈⠱⢄⠀⠀⠊⠀⠀⠀⢺⣿⣿⣿⣿⣿⣿⣿⣿⡗⠀⠠⠲⡴⣤⣤⣤⣄⣀⣈⣉⢉⠓⠓⠶⠶⠶⣴⣥⣥⣥⣄⣂⣂⣀⡀⡀",
                "⣿⠿⠟⠛⠉⠀⠀⣤⣴⣾⠿⠟⣋⣭⣶⣾⠿⠛⣉⣡⣤⠴⠖⣋⡥⠖⠚⠉⠀⣀⡄⠤⠂⠠⠀⠁⠀⢾⣿⡿⠿⠭⠙⠁⠀⠀⣿⡀⠐⣀⡰⠀⠈⠢⡀⠀⠙⠢⡠⠐⠈⠀⠸⡟⣿⣿⣿⣿⣿⣿⣿⠁⡀⠂⠀⠠⠤⠤⣈⣉⡉⡛⠛⠻⠻⠿⢿⢷⣷⣶⣶⣦⣧⣭⣍⣛⣛⠻⠻",
                "  ⣀⣤⣦⣶⣿⣿⣩⣤⣶⡿⢟⣋⣥⣶⣾⣿⣯⠧⠖⠚⠉⠀⣀⣠⡴⠖⠋⠁⠀⢀⡀⠀⢀⠀⠀⣻⣿⣿⣿⣷⣦⡀⠁⠀⣿⡆⠀⠀⠀⠤⠊⠀⠀⠠⠀⢀⠀⠁⠀⣀⣄⣬⣭⣻⣿⣿⣿⣿⠃⢀⢪⠀⣀⡀⡀⠀⠀⠀⠉⠉⠓⠓⠶⠴⣤⣤⣀⣈⡉⠙⠛⠻⠿⠿⣿⣿⣿",
            };

        box3Text[0] = new List<string>()
            {
                "이름을 입력해주세요. (공백 입력시 취소)"
            };

        box1Text[1] = new List<string>()
            {
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀     ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣠⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀     ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠠⠤⠄⠃⠈⠀⠉⠉⠁⠒⠂⠤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢣⠀⡤⣀⣀⣀⠀⢀⣀⣀⣀⣀⠉⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀     ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡀⡇⠀⠀⠀⢧⡾⠇⠀⠀⢸⣀⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀     ⠀⠀⢡⡷⣿⣶⣄⠈⣠⣴⣾⡷⢼⣿⠀⠀⠀⠀던전은 위험하단다.",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀     ⠀⠀⠀⠀⠀⠀⠋⡇⣇⢸⠉⠀⠈⠹⠀⣇⢸⣷⡇⠀⠀⠀이 중 하나를 고르렴.",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀     ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⡇⠉⠁⢴⠀⠀⠈⠙⠋⣼⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀     ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠱⣄⠰⢶⣶⣶⡶⠛⣠⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀     ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⠀⢤⠒⢹⣦⣤⣕⣂⣤⣾⡇⠱⡤⠀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀     ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠔⠊⠁⠀⢀⠆⠀⣿⣿⣿⣿⣿⣿⣿⡇⠀⢃⠀⠀⠉⠐⠠⢀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀     ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⠀⠀⠀⠀⡌⠀⢀⣿⣿⣿⣿⣿⣿⣿⡇⠀⠘⡀⠀⠀⠀⠀⠋⠆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀     ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠆⠀⠀⠀⢀⠬⠋⢸⣿⣿⣿⣿⣿⣿⣿⠀⠲⢒⠁⠀⠀⠀⡜⠀⠰⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀     ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠎⠀⢰⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⠀⠀⢠⠃⠀⠀⠀⠀⠀⠀⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀     ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣠⣤⣤⣤⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣠⣤⣤⣤⣶⣤⣤⣤⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "     ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣴⣶⣆⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀     ⠀⠀⠀⠀⠀⠀⠀⣠⠤⢤⣤⣤⡶⠿⣿⣿⣿⣿⠿⣿⣦⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣰⣿⠿⣿⣿⠈⠙⠻⣿⣿⣿⣿⣿⣿⣿⣷⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀     ⠀⠀⠀⠀⠀⠀⢸⠁⠲⣦⡞⠁⣖⠀⠈⠁⢰⣖⠀⠀⠙⣿⣷⡦⠤⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⣼⡟⠀⠀⣀⡈⠀⠀⠀⣿⠿⠛⠻⢿⣿⣿⣿⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣿⣿⣿⣿⣿⣿⣿⣿⢻⣿⣿⣿⣿⣿⣿⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀     ⠀⠀⠀⠀⠀⠀⠈⡶⠞⠋⠉⠀⢝⣶⣶⠄⠈⠉⠀⠠⢴⣿⣿⠧⠤⠀⢻⡄⠀⠀⠀⠀⠀⠀⠀⣿⡇⠀⠠⣿⣿⠀⠀⠀⠁⠀⣠⣤⣄⠈⠙⢛⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⡟⠹⢭⡭⠉⠋⠁⠿⣿⠍⠉⣿⣟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀     ⠀⠀⠀⠀⠀⠀⣸⠀⠀⠀⠀⢀⣰⣋⠀⠀⠀⠀⠀⠀⠀⢻⣿⣇⣀⣶⣾⠃⠀⠀⠀⠀⠀⠀⠀⣿⣧⠀⠈⢿⣿⠀⠀⠀⣴⣾⣿⣿⣿⣷⣴⣿⣿⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⣿⣇⠀⠀⠀⢀⡀⠀⠀⠀⠀⠀⡿⡽⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "     ⠀⠀⠀⠀⠀⠀⢰⠿⣀⡖⠒⠻⣿⣿⣿⣿⣾⣶⣤⡀⠀⠀⣸⣿⡿⠛⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠹⣿⣷⣤⣀⣀⠀⠀⠀⡿⢿⣿⣿⣿⣿⣿⣿⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⣄⠀⠀⢀⣁⣁⠀⠀⠀⣸⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀     ⠀⠀⠀⠀⠀⠀⢹⣯⣾⡷⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⢿⣿⣿⣿⣷⣦⣄⣤⣾⣿⣿⣿⣿⡿⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣦⡀⠀⠀⠀⠀⣠⣼⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀     ⠀⠀⠀⠀⢀⣴⠛⣫⠟⠈⠁⢚⡋⣽⢿⣿⣿⣿⣿⣿⣿⡟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣩⡟⠿⣿⣿⣿⣿⣿⣿⡿⣟⡋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡏⠉⠛⠛⠛⠛⠁⣿⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "     ⠀⠀⠀⢀⡴⠿⢀⣖⠁⠀⢠⡴⠋⠐⢠⣿⣿⣿⣿⣿⣿⣿⠇⠀⠀⠀⠀⢀⣴⣶⣤⠀⢀⣀⣠⣤⣴⣶⣿⣿⣿⣄⠀⠈⢩⣿⣿⣦⡀⣿⣿⣷⣶⣶⣤⣄⠀⠀⠀⠀⠀⠀⠀⣀⣤⣴⣶⢿⣦⣀⠀⠀⠀⠀⢀⣼⡿⣷⢦⣤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀     ⠀⠀⣾⡇⣜⠁⠀⢜⡴⠳⠤⣓⣾⣿⣿⣿⠟⠉⢻⣿⣿⣇⠀⠀⠀⢀⣾⣿⡿⠁⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⠞⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⠀⠀⢀⣠⣶⢿⣛⣯⣷⣾⣿⣿⣿⣿⣶⣶⣾⣿⣿⣿⣶⣭⣟⡿⠿⣶⣦⡀⠀⠀⠀⠀⠀",
                "⠀     ⠀⠀⠘⢿⣯⣶⣤⣾⣡⢉⣲⣿⣿⣿⣿⣿⣴⣾⣿⣿⣿⣿⣦⣄⣠⣼⣿⣿⠇⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣇⢠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⠀⠀⣼⣿⣾⣿⣟⡄⢜⣿⣿⣿⣿⣿⣿⡿⣿⣿⣿⣿⣿⡰⡗⣸⣿⣶⣿⣧⠀⠀⠀⠀⠀"
            };

        box3Text[1] = new List<string>()
            {
                "직업을 선택해주세요.",
                "1. 쌀숭이          2. 주갤러          3. 프로게이머          0. 돌아가기"
            };

        box1Text[2] = new List<string>()
            {
                "",
                "",
                "",
                "`darkgray,black`⠀    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⡠⡤⡲⡲⡲⡲⡲⡲⢦⢤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "`darkyellow,black`⠀    ⠀⠀⠀⠀⠀⣀⢄⡄⡄⡄⡄⡄⡄⡄⣄⡠⣀⢄⡠⣀⢄⡠⣀⢄⡠⣀⢄⡀⠀⠀⠀`darkgray,black`⠀⠀⠀⠀⠀⢀⢔⢪⢪⢪⢪⢪⢪⢪⢪⢪⢪⢪⢪⢪⢻⣢⢄⠀⠀⠀⠀⠀⠀`yellow,black`⠀⠀⡠⠄⠤⠄⡄⣀⣀⣀⢀⢀⢀⢀⢀⠀⠀⠀⠀⠀⢀⢀⢀⢀⢀⣀⣀⡀⠀⠀⠀   ",
                "`darkyellow,black`⠀    ⠀⠀⠀⠀⠀⡮⣳⢵⢹⢜⢕⡝⣜⢮⢲⢱⢣⢳⢹⢸⢪⢺⢸⡱⡹⣸⢱⢽⠀⠀⠀⠀`darkgray,black`⠀⠀⠀⡠⡣⡣⡣⡣⡣⡣⡣⡣⡣⡣⡣⡣⡣⡣⡣⡣⡳⡫⣗⡄⠀⠀⠀`yellow,black`⠀⠀⢰⠁⡐⢀⠡⠀⠂⠄⢐⠀⠅⠡⠈⠄⠡⠉⠌⠡⠡⠑⠠⢁⠡⢁⢰⠬⡹⡂⠀⠀",
                "`darkyellow,black`⠀    ⠀⠀⠀⠀⢐⢝⢮⡪⡳⠓⠓⠓⠑⠓⠓⠙⠊⠓⠙⠊⠓⠓⠓⠑⡏⣎⢞⢾⢝⡆⠀`darkgray,black`⠀⠀⣠⢪⢪⢪⢪⠪⡪⡪⡪⡪⡪⡪⡪⡪⡪⡪⡪⡪⡪⡪⡪⡳⣝⢶⡄⠀`yellow,black`⠀⠀⢸⠠⢐⢀⢂⠡⢈⢐⢀⠂⠡⠈⠄⠡⠈⠄⠡⠈⠄⠡⠈⠄⢂⠐⢜⢝⡎⡇⠀⠀",
                "`darkyellow,black`⠀    ⠀⠀⠀⠀⢸⢕⣗⢝⡎⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣏⢎⢮⢯⣳⠃`darkgray,black`⠀⠀⠈⡖⡕⡕⡕⠕⠱⠑⠕⠱⠑⠕⠕⠕⠕⠕⠕⠕⠕⠕⢕⢕⢕⢜⢵⣻⠀⠀⠀`yellow,black`⠀⠣⢣⠲⡰⠱⡢⠲⡰⡡⠥⠥⠥⠥⡡⡡⡅⡅⣅⡅⢅⢕⠄⠅⣇⢗⡕⡇⠀⠀",
                "`darkyellow,black`⠀    ⠀⠀⠀⠀⡪⡳⡕⡧⡇" + PrintUtil.AlignLeft((player1.Level == 0)? "" : $"`gray,black`Level : {player1.Level}", 14) + "`darkyellow,black`⢀⢗⢕⣳⡳⣵⠁⠀⠀`darkgray,black`⢈⢮⢪⠪⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⢸⢨⢪⢺⣺⠀⠀⠀⠀`yellow,black`⠀⢸⠐⡈⠈⠈⠈⠐⠈⠈⠈⠈⠈⠂⠑⠐⠑⠐⠨⠘⠌⠪⠩⡊⡣⢪⠃⠀⠀",
                "`darkyellow,black`⠀    ⠀⠀⠀⠀⡯⣺⢕⣳⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⢱⢣⡳⣝⠾⠀⠀⠀`darkgray,black`⢀⢇⢇⢇⡇" + PrintUtil.AlignCenter($"`gray,black`{player2.Name}",14) + "`darkgray,black`⠀⢸⢸⢸⢨⡳⣵⠀⠀⠀⠀`yellow,black`⠀⠸⢀⠐⠀              ⠀⠀⠀`yellow,black`⠐⠠⠈⠼⠀⠀⠀",
                "`darkyellow,black`⠀    ⠀⠀⠀⢠⢫⢮⡣⣳⠀              ⢸⢱⢣⣻⡪⡇⠀⠀`darkgray,black`⠀⠠⡣⡣⡣⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⢸⠸⡸⡺⣺⠀⠀⠀⠀`yellow,black`⠀⡣⠀⢂⠀" + PrintUtil.AlignLeft((player3.Level == 0)? "" : $"`gray,black`Level : {player3.Level}", 14) + "⠀⠀⠀`yellow,black`⢁⠂⠡⡃⠀⠀⠀",
                "`darkyellow,black`⠀    ⠀⠀⠀⢸⢕⡗⣝⡜⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡏⣎⢧⡳⣝⡇⠀⠀`darkgray,black`⠀⢐⢕⢕⢕⠇" + PrintUtil.AlignLeft((player2.Level == 0)? "" : $"`gray,black`Level : {player2.Level}", 14) + "`darkgray,black`⠀⢸⢸⢸⢘⢮⣫⠀⠀⠀`yellow,black`⠀⠀⡇⠈⠄⠀              ⠀⠀⠀`yellow,black`⠂⠄⢡⠃⠀⠀⠀",
                "`darkyellow,black`    ⠀⠀⠀⠀⣜⢵⢝⡲⡃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡏⡮⣪⢾⢵⠁⠀⠀`darkgray,black`⠀⢐⡕⡕⡕⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⢸⠸⡸⣱⢽⠀⠀⠀`yellow,black`⠀⢨⠂⡁⠂⠀" + PrintUtil.AlignRight($"`gray,black`{player3.Name}",14) + "⠀⠀`yellow,black`⠈⠐⡈⢸⠀⠀⠀⠀",
                "`darkyellow,black`    ⠀⠀⠀⠀⣗⢵⢳⢭⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢨⢣⡳⣕⡟⣞⠀⠀`darkgray,black`⠀⠀⠠⡣⡣⡪⡃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⢸⢸⢸⣪⣻⠀⠀⠀`yellow,black`⠀⡘⠠⠐⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀`yellow,black`⠈⠄⠐⡌⠀⠀⠀⠀",
                "`darkyellow,black`    ⠀⠀⠀⢠⡳⣹⢕⢽" + PrintUtil.AlignRight($"`gray,black`{player1.Name}",14) + "`darkyellow,black` ⢸⢱⡱⡵⣝⢞⠀⠀`darkgray,black`⠀⠀⢈⡇⡇⡇⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀`darkgray,black`⢸⢸⢸⢰⢕⢷⠀⠀⠀`yellow,black`⠀⡎⠀⠅⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀`yellow,black`⢁⠈⠄⡇⠀⠀⠀⠀",
                "`darkyellow,black`    ⠀⠀⠀⢸⡪⣳⢹⡌              ⠀⡪⣣⢳⢽⣪⠇⠀⠀`darkgray,black`⠀⠀⠠⡇⡇⡇⡇⠀              `darkgray,black`⢸⢸⢘⢬⢫⢯⠀⠀`yellow,black`⠀⠠⡡⠈⠄⠀⠀              ⠀`yellow,black`⠀⢂⠐⠸⡀⠀⠀⠀⠀",
                "`darkyellow,black`    ⠀⠀⠀⡼⣸⡕⣕⢧⢳⢪⡲⡕⡮⡲⡕⣎⢖⡕⡮⡲⡪⣲⢱⢝⢜⢎⣗⢵⠇⠀`darkgray,black`⠀⠀⠀⢐⢇⢇⢇⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀`darkgray,black`⢸⢸⢸⢨⡳⣻⠀⠀⠀`yellow,black`⢠⠁⡂⡁⢀⠀⡀⢀⠀⢀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠐⠀⡂⢱⠀⠀⠀⠀⠀",
                "`darkyellow,black`    ⠀⠀⠀⡯⣪⡺⣜⣜⣎⢧⣣⡳⣱⡣⣳⡱⣕⣕⢇⣏⢞⣜⢎⡮⣳⡹⣎⢯⠂⠀`darkgray,black`⠀⠀⠀⠐⡇⡇⡇⡭⠭⠭⠭⠭⠭⠭⠭⠭⠭⠭⠭⢭⢩⠭⡍⡇⡇⡇⡇⡯⣺⣀⡀`yellow,black`⡰⠊⠒⢒⠐⡒⠢⠤⠴⠬⠤⠬⠬⠌⠬⠬⠤⠥⠬⠤⠥⠢⠥⠥⡬⡺⡰⡄⠀⠀⠀",
                "`darkyellow,black`    ⠀⠀⠀⡯⡎⠍⠌⠌⢌⠅⡅⢍⠅⡍⢕⢙⢚⢚⢛⢚⢓⢓⠫⡚⢕⠫⡺⡽⠀`darkgray,black`⠀⠀⡴⡪⢫⢓⡓⡓⣓⠫⡫⡙⡎⡏⠧⠯⡭⠽⢼⢹⢬⠣⢧⠣⡇⢧⠣⡇⡯⡳⣕⡇`yellow,black`⡇⢈⠈⠄⢂⠐⠐⢐⠀⠅⢂⠨⠀⠅⠨⠀⠅⠨⠀⠅⠨⢈⠨⢐⠵⡕⡕⡇⠀⠀⠀",
                "`darkyellow,black`    ⠀⠀⠀⢫⢞⡬⡬⡬⡤⢥⢌⢆⢕⢌⣂⢢⡑⡄⡕⣌⢢⣑⢅⢇⡕⣝⢞⡝⠀⠀`darkgray,black`⠀⡧⡣⡣⡣⡣⡣⡣⡣⡣⡣⡣⠭⡍⡇⡇⡏⡎⡖⡕⡝⡜⡜⡜⡜⡜⡜⡮⣫⢞⡎`yellow,black`⢇⣂⢢⢡⢐⠌⡌⢄⢢⠡⣐⢠⢡⠨⠠⠡⡈⢄⠡⡈⡐⡀⡂⢘⢕⢕⢕⠇⠀⠀⠀",
                "`darkyellow,black`    ⠀⠀⠀⠀⠉⠊⠑⠉⠊⠉⠊⠉⠑⠙⠘⠑⠋⠋⠛⠊⠓⠓⠋⠓⠙⠚⠙⠀⠀⠀`darkgray,black`⠀⠳⠵⠵⠵⠵⠵⠵⠕⠵⠵⠭⡣⠧⠧⠧⠧⠧⠧⠧⠧⡧⢧⢧⣳⣝⣜⠽⠚⠁⠀`yellow,black`⠀⠀⠁⠁⠁⠁⠀⠁⠀⠁⠈⠀⠁⠉⠉⠉⠈⠘⠈⠒⠊⠒⠪⠆⢗⠵⠉⠀⠀⠀⠀",
                "",
                "",
                "",
                "",
            };

        box3Text[2] = new List<string>()
            {
                "데이터를 저장할 슬롯을 선택해주세요(1~3) (0. 돌아가기)"
            };
    }

    public static class TempPlayerData
    {
        public static string TempPlayerName { get; set; }
        public static string TempPlayerJob { get; set; }
    }
}