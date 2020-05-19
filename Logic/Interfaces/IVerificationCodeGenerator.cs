namespace Logic.Interfaces
{
    public interface IVerificationCodeGenerator
    {
        int GenerateVerificationCode(int min, int max, int seed);

    }
}