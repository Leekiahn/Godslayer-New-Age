using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class SceneManager
{
    private static Stack<IScene> sceneStack = new Stack<IScene>();
    private static int currentPhase = 0;

    public static void Run()
    {
        sceneStack.Push(new IntroScene());

        while (sceneStack.Count > 0)
        {
            Console.Clear();

            IScene currentScene = sceneStack.Peek();
            GameState next = currentScene.Run(currentPhase);

            switch (next)
            {
                case GameState.Retry:
                    continue;

                case GameState.Pop:
                    sceneStack.Pop();
                    currentPhase = 0;
                    continue;

                case GameState.Exit:
                    sceneStack.Clear();
                    return;

                default:
                    ChangeScene(next);
                    break;
            }
        }

        Console.WriteLine("게임을 종료");
    }

    public static void SetPhase(int nextPhase)
    {
        currentPhase = nextPhase;
    }

    private static void ChangeScene(GameState next)
    {
        if (sceneStack.Count > 0 && sceneStack.Peek().SceneType == next)
        {
            // 같은 씬 → phase만 변경
            currentPhase = 0; // or 유지
        }
        else
        {
            // 다른 씬 → 새로운 인스턴스 push
            sceneStack.Push(CreateSceneByState(next));
            currentPhase = 0;
        }
    }

    private static IScene CreateSceneByState(GameState state)
    {
        switch (state)
        {
            case GameState.Intro:
                BGM_Player.Instance().Play_Intro_Loop();
                return new IntroScene();
            case GameState.Start:
                return new StartScene();
            case GameState.CreateCharacter:
                BGM_Player.Instance().Play_Finally_Awake();
                return new CreateCharacterScene();
            case GameState.Save:
                BGM_Player.Instance().Play_SaveLoad_Loop();
                return new SaveScene();
            case GameState.Load:
                BGM_Player.Instance().Play_SaveLoad_Loop();
                return new LoadScene();
            case GameState.Main:
                //BGM_Player.Instance().Play_Main_Loop();
                return new MainScene();
            case GameState.Inventory:
                return new InventoryScene();
            case GameState.Shop:
                return new ShopScene();
            case GameState.Dungeon:
                BGM_Player.Instance().Play_Dungeon();
                return new DungeonScene();
            case GameState.Rest:
                BGM_Player.Instance().Play_Rest();
                return new RestScene();
            case GameState.Maple:
                return new MapleScene();
            case GameState.Space:
                return new SpaceScene();
            case GameState.LOL:
                return new LOLScene();
            case GameState.GameOver:
                return new GameOverScene();
            case GameState.Clear:
                BGM_Player.Instance().Play_Clear_Loop();
                return new ClearScene();
            default:
                throw new InvalidOperationException("등록되지 않은 씬입니다.");
        }
    }
}