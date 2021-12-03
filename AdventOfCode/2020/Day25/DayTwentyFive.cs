using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace AdventOfCode._2020.Day25
{
    public static class DayTwentyFive
    {
        public static string ExecuteFirst()
        {
            var lines = GetInputArray();

            var encryptionKey = GetEncryptionKey(lines[0], lines[1]);
            return encryptionKey.ToString();
        }


        public static string ExecuteSecond()
        {
            return string.Empty;
        }

        private static List<BigInteger> GetInputArray()
        {
            return File.ReadAllLines("Day25/input.txt").Select(l => BigInteger.Parse(l)).ToList();
        }

        private static BigInteger GetEncryptionKey(
           BigInteger publicKeyDoor,
           BigInteger publicKeyCard)
        {
            var loopSizeDoor = GetLoopSizeFromPublicKey(publicKeyDoor);
            var loopSizeCard = GetLoopSizeFromPublicKey(publicKeyCard);

            var encryptionKeyDoor = GetTransformedSubjectNumber(loopSizeDoor, publicKeyCard);
            var encryptionKeyCard = GetTransformedSubjectNumber(loopSizeCard, publicKeyDoor);

            if (encryptionKeyCard != encryptionKeyDoor)
            {
                throw new Exception($"Encryption keys don't match: card: {encryptionKeyCard}, door: {encryptionKeyDoor}");
            }

            return encryptionKeyCard;
        }

        private static int GetLoopSizeFromPublicKey(BigInteger publicKey)
        {
            int result = 1;
            BigInteger currentTransform = 1;
            while (true)
            {
                currentTransform = (currentTransform * 7) % 20201227;
                if (currentTransform == publicKey)
                {
                    break;
                }
                result++;
            }
            return result;
        }

        private static BigInteger GetTransformedSubjectNumber(int loopSize, BigInteger subjectNumber)
        {
            BigInteger result = 1;
            for (int i = 0; i < loopSize; i++)
            {
                result = (result * subjectNumber) % 20201227;
            }
            return result;
        }

    }
}
