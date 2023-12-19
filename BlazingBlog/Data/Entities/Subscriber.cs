﻿using System.ComponentModel.DataAnnotations;

namespace BlazingBlog.Data.Entities
{
	public class Subscriber
	{
		public long Id { get; set; }

		[Required, EmailAddress, MaxLength(150)]
		public string Email { get; set; }

		[Required, MaxLength(25)]
		public string Name { get; set; }

		public DateTime SubscribedOn { get; set; }
	}
}
