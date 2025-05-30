using System;
using System.Collections.Generic;

internal class LoadScene : IScene
{
    public GameState SceneType => GameState.Load;

    public GameState Run(int phase)
    {
        PrintDB.box1Data = box1Text.ContainsKey(phase) ? box1Text[phase] : new List<string>();
        PrintDB.box3Data = box3Text.ContainsKey(phase) ? box3Text[phase] : new List<string>();

        PrintUtil.CreateBox();
        // 입력에 따라 다음 상태 반환
        string input = Console.ReadLine();
        if (input == "1") { if (SaveLoad.LoadPlayer("player1.dat").Name != null) { Player.SetInstance(SaveLoad.LoadPlayer("player1.dat")); return GameState.Main; } }
        if (input == "2") { if (SaveLoad.LoadPlayer("player2.dat").Name != null) { Player.SetInstance(SaveLoad.LoadPlayer("player2.dat")); return GameState.Main; } }
        if (input == "3") { if (SaveLoad.LoadPlayer("player3.dat").Name != null) { Player.SetInstance(SaveLoad.LoadPlayer("player3.dat")); return GameState.Main; } }
        if (input == "0") return GameState.Pop;

        

        return GameState.Retry; // 다시 실행
    }

    public Dictionary<int, List<string>> box1Text = new Dictionary<int, List<string>>();
    public Dictionary<int, List<string>> box3Text = new Dictionary<int, List<string>>();

    Player player1 = SaveLoad.LoadPlayer("player1.dat");
    Player player2 = SaveLoad.LoadPlayer("player2.dat");
    Player player3 = SaveLoad.LoadPlayer("player3.dat");

    public LoadScene()
    {
        box1Text[0] = new List<string>()
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

        box3Text[0] = new List<string>()
            {
                "데이터를 불러올 슬롯을 선택해주세요(1~3) (0. 돌아가기)"
            };

        box1Text[1] = new List<string>()
            {
                ""
            };

        box3Text[1] = new List<string>()
            {
                ""
            };
    }
}