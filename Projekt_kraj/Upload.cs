using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace Projekt_kraj
{
    class Upload
    {
        private readonly string pathToFile;
        private readonly string userName;
        private readonly string password;
        private readonly string serverAdress;
        public Upload(string pathToFile, string userName, string password, string serverAdress)
        {
            this.pathToFile = pathToFile;
            this.userName = userName;
            this.password = password;
            this.serverAdress = serverAdress;
        }

        public void UploadFileToFtp()
        {
            var fileName = Path.GetFileName(pathToFile);
            var request = (FtpWebRequest)WebRequest.Create(serverAdress + fileName);

            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(userName, password);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            using (var fileStream = File.OpenRead(pathToFile))
            {
                using (var requestStream = request.GetRequestStream())
                {
                    fileStream.CopyTo(requestStream);
                    requestStream.Close();
                }
            }

            var response = (FtpWebResponse)request.GetResponse();
            Console.WriteLine("Upload done: {0}", response.StatusDescription);
            response.Close();
        }
    }
}
