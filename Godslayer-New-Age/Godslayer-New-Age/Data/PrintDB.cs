using System;
using System.Collections.Generic;

public static class PrintDB
{
    public static List<string> box1Data = new List<string>();
    /* How to Use
     * using Utils;
     * 
     * List<string> aaa = new List<string> { "`red,blue`a1", "a2", "`null,green`a3" }; << maximum 25lines
     * PrintDB.box1Data = aaa;
     *
     * element) "`red,blue`a1" << print "a1", font color: red, background color: blue
     * ` << backtick
     */

    // TODO: introScene 비활성화
    public static List<string> box2Data = new List<string>();

    //2 lines
    public static List<string> box3Data = new List<string>();

    public static List<string> GetPlayerStatus()
    {
        return new List<string>
            {
                $" {Player.Instance.Name} ({Player.Instance.PlayerJob})",
                $" Lv {Player.Instance.Level:D2} ({Player.Instance.EXP}/{Player.Instance.RequiredExp})",
                GaugeViewer(Player.Instance.EXP, Player.Instance.RequiredExp, "yellow"),
                PrintUtil.AlignCenter(new string('-', Constants.BOX2_WIDTH - 2), Constants.BOX2_WIDTH),
                "",
                $" `darkred,black`HP `gray,black`{Player.Instance.HP} / {Player.Instance.currentMaxHP} {PlayerInventory.GetStatBonusToStr(StatType.HP)}",
                GaugeViewer(Player.Instance.HP, Player.Instance.MaxHP, "red"),
                $" `darkblue,black`MP `gray,black`{Player.Instance.MP} / {Player.Instance.MaxMP}",
                GaugeViewer(Player.Instance.MP, Player.Instance.MaxMP, "blue"),
                "",
                PrintUtil.AlignCenter(new string('-', Constants.BOX2_WIDTH - 2), Constants.BOX2_WIDTH),
                $" `red,black`ATK `gray,black`{Player.Instance.currentATK} {PlayerInventory.GetStatBonusToStr(StatType.ATK)}",
                $" `blue,black`DEF `gray,black`{Player.Instance.currentDEF} {PlayerInventory.GetStatBonusToStr(StatType.DEF)}",
                $" `magenta,black`CRT `gray,black`{Player.Instance.currentCRT} {PlayerInventory.GetStatBonusToStr(StatType.CRT)}",
                $" `green,black`EVA `gray,black`{Player.Instance.currentEVA} {PlayerInventory.GetStatBonusToStr(StatType.EVA)}",
                $" `cyan,black`SPD `gray,black`{Player.Instance.currentSPD} {PlayerInventory.GetStatBonusToStr(StatType.SPD)}",
                "",
                PrintUtil.AlignCenter(new string('-', Constants.BOX2_WIDTH - 2), Constants.BOX2_WIDTH),
                " [장비]",
                $" {PlayerInventory.GetEquippedName(ItemType.Weapon)}",
                $" {PlayerInventory.GetEquippedName(ItemType.Armor)}",
                $" {PlayerInventory.GetEquippedName(ItemType.Accessory)}",
                "",
                PrintUtil.AlignCenter(new string('-', Constants.BOX2_WIDTH - 2), Constants.BOX2_WIDTH),
                " Gold) " + PrintUtil.AlignRight($"{Player.Instance.Gold} G", Constants.BOX2_WIDTH - 8)
            };
    }

    private static string GaugeViewer(float nowStat, float maxStat, string color)
    {
        int gaugeLength = (int)Math.Floor(nowStat / maxStat * (Constants.BOX2_WIDTH - 2));
        int emptyLength = (Constants.BOX2_WIDTH - 2) - gaugeLength;

        string gauge = new string(' ', gaugeLength);
        string empty = new string(' ', emptyLength);

        return $" `null,{color}`{gauge}`null,gray`{empty}`null,black` ";
    }
}