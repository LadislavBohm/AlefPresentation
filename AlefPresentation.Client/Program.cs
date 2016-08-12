using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlefPresentation.Model;
using RestSharp;

namespace AlefPresentation.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("http://localhost:2883/api");
            var getRequest = new RestRequest("lecturer",Method.GET);

            var result = client.Execute<List<Lecturer>>(getRequest);

            foreach (var lecturer in result.Data)
            {
                Console.WriteLine(lecturer);
            }


            var postRequest = new RestRequest("lecturer",Method.PUT);


            var postResult = client.Execute(postRequest);

            Console.ReadLine();
        }
    }
}
