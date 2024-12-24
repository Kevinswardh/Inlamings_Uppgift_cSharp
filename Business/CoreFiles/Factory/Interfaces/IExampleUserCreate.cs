using Business.CoreFiles.Models.Users;

namespace Business.CoreFiles.Factory.Interfaces
{
    /// <summary>
    /// Ett interface för att skapa en exempelanvändare med fördefinierade värden.
    /// </summary>
    public interface IExampleUserCreate
    {
        /// <summary>
        /// Skapar en exempelanvändare med fördefinierade namn, e-post, kontakter och favoriter.
        /// </summary>
        /// <returns>En instans av <see cref="BaseUser"/> som representerar exempelanvändaren.</returns>
        BaseUser CreateExampleUser();
    }
}
