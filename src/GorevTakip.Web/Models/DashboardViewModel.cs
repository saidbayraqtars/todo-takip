// Bu dosya: Dashboard ekraninin model'i. Sayaclar + yaklasan gorev listesini tasir.
using GorevTakip.Entities;

namespace GorevTakip.Web.Models;

public class DashboardViewModel
{
    public int Toplam { get; set; }
    public int Tamamlanan { get; set; }
    public int Bekleyen { get; set; }
    public string KullaniciAdi { get; set; } = string.Empty;

    /// <summary>Son tarihi yaklaşan birkaç görev (dashboard önizleme).</summary>
    public List<Gorev> YaklasanGorevler { get; set; } = new();
}
