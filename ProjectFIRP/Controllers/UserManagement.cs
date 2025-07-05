// Controllers/UserManagementController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectFIRP.Models;

namespace ProjectFIRP.Controllers
{
    [Authorize] // Pastikan hanya user yang sudah login yang bisa akses
    public class UserManagementController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: UserManagement
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDisplayModels = new List<UserDisplayModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDisplayModels.Add(new UserDisplayModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Roles = roles.ToList()
                });
            }

            var viewModel = new UserManagementViewModel
            {
                Users = userDisplayModels,
                CreateUser = new CreateUserViewModel() // 🛠️ ini penting
            };

            return View(viewModel);
        }

        // POST: UserManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(Prefix = "CreateUser")] CreateUserViewModel model)
        {
            Console.WriteLine("=== MULAI CREATE USER ===");
            Console.WriteLine($"Data yang diterima:\n - Username: {model.UserName}\n - Email: {model.Email}\n - Password: {model.Password}\n - ConfirmPassword: {model.ConfirmPassword}\n - Role: {model.Role}");

            if (ModelState.IsValid)
            {
                Console.WriteLine("✓ ModelState valid. Lanjut ke pembuatan user...");

                var user = new IdentityUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = true
                };

                Console.WriteLine($"→ Membuat objek IdentityUser dengan:\n   • UserName: {user.UserName}\n   • Email: {user.Email}");

                var result = await _userManager.CreateAsync(user, model.Password);
                Console.WriteLine("✓ Memanggil _userManager.CreateAsync()...");

                if (result.Succeeded)
                {
                    Console.WriteLine("✓ User berhasil dibuat!");

                    Console.WriteLine($"→ Mengecek apakah role '{model.Role}' sudah ada...");
                    if (!await _roleManager.RoleExistsAsync(model.Role))
                    {
                        Console.WriteLine($"⚠ Role '{model.Role}' belum ada, membuat role...");
                        var roleResult = await _roleManager.CreateAsync(new IdentityRole(model.Role));
                        Console.WriteLine("✓ Role berhasil dibuat? " + (roleResult.Succeeded ? "Ya" : "Tidak"));
                    }
                    else
                    {
                        Console.WriteLine($"✓ Role '{model.Role}' sudah ada.");
                    }

                    Console.WriteLine($"→ Menambahkan user ke role '{model.Role}'...");
                    await _userManager.AddToRoleAsync(user, model.Role);
                    Console.WriteLine("✓ User berhasil ditambahkan ke role.");

                    TempData["SuccessMessage"] = "User berhasil dibuat!";
                    Console.WriteLine("✓ Redirect ke Index");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Console.WriteLine("✗ Gagal membuat user. Daftar error:");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($" - {error.Code}: {error.Description}");
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            else
            {
                Console.WriteLine("✗ ModelState tidak valid. Daftar error:");
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($" - Field '{key}': {error.ErrorMessage}");
                    }
                }
            }

            Console.WriteLine("→ Mengambil ulang data user untuk ditampilkan kembali ke View...");
            var users = await _userManager.Users.ToListAsync();
            var userDisplayModels = new List<UserDisplayModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDisplayModels.Add(new UserDisplayModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    CreatedDate = DateTime.Now, // Perlu disesuaikan jika Anda punya tanggal dari DB
                    IsActive = true,            // Jika punya kolom status aktif di DB, ambil dari sana
                    Roles = roles.ToList()
                });
            }

            var viewModel = new UserManagementViewModel
            {
                Users = userDisplayModels,
                CreateUser = model
            };

            Console.WriteLine("✗ Return View Index karena proses pembuatan gagal.");
            Console.WriteLine("=== SELESAI ===");
            return View("Index", viewModel);
        }


        // GET: UserManagement/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = roles.FirstOrDefault() ?? "User",
                IsActive = true
            };

            return View(model);
        }

        // POST: UserManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = model.UserName;
                user.Email = model.Email;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    // Update roles
                    var currentRoles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);

                    if (!await _roleManager.RoleExistsAsync(model.Role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(model.Role));
                    }

                    await _userManager.AddToRoleAsync(user, model.Role);

                    TempData["SuccessMessage"] = "User berhasil diupdate!";
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        // GET: UserManagement/ChangePassword/5
        public async Task<IActionResult> ChangePassword(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new ChangePasswordViewModel
            {
                UserId = user.Id,
                UserName = user.UserName
            };

            return View(model);
        }

        // POST: UserManagement/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    return NotFound();
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Password berhasil diubah!";
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        // POST: UserManagement/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "User berhasil dihapus!";
            }
            else
            {
                TempData["ErrorMessage"] = "Gagal menghapus user!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}