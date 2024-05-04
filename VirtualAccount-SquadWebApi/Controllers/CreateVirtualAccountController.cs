
[ApiController]
[Route("[virtual/create]")]
public class CreatVirtualAccountController : ControllerBase
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly ILogger<CreatVirtualAccountController> _logger;


    public VirtualAccountController(ILogger<CreatVirtualAccountController> logger, IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateVirtualAccount([FromBody] VirtualAccountRequest request)
    {
        // Prepare the JSON request body
        string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(request);

        // Create an HttpClient instance
        var client = _clientFactory.CreateClient();

        // Set the API URL
        var apiUrl = "https://sandbox-api-d.squadco.com/virtual-account";

        // Set the authorization header
        client.DefaultRequestHeaders.Add("Authorization", "sandbox_sk_bc74f1c3029ae493ed826988f56e1c337e2e806f989f");

        // Create the HTTP request content
        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        try
        {
            // Send the POST request to the API
            var response = await client.PostAsync(apiUrl, content);

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                string responseContent = await response.Content.ReadAsStringAsync();
                return Ok(responseContent);
            }
            else
            {
                // Return the status code if the request was not successful
                return StatusCode((int)response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            // Return any exception that occurred during the request
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    public class VirtualAccountRequest
    {
        public string CustomerIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNum { get; set; }
        public string Email { get; set; }
        public string Bvn { get; set; }
        public string Dob { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string BeneficiaryAccount { get; set; }
    }

}