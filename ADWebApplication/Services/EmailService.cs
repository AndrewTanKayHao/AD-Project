using Resend;

namespace ADWebApplication.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _cfg;
        private readonly HttpClient _http;

        public EmailService(IConfiguration cfg, HttpClient http)
        {
            _cfg = cfg;
            _http = http;
        }

        public async Task SendOtpEmail(string toEmail, string otp)
        {
            var apiKey = _cfg["EmailSettings:ResendApiKey"]
                ?? throw new InvalidOperationException("ResendApiKey missing.");
            var from = _cfg["EmailSettings:FromAddress"] ?? "onboarding@resend.dev";

            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            var payload = new
            {
                from,
                to = new[] { toEmail },
                subject = "Login OTP",
                text = $"Your OTP is {otp}. It is valid for 1 minute."
            };

            var resp = await _http.PostAsJsonAsync("https://api.resend.com/emails", payload);
            resp.EnsureSuccessStatusCode();
        }
    }
}
