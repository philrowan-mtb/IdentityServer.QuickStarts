using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace ApiClient
{
    public class Program
    {
		public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

	    private static async Task MainAsync()
		{
			Console.Title = "IdentityServer4 - Api Client";
			// discover endpoints from metadata
			var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
			while (true)
			{
				var type = PromptAccessType();
				TokenResponse tokenResponse;
				switch (type)
				{
					case 1:
						tokenResponse = await ClientCredentialAccess(disco);
						break;
					case 2:
						tokenResponse = await ResourceOwnerCredentialAccess(disco);
						break;
					case 0:
					default:
						Console.WriteLine("Ok, Bye.");
						return;
				}
				if (tokenResponse != null)
				{
					await CallApi(tokenResponse);
				}
			}
		}

	    private static int PromptAccessType()
	    {
		    Console.WriteLine("What type of access?");
		    Console.WriteLine("1: Client Credential");
		    Console.WriteLine("2: Resource Owner Credential");
		    Console.WriteLine("0: Quit");
		    var key = Console.ReadKey();
		    return int.Parse(key.KeyChar.ToString());
	    }

	    private static async Task CallApi(TokenResponse tokenResponse)
	    {
			// call api
		    var client = new HttpClient();
		    client.SetBearerToken(tokenResponse.AccessToken);

		    var response = await client.GetAsync("http://localhost:5001/identity");
		    if (!response.IsSuccessStatusCode)
		    {
			    Console.WriteLine(response.StatusCode);
		    }
		    else
		    {
			    var content = await response.Content.ReadAsStringAsync();
			    Console.WriteLine(JArray.Parse(content));
		    }
		}

	    private static async Task<TokenResponse> ResourceOwnerCredentialAccess(DiscoveryResponse disco)
	    {
			// request token
		    var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
		    var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "api1");

		    if (tokenResponse.IsError)
		    {
			    Console.WriteLine(tokenResponse.Error);
			    return tokenResponse;
		    }

		    Console.WriteLine(tokenResponse.Json);
		    Console.WriteLine("\n\n");
		    return tokenResponse;
	    }

		private static async Task<TokenResponse> ClientCredentialAccess(DiscoveryResponse disco)
		{
			// request token
			var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
			var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

			if (tokenResponse.IsError)
			{
				Console.WriteLine(tokenResponse.Error);
				return tokenResponse;
			}

			Console.WriteLine(tokenResponse.Json);
			Console.WriteLine("\n\n");

			return tokenResponse;
		}
	}
}
