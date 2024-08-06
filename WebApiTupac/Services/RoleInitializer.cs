using Microsoft.AspNetCore.Identity;
using WebApiTupac.Entities;

namespace WebApiTupac.Services
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();

            string[] roleNames = { "Administrador", "Docente", "Alumno" };

            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    // Crear los roles y sembrar en la base de datos
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }


            // Crear un administrador predeterminado
            var adminUserName = "admin";
            var adminEmail = "admin@example.com";
            var adminPassword = "Admin@123";

            IdentityRole role = await roleManager.FindByNameAsync("Administrador");

            var adminUser = new Usuario
            {
                UserName = adminUserName,
                Nombre = adminUserName,
                Apellido = adminUserName,
                Email = adminEmail,
                EmailConfirmed = true,
            };

            var user = await userManager.FindByEmailAsync(adminEmail);
            if (user == null)
            {
                var createUser = await userManager.CreateAsync(adminUser, adminPassword);
                if (createUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Administrador");
                }
            }
        }
    }
}
