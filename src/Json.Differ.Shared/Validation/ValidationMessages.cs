using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json.Differ.Shared.Validation
{
    public static class ValidationMessages
    {
        public static string InvalidCharacteresMessage { get; } = "Inválid characteres.";
        public static string NotNullMessage { get; } = "It cannot be null.";
        public static string NotNullOrEmptyMessage { get; } = "It cannot be null or empty.";
        public static string NotNegativeMessage { get; } = "It cannot be negative.";
        public static string GreaterThanZeroMessage { get; } = "It most be greater than zero.";
        public static string InvalidDocumentMessage { get; } = "Invalid Document.";
        public static string MaxLengthMessage { get; } = "Oversize.";
    }
}
