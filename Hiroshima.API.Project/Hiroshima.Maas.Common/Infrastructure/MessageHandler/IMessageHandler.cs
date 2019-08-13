using Microsoft.AspNetCore.Identity;

namespace Hiroshima.Maas.Common.Infrastructure.MessageHandler
{
    public interface IMessageHandler
    {
        string GetMessage(ErrorMessagesEnum message);
        string GetSuccessMessage(SuccessMessagesEnum message);
        string ErrorIdentityResult(IdentityResult result);
    }


    public enum ErrorMessagesEnum
    {
        EntityNull = 1,
        ModelValidation = 2,
        AuthUserDoesNotExists = 3,
        AuthWrongCredentials = 4,
        AuthCannotCreate = 5,
        AuthCannotDelete = 6,
        AuthCannotUpdate = 7,
        AuthNotValidInformations = 8,
        AuthCannotRetrieveToken = 9,
        NullReferenceException=10,
        UserAlreadyExist=11,
        NullException = 12,
        NotValidInformations=13,
        InValidPassword=14,
        InValidPassInformation=15,
        InValidPassDescription=16,
        InValidPTOInformation=17,
        InValidPTODescription=18,
        InValidTravellerInformation=19,
        InValidConfiguration=20,
        InValidLanguageInformation=21,
        BookingWithInvalidDeviceId=22,
        InValidPassengerInformation=23,
        InValidQRCode=24,
        InValidQRConfiguration = 25
    }
    public enum SuccessMessagesEnum
    {
        SuccessfullySaved = 1,
        SuccessfullyUpdated = 2,
        SuccessfullyDeleted = 3,
        SuccessfullRegister = 4,
        SuccessfullyLoggedIn = 5,
        SuccessfullyActivated= 6,
        SuccessfullySubmitted=7
    }

}
