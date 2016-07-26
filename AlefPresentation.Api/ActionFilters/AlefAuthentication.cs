using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace AlefPresentation.Api.ActionFilters
{
    public class AlefAuthentication : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var authData = ExtractUserNameAndPassword(context.Request.Headers.Authorization?.Parameter);

            if (authData == null)
            {
                context.ErrorResult = new ResponseMessageResult(context.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,"No Auth data"));
                return;
            }

            if (authData.Item1 == "admin" && authData.Item2 == "123456")
            {
                context.Principal = new GenericPrincipal(new GenericIdentity(authData.Item1), new[] {"admin", "user", "lecturer"});
                return;
            }
            if (authData.Item1 == "user" && authData.Item2 == "123456")
            {
                context.Principal = new GenericPrincipal(new GenericIdentity(authData.Item1), new[] { "user" });
                return;
            }

            context.ErrorResult = new ResponseMessageResult(context.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Wrong username or password."));
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        private static Tuple<string, string> ExtractUserNameAndPassword(string authorizationParameter)
        {
            try
            {
                if (string.IsNullOrEmpty(authorizationParameter)) return null;

                var credentialBytes = Convert.FromBase64String(authorizationParameter);
                Encoding encoding = Encoding.ASCII;
                encoding = (Encoding)encoding.Clone();
                encoding.DecoderFallback = DecoderFallback.ExceptionFallback;

                var decodedCredentials = encoding.GetString(credentialBytes);
                if (String.IsNullOrEmpty(decodedCredentials)) return null;
                int colonIndex = decodedCredentials.IndexOf(':');

                if (colonIndex == -1) return null;

                return new Tuple<string, string>(decodedCredentials.Substring(0, colonIndex), decodedCredentials.Substring(colonIndex + 1));
            }
            catch (Exception e) when (e is FormatException || e is DecoderFallbackException)
            {
                return null;
            }
            catch (Exception e)
            {
                return null; //neco je spatne
            }
        }
    }
}