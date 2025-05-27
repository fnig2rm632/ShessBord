using System.Linq;
using System.Text.RegularExpressions;
using ShessBord.Interfaces;

namespace ShessBord.Services;

public class AppValidationDataService : IAppValidationDataService
{
    readonly Regex _allowedCharsRegex = new Regex(@"^[a-zA-Z0-9!@#$%\^&*()_+=\[{\]};:<>|\.\/\?,\-]*$");
    
    public (bool isValid, string massage) PasswordValidator(string password, IAppLocalizationService localization)
    {
        if (string.IsNullOrEmpty(password))
            return (false, localization["PasswordCannotBeEmpty"]);
        
        if (!Regex.IsMatch(password, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]"))
            return (false, localization["PasswordMustContainSpecialChar"]);

        if (password.Length < 8)
            return (false, localization["PasswordMustBeAtLeast8Characters"]);

        if (!Regex.IsMatch(password, "[a-z]"))
            return (false, localization["PasswordMustContainLowercase"]);

        if (!Regex.IsMatch(password, "[A-Z]"))
            return (false, localization["PasswordMustContainUppercase"]);
        
        if (!_allowedCharsRegex.IsMatch(password))
            return (false, localization["PasswordMustNotContainSpecialCharacters"]);
        
        if (!Regex.IsMatch(password, "[0-9]"))
            return (false, localization["PasswordMustContainNumber"]);

        if (password.Contains(" "))
            return (false, localization["PasswordCannotContainSpaces"]);
        
        return (true, localization["IsValid"]);
    }

    public (bool isValid, string massage) EmailValidator(string email, IAppLocalizationService localization)
    {
        if (string.IsNullOrEmpty(email))
            return (false, localization["EmailCannotBeEmpty"]);
        
        if (!_allowedCharsRegex.IsMatch(email))
            return (false, localization["EmailMustNotContainSpecialCharacters"]);
        
        try
        {
            var mailAddress = new System.Net.Mail.MailAddress(email);
            if (mailAddress.Address != email.Trim())
                return (false, localization["IsNotAnEmail"]);
        }
        catch
        {
            return (false, localization["IsNotAnEmail"]);
        }
        
        return (true, localization["IsValid"]);
    }
    
    public (bool isValid, string massage) NameValidator(string name, IAppLocalizationService localization)
    {
        if (string.IsNullOrEmpty(name))
            return (false, localization["NameCannotBeEmpty"]);
        
        if (!_allowedCharsRegex.IsMatch(name))
            return (false, localization["NameMustNotContainSpecialCharacters"]);
        
        return (true, localization["IsValid"]);
    }
    
    public (bool isValid, string massage) AllValidator(string name, string email,string password, IAppLocalizationService localization)
    {
        var validator = NameValidator(name, localization);

        if (validator.isValid)
        {
            validator = EmailValidator(email, localization);

            if (validator.isValid)
            {
                validator = PasswordValidator(password, localization);
            }
        }

        return validator;
    }
}