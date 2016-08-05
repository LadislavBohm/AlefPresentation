using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlefPresentation.Model;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers;

namespace AlefPresentation.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = CreateClient();
            var lecturerRequest = new RestRequest("lecturer", Method.GET);
            var lecturerResponse = client.Execute<List<Lecturer>>(lecturerRequest);

            foreach (var lecturer in lecturerResponse.Data)
            {
                Console.WriteLine(lecturer);
            }

            Console.WriteLine("\n\n");

            var presentationRequest = new RestRequest("presentation/nonempty",Method.GET);
            var presentationResponse = client.Execute<List<Presentation>>(presentationRequest);
            var presentations = JsonConvert.DeserializeObject<IList<Presentation>>(presentationResponse.Content);

            foreach (var presentation in presentations)
            {
                Console.WriteLine(presentation + "\n");
            }

            Console.WriteLine("\n\n");

            var deleteRequest = new RestRequest("presentation",Method.DELETE);

            //smazani jen jedne prezentace
            deleteRequest.AddQueryParameter("id", presentations.First().Id.ToString());

            //smazani vsech prezentaci
            //deleteRequest.AddJsonBody(presentations.Select(p => p.Id.ToString()));

            var deleteResponse = client.Execute(deleteRequest);
            presentationResponse = client.Execute<List<Presentation>>(presentationRequest);
            presentations = JsonConvert.DeserializeObject<IList<Presentation>>(presentationResponse.Content);

            foreach (var presentation in presentations)
            {
                Console.WriteLine(presentation + "\n");
            }

            Console.ReadKey();
        }

        private static IRestClient CreateClient()
        {
            var result = new RestClient("http://localhost:2883/api")
            {
                Authenticator = new HttpBasicAuthenticator("admin", "123456")
            };
            
            return result;
        }
    }
}
