namespace MyMonkeyApp.Helpers;

/// <summary>
/// ASCII 아트와 UI 유틸리티를 관리하는 정적 클래스
/// </summary>
public static class AsciiArtHelper
{
    private static readonly Random _random = new();

    /// <summary>
    /// 무작위 ASCII 아트를 반환합니다.
    /// </summary>
    /// <returns>ASCII 아트 문자열</returns>
    public static string GetRandomAsciiArt()
    {
        var arts = new[]
        {
            GetMonkeyWelcome(),
            GetMonkeyHanging(),
            GetMonkeyFace(),
            GetBananaArt(),
            GetJungleScene()
        };

        return arts[_random.Next(arts.Length)];
    }

    /// <summary>
    /// 환영 메시지용 원숭이 ASCII 아트
    /// </summary>
    private static string GetMonkeyWelcome()
    {
        return @"
    🐵 WELCOME TO MONKEY APP! 🐵
   ═══════════════════════════════
         .-""-.
        /     \
       | () () |
        \  ^  /
         |||||
         |||||
    _____|||||_____
   /               \
  |  Welcome to the |
  |  Monkey World!  |
   \_______________/
        |      |
        🐒    🙈
";
    }

    /// <summary>
    /// 매달린 원숭이 ASCII 아트
    /// </summary>
    private static string GetMonkeyHanging()
    {
        return @"
      🌿🌿🌿🌿🌿🌿🌿
        |   |   |
        🐵  🐒  🙈
       / \ / \ / \
      Hang in there!
";
    }

    /// <summary>
    /// 원숭이 얼굴 ASCII 아트
    /// </summary>
    private static string GetMonkeyFace()
    {
        return @"
        .-"""""-.
       /  o   o  \
      |     <     |
       \   ___   /
        '-.._..-'
         _|   |_
        |  🐵  |
        '-------'
    Time for monkey business!
";
    }

    /// <summary>
    /// 바나나 ASCII 아트
    /// </summary>
    private static string GetBananaArt()
    {
        return @"
           🍌
          /  \
         /    \
        |  🐒  |
         \    /
          \  /
           \/
    Bananas for everyone! 🍌🍌🍌
";
    }

    /// <summary>
    /// 정글 씬 ASCII 아트
    /// </summary>
    private static string GetJungleScene()
    {
        return @"
    🌴🌿🌳🌿🌴🌿🌳🌿🌴
        🐵    🙊    🙉
       /|\   /|\   /|\
        |     |     |
    🌿🌱🌿🌱🌿🌱🌿🌱🌿🌱🌿
     Welcome to the jungle!
";
    }

    /// <summary>
    /// 컬러풀한 제목 표시
    /// </summary>
    public static void DisplayTitle()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(GetRandomAsciiArt());
        Console.ResetColor();
        Console.WriteLine();
    }

    /// <summary>
    /// 메뉴 옵션을 이쁘게 표시
    /// </summary>
    public static void DisplayMenu()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("┌─────────────────────────────────────┐");
        Console.WriteLine("│          🐵 MONKEY MENU 🐵         │");
        Console.WriteLine("├─────────────────────────────────────┤");
        Console.WriteLine("│  1. 🐒 모든 원숭이 나열             │");
        Console.WriteLine("│  2. 🔍 이름으로 원숭이 검색         │");
        Console.WriteLine("│  3. 🎲 무작위 원숭이 선택           │");
        Console.WriteLine("│  4. 📊 액세스 통계 보기             │");
        Console.WriteLine("│  5. 🌐 서버 연결 상태 확인          │");
        Console.WriteLine("│  6. 🚪 앱 종료                      │");
        Console.WriteLine("└─────────────────────────────────────┘");
        Console.ResetColor();
        Console.Write("\n메뉴를 선택하세요 (1-6): ");
    }

    /// <summary>
    /// 로딩 애니메이션 표시
    /// </summary>
    public static async Task ShowLoadingAnimation(string message)
    {
        Console.Write(message);
        var loadingChars = new[] { '|', '/', '-', '\\' };
        
        for (int i = 0; i < 8; i++)
        {
            Console.Write($" {loadingChars[i % 4]}");
            await Task.Delay(200);
            Console.Write("\b\b");
        }
        Console.WriteLine(" ✅");
    }

    /// <summary>
    /// 성공 메시지 표시
    /// </summary>
    public static void ShowSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"✅ {message}");
        Console.ResetColor();
    }

    /// <summary>
    /// 오류 메시지 표시
    /// </summary>
    public static void ShowError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"❌ {message}");
        Console.ResetColor();
    }

    /// <summary>
    /// 정보 메시지 표시
    /// </summary>
    public static void ShowInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"ℹ️  {message}");
        Console.ResetColor();
    }

    /// <summary>
    /// 원숭이 정보를 이쁘게 표시
    /// </summary>
    public static void DisplayMonkeyInfo(Models.Monkey monkey)
    {
        Console.WriteLine("\n┌─────────────────────────────────────────────────────────────┐");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"│ 🐵 {monkey.Name.PadRight(55)} │");
        Console.ResetColor();
        Console.WriteLine("├─────────────────────────────────────────────────────────────┤");
        Console.WriteLine($"│ 📍 위치: {monkey.Location.PadRight(49)} │");
        Console.WriteLine($"│ 👥 개체수: {monkey.Population:N0}마리".PadRight(58) + " │");
        Console.WriteLine($"│ 🌍 좌표: ({monkey.Latitude:F2}, {monkey.Longitude:F2})".PadRight(58) + " │");
        Console.WriteLine("├─────────────────────────────────────────────────────────────┤");
        
        // 세부 정보를 여러 줄로 나누어 표시
        var details = monkey.Details;
        var maxWidth = 55;
        var words = details.Split(' ');
        var currentLine = "";
        
        foreach (var word in words)
        {
            if (currentLine.Length + word.Length + 1 <= maxWidth)
            {
                currentLine += (currentLine.Length > 0 ? " " : "") + word;
            }
            else
            {
                Console.WriteLine($"│ {currentLine.PadRight(57)} │");
                currentLine = word;
            }
        }
        
        if (currentLine.Length > 0)
        {
            Console.WriteLine($"│ {currentLine.PadRight(57)} │");
        }
        
        Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
    }

    /// <summary>
    /// 계속하려면 키를 누르라는 메시지
    /// </summary>
    public static void PressAnyKey()
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
        Console.ResetColor();
        Console.ReadKey();
    }
}