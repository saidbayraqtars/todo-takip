// Bu dosya: Dashboard ekranindaki 3 sayaci tasiyan veri sinifi (toplam/tamamlanan/bekleyen).
namespace GorevTakip.Business.DTOs;

/// <summary>Dashboard sayaçları: toplam / tamamlanan / bekleyen görev.</summary>
public class DashboardOzet
{
    public int Toplam { get; set; }
    public int Tamamlanan { get; set; }
    public int Bekleyen { get; set; }
}
