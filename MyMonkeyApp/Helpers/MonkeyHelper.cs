using MyMonkeyApp.Models;
using MyMonkeyApp.Data;
using System.Text.Json;

namespace MyMonkeyApp.Helpers;

/// <summary>
/// 원숭이 데이터 관리를 위한 정적 도우미 클래스
/// Monkey MCP 서버와 연동하여 데이터를 가져오고 관리합니다.
/// </summary>
public static class MonkeyHelper
{
    private static List<Monkey>? _cachedMonkeys;
    private static readonly Dictionary<string, int> _accessCount = new();
    private static readonly Random _random = new();
    private static DateTime _lastCacheUpdate = DateTime.MinValue;
    private static readonly TimeSpan _cacheExpiry = TimeSpan.FromMinutes(30);
    private static readonly MonkeyRepository _repository = new();

    /// <summary>
    /// 모든 원숭이 데이터를 가져옵니다. 캐시된 데이터가 있으면 반환하고, 없으면 MCP 서버에서 새로 가져옵니다.
    /// </summary>
    /// <returns>모든 원숭이 목록</returns>
    public static async Task<List<Monkey>> GetAllMonkeysAsync()
    {
        // 캐시가 유효한지 확인
        if (_cachedMonkeys != null && DateTime.Now - _lastCacheUpdate < _cacheExpiry)
        {
            return _cachedMonkeys;
        }

        try
        {
            // Repository를 통해 MCP 서버에서 데이터 가져오기
            _cachedMonkeys = await _repository.GetAllMonkeysAsync();
            _lastCacheUpdate = DateTime.Now;
            
            Console.WriteLine($"✅ {_cachedMonkeys.Count}마리의 원숭이 데이터를 로드했습니다.");
            return _cachedMonkeys;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ 원숭이 데이터 로드 중 오류: {ex.Message}");
            
            // 실패 시 하드코딩된 기본 데이터 반환
            return GetFallbackMonkeys();
        }
    }

