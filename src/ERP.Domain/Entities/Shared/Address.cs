namespace ERP.Domain.Entities.Shared;
public sealed record DiaChi
{

    public string Duong { get; init; }

    public string Phuong { get; init; }

    public string ThanhPho { get; init; }

    public string Tinh { get; init; }

    public static DiaChi Create(string duong, string thanhPho, string tinh)
    {
        if (tinh.Length == 0 || thanhPho.Length == 0 || duong.Length == 0)
        {
            throw new ApplicationException("Vui lòng điền đầy đủ thông tin địa chỉ!");
        }

        return new DiaChi
        {
            Duong = duong,
            ThanhPho = thanhPho,
            Tinh = tinh
        };
    }
}
