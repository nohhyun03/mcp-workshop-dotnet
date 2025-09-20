namespace MyMonkeyApp.Models;

/// <summary>
/// 원숭이 컬렉션의 통계 정보
/// </summary>
public class MonkeyStatistics
{
    /// <summary>
    /// 총 원숭이 종류 수
    /// </summary>
    public int TotalSpecies { get; set; }

    /// <summary>
    /// 총 개체수
    /// </summary>
    public int TotalPopulation { get; set; }

    /// <summary>
    /// 가장 많은 개체수를 가진 종
    /// </summary>
    public Monkey? MostPopulousSpecies { get; set; }

    /// <summary>
    /// 가장 적은 개체수를 가진 종
    /// </summary>
    public Monkey? LeastPopulousSpecies { get; set; }

    /// <summary>
    /// 멸종 위기종 수
    /// </summary>
    public int EndangeredSpeciesCount { get; set; }

    /// <summary>
    /// 지역별 분포
    /// </summary>
    public Dictionary<string, int> RegionalDistribution { get; set; } = new();

    /// <summary>
    /// 평균 개체수
    /// </summary>
    public double AveragePopulation => TotalSpecies > 0 ? (double)TotalPopulation / TotalSpecies : 0;

    /// <summary>
    /// 통계 정보를 문자열로 변환
    /// </summary>
    /// <returns>통계 요약</returns>
    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"🐵 원숭이 통계 요약");
        sb.AppendLine($"━━━━━━━━━━━━━━━━━━━━");
        sb.AppendLine($"총 종류: {TotalSpecies}종");
        sb.AppendLine($"총 개체수: {TotalPopulation:N0}마리");
        sb.AppendLine($"평균 개체수: {AveragePopulation:F0}마리");
        
        if (MostPopulousSpecies != null)
            sb.AppendLine($"최다 개체수: {MostPopulousSpecies.Name} ({MostPopulousSpecies.Population:N0}마리)");
        
        if (LeastPopulousSpecies != null)
            sb.AppendLine($"최소 개체수: {LeastPopulousSpecies.Name} ({LeastPopulousSpecies.Population:N0}마리)");
        
        sb.AppendLine($"멸종위기종: {EndangeredSpeciesCount}종");
        
        if (RegionalDistribution.Count > 0)
        {
            sb.AppendLine("\n🌍 지역별 분포:");
            foreach (var region in RegionalDistribution.OrderByDescending(x => x.Value))
            {
                sb.AppendLine($"  {region.Key}: {region.Value}종");
            }
        }

        return sb.ToString();
    }
}