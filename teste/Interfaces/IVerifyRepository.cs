using APIperson.Models.DTOs;

namespace APIperson.Interfaces
{
    public interface IVerifyRepository
    {
        public bool VerifyDisconnect();
        public bool VerifyConnect();
    }
}
