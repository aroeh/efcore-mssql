namespace EFCore.MSSQL.Shared.Utils;

public static class IdGenerator
{
    /// <summary>
    /// Generates a new Id
    /// </summary>
    /// <remarks>
    /// The Id is a <see cref="Guid"/> with the dashes removed
    /// </remarks>
    /// <returns>New Id for an entity</returns>
    public static string GenerateId()
    {
        Guid newId = Guid.NewGuid();
        return newId.ToString().Replace("-", "");
    }
}
