namespace SalesLedger.Domain.Enums;

public enum TransactionSourceType
{
    Manual = 1,
    ExcelImport = 2,
    PaperRecord = 3,
    OtherSeller = 4,
    LegacyMigration = 5
}
