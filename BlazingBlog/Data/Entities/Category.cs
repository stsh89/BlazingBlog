using System.ComponentModel.DataAnnotations;
using BlazingBlog.Data;

namespace BlazingBlog.Data.Entities
{
	public class Category
	{
		public short Id { get; set; }

		[Required, MaxLength(50)]
		public string Name { get; set; }

		[MaxLength(75)]
		public string Slug { get; set; }

		public bool ShowOnNavbar { get; set; }

		public static Category[] GetSeedCategories()
		{
			return [
				new Category { Name = "C#", Slug = "c-sharp", ShowOnNavbar = true },
				new Category { Name = "Blazor", Slug = "blazor", ShowOnNavbar = true },
			];
		}
	}
}
