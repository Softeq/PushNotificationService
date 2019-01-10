// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Softeq.NetKit.Services.PushNotifications.Helpers
{
    internal static class TagExpressionGenerator
    {
        public static string GetExpressionString(IList<string> tagsToInclude, IList<string> tagsToExclude = null)
        {
            var builder = new StringBuilder();

            builder.Append("(");

            for (int i = 0; i < tagsToInclude.Count; i++)
            {
                builder.Append($"{tagsToInclude[i]}");

                if (i < (tagsToInclude.Count - 1) || (tagsToExclude != null && tagsToExclude.Any()))
                {
                    builder.Append(" && ");
                }
            }

            if (tagsToExclude != null)
            {
                for (int i = 0; i < tagsToExclude.Count; i++)
                {
                    builder.Append($"!{tagsToExclude[i]}");

                    if (i < (tagsToExclude.Count - 1))
                    {
                        builder.Append(" && ");
                    }
                }
            }

            builder.Append(")");

            return builder.ToString();
        }
    }
}
