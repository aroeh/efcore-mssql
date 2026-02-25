namespace EFCore.MSSQL.Shared.Utils;

public static class IdGenerator
{
    public static string GenerateId()
    {
        Guid newId = Guid.NewGuid();
        return newId.ToString().Replace("-", "");
    }
}