    /// <summary>
    /// 이름으로 특정 원숭이를 검색합니다.
    /// </summary>
    /// <param name="name">검색할 원숭이 이름 (대소문자 구분 안함)</param>
    /// <returns>찾은 원숭이 또는 null</returns>
    public static async Task<Monkey?> GetMonkeyByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        var monkeys = await GetAllMonkeysAsync();
        var foundMonkey = monkeys.FirstOrDefault(m => 
            string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));

        if (foundMonkey != null)
        {
            IncrementAccessCount(foundMonkey.Name);
            Console.WriteLine($"🔍 '{foundMonkey.Name}' 원숭이를 찾았습니다.");
        }
        else
        {
            Console.WriteLine($"❌ '{name}' 이름의 원숭이를 찾을 수 없습니다.");
        }

        return foundMonkey;
    }

    /// <summary>
    /// 무작위로 원숭이를 선택합니다.
    /// </summary>
    /// <returns>무작위 원숭이</returns>
    public static async Task<Monkey> GetRandomMonkeyAsync()
    {
        var monkeys = await GetAllMonkeysAsync();
        
        if (monkeys.Count == 0)
            throw new InvalidOperationException("사용 가능한 원숭이가 없습니다.");

        var randomMonkey = monkeys[_random.Next(monkeys.Count)];
        IncrementAccessCount(randomMonkey.Name);
        
        Console.WriteLine($"🎲 무작위로 '{randomMonkey.Name}' 원숭이가 선택되었습니다!");
        return randomMonkey;
    }

    /// <summary>
    /// 특정 원숭이의 액세스 횟수를 증가시킵니다.
    /// </summary>
    /// <param name="monkeyName">원숭이 이름</param>
    private static void IncrementAccessCount(string monkeyName)
    {
        if (_accessCount.ContainsKey(monkeyName))
        {
            _accessCount[monkeyName]++;
        }
        else
        {
            _accessCount[monkeyName] = 1;
        }
    }

    /// <summary>
    /// 특정 원숭이의 액세스 횟수를 반환합니다.
    /// </summary>
    /// <param name="monkeyName">원숭이 이름</param>
    /// <returns>액세스 횟수</returns>
    public static int GetAccessCount(string monkeyName)
    {
        return _accessCount.TryGetValue(monkeyName, out int count) ? count : 0;
    }

    /// <summary>
    /// 모든 원숭이의 액세스 통계를 반환합니다.
    /// </summary>
    /// <returns>액세스 통계 딕셔너리</returns>
    public static Dictionary<string, int> GetAccessStatistics()
    {
        return new Dictionary<string, int>(_accessCount);
    }

    /// <summary>
    /// 가장 인기 있는 원숭이 목록을 반환합니다.
    /// </summary>
    /// <param name="topCount">반환할 상위 개수</param>
    /// <returns>인기 원숭이 목록</returns>
    public static List<(string Name, int AccessCount)> GetMostPopularMonkeys(int topCount = 5)
    {
        return _accessCount
            .OrderByDescending(kvp => kvp.Value)
            .Take(topCount)
            .Select(kvp => (kvp.Key, kvp.Value))
            .ToList();
    }

    /// <summary>
    /// 원숭이 통계 정보를 생성합니다.
    /// </summary>
    /// <returns>통계 정보 객체</returns>
    public static async Task<MonkeyStatistics> GetStatisticsAsync()
    {
        var monkeys = await GetAllMonkeysAsync();
        
        var statistics = new MonkeyStatistics
        {
            TotalSpecies = monkeys.Count,
            TotalPopulation = monkeys.Sum(m => m.Population),
            MostPopulousSpecies = monkeys.OrderByDescending(m => m.Population).FirstOrDefault(),
            LeastPopulousSpecies = monkeys.OrderBy(m => m.Population).FirstOrDefault(),
            EndangeredSpeciesCount = monkeys.Count(m => m.Population < 2000) // 2000 이하를 멸종위기로 가정
        };

        // 지역별 분포 계산
        var regionGroups = monkeys
            .GroupBy(m => m.Location)
            .ToDictionary(g => g.Key, g => g.Count());
        
        statistics.RegionalDistribution = regionGroups;

        return statistics;
    }

    /// <summary>
    /// 캐시를 초기화합니다.
    /// </summary>
    public static void ClearCache()
    {
        _cachedMonkeys = null;
        _lastCacheUpdate = DateTime.MinValue;
        Console.WriteLine("🗑️ 원숭이 데이터 캐시가 초기화되었습니다.");
    }

    /// <summary>
    /// 액세스 통계를 초기화합니다.
    /// </summary>
    public static void ClearAccessStatistics()
    {
        _accessCount.Clear();
        Console.WriteLine("📊 액세스 통계가 초기화되었습니다.");
    }

    /// <summary>
    /// 액세스 통계를 이쁘게 표시합니다.
    /// </summary>
    public static void DisplayAccessStatistics()
    {
        Console.WriteLine("\n📊 원숭이 액세스 통계");
        Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

        if (_accessCount.Count == 0)
        {
            Console.WriteLine("아직 액세스된 원숭이가 없습니다. 🐵");
            return;
        }

        var sortedStats = _accessCount
            .OrderByDescending(kvp => kvp.Value)
            .ToList();

        Console.WriteLine($"총 {sortedStats.Count}마리의 원숭이가 액세스되었습니다.\n");

        int rank = 1;
        foreach (var stat in sortedStats)
        {
            var medal = rank switch
            {
                1 => "🥇",
                2 => "🥈", 
                3 => "🥉",
                _ => "  "
            };

            Console.WriteLine($"{medal} {rank}위: {stat.Key} ({stat.Value}회)");
            rank++;
        }

        var totalAccesses = _accessCount.Values.Sum();
        var averageAccesses = totalAccesses / (double)_accessCount.Count;
        Console.WriteLine($"\n총 액세스 횟수: {totalAccesses}회");
        Console.WriteLine($"평균 액세스 횟수: {averageAccesses:F1}회");
    }

    /// <summary>
    /// MCP 서버 연결 상태를 확인합니다.
    /// </summary>
    /// <returns>연결 상태</returns>
    public static async Task<bool> CheckServerConnectionAsync()
    {
        try
        {
            bool isAvailable = await _repository.IsServerAvailableAsync();
            
            if (isAvailable)
            {
                Console.WriteLine("✅ MCP 서버 연결 상태: 정상");
            }
            else
            {
                Console.WriteLine("❌ MCP 서버 연결 상태: 연결 실패");
            }
            
            return isAvailable;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ MCP 서버 연결 확인 중 오류: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// MCP 서버 연결 실패 시 사용할 폴백 데이터
    /// </summary>
    /// <returns>기본 원숭이 목록</returns>
    private static List<Monkey> GetFallbackMonkeys()
    {
        Console.WriteLine("⚠️ MCP 서버 연결 실패. 기본 데이터를 사용합니다.");
        return new List<Monkey>
        {
            new() {
                Name = "Baboon",
                Location = "Africa & Asia",
                Details = "Baboons are African and Arabian Old World monkeys belonging to the genus Papio, part of the subfamily Cercopithecinae.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/baboon.jpg",
                Population = 10000,
                Latitude = -8.783195,
                Longitude = 34.508523
            },
            new() {
                Name = "Japanese Macaque",
                Location = "Japan",
                Details = "The Japanese macaque, is a terrestrial Old World monkey species native to Japan. They are also sometimes known as the snow monkey because they live in areas where snow covers the ground for months each",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/macasa.jpg",
                Population = 1000,
                Latitude = 36.204824,
                Longitude = 138.252924
            },
            new() {
                Name = "Sebastian",
                Location = "Seattle",
                Details = "This little trouble maker lives in Seattle with James and loves traveling on adventures with James and tweeting @MotzMonkeys. He by far is an Android fanboy and is getting ready for the new Google Pixel 9!",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/sebastian.jpg",
                Population = 1,
                Latitude = 47.606209,
                Longitude = -122.332071
            }
        };
    }
}