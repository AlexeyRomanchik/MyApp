using System;
using WebApplication.Interfaces;

namespace WebApplication.Services
{
    public class VerificationCodeGenerator : IVerificationCodeGenerator
    {
        public int GenerateVerificationCode(int mim, int max, int seed)
        {
            return new Random(seed).Next(mim, max); ;
        }
    }
}
