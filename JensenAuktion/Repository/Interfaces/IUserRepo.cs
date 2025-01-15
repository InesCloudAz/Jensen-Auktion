using JensenAuktion.Repository.Entities;

namespace JensenAuktion.Repository.Interfaces
{
    public interface IUserRepo
    {

        public void InsertUser(User user);
        public void UpdateUser(User user);


    }
}
