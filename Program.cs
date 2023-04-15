using System.Net;

namespace 入门实践
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //控制台基础设置
            //隐藏光标
            Console.CursorVisible = false;
            //设置舞台（控制台）的大小
            int w = 50, h = 35;
            Console.SetWindowSize(w,h);//窗口大小
            Console.SetBufferSize(w,h);//缓冲区大小

            //多个场景
            int nowSenceID = 1;

            //结束场景显示的提示内容
            string gameOverInfo = "";
            while(true)
            {
                //不同的场景ID 进行不同的逻辑处理
                switch(nowSenceID)
                {
                    //开始场景
                    case 1:
                        Console.Clear();//每次切换场景时需要清空上个场景的残留
                        #region 开始场景逻辑
                        Console.SetCursorPosition(w / 2 - 5, 8);
                        Console.Write("~测试游戏~");

                        //设置当前选项（如：开始/退出游戏）的编号
                        int nowSelIndex = 0;

                        //因为要输入，我们可以构造一个开始界面自己的死循环
                        //专门用来处理 开始场景相关的逻辑
                        while (true)
                        {
                            bool isQuitWhile = false;//用一个标识来处理，想要在while循环 内部的switch逻辑执行时 能够退出外层while循环，改变标识即可
                            //显示内容
                            Console.SetCursorPosition(w / 2 - 4, 13);
                            //根据当前的编号来决定是否变红色
                            Console.ForegroundColor = (nowSelIndex == 0 ? ConsoleColor.Red : ConsoleColor.White);
                            Console.Write("开始游戏");
                            Console.SetCursorPosition(w / 2 - 4, 15);
                            Console.ForegroundColor = (nowSelIndex == 1 ? ConsoleColor.Red : ConsoleColor.White);
                            Console.Write("退出游戏");

                            //检测输入：检测输入的第一个内容，不会在控制台显示输入的内容
                            char input = Console.ReadKey(true).KeyChar;
                            switch (input)
                            {
                                case 'W':
                                case 'w':
                                    --nowSelIndex;
                                    if (nowSelIndex < 0)
                                    {
                                        nowSelIndex = 0;
                                    }
                                    break;
                                case 'S':
                                case 's':
                                    ++nowSelIndex;
                                    if (nowSelIndex > 1)
                                    {
                                        nowSelIndex = 1;
                                    }
                                    break;
                                //进入游戏
                                case 'J':
                                case 'j':
                                    if (nowSelIndex == 0)
                                    {
                                        //改变当前选择的场景ID
                                        nowSenceID = 2;
                                        //要退出内层while循环：在外部声明标识
                                        isQuitWhile = true;
                                    }
                                    else
                                    {
                                        Environment.Exit(0);
                                    }
                                    break;
                            }
                            //跳出循环，如果isQuitWhile为true，再跳到外层，此时不满足isQuitWhile条件，循环结束。
                            if(isQuitWhile)
                            {
                                break;
                            }
                        }
                        #endregion
                        break;


                    //游戏场景
                    case 2:
                        Console.Clear();
                        #region 游戏场景逻辑


                        #region 背景红墙
                        //设置颜色为红色
                        Console.ForegroundColor = ConsoleColor.Red;
                        //画墙
                        for(int i = 0; i < w; i += 2)
                        {
                            //上方
                            Console.SetCursorPosition(i, 0);
                            Console.Write("■");

                            //下方
                            Console.SetCursorPosition(i, h - 1);
                            Console.Write("■");

                            //中间
                            Console.SetCursorPosition(i, h - 6);
                            Console.Write("■");
                        }
                        for(int i = 0; i < h; i++)
                        {
                            //左墙
                            Console.SetCursorPosition(0, i);
                            Console.Write("■");

                            //右墙
                            Console.SetCursorPosition(w - 2, i);
                            Console.Write("■");
                        }
                        #endregion

                        #region BOSS属性
                        int bossX = 24;
                        int bossY = 15;
                        int bossAtkMin = 2;
                        int bossAtkMax = 20;
                        int bossHP = 100;
                        string bossIcon = "■";
                        //声明一个颜色变量
                        ConsoleColor bossColor = ConsoleColor.Green;
                        #endregion

                        #region 玩家属性
                        int playerX = 4;
                        int playerY = 5;
                        int playerAtkMin = 8;
                        int playerAtkMax = 12;
                        int playerHP = 100;
                        string playerIcon = "■";
                        ConsoleColor playerColor = ConsoleColor.Yellow;
                        
                        //玩家输入的内容
                        char playerInput;
                        
                        //战斗状态
                        bool isFight = false;
                        #endregion

                        #region 公主属性
                        int princessX = 24;
                        int princessY = 5;
                        string princessIcon = "☆";
                        ConsoleColor princessColor = ConsoleColor.Blue;
                        #endregion

                        bool isOver = false;
                        //游戏场景的死循环 用来检测玩家输入相关的循环
                        while (true)
                        {
                            #region BOSS图形绘制
                            //血量大于0才绘制
                            if (bossHP > 0)
                            {
                                //绘制BOSS图标
                                Console.SetCursorPosition(bossX, bossY);
                                Console.ForegroundColor = bossColor;
                                Console.Write(bossIcon);
                            }
                            #endregion
                            #region 公主相关
                            //BOSS不存在时，显示公主
                            else
                            {
                                Console.SetCursorPosition(princessX, princessY);
                                Console.ForegroundColor = princessColor;
                                Console.Write(princessIcon);
                            }
                            #endregion
                            //画出玩家
                            Console.SetCursorPosition(playerX, playerY);
                            Console.ForegroundColor = playerColor;
                            Console.Write(playerIcon);

                            //得到玩家输入
                            playerInput = Console.ReadKey(true).KeyChar;
                            //战斗状态处理什么逻辑
                            if (isFight)
                            {
                                #region 玩家战斗相关
                                //只会处理J键，直接用if就行
                                if(playerInput == 'J' || playerInput == 'j')
                                {
                                    //判断玩家或者怪物是否死亡，如果死亡，继续之后的流程
                                    if(playerHP <= 0)
                                    {
                                        //游戏结束，显示游戏结束画面，改变提示语
                                        nowSenceID = 3;
                                        gameOverInfo = "WASTED";
                                        break;
                                    }
                                    else if(bossHP <= 0)
                                    {
                                        //去营救公主
                                        //BOSS擦除
                                        //战斗逻辑结束
                                        Console.SetCursorPosition(bossX, bossY);
                                        Console.Write("  ");
                                        isFight = false;
                                    }
                                    else
                                    {
                                        //按J攻击
                                        //玩家打怪
                                        Random r = new Random();
                                        int atk = r.Next(playerAtkMin, playerAtkMax);
                                        //血量减攻击力
                                        bossHP -= atk;
                                        //打印信息
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        //先擦除这一行上一次显示的内容
                                        Console.SetCursorPosition(2, h - 4);
                                        Console.Write("                                             ");
                                        //再来写新的内容
                                        Console.SetCursorPosition(2, h - 4);
                                        Console.Write("你对BOSS造成了{0}点伤害,BOSS剩余血量{1}", atk, bossHP);

                                        //怪打玩家
                                        if (bossHP > 0)
                                        {
                                            //得到随机攻击
                                            atk = r.Next(bossAtkMin, bossAtkMax);
                                            playerHP -= atk;

                                            //打印信息
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            //先擦除这一行上一次显示的内容
                                            Console.SetCursorPosition(2, h - 3);
                                            Console.Write("                                             ");
                                            //再来写新的内容
                                            //这里还应写：如果BOSS把你打死应该显示的内容
                                            if (playerHP > 0)
                                            {
                                                Console.SetCursorPosition(2, h - 3);
                                                Console.Write("BOSS对你造成了{0}点伤害,你剩余血量{1}", atk, playerHP);
                                            }
                                            else
                                            {
                                                Console.SetCursorPosition(2, h - 3);
                                                Console.Write("很遗憾，你没有通过试炼");
                                            }
                                        }
                                        else//BOSS被打败
                                        {
                                            //显示胜利信息
                                            //擦除之前的战斗信息
                                            Console.SetCursorPosition(2, h - 5);
                                            Console.Write("                                             ");
                                            Console.SetCursorPosition(2, h - 4);
                                            Console.Write("                                             ");
                                            Console.SetCursorPosition(2, h - 3);
                                            Console.Write("                                             ");
                                            //显示胜利，让玩家继续
                                            Console.SetCursorPosition(2, h - 5);
                                            Console.Write("你战胜了BOSS！快去上方营救公主吧。");
                                            Console.SetCursorPosition(2, h - 4);
                                            Console.Write("请在公主身边按J继续");
                                        }
                                    }
                                }


                                #endregion
                            }
                            else//非战斗状态处理什么逻辑(isFight = false)
                            {
                                #region 玩家非战斗/移动相关
                                //擦除
                                Console.SetCursorPosition(playerX, playerY);
                                Console.Write("  ");
                                //改位置
                                switch (playerInput)
                                {
                                    case 'W':
                                    case 'w':
                                        --playerY;
                                        //不顶墙 判定
                                        if (playerY < 1)
                                        {
                                            playerY = 1;
                                        }
                                        //位置如果和BOSS重合，并且BOSS没有死
                                        else if (playerX == bossX && playerY == bossY && bossHP > 0)
                                        {
                                            //拉回去
                                            ++playerY;
                                        }
                                        //对公主同样
                                        else if(playerX == princessX && playerY == princessY && bossHP <= 0)
                                        {
                                            //拉回去
                                            ++playerY;
                                        }
                                        break;
                                    case 'S':
                                    case 's':
                                        ++playerY;
                                        if (playerY > h - 7)//不顶中间墙
                                        {
                                            playerY = h - 7;
                                        }
                                        else if (playerX == bossX && playerY == bossY && bossHP > 0)
                                        {
                                            --playerY;
                                        }
                                        //对公主同样
                                        else if (playerX == princessX && playerY == princessY && bossHP <= 0)
                                        {
                                            --playerY;
                                        }
                                        break;
                                    case 'A':
                                    case 'a':
                                        playerX -= 2;
                                        if (playerX < 2)
                                        {
                                            playerX = 2;
                                        }
                                        else if (playerX == bossX && playerY == bossY && bossHP > 0)
                                        {
                                            //拉回去
                                            playerX += 2;
                                        }
                                        //对公主同样
                                        else if (playerX == princessX && playerY == princessY && bossHP <= 0)
                                        {
                                            //拉回去
                                            playerX += 2;
                                        }
                                        break;
                                    case 'd':
                                    case 'D':
                                        playerX += 2;
                                        if (playerX > w - 4)//注意要减4
                                        {
                                            playerX = w - 4;
                                        }
                                        else if (playerX == bossX && playerY == bossY && bossHP > 0)
                                        {
                                            playerX -= 2;
                                        }
                                        //对公主同样
                                        else if (playerX == princessX && playerY == princessY && bossHP <= 0)
                                        {
                                            //拉回去
                                            playerX -= 2;
                                        }
                                        break;

                                    case 'J':
                                    case 'j':
                                        //开始战斗
                                        if ((playerX == bossX && playerY == bossY - 1 ||
                                           playerX == bossX && playerY == bossY + 1 ||
                                           playerX == bossX + 2 && playerY == bossY ||
                                           playerX == bossX - 2 && playerY == bossY) && bossHP > 0)
                                        {
                                            //进入战斗状态，跳出非战斗逻辑
                                            isFight = true;

                                            //显示战斗信息
                                            Console.SetCursorPosition(2, h - 5);
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.Write("可以和BOSS战斗了，按J键继续");
                                            Console.SetCursorPosition(2, h - 4);
                                            Console.Write("玩家当前血量为{0}", playerHP);
                                            Console.SetCursorPosition(2, h - 3);
                                            Console.Write("BOSS当前血量为{0}", bossHP);
                                        }
                                        //判断是否在公主身边
                                        else if((playerX == princessX && playerY == princessY - 1 ||
                                           playerX == princessX && playerY == princessY + 1 ||
                                           playerX == princessX + 2 && playerY == princessY ||
                                           playerX == princessX - 2 && playerY == princessY))
                                        {
                                            //改变场景ID/结束提示语
                                            nowSenceID = 3;
                                            gameOverInfo = "Bravo!";
                                            //跳出游戏场景死循环，回到主循环
                                            isOver = true;
                                            break;
                                        }
                                        break;
                                }
                                #endregion
                            }
                            //外层while循环逻辑,使得游戏进入结束界面
                            if (isOver)
                            {
                                break;
                            }
                        }
                        
                        #endregion
                        break;


                    //结束场景
                    case 3:
                        Console.Clear();
                        #region 结束场景逻辑
                        //标题显示
                        Console.SetCursorPosition(w / 2 - 5, 5);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("-Game Over-");

                        //可变内容显示，根据失败或成功显示不同内容
                        Console.SetCursorPosition(w/2 - 3, 7);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(gameOverInfo);

                        //结束场景死循环
                        int nowSelEndIndex = 0;
                        while (true)
                        {
                            bool isQuitEndWhile = false;

                            Console.SetCursorPosition(w / 2 - 6, 9);
                            Console.ForegroundColor = (nowSelEndIndex == 0 ? ConsoleColor.Red : ConsoleColor.White);
                            Console.Write("回到开始界面");
                            Console.SetCursorPosition(w / 2 - 4, 10);
                            Console.ForegroundColor = (nowSelEndIndex == 1 ? ConsoleColor.Red : ConsoleColor.White);
                            Console.Write("退出游戏");

                            char Input = Console.ReadKey(true).KeyChar;

                            switch (Input)
                            {
                                case 'W':
                                case 'w':
                                    --nowSelEndIndex;
                                    if(nowSelEndIndex < 0)
                                    {
                                        nowSelEndIndex = 0;
                                    }
                                    break;
                                case 'S':
                                case 's':
                                    ++nowSelEndIndex;
                                    if(nowSelEndIndex > 1)
                                    {
                                        nowSelEndIndex = 1;
                                    }
                                    break;
                                case 'J':
                                case 'j':
                                    if(nowSelEndIndex == 0) 
                                    {
                                        nowSenceID = 1;
                                        isQuitEndWhile = true;
                                        break;
                                    }
                                    else
                                    {
                                        Environment.Exit(0);
                                    }
                                    break;
                            }
                            //为了从switch循环跳出上一层的switch
                            if (isQuitEndWhile)
                            {
                                break;
                            }
                        }
                        #endregion
                        break;

                }
            }
        }
    }
}