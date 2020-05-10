namespace WebApplication.Contracts
{
    public interface IVerificationCodeGenerator
    {
        int GenerateVerificationCode(int min, int max, int seed);

    }
}