using System;

namespace open3mod
{
	// Token: 0x0200000D RID: 13
	public static class EnumExtensionsNet4Backported
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00004F1C File Offset: 0x0000311C
		public static bool HasFlag(this Enum variable, Enum value)
		{
			if (variable == null)
			{
				return false;
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!Enum.IsDefined(variable.GetType(), value))
			{
				throw new ArgumentException(string.Format("Enumeration type mismatch.  The flag is of type '{0}', was expecting '{1}'.", value.GetType(), variable.GetType()));
			}
			ulong num = Convert.ToUInt64(value);
			return (Convert.ToUInt64(variable) & num) == num;
		}
	}
}
