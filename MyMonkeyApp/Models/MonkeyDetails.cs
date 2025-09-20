namespace MyMonkeyApp.Models;

/// <summary>
/// 원숭이 종의 상세 정보를 나타내는 확장된 모델 클래스
/// </summary>
public class MonkeyDetails : Monkey
{
    /// <summary>
    /// 원숭이 분류 (구세계/신세계)
    /// </summary>
    public MonkeyClassification Classification { get; set; }

    /// <summary>
    /// 멸종 위기 상태
    /// </summary>
    public ConservationStatus ConservationStatus { get; set; }

    /// <summary>
    /// ASCII 아트 표현
    /// </summary>
    public string AsciiArt { get; set; } = string.Empty;

    /// <summary>
    /// 특별한 원숭이 여부 (애완동물 등)
    /// </summary>
    public bool IsSpecialMonkey { get; set; }

    /// <summary>
    /// 생성 또는 마지막 업데이트 날짜
    /// </summary>
    public DateTime LastUpdated { get; set; } = DateTime.Now;

    /// <summary>
    /// 지리적 좌표
    /// </summary>
    public Coordinates Coordinates => new(Latitude, Longitude);

    /// <summary>
    /// 상세 정보를 포함한 문자열 표현
    /// </summary>
    /// <returns>원숭이의 상세 정보</returns>
    public override string ToString()
    {
        var status = ConservationStatus switch
        {
            ConservationStatus.LeastConcern => "안전",
            ConservationStatus.Vulnerable => "취약",
            ConservationStatus.Endangered => "위험",
            ConservationStatus.CriticallyEndangered => "심각한 위험",
            ConservationStatus.DataDeficient => "데이터 부족",
            _ => "알 수 없음"
        };

        return $"{Name} ({Classification}) - {Location}\n" +
               $"개체수: {Population:N0}, 보호상태: {status}\n" +
               $"좌표: {Coordinates}";
    }
}