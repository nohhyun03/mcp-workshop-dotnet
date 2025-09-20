using MyMonkeyApp.Models;

namespace MyMonkeyApp.Data;

/// <summary>
/// 원숭이 데이터에 대한 저장소 패턴 구현
/// MCP 서버와의 통신을 담당합니다.
/// </summary>
public class MonkeyRepository
{
    private static readonly HttpClient _httpClient = new();
    private const string MCP_SERVER_URL = "http://localhost:3000"; // MCP 서버 URL (예시)

    /// <summary>
    /// MCP 서버에서 모든 원숭이 데이터를 가져옵니다.
    /// </summary>
    /// <returns>원숭이 목록</returns>
    public async Task<List<Monkey>> GetAllMonkeysAsync()
    {
        try
        {
            // 실제 MCP 서버 호출 구현
            // 현재는 시뮬레이션으로 구현
            return await SimulateMcpServerCallAsync();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"MCP 서버에서 데이터를 가져오는데 실패했습니다: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// 특정 이름의 원숭이 데이터를 MCP 서버에서 가져옵니다.
    /// </summary>
    /// <param name="name">원숭이 이름</param>
    /// <returns>원숭이 정보 또는 null</returns>
    public async Task<Monkey?> GetMonkeyByNameAsync(string name)
    {
        try
        {
            // 실제 MCP 서버의 특정 원숭이 조회 API 호출
            return await SimulateMcpServerGetByNameAsync(name);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"'{name}' 원숭이 데이터를 가져오는데 실패했습니다: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// MCP 서버에서 무작위 원숭이를 가져옵니다.
    /// </summary>
    /// <returns>무작위 원숭이</returns>
    public async Task<Monkey> GetRandomMonkeyAsync()
    {
        try
        {
            // 실제 MCP 서버의 무작위 원숭이 API 호출
            return await SimulateMcpServerRandomAsync();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"무작위 원숭이 데이터를 가져오는데 실패했습니다: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// MCP 서버 연결 상태를 확인합니다.
    /// </summary>
    /// <returns>연결 가능 여부</returns>
    public async Task<bool> IsServerAvailableAsync()
    {
        try
        {
            // 실제 구현에서는 MCP 서버 health check 엔드포인트 호출
            await Task.Delay(50); // 네트워크 호출 시뮬레이션
            return true; // 시뮬레이션에서는 항상 true
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// MCP 서버 호출 시뮬레이션 - 모든 원숭이
    /// </summary>
    private async Task<List<Monkey>> SimulateMcpServerCallAsync()
    {
        await Task.Delay(200); // 네트워크 지연 시뮬레이션

        // 실제 MCP 서버에서 받은 JSON 데이터를 기반으로 구현
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
                Name = "Capuchin Monkey",
                Location = "Central & South America",
                Details = "The capuchin monkeys are New World monkeys of the subfamily Cebinae. Prior to 2011, the subfamily contained only a single genus, Cebus.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/capuchin.jpg",
                Population = 23000,
                Latitude = 12.769013,
                Longitude = -85.602364
            },
            new() {
                Name = "Blue Monkey",
                Location = "Central and East Africa",
                Details = "The blue monkey or diademed monkey is a species of Old World monkey native to Central and East Africa, ranging from the upper Congo River basin east to the East African Rift and south to northern Angola and Zambia",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/bluemonkey.jpg",
                Population = 12000,
                Latitude = 1.957709,
                Longitude = 37.297204
            },
            new() {
                Name = "Squirrel Monkey",
                Location = "Central & South America",
                Details = "The squirrel monkeys are the New World monkeys of the genus Saimiri. They are the only genus in the subfamily Saimirinae. The name of the genus Saimiri is of Tupi origin, and was also used as an English name by early researchers.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/saimiri.jpg",
                Population = 11000,
                Latitude = -8.783195,
                Longitude = -55.491477
            },
            new() {
                Name = "Golden Lion Tamarin",
                Location = "Brazil",
                Details = "The golden lion tamarin also known as the golden marmoset, is a small New World monkey of the family Callitrichidae.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/tamarin.jpg",
                Population = 19000,
                Latitude = -14.235004,
                Longitude = -51.92528
            },
            new() {
                Name = "Howler Monkey",
                Location = "South America",
                Details = "Howler monkeys are among the largest of the New World monkeys. Fifteen species are currently recognised. Previously classified in the family Cebidae, they are now placed in the family Atelidae.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/alouatta.jpg",
                Population = 8000,
                Latitude = -8.783195,
                Longitude = -55.491477
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
                Name = "Mandrill",
                Location = "Southern Cameroon, Gabon, and Congo",
                Details = "The mandrill is a primate of the Old World monkey family, closely related to the baboons and even more closely to the drill. It is found in southern Cameroon, Gabon, Equatorial Guinea, and Congo.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/mandrill.jpg",
                Population = 17000,
                Latitude = 7.369722,
                Longitude = 12.354722
            },
            new() {
                Name = "Proboscis Monkey",
                Location = "Borneo",
                Details = "The proboscis monkey or long-nosed monkey, known as the bekantan in Malay, is a reddish-brown arboreal Old World monkey that is endemic to the south-east Asian island of Borneo.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/borneo.jpg",
                Population = 15000,
                Latitude = 0.961883,
                Longitude = 114.55485
            },
            new() {
                Name = "Sebastian",
                Location = "Seattle",
                Details = "This little trouble maker lives in Seattle with James and loves traveling on adventures with James and tweeting @MotzMonkeys. He by far is an Android fanboy and is getting ready for the new Google Pixel 9!",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/sebastian.jpg",
                Population = 1,
                Latitude = 47.606209,
                Longitude = -122.332071
            },
            new() {
                Name = "Henry",
                Location = "Phoenix",
                Details = "An adorable Monkey who is traveling the world with Heather and live tweets his adventures @MotzMonkeys. His favorite platform is iOS by far and is excited for the new iPhone Xs!",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/henry.jpg",
                Population = 1,
                Latitude = 33.448377,
                Longitude = -112.074037
            },
            new() {
                Name = "Red-shanked douc",
                Location = "Vietnam",
                Details = "The red-shanked douc is a species of Old World monkey, among the most colourful of all primates. The douc is an arboreal and diurnal monkey that eats and sleeps in the trees of the forest.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/douc.jpg",
                Population = 1300,
                Latitude = 16.111648,
                Longitude = 108.262122
            },
            new() {
                Name = "Mooch",
                Location = "Seattle",
                Details = "An adorable Monkey who is traveling the world with Heather and live tweets his adventures @MotzMonkeys. Her favorite platform is iOS by far and is excited for the new iPhone 16!",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/Mooch.PNG",
                Population = 1,
                Latitude = 47.608013,
                Longitude = -122.335167
            }
        };
    }

    /// <summary>
    /// 이름으로 원숭이 검색 시뮬레이션
    /// </summary>
    private async Task<Monkey?> SimulateMcpServerGetByNameAsync(string name)
    {
        await Task.Delay(100);
        var allMonkeys = await SimulateMcpServerCallAsync();
        return allMonkeys.FirstOrDefault(m => 
            string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// 무작위 원숭이 선택 시뮬레이션
    /// </summary>
    private async Task<Monkey> SimulateMcpServerRandomAsync()
    {
        await Task.Delay(100);
        var allMonkeys = await SimulateMcpServerCallAsync();
        var random = new Random();
        return allMonkeys[random.Next(allMonkeys.Count)];
    }

    /// <summary>
    /// 리소스 정리
    /// </summary>
    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}