using System;
using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Microsoft.AspNetCore.Identity;

namespace Hiroshima.Maas.Common.Infrastructure.MessageHandler
{
    public class MessageHandler : IMessageHandler
    {
        public string GetMessage(ErrorMessagesEnum message)
        {
            switch (message)
            {
                case ErrorMessagesEnum.EntityNull:
                    return "The request cannot process";
                //return "The entity passed is null";
                case ErrorMessagesEnum.ModelValidation:
                    return "The request data is not correct. Additional information: {0}";
                case ErrorMessagesEnum.NullException:
                    return "NullException";
                // return "The request cannot process";
                case ErrorMessagesEnum.AuthUserDoesNotExists:
                    return "AuthUserDoesNotExists";
                //return "The user doesn't not exists";
                case ErrorMessagesEnum.UserAlreadyExist:
                    return "UserAlreadyExist";
                // return "The user is already exists";
                case ErrorMessagesEnum.AuthWrongCredentials:
                    return "AuthWrongCredentials";
                //return "Invalid credential";
                case ErrorMessagesEnum.AuthCannotCreate:
                    return "Cannot create user";
                case ErrorMessagesEnum.AuthCannotDelete:
                    return "Cannot delete user";
                case ErrorMessagesEnum.AuthCannotUpdate:
                    return "Cannot update user";
                case ErrorMessagesEnum.NotValidInformations:
                    return "NotValidInformations";
                //return "Invalid informations";
                case ErrorMessagesEnum.AuthCannotRetrieveToken:
                    return "Cannot retrieve token";
                case ErrorMessagesEnum.InValidPassword:
                    return "InValidPassword";
                   // return "Invalid password";
                case ErrorMessagesEnum.InValidPassInformation:
                    return "InValidPassInformation";
                //return "No pass information available";
                case ErrorMessagesEnum.InValidPassDescription:
                    return "InValidPassDescription";
                //return "No pass description available";
                case ErrorMessagesEnum.InValidPTOInformation:
                    return "InValidPTOInformation";
                //return "No PTO information available";
                case ErrorMessagesEnum.InValidPTODescription:
                    return "InValidPTODescription";
                // return "No PTO description available";
                case ErrorMessagesEnum.InValidTravellerInformation:
                    return "InValidTravellerInformation";
                //return "No traveller information available";
                case ErrorMessagesEnum.InValidConfiguration:
                    return "InValidConfiguration";
                //return "No configuration available";
                case ErrorMessagesEnum.InValidLanguageInformation:
                    return "InValidLanguageInformation";
                //return "No language has been associatted with Pass description";
                case ErrorMessagesEnum.BookingWithInvalidDeviceId:
                    return "BookingWithInvalidDeviceId";
                //return "Seems like you haven't book any pass yet";
                case ErrorMessagesEnum.InValidPassengerInformation:
                    return "InValidPassengerInformation";
                //return "Please add passenger information";
                case ErrorMessagesEnum.InValidQRCode:
                    return "InValidQRCode";
                //return "Invalid QR code information";
                case ErrorMessagesEnum.InValidQRConfiguration:
                    return "InValidQRConfiguration";
                //return "Invalid QR code information";

                default:
                    throw new ArgumentOutOfRangeException(nameof(message), message, null);
            }
        }
        public string GetSuccessMessage(SuccessMessagesEnum message)
        {
            switch (message)
            {
                case SuccessMessagesEnum.SuccessfullySaved:
                    return "SuccessfullySaved";
                case SuccessMessagesEnum.SuccessfullyUpdated:
                    return "SuccessfullyUpdated";
                case SuccessMessagesEnum.SuccessfullyDeleted:
                    return "SuccessfullyDeleted";
                case SuccessMessagesEnum.SuccessfullyLoggedIn:
                    return "SuccessfullyLoggedIn";
                case SuccessMessagesEnum.SuccessfullRegister:
                    return "SuccessfullRegister";
                case SuccessMessagesEnum.SuccessfullyActivated:
                    return "SuccessfullyActivated";
                case SuccessMessagesEnum.SuccessfullySubmitted:
                    return "SuccessfullySubmitted";
                default:
                    throw new ArgumentOutOfRangeException(nameof(message), message, null);
            }

        }

        public string ErrorIdentityResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {

            }

            return string.Empty;
        }
    }
}
