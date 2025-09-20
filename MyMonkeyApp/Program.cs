using MyMonkeyApp.Models;
using MyMonkeyApp.Helpers;

namespace MyMonkeyApp;

/// <summary>
/// Monkey 콘솔 애플리케이션의 메인 클래스
/// </summary>
class Program
{
    /// <summary>
    /// 애플리케이션 진입점
    /// </summary>
    /// <param name="args">명령줄 인수</param>
    static async Task Main(string[] args)
    {
        try
        {
            await RunMonkeyAppAsync();
        }
        catch (Exception ex)
        {
            AsciiArtHelper.ShowError($"애플리케이션 실행 중 오류가 발생했습니다: {ex.Message}");
            AsciiArtHelper.PressAnyKey();
        }
    }

    /// <summary>
    /// 메인 애플리케이션 루프
    /// </summary>
    private static async Task RunMonkeyAppAsync()
    {
        bool isRunning = true;

        while (isRunning)
        {
            AsciiArtHelper.DisplayTitle();
            AsciiArtHelper.DisplayMenu();

            var input = Console.ReadLine()?.Trim();

            switch (input)
            {
                case "1":
                    await ShowAllMonkeysAsync();
                    break;
                case "2":
                    await SearchMonkeyByNameAsync();
                    break;
                case "3":
                    await ShowRandomMonkeyAsync();
                    break;
                case "4":
                    await ShowAccessStatisticsAsync();
                    break;
                case "5":
                    await CheckServerConnectionAsync();
                    break;
                case "6":
                    isRunning = false;
                    ShowExitMessage();
                    break;
                default:
                    AsciiArtHelper.ShowError("잘못된 선택입니다. 1-6 사이의 숫자를 입력해주세요.");
                    AsciiArtHelper.PressAnyKey();
                    break;
            }
        }
    }

    /// <summary>
    /// 모든 원숭이 목록을 표시합니다.
    /// </summary>
    private static async Task ShowAllMonkeysAsync()
    {
        try
        {
            await AsciiArtHelper.ShowLoadingAnimation("원숭이 데이터 로딩 중");
            
            var monkeys = await MonkeyHelper.GetAllMonkeysAsync();
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("🐵 모든 원숭이 목록 🐵");
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.ResetColor();
            
            if (monkeys.Count == 0)
            {
                AsciiArtHelper.ShowInfo("사용 가능한 원숭이가 없습니다.");
            }
            else
            {
                int index = 1;
                foreach (var monkey in monkeys.OrderBy(m => m.Name))
                {
                    Console.WriteLine($"{index}. {monkey}");
                    index++;
                }
                
                AsciiArtHelper.ShowSuccess($"총 {monkeys.Count}마리의 원숭이를 찾았습니다!");
                
                // 통계 정보 표시
                Console.WriteLine("\n📊 간단한 통계:");
                var stats = await MonkeyHelper.GetStatisticsAsync();
                Console.WriteLine($"   총 개체수: {stats.TotalPopulation:N0}마리");
                Console.WriteLine($"   평균 개체수: {stats.AveragePopulation:F0}마리");
                
                if (stats.MostPopulousSpecies != null)
                {
                    Console.WriteLine($"   최다 개체수: {stats.MostPopulousSpecies.Name} ({stats.MostPopulousSpecies.Population:N0}마리)");
                }
            }
        }
        catch (Exception ex)
        {
            AsciiArtHelper.ShowError($"원숭이 목록을 가져오는데 실패했습니다: {ex.Message}");
        }
        
        AsciiArtHelper.PressAnyKey();
    }

    /// <summary>
    /// 이름으로 원숭이를 검색합니다.
    /// </summary>
    private static async Task SearchMonkeyByNameAsync()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("🔍 원숭이 이름 검색");
        Console.WriteLine("═══════════════════════");
        Console.ResetColor();
        
