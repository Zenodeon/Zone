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
            return FindPattern(body, pattern, searchDirectionToRight: true);
        }

        public static int LastIndexOfPattern(this byte[] body, byte[] pattern)
        {
            return FindPattern(body, pattern, searchDirectionToRight: false);
        }

        private static int FindPattern(this byte[] body, byte[] pattern, bool searchDirectionToRight)
        {
            int foundIndex = -1;

            if (body.Length <= 0 || pattern.Length <= 0 || pattern.Length > body.Length)
                return foundIndex;

            foreach (int patternStart in IndexOfPatternStart(body, pattern, searchDirectionToRight))
                if (IsPattern(body, pattern, patternStart))
                    return patternStart;

            return foundIndex;
        }

        private static IEnumerable<int> IndexOfPatternStart(byte[] body, byte[] pattern, bool searchDirectionToRight)
        {
            if (searchDirectionToRight)
            {
                for (var index = 0; index <= body.Length - pattern.Length; index++)
                    if (body[index] == pattern[0])
                        yield return index;
            }
            else
            {
                for (var index = body.Length - pattern.Length; index >= 0; index--)
                    if (body[index] == pattern[0])
                        yield return index;
            }
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
