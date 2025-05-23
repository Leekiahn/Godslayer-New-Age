using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



/*
 
Console.Write("세이브 파일 고르기 : ");        //세이브 파일 고르기(예시)
select = int.Parse(Console.ReadLine());
if(select == 1) { saveName1 = "player1.dat"; saveName2 = "shop1.dat"; }
else if(select == 2) { saveName1 = "player2.dat"; saveName2 = "shop2.dat"; }
else if(select == 3) { saveName1 = "player3.dat"; saveName2 = "shop3.dat"; }


Player player1 = SaveLoad.Instance().LoadPlayer(saveName1);  //로드
Shop shop = SaveLoad.Instance().LoadShop(saveName2);
 
SaveLoad.Instance().SavePlayer(player, saveName1);      //세이브

 */


internal class SaveLoad
{
    private static SaveLoad SaveManager;
    public static SaveLoad Instance()
    {
        if (SaveManager == null)
        {
            SaveManager = new SaveLoad();
        }
        return SaveManager;
    }

    public static void Delete(string fileName)
    {
        if (File.Exists(fileName)) { File.Delete(fileName); }
    }

    public static void SavePlayer(Player player, string fileName)
    {
        using (FileStream fs = new FileStream(fileName, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, player);
        }
    }

    public static Player LoadPlayer(string fileName)
    {
        if (!File.Exists(fileName)) return new Player(0, null, 0, 0f, 100f, 100f, 50f, 50f, 20f, 5f, 100, 20f, 1.5f, 1f, 50f, true);

        FileInfo fileInfo = new FileInfo(fileName);
        if (fileInfo.Length == 0) return new Player(0, null, 0, 0f, 100f, 100f, 50f, 50f, 20f, 5f, 100, 20f, 1.5f, 1f, 50f, true); // 빈 파일이면 기본 인스턴스 반환

        using (FileStream fs = new FileStream(fileName, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return (Player)formatter.Deserialize(fs);
        }
    }
}

