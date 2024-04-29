using System;
namespace LibraryManager.Core.ValueObjects
{
	public record LocationInfo(string Cep, string Address, string District, string City, string State)
	{
	}
}

