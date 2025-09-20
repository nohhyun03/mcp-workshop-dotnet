namespace MyMonkeyApp.Models;

/// <summary>
/// 지리적 좌표를 나타내는 구조체
/// </summary>
public readonly struct Coordinates
{
    /// <summary>
    /// 위도
    /// </summary>
    public double Latitude { get; }

    /// <summary>
    /// 경도
    /// </summary>
    public double Longitude { get; }

    /// <summary>
    /// 좌표 생성자
    /// </summary>
    /// <param name="latitude">위도</param>
    /// <param name="longitude">경도</param>
    public Coordinates(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    /// <summary>
    /// 좌표를 문자열로 변환
    /// </summary>
    /// <returns>위도, 경도 형식의 문자열</returns>
    public override string ToString() => $"({Latitude:F6}, {Longitude:F6})";

    /// <summary>
    /// 두 좌표 간의 거리 계산 (대략적인 킬로미터 단위)
    /// </summary>
    /// <param name="other">다른 좌표</param>
    /// <returns>거리(km)</returns>
    public double DistanceTo(Coordinates other)
    {
        const double earthRadius = 6371; // 지구 반지름 (km)
        
        var lat1Rad = Latitude * Math.PI / 180;
        var lat2Rad = other.Latitude * Math.PI / 180;
        var deltaLat = (other.Latitude - Latitude) * Math.PI / 180;
        var deltaLon = (other.Longitude - Longitude) * Math.PI / 180;

        var a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return earthRadius * c;
    }
}