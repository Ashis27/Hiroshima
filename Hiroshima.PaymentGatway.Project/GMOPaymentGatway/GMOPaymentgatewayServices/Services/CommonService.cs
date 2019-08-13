//using GMOPaymentgatewayServices.IServices;
//using GMOPaymentgatewayServices.ViewModel;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;

//namespace GMOPaymentgatewayServices.Services
//{
//   public  class CommonService: ICommonService
//    {
//        public CommonService()
//        {

//        }
//        public string GeneratePGForm(string paymentUrl, Dictionary<string, string> parameters)
//        {
//            string outputHTML = "<html>";
//            outputHTML += "<head>";
//            outputHTML += "<title>Merchant Check Out Page</title>";
//            outputHTML += "</head>";
//            outputHTML += "<body>";
//            outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
//            outputHTML += "<form method='post' action='" + paymentUrl + "' name='f1'>";
//            outputHTML += "<table border='1'>";
//            outputHTML += "<tbody>";
//            foreach (string key in parameters.Keys)
//            {
//                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
//            }
//            // outputHTML += "<input type='hidden' name='SHOPPASSSTRING' value='" + shopPassString + "'>";
//            outputHTML += "</tbody>";
//            outputHTML += "</table>";
//            outputHTML += "<script type='text/javascript'>";
//            outputHTML += "document.f1.submit();";
//            outputHTML += "</script>";
//            outputHTML += "</form>";
//            outputHTML += "</body>";
//            outputHTML += "</html>";
//            return outputHTML;
//        }
//        protected string GenerateMD5SignatureForGMO(string input)
//        {
//            MD5 md5Hash = MD5.Create();
//            // Convert the input string to a byte array and compute the hash.
//            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

//            // Create a new Stringbuilder to collect the bytes
//            // and create a string.
//            StringBuilder sBuilder = new StringBuilder();

//            // Loop through each byte of the hashed data 
//            // and format each one as a hexadecimal string.
//            for (int i = 0; i < data.Length; i++)
//            {
//                sBuilder.Append(data[i].ToString("x2"));
//            }

//            // Return the hexadecimal string.
//            return sBuilder.ToString();
//        }
//    }
//}
