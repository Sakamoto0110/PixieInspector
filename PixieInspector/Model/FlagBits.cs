namespace PixieInspector.Model;

// Bit-preserving conversion, including negative enum constants and the unsigned 64-bit high bit.
// Numeric conversion through double or checked UInt64 would lose one of those cases.
internal static class FlagBits
{
    internal static Type Storage(Type type)
    {
        type = Nullable.GetUnderlyingType(type) ?? type;
        return type.IsEnum ? Enum.GetUnderlyingType(type) : type;
    }

    internal static ulong WidthMask(Type type) => Type.GetTypeCode(Storage(type)) switch
    {
        TypeCode.SByte or TypeCode.Byte => byte.MaxValue,
        TypeCode.Int16 or TypeCode.UInt16 => ushort.MaxValue,
        TypeCode.Int32 or TypeCode.UInt32 => uint.MaxValue,
        TypeCode.Int64 or TypeCode.UInt64 => ulong.MaxValue,
        _ => throw new InvalidOperationException("Flags require an enum or an 8/16/32/64-bit integral member."),
    };

    internal static ulong Read(object value, Type type)
    {
        var signed = Type.GetTypeCode(Storage(type)) is TypeCode.SByte or TypeCode.Int16 or TypeCode.Int32 or TypeCode.Int64;
        return (signed ? unchecked((ulong)Convert.ToInt64(value)) : Convert.ToUInt64(value)) & WidthMask(type);
    }

    internal static object Write(ulong bits, Type type)
    {
        type = Nullable.GetUnderlyingType(type) ?? type;
        object number = Type.GetTypeCode(Storage(type)) switch
        {
            TypeCode.SByte => unchecked((sbyte)bits),
            TypeCode.Byte => unchecked((byte)bits),
            TypeCode.Int16 => unchecked((short)bits),
            TypeCode.UInt16 => unchecked((ushort)bits),
            TypeCode.Int32 => unchecked((int)bits),
            TypeCode.UInt32 => unchecked((uint)bits),
            TypeCode.Int64 => unchecked((long)bits),
            TypeCode.UInt64 => bits,
            _ => throw new InvalidOperationException("Flags require an integral member."),
        };
        return type.IsEnum ? Enum.ToObject(type, number) : number;
    }
}
