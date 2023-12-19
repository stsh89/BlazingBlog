﻿using System.ComponentModel.DataAnnotations;
using BlazingBlog.Data;

namespace BlazingBlog.Data.Entities
{
	public class Category
	{
		public short Id { get; set; }

		[Required, MaxLength(50)]
		public string Name { get; set; }

		public string Slug { get; set; }

		public bool ShowOnNavbar { get; set; }
	}
}
