using BlazingBlog.Data;
using BlazingBlog.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

internal static class AdminAccount
{
	public const string Name = "Abhay Prince";
	public const string Email = "abhay@email.com";
	public const string Role = "Admin";
	public const string Password = "Pa$$w0rd";
}

namespace BlazingBlog.Services
{
	public interface ISeedService
	{
		Task SeedDataAsync();
	}

	public class SeedService : ISeedService
	{
		private readonly ApplicationDbContext context;
		private readonly IUserStore<ApplicationUser> userStore;
		private readonly UserManager<ApplicationUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public SeedService(ApplicationDbContext context,
			IUserStore<ApplicationUser> userStore,
			UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			this.context = context;
			this.userStore = userStore;
			this.userManager = userManager;
			this.roleManager = roleManager;
		}

		public async Task SeedDataAsync()
		{
			var foundAccount = await roleManager.FindByNameAsync(AdminAccount.Role);

			if (foundAccount == null)
			{
				var adminRole = new IdentityRole(AdminAccount.Role);
				var result = await roleManager.CreateAsync(adminRole);

				if (!result.Succeeded)
				{
					var errorDescription = result.Errors.Select(e => e.Description);
					var exceptionDescription = string.Join(Environment.NewLine, errorDescription);

					throw new Exception($"Error during user role creation: {exceptionDescription}");
				}
			}

			var adminUser = await userManager.FindByEmailAsync(AdminAccount.Email);
			if (adminUser is null)
			{
				adminUser = new ApplicationUser();
				adminUser.Name = AdminAccount.Name;

				await userStore.SetUserNameAsync(adminUser, AdminAccount.Email, CancellationToken.None);

				var emailStore = (IUserEmailStore<ApplicationUser>)userStore;
				await emailStore.SetEmailAsync(adminUser, AdminAccount.Email, CancellationToken.None);
				var result = await userManager.CreateAsync(adminUser, AdminAccount.Password);

				if (!result.Succeeded)
				{
					var errorDescription = result.Errors.Select(e => e.Description);
					var exceptionDescription = string.Join(Environment.NewLine, errorDescription);

					throw new Exception($"Error during admin user creation: {exceptionDescription}");
				}
			}

			var categoriesExists = await context.Categories.AsNoTracking().AnyAsync();
			if (!categoriesExists)
			{
				await context.Categories.AddRangeAsync(Category.GetSeedCategories());
				await context.SaveChangesAsync();
			}
		}
	}
}
