using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace WebApp.Client.Pages.Examples;

public partial class Form : ComponentBase
{
    [Parameter]
    public string? Action { get; set; } = default!;

    public UserModel UserModel { get; set; } = default!;
    public EditContext EditContext { get; set; } = default!;
    public ValidationMessageStore ValidationMessageStore { get; set; } = default!;

    public MudTextField<string> NameRef { get; set; } = default!;
    public MudTextField<string> EmailRef { get; set; } = default!;
    public MudNumericField<int> AgeRef { get; set; } = default!;

    bool isLoading;

    protected override void OnInitialized()
    {
        ResetForm();
    }

    protected override async Task OnInitializedAsync()
    {
        if (Action?.ToUpper() == "EDIT")
        {
            await Task.Delay(1_000);

            UserModel.Name = "ADMIN";
        }
    }

    void ResetForm()
    {
        UserModel = new UserModel();
        EditContext = new EditContext(UserModel);
        ValidationMessageStore = new ValidationMessageStore(EditContext);

        EditContext.OnFieldChanged += async (sender, args) =>
        {
            await Validate();
            EditContext.NotifyValidationStateChanged();
        };
    }

    async Task Validate()
    {
        await Task.Delay(1_000);
        ValidationMessageStore.Clear();

        if (UserModel.Name.ToUpper() == "ADMIN")
        {
            ValidationMessageStore.Add(EditContext.Field(nameof(UserModel.Name)), "Name cannot be ADMIN.");
        }
    }

    async Task HandleValidSubmit()
    {
        isLoading = true;

        try
        {
            await Validate();
            var isValid = EditContext.Validate();

            if (isValid)
            {
                ResetForm();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        isLoading = false;
    }
}

public class UserModel
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue)]
    public int Age { get; set; }
}
