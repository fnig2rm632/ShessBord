namespace ShessBord.Interfaces;

public interface IAppValidationDataService
{
    (bool isValid, string massage) PasswordValidator(string password, IAppLocalizationService localization);
    (bool isValid, string massage) EmailValidator(string email, IAppLocalizationService localization);
    (bool isValid, string massage) NameValidator(string name, IAppLocalizationService localization);
    (bool isValid, string massage) AllValidator(string name,string email,string password, IAppLocalizationService localization);
}