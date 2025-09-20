namespace MyMonkeyApp.Models;

/// <summary>
/// 원숭이 분류
/// </summary>
public enum MonkeyClassification
{
    /// <summary>
    /// 구세계 원숭이 (아프리카, 아시아)
    /// </summary>
    OldWorld,

    /// <summary>
    /// 신세계 원숭이 (아메리카 대륙)
    /// </summary>
    NewWorld,

    /// <summary>
    /// 특별한 원숭이 (애완동물 등)
    /// </summary>
    Special
}

/// <summary>
/// 멸종 위기 상태
/// </summary>
public enum ConservationStatus
{
    /// <summary>
    /// 안전함
    /// </summary>
    LeastConcern,

    /// <summary>
    /// 취약함
    /// </summary>
    Vulnerable,

    /// <summary>
    /// 위험함
    /// </summary>
    Endangered,

    /// <summary>
    /// 심각한 위험
    /// </summary>
    CriticallyEndangered,

    /// <summary>
    /// 데이터 부족
    /// </summary>
    DataDeficient
}