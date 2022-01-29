using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zone.FileInterface.Helper
{
    public static class BytePatternLocator
    {
        public static int IndexOfPattern(this byte[] body, byte[] pattern)
        {
            int foundIndex = -1;

            if (body.Length <= 0 || pattern.Length <= 0 || pattern.Length > body.Length)
                return foundIndex;

            foreach (int patternStart in IndexOfSimilarPatternStart(body, pattern))
                if (IsPattern(body, pattern, patternStart))
                    return patternStart;
                
            return foundIndex;
        }

        private static IEnumerable<int> IndexOfSimilarPatternStart(byte[] body, byte[] pattern)
        {
            for (var index = 0; index <= body.Length - pattern.Length; index++)
                if (body[index] == pattern[0])
                    yield return index;
        }

        private static bool IsPattern(byte[] body, byte[] pattern, int bodyIndex)
        {
            for (var index = 1; index <= pattern.Length - 1; index++)
            {
                if (body[bodyIndex + index] == pattern[index])
                    continue;

                return false;
            }
            return true;
        }
    }
}
