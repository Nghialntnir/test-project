namespace CreditCard_Limit_Service_5_Nghia.Models
{
    public enum ApprovalMethod_5_Nghia { ByIncome_5_Nghia, ByAsset_5_Nghia } // Phương thức thế chấp
    public enum CardTier_5_Nghia { None_5_Nghia, Classic_5_Nghia, Gold_5_Nghia, Platinum_5_Nghia } // Phân loại thẻ

    public class Customer_5_Nghia
    {
        public string FullName_5_Nghia { get; set; } = string.Empty;
        public int Age_5_Nghia { get; set; }
        public decimal Income_5_Nghia { get; set; }
        public int CicGroup_5_Nghia { get; set; }
        public decimal AssetValue_5_Nghia { get; set; }
        public bool IsVietnamese_5_Nghia { get; set; } = true;
        public ApprovalMethod_5_Nghia PreferredMethod { get; set; }
    }
}