        Console.Write("검색할 원숭이 이름을 입력하세요: ");
        var name = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            AsciiArtHelper.ShowError("원숭이 이름을 입력해주세요.");
            AsciiArtHelper.PressAnyKey();
            return;
        }

        try
        {
            await AsciiArtHelper.ShowLoadingAnimation($"'{name}' 검색 중");
            
            var monkey = await MonkeyHelper.GetMonkeyByNameAsync(name);

            if (monkey != null)
            {
                AsciiArtHelper.ShowSuccess($"'{monkey.Name}' 원숭이를 찾았습니다!");
                AsciiArtHelper.DisplayMonkeyInfo(monkey);
                
                // 액세스 횟수 표시
                var accessCount = MonkeyHelper.GetAccessCount(monkey.Name);
                Console.WriteLine($"\n📈 이 원숭이는 지금까지 {accessCount}번 조회되었습니다.");
            }
            else
            {
                AsciiArtHelper.ShowError($"'{name}' 이름의 원숭이를 찾을 수 없습니다.");
                
                // 비슷한 이름 제안
                var allMonkeys = await MonkeyHelper.GetAllMonkeysAsync();
                var suggestions = allMonkeys
                    .Where(m => m.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .Take(3)
                    .ToList();

                if (suggestions.Count > 0)
                {
                    Console.WriteLine("\n💡 혹시 이런 원숭이를 찾고 계신가요?");
                    foreach (var suggestion in suggestions)
                    {
                        Console.WriteLine($"   - {suggestion.Name}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            AsciiArtHelper.ShowError($"검색 중 오류가 발생했습니다: {ex.Message}");
        }

        AsciiArtHelper.PressAnyKey();
    }

    /// <summary>
    /// 무작위 원숭이를 표시합니다.
    /// </summary>
    private static async Task ShowRandomMonkeyAsync()
    {
        try
        {
            await AsciiArtHelper.ShowLoadingAnimation("무작위 원숭이 선택 중");
            
            var monkey = await MonkeyHelper.GetRandomMonkeyAsync();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("🎲 무작위 원숭이 🎲");
            Console.WriteLine("═══════════════════════");
            Console.ResetColor();
            
            AsciiArtHelper.DisplayMonkeyInfo(monkey);
            
            // 재미있는 랜덤 팩트 추가
            var randomFacts = new[]
            {
                "🎯 행운의 원숭이가 선택되었습니다!",
                "🌟 오늘의 특별한 원숭이입니다!",
                "🎪 이 원숭이와 함께 모험을 떠나보세요!",
                "🎁 놀라운 원숭이 친구를 만났습니다!",
                "🚀 우주에서도 유명한 원숭이입니다!"
            };
            
            var randomFact = randomFacts[new Random().Next(randomFacts.Length)];
            AsciiArtHelper.ShowInfo(randomFact);
            
            // 액세스 횟수 표시
            var accessCount = MonkeyHelper.GetAccessCount(monkey.Name);
            Console.WriteLine($"\n📈 이 원숭이는 지금까지 {accessCount}번 선택되었습니다.");
        }
        catch (Exception ex)
        {
            AsciiArtHelper.ShowError($"무작위 원숭이 선택에 실패했습니다: {ex.Message}");
        }

        AsciiArtHelper.PressAnyKey();
    }

    /// <summary>
    /// 액세스 통계를 표시합니다.
    /// </summary>
    private static async Task ShowAccessStatisticsAsync()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("📊 액세스 통계 및 애플리케이션 정보");
        Console.WriteLine("═══════════════════════════════════════════");
        Console.ResetColor();

        try
        {
            // 액세스 통계 표시
            MonkeyHelper.DisplayAccessStatistics();
            
            Console.WriteLine();
            
            // 전체 통계 표시
            var stats = await MonkeyHelper.GetStatisticsAsync();
            Console.WriteLine(stats.ToString());
            
            // 인기 원숭이 순위
            var popularMonkeys = MonkeyHelper.GetMostPopularMonkeys(5);
            if (popularMonkeys.Count > 0)
            {
                Console.WriteLine("🏆 인기 원숭이 Top 5:");
                Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━");
                for (int i = 0; i < popularMonkeys.Count; i++)
                {
                    var (name, count) = popularMonkeys[i];
                    var medal = (i + 1) switch
                    {
                        1 => "🥇",
                        2 => "🥈",
                        3 => "🥉",
                        _ => $"{i + 1}위"
                    };
                    Console.WriteLine($"{medal} {name}: {count}회");
                }
            }
        }
        catch (Exception ex)
        {
            AsciiArtHelper.ShowError($"통계 정보를 가져오는데 실패했습니다: {ex.Message}");
        }

        AsciiArtHelper.PressAnyKey();
    }

    /// <summary>
    /// 서버 연결 상태를 확인합니다.
    /// </summary>
    private static async Task CheckServerConnectionAsync()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("🌐 서버 연결 상태 확인");
        Console.WriteLine("═══════════════════════");
        Console.ResetColor();

        try
        {
            await AsciiArtHelper.ShowLoadingAnimation("MCP 서버 연결 확인 중");
            
            bool isConnected = await MonkeyHelper.CheckServerConnectionAsync();
            
            if (isConnected)
            {
                AsciiArtHelper.ShowSuccess("MCP 서버에 성공적으로 연결되었습니다!");
                
                // 추가 정보 표시
                var monkeys = await MonkeyHelper.GetAllMonkeysAsync();
                Console.WriteLine($"✅ {monkeys.Count}마리의 원숭이 데이터 사용 가능");
                Console.WriteLine($"🕒 마지막 업데이트: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            }
            else
            {
                AsciiArtHelper.ShowError("MCP 서버 연결에 실패했습니다.");
                AsciiArtHelper.ShowInfo("폴백 데이터를 사용하여 애플리케이션이 계속 실행됩니다.");
            }
        }
        catch (Exception ex)
        {
            AsciiArtHelper.ShowError($"연결 확인 중 오류가 발생했습니다: {ex.Message}");
        }

        AsciiArtHelper.PressAnyKey();
    }

    /// <summary>
    /// 종료 메시지를 표시합니다.
    /// </summary>
    private static void ShowExitMessage()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        
        var exitArt = @"
    🐵 안녕히 가세요! 🐵
   ═══════════════════════
         .-""-.
        /     \
       | () () |
        \  ^  /
         |||||
      Goodbye!
         👋
        
    Monkey App을 이용해주셔서
        감사합니다! 🙏
    
    또 만나요! 🐒✨
";
        
        Console.WriteLine(exitArt);
        Console.ResetColor();
        
        // 마지막 통계 표시
        var stats = MonkeyHelper.GetAccessStatistics();
        if (stats.Count > 0)
        {
            Console.WriteLine($"이번 세션에서 {stats.Values.Sum()}번의 원숭이 조회가 있었습니다.");
            Console.WriteLine($"총 {stats.Count}마리의 서로 다른 원숭이를 만나셨네요!");
        }
        
        Console.WriteLine("\n잠시 후 애플리케이션이 종료됩니다...");
        Task.Delay(3000).Wait();
    }
}