using System;
using Logic.Interfaces;

namespace Logic.Services
{
    public class VerificationCodeGenerator : IVerificationCodeGenerator
    {
        public int GenerateVerificationCode(int mim, int max, int seed)
        {
            return new Random(seed).Next(mim, max); ;
        }
    }
}
