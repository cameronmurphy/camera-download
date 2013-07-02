using System;
using System.Collections.Generic;
using System.Text;

namespace Camurphy.UpdateService
{
	public class ProductVersion : IComparable<ProductVersion>
	{
		public ProductVersion(string versionString)
		{
			string[] version = versionString.Split('.');

			Major = Int32.Parse(version[0]);
			Minor = Int32.Parse(version[1]);
			Bugfix = Int32.Parse(version[2]);
		}

		public int Major { get; set; }
		public int Minor { get; set; }
		public int Bugfix { get; set; }

		#region IComparable<ProductVersion> Members

		public int CompareTo(ProductVersion other)
		{
			if (Major > other.Major)
			{
				return 1;
			}
			else if (Major == other.Major)
			{
				if (Minor > other.Minor)
				{
					return 1;
				}
				else if (Minor == other.Minor)
				{
					if (Bugfix > other.Bugfix)
					{
						return 1;
					}
					else if (Bugfix == other.Bugfix)
					{
						return 0;
					}
					else
					{
						return -1;
					}
				}
				else
				{
					return -1;
				}
			}
			else
			{
				return -1;
			}
		}

		#endregion
	}
}
