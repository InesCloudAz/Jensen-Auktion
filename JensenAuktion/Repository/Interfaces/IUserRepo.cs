using JensenAuktion.Repository.Entities;

namespace JensenAuktion.Repository.Interfaces
{
    public interface IUserRepo
    {
        List<User> GetAllUsers();


        public void InsertUser(User user);
        public void UpdateUser(User user);
        int? LoginUser(string userName, string password);
    }
}
