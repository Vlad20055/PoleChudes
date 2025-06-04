using Domain.Entities;
using Domain.Interfaces;
using System.Text;
using System.Security.Cryptography;
using UI.Services;

namespace UI;

public partial class LoginPage : ContentPage
{
    private readonly IUserRepository _userRepo;
    private readonly CurrentUserService _currentUser;

    public LoginPage(IUserRepository userRepository, CurrentUserService currentUser)
    {
        InitializeComponent();
        _userRepo = userRepository;
        _currentUser = currentUser;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string login = LoginEntry.Text?.Trim() ?? string.Empty;
        string password = PasswordEntry.Text?.Trim() ?? string.Empty;

        if (login.Length < 3 || password.Length < 3)
        {
            await DisplayAlert("Ошибка", "Логин и пароль должны быть минимум 3 символа.", "OK");
            return;
        }

        string passwordHash = ComputeSha256Hash(password);

        try
        {
            var user = await _userRepo.AuthenticateAsync(login, passwordHash);
            if (user != null)
            {
                GamePage? gamePage = null;
                string name = LoginEntry.Text?.Trim() ?? string.Empty;
                _currentUser.UserName = name;
                if (Handler != null && Handler.MauiContext != null) gamePage = Handler.MauiContext.Services.GetService<GamePage>()!;
                
                await Navigation.PushAsync(gamePage);
            }
            else
            {
                await DisplayAlert("Ошибка", "Неверный логин или пароль.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", $"Не удалось войти: {ex.Message}", "OK");
        }
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        string login = LoginEntry.Text?.Trim() ?? string.Empty;
        string password = PasswordEntry.Text?.Trim() ?? string.Empty;

        if (login.Length < 3 || password.Length < 3)
        {
            await DisplayAlert("Ошибка", "Логин и пароль должны быть минимум 3 символа.", "OK");
            return;
        }

        string passwordHash = ComputeSha256Hash(password);

        var newUser = new User
        {
            Name = login, // пока Name = Login
            Login = login,
            PasswordHash = passwordHash
        };

        try
        {
            bool created = await _userRepo.RegisterAsync(newUser);
            if (created)
            {
                await DisplayAlert("Успех", "Регистрация прошла успешно! Теперь войдите.", "OK");
            }
            else
            {
                await DisplayAlert("Ошибка", "Пользователь с таким логином уже существует.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", $"Не удалось зарегистрироваться: {ex.Message}", "OK");
        }
    }

    private static string ComputeSha256Hash(string raw)
    {
        using var sha = SHA256.Create();
        byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(raw));
        var sb = new StringBuilder();
        foreach (var b in bytes)
            sb.Append(b.ToString("x2"));
        return sb.ToString();
    }
}