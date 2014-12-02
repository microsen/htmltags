using System;

namespace HtmlTags.Mvc
{
    public static class TypeHelpers
    {
        public static bool IsNumeric(this Type type)
        {
            if (type == null) return false;
            if (type.IsEnum) return false;
            return Type.GetTypeCode(type).IsNumeric();
        }

        public static bool IsNumeric(this TypeCode typeCode)
        {
            return typeCode == TypeCode.Byte
                   || typeCode == TypeCode.Decimal
                   || typeCode == TypeCode.Double
                   || typeCode == TypeCode.Int16
                   || typeCode == TypeCode.Int32
                   || typeCode == TypeCode.Int64
                   || typeCode == TypeCode.SByte
                   || typeCode == TypeCode.Single
                   || typeCode == TypeCode.UInt16
                   || typeCode == TypeCode.UInt32
                   || typeCode == TypeCode.UInt64;

        }
    }
}