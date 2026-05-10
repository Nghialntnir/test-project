using CreditCard_Limit_Service_5_Nghia.Models;
using CreditCard_Limit_Service_5_Nghia;

[TestFixture]
public class CreditCardTests_5_Nghia
{
    private CreditService_5_Nghia _service;

    [SetUp]
    public void Setup() => _service = new CreditService_5_Nghia();

    internal static IEnumerable<TestCaseData> GetTestData()
    {
        string filePath_5_Nghia = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data_5_Nghia", "TestData_5_Nghia.csv");
        if (!File.Exists(filePath_5_Nghia)) yield break;

        var lines_5_Nghia = File.ReadAllLines(filePath_5_Nghia).Skip(1);
        foreach (var line_5_Nghia in lines_5_Nghia)
        {
            if (string.IsNullOrWhiteSpace(line_5_Nghia)) continue;
            var c_5_Nghia = line_5_Nghia.Split(',').Select(s_Nghia => s_Nghia.Trim()).ToArray();
            yield return new TestCaseData((object)c_5_Nghia); // Ép kiểu để NUnit nhận diện đúng mảng
        }
    }

    [Test]
    [TestCaseSource(nameof(GetTestData))]
    public void EvaluateCreditCard_Test(string[] data_5_Nghia)
    {
        var customer_5_Nghia = new Customer_5_Nghia
        {
            FullName_5_Nghia = data_5_Nghia[2],
            IsVietnamese_5_Nghia = bool.Parse(data_5_Nghia[3].ToLower()),
            Age_5_Nghia = int.Parse(data_5_Nghia[4]),
            CicGroup_5_Nghia = int.Parse(data_5_Nghia[5]),
            PreferredMethod = Enum.Parse<ApprovalMethod_5_Nghia>(data_5_Nghia[6]),
            Income_5_Nghia = decimal.Parse(data_5_Nghia[7]),
            AssetValue_5_Nghia = decimal.Parse(data_5_Nghia[8])
        };

        var reqTier_5_Nghia = Enum.Parse<CardTier_5_Nghia>(data_5_Nghia[9]);

        var (isApproved_5_Nghia, limit_5_Nghia, tier_5_Nghia) =
            _service.EvaluateACB_5_Nghia(customer_5_Nghia, reqTier_5_Nghia);

        Assert.Multiple(() => {
            Assert.That(isApproved_5_Nghia, Is.EqualTo(bool.Parse(data_5_Nghia[10].ToLower())));
            Assert.That((double)limit_5_Nghia, Is.EqualTo(double.Parse(data_5_Nghia[11])).Within(2));
            Assert.That(tier_5_Nghia, Is.EqualTo(Enum.Parse<CardTier_5_Nghia>(data_5_Nghia[12])));
        });
    }

    // ─── TC01 → TC05: kiểm tra điều kiện loại đầu vào ───────────────────────

    // TC01_5_Nghia
    // Người nước ngoài (Loại)
    [TestCase("TC01_5_Nghia", "Customer 1", false, 25, 1, ApprovalMethod_5_Nghia.ByIncome_5_Nghia, 50000000, 0, CardTier_5_Nghia.Classic_5_Nghia, false, 0, CardTier_5_Nghia.None_5_Nghia)]
    // TC02_5_Nghia
    // Dưới 18 tuổi (Loại)
    [TestCase("TC02_5_Nghia", "Customer 2", true, 17, 1, ApprovalMethod_5_Nghia.ByIncome_5_Nghia, 50000000, 0, CardTier_5_Nghia.Classic_5_Nghia, false, 0, CardTier_5_Nghia.None_5_Nghia)]
    // TC03_5_Nghia
    // Trên 70 tuổi (Loại)
    [TestCase("TC03_5_Nghia_10_5", "Customer 3", true, 69, 1, ApprovalMethod_5_Nghia.ByIncome_5_Nghia, 50000000, 0, CardTier_5_Nghia.Classic_5_Nghia, false, 0, CardTier_5_Nghia.None_5_Nghia)]
    // TC04_5_Nghia
    // CIC bằng 0 (Loại)
    [TestCase("TC04_5_Nghia", "Customer 4", true, 25, 0, ApprovalMethod_5_Nghia.ByIncome_5_Nghia, 50000000, 0, CardTier_5_Nghia.Classic_5_Nghia, false, 0, CardTier_5_Nghia.None_5_Nghia)]
    // TC05_5_Nghia
    // CIC bằng 3 – Nợ xấu (Loại)
    [TestCase("TC05_5_Nghia", "Customer 5", true, 25, 3, ApprovalMethod_5_Nghia.ByIncome_5_Nghia, 50000000, 0, CardTier_5_Nghia.Classic_5_Nghia, false, 0, CardTier_5_Nghia.None_5_Nghia)]
    public void EvaluateEligibility_TC01_to_TC05(
        string tcId,
        string fullName,
        bool isVietnamese,
        int age,
        int cicGroup,
        ApprovalMethod_5_Nghia preferredMethod,
        decimal income,
        decimal assetValue,
        CardTier_5_Nghia requestedTier,
        bool expectedApproved,
        decimal expectedLimit,
        CardTier_5_Nghia expectedTier)
    {
        var customer_5_Nghia = new Customer_5_Nghia
        {
            FullName_5_Nghia = fullName,
            IsVietnamese_5_Nghia = isVietnamese,
            Age_5_Nghia = age,
            CicGroup_5_Nghia = cicGroup,
            PreferredMethod = preferredMethod,
            Income_5_Nghia = income,
            AssetValue_5_Nghia = assetValue
        };

        var (isApproved_5_Nghia, limit_5_Nghia, tier_5_Nghia) =
            _service.EvaluateACB_5_Nghia(customer_5_Nghia, requestedTier);

        Assert.Multiple(() => {
            Assert.That(isApproved_5_Nghia, Is.EqualTo(expectedApproved), $"[{tcId}] IsApproved sai");
            Assert.That((double)limit_5_Nghia, Is.EqualTo((double)expectedLimit).Within(2), $"[{tcId}] Limit sai");
            Assert.That(tier_5_Nghia, Is.EqualTo(expectedTier), $"[{tcId}] Tier sai");
        });
    }
}