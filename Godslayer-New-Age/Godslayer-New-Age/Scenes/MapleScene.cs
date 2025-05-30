using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal class MapleScene : IScene
{
    Random random = new Random();
    public GameState SceneType => GameState.Maple;//바꾸기

    private int room = 1;
    private int turn;
    private int randnum1;
    private int randnum2;
    bool[] isDie = { false, false };
    List<Monster> monsters = new List<Monster>();
    public void AddMonster()//다른 곳에서바꾸기
    {
        monsters.Clear();
        monsters.Add(Monster.slime.Clone());
        monsters.Add(Monster.orangeMushroom.Clone());
        monsters.Add(Monster.slime.Clone());
        monsters.Add(Monster.orangeMushroom.Clone());
        monsters.Add(Monster.kangWunky);
        monsters.Add(Monster.godChangseop);
        monsters.Add(Monster.elonMusk);
        monsters.Add(Monster.GOD);
    }
    public void StartBattle(Monster monster1, Monster monster2, int skillnum, int targetnum)//일반몹(2명씩 나올 예정)
    {
        Unit unit;
        float player_rand_Spd = random.Next(0, 5) + Player.Instance.Speed;
        float monster1_rand_Spd = random.Next(0, 5) + monster1.Speed;
        float monster2_rand_Spd = random.Next(0, 5) + monster2.Speed;
        var turnList = new List<(float speed, object character)>
        {
            (player_rand_Spd, Player.Instance),
            (monster1_rand_Spd, monster1),
            (monster2_rand_Spd, monster2)
        };

        turnList.Sort((a, b) => b.speed.CompareTo(a.speed));//순서 정렬

        foreach (var entry in turnList)//순서에 따라 하나씩 실행
        {
            if (entry.character is Player)
            {
                if(targetnum==0)
                {
                    unit = Player.Instance;
                }
                else if(targetnum ==1)
                {
                    unit = monster1;
                }
                else
                {
                    unit = monster2;
                }
                PlayerTurn(skillnum, unit,targetnum);
            }
            else if (entry.character is Monster monster)
            {
                int alpa;
                if(monster == monster1)
                {
                    alpa = 1;
                }
                else
                {
                    alpa = 2;
                }
                MonsterTurn(monster,alpa);
            }
        }
        if (monster1.HP <= 0 && isDie[0]==false)
        {
            monster1.HP = 0;
            Player.Instance.EXP += monster1.EXP;
            box1Text[2].Add($"{monster1.Name}A을(를) 쓰러뜨렸습니다.");
            box1Text[2].Add($"{monster1.EXP} 경험치를 획득하였습니다.");
            isDie[0] = true;
        }
        if (monster2.HP <= 0 && isDie[1] == false)
        {
            monster2.HP = 0;
            Player.Instance.EXP += monster2.EXP;
            box1Text[2].Add($"{monster2.Name}B을(를) 쓰러뜨렸습니다.");
            box1Text[2].Add($"{monster2.EXP} 경험치를 획득하였습니다.");
            isDie[1] = true;
        }
        Player.Instance.ProcessBuffs();
        monster1.ProcessBuffs();
        monster2.ProcessBuffs();
        turn++;
    }

    public void StartBattle(Monster boss, int skillnum, int targetnum)//보스전
    {
        Unit unit;
        float player_rand_Spd = random.Next(0, 5) + Player.Instance.Speed;
        float boss_rand_Spd = random.Next(0, 5) + boss.Speed;
        var turnList = new List<(float speed, object character)>
        {
            (player_rand_Spd, Player.Instance),
            (boss_rand_Spd, boss)
        };

        turnList.Sort((a, b) => b.speed.CompareTo(a.speed));

        foreach (var entry in turnList)
        {
            if (entry.character is Player)
            {
                if (targetnum == 0)
                {
                    unit = Player.Instance;
                }
                else
                {
                    unit = boss;
                }
                PlayerTurn(skillnum, unit,20);
            }
            else if (entry.character is Monster monster)
            {
                MonsterTurn(monster, 20);
            }
        }
        if (boss.HP == 0)
        {
            Player.Instance.EXP += boss.EXP;
            box1Text[3].Add($"{boss.Name}을(를) 쓰러뜨렸습니다.");
            box1Text[3].Add($"{boss.EXP} 경험치를 획득하였습니다.");
        }
        Player.Instance.ProcessBuffs();
        boss.ProcessBuffs();
        turn++;
    }
    public bool HasPlusGold()
    {
        string[] namesToCheck = { "솜크빈", "자석펫", "테슬라 기어봉", "화성인 가면", "그랜드 마스터 딱지", "챌린저 딱지" };

        return PlayerInventory.EquippedItems.Any(item => namesToCheck.Contains(item.Name));
    }
    public void BossDrop()//여기 바꾸기
    {
        if (Player.Instance.PlayerJob == Player.Job.RiceMonkey)
        {
            Item absolcalibur = new Item("앱솔칼리버", ItemType.Weapon, new Dictionary<StatType, float> { { StatType.ATK, 100 } }, "신창섭의 가호를 받은 기간제 무기", "", 8500);
            PlayerInventory.Add(absolcalibur);
            Item challengerArmor = new Item("도전자의 갑옷", ItemType.Armor, new Dictionary<StatType, float> { { StatType.DEF, 80 } }, "신창섭의 가호를 받은 기간제 갑옷", "", 9000);
            PlayerInventory.Add(challengerArmor);
            box1Text[5].Add($"{Player.Instance.Name}은(는) {monsters[5].Name}을(를) 꺾고 {absolcalibur.Name}와 {challengerArmor.Name}을 획득하였다!!");
        }
        else
        {
            box1Text[5].Add($"{Player.Instance.Name}은(는) 상자를 열었지만 그곳에는 아무것도 없었다");
        }
    }
    public void PlayerTurn(int skillnum, Unit unit, int num)
    {
        float FHP = unit.HP;
        int textnum;
        switch(room)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 6:
            case 7:
            case 8:
            case 9:
                textnum = 2;
                break;
            case 5:
                textnum = 3;
                break;
            case 10:
                textnum = 4;
                break;
            default:
                textnum = 2;
                break;
        }
        if (Player.Instance.HP > 0)
        {
            Player.Instance.PlayerSkills[skillnum].Use(unit, Player.Instance);
            if (unit.HP < FHP)
            {
                if (num == 1)
                {
                    box1Text[textnum].Add($"{Player.Instance.Name}이(가) {unit.Name}A에게 {FHP - unit.HP}만큼 피해를 입혔다!!");
                }
                else if (num == 2)
                {
                    box1Text[textnum].Add($"{Player.Instance.Name}이(가) {unit.Name}B에게 {FHP - unit.HP}만큼 피해를 입혔다!!");
                }
                else
                {
                    box1Text[textnum].Add($"{Player.Instance.Name}이(가) {unit.Name}에게 {FHP - unit.HP}만큼 피해를 입혔다!!");
                }
            }
        }
    }
    public void MonsterTurn(Monster monster, int num)
    {
        int randomskill;
        float FHP = Player.Instance.HP;
        int textnum;
        switch (room)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 6:
            case 7:
            case 8:
            case 9:
                textnum = 2;
                break;
            case 5:
                textnum = 3;
                break;
            case 10:
                textnum = 4;
                break;
            default:
                textnum = 2;
                break;
        }
        if(monster.Name == "신창섭")
        {
            randomskill = random.Next(1, monster.MonsterSkills.Count);
            if (monster.HP > 0)
            {
                if(turn == 1)
                {
                    monster.MonsterSkills[0].Use(Player.Instance, monster);
                    box1Text[textnum].Add($"{monster.Name}이 {Player.Instance.Name}을(를) 정상화시켰다!!");
                }
                else
                {
                    monster.MonsterSkills[randomskill].Use(Player.Instance, monster);
                    box1Text[textnum].Add($"{monster.Name}이(가) {Player.Instance.Name}에게 {FHP - Player.Instance.HP}만큼 피해를 입혔다!!");
                }
            }
            else
            {
                box1Text[textnum].Add($"{monster.Name}가 어떤 스킬을 썼다!");
            }
        }
        else
        {
            randomskill = random.Next(0, monster.MonsterSkills.Count);
            if (monster.HP > 0)
            {
                monster.MonsterSkills[randomskill].Use(Player.Instance, monster);
                if (Player.Instance.HP < FHP)
                {
                    if (num == 1)
                    {
                        box1Text[textnum].Add($"{monster.Name}A가 {Player.Instance.Name}에게 {FHP - Player.Instance.HP}만큼 피해를 입혔다!!");
                    }
                    else if (num == 2)
                    {
                        box1Text[textnum].Add($"{monster.Name}B가 {Player.Instance.Name}에게 {FHP - Player.Instance.HP}만큼 피해를 입혔다!!");
                    }
                    else
                    {
                        box1Text[textnum].Add($"{monster.Name}이(가) {Player.Instance.Name}에게 {FHP - Player.Instance.HP}만큼 피해를 입혔다!!");
                    }
                }
                else
                {
                    box1Text[textnum].Add($"{monster.Name}가 어떤 스킬을 썼다!");
                }
            }
        }
    }//여기 바꾸기
    public void StageText(int Scene)
    {
        box1Text[Scene].Clear();
        box1Text[Scene].Add("Stage " + room);
        box1Text[Scene].Add(" ");
    }
    public int TargetText(int Scene) 
    {
        int input3;
        box3Text[Scene].Clear();
        box3Text[Scene].Add("대상을 선택해주세요");
        box3Text[Scene].Add($"0.{Player.Instance.Name}   1.{monsters[randnum1].Name}A   2.{monsters[randnum2].Name}B");
        PrintDB.box2Data = PrintDB.GetPlayerStatus();
        PrintUtil.CreateBox();
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out input3) && (input3 >= 0 && input3 <= 2))
            {
                return input3;
            }
            else
            {
                return 100;
            }
        }
    }
    public int TargetText2(int Scene)
    {
        int input3;
        box3Text[Scene].Clear();
        box3Text[Scene].Add("대상을 선택해주세요");
        string bossname = (room == 5) ? $"{monsters[4].Name}" : $"{monsters[5].Name}";
        box3Text[Scene].Add($"0.{Player.Instance.Name}   1.{bossname}");
        PrintDB.box2Data = PrintDB.GetPlayerStatus();
        PrintUtil.CreateBox();
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out input3) && (input3 >= 0 && input3 <= 1))
            {
                return input3;
            }
            else
            {
                return 100;
            }
        }
    }
    public int PlayerSkillText(int Scene)
    {
        int input2;
        box3Text[Scene].Clear();
        box3Text[Scene].Add("사용할 스킬을 선택해주세요");
        box3Text[Scene].Add($"1.{Player.Instance.PlayerSkills[0].SkillName} 2.{Player.Instance.PlayerSkills[1].SkillName} 3.{Player.Instance.PlayerSkills[2].SkillName} 4.{Player.Instance.PlayerSkills[3].SkillName}");
        PrintDB.box2Data = PrintDB.GetPlayerStatus();
        PrintUtil.CreateBox();
        while (true)
        {
            int.TryParse(Console.ReadLine(), out input2);
            if (input2 >= 1 && input2 <= 4)
            {
                return input2 - 1;
            }
            else
            {
                return 100;
            }
        }
    }
    public void PressAnyKey(int Scene)
    {
        box3Text[Scene].Clear();
        box3Text[Scene].Add("아무 키나 눌러 진행");
        PrintUtil.CreateBox();
        Console.ReadKey(true);
    }
    public GameState Run(int phase)
    {
        PrintDB.box1Data = box1Text.ContainsKey(phase) ? box1Text[phase] : new List<string>();
        PrintDB.box2Data = PrintDB.GetPlayerStatus();
        PrintDB.box3Data = box3Text.ContainsKey(phase) ? box3Text[phase] : new List<string>();

        PrintUtil.CreateBox();

        AddMonster();
        switch (phase)
        {
            case 0:
                BGM_Player.Instance().Music_Exit();
                PressAnyKey(0);
                SceneManager.SetPhase(1);
                return GameState.Retry;
            case 1:
                string input = Console.ReadLine();
                if (input == "0")
                {
                    monsters.Clear();
                    return GameState.Pop;
                }
                else if(input =="q")
                {
                    room = 5;
                    return GameState.Retry;
                }
                else if(input =="w")
                {
                    room = 10;
                    return GameState.Retry;
                }
                switch (input)
                {
                    case "1":
                        turn = 1;
                        switch (room)
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                                randnum1 = random.Next(0, 2);
                                randnum2 = random.Next(2, 4);
                                BGM_Player.Instance().Play_Maple_Easy_Loop();
                                SceneManager.SetPhase(2);
                                return GameState.Retry;
                            case 5:
                                BGM_Player.Instance().Play_Maple_Gang_Loop();
                                SceneManager.SetPhase(3);
                                return GameState.Retry;
                            case 10:
                                BGM_Player.Instance().Play_Maple_SinChangSup_Loop();
                                SceneManager.SetPhase(4);
                                return GameState.Retry;
                            default:
                                return GameState.Main;
                        }
                    default:
                        return GameState.Retry;
                }
            case 2:
                StageText(2);
                box1Text[2].Add($"{monsters[randnum1].Name}A와 {monsters[randnum2].Name}B가 공격해온다!!");
                while (Player.Instance.HP > 0 && (monsters[randnum1].HP >0 || monsters[randnum2].HP > 0))
                {
                    box1Text[2].Add(" ");
                    int skillnum = PlayerSkillText(2);
                    if (skillnum == 100)
                    {
                        return GameState.Retry;
                    }
                    int targetnum = TargetText(2);
                    if (targetnum == 100)
                    {
                        return GameState.Retry;
                    }
                    if (box1Text[2].Count > 18)
                    {
                        box1Text[2].RemoveRange(2, box1Text.Count - 2);
                    }
                    StartBattle(monsters[randnum1], monsters[randnum2], skillnum, targetnum);
                }
                Player.Instance.Buffs.Clear();
                monsters[0].Buffs.Clear();
                monsters[1].Buffs.Clear();
                monsters[2].Buffs.Clear();
                monsters[3].Buffs.Clear();
                isDie[0] = false;
                isDie[1] = false;
                if (Player.Instance.HP <= 0)
                {
                    Player.Instance.HP = 0;
                    return GameState.GameOver;
                }
                else
                {
                    BGM_Player.Instance().Play_Victory();
                    box1Text[2].Add(" ");
                    box1Text[2].Add("승리하였습니다");
                    if (HasPlusGold())
                    {
                        box1Text[2].Add($"+{(monsters[randnum1].Gold + monsters[randnum2].Gold) * 2}gold");
                        Player.Instance.Gold += (monsters[randnum1].Gold + monsters[randnum2].Gold) * 2;
                    }
                    else
                    {
                        box1Text[2].Add($"+{monsters[randnum1].Gold + monsters[randnum2].Gold}gold");
                        Player.Instance.Gold += monsters[randnum1].Gold + monsters[randnum2].Gold;
                    }
                    PrintDB.box2Data = PrintDB.GetPlayerStatus();
                    PressAnyKey(2);
                    room++;
                    SceneManager.SetPhase(1);
                    return GameState.Retry;
                }
            case 3:
                if (monsters[4].HP <= 0)
                {
                    StageText(3);
                    box1Text[3].Add("텅 빈 던전을 지나간다");
                    PressAnyKey(3);
                    room++;
                    SceneManager.SetPhase(1);
                    return GameState.Retry;
                }
                while (Player.Instance.HP > 0 && monsters[4].HP > 0)
                {
                    int skillnum = PlayerSkillText(3);
                    if (skillnum == 100)
                    {
                        return GameState.Retry;
                    }
                    int targetnum = TargetText2(3);
                    if (targetnum == 100)
                    {
                        return GameState.Retry;
                    }
                    box1Text[3].RemoveRange(19, box1Text[3].Count - 19);
                    StartBattle(monsters[4], skillnum, targetnum);
                }
                if (Player.Instance.HP <= 0)
                {
                    Player.Instance.HP = 0;
                    return GameState.GameOver;
                }
                else
                {
                    BGM_Player.Instance().Play_Victory();
                    StageText(3);
                    box1Text[3].Add("승리하였습니다");
                    if (HasPlusGold())
                    {
                        box1Text[3].Add($"+{(monsters[4].Gold) * 2}gold");
                        Player.Instance.Gold += (monsters[4].Gold) * 2;
                    }
                    else
                    {
                        box1Text[3].Add($"+{(monsters[4].Gold) * 2}gold");
                        Player.Instance.Gold += (monsters[4].Gold) * 2;
                    }
                    PrintDB.box2Data = PrintDB.GetPlayerStatus();
                    PressAnyKey(3);
                    room++;
                    SceneManager.SetPhase(1);
                    return GameState.Retry;
                }
            case 4:
                if (monsters[5].HP <= 0)
                {
                    StageText(4);
                    box1Text[4].Add("텅 빈 던전을 나와 마을로 돌아간다");
                    PressAnyKey(4);
                    return GameState.Main;
                }
                while (Player.Instance.HP > 0 && monsters[5].HP > 0)
                {
                    int skillnum = PlayerSkillText(4);
                    if (skillnum == 100)
                    {
                        return GameState.Retry;
                    }
                    int targetnum = TargetText2(4);
                    if (targetnum == 100)
                    {
                        return GameState.Retry;
                    }
                    box1Text[4].RemoveRange(19, box1Text[4].Count - 19);
                    StartBattle(monsters[5], skillnum, targetnum);
                }

                if (Player.Instance.HP <= 0)
                {
                    Player.Instance.HP = 0;
                    return GameState.GameOver;
                }
                else
                {
                    BGM_Player.Instance().Play_Victory();
                    StageText(4);
                    box1Text[4].Add("승리하였습니다");
                    if (HasPlusGold())
                    {
                        box1Text[4].Add($"+{(monsters[5].Gold) * 2}gold");
                        Player.Instance.Gold += (monsters[5].Gold) * 2;
                    }
                    else
                    {
                        box1Text[4].Add($"+{(monsters[5].Gold) * 2}gold");
                        Player.Instance.Gold += (monsters[5].Gold) * 2;
                    }
                    PrintDB.box2Data = PrintDB.GetPlayerStatus();
                    PressAnyKey(4);
                    SceneManager.SetPhase(5);
                    return GameState.Retry;
                }
            case 5:
                BossDrop();
                PrintDB.box2Data = PrintDB.GetPlayerStatus();
                PrintUtil.CreateBox();
                Console.ReadKey(true);
                if (monsters[5].HP <= 0 && monsters[6].HP <= 0 && monsters[7].HP <= 0)
                {
                    return GameState.Clear;
                }
                return GameState.Main;
            default:
                return GameState.Main;
        }
    }

    public Dictionary<int, List<string>> box1Text = new Dictionary<int, List<string>>();
    public Dictionary<int, List<string>> box3Text = new Dictionary<int, List<string>>();

    public MapleScene()//바꾸기
    {
        //0
        box1Text[0] = new List<string>()
            {
                "본섭의 눈물을 듣고 신이 되기로 결정한 신창섭",
                "리슝좍 10만명을 척결하고 신이 된 그는 이번에는 쌀숭이들 정상화시키려고 한다",
                "당신과 얼마 안남은 동료들의 생계를 위해 그를 죽여야만 한다... "
            };

        box3Text[0] = new List<string>()
            {
                " "
            };
        //1
        box1Text[1] = new List<string>()
        {
            "",
            PrintUtil.AlignCenter("⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣖⢎⣖⢬⢆⢖⣎⢦⡱⡌⡦⡕⡌⣖⢕⢔⢕⢔⣑⡲⡕⢮⢪⢗⡕⡵⡱⣹⡪⡪⡪⡎⡞⡼⡡⣣⢳⢱⠽⡑⢕⢎⢖⡆⡎⡢⣕⢬⢢⡳⣆⢕⡢⣣⡪⣶⢒⣒⡲⣲⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠠⡷⡽⣜⡮⣧⣹⢮⢧⢧⢯⣎⢮⣎⢷⣕⢧⢯⢎⢳⣕⢝⢼⢸⢜⣇⢧⣣⣳⡯⣮⣮⢮⣮⢾⣸⣜⣜⣵⢫⢪⢣⢳⢕⡟⡪⣗⣕⢵⣕⢯⣗⢼⡸⡦⡯⣻⣔⢵⣹⢼⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⠀⠀⠀⠀⠀⠀⠀⠀⢰⢧⡳⣕⣕⢦⡫⣹⢕⢭⡣⡭⡹⡭⡍⣇⡯⡣⡫⣣⢏⢏⢷⡱⣕⣚⣧⣳⡵⠷⠛⠙⠉⠀⠁⠀⠀⠀⠀⠁⠁⠉⠙⠚⠗⡯⣮⣳⢫⢎⢮⡺⡺⣹⡸⡍⡮⢭⢯⠭⡍⡇⡧⣫⢯⣻⢸⡸⡜⡮⣾⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⠀⠀⠀⠀⠀⠀⠀⠀⢹⣗⡷⣵⢮⣣⢷⣽⣳⡳⡽⡺⠾⢵⣕⠷⢯⢻⠮⣧⡣⣇⠧⡝⣖⡷⠋⠊⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠳⣳⣧⢏⢮⣚⢦⣳⢽⠾⢽⠽⡷⡽⡾⢽⠾⡽⠾⢾⢮⡷⠿⢷⣁⡀⠀⠀⠀⠀⠀⠀⠀⠀",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⠀⠀⠀⠀⣶⢳⣝⢭⢧⡣⣯⡺⣪⢮⡣⣲⣣⡳⡝⡼⡹⡢⣳⢝⢎⣏⢕⢮⢹⢎⡷⡽⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⢽⢮⡮⢞⡜⣜⢝⣕⢽⢸⢽⡪⡪⡺⣪⢫⡣⡯⡎⡯⡣⣗⣿⠀⠀⠀⠀⠀⠀⠀⠀",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⠀⠀⠀⠀⢾⣗⣷⣽⢵⣽⣺⡾⣮⣾⢼⣪⢾⢼⣪⡷⣝⣮⣗⣗⢯⢮⣳⡹⣜⣽⠝⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⢮⢅⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠛⣯⣣⢫⡮⣳⠗⣗⢯⢯⢧⢯⢾⢮⢷⢽⣺⢽⢮⢯⣟⣎⢀⡀⣀⢀⢀⠀⠀⠀",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⠀⠀⠀⠀⣗⢵⡳⣜⢮⡲⡵⡕⢵⣣⡳⣕⢗⢵⡱⡸⣧⢳⡸⣎⢧⡣⣝⢽⣽⠊⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢔⢕⢕⡗⢥⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⣾⢝⢭⡲⡹⡼⡱⣕⠵⡕⣯⢢⢣⢳⡸⡜⣎⢗⣗⢜⢮⡺⣪⡫⣿⠆⠀⠀",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⠀⠀⠀⢀⣟⣷⡽⣮⣗⣷⢽⢮⣻⢵⢯⢾⢵⢧⢯⢞⡷⣳⡽⣺⣜⣞⢼⢵⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣆⡀⡠⡣⡳⡵⢭⢪⢢⢀⢠⠬⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢘⣯⢺⡪⣝⡮⣯⣮⢯⣺⣞⣵⣽⢮⣮⣳⣕⣯⡷⣝⣞⣮⣳⣽⣽⢅⡀⠀",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⠀⠀⣺⢕⡗⡮⣺⢢⡳⡵⣕⢗⣎⢮⡪⡎⣺⣕⢵⡱⣪⡲⣝⢦⡣⣫⢻⡻⠀⠀⢀⠀⠀⠀⠀⠀⠀⠀⠀⠒⡝⣜⢜⢆⢯⠣⣇⢏⡎⡮⠃⠀⠀⠀⠀⠀⠀⠀⢀⠀⠀⠀⢾⣓⢯⢱⡱⣕⣎⢮⡲⣪⢲⢺⡣⡮⡗⣜⢦⢳⢕⢧⣳⡇⣖⢞⡼⣺⡇",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⠀⠀⣺⣳⣝⣝⢮⢧⣫⡷⣕⣗⢵⣳⣹⢮⣞⣮⡺⣜⣎⣾⢵⡧⣯⡺⣽⡇⠀⠀⠈⣳⡢⣄⢄⡀⡠⡢⠀⠀⢣⢣⢇⡳⣹⢵⢕⢕⣕⠅⠀⢀⡆⠀⣀⢀⢄⢮⡚⠀⠀⠀⢼⣇⣏⣇⡯⡮⣎⣞⣜⣎⡧⣟⣽⣸⢧⡳⣳⣹⣪⢷⣳⡯⡮⣳⢽⣺⡇",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⠀⠀⣺⣓⡝⣞⢻⢝⣍⢏⣏⢞⢻⡪⡍⡏⣎⢭⠭⣝⢬⢽⢍⡍⡇⡏⣽⠖⠀⠀⠀⠐⣝⢮⡪⣪⢪⡫⡣⡀⠀⡳⣱⢹⣺⡱⣱⢇⠇⠀⡠⣣⢫⢣⡣⢯⢞⡕⠀⠀⠀⠀⢺⡕⡍⣎⢝⡭⣏⢾⢱⠭⡭⡹⡱⡹⡹⡽⣣⢫⡚⣝⢕⣟⣟⢝⣝⡳⡣",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⠀⠀⣷⡳⣝⢮⣺⣳⢕⣗⢵⣣⢳⣝⢮⣫⢺⢜⢽⢜⢵⢽⡳⣝⡝⣞⢾⡇⠀⠀⠀⠀⢜⢮⢝⢮⡪⣺⢸⢜⢤⢺⢪⡣⣳⡫⡎⡮⡣⡰⡕⡵⣱⡣⣟⡳⡕⡅⠀⠀⠀⠀⢼⢇⢯⡺⡵⢽⣓⡯⣇⢯⡺⡺⣜⢕⢗⣽⡪⡺⣚⣎⢧⡳⣏⣗⢵⢝⡧",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⠀⣀⣻⣽⢾⣳⢽⢽⡳⣵⣗⢷⣫⢷⣳⡳⣽⣺⢵⠯⡷⣻⡽⣞⢾⢵⣻⣕⠀⠀⠀⠀⠀⣗⢳⢝⢞⣮⡳⡕⣝⢜⡕⡽⢽⣪⡺⣪⣣⢳⢱⡯⣞⢮⢎⡞⡌⠀⠀⠀⠀⠀⢺⡽⣳⡻⣺⢯⡗⡿⡞⣞⣮⠷⡵⢯⢟⡾⣞⢽⡮⡮⡷⡽⡷⡵⣯⢯⡇",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⢸⣗⢖⢽⣕⣗⢽⡪⡮⣺⡲⣕⢮⢖⣖⡕⡆⡿⡮⡳⣳⣹⡺⣜⢮⡳⣺⣇⠀⠀⠀⠚⠪⠮⡧⣫⢣⡳⡽⣺⣜⢧⡓⡭⣻⡺⡜⡮⣞⡮⡗⣏⢮⣺⠵⡕⠗⠖⠂⠀⠀⠀⢸⣗⡕⣗⢵⡳⣇⢧⠧⡳⣇⢗⢵⡱⣕⢮⡲⡽⡗⣕⢧⡳⡵⡹⣾⢱⡇",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⢸⣷⣫⢿⣞⢮⣳⣝⡽⡼⣏⣮⣫⡳⣵⣹⣪⣻⡽⣹⢼⣼⢻⡚⣗⡻⣺⡇⠀⠀⠀⠀⠀⠀⠈⠊⢷⢕⢟⢎⢗⢷⣝⢞⣽⡺⣜⡮⡟⡮⡏⣗⢯⠪⠉⠀⠀⠀⠀⠀⠀⠀⢺⡳⣝⢞⡗⣟⢾⢼⣝⣾⣫⡳⣵⡹⣜⢮⣺⡽⣏⢮⡳⣝⢮⣫⣾⢵⡇",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⢨⣟⢞⡟⣝⣝⢵⢯⣫⡫⣝⢜⡍⣟⢵⢭⢭⢭⡹⡩⣝⢼⡳⣝⢼⡺⣼⡏⠀⠀⠀⠀⠀⠀⠀⢀⢠⣫⠽⡵⣫⢷⢽⡳⡽⣗⡽⣞⣵⣣⢯⣚⣆⢀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣝⢮⡳⡝⡾⣝⣍⢮⡪⡮⣹⢕⣟⣛⣛⢏⡟⡟⡯⣿⢓⣏⢭⢝⢽⢵",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⢸⡮⣳⢝⣞⢼⢪⣗⢧⢯⢮⡳⣝⢮⡳⡝⡮⡳⣝⢝⢮⣻⣛⣝⢯⣛⢷⣏⠀⠀⠀⠀⢀⢠⡲⡯⣗⢽⢽⡹⣵⡫⡗⠋⢙⠈⠪⡺⣜⢽⡱⣗⣗⡽⣵⣢⢀⠀⠀⠀⠀⠀⢹⡺⣓⣻⢹⡻⣕⢮⡳⣝⢮⡳⡽⣞⢔⣗⢝⡮⣫⢾⣽⢕⣗⢽⡹⣕⣿",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⢼⣻⢮⣫⣮⣫⣳⣻⡵⣗⣧⡯⣮⣟⣮⡯⣾⣝⡼⣝⢮⢾⣪⢮⣳⢳⣽⡗⠀⠀⠀⠀⠀⠀⠀⠉⠈⠉⠱⡝⠎⠊⠀⠀⢸⡁⠀⠈⠘⠳⣝⠕⠑⠉⠀⠀⠀⠀⠀⠀⠀⠀⢼⣫⡳⣕⢯⣺⣳⣳⣹⣪⣣⢯⢾⣝⣞⢮⡳⣝⡮⣟⣾⣳⣕⣯⣺⣵⠯",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⢸⡫⡯⡝⣞⢽⡩⡭⣫⡹⣳⡹⣩⢍⣇⢯⣙⣺⢍⣏⣝⢽⡫⡗⣟⡳⣻⡝⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⡂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣸⠷⡯⡯⡷⢷⡫⡺⣺⡚⣏⢟⣛⡳⣛⢯⣛⢗⣛⣏⢟⣳⣻⡺⡹⣜⢯",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⢸⣝⢮⡳⣽⡳⡽⣹⡪⣞⢾⡽⣪⢗⡵⡳⣕⢾⡳⡵⣪⢿⣹⡪⣗⢽⣺⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢈⢧⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡽⣪⡳⡽⣕⡯⣫⢷⡫⣎⢗⡵⣝⢮⢿⡺⡸⡮⡮⣫⢞⡼⡯⣝⢮⢿",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⢸⣯⡷⣯⣿⢽⡮⣷⢽⣞⣯⡿⣵⢯⣞⣽⢾⣻⣻⣮⣗⣿⠾⡾⡽⢷⢿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣟⣆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢹⡽⢷⡻⡽⡾⣽⢞⣯⣯⢾⡵⣟⣾⢽⡯⣏⣯⢾⣝⣮⢷⣟⣯⣞⣽⡟",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⢺⣪⢞⡼⡔⣯⢯⢮⡳⣕⢧⡳⡵⣹⡵⣝⢽⢜⢮⢦⡣⣾⣝⡮⣯⡳⣽⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠑⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣺⣝⣗⢽⢝⣾⢭⡫⣎⣎⢏⡽⣹⢭⢹⠭⣝⢼⢝⡮⣿⡹⣜⢵⢝⡵⣧",Constants.BOX1_WIDTH),
            PrintUtil.AlignCenter("⢸⣗⡽⣪⢧⣻⣻⢮⡺⡮⣳⢝⣞⢼⣟⡮⣳⢽⢕⣗⣝⢾⢾⢽⢞⣾⣻⣕⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢼⣞⣮⣯⣯⣾⣳⢝⡮⡮⣫⢞⣽⣣⡳⣝⢮⣫⡳⣽⣻⢜⢮⢯⡳⡽⣽",Constants.BOX1_WIDTH),

        };

        box3Text[1] = new List<string>()
            {
                "원하시는 행동을 선택해 주세요",
                "1. 앞으로 나아간다   0. 마을로 돌아간다"
            };
        //2
        box1Text[2] = new List<string>()
            {
                " "
            };

        box3Text[2] = new List<string>()
            {
                " "
            };
        //3
        box1Text[3] = new List<string>()
            {
               "Stage 5",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⣠⣄⣀⣀⡔⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢻⡯⠉⠀⠀⠀⢈⣙⣿⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⣟⣒⡒⣲⣊⡙⣿⣿⣧⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣶⣏⢛⡕⣏⠛⡹⣿⣿⣿⡿⠛⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢹⠈⣝⣶⣚⡍⢠⣿⡿⠛⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡤⢬⣶⠁⠉⠁⣱⣾⣿⡃⣠⠴⢖⣒⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⠏⣴⣿⣿⠁⠛⠉⢛⣿⣿⢟⣽⣾⣿⣿⣿⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⢇⣾⠟⢉⡀⠀⠀⣴⣿⡟⣵⡿⠛⠹⣿⣿⣿⣿⠴⠒⠦⣄⡀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⠖⠉⢩⠏⡾⣼⠋⠀⠀⢈⠇⣼⣿⣏⣾⡇⠀⠀⠀⣿⣿⣿⡿⠀⠀⠀⠀⠙⢦⡀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⠞⠁⠀⢠⠎⢠⣧⡇⠀⢀⣀⣞⣠⣿⣿⡿⠁⠀⠀⠀⠀⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠙⢦⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣃⣤⣤⣴⠃⣀⣿⢿⡗⢚⣿⠟⠋⠉⠁⠀⠈⠁⠀⠀⠀⣰⣿⣿⣿⣿⡀⠀⠀⡀⢰⣦⣀⠈⢧",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⡇⠀⠈⠁⢺⠀⢸⡏⠀⠀⠀⠀⠀⠀⠀⠀⢢⣴⣿⣿⣿⣿⣿⣿⣼⠶⠟⠶⠇⠈⠛⠚⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡇⠀⠀⠀⠈⠣⣴⣳⣤⣄⣀⣀⣀⣤⣴⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⡆⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠳⣦⣤⣶⣶⣶⣿⣿⣿⣿⣿⣭⣭⣭⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠇⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡇⠀⠀⠉⠉⢻⡿⠻⠿⠿⠿⠿⠿⠿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢳⠀⡀⠀⠀⠉⠈⠀⠀⠀⠀⠀⠀⢠⣿⣿⣿⣿⣿⣿⣿⣿⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⡙⠧⢤⣀⣀⡀⠀⠀⠀⠀⠀⣠⣿⣿⣿⣿⠿⠿⠟⢛⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                " ",
                "윙키가 공격해온다!"
            };

        box3Text[3] = new List<string>()
            {
                " "
            };
        //4
        box1Text[4] = new List<string>()
            {
                "Stage 10",
                PrintUtil.AlignCenter("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠟⠛⠛⠿⠛⠛⠛⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",Constants.BOX1_WIDTH),
                PrintUtil.AlignCenter("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",Constants.BOX1_WIDTH),
                PrintUtil.AlignCenter("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",Constants.BOX1_WIDTH),
                PrintUtil.AlignCenter("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠃⠀⠀⠀⠀⠀⣀⣤⣤⣶⣾⣷⣶⣦⣄⠀⠀⠀⠈⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",Constants.BOX1_WIDTH),
                PrintUtil.AlignCenter("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⢀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡀⠀⠀⠈⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",Constants.BOX1_WIDTH),
                PrintUtil.AlignCenter("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⢈⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡀⠀⠀⠸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",Constants.BOX1_WIDTH),
                PrintUtil.AlignCenter("⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠸⢿⣿⣿⣿⣿⠋⠉⢀⣠⣾⣿⢟⣻⣇⠀⠀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",Constants.BOX1_WIDTH),
                PrintUtil.AlignCenter("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⠀⠀⠀⠀⠀⠀⠀⢉⡅⠀⠀⠉⢛⣻⣿⣿⣿⣿⣆⢸⡿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",Constants.BOX1_WIDTH),
                PrintUtil.AlignCenter("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣟⣀⠀⠀⠀⠀⠀⠀⢸⣿⣤⣀⣀⣴⣿⣿⣿⣿⣿⣿⣿⣷⣯⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",Constants.BOX1_WIDTH),
                PrintUtil.AlignCenter("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡀⠀⠀⣠⡄⠀⣿⣿⣿⣿⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡏⠈⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",Constants.BOX1_WIDTH),
                PrintUtil.AlignCenter("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠃⠀⠀⣭⠂⠀⠸⠿⠟⢛⣥⣌⣿⣿⣿⣿⣿⣿⣿⡏⠁⠀⠀⠀⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",Constants.BOX1_WIDTH),
                PrintUtil.AlignCenter("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡏⠀⠀⠀⠀⠀⠀⠀⠀⢀⡾⠿⣿⣿⣿⣿⣿⣿⣿⣿⠃⠀⠀⠀⠀⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",Constants.BOX1_WIDTH),
                PrintUtil.AlignCenter("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⡀⠈⣿⣿⣿⣿⣿⣿⡏⠀⠀⠀⠀⠀⠀⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",Constants.BOX1_WIDTH),
                PrintUtil.AlignCenter("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠠⠴⢿⣿⣿⣿⣿⣿⢿⣇⡀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",Constants.BOX1_WIDTH),
                PrintUtil.AlignCenter("⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣾⣿⣿⡿⠋⠁⣼⣿⣿⣿⣦⣄⣀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",Constants.BOX1_WIDTH),
                PrintUtil.AlignCenter("⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠗⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⠿⠟⠛⠋⠀⢀⣼⣿⡟⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",Constants.BOX1_WIDTH),
                PrintUtil.AlignCenter("⣿⣿⣿⣿⡿⠟⠛⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠻⣿⡿⠁⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿",Constants.BOX1_WIDTH),
                "",
                "신창섭이 공격해온다!"
            };

        box3Text[4] = new List<string>()
            {
                " "
            };
        //5
        box1Text[5] = new List<string>()
            {
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣾⣿⣿⣿⣿⣿⣍⠉⠉⠉⠉⠉⠉⠉⠉⠉⠉⠉⠉⠉⠉⠉⠉⠉⠉⣿⡍⠉⠉⠉⠉⠉⠉⠉⠉⠉⠉⠛⢿⣿⣦⡀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣿⣿⣿⣿⣿⣿⣿⣿⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣧⠽⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢻⣿⣿⡄⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡤⡼⠚⣅⠙⢶⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⢻⣿⣷⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⡀⠀⣻⣴⣤⣯⣔⣼⣄⣠⣴⣆⣀⣀⣀⣀⣀⣀⣀⣀⣀⣸⣿⣿⡄⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⣿⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀                                                      ",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠿⠿⢿⣿⣿⠃⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠛⠛⠻⣿⣟⣉⣉⣉⡹⠟⢯⡉⠹⡏⡉⠻⢍⣹⡩⠿⠉⢹⣇⡴⡿⠻⢲⢟⣙⡟⠀⠀⠀⢸⣿⣿⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠹⣷⠡⣄⣰⠐⠈⢧⠸⠲⠃⠀⡀⠀⢹⠀⠀⡀⠘⣫⣄⠋⢀⣷⣉⡿⠁⠀⠀⠀⢸⣿⣿⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠘⣷⠠⠽⠷⣄⡘⡧⠀⢀⠀⠈⠓⢾⡶⠚⠀⠀⣿⡸⠟⠁⢀⣸⠧⠀⠀ ⠀⣼⣿⣿⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⢘⡯⠀⠴⠾⠛⠳⢦⣼⡄⠀⠀⢸⡁⢀⣤⠖⠛⠳⢶⣶⠟⠁⠀⠀⠀ ⠀⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠘⠻⠿⣤⣀⠶⠖⠚⠋⠉⠟⠻⢶⣬⣖⡋⠉⠛⣂⢠⡟⠁⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠉⠓⡆⠀⢀⣀⡤⢶⣿⣿⡿⠿⣟⣙⣷⣾⣻⣦⡤⠀⠀⠀⠀ ⠀⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⢀⡾⢩⣛⣋⣙⡴⠋⠀⣿⡇⠀⠀⠉⠉⠉⠁⠀⠀⠀⠀⠀⠀ ⠀⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠉⠉⠉⠁⠀⠁⠀⠀ ⠀⡿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⡏⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠐⠷⠇⠀⠀⣀⣀⣀⣀⣠⣤⣤⣤⣤⣶⣶⣶⣿⣿⠀⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠻⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⣀⣀⣀⣀⣠⣤⣤⣤⣤⣶⣶⣶⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠿⠿⠿⠛⠛⠛⠁⠀⠀⠀⠀⠀⠀",
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠿⠿⠿⠟⠛⠛⠛⠉⠉⠉⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
                " ",
                " "
            };

        box3Text[5] = new List<string>()
            {
                "아무 키나 눌러 마을로 귀환"
            };
    }
}