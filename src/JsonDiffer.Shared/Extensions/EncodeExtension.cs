using Json.Differ.Core.NotificationResults;
using System.Text;

namespace JsonDiffer.Shared.Extensions
{
    public static class EncodeExtension
    {
        public static string EncodeBase64(this string value)
        {
            var valueBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(valueBytes);
        }

        public static NotificationResult<string> DecodeBase64(this string value)
        {
            try
            {
                var valueBytes = System.Convert.FromBase64String(value);
                var encoded = Encoding.UTF8.GetString(valueBytes);

                return NotificationResult<string>.Success(encoded);
            }
            catch (Exception ex)
            {
                return NotificationResult<string>.Error(ex.Message);
            }

        }
    }
}
