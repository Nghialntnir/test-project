using System;
using CreditCard_Limit_Service_5_Nghia.Models;

namespace CreditCard_Limit_Service_5_Nghia
{
    public class CreditService_5_Nghia
    {
        public (bool IsApproved_5_Nghia, decimal Limit_5_Nghia, CardTier_5_Nghia Tier_5_Nghia) EvaluateACB_5_Nghia(
            Customer_5_Nghia customer_5_Nghia,
            CardTier_5_Nghia requestedTier_5_Nghia)
        {
            // 1. Kiểm tra điều kiện ban đầu
            if (!customer_5_Nghia.IsVietnamese_5_Nghia ||
                customer_5_Nghia.Age_5_Nghia < 18 ||
                customer_5_Nghia.Age_5_Nghia > 70 ||
                customer_5_Nghia.CicGroup_5_Nghia != 1 && customer_5_Nghia.CicGroup_5_Nghia != 2) // c1
            {
                return (false, 0, CardTier_5_Nghia.None_5_Nghia);
            }

            //TC07_5_Nghia_10 / 5 / 2026,[Limit Classic -TSTC] Bien duoi-1(loai),Customer 7,true,18,1,ByAsset_5_Nghia,3000000,12499999,Classic_5_Nghia,false,0,None_5_Nghia

            // 2. Tính hạn mức
            decimal rawLimit_5_Nghia = 0;
            // Tính bằng thu nhập
            if (customer_5_Nghia.PreferredMethod == ApprovalMethod_5_Nghia.ByIncome_5_Nghia) // c2
            {
                rawLimit_5_Nghia = customer_5_Nghia.Income_5_Nghia * 3;
            }
            // Tính bằng tài sản thế chấp
            else
            {
                rawLimit_5_Nghia = customer_5_Nghia.AssetValue_5_Nghia * 0.8m;
            }

            // 3. Áp dụng Business Rules theo loại thẻ yêu cầu (Clamp & Validate)
            decimal finalLimit_5_Nghia = 0;

            switch (requestedTier_5_Nghia)
            {
                case CardTier_5_Nghia.Classic_5_Nghia:
                    // [10-50)
                    if (rawLimit_5_Nghia < 10_000_000) return (false, 0, CardTier_5_Nghia.None_5_Nghia);
                    finalLimit_5_Nghia = Math.Min(rawLimit_5_Nghia, 49_999_999);
                    break;

                case CardTier_5_Nghia.Gold_5_Nghia:
                    // [50-500)
                    if (rawLimit_5_Nghia < 50_000_000) return (false, 0, CardTier_5_Nghia.None_5_Nghia);
                    finalLimit_5_Nghia = Math.Min(rawLimit_5_Nghia, 499_999_999);
                    break;

                case CardTier_5_Nghia.Platinum_5_Nghia:
                    // [500-1000]
                    if (rawLimit_5_Nghia < 500_000_000) return (false, 0, CardTier_5_Nghia.None_5_Nghia);
                    finalLimit_5_Nghia = Math.Min(rawLimit_5_Nghia, 1_000_000_000);
                    break;

                default:
                    return (false, 0, CardTier_5_Nghia.None_5_Nghia);
            }

            // 4. Trả kết quả
            return (true, finalLimit_5_Nghia, requestedTier_5_Nghia);
        }
    }
}