using APIperson.Models.DTOs;

namespace APIperson.Interfaces
{
    public interface IVerifyRepository
    {
        public bool VerifyCloseConnect();
        public bool VerifyOpenConnect();
    }
}
