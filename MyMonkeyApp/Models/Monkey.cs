namespace MyMonkeyApp.Models;

/// <summary>
/// 원숭이 종의 정보를 나타내는 모델 클래스
/// </summary>
public class Monkey
{
    /// <summary>
    /// 원숭이의 이름
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 원숭이가 서식하는 지역
    /// </summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// 원숭이에 대한 상세 설명
    /// </summary>
    public string Details { get; set; } = string.Empty;

    /// <summary>
    /// 이미지 URL
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// 추정 개체수
    /// </summary>
    public int Population { get; set; }

    /// <summary>
    /// 위도 좌표
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// 경도 좌표
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// 원숭이 정보를 문자열로 반환
    /// </summary>
    /// <returns>원숭이의 기본 정보</returns>
    public override string ToString()
    {
        return $"{Name} - {Location} (개체수: {Population:N0})";
    }
}