// Bu dosya: Görevin durumunu tutan enum. (Bekliyor / Tamamlandi)
// Bekleyen ve Tamamlanan görev filtrelerinde bu değer kullanılır.
namespace GorevTakip.Entities.Enums;

/// <summary>
/// Görevin durumunu temsil eder. Bekleyen ve tamamlanan görev filtreleri bu değer üzerinden yapılır.
/// </summary>
public enum GorevDurum
{
    Bekliyor = 0,
    Tamamlandi = 1
}
