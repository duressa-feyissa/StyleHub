namespace backend.Application.DTO.Shop.ShopDTO.DTO;

public class ShopVerifyRequestDTO
{
    public string ShopId { get; set; } = string.Empty;
    public string OwnerIdentityCardUrl { get; set; } = string.Empty;
    public string BusinessRegistrationNumber { get; set; } = string.Empty;
    public string BusinessRegistrationDocumentUrl { get; set; } = string.Empty;
}